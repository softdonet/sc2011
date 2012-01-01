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
            byte[] hehe=h.ToByte();
           string heihie= StringHelper.DataToStr(hehe);
       
            TcpNetServer tcpserver = new TcpNetServer();
            tcpserver.TestEvent += new TcpNetServer.TestHandle(tcpserver_TestEvent);
        }

        void tcpserver_TestEvent(NetData.Test_R test_R)
        {
            SetText(test_R.Header.DeviceSN);
            SetText(test_R.Header.SateTimeMark.ToString());
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
