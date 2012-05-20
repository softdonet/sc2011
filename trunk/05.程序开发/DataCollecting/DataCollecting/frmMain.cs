using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Net;
using System.Collections;
using DataCollecting.NetServer;
using NetData;
using Utility;

namespace DataCollecting
{


    public partial class frm_Test : Form
    {
        //tcp服务
        TcpNetServer tcpserver = new TcpNetServer();
        //数据解析
        ConmmandAnalysis commandAnalysis = null;
        public frm_Test()
        {
            InitializeComponent();
            RefreshState();
            LogItem litem = new LogItem()
            {
                Event = "启动SCADA入库程序",
                Time = DateTime.Now
            };
            LogChanged(litem);
            tcpserver.SystenErrorEvent += new TcpNetServer.SystenErrorHandle(tcpserver_SystenErrorEvent);
            commandAnalysis = new ConmmandAnalysis(tcpserver);
            commandAnalysis.OnReceiveMsg += new ConmmandAnalysis.OnReceiveHandle(commandAnalysis_OnReceiveMsg);
        }

        /// <summary>
        /// 记录日志
        /// </summary>
        /// <param name="str"></param>
        void commandAnalysis_OnReceiveMsg(string str)
        {
            LogHelper.WriteInformationLog(str + Environment.NewLine);
        }

        /// <summary>
        /// 通讯事件显示
        /// </summary>
        /// <param name="ex"></param>
        void tcpserver_SystenErrorEvent(Exception ex)
        {
            LogItem litem = new LogItem()
            {
                Event = "通讯异常",
                Memo = ex.Message,
                Time = DateTime.Now
            };
            RefreshListView(litem);
        }

        /// <summary>
        /// 异步数据显示在界面
        /// </summary>
        /// <param name="str"></param>
        delegate void RefreshListViewHandle(LogItem litem);
        private void RefreshListView(LogItem litem)
        {
            if (listView1.InvokeRequired)
            {
                listView1.Invoke(new RefreshListViewHandle(RefreshListView), litem);
            }
            else
            {
                LogChanged(litem);
            }
        }

        /// <summary>
        /// 显示日志信息
        /// </summary>
        /// <param name="lTest"></param>
        private void LogChanged(LogItem lTest)
        {
            ListViewItem item = new ListViewItem();
            item.Text = lTest.Time.ToString("yyyy-MM-dd HH:mm:ss");
            item.SubItems.Add(lTest.Event);
            item.SubItems.Add(lTest.Memo);
            this.listView1.Items.Add(item);
        }

        #region 窗体通知相关

        private void frm_Test_SizeChanged(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                HidMainForm();
            }
        }

        /// <summary>
        /// 隐藏窗体
        /// </summary>
        void HidMainForm()
        {
            this.Hide();
        }

        /// <summary>
        /// 显示窗体
        /// </summary>
        void ShowMainForm()
        {
            this.Show();
            this.WindowState = FormWindowState.Normal;
        }

        private void frm_Test_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult res = MessageBox.Show("您确定退出系统吗？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (res == DialogResult.Yes)
            {
                this.Hide();
                this.notifyIcon1.Visible = false;
                this.Dispose();
                Application.Exit();
            }
            else
            {
                e.Cancel = true;
            }
        }

        private void notifyIcon1_Click(object sender, EventArgs e)
        {
            MouseEventArgs Mouse_e = (MouseEventArgs)e;
            if (Mouse_e.Button == MouseButtons.Left)
            {
                ShowMainForm();
            }
        }

        #endregion

        private void 设置ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmSettings frm = new frmSettings();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                Comm.UpdateToDB = frm.chkWriteToDB.Checked;
                RefreshState();
            }
        }
        /// <summary>
        /// 刷新状态栏
        /// </summary>
        void RefreshState()
        {
            if (Comm.UpdateToDB)
            {
                Image RunImage = Image.FromFile(System.Windows.Forms.Application.StartupPath + "\\RUN.ico");
                this.toolStripStatusDBMode.Image = RunImage;
                this.toolStripStatusDBMode.Text = "实时入库：已开启";
            }
            else
            {
                this.toolStripStatusDBMode.Text = "实时入库：已关闭";
                Image StopImage = Image.FromFile(System.Windows.Forms.Application.StartupPath + "\\STOP.ico");
                this.toolStripStatusDBMode.Image = StopImage;
            }
        }
        /// <summary>
        /// 监控窗口
        /// </summary>
        private frmMonitoring frmMonitor = null;
        private void 视图ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (frmMonitor == null || frmMonitor.IsDisposed)
            {
                frmMonitor = new frmMonitoring(commandAnalysis);
            }
            frmMonitor.Show();
        }

        /// <summary>
        /// 打开日志
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 日志ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.Multiselect = false;
            openFileDialog1.Filter = "日志文件|*.txt";
            openFileDialog1.Title = "选择日志文件";
            openFileDialog1.InitialDirectory = System.Windows.Forms.Application.StartupPath + "\\logs";
            openFileDialog1.FileName = DateTime.Now.ToString("yyyy-MM-dd") + ".txt";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                System.Diagnostics.Process.Start(openFileDialog1.FileName);
            }
        }

        /// <summary>
        /// 客户端模拟工具
        /// </summary>
        private frmTool frm = null;
        private void 工具ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (frm == null || frm.IsDisposed)
            {
                frm = new frmTool();
            }
            frm.Show();
        }

        private void 系统设置ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmSettings frm = new frmSettings();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                Comm.UpdateToDB = frm.chkWriteToDB.Checked;
                RefreshState();
            }
        }

        private void 退出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

