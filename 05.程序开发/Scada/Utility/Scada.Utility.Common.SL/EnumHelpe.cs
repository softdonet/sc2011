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
using System.Runtime.Serialization;

namespace Scada.Utility.Common.SL
{
    public static class EnumHelper
    {
        public static string Display<T>(object enumObj)
            where T : struct
        {
            Type enumType = typeof(T);
            return Display(enumType, enumObj);
        }
        public static string Display(Type enumType, object enumObj)
        {
            if (!enumType.IsEnum)
            {
                throw new Exception("enumType必须是枚举类型！");
            }
            string str = Enum.GetName(enumType, enumObj);
            EnumMemberAttribute customAttribute = (EnumMemberAttribute)Attribute.GetCustomAttribute(enumType.GetField(str), typeof(EnumMemberAttribute));
            return (customAttribute == null) ? str : customAttribute.Value;
        }
        public static string Display(this Enum enumObj)
        {
            return Display(enumObj.GetType(), enumObj);
        }
    }

}
