using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Scada.Server.WCFService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IDeviceRealTimeService" in both code and config file together.
    [ServiceContract(Namespace = "", CallbackContract = typeof(IDeviceRealTimeServiceCallback))]
    public interface IDeviceRealTimeService
    {
        [OperationContract(IsOneWay = true)]
        void SentData(string data);
        [OperationContract(IsOneWay = true)]
        void InitData();
    }

    [ServiceContract]
    public interface IDeviceRealTimeServiceCallback
    {
        [OperationContract(IsOneWay = true)]
        void GetData(string data);
    }
}
