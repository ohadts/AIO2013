namespace AIO2013
{
    partial class chkdskForm
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.recoveryCheckBox = new System.Windows.Forms.CheckBox();
            this.fixErrCheckBox = new System.Windows.Forms.CheckBox();
            this.schedual_btn = new System.Windows.Forms.Button();
            this.cancel_btn = new System.Windows.Forms.Button();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.recoveryCheckBox);
            this.groupBox1.Controls.Add(this.fixErrCheckBox);
            this.groupBox1.Location = new System.Drawing.Point(12, 63);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(258, 67);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Check disk options";
            // 
            // recoveryCheckBox
            // 
            this.recoveryCheckBox.AutoSize = true;
            this.recoveryCheckBox.Location = new System.Drawing.Point(11, 40);
            this.recoveryCheckBox.Name = "recoveryCheckBox";
            this.recoveryCheckBox.Size = new System.Drawing.Size(239, 17);
            this.recoveryCheckBox.TabIndex = 1;
            this.recoveryCheckBox.Text = "Scan for and attempt recovery of bad sectors";
            this.recoveryCheckBox.UseVisualStyleBackColor = true;
            this.recoveryCheckBox.CheckedChanged += new System.EventHandler(this.fixErrCheckBox_CheckedChanged);
            // 
            // fixErrCheckBox
            // 
            this.fixErrCheckBox.AutoSize = true;
            this.fixErrCheckBox.Checked = true;
            this.fixErrCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.fixErrCheckBox.Location = new System.Drawing.Point(11, 23);
            this.fixErrCheckBox.Name = "fixErrCheckBox";
            this.fixErrCheckBox.Size = new System.Drawing.Size(181, 17);
            this.fixErrCheckBox.TabIndex = 0;
            this.fixErrCheckBox.Text = "Automatically fix file system errors";
            this.fixErrCheckBox.UseVisualStyleBackColor = true;
            this.fixErrCheckBox.CheckedChanged += new System.EventHandler(this.fixErrCheckBox_CheckedChanged);
            // 
            // schedual_btn
            // 
            this.schedual_btn.Location = new System.Drawing.Point(123, 147);
            this.schedual_btn.Name = "schedual_btn";
            this.schedual_btn.Size = new System.Drawing.Size(75, 38);
            this.schedual_btn.TabIndex = 1;
            this.schedual_btn.Text = "Schedule on next boot";
            this.schedual_btn.UseVisualStyleBackColor = true;
            this.schedual_btn.Click += new System.EventHandler(this.schedual_btn_Click);
            // 
            // cancel_btn
            // 
            this.cancel_btn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancel_btn.Location = new System.Drawing.Point(204, 147);
            this.cancel_btn.Name = "cancel_btn";
            this.cancel_btn.Size = new System.Drawing.Size(75, 38);
            this.cancel_btn.TabIndex = 2;
            this.cancel_btn.Text = "Cancel";
            this.cancel_btn.UseVisualStyleBackColor = true;
            this.cancel_btn.Click += new System.EventHandler(this.cancel_btn_Click);
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(71, 32);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(142, 21);
            this.comboBox1.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(138, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Please choose a drive from ";
            // 
            // chkdskForm
            // 
            this.AcceptButton = this.schedual_btn;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.CancelButton = this.cancel_btn;
            this.ClientSize = new System.Drawing.Size(283, 193);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.cancel_btn);
            this.Controls.Add(this.schedual_btn);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "chkdskForm";
            this.ShowIcon = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Check Disk Local Disk";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button schedual_btn;
        private System.Windows.Forms.Button cancel_btn;
        private System.Windows.Forms.CheckBox recoveryCheckBox;
        private System.Windows.Forms.CheckBox fixErrCheckBox;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label1;
    }
}