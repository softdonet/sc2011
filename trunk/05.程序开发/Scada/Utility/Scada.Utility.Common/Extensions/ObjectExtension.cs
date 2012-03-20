using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace Scada.Utility.Common.Extensions
{
    public static class ObjectExtension
    {
        /// <summary>
        /// 根据表达式获取值
        /// </summary>
        /// <param name="obj">对象</param>
        /// <param name="expression">表达式</param>
        /// <returns></returns>
        public static object GetExpressionValue(this object value, string expression)
        {
            if (String.IsNullOrEmpty(expression))
            {
                return null;
            }
            string[] parts = expression.Split('.');
            foreach (string str in parts)
            {
                value = GetPropertyValue(value, str);
                if (value == null)
                {
                    break;
                }
            }
            return value;
        }
        /// <summary>
        /// 根据对象，对象属性名获取值
        /// </summary>
        /// <param name="obj">对象</param>
        /// <param name="propertyName">属性名</param>
        /// <returns></returns>
        public static object GetPropertyValue(this object obj, string propertyName)
        {
            if (obj != null && !String.IsNullOrEmpty(propertyName))
            {
                PropertyInfo info = obj.GetType().GetProperty(propertyName);
                return GetPropertyValue(obj, info);
            }
            return null;
        }
        public static object GetPropertyValue(this object obj, PropertyInfo pi)
        {
            if (obj != null && pi != null)
            {
                object value = pi.GetValue(obj, null);
                return value;
            }
            return null;
        }
        /// <summary>
        /// 为对象的指定公共属性赋值
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="propertyName"></param>
        /// <param name="value"></param>
        public static void SetPropertyValue(this object obj, string propertyName, object value)
        {
            PropertyInfo info = obj.GetType().GetProperty(propertyName);
            if (info != null)
            {
                try
                {
                    info.SetValue(obj, value, null);
                }
                catch (Exception) { }
            }
        }
        /// <summary>
        /// 将对象转换为某种类型的对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static T ConvertTo<T>(this object obj)
            where T : new()
        {
            T t = new T();
            foreach (PropertyInfo p in obj.GetType().GetProperties())
            {
                t.SetPropertyValue(p.Name, obj.GetPropertyValue(p.Name));
            }
            return t;
        }

        public static T ConvertTo<T>(this object obj, Action<T> func)
            where T : new()
        {
            T t = ConvertTo<T>(obj);
            func(t);
            return t;
        }
    }
}