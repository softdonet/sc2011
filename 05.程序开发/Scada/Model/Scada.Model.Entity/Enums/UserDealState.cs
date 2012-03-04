using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Scada.Model.Entity.Enums
{
    public enum UserDealState
    {
        /// <summary>
        /// 未处理
        /// </summary>
        [EnumMember(Value="未处理")]
        WontDeal=1,
        /// <summary>
        /// 处理中
        /// </summary>
        [EnumMember(Value="处理中")]
        InDeal=2,
        /// <summary>
        /// 处理完毕
        /// </summary>
        [EnumMember(Value="处理完毕")]
        DealDone=3
    }
}
