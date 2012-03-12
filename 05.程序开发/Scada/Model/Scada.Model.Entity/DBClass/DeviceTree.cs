using System;
using System.Collections.Generic;
namespace Scada.Model.Entity
{
    /// <summary>
    /// 设备管理分区结构
    /// </summary>
    public class DeviceTree
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public Guid ParentID { get; set; }
        public Guid AdminID { get; set; }
    }
}

