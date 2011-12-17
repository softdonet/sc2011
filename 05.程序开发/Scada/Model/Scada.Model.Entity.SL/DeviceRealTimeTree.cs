using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Collections.Generic;

namespace Scada.Model.Entity.SL
{
    /// <summary>
    /// 设备实时信息实体
    /// </summary>
    public class DeviceRealTimeTree
    {
        public string NodeValue { get; set; }

        public Guid NodeKey { get; set; }

        public Int32 NodeType { get; set; }

        public List<DeviceRealTimeTree> NodeChild { get; set; }

        public void AddNodeKey(DeviceRealTimeTree treeNode)
        {
            if (this.NodeChild == null)
                this.NodeChild = new List<DeviceRealTimeTree>();
            this.NodeChild.Add(treeNode);
        }
        public string InstallPlace { get; set; }
        public decimal? Temperature { get; set; }
        public int? Electricity { get; set; }
        public int? Signal { get; set; }
        public int? Status { get; set; }
        public DateTime? UpdateTime { get; set; }
    }
}
