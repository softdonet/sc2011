using System;
using System.Collections.Generic;
namespace Scada.Model.Entity
{
    /// <summary>
    /// �豸ά����Ա
    /// </summary>
    public class MaintenancePeople
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Telephone { get; set; }
        public string Mobile { get; set; }
        public string QQ { get; set; }
        public string MSN { get; set; }
        public string Email { get; set; }
        public byte[] HeadImage { get; set; }
    }
}

