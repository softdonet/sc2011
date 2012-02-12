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
                this.treeView1.ItemsSource = value;
            }
        }



        public DeviceTreeNode SelectValue
        {
            get { return (DeviceTreeNode)GetValue(SelectValueProperty); }
            set { SetValue(SelectValueProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SelectValue.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectValueProperty =
            DependencyProperty.Register("SelectValue", typeof(DeviceTreeNode), typeof(ComboBoxTreeView),new PropertyMetadata(null));

        

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
