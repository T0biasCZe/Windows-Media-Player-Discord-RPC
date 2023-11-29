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
			this.panel1.Location = new System.Drawing.Point(0, 128);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(200, 96);
			this.panel1.TabIndex = 6;
			// 
			// button1
			// 
			this.button1.FlatAppearance.BorderSize = 4;
			this.button1.Location = new System.Drawing.Point(152, 104);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(32, 16);
			this.button1.TabIndex = 7;
			this.button1.Text = ";;;\r\n\r\n";
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
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.White;
			this.ClientSize = new System.Drawing.Size(192, 121);
			this.Controls.Add(this.checkBox_showconsole);
			this.Controls.Add(this.checkBox_sendMediaInfo);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.client_id);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "Form1";
			this.Text = "WMP RPC";
			this.Load += new System.EventHandler(this.Form1_Load);
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
	}
}

