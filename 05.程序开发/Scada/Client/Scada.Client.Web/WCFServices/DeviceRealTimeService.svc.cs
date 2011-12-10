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
    public class DeviceRealTimeService : IDeviceRealTimeService
    {
        public static object LockObject = new object();
        private static List<IDeviceRealTimeServiceCallback> cilents
                                        = new List<IDeviceRealTimeServiceCallback>();
        DeviceRealTimeMonitorService deviceRealTimeMonitorService = new DeviceRealTimeMonitorService();

        /// <summary>
        /// 构造函数
        /// </summary>
        public DeviceRealTimeService()
        {
            deviceRealTimeMonitorService.AlarmDataReceived += new DataReceivedHandle(deviceRealTimeMonitorService_AlarmDataReceived);
            deviceRealTimeMonitorService.ReaTimeDataReceived += new DataReceivedHandle(deviceRealTimeMonitorService_ReaTimeDataReceived);
            deviceRealTimeMonitorService.CallDataReceived += new DataReceivedHandle(deviceRealTimeMonitorService_CallDataReceived);
        }

        #region 实时数据到达处理函数

        void deviceRealTimeMonitorService_CallDataReceived(XElement data)
        {
            SentData(data, MessageType.CallMsg);
        }

        void deviceRealTimeMonitorService_ReaTimeDataReceived(XElement data)
        {
            SentData(data, MessageType.RealTimeMsg);
        }

        void deviceRealTimeMonitorService_AlarmDataReceived(XElement data)
        {
            SentData(data, MessageType.AlarmMsg);
        }


        #endregion

        /// <summary>
        /// 发送数据
        /// </summary>
        /// <param name="data"></param>
        /// <param name="msgType"></param>
        private void SentData(XElement data, MessageType msgType)
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
    }
}

