using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessRules.RealTimeNotifyService;
using Utility;

namespace BusinessRules
{
    public class RealTimeNotify
    {
        RealTimeNotifyServiceSoapClient realTimeNotifyService = new RealTimeNotifyServiceSoapClient();
        /// <summary>
        /// 通知服务器数据更新
        /// </summary>
        /// <param name="msgtype"></param>
       public void Notify(MessageType msgtype)
        {
            try
            {
                switch (msgtype)
                {
                    case MessageType.RealTimeMsg:
                        realTimeNotifyService.ReaTimeDataReceivedReceive();
                        break;
                    case MessageType.AlarmMsg:
                        realTimeNotifyService.AlarmDataReceived();
                        break;
                    case MessageType.UserEvent:
                        realTimeNotifyService.UserEventDataReceive();
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteExceptionLog(ex);
            }

        }
    }
}
