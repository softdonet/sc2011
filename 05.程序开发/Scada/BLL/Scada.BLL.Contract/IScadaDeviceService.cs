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

        //增加设备
        Boolean AddDeviceInfo(string deviceInfo);

        //修改设备
        Boolean UpdateDeviceInfo(string deviceInfo);

        //删除设备
        Boolean DeleteDeviceInfo(string deviceGuid);

        //列出维护人员信息
        string ListMaintenancePeople();

        //列出设备树形信息
        string ListDeviceTreeView();


    }
}
