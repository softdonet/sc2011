using System;
using System.Collections.Generic;


namespace Scada.Model.Entity
{
    /// <summary>
    /// 用户事件处理详情
    /// </summary>
    public class UserEventDealDetail
    {
        public Guid ID { get; set; }
        public Guid EventID { get; set; }
        public int? StepNo { get; set; }
        public string StepName { get; set; }
        public string Memo { get; set; }
        public Guid Operator { get; set; }
        public DateTime? DealTime { get; set; }
    }
}
