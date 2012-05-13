using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Scada.Client.Web.WCFServices
{
    /// <summary>
    /// 入库程序调用
    /// 通知客户端有数据到达
    /// </summary>
    [ServiceContract]
    public interface IRealTimeService
    {
        [OperationContract]
        void UserEventDataReceive();
        [OperationContract]
        void AlarmDataReceived();
        [OperationContract]
        void ReaTimeDataReceivedReceive();
    }
}
