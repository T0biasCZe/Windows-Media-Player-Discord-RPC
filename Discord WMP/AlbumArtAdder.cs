﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrackBar;
using CsvHelper;
using System.Globalization;
using System.IO;

namespace Discord_WMP {
    public partial class AlbumArtAdder : Form {
        public AlbumArtAdder() {
            InitializeComponent();
            titlecontainsword_contains_filename.SetWatermark("Filename of albumart");
            titlecontainsword_contains.SetWatermark("Contains this string");
            titlecontainsword_containsnot.SetWatermark("Doesn't contain this string"); 
            titlecontainsword_priority.SetWatermark("Art search priority (default 2)");

            albumcontainsword_filename.SetWatermark("Filename of albumart");
            albumcontainsword_contains.SetWatermark("Contains this string");
            albumcontainsword_containsnot.SetWatermark("Doesn't contain this string");
            albumcontainsword_priority.SetWatermark("Art search priority (default 1)");

            specificalbumname_name.SetWatermark("Album name");
            specificalbumname_filename.SetWatermark("Filename of albumart");
            specificalbumname_priority.SetWatermark("Art search priority (default 0)");

            artistsname_artist.SetWatermark("Artist name");
            artistsname_filename.SetWatermark("Filename of albumart");
            artistsname_priority.SetWatermark("Art search priority (default 3)");

            filenameis_audiofilename.SetWatermark("Audio filename");
            filenameis_discordfilename.SetWatermark("Filename of albumart");
            filenameis_priority.SetWatermark("Art search priority (default 0)");

            this.FormClosing += AlbumArtAdder_Closing;
        }

        private void button1_Click(object sender, EventArgs e) {
            if(specificalbumname_name.Text == "") { MessageBox.Show("Album not filled in"); goto end; }
            if(specificalbumname_filename.Text == "") { MessageBox.Show("Album not filled in"); goto end; }
            pair per = new pair();
            per.type = 0;
            per.filename = specificalbumname_filename.Text;
            per.album = specificalbumname_name.Text;
            per.priority = 0;
            bool aaa = int.TryParse(specificalbumname_priority.Text, out int bruh);
            if(aaa) per.priority = bruh;
            Thread.Sleep(66);
			albummanager.pairList.Add(per);
            showinlistbox();
        end:;
        }
        private void button1_Click_1(object sender, EventArgs e) {
            if(albumcontainsword_contains.Text == "") { MessageBox.Show("contains not filled in"); goto end; }
            if(albumcontainsword_filename.Text == "") { MessageBox.Show("Album not filled in"); goto end; }
            pair per = new pair();
            per.type = pairtype.albumcontains;
            per.filename = albumcontainsword_filename.Text;
            per.contains = albumcontainsword_contains.Text;
            per.doesntcontain = albumcontainsword_containsnot.Text;
            per.priority = 1;
            bool aaa = int.TryParse(albumcontainsword_containsnot.Text, out int bruh);
            if(aaa) per.priority = bruh;
            Thread.Sleep(66);
			albummanager.pairList.Add(per);
            showinlistbox();
        end:;
        }
        private void button2_Click(object sender, EventArgs e) {

            if(titlecontainsword_contains.Text == "") { MessageBox.Show("contains not filled in"); goto end; }
            if(titlecontainsword_contains_filename.Text == "") { MessageBox.Show("Album not filled in"); goto end; }
            pair per = new pair();
            per.type = pairtype.titlecontains;
            per.filename = titlecontainsword_contains_filename.Text;
            per.contains = titlecontainsword_contains.Text;
            per.doesntcontain = titlecontainsword_containsnot.Text;
            per.priority = 2;
            bool aaa = int.TryParse(titlecontainsword_priority.Text, out int bruh);
            if(aaa) per.priority = bruh;
            Thread.Sleep(66);
			albummanager.pairList.Add(per);
            showinlistbox();
        end:;
        }
		private void artistname_add_Click(object sender, EventArgs e) {
			if(artistsname_artist.Text == "") { MessageBox.Show("artists name not filled in"); goto end; }
			if(artistsname_filename.Text == "") { MessageBox.Show("Albumart filename not filled in"); goto end; }
			pair per = new pair();
			per.type = pairtype.artistname;
			per.filename = artistsname_filename.Text;
			per.contains = artistsname_artist.Text;
            per.doesntcontain = "";
			per.priority = 3;
			bool aaa = int.TryParse(artistsname_priority.Text, out int bruh);
			if(aaa) per.priority = bruh;
			Thread.Sleep(66);
			albummanager.pairList.Add(per);
			showinlistbox();
		end:;
		}
		private void filenameis_add_Click(object sender, EventArgs e) {
			if(filenameis_audiofilename.Text == "") { MessageBox.Show("Audio's filename not filled in"); goto end; }
			if(filenameis_discordfilename.Text == "") { MessageBox.Show("Discord album art filename not filled in"); goto end; }
			pair per = new pair();
			per.type = pairtype.audiofilename;
			per.filename = filenameis_discordfilename.Text;
			per.contains = filenameis_audiofilename.Text;
			per.doesntcontain = "";
			per.priority = 0;
			bool aaa = int.TryParse(filenameis_priority.Text, out int bruh);
			if(aaa) per.priority = bruh;
			Thread.Sleep(66);
			albummanager.pairList.Add(per);
			showinlistbox();
		end:;
		}
		private void AlbumArtAdder_Closing(object sender, FormClosingEventArgs e) {
			albummanager.writecsv();
            Console.WriteLine("wrote csv");
		}
        private void AlbumArtAdder_Load(object sender, EventArgs e) {
            showinlistbox();

            //else MessageBox.Show("didnt find albumarts.csv, will create new one");
        }
        
