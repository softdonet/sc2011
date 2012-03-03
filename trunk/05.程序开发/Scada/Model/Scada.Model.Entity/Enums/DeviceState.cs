using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Scada.Model.Entity.Enums
{
    /// <summary>
    /// 设备状态
    /// </summary>
    public enum DeviceState
    {
        /// <summary>
        /// 设备正常
        /// </summary>
        Normal = 1,
        /// <summary>
        /// 设备离线
        /// </summary>
        Escape = 2,
        /// <summary>
        /// 设备告警
        /// </summary>
        Alert = 3
    }
}
