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

        //列设备信息
        string ViewDeviceInfo(string deviceGuid);

        //列出维护人员信息
        string ListMaintenancePeople();

        //列出设备树形信息
        string ListDeviceTreeView();


        //设备告警信息
        string GetListDeviceAlarmInfo();
        Boolean UpdateDeviceAlarmInfo(Guid AlarmId, DateTime ConfirmTime, String Comment, String DealPeople);

        //用户事件
        string GetListUserEventInfo();
        string GetUserEventKeyInfo(Guid EventKey);
        Boolean UpdateUserEventInfo(string EventDetails);
        string GetListStepInfo();

        Boolean UpdateEventState(Guid EventID,int state);
        Boolean ProcessingStepNo(String EventDealDetail);


    }
}
