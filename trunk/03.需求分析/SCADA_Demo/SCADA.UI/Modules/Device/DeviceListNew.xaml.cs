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
    public partial class DeviceListNew : UserControl
    {
        public DeviceListNew()
        {
            InitializeComponent();
            
        }
    }

    public class RelativeBlocksValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            CoverageItemBase currentValue = value as CoverageItemBase;
            if (value == null)
            {
                return DependencyProperty.UnsetValue;
            }
           

            bool showCovered = Int16.Parse(parameter.ToString()) > 0;

            int relativeValue = (showCovered) ? currentValue.BlocksCovered : currentValue.BlocksNotCovered;
            int totalValue = currentValue.BlocksCovered + currentValue.BlocksNotCovered;

            if (totalValue == 0) return "0 %";

            return String.Format("{0:f2} %", (double)relativeValue / totalValue * 100);
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
            return String.Format("Images/{0}.png", value.GetType().Name);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
