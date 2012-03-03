using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Scada.Model.Entity.Enums
{
    public enum EventTypes
	{
        /// <summary>
        /// 超高
        /// </summary>
        [EnumMember(Value = "超高")]
        High = 1,
        /// <summary>
        /// 超低
        /// </summary>
        [EnumMember(Value = "超低")]
        Low = 2,
        /// <summary>
        /// 故障
        /// </summary>
        [EnumMember(Value = "故障")]
        Broken = 3
	}
}
