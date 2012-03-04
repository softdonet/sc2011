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
using Scada.Model.Entity;
using System.Globalization;
using Scada.Model.Entity.Enums;


namespace Scada.Utility.Common.SL
{

    /// <summary>
    /// 将数字改成字符
    /// 用户事件：实体类（UserEventTab）
    /// </summary>
    public class ConvertNumberToText : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string currentText = string.Empty;
            UserEventTab currentValue = value as UserEventTab;
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

