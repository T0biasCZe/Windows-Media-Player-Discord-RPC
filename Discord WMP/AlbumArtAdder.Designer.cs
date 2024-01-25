namespace Discord_WMP {
    partial class AlbumArtAdder {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if(disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
			this.components = new System.ComponentModel.Container();
			this.listBox1 = new System.Windows.Forms.ListBox();
			this.specificalbumname_name = new System.Windows.Forms.TextBox();
			this.titlecontainsword_contains = new System.Windows.Forms.TextBox();
			this.titlecontainsword_containsnot = new System.Windows.Forms.TextBox();
			this.titlecontainsword_contains_filename = new System.Windows.Forms.TextBox();
			this.album_add = new System.Windows.Forms.Button();
			this.specificalbumname_filename = new System.Windows.Forms.TextBox();
			this.titlebased_add = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.component11 = new Discord_WMP.Component1(this.components);
			this.specificalbumname_priority = new System.Windows.Forms.TextBox();
			this.titlecontainsword_priority = new System.Windows.Forms.TextBox();
			this.albumcontainsword_filename = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.button1 = new System.Windows.Forms.Button();
			this.albumcontainsword_priority = new System.Windows.Forms.TextBox();
			this.albumcontainsword_containsnot = new System.Windows.Forms.TextBox();
			this.albumcontainsword_contains = new System.Windows.Forms.TextBox();
			this.panel1 = new System.Windows.Forms.Panel();
			this.label6 = new System.Windows.Forms.Label();
			this.filenameis_audiofilename = new System.Windows.Forms.TextBox();
			this.filenameis_add = new System.Windows.Forms.Button();
			this.filenameis_discordfilename = new System.Windows.Forms.TextBox();
			this.filenameis_priority = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.artistsname_artist = new System.Windows.Forms.TextBox();
			this.artistname_add = new System.Windows.Forms.Button();
			this.artistsname_filename = new System.Windows.Forms.TextBox();
			this.artistsname_priority = new System.Windows.Forms.TextBox();
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// listBox1
			// 
			this.listBox1.FormattingEnabled = true;
			this.listBox1.Location = new System.Drawing.Point(16, 32);
			this.listBox1.Name = "listBox1";
			this.listBox1.Size = new System.Drawing.Size(680, 433);
			this.listBox1.TabIndex = 0;
			// 
			// specificalbumname_name
			// 
			this.specificalbumname_name.Location = new System.Drawing.Point(3, 42);
			this.specificalbumname_name.Name = "specificalbumname_name";
			this.specificalbumname_name.Size = new System.Drawing.Size(224, 20);
			this.specificalbumname_name.TabIndex = 1;
			// 
			// titlecontainsword_contains
			// 
			this.titlecontainsword_contains.Location = new System.Drawing.Point(3, 362);
			this.titlecontainsword_contains.Name = "titlecontainsword_contains";
			this.titlecontainsword_contains.Size = new System.Drawing.Size(224, 20);
			this.titlecontainsword_contains.TabIndex = 2;
			this.titlecontainsword_contains.TextChanged += new System.EventHandler(this.contains_TextChanged);
			// 
			// titlecontainsword_containsnot
			// 
			this.titlecontainsword_containsnot.Location = new System.Drawing.Point(3, 386);
			this.titlecontainsword_containsnot.Name = "titlecontainsword_containsnot";
			this.titlecontainsword_containsnot.Size = new System.Drawing.Size(224, 20);
			this.titlecontainsword_containsnot.TabIndex = 3;
			this.titlecontainsword_containsnot.TextChanged += new System.EventHandler(this.containsnot_TextChanged);
			// 
			// titlecontainsword_contains_filename
			// 
			this.titlecontainsword_contains_filename.Location = new System.Drawing.Point(3, 410);
			this.titlecontainsword_contains_filename.Name = "titlecontainsword_contains_filename";
			this.titlecontainsword_contains_filename.Size = new System.Drawing.Size(224, 20);
			this.titlecontainsword_contains_filename.TabIndex = 4;
			this.titlecontainsword_contains_filename.TextChanged += new System.EventHandler(this.title_filename_TextChanged);
			// 
			// album_add
			// 
			this.album_add.Location = new System.Drawing.Point(3, 114);
			this.album_add.Name = "album_add";
			this.album_add.Size = new System.Drawing.Size(75, 23);
			this.album_add.TabIndex = 5;
			this.album_add.Text = "Add pair";
			this.album_add.UseVisualStyleBackColor = true;
			this.album_add.Click += new System.EventHandler(this.button1_Click);
			// 
			// specificalbumname_filename
			// 
			this.specificalbumname_filename.Location = new System.Drawing.Point(3, 66);
			this.specificalbumname_filename.Name = "specificalbumname_filename";
			this.specificalbumname_filename.Size = new System.Drawing.Size(224, 20);
			this.specificalbumname_filename.TabIndex = 6;
			// 
			// titlebased_add
			// 
			this.titlebased_add.Location = new System.Drawing.Point(3, 458);
			this.titlebased_add.Name = "titlebased_add";
			this.titlebased_add.Size = new System.Drawing.Size(75, 23);
			this.titlebased_add.TabIndex = 7;
			this.titlebased_add.Text = "Add pair";
			this.titlebased_add.UseVisualStyleBackColor = true;
			this.titlebased_add.Click += new System.EventHandler(this.button2_Click);
			// 
			// label1
			// 
			this.label1.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.label1.Location = new System.Drawing.Point(16, 8);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(448, 23);
			this.label1.TabIndex = 8;
			this.label1.Text = "Already paired";
			// 
			// label3
			// 
			this.label3.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.label3.Location = new System.Drawing.Point(3, 330);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(224, 32);
			this.label3.TabIndex = 10;
			this.label3.Text = "Finding art based on title containing words";
			this.label3.Click += new System.EventHandler(this.label3_Click);
			// 
			// label4
			// 
			this.label4.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.label4.Location = new System.Drawing.Point(3, 10);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(224, 32);
			this.label4.TabIndex = 11;
			this.label4.Text = "Finding art based on specific album name";
			// 
			// specificalbumname_priority
			// 
			this.specificalbumname_priority.Location = new System.Drawing.Point(3, 90);
			this.specificalbumname_priority.Name = "specificalbumname_priority";
			this.specificalbumname_priority.Size = new System.Drawing.Size(224, 20);
			this.specificalbumname_priority.TabIndex = 12;
			// 
			// titlecontainsword_priority
			// 
			this.titlecontainsword_priority.Location = new System.Drawing.Point(3, 434);
			this.titlecontainsword_priority.Name = "titlecontainsword_priority";
			this.titlecontainsword_priority.Size = new System.Drawing.Size(224, 20);
			this.titlecontainsword_priority.TabIndex = 13;
			// 
			// albumcontainsword_filename
			// 
			this.albumcontainsword_filename.Location = new System.Drawing.Point(3, 258);
			this.albumcontainsword_filename.Name = "albumcontainsword_filename";
			this.albumcontainsword_filename.Size = new System.Drawing.Size(224, 20);
			this.albumcontainsword_filename.TabIndex = 19;
			// 
			// label2
			// 
			this.label2.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.label2.Location = new System.Drawing.Point(3, 154);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(224, 32);
			this.label2.TabIndex = 18;
			this.label2.Text = "Finding art based on album containing words";
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(3, 282);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(75, 23);
			this.button1.TabIndex = 17;
			this.button1.Text = "Add pair";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.button1_Click_1);
			// 
			// albumcontainsword_priority
			// 
			this.albumcontainsword_priority.Location = new System.Drawing.Point(3, 234);
			this.albumcontainsword_priority.Name = "albumcontainsword_priority";
			this.albumcontainsword_priority.Size = new System.Drawing.Size(224, 20);
			this.albumcontainsword_priority.TabIndex = 16;
			// 
			// albumcontainsword_containsnot
			// 
			this.albumcontainsword_containsnot.Location = new System.Drawing.Point(3, 210);
			this.albumcontainsword_containsnot.Name = "albumcontainsword_containsnot";
			this.albumcontainsword_containsnot.Size = new System.Drawing.Size(224, 20);
			this.albumcontainsword_containsnot.TabIndex = 15;
			// 
			// albumcontainsword_contains
			// 
			this.albumcontainsword_contains.Location = new System.Drawing.Point(3, 186);
			this.albumcontainsword_contains.Name = "albumcontainsword_contains";
			this.albumcontainsword_contains.Size = new System.Drawing.Size(224, 20);
			this.albumcontainsword_contains.TabIndex = 14;
			// 
			// panel1
			// 
			this.panel1.AutoScroll = true;
			this.panel1.Controls.Add(this.label6);
			this.panel1.Controls.Add(this.filenameis_audiofilename);
			this.panel1.Controls.Add(this.filenameis_add);
			this.panel1.Controls.Add(this.filenameis_discordfilename);
			this.panel1.Controls.Add(this.filenameis_priority);
			this.panel1.Controls.Add(this.label5);
			this.panel1.Controls.Add(this.artistsname_artist);
			this.panel1.Controls.Add(this.artistname_add);
			this.panel1.Controls.Add(this.artistsname_filename);
			this.panel1.Controls.Add(this.artistsname_priority);
			this.panel1.Controls.Add(this.label4);
			this.panel1.Controls.Add(this.albumcontainsword_filename);
			this.panel1.Controls.Add(this.specificalbumname_name);
			this.panel1.Controls.Add(this.label2);
			this.panel1.Controls.Add(this.titlecontainsword_contains);
			this.panel1.Controls.Add(this.button1);
			this.panel1.Controls.Add(this.titlecontainsword_containsnot);
			this.panel1.Controls.Add(this.albumcontainsword_priority);
			this.panel1.Controls.Add(this.titlecontainsword_contains_filename);
			this.panel1.Controls.Add(this.albumcontainsword_containsnot);
			this.panel1.Controls.Add(this.album_add);
			this.panel1.Controls.Add(this.albumcontainsword_contains);
			this.panel1.Controls.Add(this.specificalbumname_filename);
			this.panel1.Controls.Add(this.titlecontainsword_priority);
			this.panel1.Controls.Add(this.titlebased_add);
			this.panel1.Controls.Add(this.specificalbumname_priority);
			this.panel1.Controls.Add(this.label3);
			this.panel1.Location = new System.Drawing.Point(695, -6);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(247, 482);
			this.panel1.TabIndex = 20;
			// 
			// label6
			// 
			this.label6.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.label6.Location = new System.Drawing.Point(3, 632);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(224, 16);
			this.label6.TabIndex = 28;
			this.label6.Text = "Finding art based on artist";
			// 
			// filenameis_audiofilename
			// 
			this.filenameis_audiofilename.Location = new System.Drawing.Point(3, 651);
			this.filenameis_audiofilename.Name = "filenameis_audiofilename";
			this.filenameis_audiofilename.Size = new System.Drawing.Size(224, 20);
			this.filenameis_audiofilename.TabIndex = 25;
			// 
			// filenameis_add
			// 
			this.filenameis_add.Location = new System.Drawing.Point(3, 723);
			this.filenameis_add.Name = "filenameis_add";
			this.filenameis_add.Size = new System.Drawing.Size(75, 23);
			this.filenameis_add.TabIndex = 26;
			this.filenameis_add.Text = "Add pair";
			this.filenameis_add.UseVisualStyleBackColor = true;
			this.filenameis_add.Click += new System.EventHandler(this.filenameis_add_Click);
			// 
			// filenameis_discordfilename
			// 
			this.filenameis_discordfilename.Location = new System.Drawing.Point(3, 675);
			this.filenameis_discordfilename.Name = "filenameis_discordfilename";
			this.filenameis_discordfilename.Size = new System.Drawing.Size(224, 20);
			this.filenameis_discordfilename.TabIndex = 27;
			// 
			// filenameis_priority
			// 
			this.filenameis_priority.Location = new System.Drawing.Point(3, 699);
			this.filenameis_priority.Name = "filenameis_priority";
			this.filenameis_priority.Size = new System.Drawing.Size(224, 20);
			this.filenameis_priority.TabIndex = 29;
			// 
			// label5
			// 
			this.label5.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.label5.Location = new System.Drawing.Point(3, 497);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(224, 16);
			this.label5.TabIndex = 23;
			this.label5.Text = "Finding art based on artist";
			// 
			// artistsname_artist
			// 
			this.artistsname_artist.Location = new System.Drawing.Point(3, 516);
			this.artistsname_artist.Name = "artistsname_artist";
			this.artistsname_artist.Size = new System.Drawing.Size(224, 20);
			this.artistsname_artist.TabIndex = 20;
			// 
			// artistname_add
			// 
			this.artistname_add.Location = new System.Drawing.Point(3, 588);
			this.artistname_add.Name = "artistname_add";
			this.artistname_add.Size = new System.Drawing.Size(75, 23);
			this.artistname_add.TabIndex = 21;
			this.artistname_add.Text = "Add pair";
			this.artistname_add.UseVisualStyleBackColor = true;
			this.artistname_add.Click += new System.EventHandler(this.artistname_add_Click);
			// 
			// artistsname_filename
			// 
			this.artistsname_filename.Location = new System.Drawing.Point(3, 540);
			this.artistsname_filename.Name = "artistsname_filename";
			this.artistsname_filename.Size = new System.Drawing.Size(224, 20);
			this.artistsname_filename.TabIndex = 22;
			// 
			// artistsname_priority
			// 
			this.artistsname_priority.Location = new System.Drawing.Point(3, 564);
			this.artistsname_priority.Name = "artistsname_priority";
			this.artistsname_priority.Size = new System.Drawing.Size(224, 20);
			this.artistsname_priority.TabIndex = 24;
			// 
			// AlbumArtAdder
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(942, 476);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.listBox1);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "AlbumArtAdder";
			this.Text = "AlbumArtAdder";
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.AlbumArtAdder_FormClosed);
			this.Load += new System.EventHandler(this.AlbumArtAdder_Load);
			this.SizeChanged += new System.EventHandler(this.AlbumArtAdder_SizeChanged);
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.ResumeLayout(false);

        }

        #endregion

        private Component1 component11;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.TextBox titlecontainsword_contains;
        private System.Windows.Forms.TextBox titlecontainsword_containsnot;
        private System.Windows.Forms.TextBox titlecontainsword_contains_filename;
        private System.Windows.Forms.Button album_add;
        private System.Windows.Forms.TextBox specificalbumname_filename;
        private System.Windows.Forms.Button titlebased_add;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox specificalbumname_name;
        private System.Windows.Forms.TextBox specificalbumname_priority;
        private System.Windows.Forms.TextBox titlecontainsword_priority;
        private System.Windows.Forms.TextBox albumcontainsword_filename;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox albumcontainsword_priority;
        private System.Windows.Forms.TextBox albumcontainsword_containsnot;
        private System.Windows.Forms.TextBox albumcontainsword_contains;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.TextBox filenameis_audiofilename;
		private System.Windows.Forms.Button filenameis_add;
		private System.Windows.Forms.TextBox filenameis_discordfilename;
		private System.Windows.Forms.TextBox filenameis_priority;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.TextBox artistsname_artist;
		private System.Windows.Forms.Button artistname_add;
		private System.Windows.Forms.TextBox artistsname_filename;
		private System.Windows.Forms.TextBox artistsname_priority;
	}
}