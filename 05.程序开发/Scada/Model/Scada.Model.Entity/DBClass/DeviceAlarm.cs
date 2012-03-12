using System;
using System.Collections.Generic;


namespace Scada.Model.Entity
{
    /// <summary>
    ///Éè±¸¸æ¾¯
    /// </summary>
    public class DeviceAlarm
    {
        public Guid ID { get; set; }
        public Guid DeviceID { get; set; }
        public string DeviceNo { get; set; }
        public int? EventType { get; set; }
        public int? EventLevel { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? ConfirmTime { get; set; }
        public Guid DealPeopleID { get; set; }
        public int? DealStatus { get; set; }
        public string Comment { get; set; }
    }
}

