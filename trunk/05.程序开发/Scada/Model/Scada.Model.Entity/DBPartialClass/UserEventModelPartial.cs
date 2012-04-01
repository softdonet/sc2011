using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Scada.Model.Entity
{
    public partial class UserEventModel
    {
        public string EventTypeName { get; set; }
        public string Step1 { get; set; }
        public string Step2 { get; set; }
        public string Step3 { get; set; }
        public string Step4 { get; set; }
        public string Step5 { get; set; }
    }
}
