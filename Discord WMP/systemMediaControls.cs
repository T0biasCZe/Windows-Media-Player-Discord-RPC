using AxWMPLib;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
/*using Windows.Foundation;
using Windows.Media;
using Windows.Media.Playback;
using Windows.Storage;
using Windows.Storage.FileProperties;
using Windows.Storage.Streams;*/

namespace Discord_WMP {
	public static class systemMediaControls {
		static RemotedWindowsMediaPlayer rm;
		/*public static void SystemControls_ButtonPressed(SystemMediaTransportControls sender, SystemMediaTransportControlsButtonPressedEventArgs args) {
			switch(args.Button) {
				case SystemMediaTransportControlsButton.Play:
					((WMPLib.IWMPPlayer4)rm.GetOcx()).controls.play();
					break;
				case SystemMediaTransportControlsButton.Pause:
					((WMPLib.IWMPPlayer4)rm.GetOcx()).controls.pause();
					break;
				case SystemMediaTransportControlsButton.Stop:
					((WMPLib.IWMPPlayer4)rm.GetOcx()).controls.stop();
					break;
				case SystemMediaTransportControlsButton.Next:
					((WMPLib.IWMPPlayer4)rm.GetOcx()).controls.next();
					break;
				case SystemMediaTransportControlsButton.Previous:
					((WMPLib.IWMPPlayer4)rm.GetOcx()).controls.previous();
					break;
			}
		}*/
		//static SystemMediaTransportControls systemControls = BackgroundMediaPlayer.Current.SystemMediaTransportControls;
		public static Form1 form1;
		public static Form1.playback_data data;
		public static string thumbnail_path;

		public static bool server_running = false;
		public static bool _run_server = true;
		public static bool registered_events = false;

		public static bool useMediaKeys = false;

