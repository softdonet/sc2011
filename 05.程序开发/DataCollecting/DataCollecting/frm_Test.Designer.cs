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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_Test));
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.上行命令ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.生成测试命令ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.生成实时数据命令ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.生成设备请求配置命令ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.用户事件命令ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.固件更新命令ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.注册命令ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.主动关闭告知命令ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.批量实时数据ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.清除屏幕ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip2 = new System.Windows.Forms.MenuStrip();
            this.下行命令ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.实时数据回复命令ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1 = new System.Windows.Forms.Panel();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.chkPrintCmd = new System.Windows.Forms.CheckBox();
            this.chkUpdateToDB = new System.Windows.Forms.CheckBox();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.系统设置ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.退出ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.menuStrip2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
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
            this.注册命令ToolStripMenuItem,
            this.主动关闭告知命令ToolStripMenuItem,
            this.批量实时数据ToolStripMenuItem,
            this.清除屏幕ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1019, 24);
            this.menuStrip1.TabIndex = 4;
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
            // 主动关闭告知命令ToolStripMenuItem
            // 
            this.主动关闭告知命令ToolStripMenuItem.Name = "主动关闭告知命令ToolStripMenuItem";
            this.主动关闭告知命令ToolStripMenuItem.Size = new System.Drawing.Size(113, 20);
            this.主动关闭告知命令ToolStripMenuItem.Text = "主动关闭告知命令";
            this.主动关闭告知命令ToolStripMenuItem.Click += new System.EventHandler(this.主动关闭告知命令ToolStripMenuItem_Click);
            // 
            // 批量实时数据ToolStripMenuItem
            // 
            this.批量实时数据ToolStripMenuItem.Name = "批量实时数据ToolStripMenuItem";
            this.批量实时数据ToolStripMenuItem.Size = new System.Drawing.Size(89, 20);
            this.批量实时数据ToolStripMenuItem.Text = "批量实时数据";
            this.批量实时数据ToolStripMenuItem.Click += new System.EventHandler(this.批量实时数据ToolStripMenuItem_Click);
            // 
            // 清除屏幕ToolStripMenuItem
            // 
            this.清除屏幕ToolStripMenuItem.Name = "清除屏幕ToolStripMenuItem";
            this.清除屏幕ToolStripMenuItem.Size = new System.Drawing.Size(41, 20);
            this.清除屏幕ToolStripMenuItem.Text = "清屏";
            this.清除屏幕ToolStripMenuItem.Click += new System.EventHandler(this.清除屏幕ToolStripMenuItem_Click);
            // 
            // menuStrip2
            // 
            this.menuStrip2.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.menuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.下行命令ToolStripMenuItem,
            this.实时数据回复命令ToolStripMenuItem1});
            this.menuStrip2.Location = new System.Drawing.Point(0, 24);
            this.menuStrip2.Name = "menuStrip2";
            this.menuStrip2.Size = new System.Drawing.Size(1019, 24);
            this.menuStrip2.TabIndex = 8;
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
            // panel1
            // 
            this.panel1.Controls.Add(this.textBox1);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 48);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1019, 420);
            this.panel1.TabIndex = 9;
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.Color.Black;
            this.textBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox1.ForeColor = System.Drawing.Color.Lime;
            this.textBox1.Location = new System.Drawing.Point(0, 35);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox1.Size = new System.Drawing.Size(1019, 385);
            this.textBox1.TabIndex = 1;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.chkPrintCmd);
            this.panel2.Controls.Add(this.chkUpdateToDB);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1019, 35);
            this.panel2.TabIndex = 2;
            // 
            // chkPrintCmd
            // 
            this.chkPrintCmd.AutoSize = true;
            this.chkPrintCmd.Checked = true;
            this.chkPrintCmd.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkPrintCmd.Location = new System.Drawing.Point(115, 8);
            this.chkPrintCmd.Name = "chkPrintCmd";
            this.chkPrintCmd.Size = new System.Drawing.Size(96, 16);
            this.chkPrintCmd.TabIndex = 1;
            this.chkPrintCmd.Text = "实时打印命令";
            this.chkPrintCmd.UseVisualStyleBackColor = true;
            this.chkPrintCmd.CheckedChanged += new System.EventHandler(this.chkPrintCmd_CheckedChanged);
            // 
            // chkUpdateToDB
            // 
            this.chkUpdateToDB.AutoSize = true;
            this.chkUpdateToDB.Location = new System.Drawing.Point(12, 8);
            this.chkUpdateToDB.Name = "chkUpdateToDB";
            this.chkUpdateToDB.Size = new System.Drawing.Size(96, 16);
            this.chkUpdateToDB.TabIndex = 0;
            this.chkUpdateToDB.Text = "更新到数据库";
            this.chkUpdateToDB.UseVisualStyleBackColor = true;
            this.chkUpdateToDB.CheckedChanged += new System.EventHandler(this.chkUpdateToDB_CheckedChanged);
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.ContextMenuStrip = this.contextMenuStrip1;
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "温度采集系统正在运行";
            this.notifyIcon1.Visible = true;
            this.notifyIcon1.Click += new System.EventHandler(this.notifyIcon1_Click);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.系统设置ToolStripMenuItem,
            this.退出ToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(119, 48);
            // 
            // 系统设置ToolStripMenuItem
            // 
            this.系统设置ToolStripMenuItem.Name = "系统设置ToolStripMenuItem";
            this.系统设置ToolStripMenuItem.Size = new System.Drawing.Size(118, 22);
            this.系统设置ToolStripMenuItem.Text = "系统设置";
            // 
            // 退出ToolStripMenuItem
            // 
            this.退出ToolStripMenuItem.Name = "退出ToolStripMenuItem";
            this.退出ToolStripMenuItem.Size = new System.Drawing.Size(118, 22);
            this.退出ToolStripMenuItem.Text = "退出";
            // 
            // frm_Test
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1019, 468);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.menuStrip2);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "frm_Test";
            this.ShowInTaskbar = false;
            this.Text = "命令测试";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frm_Test_FormClosing);
            this.Load += new System.EventHandler(this.frm_Test_Load);
            this.SizeChanged += new System.EventHandler(this.frm_Test_SizeChanged);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.menuStrip2.ResumeLayout(false);
            this.menuStrip2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 生成测试命令ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 生成实时数据命令ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 生成设备请求配置命令ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 用户事件命令ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 固件更新命令ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 注册命令ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 主动关闭告知命令ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 清除屏幕ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 上行命令ToolStripMenuItem;
        private System.Windows.Forms.MenuStrip menuStrip2;
        private System.Windows.Forms.ToolStripMenuItem 下行命令ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 实时数据回复命令ToolStripMenuItem1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.CheckBox chkUpdateToDB;
        private System.Windows.Forms.CheckBox chkPrintCmd;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 系统设置ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 退出ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 批量实时数据ToolStripMenuItem;
    }
}

