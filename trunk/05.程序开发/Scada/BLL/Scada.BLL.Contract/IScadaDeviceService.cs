using System;

namespace Scada.BLL.Contract
{
    public interface IScadaDeviceService
    {

        //测试流程
        int Add(int x, int y);

        //设备查询
        string GetListDeviceInfo(Guid DeviceID, Int32 DeviceType,
                                    DateTime? StartDate, DateTime? EndDate);


    }
}
