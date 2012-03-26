using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Data;
using Scada.Model.Entity.Common;
using Scada.Model.Entity.Enums;

namespace Scada.Utility.Common.SL
{
    public class ConverNumberToDisplayType : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string currentText = string.Empty;
            KeyValue currentValue = value as KeyValue;
            if (currentValue==null)
            {
                return DependencyProperty.UnsetValue;
            }
            switch (parameter.ToString().ToLower())
            {
                case "1":
                case "2":
                case "3":
                    if (currentValue.cKey.HasValue)
                    {
                        return EnumHelper.Display<CurrentModel>(currentValue.cKey.Value);
                    }
                    break;
                default:
                    break;
            }
            return currentText;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
