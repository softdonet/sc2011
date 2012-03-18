using System;
using System.Collections.Generic;

namespace Scada.Model.Entity
{
    /// <summary>
    /// 设备信息
    /// </summary>
    public class DeviceInfo
    {
        public Guid ID { get; set; }
        //设备编号
        public string DeviceNo { get; set; }
        //设备SN号
        public string DeviceSN { get; set; }
        //硬件类型
        public string HardType { get; set; }
        //生产日期
        public DateTime? ProductDate { get; set; }
        public string DeviceMAC { get; set; }
        public string SIMNo { get; set; }
        //管理分区
        public Guid ManageAreaID { get; set; }
        //维修人员ID
        public Guid MaintenancePeopleID { get; set; }
        //安装地点
        public string InstallPlace { get; set; }
        //经度
        public decimal? Longitude { get; set; }
        //维度
        public decimal? Latitude { get; set; }
        public decimal? High { get; set; }
        public string Comment { get; set; }
        //偏差
        public int? Windage { get; set; }
        //硬件版本
        public string HardwareVersion { get; set; }
        //软件版本
        public string SoftWareVersion { get; set; }
        //LCD显示类型
        public int? LCDScreenDisplayType { get; set; }
        public bool? UrgencyBtnEnable { get; set; }
        public bool? InforBtnEnable { get; set; }
        //主温度设置有效
        public bool? Temperature1AlarmValid { get; set; }
        //主温度高报警
        public decimal? Temperature1HighAlarm { get; set; }
        //主温度低报警
        public decimal? Temperature1LowAlarm { get; set; }
        //从温度设置有效
        public bool? Temperature2AlarmValid { get; set; }
        //从温度高报警
        public decimal? Temperature2HighAlarm { get; set; }
        //从温度低报警
        public decimal? Temperature2LowAlarm { get; set; }
        //湿度设置有效
        public bool? HumidityAlarmValid { get; set; }
        //湿度高报警
        public decimal? HumidityHighAlarm { get; set; }
        //湿度低报警
        public decimal? HumidityLowAlarm { get; set; }
        //信号设置有效
        public bool? SignalAlarmValid { get; set; }
        public int? SignalHighAlarm { get; set; }
        public int? SignalLowAlarm { get; set; }
        //电量设置有效
        public bool? ElectricityAlarmValid { get; set; }
        public int? ElectricityHighAlarm { get; set; }
        public int? ElectricityLowAlarm { get; set; }
        //当前模式
        public int? CurrentModel { get; set; }
        //实时模式参数
        public int? RealTimeParam { get; set; }
        //整点模式参数1
        public int? FullTimeParam1 { get; set; }
        //整点模式参数2
        public int? FullTimeParam2 { get; set; }
        //逢变则报参数1
        public int? OptimizeParam1 { get; set; }
        //逢变则报参数1
        public int? OptimizeParam2 { get; set; }
        //逢变则报参数1
        public int? OptimizeParam3 { get; set; }
    }
}

