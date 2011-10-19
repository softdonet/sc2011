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
using System.Windows.Data;
using System.Globalization;

namespace SCADA.UI.Modules.Device
{
    public partial class DeviceList : UserControl
    {
        public DeviceList()
        {
            InitializeComponent();
            MyContent.CloseBtn += new EventHandler(MyContent_CloseBtn);
        }
        void MyContent_CloseBtn(object sender, EventArgs e)
        {
            Storyboard2.Begin();
            ViewHost.Visibility = Visibility.Collapsed;
        }
        private void GridViewDataColumn_SortingStateChanged(object sender, Telerik.Windows.RadRoutedPropertyChangedEventArgs<Telerik.Windows.Controls.SortingState> e)
        {

        }

        private void RadGridView1_SelectionChanged(object sender, Telerik.Windows.Controls.SelectionChangeEventArgs e)
        {
            Storyboard1.Begin();
            MyContent.Content = new DetailsPage();
            MyContent.Title = "设备详细信息";
        }
    }


}
