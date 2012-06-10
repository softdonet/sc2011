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
using Scada.Model.Entity.Enums;

namespace Scada.Client.SL.Modules.Device
{

    /// <summary>
    /// 设备列表
    /// </summary>
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
            MyContent.CloseBtn += new EventHandler(MyContent_CloseBtn);
            //注意：以下代码必须放在构造函数中
            DeviceListViewModel dlvm = new DeviceListViewModel();
            this.DataContext = dlvm;
            dlvm.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(dlvm_PropertyChanged);
        }

        #endregion

        #region 界面初期化

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

        #region 设备详情

        private void MyContent_CloseBtn(object sender, EventArgs e)
        {
            Storyboard2.Begin();
            ViewHost.Visibility = Visibility.Collapsed;
        }

        private void hlUrl_Click(object sender, RoutedEventArgs e)
        {
            HyperlinkButton hlB = sender as HyperlinkButton;
            Guid id = (hlB.DataContext as DeviceRealTimeTree).NodeKey;
            Storyboard1.Begin();
            MyContent.Content = new DetailsPage(id);
            MyContent.Title = "设备详细信息";
        }

        #endregion
    }

    /// <summary>
    /// 数字转换图片
    /// </summary>
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
                        //电量 0--400 //界面量程(0--4)
                        if (currentValue.Electricity.Value < 50)
                        {
                            img = "electric0.png";
                        }
                        else if (currentValue.Electricity.Value >= 50 && currentValue.Electricity.Value < 100)
                        {
                            img = "electric1.png";
                        }
                        else if (currentValue.Electricity.Value >= 100 && currentValue.Electricity.Value < 200)
                        {
                            img = "electric2.png";
                        }
                        else if (currentValue.Electricity.Value >= 200 && currentValue.Electricity.Value < 300)
                        {
                            img = "electric3.png";
                        }
                        else if (currentValue.Electricity.Value >= 300)
                        {
                            img = "electric4.png";
                        }
                    }
                    else
                    {
                        img = string.Empty;
                    }
                    break;
                case "signal":
                    if (currentValue.Signal.HasValue)
                    {
                        if (currentValue.Signal.Value < 50)
                        {
                            img = "signal0.png";
                        }
                        else if (currentValue.Signal.Value >= 50 && currentValue.Signal.Value < 100)
                        {
                            img = "signal1.png";
                        }
                        else if (currentValue.Signal.Value >= 100 && currentValue.Signal.Value < 200)
                        {
                            img = "signal2.png";
                        }
                        else if (currentValue.Signal.Value >= 200 && currentValue.Signal.Value < 300)
                        {
                            img = "signal3.png";
                        }
                        else if (currentValue.Signal.Value >= 300)
                        {
                            img = "signal4.png";
                        }
                    }
                    else
                    {
                        img = string.Empty;
                    }
                    break;
                case "status":
                    if (currentValue.Status.HasValue)
                    {
                        switch ((DeviceStates)currentValue.Status.Value)
                        {
                            case DeviceStates.Escape:
                                img = "grayer.png";
                                break;
                            case DeviceStates.Normal:
                                img = "green.png";
                                break;
                            case DeviceStates.Alert:
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



