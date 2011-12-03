namespace Lcd
{
    partial class frmWelcome
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
            this.lblRowOneText = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblRowTwoText = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblRowOneText
            // 
            this.lblRowOneText.AutoSize = true;
            this.lblRowOneText.BackColor = System.Drawing.Color.Transparent;
            this.lblRowOneText.Location = new System.Drawing.Point(185, 62);
            this.lblRowOneText.Name = "lblRowOneText";
            this.lblRowOneText.Size = new System.Drawing.Size(317, 40);
            this.lblRowOneText.TabIndex = 0;
            this.lblRowOneText.Text = "欢迎***公司领导";
            this.lblRowOneText.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel1
            // 
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel1.Controls.Add(this.lblRowTwoText);
            this.panel1.Controls.Add(this.lblRowOneText);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Font = new System.Drawing.Font("SimSun", 30F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.panel1.ForeColor = System.Drawing.Color.Red;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(709, 343);
            this.panel1.TabIndex = 1;
            // 
            // lblRowTwoText
            // 
            this.lblRowTwoText.AutoSize = true;
            this.lblRowTwoText.BackColor = System.Drawing.Color.Transparent;
            this.lblRowTwoText.Location = new System.Drawing.Point(76, 119);
            this.lblRowTwoText.Name = "lblRowTwoText";
            this.lblRowTwoText.Size = new System.Drawing.Size(537, 40);
            this.lblRowTwoText.TabIndex = 1;
            this.lblRowTwoText.Text = "莅临北京分公司检查指导工作";
            // 
            // frmWelcome
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(709, 343);
            this.Controls.Add(this.panel1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmWelcome";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmWelcome";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmWelcome_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblRowOneText;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblRowTwoText;

    }
}