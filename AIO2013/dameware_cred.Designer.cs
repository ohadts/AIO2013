namespace AIO2013
{
    partial class dameware_cred
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.saveBtn = new System.Windows.Forms.Button();
            this.displayNameTxtbox = new System.Windows.Forms.TextBox();
            this.usernameTxtbox = new System.Windows.Forms.TextBox();
            this.passwordTxtbox = new System.Windows.Forms.TextBox();
            this.domainTxtbox = new System.Windows.Forms.TextBox();
            this.localChkbox = new System.Windows.Forms.CheckBox();
            this.validateLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 41);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "User name:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(22, 66);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Password:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(21, 16);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(73, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Display name:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(23, 116);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(46, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Domain:";
            // 
            // saveBtn
            // 
            this.saveBtn.Location = new System.Drawing.Point(84, 155);
            this.saveBtn.Name = "saveBtn";
            this.saveBtn.Size = new System.Drawing.Size(75, 23);
            this.saveBtn.TabIndex = 4;
            this.saveBtn.Text = "Save";
            this.saveBtn.UseVisualStyleBackColor = true;
            this.saveBtn.Click += new System.EventHandler(this.button1_Click);
            // 
            // displayNameTxtbox
            // 
            this.displayNameTxtbox.Location = new System.Drawing.Point(101, 13);
            this.displayNameTxtbox.Name = "displayNameTxtbox";
            this.displayNameTxtbox.Size = new System.Drawing.Size(115, 20);
            this.displayNameTxtbox.TabIndex = 5;
            // 
            // usernameTxtbox
            // 
            this.usernameTxtbox.ForeColor = System.Drawing.SystemColors.GrayText;
            this.usernameTxtbox.Location = new System.Drawing.Point(101, 37);
            this.usernameTxtbox.Name = "usernameTxtbox";
            this.usernameTxtbox.Size = new System.Drawing.Size(115, 20);
            this.usernameTxtbox.TabIndex = 6;
            this.usernameTxtbox.Text = "Username";
            // 
            // passwordTxtbox
            // 
            this.passwordTxtbox.ForeColor = System.Drawing.SystemColors.GrayText;
            this.passwordTxtbox.Location = new System.Drawing.Point(101, 61);
            this.passwordTxtbox.Name = "passwordTxtbox";
            this.passwordTxtbox.Size = new System.Drawing.Size(115, 20);
            this.passwordTxtbox.TabIndex = 7;
            this.passwordTxtbox.Text = "Password";
            // 
            // domainTxtbox
            // 
            this.domainTxtbox.ForeColor = System.Drawing.SystemColors.GrayText;
            this.domainTxtbox.Location = new System.Drawing.Point(101, 113);
            this.domainTxtbox.Name = "domainTxtbox";
            this.domainTxtbox.Size = new System.Drawing.Size(115, 20);
            this.domainTxtbox.TabIndex = 8;
            this.domainTxtbox.Text = "Domain";
            // 
            // localChkbox
            // 
            this.localChkbox.AutoSize = true;
            this.localChkbox.Location = new System.Drawing.Point(25, 94);
            this.localChkbox.Name = "localChkbox";
            this.localChkbox.Size = new System.Drawing.Size(95, 17);
            this.localChkbox.TabIndex = 9;
            this.localChkbox.Text = "Local machine";
            this.localChkbox.UseVisualStyleBackColor = true;
            this.localChkbox.CheckStateChanged += new System.EventHandler(this.localChkbox_CheckStateChanged);
            // 
            // validateLabel
            // 
            this.validateLabel.AutoSize = true;
            this.validateLabel.ForeColor = System.Drawing.Color.Red;
            this.validateLabel.Location = new System.Drawing.Point(62, 138);
            this.validateLabel.Name = "validateLabel";
            this.validateLabel.Size = new System.Drawing.Size(133, 13);
            this.validateLabel.TabIndex = 10;
            this.validateLabel.Text = "You must fill all of the fields";
            this.validateLabel.Visible = false;
            // 
            // dameware_cred
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(248, 181);
            this.Controls.Add(this.validateLabel);
            this.Controls.Add(this.localChkbox);
            this.Controls.Add(this.domainTxtbox);
            this.Controls.Add(this.passwordTxtbox);
            this.Controls.Add(this.usernameTxtbox);
            this.Controls.Add(this.displayNameTxtbox);
            this.Controls.Add(this.saveBtn);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Name = "dameware_cred";
            this.Text = "Add new credentials";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button saveBtn;
        private System.Windows.Forms.TextBox displayNameTxtbox;
        private System.Windows.Forms.TextBox usernameTxtbox;
        private System.Windows.Forms.TextBox passwordTxtbox;
        private System.Windows.Forms.TextBox domainTxtbox;
        private System.Windows.Forms.CheckBox localChkbox;
        private System.Windows.Forms.Label validateLabel;
    }
}