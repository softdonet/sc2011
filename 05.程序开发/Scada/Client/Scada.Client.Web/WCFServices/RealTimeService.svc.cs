using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Scada.Client.Web.WCFServices
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "RealTimeService" in code, svc and config file together.
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class RealTimeService : IRealTimeService
    {
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
