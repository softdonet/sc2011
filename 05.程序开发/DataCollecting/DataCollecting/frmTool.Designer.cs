namespace DataCollecting
{
    partial class frmTool
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.menuStrip2 = new System.Windows.Forms.MenuStrip();
            this.下行命令ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.实时数据回复命令ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.上行命令ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.生成测试命令ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.生成实时数据命令ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.生成设备请求配置命令ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.用户事件命令ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.固件更新命令ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.注册命令ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1.SuspendLayout();
            this.menuStrip2.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.textBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 48);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(673, 225);
            this.panel1.TabIndex = 12;
            // 
            // textBox1
            // 
            this.textBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox1.Location = new System.Drawing.Point(0, 0);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(673, 225);
            this.textBox1.TabIndex = 0;
            // 
            // menuStrip2
            // 
            this.menuStrip2.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.menuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.下行命令ToolStripMenuItem,
            this.实时数据回复命令ToolStripMenuItem1});
            this.menuStrip2.Location = new System.Drawing.Point(0, 24);
            this.menuStrip2.Name = "menuStrip2";
            this.menuStrip2.Size = new System.Drawing.Size(673, 24);
            this.menuStrip2.TabIndex = 11;
            this.menuStrip2.Text = "menuStrip2";
            // 
            // 下行命令ToolStripMenuItem
            // 
            this.下行命令ToolStripMenuItem.ForeColor = System.Drawing.Color.Blue;
            this.下行命令ToolStripMenuItem.Name = "下行命令ToolStripMenuItem";
            this.下行命令ToolStripMenuItem.Size = new System.Drawing.Size(77, 20);
            this.下行命令ToolStripMenuItem.Text = "下行命令：";
            // 
            // 实时数据回复命令ToolStripMenuItem1
            // 
            this.实时数据回复命令ToolStripMenuItem1.Name = "实时数据回复命令ToolStripMenuItem1";
            this.实时数据回复命令ToolStripMenuItem1.Size = new System.Drawing.Size(113, 20);
            this.实时数据回复命令ToolStripMenuItem1.Text = "实时数据回复命令";
            this.实时数据回复命令ToolStripMenuItem1.Click += new System.EventHandler(this.实时数据回复命令ToolStripMenuItem1_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.Wheat;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.上行命令ToolStripMenuItem,
            this.生成测试命令ToolStripMenuItem,
            this.生成实时数据命令ToolStripMenuItem,
            this.生成设备请求配置命令ToolStripMenuItem,
            this.用户事件命令ToolStripMenuItem,
            this.固件更新命令ToolStripMenuItem,
            this.注册命令ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(673, 24);
            this.menuStrip1.TabIndex = 10;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 上行命令ToolStripMenuItem
            // 
            this.上行命令ToolStripMenuItem.Font = new System.Drawing.Font("SimSun", 9F);
            this.上行命令ToolStripMenuItem.ForeColor = System.Drawing.Color.Blue;
            this.上行命令ToolStripMenuItem.Name = "上行命令ToolStripMenuItem";
            this.上行命令ToolStripMenuItem.Size = new System.Drawing.Size(77, 20);
            this.上行命令ToolStripMenuItem.Text = "上行命令：";
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
            // 生成设备请求配置命令ToolStripMenuItem
            // 
            this.生成设备请求配置命令ToolStripMenuItem.Name = "生成设备请求配置命令ToolStripMenuItem";
            this.生成设备请求配置命令ToolStripMenuItem.Size = new System.Drawing.Size(137, 20);
            this.生成设备请求配置命令ToolStripMenuItem.Text = "生成设备请求配置命令";
            this.生成设备请求配置命令ToolStripMenuItem.Click += new System.EventHandler(this.生成设备请求配置命令ToolStripMenuItem_Click);
            // 
            // 用户事件命令ToolStripMenuItem
            // 
            this.用户事件命令ToolStripMenuItem.Name = "用户事件命令ToolStripMenuItem";
            this.用户事件命令ToolStripMenuItem.Size = new System.Drawing.Size(89, 20);
            this.用户事件命令ToolStripMenuItem.Text = "用户事件命令";
            this.用户事件命令ToolStripMenuItem.Click += new System.EventHandler(this.用户事件命令ToolStripMenuItem_Click);
            // 
            // 固件更新命令ToolStripMenuItem
            // 
            this.固件更新命令ToolStripMenuItem.Name = "固件更新命令ToolStripMenuItem";
            this.固件更新命令ToolStripMenuItem.Size = new System.Drawing.Size(89, 20);
            this.固件更新命令ToolStripMenuItem.Text = "固件更新命令";
            this.固件更新命令ToolStripMenuItem.Click += new System.EventHandler(this.固件更新命令ToolStripMenuItem_Click);
            // 
            // 注册命令ToolStripMenuItem
            // 
            this.注册命令ToolStripMenuItem.Name = "注册命令ToolStripMenuItem";
            this.注册命令ToolStripMenuItem.Size = new System.Drawing.Size(65, 20);
            this.注册命令ToolStripMenuItem.Text = "注册命令";
            this.注册命令ToolStripMenuItem.Click += new System.EventHandler(this.注册命令ToolStripMenuItem_Click);
            // 
            // frmTool
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(673, 273);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.menuStrip2);
            this.Controls.Add(this.menuStrip1);
            this.Name = "frmTool";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "客户端命令生成工具";
            this.TopMost = true;
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.menuStrip2.ResumeLayout(false);
            this.menuStrip2.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.MenuStrip menuStrip2;
        private System.Windows.Forms.ToolStripMenuItem 下行命令ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 实时数据回复命令ToolStripMenuItem1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 上行命令ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 生成测试命令ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 生成实时数据命令ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 生成设备请求配置命令ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 用户事件命令ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 固件更新命令ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 注册命令ToolStripMenuItem;
        private System.Windows.Forms.TextBox textBox1;
    }
}