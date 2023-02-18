using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using DiscordRPC;
using DiscordRPC.Logging;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Discord_WMP {
    public partial class Form1 : Form {
        albummanager AlbumManager = new albummanager();
        RemotedWindowsMediaPlayer rm = new RemotedWindowsMediaPlayer();
        private bool show_author;
        private bool show_title;
        private bool show_album;
        private bool show_albumart;
        private bool show_progressbar;
        public DiscordRpcClient client;
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
            Console.SetWindowSize(50, 15);
            InitializeComponent();

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
        public string[] Data() {
            var title = "";
            var album = "";
            var artist = "";

            var lenght = "";
            var position = "";
            double lenght_sec = -1;
            double position_sec = -1;
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
            title = ((WMPLib.IWMPPlayer4)rm.GetOcx()).currentMedia.getItemInfo("Title");
            album = ((WMPLib.IWMPPlayer4)rm.GetOcx()).currentMedia.getItemInfo("WM/AlbumTitle");
            artist = ((WMPLib.IWMPPlayer4)rm.GetOcx()).currentMedia.getItemInfo("WM/AlbumArtist");
            lenght = ((WMPLib.IWMPPlayer4)rm.GetOcx()).currentMedia.durationString;
            position = ((WMPLib.IWMPPlayer4)rm.GetOcx()).controls.currentPositionString;
            lenght_sec = ((WMPLib.IWMPPlayer4)rm.GetOcx()).currentMedia.duration;
            position_sec = ((WMPLib.IWMPPlayer4)rm.GetOcx()).controls.currentPosition;
        abort:;
            return new string[] { title, album, artist, lenght, position, lenght_sec.ToString(), position_sec.ToString() };
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

                update.Interval = 5000;
                string[] data = Data();
                var title = data[0];
                var album = data[1];
                var artist = data[2];
                var lenght = data[3];
                var position = data[4];
                var lenght_num = Convert.ToDouble(data[5]);
                var position_num = Convert.ToDouble(data[6]);
                if(lenght_num == -1 || position_num == -1) { 
                    playeddata = "Couldnt find WMP";
                    this.Refresh();
                    goto skip;
                }
                var mil = position_num / lenght_num;
                var time = position + "/" + lenght;
                var playbar = progressbar(mil, 10);
                if(album == "") {
                    //grab string after " - " in title and put it to album
                    album = title.Substring(title.IndexOf(" - ") + 3);
                }
                playeddata = title + "\n " + artist + "\n " + album + "\n " + "\n" + time + "\n" + progressbar(mil, 21);
                string albumart = AlbumManager.getalbumart(album, title);
                playeddata += "\n" + albumart;
                this.Refresh();
                client.SetPresence(new RichPresence() {
                    Details = title.Truncate(32),
                    State = artist.Truncate(32),
                    Assets = new Assets() {
                        LargeImageKey = albumart,
                        LargeImageText = album.Truncate(32),
                        SmallImageKey = "wmp_icon"
                    },
                    Timestamps = new Timestamps() {
                        Start = DateTime.UtcNow.Subtract(TimeSpan.FromSeconds(position_num)),
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
                Console.WriteLine("Received Update! {0}", e.Presence);
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
            Console.WriteLine("loaded settings");
            show_album = Settings1.Default.show_album;
            show_albumart = Settings1.Default.show_albumart;
            show_author = Settings1.Default.show_author;
            show_progressbar = Settings1.Default.show_progressbar;
            client_id.Text = Settings1.Default.RPC_ID;
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
            /*show_album = checkBox_show_album.Checked;
            show_albumart = checkBox_show_albumart.Checked;
            show_author = checkBox_show_author.Checked;
            show_progressbar = checkBox_show_playbar.Checked;
            Settings1.Default.show_album = show_album;
            Settings1.Default.show_albumart = show_albumart;
            Settings1.Default.show_author = show_author;
            Settings1.Default.show_progressbar = show_progressbar;*/
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
