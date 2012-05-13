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
        /// <summary>
        ///获取实时数据
        /// </summary>
        [OperationContract(IsOneWay = true)]
        void GetRealTimeDataList();
        /// <summary>
        /// 获取告警数据
        /// </summary>
        [OperationContract(IsOneWay = true)]
        void GetAlarmDataList();
        /// <summary>
        /// 获取用户事件
        /// </summary>
        [OperationContract(IsOneWay = true)]
        void GetUserEventDataList();
    }

    [ServiceContract]
    public interface IDeviceRealTimeServiceCallback
    {
        /// <summary>
        /// 获取实时数据
        /// </summary>
        /// <param name="data"></param>
        [OperationContract(IsOneWay = true)]
        void GetRealTimeData(string data);

        /// <summary>
        /// 获取告警数据
        /// </summary>
        /// <param name="data"></param>
        [OperationContract(IsOneWay = true)]
        void GetAlarmData(string data);

        /// <summary>
        /// 获取用户数据
        /// </summary>
        /// <param name="data"></param>
        [OperationContract(IsOneWay = true)]
        void GetUserEventData(string data);
    }
}
