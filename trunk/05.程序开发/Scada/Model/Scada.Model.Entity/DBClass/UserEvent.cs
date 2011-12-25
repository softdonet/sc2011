using System;

namespace Scada.Model.Entity
{

    public class UserEvent
    {
        public Guid ID { get; set; }
        public String EventNo { get; set; }
        public Guid DeviceID { get; set; }
        public String DeviceNo { get; set; }
        public Int32? State { get; set; }
        public Int32? Count { get; set; }
        public DateTime? RequestTime { get; set; }

    }
}
