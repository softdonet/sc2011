namespace DataCollecting
{
    partial class frmSettings
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
            this.chkWriteToDB = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lbtnOK = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // chkWriteToDB
            // 
            this.chkWriteToDB.AutoSize = true;
            this.chkWriteToDB.Location = new System.Drawing.Point(20, 20);
            this.chkWriteToDB.Name = "chkWriteToDB";
            this.chkWriteToDB.Size = new System.Drawing.Size(72, 16);
            this.chkWriteToDB.TabIndex = 0;
            this.chkWriteToDB.Text = "实时入库";
            this.chkWriteToDB.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chkWriteToDB);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(326, 52);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            // 
            // lbtnOK
            // 
            this.lbtnOK.Location = new System.Drawing.Point(247, 80);
            this.lbtnOK.Name = "lbtnOK";
            this.lbtnOK.Size = new System.Drawing.Size(75, 23);
            this.lbtnOK.TabIndex = 2;
            this.lbtnOK.Text = "确定";
            this.lbtnOK.UseVisualStyleBackColor = true;
            this.lbtnOK.Click += new System.EventHandler(this.lbtnOK_Click);
            // 
            // frmSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(350, 115);
            this.Controls.Add(this.lbtnOK);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmSettings";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "系统配置";
            this.TopMost = true;
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button lbtnOK;
        public System.Windows.Forms.CheckBox chkWriteToDB;
    }
}