using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading;
using System.Xml;
using System.Xml.Linq;

namespace Scada.Client.Web.WCFServices
{
    /// <summary>
    /// 消息类别
    /// </summary>
    public enum MessageType
    {
        RealTimeMsg = 0,
        AlarmMsg = 1,
        CallMsg = 2
    }

    /// <summary>
    /// 消息异常委托
    /// </summary>
    /// <param name="client"></param>
    public delegate void ExceptionDelegate(IDeviceRealTimeServiceCallback client);

    /// <summary>
    /// 异步发送消息
    /// yanghk at 2011-12-4
    /// </summary>
    public class NoticeMessage
    {
        private Thread noticeThread;

        /// <summary>
        /// 客户端句柄
        /// </summary>
        public IDeviceRealTimeServiceCallback NoticeClient { get; set; }

        /// <summary>
        /// 要发送的数据
        /// </summary>
        public XElement UsersMessages { get; set; }

        /// <summary>
        /// 消息类别
        /// </summary>
        public MessageType MsgType { get; set; }

        /// <summary>
        /// 发送异常事件
        /// </summary>
        public event ExceptionDelegate exceptionDelegate;
       
        public NoticeMessage()
        {
            noticeThread = new Thread(new ThreadStart(SendMessage));
        }

        public void Notifiy()
        {
            noticeThread.Start();
        }

        /// <summary>
        /// 发送消息
        /// </summary>
        private void SendMessage()
        {
            if (NoticeClient != null)
            {
                try
                {
                    switch (MsgType)
                    {
                        case MessageType.RealTimeMsg:
                            NoticeClient.GetRealTimeData(UsersMessages);
                            break;
                        case MessageType.AlarmMsg:
                            NoticeClient.GetAlarmData(UsersMessages);
                            break;
                        case MessageType.CallMsg:
                            NoticeClient.GetCallData(UsersMessages);
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    if (exceptionDelegate != null)
                        exceptionDelegate.Invoke(NoticeClient);
                }
            }
        }
    }
}