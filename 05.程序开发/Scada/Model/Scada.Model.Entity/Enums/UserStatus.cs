using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Runtime.Serialization;

namespace Scada.Model.Entity.Enums
{
    public enum UserStatus
    {
        /// <summary>
        /// 启用
        /// </summary>
        [EnumMember(Value = "启用")]
        Used = 1,

        /// <summary>
        /// 锁定
        /// </summary>
        [EnumMember(Value = "锁定")]
        Locked = 2
    }
}
