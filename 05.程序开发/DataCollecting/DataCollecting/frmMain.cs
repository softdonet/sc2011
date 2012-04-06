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
        TcpNetServer tcpserver = new TcpNetServer();
        public frm_Test()
        {
            InitializeComponent();
            LogItem litem = new LogItem()
            {
                Event = "启动SCADA入库程序",
                Time = DateTime.Now
            };
            LogChanged(litem);
            tcpserver.SystenErrorEvent += new TcpNetServer.SystenErrorHandle(tcpserver_SystenErrorEvent);
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
        /// <summary>
        /// 异步数据显示在界面
        /// </summary>
        /// <param name="str"></param>
        delegate void SetTextHandle(string str, bool isClear);
        private void SetText(string str, bool isClear)
        {
            if (Comm.PrintCmd)
            {
                if (this.InvokeRequired)
                {
                    this.Invoke(new SetTextHandle(SetText), str, isClear);
                }
                else
                {
                    if (isClear)
                    {
                        //this.textBox1.Text = str + Environment.NewLine;
                    }
                    else
                    {
                        //this.textBox1.Text += str + Environment.NewLine;
                        LogHelper.WriteInformationLog(str + Environment.NewLine);
                    }
                    //textBox1.SelectionStart = textBox1.TextLength;
                    //textBox1.ScrollToCaret();
                }
            }
        }
        #region 生成测试命令

        private void 生成测试命令ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Head h = new Head();
            h.CmdHeader = Const.UP_HEADER;
            h.CmdCommand = Command.cmd_Test_R;
            h.DataContext = 42605;
            h.DeviceSN = "0A5F01CD0001";
            h.State = 0;
            h.SateTimeMark = DateTime.Now;

            Test_R tr = new Test_R();
            tr.Header = h;
            tr.Content = 1234;

            SetText(StringHelper.DataToStr(tr.ToByte()) + Environment.NewLine, false);
        }

        private void 生成实时数据命令ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Head h = new Head();
            h.CmdHeader = Const.UP_HEADER;
            h.CmdCommand = Command.cmd_RealTimeDate_R;
            h.DataContext = 42605;
            h.DeviceSN = "0A5F01CD0001";
            h.State = 0;
            h.SateTimeMark = DateTime.Now;
            RealTimeData_R rr = new RealTimeData_R();
            rr.Header = h;

            RealTimeDataBlock block = new RealTimeDataBlock();
            block.BlockNo = 1;
            block.SateTimeMark = DateTime.Now;
            block.Temperature1 = RandomHelper.GetRandom(15, 60) + 0.5M;
            block.Temperature2 = RandomHelper.GetRandom(15, 60) + 0.5M;
            block.Humidity = 0.2M;
            block.Electric = (ushort)RandomHelper.GetRandom(0, 400);
            block.Signal = (ushort)RandomHelper.GetRandom(0, 400);

            RealTimeDataBlock block1 = new RealTimeDataBlock();
            block1.BlockNo = 1;
            block1.SateTimeMark = DateTime.Now;
            block1.Temperature1 = RandomHelper.GetRandom(15, 60) + 0.5M;
            block1.Temperature2 = RandomHelper.GetRandom(15, 60) + 0.5M;
            block1.Humidity = 0.2M;
            block1.Electric = (ushort)RandomHelper.GetRandom(0, 400);
            block1.Signal = (ushort)RandomHelper.GetRandom(0, 400);

            rr.RealTimeDataBlocks.Add(block);
            rr.RealTimeDataBlocks.Add(block1);

            SetText(StringHelper.DataToStr(rr.ToByte()) + Environment.NewLine, false);
        }

        private void 生成设备请求配置命令ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Head h = new Head();
            h.CmdHeader = Const.UP_HEADER;
            h.CmdCommand = Command.cmd_Config_R;
            h.DataContext = 42605;
            h.DeviceSN = "0A5F01CD0001";
            h.State = 0;
            h.SateTimeMark = DateTime.Now;

            Config_R cr = new Config_R();
            cr.Header = h;
            cr.MAC = "00-10-30-AF-E1-30-40";
            cr.SIM = "10303239876321900102";
            cr.DeviveType = "ICG-P1000-E";
            cr.HardwareVersionMain = 12;
            cr.HardwareVersionChild = 25;
            cr.SoftwareVersionMain = 13;
            cr.SoftwareVersionChild = 26;
            cr.WorkstateMain = 14;
            cr.WorkstateChild = 17;
            SetText(StringHelper.DataToStr(cr.ToByte()) + Environment.NewLine, false);
        }

        private void 用户事件命令ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Head h = new Head();
            h.CmdHeader = Const.UP_HEADER;
            h.CmdCommand = Command.cmd_UserEvent_R;
            h.DataContext = 42605;
            h.DeviceSN = "000000000000";
            h.State = 0;
            h.SateTimeMark = DateTime.Now;

            UserEvent_R cr = new UserEvent_R();
            cr.Header = h;
            cr.MAC = "00-10-30-AF-E1-30-40";
            cr.SIM = "10303239876321900102";
            cr.DeviveType = "ICG-P1000-ED";
            cr.HardwareVersionMain = 12;
            cr.HardwareVersionChild = 25;
            cr.SoftwareVersionMain = 13;
            cr.SoftwareVersionChild = 26;
            cr.WorkstateMain = 14;
            cr.WorkstateChild = 17;
            SetText(StringHelper.DataToStr(cr.ToByte()) + Environment.NewLine, false);
        }

        private void 固件更新命令ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Head h = new Head();
            h.CmdHeader = Const.UP_HEADER;
            h.CmdCommand = Command.cmd_FirmwareRequest_R;
            h.DataContext = 42605;
            h.DeviceSN = "0A5F01CD0001";
            h.State = 0;
            h.SateTimeMark = DateTime.Now;

            FirmwareRequest_R cr = new FirmwareRequest_R();
            cr.Header = h;
            cr.MAC = "00-10-30-AF-E1-30-40";
            cr.SIM = "10303239876321900102";
            cr.DeviveType = "ICG-P1000-ED";
            cr.HardwareVersionMain = 12;
            cr.HardwareVersionChild = 25;
            cr.SoftwareVersionMain = 13;
            cr.SoftwareVersionChild = 26;
            cr.WorkstateMain = 14;
            cr.WorkstateChild = 17;
            SetText(StringHelper.DataToStr(cr.ToByte()) + Environment.NewLine, false);
        }

        private void 注册命令ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Head h = new Head();
            h.CmdHeader = Const.UP_HEADER;
            h.CmdCommand = Command.cmd_Register_R;
            h.DataContext = 42605;
            h.DeviceSN = "0A5F01CD0001";
            h.State = 0;
            h.SateTimeMark = DateTime.Now;

            Register_R cr = new Register_R();
            cr.Header = h;
            cr.MAC = "00-10-30-AF-E1-30-40";
            cr.SIM = "10303239876321900102";
            cr.DeviveType = "ICG-P1000-E";
            cr.HardwareVersionMain = 12;
            cr.HardwareVersionChild = 25;
            cr.SoftwareVersionMain = 13;
            cr.SoftwareVersionChild = 26;
            cr.WorkstateMain = 14;
            cr.WorkstateChild = 17;
            SetText(StringHelper.DataToStr(cr.ToByte()) + Environment.NewLine, false);
        }

        private void 主动关闭告知命令ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        #endregion

        private void 清除屏幕ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //this.textBox1.Text = string.Empty;
        }

        private void 实时数据回复命令ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Head h = new Head();
            h.CmdHeader = Const.UP_HEADER;
            h.CmdCommand = Command.cmd_Reply;
            h.DataContext = 42605;
            h.DeviceSN = "0A5F01CD0001";
            h.State = 0;
            h.SateTimeMark = DateTime.Now;

            RealTimeData_S rs = new RealTimeData_S();
            rs.Header = h;
            rs.HaveBroadcastInfo = true;
            rs.HaveConfigInfo = true;
            rs.HaveWeatherInfo = true;


            //构造设备配置信息块
            ConfigDataBlock configDataBlock = new ConfigDataBlock();
            configDataBlock.RunMode = 1;
            configDataBlock.Argument1 = 1500;
            configDataBlock.Argument2 = 1600;
            configDataBlock.Argument3 = 1700;
            configDataBlock.DeviceNo = "P-100100";
            configDataBlock.InstalPlace = "北京市朝阳区双井街道";
            configDataBlock.DisplayMode = 1;
            configDataBlock.InstancyBtnEnable = true;
            configDataBlock.InfoBtnEnable = true;
            configDataBlock.RepairTel = "13801112222";
            configDataBlock.MainIP = "202.106.42.1";
            configDataBlock.ReserveIP = "202.106.42.2";

            configDataBlock.DomainName = "xyz.dddd.com";
            configDataBlock.Port = 1789;
            configDataBlock.ConnectionType = 0;
            configDataBlock.ConnectName = "CMNET";
            rs.ConfigData = configDataBlock;

            WeatherDataBlock weatherDataBlock = new WeatherDataBlock();
            weatherDataBlock.TodayWeather = "晴转XXX多云，有阵雨。";
            rs.WeatherData = weatherDataBlock;

            BroadcastDataBlock broadcastDataBlock = new BroadcastDataBlock();
            broadcastDataBlock.Msg = "通知：明天下午开会。不要迟到，迟到一分钟扣100元。";

            rs.BroadcastData = broadcastDataBlock;
            SetText(StringHelper.DataToStr(rs.ToByte()) + Environment.NewLine, false);
        }

        private void frm_Test_Load(object sender, EventArgs e)
        {

        }

        //private void chkUpdateToDB_CheckedChanged(object sender, EventArgs e)
        //{
        //    if (this.chkUpdateToDB.Checked)
        //    {
        //        Comm.UpdateToDB = true;
        //    }
        //    else
        //    {
        //        Comm.UpdateToDB = false;
        //    }
        //}

        //private void chkPrintCmd_CheckedChanged(object sender, EventArgs e)
        //{
        //    if (this.chkPrintCmd.Checked)
        //    {
        //        Comm.PrintCmd = true;
        //    }
        //    else
        //    {
        //        Comm.PrintCmd = false;
        //    }
        //}


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

        frmMonitoring frmMonitor = null;

        private void 监控窗口ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var obj = sender as ToolStripMenuItem;
            if (obj.Checked)
            {
                frmMonitor.Hide();
                frmMonitor.RemoveEvent();
                obj.Checked = false;
            }
            else
            {
                if (frmMonitor == null || frmMonitor.IsDisposed)
                {
                    frmMonitor = new frmMonitoring(tcpserver);
                    frmMonitor.FormClosing += (d, o) =>
                    {
                        frmMonitor.RemoveEvent();
                        frmMonitor.Hide();
                        o.Cancel = true;
                        obj.Checked = false;
                    };
                }
                frmMonitor.RegistEvent();
                frmMonitor.Show();
                obj.Checked = true;
            }
        }

        private void 退出ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void 退出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion

        private void 实时入库ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var obj = sender as ToolStripMenuItem;
            if (obj.Checked)
            {
                obj.Checked = false;
                Comm.UpdateToDB = false;
                this.toolStripStatusDBMode.Text = "实时入库模式：已关闭";

                Image StopImage = Image.FromFile(System.Windows.Forms.Application.StartupPath + "//STOP.ico");
                this.toolStripStatusDBMode.Image = StopImage;
            }
            else
            {
                obj.Checked = true;
                Comm.UpdateToDB = true;
                Image RunImage = Image.FromFile(System.Windows.Forms.Application.StartupPath + "//RUN.ico");
                this.toolStripStatusDBMode.Image = RunImage;
                this.toolStripStatusDBMode.Text = "实时入库模式：已开启";
            }
        }

        private void 查看日志ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.Multiselect = false;
            openFileDialog1.Filter = "日志文件|*.txt";
            openFileDialog1.Title = "选择日志文件";
            openFileDialog1.InitialDirectory = System.Windows.Forms.Application.StartupPath + "\\logs";
            //openFileDialog1.RestoreDirectory = true;
            openFileDialog1.FileName = DateTime.Now.ToString("yyyy-MM-dd") + ".txt";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                System.Diagnostics.Process.Start(openFileDialog1.FileName);
            }
        }
    }
}
