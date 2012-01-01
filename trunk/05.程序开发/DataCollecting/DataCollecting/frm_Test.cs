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
using DataCollecting.NetData;
using DataCollecting.Helper;
using DataCollecting.Common;

namespace DataCollecting
{
    public partial class frm_Test : Form
    {
        public frm_Test()
        {
            InitializeComponent();



            TcpNetServer tcpserver = new TcpNetServer();
            tcpserver.TestEvent += new TcpNetServer.TestHandle(tcpserver_TestEvent);
            tcpserver.ConfigEvent += new TcpNetServer.ConfigHandle(tcpserver_ConfigEvent);
            tcpserver.RealTimeDataEvent += new TcpNetServer.RealTimeDataHandle(tcpserver_RealTimeDataEvent);
            tcpserver.UserEventEvent += new TcpNetServer.UserEventHandle(tcpserver_UserEventEvent);
        }

        void tcpserver_UserEventEvent(UserEvent_R userEvent_R)
        {
            SetText(DateTime.Now.ToString() + " 收到用户事件信息：", false);
            SetText("--------------------------------------", false);
            ShowHeader(userEvent_R.Header);
            SetText("报体：", false);
            SetText("--------------------------------------", false);
        }

        void tcpserver_RealTimeDataEvent(RealTimeData_R realTimeData_R)
        {
            SetText(DateTime.Now.ToString() + " 收到实时数据信息：", false);
            SetText("--------------------------------------", false);
            ShowHeader(realTimeData_R.Header);
            SetText("报体：", false);
            SetText("数据块数：" + realTimeData_R.BlockCount.ToString(), false);
            foreach (RealTimeDataBlock item in realTimeData_R.RealTimeDataBlocks)
            {
                SetText("块序号：" + item.BlockNo.ToString(), false);
                SetText("采集时间：" + item.SateTimeMark.ToString(), false);
                SetText("温度1：" + item.Temperature1.ToString(), false);
                SetText("温度2：" + item.Temperature2.ToString(), false);
                SetText("温度3：" + item.Temperature3.ToString(), false);
                SetText("温度4：" + item.Temperature4.ToString(), false);
                SetText("温度5：" + item.Temperature5.ToString(), false);
                SetText("湿度：" + item.Humidity.ToString(), false);
                SetText("电量：" + item.Electric.ToString(), false);
                SetText("信号：" + item.Signal.ToString(), false);
            }
            SetText("--------------------------------------", false);
        }

        void tcpserver_ConfigEvent(Config_R config_R)
        {
            SetText(DateTime.Now.ToString() + " 收到配置请求信息：", false);
            SetText("--------------------------------------", false);
            ShowHeader(config_R.Header);
            SetText("报体：", false);
            SetText("--------------------------------------", false);
        }

        void tcpserver_TestEvent(NetData.Test_R test_R)
        {
            SetText(DateTime.Now.ToString() + " 收到测试信息：", false);
            SetText("--------------------------------------", false);
            ShowHeader(test_R.Header);
            SetText("报体：", false);
            SetText("--------------------------------------", false);
        }

        /// <summary>
        /// 显示报头信息
        /// </summary>
        /// <param name="h"></param>
        private void ShowHeader(Head h)
        {
            SetText("报头：" + StringHelper.DataToStrV2(h.ToByte()), false);
            SetText("设备SN：" + h.DeviceSN, false);
            SetText("时间戳：" + h.SateTimeMark.ToString(), false);

        }
        /// <summary>
        /// 异步数据显示在界面
        /// </summary>
        /// <param name="str"></param>
        delegate void SetTextHandle(string str, bool isClear);
        private void SetText(string str, bool isClear)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new SetTextHandle(SetText), str, isClear);
            }
            else
            {
                if (isClear)
                {
                    this.textBox1.Text = str + Environment.NewLine;
                }
                else
                {
                    this.textBox1.Text += str + Environment.NewLine;
                }

            }
        }



        private void 生成测试命令ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Head h = new Head();
            h.CmdHeader = 43605;
            h.CmdCommand = Common.Command.cmd_Test;
            h.DataContext = 43605;
            h.CommandCount = 21;
            h.DeviceSN = "0A5F01CD0001";
            h.State = 0;
            h.SateTimeMark = DateTime.Now;
            SetText(StringHelper.DataToStr(h.ToByte()), true);
        }

        private void 生成实时数据命令ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Head h = new Head();
            h.CmdHeader = 43605;
            h.CmdCommand = Common.Command.cmd_RealTimeDate;
            h.DataContext = 43605;
            h.CommandCount = 21;
            h.DeviceSN = "0A5F01CD0001";
            h.State = 0;
            h.SateTimeMark = DateTime.Now;
            RealTimeData_R rr = new RealTimeData_R();
            rr.Header = h;

            RealTimeDataBlock block = new RealTimeDataBlock();
            block.BlockNo = 1;
            block.SateTimeMark = DateTime.Now;
            block.Temperature1 = (decimal)11.45;
            block.Temperature2 = (decimal)12.45;
            block.Temperature3 = (decimal)13.45;
            block.Temperature4 = (decimal)14.45;
            block.Temperature5 = (decimal)15.45;
            block.Humidity = (decimal)16.45;
            block.Electric = (decimal)17.45;
            block.Signal = (decimal)18.45;

            RealTimeDataBlock block1 = new RealTimeDataBlock();
            block1.BlockNo = 2;
            block1.SateTimeMark = DateTime.Now;
            block1.Temperature1 = (decimal)21.45;
            block1.Temperature2 = (decimal)22.45;
            block1.Temperature3 = (decimal)23.45;
            block1.Temperature4 = (decimal)24.45;
            block1.Temperature5 = (decimal)25.45;
            block1.Humidity = (decimal)26.45;
            block1.Electric = (decimal)27.45;
            block1.Signal = (decimal)28.45;

            RealTimeDataBlock block2 = new RealTimeDataBlock();
            block2.BlockNo = 3;
            block2.SateTimeMark = DateTime.Now;
            block2.Temperature1 = (decimal)31.45;
            block2.Temperature2 = (decimal)32.45;
            block2.Temperature3 = (decimal)33.45;
            block2.Temperature4 = (decimal)34.45;
            block2.Temperature5 = (decimal)35.45;
            block2.Humidity = (decimal)36.45;
            block2.Electric = (decimal)37.45;
            block2.Signal = (decimal)38.45;

            rr.RealTimeDataBlocks.Add(block);
            rr.RealTimeDataBlocks.Add(block1);
            rr.RealTimeDataBlocks.Add(block2);

            SetText(StringHelper.DataToStr(rr.ToByte()), true);
        }
    }
}
