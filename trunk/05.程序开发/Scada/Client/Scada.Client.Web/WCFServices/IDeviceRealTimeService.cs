using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace Scada.Client.Web.WCFServices
{

    [ServiceContract(Namespace = "", CallbackContract = typeof(IDeviceRealTimeServiceCallback))]
    public interface IDeviceRealTimeService
    {
        /// <summary>
        /// 初始化数据
        /// </summary>
        [OperationContract(IsOneWay = true)]
        void InitData();
    }

    [ServiceContract]
    public interface IDeviceRealTimeServiceCallback
    {
        /// <summary>
        /// 获取实时数据
        /// </summary>
        /// <param name="data"></param>
        [OperationContract(IsOneWay = true)]
        void GetRealTimeData(XElement data);

        /// <summary>
        /// 获取告警数据
        /// </summary>
        /// <param name="data"></param>
        [OperationContract(IsOneWay = true)]
        void GetAlarmData(XElement data);

        /// <summary>
        /// 获取呼叫数据
        /// </summary>
        /// <param name="data"></param>
        [OperationContract(IsOneWay = true)]
        void GetCallData(XElement data);
    }
}
