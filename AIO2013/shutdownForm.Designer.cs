namespace AIO2013
{
    partial class shutdownForm
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
            this.shtdwnOkBtn = new System.Windows.Forms.Button();
            this.shtdwnCancleBtn = new System.Windows.Forms.Button();
            this.shtdwnRadio = new System.Windows.Forms.RadioButton();
            this.rstrtRadio = new System.Windows.Forms.RadioButton();
            this.lgffRadio = new System.Windows.Forms.RadioButton();
            this.SuspendLayout();
            // 
            // shtdwnOkBtn
            // 
            this.shtdwnOkBtn.Location = new System.Drawing.Point(38, 65);
            this.shtdwnOkBtn.Name = "shtdwnOkBtn";
            this.shtdwnOkBtn.Size = new System.Drawing.Size(75, 23);
            this.shtdwnOkBtn.TabIndex = 0;
            this.shtdwnOkBtn.Text = "OK";
            this.shtdwnOkBtn.UseVisualStyleBackColor = true;
            this.shtdwnOkBtn.Click += new System.EventHandler(this.shtdwnOkBtn_Click);
            // 
            // shtdwnCancleBtn
            // 
            this.shtdwnCancleBtn.Location = new System.Drawing.Point(159, 63);
            this.shtdwnCancleBtn.Name = "shtdwnCancleBtn";
            this.shtdwnCancleBtn.Size = new System.Drawing.Size(75, 23);
            this.shtdwnCancleBtn.TabIndex = 1;
            this.shtdwnCancleBtn.Text = "Cancle";
            this.shtdwnCancleBtn.UseVisualStyleBackColor = true;
            this.shtdwnCancleBtn.Click += new System.EventHandler(this.shtdwnCancleBtn_Click);
            // 
            // shtdwnRadio
            // 
            this.shtdwnRadio.AutoSize = true;
            this.shtdwnRadio.Checked = true;
            this.shtdwnRadio.Location = new System.Drawing.Point(20, 26);
            this.shtdwnRadio.Name = "shtdwnRadio";
            this.shtdwnRadio.Size = new System.Drawing.Size(73, 17);
            this.shtdwnRadio.TabIndex = 2;
            this.shtdwnRadio.TabStop = true;
            this.shtdwnRadio.Text = "Shutdown";
            this.shtdwnRadio.UseVisualStyleBackColor = true;
            // 
            // rstrtRadio
            // 
            this.rstrtRadio.AutoSize = true;
            this.rstrtRadio.Location = new System.Drawing.Point(109, 26);
            this.rstrtRadio.Name = "rstrtRadio";
            this.rstrtRadio.Size = new System.Drawing.Size(59, 17);
            this.rstrtRadio.TabIndex = 3;
            this.rstrtRadio.Text = "Restart";
            this.rstrtRadio.UseVisualStyleBackColor = true;
            // 
            // lgffRadio
            // 
            this.lgffRadio.AutoSize = true;
            this.lgffRadio.Location = new System.Drawing.Point(184, 26);
            this.lgffRadio.Name = "lgffRadio";
            this.lgffRadio.Size = new System.Drawing.Size(60, 17);
            this.lgffRadio.TabIndex = 4;
            this.lgffRadio.Text = "Log Off";
            this.lgffRadio.UseVisualStyleBackColor = true;
            // 
            // shutdownForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(272, 100);
            this.Controls.Add(this.lgffRadio);
            this.Controls.Add(this.rstrtRadio);
            this.Controls.Add(this.shtdwnRadio);
            this.Controls.Add(this.shtdwnCancleBtn);
            this.Controls.Add(this.shtdwnOkBtn);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "shutdownForm";
            this.RightToLeftLayout = true;
            this.ShowIcon = false;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button shtdwnOkBtn;
        private System.Windows.Forms.Button shtdwnCancleBtn;
        private System.Windows.Forms.RadioButton shtdwnRadio;
        private System.Windows.Forms.RadioButton rstrtRadio;
        private System.Windows.Forms.RadioButton lgffRadio;
    }
}