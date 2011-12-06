using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading;

namespace Scada.Client.Web.WCFServices
{
    /// <summary>
    /// 实时数据服务
    /// yanghk at 2011-12-4
    /// </summary>
    public class DeviceRealTimeService : IDeviceRealTimeService
    {


        public static object LockObject = new object();
        private Timer timer;
        private static List<IDeviceRealTimeServiceCallback> cilents 
                                        = new List<IDeviceRealTimeServiceCallback>();


        /// <summary>
        /// 构造函数
        /// </summary>
        public DeviceRealTimeService()
        {
            timer = new Timer(new System.Threading.TimerCallback(CheckMessages), this, 5000, 5000);
        }

        /// <summary>
        /// 检测数据库是否有新数据，并且发送到客户端
        /// </summary>
        /// <param name="o"></param>
        private void CheckMessages(object o)
        {
            try
            {
                //TODO：检测数据库是否有变化
                SentData(DateTime.Now.ToString());
            }
            catch
            {
            }
        }

        /// <summary>
        /// 发送数据
        /// </summary>
        /// <param name="data"></param>
        public void SentData(string data)
        {
            if (cilents != null && cilents.Count != 0)
            {
                foreach (IDeviceRealTimeServiceCallback isc in cilents)
                {
                    NoticeMessage notice = new NoticeMessage();
                    notice.NoticeClient = isc;
                    notice.exceptionDelegate += new ExceptionDelegate(notice_exceptionDelegate);
                    notice.UsersMessages = "数目：" + cilents.Count.ToString() + "时间" + data;
                    notice.NoticeGo();
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

