using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NetData;
using Utility;

namespace DataCollecting
{
    public partial class frmTool : Form
    {
        public frmTool()
        {
            InitializeComponent();
        }

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

            this.textBox1.Text = StringHelper.DataToStr(tr.ToByte());

        }

        private void 生成实时数据命令ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Head h = new Head();
            h.CmdHeader = Const.UP_HEADER;
            h.CmdCommand = Command.cmd_RealTimeDate_R;
            h.DataContext = 42605;
            h.DeviceSN = "0A5F01CD0007";
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
            block1.BlockNo = 2;
            block1.SateTimeMark = DateTime.Now;
            block1.Temperature1 = RandomHelper.GetRandom(15, 60) + 0.5M;
            block1.Temperature2 = RandomHelper.GetRandom(15, 60) + 0.5M;
            block1.Humidity = 0.2M;
            block1.Electric = (ushort)RandomHelper.GetRandom(0, 400);
            block1.Signal = (ushort)RandomHelper.GetRandom(0, 400);

            rr.RealTimeDataBlocks.Add(block);
            rr.RealTimeDataBlocks.Add(block1);

            this.textBox1.Text = StringHelper.DataToStr(rr.ToByte());
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
            cr.MAC = "00-10-30-AF-E1-30-47";
            cr.SIM = "10303239876321900107";
            cr.DeviveType = "ICG-P1000-X";
            cr.HardwareVersionMain = 10;
            cr.HardwareVersionChild = 22;
            cr.SoftwareVersionMain = 33;
            cr.SoftwareVersionChild = 44;
            cr.WorkstateMain = 55;
            cr.WorkstateChild = 66;
            this.textBox1.Text = StringHelper.DataToStr(cr.ToByte());
        }

        private void 用户事件命令ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Head h = new Head();
            h.CmdHeader = Const.UP_HEADER;
            h.CmdCommand = Command.cmd_UserEvent_R;
            h.DataContext = 42605;
            h.DeviceSN = "370201CD0102";
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
            this.textBox1.Text = StringHelper.DataToStr(cr.ToByte());
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
            this.textBox1.Text = StringHelper.DataToStr(cr.ToByte());
        }

        private void 注册命令ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Head h = new Head();
            h.CmdHeader = Const.UP_HEADER;//其实应为Const.DOWN_HEADER,为了测试方便先这样
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
            this.textBox1.Text = StringHelper.DataToStr(cr.ToByte());
        }

        private void 实时数据回复命令ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Head h = new Head();
            h.CmdHeader = Const.UP_HEADER;
            h.CmdCommand = Command.cmd_Reply;
            h.DataContext = 42605;
            h.DeviceSN = "370201CD0118";
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
            broadcastDataBlock.Msg = "通知：明天下午开会,不要迟到。";

            rs.BroadcastData = broadcastDataBlock;
            this.textBox1.Text = StringHelper.DataToStr(rs.ToByte());
        }

        private void 实时回复命令带空格ToolStripMenuItem_Click(object sender, EventArgs e)
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
            broadcastDataBlock.Msg = "通知：明天下午开会,不要迟到。";

            rs.BroadcastData = broadcastDataBlock;
            this.textBox1.Text = StringHelper.DataToStrV2(rs.ToByte());
        }
    }
}
