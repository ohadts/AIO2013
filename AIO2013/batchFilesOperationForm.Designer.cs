namespace AIO2013
{
    partial class batch_files_operation
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
            this.mainTxtBx = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.sepComboBx = new System.Windows.Forms.ComboBox();
            this.pasteBtn = new System.Windows.Forms.Button();
            this.OkBtn = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.CnclBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // mainTxtBx
            // 
            this.mainTxtBx.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.mainTxtBx.Location = new System.Drawing.Point(3, 21);
            this.mainTxtBx.MinimumSize = new System.Drawing.Size(30, 30);
            this.mainTxtBx.Multiline = true;
            this.mainTxtBx.Name = "mainTxtBx";
            this.mainTxtBx.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.mainTxtBx.Size = new System.Drawing.Size(240, 196);
            this.mainTxtBx.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 223);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(102, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Choose a seperator:";
            // 
            // sepComboBx
            // 
            this.sepComboBx.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.sepComboBx.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.sepComboBx.FormattingEnabled = true;
            this.sepComboBx.Items.AddRange(new object[] {
            "New line",
            "Space",
            ";",
            "|",
            ".",
            ","});
            this.sepComboBx.Location = new System.Drawing.Point(107, 220);
            this.sepComboBx.Name = "sepComboBx";
            this.sepComboBx.Size = new System.Drawing.Size(65, 21);
            this.sepComboBx.TabIndex = 2;
            this.toolTip1.SetToolTip(this.sepComboBx, "Choose the character that seperates the hosts");
            // 
            // pasteBtn
            // 
            this.pasteBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.pasteBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.pasteBtn.Image = global::AIO2013.Properties.Resources.klipper_dock;
            this.pasteBtn.Location = new System.Drawing.Point(219, 220);
            this.pasteBtn.Name = "pasteBtn";
            this.pasteBtn.Size = new System.Drawing.Size(23, 23);
            this.pasteBtn.TabIndex = 16;
            this.toolTip1.SetToolTip(this.pasteBtn, "Paste from clipboard");
            this.pasteBtn.UseVisualStyleBackColor = true;
            this.pasteBtn.Click += new System.EventHandler(this.pasteBtn_Click);
            // 
            // OkBtn
            // 
            this.OkBtn.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.OkBtn.Location = new System.Drawing.Point(135, 255);
            this.OkBtn.Name = "OkBtn";
            this.OkBtn.Size = new System.Drawing.Size(75, 23);
            this.OkBtn.TabIndex = 17;
            this.OkBtn.Text = "OK";
            this.OkBtn.UseVisualStyleBackColor = true;
            this.OkBtn.Click += new System.EventHandler(this.OkBtn_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(1, 3);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(134, 13);
            this.label2.TabIndex = 18;
            this.label2.Text = "Enter a list of remote hosts:";
            // 
            // CnclBtn
            // 
            this.CnclBtn.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.CnclBtn.Location = new System.Drawing.Point(43, 255);
            this.CnclBtn.Name = "CnclBtn";
            this.CnclBtn.Size = new System.Drawing.Size(75, 23);
            this.CnclBtn.TabIndex = 19;
            this.CnclBtn.Text = "Cancel";
            this.CnclBtn.UseVisualStyleBackColor = true;
            this.CnclBtn.Click += new System.EventHandler(this.CnclBtn_Click);
            // 
            // batch_files_operation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.CnclBtn;
            this.ClientSize = new System.Drawing.Size(246, 284);
            this.Controls.Add(this.CnclBtn);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.OkBtn);
            this.Controls.Add(this.pasteBtn);
            this.Controls.Add(this.sepComboBx);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.mainTxtBx);
            this.MinimumSize = new System.Drawing.Size(220, 160);
            this.Name = "batch_files_operation";
            this.Text = "Batch Operation";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox mainTxtBx;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox sepComboBx;
        private System.Windows.Forms.Button pasteBtn;
        private System.Windows.Forms.Button OkBtn;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Button CnclBtn;
    }
}