using System;
using System.Collections.Generic;


namespace Scada.Model.Entity
{

    /// <summary>
    /// 图表数据源
    /// </summary>
    public class ChartSource
    {
        //设备标识
        public Guid DeviceID { get; set; }
        //设备更新日期
        public DateTime DeviceDate { get; set; }
        //平均温度
        public Double DeviceTemperature { get; set; }

    }
}
