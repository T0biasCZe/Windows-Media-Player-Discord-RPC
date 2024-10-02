using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using DiscordRPC;
using DiscordRPC.Logging;
/*using Windows.Media;
using Windows.Media.Playback;*/
using WMPLib;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Discord_WMP {
    public partial class Form1 : Form {

        const string version = "v2.2.2b W7";
        const string date = "28.9.24";
		string versionn = $"{Discord_WMP.Properties.Resources.CurrentCommit.Trim()} {version} {date}";

		public static string url = "https://github.com/T0biasCZe/Windows-Media-Player-Discord-RPC/";

        public RemotedWindowsMediaPlayer rm = new RemotedWindowsMediaPlayer();
        //RemotedWindowsMediaPlayer rm;
        private bool show_author;
        private bool show_title;
        private bool show_album;
        private bool show_albumart;
        private bool show_progressbar;
        private bool send_media_info;
        private bool show_console;
        private bool use_rpc;
        public DiscordRpcClient client;
        public static int random_port = 42565;
        public static bool albummanageropen = false;

		[DllImport("kernel32.dll")]
        static extern IntPtr GetConsoleWindow();

        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

		[DllImport("user32.dll")]
		private static extern IntPtr GetForegroundWindow();

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
            //check if app is already running by sending http request to "localhost:42565/alive"
            bool appAlreadyRunning = false;
            try {
				HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://localhost:42565/alive");
				request.Timeout = 2000;
				HttpWebResponse response = (HttpWebResponse)request.GetResponse();
				if(response.StatusCode == HttpStatusCode.OK) {
                    appAlreadyRunning = true;
				}
			}
			catch(WebException) {

			}
            if(!appAlreadyRunning) { //app unresponsive or not running, so kill it
                Process[] processes = Process.GetProcessesByName("Discord WMP");
				foreach(Process process in processes) {
                    if(process.Id == Process.GetCurrentProcess().Id) continue;
					process.Kill();
				}
            }

            if(appAlreadyRunning) {
                if(Environment.GetCommandLineArgs().Contains("getdata")){
                    //send http request to "localhost:42565/getdata" to get data
					HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://localhost:42565/info");
					request.Timeout = 1000;
					HttpWebResponse response = (HttpWebResponse)request.GetResponse();
					if(response.StatusCode == HttpStatusCode.OK) {
						//get data from response
						Stream dataStream = response.GetResponseStream();
						StreamReader reader = new StreamReader(dataStream);
						string responseFromServer = reader.ReadToEnd();
						//send data to console
						Console.WriteLine(responseFromServer);
						//close response
						response.Close();
						//close app
						Environment.Exit(0);
					}
                }
                else Environment.Exit(0);
            }

            if(Settings1.Default.first_setup) {
                loadingsettings = true;
                DialogResult dialog_userpc = MessageBox.Show("Do you want to put WMP play data into Discord Rich presence?\n(Recommended YES if you have Discord installed)", "Initial setup", MessageBoxButtons.YesNo);
                if(dialog_userpc == DialogResult.Yes) {
                    use_rpc = true;
                    Settings1.Default.show_discord = true;
                }
                else if(dialog_userpc == DialogResult.No) {
                    use_rpc = false;
                    Settings1.Default.show_discord = false;
                }
                DialogResult dialog_console = MessageBox.Show("Do you want to bridge WMP play data and Windows 10 Media Info?\nThis will allow you to use keyboard media keys and allow showing playing music for example on Wallpaper Engine\n(Recommended YES if not sure)", "Initial setup", MessageBoxButtons.YesNo);
                loadingsettings = false;
                Settings1.Default.first_setup = false;
                Settings1.Default.Save();
            }

			var handle = GetConsoleWindow();
			// Show console during boot
			ShowWindow(handle, SW_SHOW);

			/*Random random = new Random();
            bool isAvailable = false;
			while(isAvailable == false) {
				random_port = random.Next(49152, 65535);
				//check if port is being used and if it is, generate new one
				IPGlobalProperties ipGlobalProperties = IPGlobalProperties.GetIPGlobalProperties();
				TcpConnectionInformation[] tcpConnInfoArray = ipGlobalProperties.GetActiveTcpConnections();

				foreach(TcpConnectionInformation tcpi in tcpConnInfoArray) {
					if(tcpi.LocalEndPoint.Port == random_port) {
						isAvailable = false;
                        continue;
					}
                    else {
						isAvailable = true;
                    }
				}
			}*/
			Console.SetWindowSize(50, 15);
            InitializeComponent();

			linkLabel2.Text = $"http://localhost:{random_port}/";
			linkLabel2.Links.Add(0, linkLabel2.Text.Length, $"http://localhost:{random_port}/");

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

            linkLabel1.Text = versionn;
            linkLabel1.Links.Add(0, versionn.Length, url);

            //run function settingsload when Settings1 finish loading
            settingsload();
			handle = GetConsoleWindow();
			// Show console during boot
			ShowWindow(handle, SW_SHOW);

			Console.WriteLine("Loading app, please wait:)");
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.SmoothingText_Paint);
        }
		private void Form1_Deactivate(object sender, EventArgs e) {
			//check if console is active
			var handle = GetConsoleWindow();
			var foregroundHandle = GetForegroundWindow();
			bool consoleopen = (foregroundHandle == handle);
			if(!albummanageropen && !consoleopen && !checkBox_dontautohide.Checked) {
				Hide();

				// Hide
				ShowWindow(handle, SW_HIDE);
				notifyIcon1.Visible = true;
				notifyIcon1.BalloonTipText = "Veemo";
				notifyIcon1.Text = "Windows Media Player Discord RPC";
				notifyIcon1.ShowBalloonTip(1000);
			}
		}
		private void RestoreForm() {
			if(show_console) {
				var handle = GetConsoleWindow();
				ShowWindow(handle, SW_SHOW);
			}
			Show();
			WindowState = FormWindowState.Normal;
			notifyIcon1.Visible = false;
		}
		protected override void OnFormClosing(FormClosingEventArgs e) {
			notifyIcon1.Dispose();
			base.OnFormClosing(e);
			systemMediaControls._run_server = false;
		}
		private void Form1_Load(object sender, EventArgs e) {
			Console.WriteLine("veemo");
			albummanager.LoadListFromCsv();
			if(!show_console) {
				var handle = GetConsoleWindow();
				// Show console during boot
				ShowWindow(handle, SW_HIDE);
			}
		}

		private void client_id_TextChanged(object sender, EventArgs e) {
			//set rpc_id in settings1 to client_id.Text
			Settings1.Default.RPC_ID = client_id.Text;
		}
		private static bool loadingsettings = false;
		private void settingsload() {
			loadingsettings = true;
			show_album = Settings1.Default.show_album;
			show_albumart = Settings1.Default.show_albumart;
			show_author = Settings1.Default.show_author;
			show_progressbar = Settings1.Default.show_progressbar;
			client_id.Text = Settings1.Default.RPC_ID;
			show_console = Settings1.Default.show_console;
			checkBox_showconsole.Checked = show_console;
			send_media_info = Settings1.Default.send_media_info;
			checkBox_sendMediaInfo.Checked = send_media_info;
			use_rpc = Settings1.Default.show_discord;
			checkBox_userpc.Checked = use_rpc;
            checkBox_dontautohide.Checked = Settings1.Default.dont_hide;
            checkBox_mediakeys.Checked = Settings1.Default.media_keys;

			Console.WriteLine("loaded settings");
			loadingsettings = false;
		}
		private void Form1_Closing(object sender, FormClosingEventArgs e) {
			Console.WriteLine("saved settings");
            systemMediaControls.Unhook();
			//save settings
			Settings1.Default.Save();
			//close the console window
			//get handlr
			var handle = GetConsoleWindow();
			Application.Exit();
		}

		private void checkBox_changed(object sender, EventArgs e) {
			//do nothing if settings are loading
			if(loadingsettings) return;
			send_media_info = checkBox_sendMediaInfo.Checked;
			Settings1.Default.send_media_info = send_media_info;
			show_console = checkBox_showconsole.Checked;
			Settings1.Default.show_console = show_console;
			use_rpc = checkBox_userpc.Checked;
			Settings1.Default.show_discord = use_rpc;
            Settings1.Default.dont_hide = checkBox_dontautohide.Checked;
            Settings1.Default.media_keys = checkBox_mediakeys.Checked;

			var handle = GetConsoleWindow();
			if(show_console) ShowWindow(handle, SW_SHOW);
			else ShowWindow(handle, SW_HIDE);

			Console.WriteLine("saved settings");
			Settings1.Default.Save();
		}
		private void button1_Click(object sender, EventArgs e) {
			//open albumartadder form
			albummanageropen = true;
			AlbumArtAdder form = new AlbumArtAdder();
			form.Show();
		}

		private void button_resizeBack_Click(object sender, EventArgs e) {
			this.Height = 160;
			this.Width = 208;
		}

		private void button_settings_Click(object sender, EventArgs e) {
			if(this.Height > 160) {
				this.Height = 160;
				this.Width = 208;
			}
			else {
				this.Width = 400;
				this.Height = 428;
			}
		}
		private void linkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
			string URL = ((LinkLabel)sender).Links[0].LinkData.ToString();
			Console.WriteLine(URL);
			System.Diagnostics.Process.Start(URL);
		}

		public struct playback_data {
            public string title;
			public string album;
			public string artist;
            public string audiofilename;
            public string audiofilepath;

			public string lenght;
			public string position;
			public double lenght_sec;
			public double position_sec;
            public WMPLib.WMPPlayState play_state;

            public string guid;
            public string path;
            public string media_type;
		}
        //gets current information from Windows Media Player
        public playback_data Data() {
            playback_data data = new playback_data();
            data.artist = ""; data.album = ""; data.title = ""; data.lenght = ""; data.position = ""; data.lenght_sec = -1; data.position_sec = -1; data.play_state = WMPLib.WMPPlayState.wmppsStopped; data.guid = ""; data.path = "";
            // Get the currently playing media information
            int retrycount = 0;
            WMPLib.IWMPPlayer4 player = (WMPLib.IWMPPlayer4)rm.GetOcx();
            veemo:
            try {
                player = ((WMPLib.IWMPPlayer4)rm.GetOcx());
				var a = player.currentMedia.duration;
            }
            catch {
                Thread.Sleep(20);
                retrycount++;
                ConsoleColor old = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("error trying to read WMP data, retrying");
				Console.ForegroundColor = old;
				if(retrycount < numericUpDown_retryattempts.Value) goto veemo;
                else {
					Console.ForegroundColor = ConsoleColor.Red;
					playeddata = "Couldnt find WMP";
                    Console.WriteLine("aborting");
                    Console.ForegroundColor = old;
                    goto abort;
                }
            }

            retrycount = 0;
            while(retrycount < numericUpDown_retryattempts.Value) {
                try {
                    data.title = player.currentMedia.getItemInfo("Title");
                    data.album = player.currentMedia.getItemInfo("WM/AlbumTitle");
                    data.artist = player.currentMedia.getItemInfo("WM/AlbumArtist");
                    data.audiofilepath = player.controls.currentItem.sourceURL;
                    //get filename from the path
                    data.audiofilename = Path.GetFileName(data.audiofilepath);
                    data.lenght = player.currentMedia.durationString;
                    data.position = player.controls.currentPositionString;
                    data.lenght_sec = player.currentMedia.duration;
                    data.position_sec = player.controls.currentPosition;
                    data.play_state = player.playState;
                    data.guid = player.currentMedia.getItemInfo("WMCollectionID");
                    data.path = player.currentMedia.sourceURL;
                    data.media_type = player.currentMedia.getItemInfo("MediaType");


                    break;
                }
                catch {
                    retrycount++;
                    Thread.Sleep(1);
                    ConsoleColor old = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("error while reading data from WMP (this can happen if song changes when the data is being read)");
                    Console.ForegroundColor = old;
                }
            }

        abort:;
            return data;
        }
        //displays the current information from Windows Media Player
		private void debug(playback_data data) {
            label3.Text = "initialized " + initialized.ToString();
            label4.Text = "send_data_lasttime " + send_data_lasttime.ToString();
            label5.Text = "play_state " + data.play_state.ToString();
            label6.Text = "mediatype " + data.media_type;
			label7.Text = "audiofilename " + data.audiofilename;
		}
        public static bool check_discord_running() {
            //check if executable discord.exe, discordcanary.exe or discordptb.exe is running
            Process[] pname = Process.GetProcessesByName("discord");
            Process[] pname2 = Process.GetProcessesByName("discordcanary");
            Process[] pname3 = Process.GetProcessesByName("discordptb");
            bool discordrunning = !(pname.Length == 0 && pname2.Length == 0 && pname3.Length == 0);
            Console.BackgroundColor = ConsoleColor.Cyan;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Discord running: " + discordrunning);
            Console.ResetColor();
            return discordrunning;
        }

		bool initialized = false;
        bool send_data_lasttime = false;
        Stopwatch pause_stopwatch = new Stopwatch();
		private void update_Tick(object sender, EventArgs e) {
            var data = Data();
            debug(data);
            if(data.lenght_sec == -1 || data.position_sec == -1) { 
                playeddata = "Couldnt find WMP";
                this.Refresh();
            }
            bool stopped = data.play_state.In(WMPLib.WMPPlayState.wmppsStopped, WMPLib.WMPPlayState.wmppsMediaEnded, WMPLib.WMPPlayState.wmppsUndefined);
            if(data.play_state == WMPPlayState.wmppsPaused) {
                //if stopwatch is not running, start it. then if music is paused for more than 5 seconds, stop sending data
                if(!pause_stopwatch.IsRunning) {
					pause_stopwatch.Start();
				}
                if(pause_stopwatch.ElapsedMilliseconds > 30000) {
					stopped = true;
				}
			}
			else {
				pause_stopwatch.Reset();
            }
            if(stopped) {
				playeddata = "Stopped";
				this.Refresh();
			}
			if(send_media_info) {
                systemMediaControls.update(data, this);
			}
            if(use_rpc && !stopped) {
                if(!check_discord_running()) {
                    if(initialized) {
						Console.ForegroundColor = ConsoleColor.Yellow;
						Console.WriteLine("Discord not running but RPC inited, deiniting");
						Console.ResetColor();
						Deinitialize();
                        initialized = false;
                    }
				}
                else {
					if(!initialized) {
						Console.ForegroundColor = ConsoleColor.Yellow;
						Console.WriteLine("RPC Client not initialized, initing");
						Initialize();
						initialized = true;
						Console.WriteLine("Initialized Discord RPC");
						Console.ResetColor();
					}
				}

				var mil = data.position_sec / data.lenght_sec;
				var time = data.position + "/" + data.lenght;
				var playbar = progressbar(mil, 10);
				if(data.album == "") {
					//grab string after " - " in title and put it to album
					data.album = data.title.Substring(data.title.IndexOf(" - ") + 3);
				}
				playeddata = data.title + "\n " + data.artist + "\n " + data.album + "\n " + "\n" + time + "\n" + progressbar(mil, 21);
				string albumart = albummanager.getalbumart(data.album, data.title, data.artist, data.audiofilename);
				playeddata += "\n" + albumart;
				this.Refresh();

                if(!initialized) return;

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
                send_data_lasttime = true;
                Console.WriteLine("set data");
            }
		    if(send_data_lasttime && initialized && stopped) {
                client.ClearPresence();
            }
            if(!use_rpc && initialized) {
				client.ClearPresence();
				Deinitialize();
				initialized = false;
			}
        }

		bool Initialize() {
            Console.BackgroundColor = ConsoleColor.Cyan;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Initialize() ran");
            Console.ResetColor();

            if(client_id.Text.Length <= 8) {
                Console.WriteLine("invalid client id");
				return false;
			}
            if(client != null) {
                try {
                    client.Deinitialize();
                    client.Dispose();
                }
                catch {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Error while deinitializing client while initializing client");
                    Console.ResetColor();
                }
            }
            client = new DiscordRpcClient(client_id.Text, -1);

            //Set the logger
            client.Logger = new ConsoleLogger() { Level = LogLevel.Warning };

            //Subscribe to events
            client.OnReady += (sender, e) => {
                Console.WriteLine("Received Ready from user {0}", e.User.Username);
            };

            client.OnPresenceUpdate += (sender, e) => {
                Console.WriteLine("Received Update! {0}", e.Presence);
            };

            //Connect to the RPC
            client.Initialize();
            return true;
        }
        void Deinitialize() {
            client.Deinitialize();
			client.Dispose();
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
	}
	//create new class for extension method
	public static class ExtensionMethods {
        public static string Truncate(this string value, int maxChars) {
            return value.Length <= maxChars ? value : value.Substring(0, maxChars);
        }
		public static bool In<T>(this T obj, params T[] args) {
			return args.Contains(obj);
		}
	}
}
