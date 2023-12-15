namespace Discord_WMP {
    partial class Form1 {
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
			this.update = new System.Windows.Forms.Timer(this.components);
			this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
			this.client_id = new System.Windows.Forms.TextBox();
			this.panel1 = new System.Windows.Forms.Panel();
			this.button1 = new System.Windows.Forms.Button();
			this.checkBox_sendMediaInfo = new System.Windows.Forms.CheckBox();
			this.checkBox_showconsole = new System.Windows.Forms.CheckBox();
			this.checkBox_userpc = new System.Windows.Forms.CheckBox();
			this.label1 = new System.Windows.Forms.Label();
			this.numericUpDown_retryattempts = new System.Windows.Forms.NumericUpDown();
			this.button_settings = new System.Windows.Forms.Button();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.checkBox_dontautohide = new System.Windows.Forms.CheckBox();
			this.label2 = new System.Windows.Forms.Label();
			this.linkLabel1 = new System.Windows.Forms.LinkLabel();
			this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown_retryattempts)).BeginInit();
			this.SuspendLayout();
			// 
			// update
			// 
			this.update.Enabled = true;
			this.update.Interval = 1000;
			this.update.Tick += new System.EventHandler(this.update_Tick);
			// 
			// notifyIcon1
			// 
			this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
			this.notifyIcon1.Text = "notifyIcon1";
			this.notifyIcon1.Visible = true;
			// 
			// client_id
			// 
			this.client_id.Location = new System.Drawing.Point(8, 8);
			this.client_id.Name = "client_id";
			this.client_id.Size = new System.Drawing.Size(176, 20);
			this.client_id.TabIndex = 0;
			this.client_id.TextChanged += new System.EventHandler(this.client_id_TextChanged);
			// 
			// panel1
			// 
			this.panel1.Location = new System.Drawing.Point(0, 130);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(192, 96);
			this.panel1.TabIndex = 6;
			// 
			// button1
			// 
			this.button1.FlatAppearance.BorderSize = 4;
			this.button1.Location = new System.Drawing.Point(134, 104);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(32, 16);
			this.button1.TabIndex = 7;
			this.button1.Text = ";;;\r\n\r\n";
			this.toolTip1.SetToolTip(this.button1, "Show album art manager");
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// checkBox_sendMediaInfo
			// 
			this.checkBox_sendMediaInfo.AutoSize = true;
			this.checkBox_sendMediaInfo.Location = new System.Drawing.Point(0, 232);
			this.checkBox_sendMediaInfo.Name = "checkBox_sendMediaInfo";
			this.checkBox_sendMediaInfo.Size = new System.Drawing.Size(115, 30);
			this.checkBox_sendMediaInfo.TabIndex = 8;
			this.checkBox_sendMediaInfo.Text = "Send WMP media \r\ninfo to Windows";
			this.checkBox_sendMediaInfo.UseVisualStyleBackColor = true;
			this.checkBox_sendMediaInfo.CheckedChanged += new System.EventHandler(this.checkBox_changed);
			// 
			// checkBox_showconsole
			// 
			this.checkBox_showconsole.AutoSize = true;
			this.checkBox_showconsole.Location = new System.Drawing.Point(0, 268);
			this.checkBox_showconsole.Name = "checkBox_showconsole";
			this.checkBox_showconsole.Size = new System.Drawing.Size(91, 17);
			this.checkBox_showconsole.TabIndex = 9;
			this.checkBox_showconsole.Text = "show console";
			this.checkBox_showconsole.UseVisualStyleBackColor = true;
			this.checkBox_showconsole.CheckedChanged += new System.EventHandler(this.checkBox_changed);
			// 
			// checkBox_userpc
			// 
			this.checkBox_userpc.AutoSize = true;
			this.checkBox_userpc.Location = new System.Drawing.Point(0, 294);
			this.checkBox_userpc.Name = "checkBox_userpc";
			this.checkBox_userpc.Size = new System.Drawing.Size(109, 17);
			this.checkBox_userpc.TabIndex = 10;
			this.checkBox_userpc.Text = "Use Discord RPC";
			this.checkBox_userpc.UseVisualStyleBackColor = true;
			this.checkBox_userpc.CheckedChanged += new System.EventHandler(this.checkBox_changed);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(5, 314);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(70, 13);
			this.label1.TabIndex = 13;
			this.label1.Text = "retry attemps:";
			// 
			// numericUpDown_retryattempts
			// 
			this.numericUpDown_retryattempts.Location = new System.Drawing.Point(81, 311);
			this.numericUpDown_retryattempts.Name = "numericUpDown_retryattempts";
			this.numericUpDown_retryattempts.Size = new System.Drawing.Size(66, 20);
			this.numericUpDown_retryattempts.TabIndex = 14;
			this.numericUpDown_retryattempts.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
			// 
			// button_settings
			// 
			this.button_settings.FlatAppearance.BorderSize = 4;
			this.button_settings.Location = new System.Drawing.Point(172, 104);
			this.button_settings.Name = "button_settings";
			this.button_settings.Size = new System.Drawing.Size(22, 20);
			this.button_settings.TabIndex = 15;
			this.button_settings.Text = "⚙";
			this.button_settings.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			this.toolTip1.SetToolTip(this.button_settings, "Show settings menu");
			this.button_settings.UseVisualStyleBackColor = true;
			this.button_settings.Click += new System.EventHandler(this.button_settings_Click);
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(227, 21);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(35, 13);
			this.label3.TabIndex = 17;
			this.label3.Text = "label3";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(227, 43);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(35, 13);
			this.label4.TabIndex = 18;
			this.label4.Text = "label4";
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(227, 67);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(35, 13);
			this.label5.TabIndex = 19;
			this.label5.Text = "label5";
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(227, 91);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(35, 13);
			this.label6.TabIndex = 20;
			this.label6.Text = "label6";
			// 
			// checkBox_dontautohide
			// 
			this.checkBox_dontautohide.AutoSize = true;
			this.checkBox_dontautohide.Location = new System.Drawing.Point(0, 342);
			this.checkBox_dontautohide.Name = "checkBox_dontautohide";
			this.checkBox_dontautohide.Size = new System.Drawing.Size(70, 17);
			this.checkBox_dontautohide.TabIndex = 21;
			this.checkBox_dontautohide.Text = "dont hide";
			this.checkBox_dontautohide.UseVisualStyleBackColor = true;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(214, 2);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(117, 13);
			this.label2.TabIndex = 22;
			this.label2.Text = "Debug values:  (ignore)";
			// 
			// linkLabel1
			// 
			this.linkLabel1.AutoSize = true;
			this.linkLabel1.Location = new System.Drawing.Point(5, 367);
			this.linkLabel1.Name = "linkLabel1";
			this.linkLabel1.Size = new System.Drawing.Size(89, 13);
			this.linkLabel1.TabIndex = 23;
			this.linkLabel1.TabStop = true;
			this.linkLabel1.Text = "linkLabel_version";
			// 
			// toolTip1
			// 
			this.toolTip1.ToolTipTitle = "settings";
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.White;
			this.ClientSize = new System.Drawing.Size(192, 121);
			this.Controls.Add(this.linkLabel1);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.checkBox_dontautohide);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.button_settings);
			this.Controls.Add(this.numericUpDown_retryattempts);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.checkBox_userpc);
			this.Controls.Add(this.checkBox_showconsole);
			this.Controls.Add(this.checkBox_sendMediaInfo);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.client_id);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "Form1";
			this.Text = "WMP RPC";
			this.Deactivate += new System.EventHandler(this.Form1_Deactivate);
			this.Load += new System.EventHandler(this.Form1_Load);
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown_retryattempts)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Timer update;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.TextBox client_id;
        private System.Windows.Forms.Button button1;
		public System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.CheckBox checkBox_sendMediaInfo;
		private System.Windows.Forms.CheckBox checkBox_showconsole;
		private System.Windows.Forms.CheckBox checkBox_userpc;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.NumericUpDown numericUpDown_retryattempts;
		private System.Windows.Forms.Button button_settings;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.CheckBox checkBox_dontautohide;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.LinkLabel linkLabel1;
		private System.Windows.Forms.ToolTip toolTip1;
	}
}

