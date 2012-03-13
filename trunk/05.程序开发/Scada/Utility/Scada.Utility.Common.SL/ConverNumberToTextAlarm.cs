﻿using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Scada.Model.Entity;
using Scada.Model.Entity.Enums;
using System.Globalization;
using System.Windows.Data;

namespace Scada.Utility.Common.SL
{


    /// <summary>
    /// 告警事件
    /// 将数字改成对应的字符
    /// </summary>
    public class ConverNumberToTextAlarm : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string currentText = string.Empty;
            DeviceAlarm currentValue = value as DeviceAlarm;
            if (currentValue == null)
            {
                return DependencyProperty.UnsetValue;
            }
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
                    break;
            }
            return currentText;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
