﻿using System;
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
        public frm_Test()
        {
            InitializeComponent();

            TcpNetServer tcpserver = new TcpNetServer();
            tcpserver.TestEvent_R += new TcpNetServer.TestHandle_R(tcpserver_TestEvent_R);
            tcpserver.TestEvent_S += new TcpNetServer.TestHandle_S(tcpserver_TestEvent_S);
            tcpserver.ConfigEvent_R += new TcpNetServer.ConfigHandle_R(tcpserver_ConfigEvent_R);
            tcpserver.RealTimeDataEvent_R += new TcpNetServer.RealTimeDataHandle_R(tcpserver_RealTimeDataEvent_R);
            tcpserver.UserEventEvent_R += new TcpNetServer.UserEventHandle_R(tcpserver_UserEventEvent_R);
            tcpserver.RegisterEvent_R += new TcpNetServer.RegisterHandle_R(tcpserver_RegisterEvent_R);
            tcpserver.FirmwareRequestEvent_R += new TcpNetServer.FirmwareRequestHandle_R(tcpserver_FirmwareRequestEvent_R);
            tcpserver.RealTimeDataEvent_S += new TcpNetServer.RealTimeDataHandle_S(tcpserver_RealTimeDataEvent_S);
        }

        void tcpserver_TestEvent_S(Test_S test_S)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(DateTime.Now.ToString() + " 【发送】测试回复：" + Environment.NewLine);
            sb.Append(GetLine());
            sb.Append(GetHeader(test_S));
            sb.Append("报体：" + Environment.NewLine);
            sb.Append("    数据内容：" + StringHelper.DataToStrV2(BitConverter.GetBytes(test_S.Content)) + Environment.NewLine);
            sb.Append(GetVerify(test_S));
            sb.Append(GetLine());
            SetText(sb.ToString(), false);
        }

        #region 公共方法
        /// <summary>
        /// 获取报头信息
        /// </summary>
        /// <param name="h"></param>
        private string GetHeader(MessageBase mb)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("报头：" + StringHelper.DataToStrV2(mb.Header.ToByte()) + Environment.NewLine);
            sb.Append("    命令头：" + StringHelper.DataToStrV2(BitConverter.GetBytes(mb.Header.CmdHeader)) + Environment.NewLine);
            sb.Append("    功能码：" + StringHelper.DataToStr(((byte)mb.Header.CmdCommand)) + Environment.NewLine);
            sb.Append("    数据上下文：" + StringHelper.DataToStrV2(BitConverter.GetBytes(mb.Header.DataContext)) + Environment.NewLine);
            sb.Append("    报文长度：" + mb.Header.CommandCount.ToString() + Environment.NewLine);
            sb.Append("    状态：" + mb.Header.State.ToString() + Environment.NewLine);
            sb.Append("    设备SN：" + mb.Header.DeviceSN + Environment.NewLine);
            sb.Append("    时间戳：" + mb.Header.SateTimeMark.ToString() + Environment.NewLine);
            return sb.ToString();
        }

        /// <summary>
        /// 获取设备请求报体
        /// </summary>
        /// <param name="rr"></param>
        /// <returns></returns>
        private string GetRequestBody(RequestBase_R rr)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("报体：" + Environment.NewLine);
            sb.Append("    MAC地址：" + rr.MAC + Environment.NewLine);
            sb.Append("    SIM卡号：" + rr.SIM + Environment.NewLine);
            sb.Append("    设备型号：" + rr.DeviveType + Environment.NewLine);
            sb.Append("    硬件版本号：" + String.Format("{0}.{1}", rr.HardwareVersionMain.ToString(), rr.HardwareVersionChild.ToString()) + Environment.NewLine);
            sb.Append("    软件版本号：" + String.Format("{0}.{1}", rr.SoftwareVersionMain.ToString(), rr.SoftwareVersionChild.ToString()) + Environment.NewLine);
            sb.Append("    工作状态：" + String.Format("{0}.{1}", rr.WorkstateMain.ToString(), rr.WorkstateChild.ToString()) + Environment.NewLine);
            return sb.ToString();
        }

        /// <summary>
        /// 获取校验位字符串
        /// </summary>
        /// <param name="mb"></param>
        /// <returns></returns>
        private string GetVerify(MessageBase mb)
        {
            return string.Format("校验位：{0}", StringHelper.DataToStrV2(BitConverter.GetBytes(mb.VerifyData)) + Environment.NewLine);
        }

        /// <summary>
        /// 获取一根线
        /// </summary>
        /// <returns></returns>
        private string GetLine()
        {
            return "--------------------------------------" + Environment.NewLine;
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
                textBox1.SelectionStart = textBox1.TextLength;
                textBox1.ScrollToCaret();
            }
        }

        #endregion

        #region 设备事件

        void tcpserver_RealTimeDataEvent_S(RealTimeData_S realTimeData_S)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(DateTime.Now.ToString() + " 【发送】服务器实时回复：" + Environment.NewLine);
            sb.Append(GetLine());
            sb.Append(GetHeader(realTimeData_S));
            sb.Append("报体：" + Environment.NewLine);
            sb.Append("    是否含有配置信息：" + realTimeData_S.HaveConfigInfo.ToString() + Environment.NewLine);
            sb.Append("    是否含有天气信息：" + realTimeData_S.HaveWeatherInfo.ToString() + Environment.NewLine);
            sb.Append("    是否含有广播信息：" + realTimeData_S.HaveBroadcastInfo.ToString() + Environment.NewLine);
            sb.Append(Environment.NewLine);
            sb.Append("    运行模式：" + realTimeData_S.ConfigData.RunMode.ToString() + Environment.NewLine);
            sb.Append("    参数1：" + realTimeData_S.ConfigData.Argument1.ToString() + Environment.NewLine);
            sb.Append("    参数2：" + realTimeData_S.ConfigData.Argument2.ToString() + Environment.NewLine);
            sb.Append("    参数3：" + realTimeData_S.ConfigData.Argument3.ToString() + Environment.NewLine);
            sb.Append("    设备编号：" + realTimeData_S.ConfigData.DeviceNo.ToString() + Environment.NewLine);
            sb.Append("    默认显示方式：" + realTimeData_S.ConfigData.DisplayMode.ToString() + Environment.NewLine);
            sb.Append("    是否启用紧急按钮：" + realTimeData_S.ConfigData.InstancyBtnEnable.ToString() + Environment.NewLine);
            sb.Append("    是否启用信息按钮：" + realTimeData_S.ConfigData.InfoBtnEnable.ToString() + Environment.NewLine);
            sb.Append("    维护人员手机号码：" + realTimeData_S.ConfigData.RepairTel.ToString() + Environment.NewLine);
            sb.Append("    主DNS：" + realTimeData_S.ConfigData.MainDNS.ToString() + Environment.NewLine);
            sb.Append("    备用DNS：" + realTimeData_S.ConfigData.ReserveDNS.ToString() + Environment.NewLine);
            sb.Append("    服务器IP：" + realTimeData_S.ConfigData.ServerIP.ToString() + Environment.NewLine);
            sb.Append("    域名：" + realTimeData_S.ConfigData.DomainName.ToString() + Environment.NewLine);
            sb.Append("    端口号：" + realTimeData_S.ConfigData.Port.ToString() + Environment.NewLine);
            sb.Append(Environment.NewLine);
            sb.Append("    今日天气标识：" + realTimeData_S.WeatherData.TodayMark.ToString() + Environment.NewLine);
            sb.Append("    今日天气信息：" + realTimeData_S.WeatherData.TodayWeather + Environment.NewLine);
            sb.Append("    明日天气标识：" + realTimeData_S.WeatherData.TomorrowMark.ToString() + Environment.NewLine);
            sb.Append("    明日天气信息：" + realTimeData_S.WeatherData.TomorrowWeather + Environment.NewLine);
            sb.Append(Environment.NewLine);
            sb.Append("    广播信息：" + realTimeData_S.BroadcastData.Msg + Environment.NewLine);
            sb.Append(GetVerify(realTimeData_S));
            sb.Append(GetLine());
            SetText(sb.ToString(), false);
        }

        /// <summary>
        /// 请求固件更新
        /// </summary>
        /// <param name="firmwareRequest_R"></param>
        void tcpserver_FirmwareRequestEvent_R(FirmwareRequest_R firmwareRequest_R)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(DateTime.Now.ToString() + " 收到设备固件更新请求：" + Environment.NewLine);
            sb.Append(GetLine());
            sb.Append(GetHeader(firmwareRequest_R));
            sb.Append(GetRequestBody(firmwareRequest_R));
            sb.Append(GetVerify(firmwareRequest_R));
            sb.Append(GetLine());
            SetText(sb.ToString(), false);
        }

        /// <summary>
        /// 请求注册事件
        /// </summary>
        /// <param name="register_R"></param>
        void tcpserver_RegisterEvent_R(Register_R register_R)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(DateTime.Now.ToString() + " 收到设备注册请求：" + Environment.NewLine);
            sb.Append(GetLine());
            sb.Append(GetHeader(register_R));
            sb.Append(GetRequestBody(register_R));
            sb.Append(GetVerify(register_R));
            sb.Append(GetLine());
            SetText(sb.ToString(), false);
        }

        /// <summary>
        /// 用户事件
        /// </summary>
        /// <param name="userEvent_R"></param>
        void tcpserver_UserEventEvent_R(UserEvent_R userEvent_R)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(DateTime.Now.ToString() + "【收到】用户事件信息：" + Environment.NewLine);
            sb.Append(GetLine());
            sb.Append(GetHeader(userEvent_R));
            sb.Append(GetRequestBody(userEvent_R));
            sb.Append(GetVerify(userEvent_R));
            sb.Append(GetLine());
            SetText(sb.ToString(), false);
        }

        /// <summary>
        /// 实时数据到达事件
        /// </summary>
        /// <param name="realTimeData_R"></param>
        void tcpserver_RealTimeDataEvent_R(RealTimeData_R realTimeData_R)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(DateTime.Now.ToString() + " 【收到】实时数据信息：" + Environment.NewLine);
            sb.Append(GetLine());
            sb.Append(GetHeader(realTimeData_R));
            sb.Append("报体：" + Environment.NewLine);
            sb.Append("    数据块数：" + realTimeData_R.BlockCount.ToString() + Environment.NewLine);
            foreach (RealTimeDataBlock item in realTimeData_R.RealTimeDataBlocks)
            {
                sb.Append("    块序号：" + item.BlockNo.ToString() + Environment.NewLine);
                sb.Append("    采集时间：" + item.SateTimeMark.ToString() + Environment.NewLine);
                sb.Append("    温度1：" + item.Temperature1.ToString() + Environment.NewLine);
                sb.Append("    温度2：" + item.Temperature2.ToString() + Environment.NewLine);
                sb.Append("    温度3：" + item.Temperature3.ToString() + Environment.NewLine);
                sb.Append("    温度4：" + item.Temperature4.ToString() + Environment.NewLine);
                sb.Append("    温度5：" + item.Temperature5.ToString() + Environment.NewLine);
                sb.Append("    湿度：" + item.Humidity.ToString() + Environment.NewLine);
                sb.Append("    电量：" + item.Electric.ToString() + Environment.NewLine);
                sb.Append("    信号：" + item.Signal.ToString() + Environment.NewLine);
            }
            sb.Append(GetVerify(realTimeData_R));
            sb.Append(GetLine());
            SetText(sb.ToString(), false);
        }

        /// <summary>
        /// 请求配置事件
        /// </summary>
        /// <param name="config_R"></param>
        void tcpserver_ConfigEvent_R(Config_R config_R)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(DateTime.Now.ToString() + " 【收到】配置请求信息：" + Environment.NewLine);
            sb.Append(GetLine());
            sb.Append(GetHeader(config_R));
            sb.Append("报体：" + Environment.NewLine);
            sb.Append("    MAC地址：" + config_R.MAC + Environment.NewLine);
            sb.Append("    SIM卡号：" + config_R.SIM + Environment.NewLine);
            sb.Append("    设备型号：" + config_R.DeviveType + Environment.NewLine);
            sb.Append("    硬件版本号：" + String.Format("{0}.{1}", config_R.HardwareVersionMain.ToString(), config_R.HardwareVersionChild.ToString()) + Environment.NewLine);
            sb.Append("    软件版本号：" + String.Format("{0}.{1}", config_R.SoftwareVersionMain.ToString(), config_R.SoftwareVersionChild.ToString()) + Environment.NewLine);
            sb.Append("    工作状态：" + String.Format("{0}.{1}", config_R.WorkstateMain.ToString(), config_R.WorkstateChild.ToString()) + Environment.NewLine);
            sb.Append(GetVerify(config_R));
            sb.Append(GetLine());
            SetText(sb.ToString(), false);
        }

        /// <summary>
        /// 测试数据事件
        /// </summary>
        /// <param name="test_R"></param>
        void tcpserver_TestEvent_R(NetData.Test_R test_R)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(DateTime.Now.ToString() + " 【收到】测试信息：" + Environment.NewLine);
            sb.Append(GetLine());
            sb.Append(GetHeader(test_R));
            sb.Append("报体：" + Environment.NewLine);
            sb.Append("    数据内容：" + StringHelper.DataToStrV2(BitConverter.GetBytes(test_R.Content)) + Environment.NewLine);
            sb.Append(GetVerify(test_R));
            sb.Append(GetLine());
            SetText(sb.ToString(), false);
        }

        #endregion

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
            h.DeviceSN = "0A5F01CD0001";
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
            this.textBox1.Text = string.Empty;
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


            ConfigDataBlock configDataBlock = new ConfigDataBlock();
            configDataBlock.RunMode = 1;
            configDataBlock.Argument1 = 1500;
            configDataBlock.Argument2 = 1600;
            configDataBlock.Argument3 = 1700;
            configDataBlock.DeviceNo = "P-100100";
            configDataBlock.DisplayMode = 1;
            configDataBlock.InstancyBtnEnable = true;
            configDataBlock.InfoBtnEnable = true;
            configDataBlock.RepairTel = "13801112222";
            configDataBlock.MainDNS = "202.106.42.1";
            configDataBlock.ReserveDNS = "202.106.42.2";
            configDataBlock.ServerIP = "192.168.0.1";
            configDataBlock.DomainName = "xyz.dddd.com";
            configDataBlock.Port = 1789;
            rs.ConfigData = configDataBlock;


            WeatherDataBlock weatherDataBlock = new WeatherDataBlock();
            weatherDataBlock.TodayMark = 0;
            weatherDataBlock.TodayWeather = "晴转XXX多云，有阵雨。";
            weatherDataBlock.TomorrowMark = 1;
            weatherDataBlock.TomorrowWeather = "温度很低，有大雪，注意防寒。";
            rs.WeatherData = weatherDataBlock;

            BroadcastDataBlock broadcastDataBlock = new BroadcastDataBlock();
            broadcastDataBlock.Msg = "通知：明天下午开会。不要迟到，知道一分钟扣100元。";

            rs.BroadcastData = broadcastDataBlock;
            SetText(StringHelper.DataToStr(rs.ToByte()) + Environment.NewLine, false);
        }
    }
}
