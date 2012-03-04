using System;
using System.Collections.Generic;


namespace Scada.Model.Entity
{


    public class EventDealDetail
    {

        public Guid DetailID { get; set; }

        public Guid EventID { get; set; }

        public Guid OperatorId { get; set; }

        public Int32 StepNo { get; set; }

        public String StepName { get; set; }

        public String Memo { get; set; }

        public DateTime? DealTime { get; set; }

    }

}
