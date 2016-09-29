namespace AIO2013
{
    partial class Start_manually_file_action
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Start_manually_file_action));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.delRdio = new System.Windows.Forms.RadioButton();
            this.copyRdio = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dirRdio = new System.Windows.Forms.RadioButton();
            this.fileRdio = new System.Windows.Forms.RadioButton();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dstTxtbx = new System.Windows.Forms.TextBox();
            this.goBtn = new System.Windows.Forms.Button();
            this.cncleBtn = new System.Windows.Forms.Button();
            this.srcGroupBox1 = new System.Windows.Forms.GroupBox();
            this.srcCreateDirChkbx = new System.Windows.Forms.CheckBox();
            this.srcPathTxtbx = new System.Windows.Forms.TextBox();
            this.srcBrwsBtn = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.srcGroupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.delRdio);
            this.groupBox1.Controls.Add(this.copyRdio);
            this.groupBox1.Location = new System.Drawing.Point(7, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(272, 44);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "1. Copy or Delete";
            // 
            // delRdio
            // 
            this.delRdio.AutoSize = true;
            this.delRdio.Location = new System.Drawing.Point(157, 20);
            this.delRdio.Name = "delRdio";
            this.delRdio.Size = new System.Drawing.Size(56, 17);
            this.delRdio.TabIndex = 1;
            this.delRdio.Text = "Delete";
            this.delRdio.UseVisualStyleBackColor = true;
            // 
            // copyRdio
            // 
            this.copyRdio.AutoSize = true;
            this.copyRdio.Checked = true;
            this.copyRdio.Location = new System.Drawing.Point(65, 20);
            this.copyRdio.Name = "copyRdio";
            this.copyRdio.Size = new System.Drawing.Size(49, 17);
            this.copyRdio.TabIndex = 0;
            this.copyRdio.TabStop = true;
            this.copyRdio.Text = "Copy";
            this.copyRdio.UseVisualStyleBackColor = true;
            this.copyRdio.CheckedChanged += new System.EventHandler(this.copyRdio_CheckedChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dirRdio);
            this.groupBox2.Controls.Add(this.fileRdio);
            this.groupBox2.Location = new System.Drawing.Point(7, 54);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(272, 44);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "2. File or Directory";
            // 
            // dirRdio
            // 
            this.dirRdio.AutoSize = true;
            this.dirRdio.Location = new System.Drawing.Point(157, 21);
            this.dirRdio.Name = "dirRdio";
            this.dirRdio.Size = new System.Drawing.Size(67, 17);
            this.dirRdio.TabIndex = 1;
            this.dirRdio.Text = "Directory";
            this.dirRdio.UseVisualStyleBackColor = true;
            // 
            // fileRdio
            // 
            this.fileRdio.AutoSize = true;
            this.fileRdio.Checked = true;
            this.fileRdio.Location = new System.Drawing.Point(65, 21);
            this.fileRdio.Name = "fileRdio";
            this.fileRdio.Size = new System.Drawing.Size(41, 17);
            this.fileRdio.TabIndex = 0;
            this.fileRdio.TabStop = true;
            this.fileRdio.Text = "File";
            this.fileRdio.UseVisualStyleBackColor = true;
            this.fileRdio.CheckedChanged += new System.EventHandler(this.fileRdio_CheckedChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.dstTxtbx);
            this.groupBox3.Location = new System.Drawing.Point(7, 176);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(272, 105);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "4. Destination";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label1.Location = new System.Drawing.Point(5, 47);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(251, 52);
            this.label1.TabIndex = 7;
            this.label1.Text = "Tags: \r\n<remotepc> - The selected pc.\r\n<username> - The currently logged on user." +
                "\r\n<userdomain> - Logged on user domain.";
            this.toolTip1.SetToolTip(this.label1, "This tags will be automaticlly replaced when the spcecific action will be emited." +
                    "");
            // 
            // dstTxtbx
            // 
            this.dstTxtbx.Location = new System.Drawing.Point(7, 22);
            this.dstTxtbx.Name = "dstTxtbx";
            this.dstTxtbx.Size = new System.Drawing.Size(258, 20);
            this.dstTxtbx.TabIndex = 0;
            this.dstTxtbx.Text = "\\\\<remotepc>\\c$\\";
            this.toolTip1.SetToolTip(this.dstTxtbx, "Use a UNC path with the tags below to build the path.\r\n\"\\\\<remotepc>\\c$\\\":\r\nThis " +
                    "is the root drive: c:\\ on the remote pc.");
            // 
            // goBtn
            // 
            this.goBtn.Location = new System.Drawing.Point(72, 287);
            this.goBtn.Name = "goBtn";
            this.goBtn.Size = new System.Drawing.Size(56, 27);
            this.goBtn.TabIndex = 3;
            this.goBtn.Text = "Go";
            this.goBtn.UseVisualStyleBackColor = true;
            this.goBtn.Click += new System.EventHandler(this.goBtn_Click);
            // 
            // cncleBtn
            // 
            this.cncleBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cncleBtn.Location = new System.Drawing.Point(164, 287);
            this.cncleBtn.Name = "cncleBtn";
            this.cncleBtn.Size = new System.Drawing.Size(56, 27);
            this.cncleBtn.TabIndex = 4;
            this.cncleBtn.Text = "Cancle";
            this.cncleBtn.UseVisualStyleBackColor = true;
            this.cncleBtn.Click += new System.EventHandler(this.cncleBtn_Click);
            // 
            // srcGroupBox1
            // 
            this.srcGroupBox1.Controls.Add(this.srcCreateDirChkbx);
            this.srcGroupBox1.Controls.Add(this.srcPathTxtbx);
            this.srcGroupBox1.Controls.Add(this.srcBrwsBtn);
            this.srcGroupBox1.Location = new System.Drawing.Point(7, 103);
            this.srcGroupBox1.Margin = new System.Windows.Forms.Padding(2);
            this.srcGroupBox1.Name = "srcGroupBox1";
            this.srcGroupBox1.Padding = new System.Windows.Forms.Padding(2);
            this.srcGroupBox1.Size = new System.Drawing.Size(272, 68);
            this.srcGroupBox1.TabIndex = 5;
            this.srcGroupBox1.TabStop = false;
            this.srcGroupBox1.Text = "3. Source";
            // 
            // srcCreateDirChkbx
            // 
            this.srcCreateDirChkbx.AutoSize = true;
            this.srcCreateDirChkbx.Enabled = false;
            this.srcCreateDirChkbx.Location = new System.Drawing.Point(5, 48);
            this.srcCreateDirChkbx.Margin = new System.Windows.Forms.Padding(2);
            this.srcCreateDirChkbx.Name = "srcCreateDirChkbx";
            this.srcCreateDirChkbx.Size = new System.Drawing.Size(261, 17);
            this.srcCreateDirChkbx.TabIndex = 4;
            this.srcCreateDirChkbx.Text = "Don\'t create directory on destination. Content only";
            this.toolTip1.SetToolTip(this.srcCreateDirChkbx, "This will copy only the files in the source directory,\r\nwithout the directory its" +
                    "elf. ");
            this.srcCreateDirChkbx.UseVisualStyleBackColor = true;
            this.srcCreateDirChkbx.CheckedChanged += new System.EventHandler(this.srcCreateDirChkbx_CheckedChanged);
            // 
            // srcPathTxtbx
            // 
            this.srcPathTxtbx.Location = new System.Drawing.Point(4, 24);
            this.srcPathTxtbx.Margin = new System.Windows.Forms.Padding(2);
            this.srcPathTxtbx.Name = "srcPathTxtbx";
            this.srcPathTxtbx.Size = new System.Drawing.Size(198, 20);
            this.srcPathTxtbx.TabIndex = 2;
            // 
            // srcBrwsBtn
            // 
            this.srcBrwsBtn.Location = new System.Drawing.Point(206, 24);
            this.srcBrwsBtn.Margin = new System.Windows.Forms.Padding(2);
            this.srcBrwsBtn.Name = "srcBrwsBtn";
            this.srcBrwsBtn.Size = new System.Drawing.Size(53, 19);
            this.srcBrwsBtn.TabIndex = 3;
            this.srcBrwsBtn.Text = "Browse";
            this.srcBrwsBtn.UseVisualStyleBackColor = true;
            this.srcBrwsBtn.Click += new System.EventHandler(this.srcBrwsBtn_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.DereferenceLinks = false;
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // Start_manually_file_action
            // 
            this.AcceptButton = this.goBtn;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cncleBtn;
            this.ClientSize = new System.Drawing.Size(284, 317);
            this.Controls.Add(this.srcGroupBox1);
            this.Controls.Add(this.cncleBtn);
            this.Controls.Add(this.goBtn);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Start_manually_file_action";
            this.Text = "Start manually file action";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.srcGroupBox1.ResumeLayout(false);
            this.srcGroupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton copyRdio;
        private System.Windows.Forms.RadioButton delRdio;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton fileRdio;
        private System.Windows.Forms.RadioButton dirRdio;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox dstTxtbx;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button goBtn;
        private System.Windows.Forms.Button cncleBtn;
        private System.Windows.Forms.GroupBox srcGroupBox1;
        private System.Windows.Forms.CheckBox srcCreateDirChkbx;
        private System.Windows.Forms.TextBox srcPathTxtbx;
        private System.Windows.Forms.Button srcBrwsBtn;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}