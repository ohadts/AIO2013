namespace Teboscreen
{
    partial class ControlPanel
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ControlPanel));
            this.bttCaptureScreen = new System.Windows.Forms.Button();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.CloseButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // bttCaptureScreen
            // 
            this.bttCaptureScreen.BackColor = System.Drawing.Color.WhiteSmoke;
            this.bttCaptureScreen.ForeColor = System.Drawing.SystemColors.ControlText;
            this.bttCaptureScreen.Location = new System.Drawing.Point(39, 61);
            this.bttCaptureScreen.Name = "bttCaptureScreen";
            this.bttCaptureScreen.Size = new System.Drawing.Size(90, 32);
            this.bttCaptureScreen.TabIndex = 0;
            this.bttCaptureScreen.TabStop = false;
            this.bttCaptureScreen.Text = "I AGREE";
            this.bttCaptureScreen.UseVisualStyleBackColor = false;
            this.bttCaptureScreen.Click += new System.EventHandler(this.bttCaptureScreen_Click);
            // 
            // CloseButton
            // 
            this.CloseButton.BackColor = System.Drawing.Color.WhiteSmoke;
            this.CloseButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CloseButton.Location = new System.Drawing.Point(160, 61);
            this.CloseButton.Name = "CloseButton";
            this.CloseButton.Size = new System.Drawing.Size(90, 32);
            this.CloseButton.TabIndex = 1;
            this.CloseButton.Text = "Deny";
            this.CloseButton.UseVisualStyleBackColor = false;
            this.CloseButton.Click += new System.EventHandler(this.CloseButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(36, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(197, 26);
            this.label1.TabIndex = 2;
            this.label1.Text = "Your IT assistant is asking for permission\r\n to take a screenshot ";
            // 
            // ControlPanel
            // 
            this.AcceptButton = this.bttCaptureScreen;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LemonChiffon;
            this.CancelButton = this.CloseButton;
            this.ClientSize = new System.Drawing.Size(275, 105);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.CloseButton);
            this.Controls.Add(this.bttCaptureScreen);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ControlPanel";
            this.ShowIcon = false;
            this.Text = "Your authorization required";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ControlPanel_FormClosing);
            this.Load += new System.EventHandler(this.ControlPanel_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button bttCaptureScreen;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.Button CloseButton;
        private System.Windows.Forms.Label label1;
    }
}