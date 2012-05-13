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
        public static object LockObject = new object();
        private static List<IDeviceRealTimeServiceCallback> cilents
                                        = new List<IDeviceRealTimeServiceCallback>();
        DeviceRealTimeMonitorService deviceRealTimeMonitorService = new DeviceRealTimeMonitorService();
        public static DeviceRealTimeService deviceRealTimeService = null;
        /// <summary>
        /// 构造函数
        /// </summary>
        public DeviceRealTimeService()
        {
            deviceRealTimeService = this;
        }

        /// <summary>
        /// 发送数据
        /// </summary>
        /// <param name="data"></param>
        /// <param name="msgType"></param>
        private void SentData(string data, MessageType msgType)
        {
            if (cilents != null && cilents.Count != 0)
            {
                foreach (IDeviceRealTimeServiceCallback isc in cilents)
                {
                    NoticeMessage notice = new NoticeMessage();
                    notice.NoticeClient = isc;
                    notice.exceptionDelegate += new ExceptionDelegate(notice_exceptionDelegate);
                    notice.UsersMessages = data;
                    notice.MsgType = msgType;
                    notice.Notifiy();
                }
            }
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
        /// 获取实时数据
        /// </summary>
        public void GetRealTimeDataList()
        {
            SentData(deviceRealTimeMonitorService.SendReaTimedata(), MessageType.RealTimeMsg);
        }

        /// <summary>
        /// 获取告警数据
        /// </summary>
        public void GetAlarmDataList()
        {
            SentData(deviceRealTimeMonitorService.SendAlarmData(), MessageType.AlarmMsg);
        }

        /// <summary>
        /// 获取用户事件
        /// </summary>
        public void GetUserEventDataList()
        {
            SentData(deviceRealTimeMonitorService.SendUserEventData(), MessageType.UserEvent);
        }

        #endregion

    }
}

