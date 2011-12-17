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
using System.Reflection;
using System.Windows.Media.Imaging;
using System.Windows.Data;
using System.Globalization;
using Scada.Client.SL.Controls;

namespace Scada.Client.SL.Modules.Device
{
    public partial class DeviceList : UserControl
    {
        public DeviceList()
        {
            InitializeComponent();
        }

        void MyContent_CloseBtn(object sender, EventArgs e)
        {
            Storyboard2.Begin();
            ViewHost.Visibility = Visibility.Collapsed;
        }
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            this.RadTreeListView1.ExpandAllHierarchyItems();
        }

        private void RadTreeListView1_RowLoaded(object sender, Telerik.Windows.Controls.GridView.RowLoadedEventArgs e)
        {
            var obj = e.Row.Cells[0].Content as FrameworkElement;
            var lbl = obj.FindName("lblDevName") as Label;
            var imgL = obj.FindName("lDevice") as Image;
            var imgP = obj.FindName("pDevice") as Image;
            var hlUrl = obj.FindName("hlUrl") as Button;
            if (lbl != null)
            {
                if (lbl.Content.ToString().Trim().IndexOf("设备") > -1)
                {
                    imgL.Visibility = Visibility.Collapsed;
                }
                else
                {
                    imgP.Visibility = Visibility.Collapsed;
                }
            }
        }


        private void hlUrl_Click(object sender, RoutedEventArgs e)
        {
            Storyboard1.Begin();
            //MyContent.Content = new DetailsPage();
            //MyContent.Title = "设备详细信息";
        }
    }

    public class RelativeBlocksValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return "查看";

            //CoverageItemBase currentValue = value as CoverageItemBase;
            //if (value == null)
            //{
            //    return DependencyProperty.UnsetValue;
            //}
            //return currentValue.BlocksCovered;

            //bool showCovered = Int16.Parse(parameter.ToString()) > 0;

            //int relativeValue = (showCovered) ? currentValue.BlocksCovered : currentValue.BlocksNotCovered;
            //int totalValue = currentValue.BlocksCovered + currentValue.BlocksNotCovered;

            //if (totalValue == 0) return "0 %";

            //return String.Format("{0:f2} %", (double)relativeValue / totalValue * 100);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {

            return DependencyProperty.UnsetValue;
        }
    }

    public class ItemImageSourceValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int type = System.Convert.ToInt32(parameter);
            Random rd = new Random();

            int i = rd.Next(1, 10);
            string imgState = string.Empty;
            string imgElectric = string.Empty;
            string imgSignal = string.Empty;
            string img = string.Empty;
            switch (i)
            {
                case 1:
                case 2:
                case 3:
                case 4:
                case 8:
                case 9:
                case 10:
                    switch (type)
                    {
                        case 1:
                            img = "green.png";
                            break;
                        case 2:
                            img = "electric4.png";
                            break;
                        case 3:
                            img = "signal3.png";
                            break;
                    }

                    break;
                case 5:
                    switch (type)
                    {
                        case 1:
                            img = "red.png";
                            break;
                        case 2:
                            img = "electric3.png";
                            break;
                        case 3:
                            img = "signal2.png";
                            break;
                    }
                    break;
                case 6:
                case 7:
                    switch (type)
                    {
                        case 1:
                            img = "grayer.png";
                            break;
                        case 2:
                            img = "electric1.png";
                            break;
                        case 3:
                            img = "signal1.png";
                            break;
                    }

                    break;

            }
            //AssemblyName assemblyName = new AssemblyName(typeof(RadTreeListXmlDataSource).Assembly.FullName);
            //string resourcePath = "/" + assemblyName.Name + ";component/Modules/Device/Image/" + img;
            //Uri resourceUri = new Uri(resourcePath, UriKind.Relative);
            //var obj = new BitmapImage(resourceUri);
            Image obj = null;
            return obj;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

}
