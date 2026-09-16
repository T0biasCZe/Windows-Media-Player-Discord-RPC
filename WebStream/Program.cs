using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using NAudio.Lame;
using NAudio.Wave;
using System;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace WebStream {
    public class Program {
        private const string WMP_BASE_URL = "http://localhost:42565";

        public static void Main(string[] args) {
            var builder = WebApplication.CreateBuilder(args);

            // Register HttpClient for proxying requests to the WinForms app
            builder.Services.AddHttpClient();

            var app = builder.Build();

            // Serve static files from the wwwroot folder (index.html, css, gfx)
            app.UseDefaultFiles();
            app.UseStaticFiles();

            // Proxy endpoint for Status (XML)
            app.MapGet("/api/status", async (HttpClient client, HttpContext context) => {
                try {
                    var response = await client.GetAsync($"{WMP_BASE_URL}/api/status");
                    var xml = await response.Content.ReadAsStringAsync();
                    context.Response.ContentType = "text/xml";
                    await context.Response.WriteAsync(xml);
                }
                catch {
                    context.Response.StatusCode = 500;
                }
            });

            // Proxy endpoint for Controls
            app.MapGet("/api/control", async (HttpClient client, HttpContext context) => {
                var action = context.Request.Query["action"];
                var pos = context.Request.Query["pos"];
                var query = $"?action={action}&pos={pos}";
                Console.WriteLine("RECEIVED ACTION: " + query);

                try {
                    var response = await client.GetAsync($"{WMP_BASE_URL}/api/control{query}");
                    context.Response.StatusCode = (int)response.StatusCode;
                    Console.WriteLine("ACTION: " + query + " STATUS: " + response.StatusCode);
                }
                catch {
                    context.Response.StatusCode = 500;
                }
            });

            // Proxy endpoint for Cover Art
            app.MapGet("/api/cover", async (HttpClient client, HttpContext context) => {
                try {
                    var response = await client.GetStreamAsync($"{WMP_BASE_URL}/");
                    context.Response.ContentType = "image/jpeg";
                    await response.CopyToAsync(context.Response.Body);
                }
                catch {
                    context.Response.StatusCode = 404;
                }
            });

            // Real-time WASAPI Loopback Audio Stream via MP3 (Icecast Protocol)
            app.MapGet("/api/audio", async (HttpContext context) => {
                context.Response.ContentType = "audio/mpeg";
                context.Response.Headers.Append("Cache-Control", "no-cache, no-store, must-revalidate");
                context.Response.Headers.Append("Connection", "keep-alive");

                context.Response.Headers.Append("icy-name", "WMP System Audio");
                context.Response.Headers.Append("icy-notice1", "<BR>This stream requires a shoutcast compatible player<BR>");

                var capture = new WasapiLoopbackCapture();

                var sourceFormat = capture.WaveFormat;
                int sourceRate = sourceFormat.SampleRate;
                int sourceChannels = sourceFormat.Channels;
                int bitsPerSample = sourceFormat.BitsPerSample;
                int bytesPerSample = bitsPerSample / 8;

                int rateDivider = (int)Math.Ceiling((double)sourceRate / 48000.0);
                int targetRate = sourceRate / rateDivider;
                int targetChannels = Math.Min(sourceChannels, 2);

                var targetFormat = new WaveFormat(targetRate, 16, targetChannels);

                var ms = new MemoryStream();
                var lameWriter = new LameMP3FileWriter(ms, targetFormat, 96);

                // Push initial ID3 headers instantly
                byte[] initialHeaders = ms.ToArray();
                ms.SetLength(0);
                if (initialHeaders.Length > 0) {
                    await context.Response.Body.WriteAsync(initialHeaders, 0, initialHeaders.Length, context.RequestAborted);
                    await context.Response.Body.FlushAsync(context.RequestAborted);
                }

                float GetSample(byte[] buffer, int index) {
                    if (bitsPerSample == 32) return BitConverter.ToSingle(buffer, index);
                    if (bitsPerSample == 16) return BitConverter.ToInt16(buffer, index) / 32768f;
                    if (bitsPerSample == 24) {
                        int val = buffer[index] | (buffer[index + 1] << 8) | ((sbyte)buffer[index + 2] << 16);
                        return val / 8388608f;
                    }
                    return 0f;
                }

                // NEW: A holding tank to build up larger chunks for IE11
                var httpBuffer = new MemoryStream();
                // 128kbps MP3 = 16 KB/s. Buffering 8 KB gives us ~0.5 seconds of audio per network transmission.
                const int MIN_CHUNK_SIZE = 4096;

                capture.DataAvailable += async (s, a) => {
                    try {
                        if (a.BytesRecorded == 0) return;

                        lock (ms) {
                            if (!ms.CanWrite) return;

                            int sourceBytesPerFrame = bytesPerSample * sourceChannels;
                            int totalFrames = a.BytesRecorded / sourceBytesPerFrame;
                            int targetFrames = totalFrames / rateDivider;
                            byte[] outBuffer = new byte[targetFrames * targetChannels * 2];
                            int outIndex = 0;

                            for (int i = 0; i < totalFrames; i += rateDivider) {
                                int sourceIndex = i * sourceBytesPerFrame;

                                float sampleL = GetSample(a.Buffer, sourceIndex);
                                if (sampleL > 1.0f) sampleL = 1.0f; else if (sampleL < -1.0f) sampleL = -1.0f;
                                short shortL = (short)(sampleL * 32767);
                                outBuffer[outIndex++] = (byte)(shortL & 0xFF);
                                outBuffer[outIndex++] = (byte)((shortL >> 8) & 0xFF);

                                if (targetChannels == 2) {
                                    float sampleR = 0;
                                    if (sourceChannels > 1) sampleR = GetSample(a.Buffer, sourceIndex + bytesPerSample);
                                    if (sampleR > 1.0f) sampleR = 1.0f; else if (sampleR < -1.0f) sampleR = -1.0f;
                                    short shortR = (short)(sampleR * 32767);
                                    outBuffer[outIndex++] = (byte)(shortR & 0xFF);
                                    outBuffer[outIndex++] = (byte)((shortR >> 8) & 0xFF);
                                }
                            }

                            lameWriter.Write(outBuffer, 0, outBuffer.Length);

                            // Instead of sending to the network instantly, pour it into our holding tank
                            if (ms.Length > 0) {
                                httpBuffer.Write(ms.GetBuffer(), 0, (int)ms.Length);
                                ms.SetLength(0);
                            }
                        }

                        // NEW: Only send data over HTTP if the holding tank has reached our chunk size threshold
                        if (httpBuffer.Length >= MIN_CHUNK_SIZE && !context.RequestAborted.IsCancellationRequested) {
                            byte[] chunkToStream = httpBuffer.ToArray();
                            httpBuffer.SetLength(0); // Reset the holding tank

                            await context.Response.Body.WriteAsync(chunkToStream, 0, chunkToStream.Length, context.RequestAborted);
                            await context.Response.Body.FlushAsync(context.RequestAborted);
                        }
                    }
                    catch (Exception) {
                        try { capture.StopRecording(); } catch { }
                    }
                };

                capture.StartRecording();

                var tcs = new TaskCompletionSource();
                context.RequestAborted.Register(() => {
                    try {
                        capture.StopRecording();
                        lameWriter.Dispose();
                        ms.Dispose();
                        httpBuffer.Dispose();
                        capture.Dispose();
                    }
                    catch { }
                    tcs.TrySetResult();
                });

                await tcs.Task;
            });

            app.Urls.Add("http://0.0.0.0:5114");

            app.Run();
        }
    }
}