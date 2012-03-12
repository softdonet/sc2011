using System;
using System.Collections.Generic;


namespace Scada.Model.Entity
{
    /// <summary>
    /// 用户事件处理步骤
    /// </summary>
    public class StepInfo
    {
        public Guid ID { get; set; }
        public string StepName { get; set; }
    }
}
