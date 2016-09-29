namespace AIO2013
{
    partial class Open_sessions_query
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Open_sessions_query));
            this.label1 = new System.Windows.Forms.Label();
            this.hostTxtBx = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.objectListView1 = new BrightIdeasSoftware.ObjectListView();
            this.olvColumnUser = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumnPcName = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumnIp = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumnType = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumnOpenFiles = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumnConnTime = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumnIdle = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.copyUserNameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyComputerNameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyIpAddressToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.searchTextBox1 = new System.Windows.Forms.TextBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            ((System.ComponentModel.ISupportInitialize)(this.objectListView1)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(135, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Please Enter Remote Host:";
            // 
            // hostTxtBx
            // 
            this.hostTxtBx.Location = new System.Drawing.Point(177, 17);
            this.hostTxtBx.Name = "hostTxtBx";
            this.hostTxtBx.Size = new System.Drawing.Size(185, 20);
            this.hostTxtBx.TabIndex = 2;
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(413, 15);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(109, 23);
            this.button1.TabIndex = 3;
            this.button1.Text = "Fetch Data !";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar1.Location = new System.Drawing.Point(9, 43);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(513, 23);
            this.progressBar1.TabIndex = 4;
            // 
            // objectListView1
            // 
            this.objectListView1.AllColumns.Add(this.olvColumnUser);
            this.objectListView1.AllColumns.Add(this.olvColumnPcName);
            this.objectListView1.AllColumns.Add(this.olvColumnIp);
            this.objectListView1.AllColumns.Add(this.olvColumnType);
            this.objectListView1.AllColumns.Add(this.olvColumnOpenFiles);
            this.objectListView1.AllColumns.Add(this.olvColumnConnTime);
            this.objectListView1.AllColumns.Add(this.olvColumnIdle);
            this.objectListView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.objectListView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvColumnUser,
            this.olvColumnPcName,
            this.olvColumnIp,
            this.olvColumnType,
            this.olvColumnOpenFiles,
            this.olvColumnConnTime,
            this.olvColumnIdle});
            this.objectListView1.ContextMenuStrip = this.contextMenuStrip1;
            this.objectListView1.EmptyListMsg = "The List is Empty!";
            this.objectListView1.EmptyListMsgFont = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.objectListView1.FullRowSelect = true;
            this.objectListView1.Location = new System.Drawing.Point(9, 123);
            this.objectListView1.Name = "objectListView1";
            this.objectListView1.ShowGroups = false;
            this.objectListView1.Size = new System.Drawing.Size(513, 167);
            this.objectListView1.SmallImageList = this.imageList1;
            this.objectListView1.TabIndex = 5;
            this.objectListView1.UseCompatibleStateImageBehavior = false;
            this.objectListView1.UseFiltering = true;
            this.objectListView1.View = System.Windows.Forms.View.Details;
            // 
            // olvColumnUser
            // 
            this.olvColumnUser.AspectName = "user";
            this.olvColumnUser.CellPadding = null;
            this.olvColumnUser.IsEditable = false;
            this.olvColumnUser.Text = "User";
            this.olvColumnUser.Width = 128;
            // 
            // olvColumnPcName
            // 
            this.olvColumnPcName.AspectName = "pcName";
            this.olvColumnPcName.CellPadding = null;
            this.olvColumnPcName.Text = "Computer Name";
            // 
            // olvColumnIp
            // 
            this.olvColumnIp.AspectName = "ipAddress";
            this.olvColumnIp.CellPadding = null;
            this.olvColumnIp.Text = "Ip Address";
            // 
            // olvColumnType
            // 
            this.olvColumnType.AspectName = "type";
            this.olvColumnType.CellPadding = null;
            this.olvColumnType.Text = "Type";
            // 
            // olvColumnOpenFiles
            // 
            this.olvColumnOpenFiles.AspectName = "numOfFiles";
            this.olvColumnOpenFiles.CellPadding = null;
            this.olvColumnOpenFiles.Text = "# Open Files";
            // 
            // olvColumnConnTime
            // 
            this.olvColumnConnTime.AspectName = "conTime";
            this.olvColumnConnTime.CellPadding = null;
            this.olvColumnConnTime.Text = "Connected Time";
            // 
            // olvColumnIdle
            // 
            this.olvColumnIdle.AspectName = "idleTime";
            this.olvColumnIdle.CellPadding = null;
            this.olvColumnIdle.Text = "Idle Time";
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.copyUserNameToolStripMenuItem,
            this.copyComputerNameToolStripMenuItem,
            this.copyIpAddressToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(195, 70);
            // 
            // copyUserNameToolStripMenuItem
            // 
            this.copyUserNameToolStripMenuItem.Name = "copyUserNameToolStripMenuItem";
            this.copyUserNameToolStripMenuItem.Size = new System.Drawing.Size(194, 22);
            this.copyUserNameToolStripMenuItem.Text = "Copy User Name";
            this.copyUserNameToolStripMenuItem.Click += new System.EventHandler(this.copyUserNameToolStripMenuItem_Click);
            // 
            // copyComputerNameToolStripMenuItem
            // 
            this.copyComputerNameToolStripMenuItem.Name = "copyComputerNameToolStripMenuItem";
            this.copyComputerNameToolStripMenuItem.Size = new System.Drawing.Size(194, 22);
            this.copyComputerNameToolStripMenuItem.Text = "Copy Computer Name";
            this.copyComputerNameToolStripMenuItem.Click += new System.EventHandler(this.copyComputerNameToolStripMenuItem_Click);
            // 
            // copyIpAddressToolStripMenuItem
            // 
            this.copyIpAddressToolStripMenuItem.Name = "copyIpAddressToolStripMenuItem";
            this.copyIpAddressToolStripMenuItem.Size = new System.Drawing.Size(194, 22);
            this.copyIpAddressToolStripMenuItem.Text = "Copy Ip Address";
            this.copyIpAddressToolStripMenuItem.Click += new System.EventHandler(this.copyIpAddressToolStripMenuItem_Click);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "user-icon.png");
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.searchTextBox1);
            this.groupBox1.Enabled = false;
            this.groupBox1.Location = new System.Drawing.Point(13, 73);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(509, 44);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Filter Results";
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(103, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Search:";
            // 
            // searchTextBox1
            // 
            this.searchTextBox1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.searchTextBox1.Location = new System.Drawing.Point(164, 18);
            this.searchTextBox1.Name = "searchTextBox1";
            this.searchTextBox1.Size = new System.Drawing.Size(185, 20);
            this.searchTextBox1.TabIndex = 0;
            this.searchTextBox1.TextChanged += new System.EventHandler(this.searchTextBox1_TextChanged);
            this.searchTextBox1.KeyUp += new System.Windows.Forms.KeyEventHandler(this.searchTextBox1_KeyUp);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 289);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(531, 22);
            this.statusStrip1.TabIndex = 7;
            this.statusStrip1.Text = "Use Right-Click to copy content";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(0, 17);
            // 
            // Open_sessions_query
            // 
            this.AcceptButton = this.button1;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(531, 311);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.objectListView1);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.hostTxtBx);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(500, 300);
            this.Name = "Open_sessions_query";
            this.Text = "Open Sessions Query";
            ((System.ComponentModel.ISupportInitialize)(this.objectListView1)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion


        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox hostTxtBx;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ProgressBar progressBar1;
        private BrightIdeasSoftware.ObjectListView objectListView1;
        private BrightIdeasSoftware.OLVColumn olvColumnUser;
        private BrightIdeasSoftware.OLVColumn olvColumnPcName;
        private BrightIdeasSoftware.OLVColumn olvColumnIp;
        private BrightIdeasSoftware.OLVColumn olvColumnType;
        private BrightIdeasSoftware.OLVColumn olvColumnOpenFiles;
        private BrightIdeasSoftware.OLVColumn olvColumnConnTime;
        private BrightIdeasSoftware.OLVColumn olvColumnIdle;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox searchTextBox1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem copyUserNameToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyComputerNameToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyIpAddressToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
    }
}