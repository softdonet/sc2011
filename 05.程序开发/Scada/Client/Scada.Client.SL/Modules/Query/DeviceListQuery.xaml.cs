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
using System.Reflection;
using System.Windows.Media.Imaging;
using Scada.Client.SL.Modules.Device;
using Scada.Client.SL.ScadaDeviceService;
using Scada.Client.SL.CommClass;
using Scada.Model.Entity;

namespace Scada.Client.SL.Modules.Query
{
    public partial class DeviceListQuery : UserControl
    {
        public DeviceListQuery()
        {
            InitializeComponent();
            ScadaDeviceServiceSoapClient scadaDeviceServiceSoapClient = ServiceManager.GetScadaDeviceService();
            scadaDeviceServiceSoapClient.GetListDeviceInfoCompleted += new EventHandler<GetListDeviceInfoCompletedEventArgs>(scadaDeviceServiceSoapClient_GetListDeviceInfoCompleted);
            scadaDeviceServiceSoapClient.GetListDeviceInfoAsync(new Guid("E963C95E-09A9-4AB6-A0C6-40A7DFE97991"), 3, null, null);

           
        }

        void scadaDeviceServiceSoapClient_GetListDeviceInfoCompleted(object sender, GetListDeviceInfoCompletedEventArgs e)
        {
            this.RadGridView1.ItemsSource = BinaryObjTransfer.BinaryDeserialize<List<DeviceRealTime>>(e.Result);
            //treeView1.ItemsSource = BinaryObjTransfer.BinaryDeserialize<List<DeviceRealTime>>(e.Result);
            //treeView1.Items.Add("yanghongkang");
            //treeView1.Items.Add("yanghongkang1");

        }
    }
    /// <summary>
    /// 类型转化器
    /// </summary>
    public class ItemImageSourceValueConverter : IValueConverter
    {
        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string img = string.Empty;
            DeviceRealTime currentValue = value as DeviceRealTime;
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

        #endregion
    }
}