		public static void update(Form1.playback_data data_, Form1 form) {
			rm = form.rm;
			data = data_;

			/*systemControls = BackgroundMediaPlayer.Current.SystemMediaTransportControls;

			if(data.play_state == WMPLib.WMPPlayState.wmppsUndefined || data.play_state == WMPLib.WMPPlayState.wmppsStopped) {
				systemControls.IsEnabled = false;
				systemControls.IsPlayEnabled = false;
				systemControls.IsPauseEnabled = false;
				systemControls.IsStopEnabled = false;
				systemControls.IsNextEnabled = false;
				systemControls.IsPreviousEnabled = false;
				systemControls.ButtonPressed -= SystemControls_ButtonPressed;
				registered_events = false;

				systemControls.DisplayUpdater.Update();
				return;
			}
			systemControls.IsEnabled = true;
			systemControls.IsPlayEnabled = true;
			systemControls.IsPauseEnabled = true;
			systemControls.IsStopEnabled = true;
			systemControls.IsNextEnabled = true;
			systemControls.IsPreviousEnabled = true;
			if(!registered_events) {
				systemControls.ButtonPressed += SystemControls_ButtonPressed;
				registered_events = true;
			}*/
			GetAlbumArt();

			if(!server_running) {
				server_running = true;
				StartServer();
			}
			else {
				checkRequests();
			}
			/*systemControls.DisplayUpdater.Type = MediaPlaybackType.Music;
			switch(data.media_type) {
				case "audio":
					systemControls.DisplayUpdater.Type = MediaPlaybackType.Music;
					break;
				case "video":
					systemControls.DisplayUpdater.Type = MediaPlaybackType.Video;
					break;
				case "image":
					systemControls.DisplayUpdater.Type = MediaPlaybackType.Image;
					break;
			}
			switch(data.play_state) {
				case WMPLib.WMPPlayState.wmppsPlaying:
					systemControls.PlaybackStatus = MediaPlaybackStatus.Playing;
					break;
					case WMPLib.WMPPlayState.wmppsPaused:
					systemControls.PlaybackStatus = MediaPlaybackStatus.Paused;
					break;
				case WMPLib.WMPPlayState.wmppsStopped:
					systemControls.PlaybackStatus = MediaPlaybackStatus.Stopped;
					break;
				case WMPLib.WMPPlayState.wmppsTransitioning:
					systemControls.PlaybackStatus = MediaPlaybackStatus.Changing;
					break;
				case WMPLib.WMPPlayState.wmppsReady:
					systemControls.PlaybackStatus = MediaPlaybackStatus.Changing;
					break;
				case WMPLib.WMPPlayState.wmppsUndefined:
					systemControls.PlaybackStatus = MediaPlaybackStatus.Closed;
					break;
				case WMPLib.WMPPlayState.wmppsMediaEnded:
					systemControls.PlaybackStatus = MediaPlaybackStatus.Stopped;
					break;
				default:
					systemControls.PlaybackStatus = MediaPlaybackStatus.Closed;
					break;

			}
			systemControls.DisplayUpdater.MusicProperties.Title = data.title;
			systemControls.DisplayUpdater.MusicProperties.Artist = data.artist;
			systemControls.DisplayUpdater.MusicProperties.AlbumTitle = data.album;
			systemControls.DisplayUpdater.Thumbnail = RandomAccessStreamReference.CreateFromUri(new Uri($"http://localhost:{Form1.random_port}"));
			//systemControls.DisplayUpdater.Thumbnail = RandomAccessStreamReference.CreateFromUri(new Uri("http://tobikcze.eu/files/822671241697689610.png"));
			systemControls.DisplayUpdater.Update();*/


			if(form.checkBox_mediakeys.Checked) {
				if(useMediaKeys == false) {
					useMediaKeys = true;
					Console.WriteLine("registering key listener");
					MediaKeys.rm = rm;
					MediaKeys.hookId = MediaKeys.SetHook(MediaKeys.HookCallback);
				}
			}
			else {
				if(useMediaKeys == true) {
					useMediaKeys = false;
					MessageBox.Show("Media keys will be disabled after restarting the application");
				}
			}
		}
		public static void GetAlbumArt() {
			thumbnail_path = "noalbumart.png"; // fallback image next to exe
			try {
				if(data.path != null) {
					string dir = Path.GetDirectoryName(data.path);
					if(dir != null) {
						string filename = Path.Combine(dir, string.Format("AlbumArt_{0}_Large.jpg", data.guid));
						if(File.Exists(filename)) {
							Console.WriteLine($"found {Path.Combine(dir, string.Format("AlbumArt_{0}_Large.jpg", data.guid))}");
							thumbnail_path = filename;
							return;
						}
						else if(File.Exists(Path.Combine(dir, "Cover.jpg"))) {
							Console.WriteLine("found cover.jpg");
							thumbnail_path = Path.Combine(dir, "Cover.jpg");
							return;
						}
						else if(File.Exists(Path.Combine(dir, "Folder.jpg"))) {
							Console.WriteLine("found folder.jpg");
							thumbnail_path = Path.Combine(dir, "Folder.jpg");
							return;
						}
						else if(File.Exists(Path.Combine(dir, "AlbumArtSmall.jpg"))) {
							Console.WriteLine("found albumartsmall.jpg");
							thumbnail_path = Path.Combine(dir, "AlbumArtSmall.jpg");
							return;
						}
					}
				}
			}
			catch(Exception e) {
				Console.WriteLine(e);
				thumbnail_path = "noalbumart.png";
			}
		}

