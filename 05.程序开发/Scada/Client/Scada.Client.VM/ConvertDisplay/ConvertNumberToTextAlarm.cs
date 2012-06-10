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
using Scada.Client.VM.Modules.Alarm;
using System.Globalization;
using Scada.Model.Entity;
using Scada.Utility.Common.SL.Enums;
using Scada.Model.Entity.Enums;

namespace Scada.Client.VM.ConvertDisplay
{
    /// <summary>
    /// 告警事件
    /// 将数字改成对应的字符
    /// </summary>
    public class ConvertNumberToTextAlarm : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string currentText = string.Empty;
            DeviceAlarm currentValue = value as DeviceAlarm;
            if (currentValue == null)
            {
                return DependencyProperty.UnsetValue;
            }
            //DeviceAlarm item = currentValue;
            switch (parameter.ToString().ToLower())
            {
                case "eventtype":
                    if (currentValue.EventType.HasValue)
                    {
                        return EnumHelper.Display<EventTypes>(currentValue.EventType.Value);
                    }
                    break;
                case "eventLevel":
                    break;
                default:
                    return "未知";
                    //break;
            }

            return currentText;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

    }
}
