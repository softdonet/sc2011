using System;
using System.Collections.Generic;
namespace Scada.Model.Entity
{
    /// <summary>
    /// �豸��������ṹ
    /// </summary>
    public class DeviceTree
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public Guid ParentID { get; set; }
        public Guid AdminID { get; set; }
    }
}