		static HttpListener listener = new HttpListener();
		static void StartServer() {
			string prefix = $"http://localhost:{Form1.random_port}/";

			listener.Prefixes.Add(prefix); // Add your localhost address
			listener.Start();
			Console.WriteLine("Listening on " + prefix);
		}
		static AsyncCallback callback = new AsyncCallback(ListenerCallback);
		static void ListenerCallback(IAsyncResult result) {
			//HttpListener listener = (HttpListener)result.AsyncState;
			// Call EndGetContext to complete the asynchronous operation.
			try {
				HttpListenerContext context = listener.EndGetContext(result);
				if(!context.Request.IsLocal) {
					Console.WriteLine("request from not localhost");
					context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
					context.Response.Close();
					return;
				}
				ProcessRequest(context);
				context.Response.Close();
			}
			finally {
				// Start listening for next request
				//listener.BeginGetContext(new AsyncCallback(ListenerCallback), listener);
				listener.BeginGetContext(callback, listener);
			}
		}
		static void checkRequests() {
			if(listener.IsListening) {
				//listener.BeginGetContext(new AsyncCallback(ListenerCallback), listener);
				listener.BeginGetContext(callback, listener);
			}
		}
		static void ProcessRequest(HttpListenerContext context) {
			string url = context.Request.Url.AbsolutePath;
			if(url == "/") {
				ProcessRequestImage(context);
			}
			else if(url == "/alive") {
				ProcessRequestAlive(context);
			}
			else if(url == "/info") {
				ProcessRequestInfo(context);
			}
			else {
				context.Response.StatusCode = (int)HttpStatusCode.NotFound;
			}
		}
		static void ProcessRequestInfo(HttpListenerContext context) {
			//return string with info about current song in format "Title\nArtist\nAlbum"
			using(HttpListenerResponse response = context.Response) {
				response.ContentType = "text/plain";
				string info = $"{data.title}\n{data.artist}\n{data.album}";
				byte[] buffer = Encoding.UTF8.GetBytes(info);
				response.ContentLength64 = buffer.Length;
				response.OutputStream.Write(buffer, 0, buffer.Length);
			}
		}
		static void ProcessRequestAlive(HttpListenerContext context) {
			using(HttpListenerResponse response = context.Response) {
				response.ContentType = "text/plain";
				response.ContentLength64 = 5;
				byte[] buffer = Encoding.UTF8.GetBytes("alive");
				response.OutputStream.Write(buffer, 0, buffer.Length);
			}
		}
		static void ProcessRequestImage(HttpListenerContext context) {
			using(HttpListenerResponse response = context.Response) {
				if(File.Exists(thumbnail_path)) {
					response.ContentType = "image/jpeg";

					using(FileStream fileStream = File.OpenRead(thumbnail_path)) {
						response.ContentLength64 = fileStream.Length;

						byte[] buffer = new byte[4096];
						int bytesRead;
						while((bytesRead = fileStream.Read(buffer, 0, buffer.Length)) > 0) {
							response.OutputStream.Write(buffer, 0, bytesRead);
						}
					}
				}
				else {
					response.StatusCode = (int)HttpStatusCode.NotFound;
				}
			}
		}

		public static void Unhook() {
			MediaKeys.UnhookWindowsHookEx(MediaKeys.hookId);
		}
	}
	public class MediaKeys {
		public static RemotedWindowsMediaPlayer rm;


		public const int WH_KEYBOARD_LL = 13;
		public const int WM_KEYDOWN = 0x0100;
		public static IntPtr hookId = IntPtr.Zero;
		public static IntPtr SetHook(LowLevelKeyboardProc proc) {
			using(Process curProcess = Process.GetCurrentProcess())
			using(ProcessModule curModule = curProcess.MainModule) {
				return SetWindowsHookEx(WH_KEYBOARD_LL, proc, GetModuleHandle(curModule.ModuleName), 0);
			}
		}

		public delegate IntPtr LowLevelKeyboardProc(int nCode, IntPtr wParam, IntPtr lParam);

		public static IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam) {
			if(nCode >= 0 && wParam == (IntPtr)WM_KEYDOWN) {
				int vkCode = Marshal.ReadInt32(lParam);
				switch(vkCode) {
					case (int)Keys.MediaPlayPause:
						((WMPLib.IWMPPlayer4)rm.GetOcx()).controls.play();
						break;
					case (int)Keys.MediaStop:
						((WMPLib.IWMPPlayer4)rm.GetOcx()).controls.stop();
						break;
					case (int)Keys.MediaNextTrack:
						((WMPLib.IWMPPlayer4)rm.GetOcx()).controls.next();
						break;
					case (int)Keys.MediaPreviousTrack:
						((WMPLib.IWMPPlayer4)rm.GetOcx()).controls.previous();
						break;
				}
			}
			return CallNextHookEx(hookId, nCode, wParam, lParam);
		}

		[DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		private static extern IntPtr SetWindowsHookEx(int idHook, LowLevelKeyboardProc lpfn, IntPtr hMod, uint dwThreadId);

		[DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool UnhookWindowsHookEx(IntPtr hhk);

		[DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);

		[DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		private static extern IntPtr GetModuleHandle(string lpModuleName);

		[DllImport("user32.dll")]
		private static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, UIntPtr dwExtraInfo);

		private const int KEYEVENTF_EXTENDEDKEY = 0x0001;
		private const int KEYEVENTF_KEYUP = 0x0002;
		private const byte VK_NUMLOCK = 0x90;
	}
}
