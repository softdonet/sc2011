using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Scada.Model.Entity
{
   public partial class DeviceAlarm
    {
        public string EventTypeName { get; set; }
        public string DealPeopleLoginID { get; set; }
        public string DealPeopleLoginName { get; set; }
    }
}
