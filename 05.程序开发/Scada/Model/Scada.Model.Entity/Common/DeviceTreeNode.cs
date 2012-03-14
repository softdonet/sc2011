using System;
using System.Collections.Generic;

namespace Scada.Model.Entity
{
    public class DeviceTreeNode
    {
        public string NodeValue { get; set; }

        public Guid NodeKey { get; set; }

        public Int32 NodeType { get; set; }

        public List<DeviceTreeNode> NodeChild { get; set; }

        public void AddNodeKey(DeviceTreeNode treeNode)
        {
            if (this.NodeChild == null)
                this.NodeChild = new List<DeviceTreeNode>();
            this.NodeChild.Add(treeNode);
        }

        public float? Longitude { get; set; }

        public float? Latitude { get; set; }
        public string InstallPlace { get; set; }

    }

}
