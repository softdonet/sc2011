using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net.Sockets;
using Utility;
using NetData;

namespace SocketTestTool
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        List<CNetClient> cList = new List<CNetClient>();
        private void button1_Click(object sender, EventArgs e)
        {
            CNetClient cc = new CNetClient("127.0.0.1");
            cc.Connect();
            DateTime startTime = this.dateTimePicker1.Value;
            DateTime EndTime = this.dateTimePicker1.Value;
            while (startTime < EndTime)
            {
                startTime = startTime.AddMinutes(10);
                Head h = new Head();
                h.CmdHeader = Const.UP_HEADER;
                h.CmdCommand = Command.cmd_RealTimeDate_R;
                h.DataContext = 42605;
                h.DeviceSN = this.txtDevSN.Text.Trim();
                h.State = 0;
                h.SateTimeMark = startTime;
                RealTimeData_R rr = new RealTimeData_R();
                rr.Header = h;
                RealTimeDataBlock block = new RealTimeDataBlock();
                block.BlockNo = 1;
                block.SateTimeMark = startTime;
                block.Temperature1 = RandomHelper.GetRandom(15, 60) + 0.5M;
                block.Temperature2 = RandomHelper.GetRandom(15, 60) + 0.5M;
                block.Humidity = 0.2M;
                block.Electric = (ushort)RandomHelper.GetRandom(0, 400);
                block.Signal = (ushort)RandomHelper.GetRandom(0, 400);
                rr.RealTimeDataBlocks.Add(block);
                string sendStr = StringHelper.DataToStr(rr.ToByte());
                byte[] bytelist = System.Text.Encoding.ASCII.GetBytes(sendStr);
                cc.SendData(bytelist);
            }
            cc.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            foreach (var item in cList)
            {
                item.Connect();
                byte[] bytelist = System.Text.Encoding.ASCII.GetBytes("AA55016DA619000A5F01CD010000DC070316003234D204E313");
                item.SendData(bytelist);
                item.Close();
            }
        }
    }
}
