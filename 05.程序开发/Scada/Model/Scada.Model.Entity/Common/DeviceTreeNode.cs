using System;
using System.Collections.Generic;

namespace Scada.Model.Entity
{
    public class DeviceTreeNode
    {
        public string NodeValue { get; set; }
        public Guid NodeKey { get; set; }
        public Int32 NodeType { get; set; }
        public DeviceTreeNode NodeParent { get; set; }
        public List<DeviceTreeNode> NodeChild { get; set; }

        public void AddNodeKey(DeviceTreeNode treeNode)
        {
            if (this.NodeChild == null)
                this.NodeChild = new List<DeviceTreeNode>();
            this.NodeChild.Add(treeNode);
        }

        //经度
        public float? Longitude { get; set; }
        //纬度
        public float? Latitude { get; set; }
        //安装地点
        public string InstallPlace { get; set; }
        //高度
        public float? High { get; set; }
        //维护人员
       public string  MaintenanceName { get; set; }
        //联系方式
        public string Mobile { get; set; }
        //备注
        public string Comment { get; set; }
    }

}
