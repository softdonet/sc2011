using System;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Collections.Generic;


namespace Scada.Client.SL.CommClass
{
    /// <summary>
    /// 枚举帮助类
    /// yanghk at 2012-3-2
    /// </summary>

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

        public static T[] GetValues<T>()
        {

            Type enumType = typeof(T);
            if (!enumType.IsEnum)
            {
                throw new ArgumentException("Type '" + enumType.Name + "' is not an enum");
            }
            List<T> values = new List<T>();
            var fields = from field in enumType.GetFields()
                         where field.IsLiteral
                         select field;
            foreach (FieldInfo field in fields)
            {
                object value = field.GetValue(enumType);
                values.Add((T)value);
            }
            return values.ToArray();
        }

        public static object[] GetValues(Type enumType)
        {

            if (!enumType.IsEnum)
            {
                throw new ArgumentException("Type '" + enumType.Name + "' is not an enum");
            }

            List<object> values = new List<object>();
            var fields = from field in enumType.GetFields()
                         where field.IsLiteral
                         select field;
            foreach (FieldInfo field in fields)
            {
                object value = field.GetValue(enumType);
                values.Add(value);
            }
            return values.ToArray();

        }


    }
}

