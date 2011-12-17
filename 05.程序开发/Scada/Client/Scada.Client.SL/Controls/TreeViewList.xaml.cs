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
using Scada.Client.SL.ScadaDeviceService;
using Scada.Client.SL.CommClass;
using Scada.Model.Entity.SL;
using System.Collections;

namespace Scada.Client.SL.Controls
{
    public partial class TreeViewList : UserControl
    {

        public TreeViewList()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 数据源
        /// </summary>
        public IEnumerable Source
        {
            get
            {
                return this.myTree.ItemsSource;
            }
            set
            {
                this.myTree.ItemsSource = value;
            }
        }


        //选择节点
        public delegate void TreeSelectItem(object sender, RoutedPropertyChangedEventArgs<object> e);
        public event TreeSelectItem OnTreeSelectItemClick;
        private void myTree_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (this.OnTreeSelectItemClick != null)
                this.OnTreeSelectItemClick(sender, e);
        }


    }
}
