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

namespace DataCollecting
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            //模拟测试命令
            Head h = new Head();
            h.CmdHeader = 43605;
            h.CmdCommand = Common.Command.cmd_Test;
            h.DataContext = 43605;
            h.CommandCount = 21;

            h.DeviceSN = "0A5F01CD0001";
            h.State = 0;
            h.SateTimeMark = DateTime.Now;
            byte[] hehe = h.ToByte();
            string heihie = StringHelper.DataToStr(hehe);

            TcpNetServer tcpserver = new TcpNetServer();
            tcpserver.TestEvent += new TcpNetServer.TestHandle(tcpserver_TestEvent);
            tcpserver.ConfigEvent += new TcpNetServer.ConfigHandle(tcpserver_ConfigEvent);
            tcpserver.RealTimeDataEvent += new TcpNetServer.RealTimeDataHandle(tcpserver_RealTimeDataEvent);
            tcpserver.UserEventEvent += new TcpNetServer.UserEventHandle(tcpserver_UserEventEvent);
        }

        void tcpserver_UserEventEvent(UserEvent_R userEvent_R)
        {
            SetText(DateTime.Now.ToString() + "收到用户事件信息：");
            SetText("--------------------------------------");
            ShowHeader(userEvent_R.Header);
            SetText("报体：");
            SetText("--------------------------------------");
        }

        void tcpserver_RealTimeDataEvent(RealTimeData_R realTimeData_R)
        {
            SetText(DateTime.Now.ToString() + "收到实时数据信息：");
            SetText("--------------------------------------");
            ShowHeader(realTimeData_R.Header);
            SetText("报体：");
            SetText("--------------------------------------");
        }

        void tcpserver_ConfigEvent(Config_R config_R)
        {
            SetText(DateTime.Now.ToString() + "收到配置请求信息：");
            SetText("--------------------------------------");
            ShowHeader(config_R.Header);
            SetText("报体：");
            SetText("--------------------------------------");
        }

        void tcpserver_TestEvent(NetData.Test_R test_R)
        {
            SetText(DateTime.Now.ToString() + "收到测试信息：");
            SetText("--------------------------------------");
            ShowHeader(test_R.Header);
            SetText("报体：");
            SetText("--------------------------------------");
        }

        /// <summary>
        /// 显示报头信息
        /// </summary>
        /// <param name="h"></param>
        private void ShowHeader(Head h)
        {
            SetText("报头：" + StringHelper.DataToStrV2(h.ToByte()));
            SetText("设备SN：" + h.DeviceSN);
            SetText("时间戳：" + h.SateTimeMark.ToString());
        
        }
        /// <summary>
        /// 异步数据显示在界面
        /// </summary>
        /// <param name="str"></param>
        delegate void SetTextHandle(string str);
        private void SetText(string str)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new SetTextHandle(SetText), str);
            }
            else
            {
                this.textBox1.Text += str + Environment.NewLine;
            }
        }
    }
}
