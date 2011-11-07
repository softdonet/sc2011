using System;
using System.ServiceModel;


namespace Scada.Server.Contract
{

    /// <summary>
    /// 天津爱德信息有限公司
    /// </summary>
    [ServiceContract]
    public interface IScadaService
    {
        [OperationContract]
        int Add(int x, int y);
    }
}
