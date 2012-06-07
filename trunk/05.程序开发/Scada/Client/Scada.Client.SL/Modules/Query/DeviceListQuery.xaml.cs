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
using Scada.Client.VM;
using Scada.Client.VM.Modules.Query;
using Telerik.Windows.Controls;
using System.IO;

namespace Scada.Client.SL.Modules.Query
{
    public partial class DeviceListQuery : UserControl
    {
        private static DeviceListQuery instance;
        public static DeviceListQuery GetInstance()
        {
            if (instance == null)
            {
                instance = new DeviceListQuery();
            }
            return instance;
        }

        DeviceListQueryViewModel dlqvm;
        public DeviceListQuery()
        {
            InitializeComponent();

            dlqvm = new DeviceListQueryViewModel();
            this.DataContext = dlqvm;
            dlqvm.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(dlqvm_PropertyChanged);
        }

        void dlqvm_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "DeviceTreeSource")
            {
                this.comboBoxTreeView1.Source = dlqvm.DeviceTreeSource;
            }
        }

        private void btnExport_Click(object sender, RoutedEventArgs e)
        {
            this.RadGridView1.Columns[4].IsVisible = true;
            this.RadGridView1.Columns[5].IsVisible = true;
            this.RadGridView1.Columns[6].IsVisible = false;
            this.RadGridView1.Columns[7].IsVisible = false;

            string extension = "xls"; ;
            ExportFormat format = ExportFormat.Html;
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.DefaultExt = extension;
            dialog.Filter = String.Format("{1} files (*.{0})|*.{0}|All files (*.*)|*.*", extension, "Excel");
            dialog.FilterIndex = 1;
            if (dialog.ShowDialog() == true)
            {
                using (Stream stream = dialog.OpenFile())
                {
                    GridViewExportOptions exportOptions = new GridViewExportOptions();
                    exportOptions.Format = format;
                    exportOptions.ShowColumnFooters = false;
                    exportOptions.ShowColumnHeaders = true;
                    exportOptions.ShowGroupFooters = true;
                    RadGridView1.Export(stream, exportOptions);
                }
            }
            this.RadGridView1.Columns[4].IsVisible = false;
            this.RadGridView1.Columns[5].IsVisible = false;
            this.RadGridView1.Columns[6].IsVisible = true;
            this.RadGridView1.Columns[7].IsVisible = true;
        }

        private void RadGridView1_Exporting(object sender, GridViewExportEventArgs e)
        {
            if (e.Element == ExportElement.HeaderRow)
            {
                e.FontWeight = FontWeights.Bold;
                e.TextAlignment = TextAlignment.Center;
                e.Background = Colors.Gray;
            }
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
                        img = "electric0.png";
                    }
                    break;
                case "signal":
                    if (currentValue.Signal.HasValue)
                    {
                        if (currentValue.Signal.Value < 50)
                        {
                            img = "signal1.png";
                        }
                        else if (currentValue.Signal.Value >= 50 && currentValue.Signal.Value < 200)
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
                        img = "signal1.png";
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