        //create function that shows pairList in listbox1        
        private void showinlistbox() {
            listBox1.Items.Clear();
            listBox1.UseTabStops = true;
            listBox1.UseCustomTabOffsets = true;
            listBox1.CustomTabOffsets.Add(300);
            listBox1.CustomTabOffsets.Add(340);
            listBox1.Items.Add("filename ;;; entry data \t priority \t entry type");
            foreach(pair pér in albummanager.pairList) {
                if(pér.type == 0) {
                    listBox1.Items.Add((pér.filename + " ;;; " + pér.album).Truncate(75) + "\t" + pér.priority + "\t (album name)");
                }
                else if(pér.type == pairtype.albumcontains){
                    listBox1.Items.Add(pér.filename + " ;;; " + pér.contains + " ;;; " + pér.doesntcontain + "\t" + pér.priority + "\t (words contained in album name)");
                }
                else if(pér.type == pairtype.titlecontains) {
                    listBox1.Items.Add(pér.filename + " ;;; " + pér.contains + " ;;; " + pér.doesntcontain + "\t" + pér.priority + "\t (words contained in title name)");
                }
				else if(pér.type == pairtype.audiofilenamecontains) {
					listBox1.Items.Add(pér.filename + " ;;; " + pér.contains + " ;;; " + pér.doesntcontain + "\t" + pér.priority + "\t (words contained in audiofile name)");
				}
				else if(pér.type == pairtype.artistname) {
					listBox1.Items.Add(pér.filename + " ;;; " + pér.contains + " ;;; " + pér.doesntcontain + "\t" + pér.priority + "\t (artists name)");
				}
				else if(pér.type == pairtype.audiofilename) {
					listBox1.Items.Add(pér.filename + " ;;; " + pér.contains + " ;;; " + pér.doesntcontain + "\t" + pér.priority + "\t (audio file name)");
				}
			}
        }

        private void label3_Click(object sender, EventArgs e) {

        }

        private void contains_TextChanged(object sender, EventArgs e) {

        }

        private void containsnot_TextChanged(object sender, EventArgs e) {

        }

        private void title_filename_TextChanged(object sender, EventArgs e) {

        }

		private void AlbumArtAdder_FormClosed(object sender, FormClosedEventArgs e) {
            Form1.ActiveForm.Show();
            Form1.ActiveForm.TopMost = true;
            Form1.ActiveForm.Activate();
            Form1.albummanageropen = false;

			Thread.Sleep(66);
		}

		private void AlbumArtAdder_SizeChanged(object sender, EventArgs e) {
            listBox1.Height = this.Height - (506 - 433);
		}
	}
	//create int enum "pairtype" with names "albumstring", "albumcontains" and "titlecontains"
	public enum pairtype : int {albumstring, albumcontains, titlecontains, audiofilename, artistname, audiofilenamecontains};
    public struct pair {
        public string filename { get; set; }
        public pairtype type { get; set; }
        public string album { get; set; }
        public string contains { get; set; }
        public string doesntcontain { get; set; }
        public int priority { get; set; }
    }
    public static class TextBoxWatermarkExtensionMethod {
        private const uint ECM_FIRST = 0x1500;
        private const uint EM_SETCUEBANNER = ECM_FIRST + 1;

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = false)]
        private static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, uint wParam, [MarshalAs(UnmanagedType.LPWStr)] string lParam);

        public static void SetWatermark(this TextBox textBox, string watermarkText) {
            SendMessage(textBox.Handle, EM_SETCUEBANNER, 0, watermarkText);
        }
    }
}
