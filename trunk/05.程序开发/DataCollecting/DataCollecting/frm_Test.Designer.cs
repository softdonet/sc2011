namespace DataCollecting
{
    partial class frm_Test
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
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.生成测试命令ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.生成实时数据命令ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.Color.Black;
            this.textBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox1.ForeColor = System.Drawing.Color.Lime;
            this.textBox1.Location = new System.Drawing.Point(0, 24);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox1.Size = new System.Drawing.Size(699, 336);
            this.textBox1.TabIndex = 0;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.生成测试命令ToolStripMenuItem,
            this.生成实时数据命令ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(699, 24);
            this.menuStrip1.TabIndex = 4;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 生成测试命令ToolStripMenuItem
            // 
            this.生成测试命令ToolStripMenuItem.Name = "生成测试命令ToolStripMenuItem";
            this.生成测试命令ToolStripMenuItem.Size = new System.Drawing.Size(89, 20);
            this.生成测试命令ToolStripMenuItem.Text = "生成测试命令";
            this.生成测试命令ToolStripMenuItem.Click += new System.EventHandler(this.生成测试命令ToolStripMenuItem_Click);
            // 
            // 生成实时数据命令ToolStripMenuItem
            // 
            this.生成实时数据命令ToolStripMenuItem.Name = "生成实时数据命令ToolStripMenuItem";
            this.生成实时数据命令ToolStripMenuItem.Size = new System.Drawing.Size(113, 20);
            this.生成实时数据命令ToolStripMenuItem.Text = "生成实时数据命令";
            this.生成实时数据命令ToolStripMenuItem.Click += new System.EventHandler(this.生成实时数据命令ToolStripMenuItem_Click);
            // 
            // frm_Test
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(699, 360);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "frm_Test";
            this.Text = "命令测试";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 生成测试命令ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 生成实时数据命令ToolStripMenuItem;
    }
}

