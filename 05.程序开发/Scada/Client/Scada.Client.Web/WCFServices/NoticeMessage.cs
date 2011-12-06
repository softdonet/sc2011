using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading;

namespace Scada.Client.Web.WCFServices
{
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
        public string UsersMessages { get; set; }
       
        public event ExceptionDelegate exceptionDelegate;

        public NoticeMessage()
        {
            noticeThread = new Thread(new ThreadStart(SendMessage));
        }

        public void NoticeGo()
        {
            noticeThread.Start();
        }

        private void SendMessage()
        {
            if (NoticeClient != null && !string.IsNullOrEmpty(UsersMessages))
            {
                try
                {
                    NoticeClient.GetData(UsersMessages);
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