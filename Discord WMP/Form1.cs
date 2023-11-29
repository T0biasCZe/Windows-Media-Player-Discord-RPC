using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using DiscordRPC;
using DiscordRPC.Logging;
using Windows.Media;
using Windows.Media.Playback;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Discord_WMP {
    public partial class Form1 : Form {
        albummanager AlbumManager = new albummanager();
        public RemotedWindowsMediaPlayer rm = new RemotedWindowsMediaPlayer();
        //RemotedWindowsMediaPlayer rm;
        private bool show_author;
        private bool show_title;
        private bool show_album;
        private bool show_albumart;
        private bool show_progressbar;
        private bool send_media_info;
        public DiscordRpcClient client;
        public static int random_port;

		[DllImport("kernel32.dll")]
        static extern IntPtr GetConsoleWindow();

        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        const int SW_HIDE = 0;
        const int SW_SHOW = 5;
        string playeddata = "played data:";
        private void SmoothingText_Paint(object sender, PaintEventArgs e) {
            //Console.WriteLine("draw function ran");
            //clear previously drawed text
            e.Graphics.Clear(Color.White);
            Font TextFont = new Font("Terminal", 8);
            e.Graphics.TextRenderingHint = TextRenderingHint.SingleBitPerPixelGridFit;
            e.Graphics.DrawString(playeddata, TextFont, Brushes.DarkGray, 10, 32);
        }

        public Form1() {
            Random random = new Random();
            random_port = random.Next(49152, 65535);
            Console.SetWindowSize(50, 15);
            InitializeComponent();

            /*//play music in axWindowsMediaPlayer1
            axWindowsMediaPlayer1.URL = "C:\\Users\\user\\Music\\hudba\\Sonic Forces\\Fading World - Imperial Tower - Tomoya Ohtani feat. Madeleine Wood & B-BANDJ.flac";
            //set axWindowsMediaPlayer1 visualisation to album art
            axWindowsMediaPlayer1.uiMode = "full";*/


			rm.Dock = DockStyle.Fill;

            panel1.Controls.Add(rm);
            panel1.Refresh();
            panel1.Update();
            this.FormClosing += Form1_Closing;
            //change console size

            notifyIcon1.Visible = true;
            notifyIcon1.ContextMenuStrip = new ContextMenuStrip();
            notifyIcon1.ContextMenuStrip.Items.Add("Restore").Click += (s, e) => RestoreForm();
            notifyIcon1.ContextMenuStrip.Items.Add("Exit").Click += (s, e) => Application.Exit();
            notifyIcon1.MouseClick += (s, e) => { if(e.Button == MouseButtons.Left) RestoreForm(); };
            //add event handler for minimize
            this.Resize += new System.EventHandler(this.Form1_Resize);
            //run function settingsload when Settings1 finish loading
            settingsload();
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.SmoothingText_Paint);
        }
        public struct playback_data {
            public string title;
			public string album;
			public string artist;

			public string lenght;
			public string position;
			public double lenght_sec;
			public double position_sec;
            public bool is_playing;

            public string guid;
            public string path;
		}
        public playback_data Data() {
            playback_data data = new playback_data();
            data.artist = ""; data.album = ""; data.title = ""; data.lenght = ""; data.position = ""; data.lenght_sec = -1; data.position_sec = -1; data.is_playing = false; data.guid = ""; data.path = "";
            // Get the currently playing media information
            int retrycount = 0;
            veemo:
            try {
                var a = ((WMPLib.IWMPPlayer4)rm.GetOcx()).currentMedia.duration;
            }
            catch {
                Thread.Sleep(20);
                retrycount++;
                if(retrycount < 10) goto veemo;
                else {
                    playeddata = "Couldnt find WMP";
                    goto abort;
                }
            }
            data.title = ((WMPLib.IWMPPlayer4)rm.GetOcx()).currentMedia.getItemInfo("Title");
            data.album = ((WMPLib.IWMPPlayer4)rm.GetOcx()).currentMedia.getItemInfo("WM/AlbumTitle");
			data.artist = ((WMPLib.IWMPPlayer4)rm.GetOcx()).currentMedia.getItemInfo("WM/AlbumArtist");
			data.lenght = ((WMPLib.IWMPPlayer4)rm.GetOcx()).currentMedia.durationString;
			data.position = ((WMPLib.IWMPPlayer4)rm.GetOcx()).controls.currentPositionString;
			data.lenght_sec = ((WMPLib.IWMPPlayer4)rm.GetOcx()).currentMedia.duration;
			data.position_sec = ((WMPLib.IWMPPlayer4)rm.GetOcx()).controls.currentPosition;
            data.is_playing = ((WMPLib.IWMPPlayer4)rm.GetOcx()).playState == WMPLib.WMPPlayState.wmppsPlaying;
            data.guid = ((WMPLib.IWMPPlayer4)rm.GetOcx()).currentMedia.getItemInfo("WMCollectionID");
            data.path = ((WMPLib.IWMPPlayer4)rm.GetOcx()).currentMedia.sourceURL;

        abort:;
            return data;
        }
        private void Form1_Resize(object sender, EventArgs e) {
            if(WindowState == FormWindowState.Minimized) {
                Hide();
                var handle = GetConsoleWindow();

                // Hide
                ShowWindow(handle, SW_HIDE);
                notifyIcon1.Visible = true;
                notifyIcon1.BalloonTipText = "Veemo";
                notifyIcon1.Text = "Windows Media Player Discord RPC";
                notifyIcon1.ShowBalloonTip(1000);
            }
        }
        private void RestoreForm() {
            var handle = GetConsoleWindow();
            // Show
            ShowWindow(handle, SW_SHOW);
            Show();
            WindowState = FormWindowState.Normal;
            notifyIcon1.Visible = false;
        }
        protected override void OnFormClosing(FormClosingEventArgs e) {
            notifyIcon1.Dispose();
            base.OnFormClosing(e);
            systemMediaControls._run_server = false;
		}
        bool initialized = false;
		private void update_Tick(object sender, EventArgs e) {
            if(!initialized) {
                if(client_id != null && client_id.Text != "" && client_id.Text.Length >= 10 /*&& int.TryParse(client_id.Text, out _)*/) {
                    Console.WriteLine("valid client id");
                    Initialize();
                    initialized = true;
                }
            }
            else {
                var data = Data();
                if(data.lenght_sec == -1 || data.position_sec == -1) { 
                    playeddata = "Couldnt find WMP";
                    this.Refresh();
                    goto skip;
                }
				if(send_media_info) {
                    systemMediaControls.update(data, this);
				}
				var mil = data.position_sec / data.lenght_sec;
                var time = data.position + "/" + data.lenght;
                var playbar = progressbar(mil, 10);
                if(data.album == "") {
                    //grab string after " - " in title and put it to album
                    data.album = data.title.Substring(data.title.IndexOf(" - ") + 3);
                }
                playeddata = data.title + "\n " + data.artist + "\n " + data.album + "\n " + "\n" + time + "\n" + progressbar(mil, 21);
                string albumart = AlbumManager.getalbumart(data.album, data.title);
                playeddata += "\n" + albumart;
                this.Refresh();
                client.SetPresence(new RichPresence() {
                    Details = data.title.Truncate(32),
                    State = data.artist.Truncate(32),
                    Assets = new Assets() {
                        LargeImageKey = albumart,
                        LargeImageText = data.album.Truncate(32),
                        SmallImageKey = "wmp_icon"
                    },
                    Timestamps = new Timestamps() {
                        Start = DateTime.UtcNow.Subtract(TimeSpan.FromSeconds(data.position_sec)),
                    },
                    Buttons = new DiscordRPC.Button[] {
                        new DiscordRPC.Button() { Label = playbar.Truncate(10), Url = "https://bing.com" }
                    }
                });
                Console.WriteLine("set data");
            skip:;
            }
        }

		void Initialize() {
            /*
            Create a Discord client
            NOTE: 	If you are using Unity3D, you must use the full constructor and define
                     the pipe connection.
            */
            client = new DiscordRpcClient(client_id.Text);

            //Set the logger
            client.Logger = new ConsoleLogger() { Level = LogLevel.Warning };

            //Subscribe to events
            client.OnReady += (sender, e) => {
                Console.WriteLine("Received Ready from user {0}", e.User.Username);
            };

            client.OnPresenceUpdate += (sender, e) => {
                //Console.WriteLine("Received Update! {0}", e.Presence);
            };

            //Connect to the RPC
            client.Initialize();
        }

        private void Form1_Load(object sender, EventArgs e) {
            Console.WriteLine("veemo");
            AlbumManager.LoadListFromCsv();
        }

        private void client_id_TextChanged(object sender, EventArgs e) {
            //set rpc_id in settings1 to client_id.Text
            Settings1.Default.RPC_ID = client_id.Text;
        }
        private void settingsload() {
            show_album = Settings1.Default.show_album;
            show_albumart = Settings1.Default.show_albumart;
            show_author = Settings1.Default.show_author;
            show_progressbar = Settings1.Default.show_progressbar;
            client_id.Text = Settings1.Default.RPC_ID;
            send_media_info = Settings1.Default.send_media_info;
            checkBox_sendMediaInfo.Checked = send_media_info;
			Console.WriteLine("loaded settings");
		}
        private void Form1_Closing(object sender, FormClosingEventArgs e) {
            Console.WriteLine("saved settings");
            //save settings
            Settings1.Default.Save();
            //close the console window
            //get handlr
            var handle = GetConsoleWindow();
            Application.Exit();
        }

        private void checkBox_changed(object sender, EventArgs e) {
            send_media_info = checkBox_sendMediaInfo.Checked;
            Settings1.Default.send_media_info = send_media_info;
            Console.WriteLine("saved settings");
			Settings1.Default.Save();
		}
        private string progressbar(double value, int lenght) {
            string bar = "";
            int progress = (int)(value * lenght);
            for(int i = 0; i < lenght; i++) {
                if(i < progress) {
                    bar += "█";
                }
                else {
                    bar += "░";
                }
            }
            return bar;
        }
        private void checknottoolong(string jedna, string dva, string tri, string ctyri, string pet, string sest) {
            if(jedna.Length > 32) {
                Console.WriteLine("title is too long");
            }
            if(dva.Length > 32) {
                Console.WriteLine("album is too long");
            }
            if(tri.Length > 32) {
                Console.WriteLine("artist is too long");
            }
            if(ctyri.Length > 32) {
                Console.WriteLine("lenght is too long");
            }
            if(pet.Length > 32) {
                Console.WriteLine("position is too long");
            }
            if(sest.Length > 32) {
                Console.WriteLine("playbar is too long");
            }

        }

        private void button1_Click(object sender, EventArgs e) {
            //open albumartadder form
            AlbumArtAdder form = new AlbumArtAdder();
            form.Show();
        }
    }
    //create new ¨class for extension method
    public static class ExtensionMethods {
        public static string Truncate(this string value, int maxChars) {
            return value.Length <= maxChars ? value : value.Substring(0, maxChars);
        }
    }
}
