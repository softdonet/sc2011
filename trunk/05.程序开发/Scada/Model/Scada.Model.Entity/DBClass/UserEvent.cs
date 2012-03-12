using System;
using System.Collections.Generic;

namespace Scada.Model.Entity
{
    /// <summary>
    /// 用户事件
    /// </summary>
    public class UserEvent
    {
        public Guid ID { get; set; }
        public string EventNo { get; set; }
        public Guid DeviceID { get; set; }
        public string DeviceNo { get; set; }
        public int? State { get; set; }
        public int? Count { get; set; }
        public DateTime? RequestTime { get; set; }
        public int? EventType { get; set; }

        /// <summary>
        /// 后续添加字段，拼接详细表中处理的内容
        /// </summary>
        public string DealInfo { get; set; }
    }
}
