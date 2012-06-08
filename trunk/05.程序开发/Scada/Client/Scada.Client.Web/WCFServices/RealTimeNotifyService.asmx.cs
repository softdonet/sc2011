using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace Scada.Client.Web.WCFServices
{
    /// <summary>
    /// 入库程序调用
    /// 通知客户端有数据到达
    /// yanghk at 2012-6-8
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class RealTimeNotifyService : System.Web.Services.WebService
    {

        [WebMethod]
        /// <summary>
        /// 用户事件到达
        /// </summary>
        public void UserEventDataReceive()
        {
            if (DeviceRealTimeService.deviceRealTimeService != null)
            {
                DeviceRealTimeService.deviceRealTimeService.GetUserEventDataListAll();
            }
        }

        [WebMethod]
        /// <summary>
        /// 实时信息到达
        /// </summary>
        public void ReaTimeDataReceivedReceive()
        {
            if (DeviceRealTimeService.deviceRealTimeService != null)
            {
                DeviceRealTimeService.deviceRealTimeService.GetRealTimeDataListAll();
            }
        }

        [WebMethod]
        /// <summary>
        /// 告警数据到达
        /// </summary>
        public void AlarmDataReceived()
        {
            if (DeviceRealTimeService.deviceRealTimeService != null)
            {
                DeviceRealTimeService.deviceRealTimeService.GetAlarmDataListAll();
            }
        }
    }
}
