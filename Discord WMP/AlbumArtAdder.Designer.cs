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
            this.specificalbumname_name.Location = new System.Drawing.Point(704, 32);
            this.specificalbumname_name.Name = "specificalbumname_name";
            this.specificalbumname_name.Size = new System.Drawing.Size(224, 20);
            this.specificalbumname_name.TabIndex = 1;
            // 
            // titlecontainsword_contains
            // 
            this.titlecontainsword_contains.Location = new System.Drawing.Point(704, 352);
            this.titlecontainsword_contains.Name = "titlecontainsword_contains";
            this.titlecontainsword_contains.Size = new System.Drawing.Size(224, 20);
            this.titlecontainsword_contains.TabIndex = 2;
            this.titlecontainsword_contains.TextChanged += new System.EventHandler(this.contains_TextChanged);
            // 
            // titlecontainsword_containsnot
            // 
            this.titlecontainsword_containsnot.Location = new System.Drawing.Point(704, 376);
            this.titlecontainsword_containsnot.Name = "titlecontainsword_containsnot";
            this.titlecontainsword_containsnot.Size = new System.Drawing.Size(224, 20);
            this.titlecontainsword_containsnot.TabIndex = 3;
            this.titlecontainsword_containsnot.TextChanged += new System.EventHandler(this.containsnot_TextChanged);
            // 
            // titlecontainsword_contains_filename
            // 
            this.titlecontainsword_contains_filename.Location = new System.Drawing.Point(704, 400);
            this.titlecontainsword_contains_filename.Name = "titlecontainsword_contains_filename";
            this.titlecontainsword_contains_filename.Size = new System.Drawing.Size(224, 20);
            this.titlecontainsword_contains_filename.TabIndex = 4;
            this.titlecontainsword_contains_filename.TextChanged += new System.EventHandler(this.title_filename_TextChanged);
            // 
            // album_add
            // 
            this.album_add.Location = new System.Drawing.Point(704, 104);
            this.album_add.Name = "album_add";
            this.album_add.Size = new System.Drawing.Size(75, 23);
            this.album_add.TabIndex = 5;
            this.album_add.Text = "Add pair";
            this.album_add.UseVisualStyleBackColor = true;
            this.album_add.Click += new System.EventHandler(this.button1_Click);
            // 
            // specificalbumname_filename
            // 
            this.specificalbumname_filename.Location = new System.Drawing.Point(704, 56);
            this.specificalbumname_filename.Name = "specificalbumname_filename";
            this.specificalbumname_filename.Size = new System.Drawing.Size(224, 20);
            this.specificalbumname_filename.TabIndex = 6;
            // 
            // titlebased_add
            // 
            this.titlebased_add.Location = new System.Drawing.Point(704, 448);
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
            this.label3.Location = new System.Drawing.Point(704, 320);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(224, 32);
            this.label3.TabIndex = 10;
            this.label3.Text = "Finding art based on title containing words";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label4.Location = new System.Drawing.Point(704, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(224, 32);
            this.label4.TabIndex = 11;
            this.label4.Text = "Finding art based on specific album name";
            // 
            // specificalbumname_priority
            // 
            this.specificalbumname_priority.Location = new System.Drawing.Point(704, 80);
            this.specificalbumname_priority.Name = "specificalbumname_priority";
            this.specificalbumname_priority.Size = new System.Drawing.Size(224, 20);
            this.specificalbumname_priority.TabIndex = 12;
            // 
            // titlecontainsword_priority
            // 
            this.titlecontainsword_priority.Location = new System.Drawing.Point(704, 424);
            this.titlecontainsword_priority.Name = "titlecontainsword_priority";
            this.titlecontainsword_priority.Size = new System.Drawing.Size(224, 20);
            this.titlecontainsword_priority.TabIndex = 13;
            // 
            // albumcontainsword_filename
            // 
            this.albumcontainsword_filename.Location = new System.Drawing.Point(704, 248);
            this.albumcontainsword_filename.Name = "albumcontainsword_filename";
            this.albumcontainsword_filename.Size = new System.Drawing.Size(224, 20);
            this.albumcontainsword_filename.TabIndex = 19;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label2.Location = new System.Drawing.Point(704, 144);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(224, 32);
            this.label2.TabIndex = 18;
            this.label2.Text = "Finding art based on album containing words";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(704, 272);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 17;
            this.button1.Text = "Add pair";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // albumcontainsword_priority
            // 
            this.albumcontainsword_priority.Location = new System.Drawing.Point(704, 224);
            this.albumcontainsword_priority.Name = "albumcontainsword_priority";
            this.albumcontainsword_priority.Size = new System.Drawing.Size(224, 20);
            this.albumcontainsword_priority.TabIndex = 16;
            // 
            // albumcontainsword_containsnot
            // 
            this.albumcontainsword_containsnot.Location = new System.Drawing.Point(704, 200);
            this.albumcontainsword_containsnot.Name = "albumcontainsword_containsnot";
            this.albumcontainsword_containsnot.Size = new System.Drawing.Size(224, 20);
            this.albumcontainsword_containsnot.TabIndex = 15;
            // 
            // albumcontainsword_contains
            // 
            this.albumcontainsword_contains.Location = new System.Drawing.Point(704, 176);
            this.albumcontainsword_contains.Name = "albumcontainsword_contains";
            this.albumcontainsword_contains.Size = new System.Drawing.Size(224, 20);
            this.albumcontainsword_contains.TabIndex = 14;
            // 
            // AlbumArtAdder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(934, 476);
            this.Controls.Add(this.albumcontainsword_filename);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.albumcontainsword_priority);
            this.Controls.Add(this.albumcontainsword_containsnot);
            this.Controls.Add(this.albumcontainsword_contains);
            this.Controls.Add(this.titlecontainsword_priority);
            this.Controls.Add(this.specificalbumname_priority);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.titlebased_add);
            this.Controls.Add(this.specificalbumname_filename);
            this.Controls.Add(this.album_add);
            this.Controls.Add(this.titlecontainsword_contains_filename);
            this.Controls.Add(this.titlecontainsword_containsnot);
            this.Controls.Add(this.titlecontainsword_contains);
            this.Controls.Add(this.specificalbumname_name);
            this.Controls.Add(this.listBox1);
            this.Name = "AlbumArtAdder";
            this.Text = "AlbumArtAdder";
            this.Load += new System.EventHandler(this.AlbumArtAdder_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

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
    }
}