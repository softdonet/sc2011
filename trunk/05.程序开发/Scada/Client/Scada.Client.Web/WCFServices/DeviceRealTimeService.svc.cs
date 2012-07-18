using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading;
using Scada.BLL.Implement;
using System.Xml;
using System.Xml.Linq;

namespace Scada.Client.Web.WCFServices
{
    /// <summary>
    /// 实时数据服务
    /// yanghk at 2011-12-4
    /// </summary>
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class DeviceRealTimeService : IDeviceRealTimeService
    {
        private Timer timer = null;
        public static object LockObject = new object();
        private static List<IDeviceRealTimeServiceCallback> cilents
                                        = new List<IDeviceRealTimeServiceCallback>();
        DeviceRealTimeMonitorService deviceRealTimeMonitorService = new DeviceRealTimeMonitorService();
        public static DeviceRealTimeService deviceRealTimeService = null;
        SystemManagerService systemManagerService = new SystemManagerService();
        /// <summary>
        /// 构造函数
        /// </summary>
        public DeviceRealTimeService()
        {
            deviceRealTimeService = this;
            int span = 5;//天气预报更新间隔(1小时)
            timer = new Timer(new System.Threading.TimerCallback(SendWeather), this, 2000, span * 60000);
        }

        /// <summary>
        /// 发送天气预报数据
        /// </summary>
        /// <param name="o"></param>
        private void SendWeather(object o)
        {
            try
            {
                SentDataAll(systemManagerService.GetWeather(), MessageType.Weather);
            }
            catch { }
        }

        /// <summary>
        /// 发送数据
        /// </summary>
        /// <param name="data"></param>
        /// <param name="msgType"></param>
        private void SentDataAll(string data, MessageType msgType)
        {
            if (cilents != null && cilents.Count != 0)
            {
                foreach (IDeviceRealTimeServiceCallback isc in cilents)
                {
                    SendData(isc, data, msgType);
                }
            }
        }

        /// <summary>
        /// 发送数据
        /// </summary>
        /// <param name="cilent"></param>
        /// <param name="data"></param>
        /// <param name="msgType"></param>
        private void SendData(IDeviceRealTimeServiceCallback cilent, string data, MessageType msgType)
        {
            NoticeMessage notice = new NoticeMessage();
            notice.NoticeClient = cilent;
            notice.exceptionDelegate += new ExceptionDelegate(notice_exceptionDelegate);
            notice.UsersMessages = data;
            notice.MsgType = msgType;
            notice.Notifiy();
        }

        /// <summary>
        /// 客户端掉线处理
        /// </summary>
        /// <param name="client"></param>
        void notice_exceptionDelegate(IDeviceRealTimeServiceCallback client)
        {
            lock (LockObject)
            {
                cilents.Remove(client);
            }
        }

        #region IDeviceRealTimeService Members

        /// <summary>
        /// 登记回调句柄
        /// </summary>
        public void InitData()
        {
            IDeviceRealTimeServiceCallback iscCurrent =
                            OperationContext.Current.GetCallbackChannel<IDeviceRealTimeServiceCallback>();
            if (!cilents.Contains(iscCurrent))
            {
                cilents.Add(OperationContext.Current.GetCallbackChannel<IDeviceRealTimeServiceCallback>());
            }
        }

        /// <summary>
        /// 获取当前客户端
        /// </summary>
        /// <returns></returns>
        private IDeviceRealTimeServiceCallback GetCurrentClient()
        {
            IDeviceRealTimeServiceCallback iscCurrent =
                                      OperationContext.Current.GetCallbackChannel<IDeviceRealTimeServiceCallback>();
            return iscCurrent;
        }

        /// <summary>
        /// 获取天气预报
        /// </summary>
        public void GetWeatherDataMsg()
        {
            var client = GetCurrentClient();
            if (client != null)
            {
                SendData(client, systemManagerService.GetWeather(), MessageType.Weather);
            }
        }


        /// <summary>
        /// 获取实时数据
        /// </summary>
        public void GetRealTimeDataList()
        {
            var client = GetCurrentClient();
            if (client != null)
            {
                SendData(client, deviceRealTimeMonitorService.SendReaTimedata(), MessageType.RealTimeMsg);
            }
        }

        /// <summary>
        /// 获取告警数据
        /// </summary>
        public void GetAlarmDataList()
        {
            var client = GetCurrentClient();
            if (client != null)
            {
                SendData(client, deviceRealTimeMonitorService.SendAlarmData(), MessageType.AlarmMsg);
            }
        }

        /// <summary>
        /// 获取用户事件
        /// </summary>
        public void GetUserEventDataList()
        {
            var client = GetCurrentClient();
            if (client != null)
            {
                SendData(client, deviceRealTimeMonitorService.SendUserEventData(), MessageType.UserEvent);
            }
        }

        /// <summary>
        /// 获取实时数据
        /// </summary>
        public void GetRealTimeDataListAll()
        {
            SentDataAll(deviceRealTimeMonitorService.SendReaTimedata(), MessageType.RealTimeMsg);
        }

        /// <summary>
        /// 获取告警数据
        /// </summary>
        public void GetAlarmDataListAll()
        {
            SentDataAll(deviceRealTimeMonitorService.SendAlarmData(), MessageType.AlarmMsg);
        }

        /// <summary>
        /// 获取用户事件数据
        /// </summary>
        public void GetUserEventDataListAll()
        {
            SentDataAll(deviceRealTimeMonitorService.SendUserEventData(), MessageType.UserEvent);
        }
        #endregion

    }
}

