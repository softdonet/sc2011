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
    }
}
