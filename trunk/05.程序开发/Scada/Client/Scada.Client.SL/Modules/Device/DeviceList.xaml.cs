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

using Scada.Model.Entity;

using Scada.Client.VM;
using Scada.Client.VM.Modules.Device;


using Scada.Client.SL.Controls;
using Scada.Client.SL.CommClass;
using Scada.Client.SL.DeviceRealTimeService;



namespace Scada.Client.SL.Modules.Device
{


    public partial class DeviceList : UserControl
    {

        #region 单例构造

        private static DeviceList instance;
        public static DeviceList GetInstance()
        {
            if (instance == null)
            {
                instance = new DeviceList();
            }
            return instance;
        }

        #endregion


        #region 构造函数

        public DeviceList()
        {
            InitializeComponent();
        }

        #endregion


        #region 界面初期化

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            DeviceListViewModel dlvm = new DeviceListViewModel();
            this.DataContext = dlvm;
            dlvm.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(dlvm_PropertyChanged);

        }

        private void dlvm_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            try
            {
                this.RadTreeListView1.ExpandAllHierarchyItems();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void RadTreeListView1_RowLoaded(object sender, Telerik.Windows.Controls.GridView.RowLoadedEventArgs e)
        {
            DeviceRealTimeTree currentValue = e.DataElement as DeviceRealTimeTree;
            var obj = e.Row.Cells[0].Content as FrameworkElement;
            var imgL = obj.FindName("lDevice") as Image;
            var imgP = obj.FindName("pDevice") as Image;
            if (obj != null && currentValue != null)
            {
                if (currentValue.NodeType == 3)
                {
                    imgL.Visibility = Visibility.Collapsed;
                }
                else
                {
                    imgP.Visibility = Visibility.Collapsed;
                }
            }
        }

        #endregion


        #region 废弃代码

        private void MyContent_CloseBtn(object sender, EventArgs e)
        {
            Storyboard2.Begin();
            ViewHost.Visibility = Visibility.Collapsed;
        }

        private void hlUrl_Click(object sender, RoutedEventArgs e)
        {
            Storyboard1.Begin();
        }

        #endregion


    }

    public class RelativeBlocksValueConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return "查看";
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
            string img = string.Empty;
            DeviceRealTimeTree currentValue = value as DeviceRealTimeTree;
            if (currentValue == null)
            {
                return DependencyProperty.UnsetValue;
            }
            switch (parameter.ToString().ToLower())
            {
                case "electricity":
                    if (currentValue.Electricity.HasValue)
                    {
                        switch (currentValue.Electricity.Value)
                        {
                            case 1:
                                img = "electric1.png";
                                break;
                            case 2:
                                img = "electric2.png";
                                break;
                            case 3:
                                img = "electric3.png";
                                break;
                            case 4:
                                img = "electric4.png";
                                break;
                        }
                        break;
                    }
                    break;
                case "signal":
                    if (currentValue.Signal.HasValue)
                    {
                        switch (currentValue.Signal.Value)
                        {
                            case 1:
                                img = "signal1.png";
                                break;
                            case 2:
                                img = "signal2.png";
                                break;
                            case 3:
                                img = "signal3.png";
                                break;
                            case 4:
                                img = "signal4.png";
                                break;
                        }
                        break;
                    }
                    break;
                case "status":
                    if (currentValue.Status.HasValue)
                    {
                        switch (currentValue.Status.Value)
                        {
                            case 1:
                                img = "grayer.png";
                                break;
                            case 2:
                                img = "green.png";
                                break;
                            case 3:
                                img = "red.png";
                                break;
                        }
                        break;
                    }
                    break;
            }
            string resourcePath = "/Scada.Client.SL;component/Images/" + img;
            Uri resourceUri = new Uri(resourcePath, UriKind.Relative);
            var obj = new BitmapImage(resourceUri);
            return obj;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

    }

}



