namespace AIO2013
{
    partial class newDelPackageForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(newDelPackageForm));
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.srcPathTxtbx = new System.Windows.Forms.TextBox();
            this.srcBrwsBtn = new System.Windows.Forms.Button();
            this.srcGroupBox1 = new System.Windows.Forms.GroupBox();
            this.srcCreateDirChkbx = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dstTxtbx = new System.Windows.Forms.TextBox();
            this.OKbtn = new System.Windows.Forms.Button();
            this.cncleBtn = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.dirRdio = new System.Windows.Forms.RadioButton();
            this.fileRdio = new System.Windows.Forms.RadioButton();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.pkgNameTxtbx = new System.Windows.Forms.TextBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.delRdio = new System.Windows.Forms.RadioButton();
            this.copyRdio = new System.Windows.Forms.RadioButton();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.srcGroupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.SuspendLayout();
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.DereferenceLinks = false;
            this.openFileDialog1.FileName = "openFileDialog1";
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
            // srcGroupBox1
            // 
            this.srcGroupBox1.Controls.Add(this.srcCreateDirChkbx);
            this.srcGroupBox1.Controls.Add(this.srcPathTxtbx);
            this.srcGroupBox1.Controls.Add(this.srcBrwsBtn);
            this.srcGroupBox1.Location = new System.Drawing.Point(7, 149);
            this.srcGroupBox1.Margin = new System.Windows.Forms.Padding(2);
            this.srcGroupBox1.Name = "srcGroupBox1";
            this.srcGroupBox1.Padding = new System.Windows.Forms.Padding(2);
            this.srcGroupBox1.Size = new System.Drawing.Size(272, 68);
            this.srcGroupBox1.TabIndex = 4;
            this.srcGroupBox1.TabStop = false;
            this.srcGroupBox1.Text = "4. Source";
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
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.dstTxtbx);
            this.groupBox2.Location = new System.Drawing.Point(7, 223);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox2.Size = new System.Drawing.Size(272, 111);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "5. Destination";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label1.Location = new System.Drawing.Point(5, 50);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(251, 52);
            this.label1.TabIndex = 6;
            this.label1.Text = "Tags: \r\n<remotepc> - The selected pc.\r\n<username> - The currently logged on user." +
                "\r\n<userdomain> - Logged on user domain.";
            this.toolTip1.SetToolTip(this.label1, "This tags will be automaticlly replaced when the spcecific action will be emited." +
                    "");
            // 
            // dstTxtbx
            // 
            this.dstTxtbx.Location = new System.Drawing.Point(4, 23);
            this.dstTxtbx.Margin = new System.Windows.Forms.Padding(2);
            this.dstTxtbx.Name = "dstTxtbx";
            this.dstTxtbx.Size = new System.Drawing.Size(255, 20);
            this.dstTxtbx.TabIndex = 4;
            this.dstTxtbx.Text = "\\\\<remotepc>\\c$\\";
            this.toolTip1.SetToolTip(this.dstTxtbx, "Use a UNC path with the tags below to build the path.\r\n\"\\\\<remotepc>\\c$\\\":\r\nThis " +
                    "is the root drive: c:\\ on the remote pc.");
            // 
            // OKbtn
            // 
            this.OKbtn.Location = new System.Drawing.Point(75, 338);
            this.OKbtn.Margin = new System.Windows.Forms.Padding(2);
            this.OKbtn.Name = "OKbtn";
            this.OKbtn.Size = new System.Drawing.Size(56, 27);
            this.OKbtn.TabIndex = 6;
            this.OKbtn.Text = "OK";
            this.OKbtn.UseVisualStyleBackColor = true;
            this.OKbtn.Click += new System.EventHandler(this.OKbtn_Click);
            // 
            // cncleBtn
            // 
            this.cncleBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cncleBtn.Location = new System.Drawing.Point(157, 338);
            this.cncleBtn.Margin = new System.Windows.Forms.Padding(2);
            this.cncleBtn.Name = "cncleBtn";
            this.cncleBtn.Size = new System.Drawing.Size(56, 27);
            this.cncleBtn.TabIndex = 7;
            this.cncleBtn.Text = "Cancle";
            this.cncleBtn.UseVisualStyleBackColor = true;
            this.cncleBtn.Click += new System.EventHandler(this.cncleBtn_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.dirRdio);
            this.groupBox3.Controls.Add(this.fileRdio);
            this.groupBox3.Location = new System.Drawing.Point(7, 102);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox3.Size = new System.Drawing.Size(272, 44);
            this.groupBox3.TabIndex = 8;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "3. File or Directory";
            // 
            // dirRdio
            // 
            this.dirRdio.AutoSize = true;
            this.dirRdio.Location = new System.Drawing.Point(148, 17);
            this.dirRdio.Margin = new System.Windows.Forms.Padding(2);
            this.dirRdio.Name = "dirRdio";
            this.dirRdio.Size = new System.Drawing.Size(67, 17);
            this.dirRdio.TabIndex = 3;
            this.dirRdio.Text = "Directory";
            this.dirRdio.UseVisualStyleBackColor = true;
            // 
            // fileRdio
            // 
            this.fileRdio.AutoSize = true;
            this.fileRdio.Checked = true;
            this.fileRdio.Location = new System.Drawing.Point(76, 17);
            this.fileRdio.Margin = new System.Windows.Forms.Padding(2);
            this.fileRdio.Name = "fileRdio";
            this.fileRdio.Size = new System.Drawing.Size(41, 17);
            this.fileRdio.TabIndex = 2;
            this.fileRdio.TabStop = true;
            this.fileRdio.Text = "File";
            this.fileRdio.UseVisualStyleBackColor = true;
            this.fileRdio.CheckedChanged += new System.EventHandler(this.dirRdio_CheckedChanged);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.pkgNameTxtbx);
            this.groupBox4.Location = new System.Drawing.Point(7, 3);
            this.groupBox4.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox4.Size = new System.Drawing.Size(272, 44);
            this.groupBox4.TabIndex = 9;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "1. Package Name";
            // 
            // pkgNameTxtbx
            // 
            this.pkgNameTxtbx.Location = new System.Drawing.Point(53, 17);
            this.pkgNameTxtbx.Margin = new System.Windows.Forms.Padding(2);
            this.pkgNameTxtbx.Name = "pkgNameTxtbx";
            this.pkgNameTxtbx.Size = new System.Drawing.Size(162, 20);
            this.pkgNameTxtbx.TabIndex = 0;
            this.toolTip1.SetToolTip(this.pkgNameTxtbx, "Give your package a name.\r\nThe name must be unique.");
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.delRdio);
            this.groupBox5.Controls.Add(this.copyRdio);
            this.groupBox5.Location = new System.Drawing.Point(7, 52);
            this.groupBox5.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox5.Size = new System.Drawing.Size(272, 44);
            this.groupBox5.TabIndex = 9;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "2. Copy or Delete";
            // 
            // delRdio
            // 
            this.delRdio.AutoSize = true;
            this.delRdio.Location = new System.Drawing.Point(148, 17);
            this.delRdio.Margin = new System.Windows.Forms.Padding(2);
            this.delRdio.Name = "delRdio";
            this.delRdio.Size = new System.Drawing.Size(56, 17);
            this.delRdio.TabIndex = 3;
            this.delRdio.Text = "Delete";
            this.delRdio.UseVisualStyleBackColor = true;
            // 
            // copyRdio
            // 
            this.copyRdio.AutoSize = true;
            this.copyRdio.Checked = true;
            this.copyRdio.Location = new System.Drawing.Point(76, 17);
            this.copyRdio.Margin = new System.Windows.Forms.Padding(2);
            this.copyRdio.Name = "copyRdio";
            this.copyRdio.Size = new System.Drawing.Size(49, 17);
            this.copyRdio.TabIndex = 2;
            this.copyRdio.TabStop = true;
            this.copyRdio.Text = "Copy";
            this.copyRdio.UseVisualStyleBackColor = true;
            this.copyRdio.CheckedChanged += new System.EventHandler(this.copyRdio_CheckedChanged);
            // 
            // newDelPackageForm
            // 
            this.AcceptButton = this.OKbtn;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.CancelButton = this.cncleBtn;
            this.ClientSize = new System.Drawing.Size(284, 368);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.cncleBtn);
            this.Controls.Add(this.OKbtn);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.srcGroupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.Name = "newDelPackageForm";
            this.Text = "Add a new Preset package";
            this.srcGroupBox1.ResumeLayout(false);
            this.srcGroupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.TextBox srcPathTxtbx;
        private System.Windows.Forms.Button srcBrwsBtn;
        private System.Windows.Forms.GroupBox srcGroupBox1;
        private System.Windows.Forms.CheckBox srcCreateDirChkbx;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox dstTxtbx;
        private System.Windows.Forms.Button OKbtn;
        private System.Windows.Forms.Button cncleBtn;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.RadioButton dirRdio;
        private System.Windows.Forms.RadioButton fileRdio;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.TextBox pkgNameTxtbx;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.RadioButton delRdio;
        private System.Windows.Forms.RadioButton copyRdio;
        private System.Windows.Forms.ToolTip toolTip1;

    }
}