using System;
using System.ServiceModel;

namespace Scada.BLL.Contract
{

    [ServiceContract]
    public interface IScadaService
    {

        [OperationContract]
        int Add(int x, int y);

    }
}
