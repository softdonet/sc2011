using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataCollecting.NetServer;
using NetData;
using Utility;
namespace DataCollecting
{

    public class ConmmandAnalysis
    {
        TcpNetServer tcpserver = null;
        public ConmmandAnalysis(TcpNetServer tcpserver1)
        {
            tcpserver = tcpserver1;
            RegistEvent();
        }
        /// <summary>
        /// 订阅事件
        /// </summary>
        public void RegistEvent()
        {
            tcpserver.TestEvent_R += new TcpNetServer.TestHandle_R(tcpserver_TestEvent_R);
            tcpserver.TestEvent_S += new TcpNetServer.TestHandle_S(tcpserver_TestEvent_S);
            tcpserver.ConfigEvent_R += new TcpNetServer.ConfigHandle_R(tcpserver_ConfigEvent_R);
            tcpserver.RealTimeDataEvent_R += new TcpNetServer.RealTimeDataHandle_R(tcpserver_RealTimeDataEvent_R);
            tcpserver.UserEventEvent_R += new TcpNetServer.UserEventHandle_R(tcpserver_UserEventEvent_R);
            tcpserver.RegisterEvent_R += new TcpNetServer.RegisterHandle_R(tcpserver_RegisterEvent_R);
            tcpserver.FirmwareRequestEvent_R += new TcpNetServer.FirmwareRequestHandle_R(tcpserver_FirmwareRequestEvent_R);
            tcpserver.RealTimeDataEvent_S += new TcpNetServer.RealTimeDataHandle_S(tcpserver_RealTimeDataEvent_S);
        }

        /// <summary>
        /// 取消订阅事件
        /// </summary>
        public void RemoveEvent()
        {
            tcpserver.TestEvent_R -= new TcpNetServer.TestHandle_R(tcpserver_TestEvent_R);
            tcpserver.TestEvent_S -= new TcpNetServer.TestHandle_S(tcpserver_TestEvent_S);
            tcpserver.ConfigEvent_R -= new TcpNetServer.ConfigHandle_R(tcpserver_ConfigEvent_R);
            tcpserver.RealTimeDataEvent_R -= new TcpNetServer.RealTimeDataHandle_R(tcpserver_RealTimeDataEvent_R);
            tcpserver.UserEventEvent_R -= new TcpNetServer.UserEventHandle_R(tcpserver_UserEventEvent_R);
            tcpserver.RegisterEvent_R -= new TcpNetServer.RegisterHandle_R(tcpserver_RegisterEvent_R);
            tcpserver.FirmwareRequestEvent_R -= new TcpNetServer.FirmwareRequestHandle_R(tcpserver_FirmwareRequestEvent_R);
            tcpserver.RealTimeDataEvent_S -= new TcpNetServer.RealTimeDataHandle_S(tcpserver_RealTimeDataEvent_S);
        }

        #region 公共方法

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
            SetText(sb.ToString());
        }

        /// <summary>
        /// 获取报头信息
        /// </summary>
        /// <param name="h"></param>
        private string GetHeader(MessageBase mb)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("报头(不含长度和校验位)：" + StringHelper.DataToStrV2(mb.Header.ToByte()) + Environment.NewLine);
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
            sb.Append("    硬件版本号：" + String.Format("{0}.{1}", StringHelper.DataToStr(rr.HardwareVersionMain), StringHelper.DataToStr(rr.HardwareVersionChild)) + Environment.NewLine);
            sb.Append("    软件版本号：" + String.Format("{0}.{1}", StringHelper.DataToStr(rr.SoftwareVersionMain), StringHelper.DataToStr(rr.SoftwareVersionChild)) + Environment.NewLine);
            sb.Append("    工作状态：" + String.Format("{0}.{1}", StringHelper.DataToStr(rr.WorkstateMain), StringHelper.DataToStr(rr.WorkstateChild)) + Environment.NewLine);
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
        public delegate void OnReceiveHandle(string str);
        public event OnReceiveHandle OnReceiveMsg;
        private void SetText(string str)
        {
            if (OnReceiveMsg != null)
            {
                this.OnReceiveMsg(str);
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
            if (realTimeData_S.HaveConfigInfo)
            {
                sb.Append(Environment.NewLine);
                sb.Append("    运行模式：" + realTimeData_S.ConfigData.RunMode.ToString() + Environment.NewLine);
                sb.Append("    参数1：" + realTimeData_S.ConfigData.Argument1.ToString() + Environment.NewLine);
                sb.Append("    参数2：" + realTimeData_S.ConfigData.Argument2.ToString() + Environment.NewLine);
                sb.Append("    参数3：" + realTimeData_S.ConfigData.Argument3.ToString() + Environment.NewLine);
                sb.Append("    设备编号：" + realTimeData_S.ConfigData.DeviceNo.ToString() + Environment.NewLine);
                sb.Append("    安装地点：" + realTimeData_S.ConfigData.InstalPlace + Environment.NewLine);
                sb.Append("    默认显示方式：" + realTimeData_S.ConfigData.DisplayMode.ToString() + Environment.NewLine);
                sb.Append("    是否启用紧急按钮：" + realTimeData_S.ConfigData.InstancyBtnEnable.ToString() + Environment.NewLine);
                sb.Append("    是否启用信息按钮：" + realTimeData_S.ConfigData.InfoBtnEnable.ToString() + Environment.NewLine);
                sb.Append("    维护人员手机号码：" + realTimeData_S.ConfigData.RepairTel.ToString() + Environment.NewLine);
                sb.Append("    主IP：" + realTimeData_S.ConfigData.MainIP.ToString() + Environment.NewLine);
                sb.Append("    备用IP：" + realTimeData_S.ConfigData.ReserveIP.ToString() + Environment.NewLine);
                sb.Append("    域名：" + realTimeData_S.ConfigData.DomainName.ToString() + Environment.NewLine);
                sb.Append("    端口号：" + realTimeData_S.ConfigData.Port.ToString() + Environment.NewLine);
                sb.Append("    连接方式：" + realTimeData_S.ConfigData.ConnectionType.ToString() + Environment.NewLine);
                sb.Append("    接入点名称：" + realTimeData_S.ConfigData.ConnectName + Environment.NewLine);
            }
            if (realTimeData_S.HaveWeatherInfo)
            {
                sb.Append(Environment.NewLine);
                sb.Append("    今日天气信息：" + realTimeData_S.WeatherData.TodayWeather + Environment.NewLine);
            }
            if (realTimeData_S.HaveBroadcastInfo)
            {
                sb.Append(Environment.NewLine);
                sb.Append("    广播信息：" + realTimeData_S.BroadcastData.Msg + Environment.NewLine);
            }
            sb.Append(GetVerify(realTimeData_S));
            sb.Append(GetLine());
            SetText(sb.ToString());
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
            SetText(sb.ToString());
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
            SetText(sb.ToString());
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
            SetText(sb.ToString());
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
                sb.Append("    湿度：" + item.Humidity.ToString() + Environment.NewLine);
                sb.Append("    电量：" + item.Electric.ToString() + Environment.NewLine);
                sb.Append("    信号：" + item.Signal.ToString() + Environment.NewLine);
            }
            sb.Append(GetVerify(realTimeData_R));
            sb.Append(GetLine());
            SetText(sb.ToString());
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
            SetText(sb.ToString());
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
            SetText(sb.ToString());
        }

        #endregion

    }
}
