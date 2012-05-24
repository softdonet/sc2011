using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Scada.Model.Entity
{
    public partial class DeviceInfo
    {
        /// <summary>
        /// 扩展维修人员姓名
        /// </summary>
        public string MaintenancePeopleName { get; set; }
        public string ManageAreaName { get; set; }
        public string LCDScreenDisplayTypeName { get; set; }
        public string CurrentModelName { get; set; }

        /// <summary>
        /// 整点模式参数1扩展名称
        /// </summary>
        public string FullTimeParam1Name { get; set; }
        /// <summary>
        /// 整点模式参数2扩展名称
        /// </summary>
        public string FullTimeParam2Name { get; set; }
        /// <summary>
        /// 逢变则报模式参数1扩展名
        /// </summary>
        public string OptimizeParam1Name { get; set; }
        /// <summary>
        /// 逢变则报模式参数2扩展名
        /// </summary>
        public string OptimizeParam2Name { get; set; }
        /// <summary>
        /// 实时温度
        /// </summary>
        public decimal? RealTimeTemperature { get; set; }

    }
}
