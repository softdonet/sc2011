using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataCollecting.Helper
{
    public class StringHelper
    {
        /// <summary>
        /// 字节转化为字符
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string DataToStr(byte[] data)
        {
            string result = "";
            for (int i = 0; i < data.Length; i++)
            {
                string temp = Convert.ToString(data[i], 16);
                if (temp.Length == 1)
                {
                    temp = "0" + temp;
                }
                result = result + temp;
            }
            return result.ToUpper();
        }

        /// <summary>
        /// 字节转化为字符(带空格)
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string DataToStrV2(byte[] data)
        {
            string result = "";
            for (int i = 0; i < data.Length; i++)
            {
                string temp = Convert.ToString(data[i], 16);
                if (temp.Length == 1)
                {
                    temp = "0" + temp;
                }
                result = result + temp+" ";
            }
            return result.ToUpper();
        }

        /// <summary>
        /// 字节数组转化为时间
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static DateTime ByteToDateTime(byte[] data)
        {
            var obj= Convert.ToDateTime(string.Format("{0}-{1}-{2} {3}:{4}:{5}",
                         BitConverter.ToUInt16(data, 0),
                         data[2].ToString(),
                         data[3].ToString(),
                         data[4].ToString(),
                         data[5].ToString(),
                         data[6].ToString()));
            return obj;
        }
    }
}
