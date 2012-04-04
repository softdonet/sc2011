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
        /// 实时温度
        /// </summary>
        public decimal? RealTimeTemperature { get; set; }

    }
}
