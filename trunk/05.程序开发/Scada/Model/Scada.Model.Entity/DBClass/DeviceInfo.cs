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
        public string DeviceNo { get; set; }
        public string DeviceSN { get; set; }
        public string HardType { get; set; }
        public DateTime? ProductDate { get; set; }
        public string DeviceMAC { get; set; }
        public string SIMNo { get; set; }
        public Guid ManageAreaID { get; set; }
        public Guid MaintenancePeopleID { get; set; }
        public string InstallPlace { get; set; }
        public decimal? Longitude { get; set; }
        public decimal? Latitude { get; set; }
        public decimal? High { get; set; }
        public string Comment { get; set; }
        public int? Windage { get; set; }
        public string HardwareVersion { get; set; }
        public string SoftWareVersion { get; set; }
        public int? LCDScreenDisplayType { get; set; }
        public bool? UrgencyBtnEnable { get; set; }
        public bool? InforBtnEnable { get; set; }
        public bool? Temperature1AlarmValid { get; set; }
        public decimal? Temperature1HighAlarm { get; set; }
        public decimal? Temperature1LowAlarm { get; set; }
        public bool? Temperature2AlarmValid { get; set; }
        public decimal? Temperature2HighAlarm { get; set; }
        public decimal? Temperature2LowAlarm { get; set; }
        public bool? HumidityAlarmValid { get; set; }
        public decimal? HumidityHighAlarm { get; set; }
        public decimal? HumidityLowAlarm { get; set; }
        public bool? SignalAlarmValid { get; set; }
        public int? SignalHighAlarm { get; set; }
        public int? SignalLowAlarm { get; set; }
        public bool? ElectricityAlarmValid { get; set; }
        public int? ElectricityHighAlarm { get; set; }
        public int? ElectricityLowAlarm { get; set; }
        public int? CurrentModel { get; set; }
        public int? RealTimeParam { get; set; }
        public int? FullTimeParam1 { get; set; }
        public int? FullTimeParam2 { get; set; }
        public int? OptimizeParam1 { get; set; }
        public int? OptimizeParam2 { get; set; }
        public int? OptimizeParam3 { get; set; }
    }
}

