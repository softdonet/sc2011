using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Scada.Model.Entity.Enums
{
    public enum UserEventType
    {
        /// <summary>
        /// 紧急报修
        /// </summary>
        [EnumMember(Value="紧急报修")]
        HurryRepair=1,
        /// <summary>
        /// 请求测温
        /// </summary>
        [EnumMember(Value="请求测温")]
        RequestTestTemp=2,

        /// <summary>
        /// 获取公告
        /// </summary>
        [EnumMember(Value="获取公告")]
        GetAffiche=3
    }
}
