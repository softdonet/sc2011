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
using System.Globalization;
using Scada.Model.Entity;
using Scada.Utility.Common.SL.Enums;
using Scada.Model.Entity.Enums;

namespace Scada.Client.VM.ConvertDisplay
{
    /// <summary>
    /// 用户事件
    /// 将数字改成字符
    /// 实体类（UserEvent）
    /// </summary>
    public class ConvertNumberToText : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string currentText = string.Empty;
            UserEventModel currentValue = value as UserEventModel;
            if (currentValue == null)
            {
                return DependencyProperty.UnsetValue;
            }
            switch (parameter.ToString().ToLower())
            {
                case "state":
                    if (currentValue.State.HasValue)
                    {
                        return EnumHelper.Display<UserDealState>(currentValue.State.Value);
                    }
                    break;
                case "eventtype":
                    if (currentValue.EventType.HasValue)
                    {
                        return EnumHelper.Display<UserEventType>(currentValue.EventType.Value);
                    }
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
