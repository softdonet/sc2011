using System;

namespace Scada.BLL.Contract
{

    public interface IScadaDeviceService
    {


        #region 测试方法

        //测试流程
        int Add(int x, int y);

        #endregion


        #region 设备查询

        //设备查询
        string GetListDeviceInfo(Guid DeviceID, Int32 DeviceType,
                                    DateTime? StartDate, DateTime? EndDate);

        //设备当天温度
        string GetDeviceOnlyDay(String DeviceID);

        //设备历史温度
        string GetDeviceOnlyMonth(String DeviceID);

        #endregion


        #region 设备管理

        //增加设备
        Boolean AddDeviceInfo(string deviceInfo);

        //修改设备
        Boolean UpdateDeviceInfo(string deviceInfo);

        //删除设备
        Boolean DeleteDeviceInfo(Guid deviceGuid);

        //列设备信息
        string ViewDeviceInfo(string deviceGuid);

        //列出维护人员信息
        string ListMaintenancePeople();

        //列出设备树形信息
        string ListDeviceTreeView();

        #endregion


        #region 设备告警

        ////列出告警信息
        //string GetListDeviceAlarmInfo();
        ////更秘诀告警状态
        Boolean UpdateDeviceAlarmInfo(Guid AlarmId, DateTime ConfirmTime,
                                            String Comment, Guid DealPeopleId);

        #endregion


        #region 用户事件

        //string GetListUserEventInfo();
        string GetUserEventDetailInfo(Guid EventKey);
        Boolean UpdateUserEventInfo(string EventDetails);
        string GetListStepInfo();

        Boolean UpdateEventState(Guid EventID, int state);
        Boolean ProcessingStepNo(String EventDealDetail);

        #endregion


        #region 图表分析

        //1)获取树型设备(combox)
        string GetDeviceTreeList();

        //2同设备不同时间段获取温度值
        string GetSameDeviceTemperatureDiffDate(Int32 DeviceType, Guid DeviceID, Int32 DateSelMode, string DateList);

        //3)同时间不同设备获取温度值
        string GetSameDateTemperatureDiffDevice(Int32 dataSelMode, DateTime starDate, DateTime endDate, String deviceInfo);

        #endregion


    }
}
