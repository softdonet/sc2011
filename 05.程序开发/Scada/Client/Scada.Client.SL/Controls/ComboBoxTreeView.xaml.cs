using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Scada.Model.Entity;

namespace Scada.Client.SL.Controls
{
    public partial class ComboBoxTreeView : UserControl
    {
        public ComboBoxTreeView()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 数据源
        /// </summary>
        public List<DeviceTreeNode> Source
        {
            set
            {
                if (value != null)
                {
                    DeviceTreeNode test = new DeviceTreeNode();
                    test.NodeKey = Guid.Empty;
                    test.NodeValue = "清空选择";
                    value.Insert(0, test);
                }

                this.treeView1.ItemsSource = value;
            }
        }

        /// <summary>
        /// 选择项
        /// </summary>
        public DeviceTreeNode SelectValue { get; set; }

        private void treeView1_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            var obj = e.NewValue as DeviceTreeNode;
            if (treeView1.Items.Count > 0)
            {
                if (obj.NodeKey == Guid.Empty)
                {
                    tbTree.Text = string.Empty;
                    SelectValue = null;
                }
                else
                {
                    tbTree.Text = obj.NodeValue;
                    SelectValue = obj;
                }  
            }
        } 
    }
}
