using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Utility
{
    /// <summary>
    /// 字节字符串转化帮助类
    /// yanghk at 2012-1-3
    /// </summary>
    public class StringHelper
    {
        #region 字节转化为字符相关
        /// <summary>
        ///通讯数据转换核心方法
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static byte[] ConvertData(byte[] data)
        {
            List<byte> result = new List<byte>();
            for (int i = 0; i < data.Length / 2; i = i + 2)
            {
                string strHex1 = System.Text.Encoding.ASCII.GetString(data, i, 1);
                string strHex2 = System.Text.Encoding.ASCII.GetString(data, i + 1, 1);
                string strHex = strHex1 + strHex2;
                byte temp = Convert.ToByte(strHex, 16);
                result.Add(temp);
                bool falg = false;
                if (data[i + 2] == 0x00 || data[i + 2] == 13 || data[i + 2] == 10)
                {
                    falg = true;
                }
                if (falg)
                {
                    break;
                }
            }
            byte[] tempByte = result.ToArray();
            byte[] newByte = new byte[1024];
            Array.Copy(tempByte, 0, newByte, 0, tempByte.Length);
            return newByte;
        }

        /// <summary>
        /// 单字节转化为字符
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string DataToStr(byte data)
        {
            byte[] arr = new byte[1] { data };
            return DataToStr(arr);
        }

        /// <summary>
        /// 字节转化为字符(不带空格)
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
        /// 字节转化为字符(不带空格)
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string DataToStr(byte[] data, int startIndex, int length)
        {
            byte[] tempArr = new byte[length];
            Array.Copy(data, startIndex, tempArr, 0, length);

            string result = "";
            for (int i = 0; i < tempArr.Length; i++)
            {
                string temp = Convert.ToString(tempArr[i], 16);
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
                result = result + temp + " ";
            }
            return result.ToUpper().TrimEnd(' '); ;
        }

        /// <summary>
        /// 字节转化为字符(带空格)
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string DataToStrV2(byte[] data, int startIndex, int length)
        {
            byte[] tempArr = new byte[length];
            Array.Copy(data, startIndex, tempArr, 0, length);
            string result = "";
            for (int i = 0; i < tempArr.Length; i++)
            {
                string temp = Convert.ToString(tempArr[i], 16);
                if (temp.Length == 1)
                {
                    temp = "0" + temp;
                }
                result = result + temp + " ";
            }
            return result.ToUpper().TrimEnd(' '); ;
        }

        #endregion

        #region  MAC相关
        /// <summary>
        /// 字节转化为MAC
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string DataToMACStr(byte[] data, int startIndex, int length)
        {
            byte[] tempArr = new byte[length];
            Array.Copy(data, startIndex, tempArr, 0, length);

            string result = "";
            for (int i = 0; i < tempArr.Length; i++)
            {
                string temp = Convert.ToString(tempArr[i], 16);
                if (temp.Length == 1)
                {
                    temp = "0" + temp;
                }
                result = result + temp + "-";
            }
            return result.ToUpper().TrimEnd('-');
        }

        /// <summary>
        /// 字节转化为MAC
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string DataToMACStr(byte[] data)
        {
            string result = "";
            for (int i = 0; i < data.Length; i++)
            {
                string temp = Convert.ToString(data[i], 16);
                if (temp.Length == 1)
                {
                    temp = "0" + temp;
                }
                result = result + temp + "-";
            }
            return result.ToUpper().TrimEnd('-');
        }

        /// <summary>
        /// MAC转化为字节
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static byte[] MACStrData(string macStr)
        {
            List<byte> result = new List<byte>();
            string[] arr = macStr.Split('-');
            foreach (string item in arr)
            {
                result.Add(Convert.ToByte(item, 16));
            }
            return result.ToArray();
        }

        #endregion

        #region 时间相关

        /// <summary>
        /// 字节数组转化为时间
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static DateTime ByteToDateTime(byte[] data)
        {
            var obj = Convert.ToDateTime(string.Format("{0}-{1}-{2} {3}:{4}:{5}",
                         BitConverter.ToUInt16(data, 0),
                         data[2].ToString(),
                         data[3].ToString(),
                         data[4].ToString(),
                         data[5].ToString(),
                         data[6].ToString()));
            return obj;
        }

        /// <summary>
        /// 字节数组转化为时间
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static DateTime ByteToDateTime(byte[] data, int startIndex, int length)
        {
            byte[] tempArr = new byte[length];
            Array.Copy(data, startIndex, tempArr, 0, length);
            var obj = Convert.ToDateTime(string.Format("{0}-{1}-{2} {3}:{4}:{5}",
                         BitConverter.ToUInt16(tempArr, 0),
                         tempArr[2].ToString(),
                         tempArr[3].ToString(),
                         tempArr[4].ToString(),
                         tempArr[5].ToString(),
                         tempArr[6].ToString()));
            return obj;
        }

        /// <summary>
        /// 时间转化为字节数组
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static byte[] DateTimeToByte(DateTime dt)
        {
            List<byte> result = new List<byte>();
            result.AddRange(BitConverter.GetBytes((ushort)dt.Year));
            result.Add((byte)dt.Month);
            result.Add((byte)dt.Day);
            result.Add((byte)dt.Hour);
            result.Add((byte)dt.Minute);
            result.Add((byte)dt.Second);
            return result.ToArray();
        }

        #endregion

        #region IP相关

        /// <summary>
        /// 字节转化为IP
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string DataToIPStr(byte[] data)
        {
            string result = "";
            for (int i = 0; i < data.Length; i++)
            {
                string temp = Convert.ToString(data[i], 10);
                result = result + temp + ".";
            }
            return result.ToUpper().TrimEnd('.'); ;
        }

        /// <summary>
        /// 字节转化为IP
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string DataToIPStr(byte[] data, int startIndex, int length)
        {
            byte[] tempArr = new byte[length];
            Array.Copy(data, startIndex, tempArr, 0, length);
            string result = "";
            for (int i = 0; i < tempArr.Length; i++)
            {
                string temp = Convert.ToString(tempArr[i], 10);
                result = result + temp + ".";
            }
            return result.ToUpper().TrimEnd('.'); ;
        }

        /// <summary>
        /// IP转化字节
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static byte[] IPStrData(string IPStr)
        {
            List<byte> result = new List<byte>();
            string[] arr = IPStr.Split('.');
            foreach (string item in arr)
            {
                result.Add(byte.Parse(item));
            }
            return result.ToArray();
        }

        #endregion

        #region ASCII相关

        /// <summary>
        /// 字节转化为ASCII
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string GetASCIIStringByByteArr(byte[] data)
        {
            return System.Text.Encoding.ASCII.GetString(data);
        }

        /// <summary>
        /// 字节转化为ASCII
        /// </summary>
        /// <param name="data"></param>
        /// <param name="startIndex"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string GetASCIIStringByByteArr(byte[] data, int startIndex, int length)
        {
            byte[] tempArr = new byte[length];
            Array.Copy(data, startIndex, tempArr, 0, length);
            return System.Text.Encoding.ASCII.GetString(tempArr);
        }

        #endregion

        #region 汉字相关
        /// <summary>
        /// 字节转化为汉字
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string GetDefulatStringByByteArr(byte[] data)
        {
            return System.Text.Encoding.Default.GetString(data);
        }

        /// <summary>
        /// 字节转化为汉字
        /// </summary>
        /// <param name="data"></param>
        /// <param name="startIndex"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string GetDefulatStringByByteArr(byte[] data, int startIndex, int length)
        {
            byte[] tempArr = new byte[length];
            Array.Copy(data, startIndex, tempArr, 0, length);
            return System.Text.Encoding.Default.GetString(tempArr);
        }

        //获取汉字所占用的字节数
        public static ushort GetLength(string str)
        {
            return (ushort)System.Text.Encoding.Default.GetByteCount(str);
        }


        /// <summary>
        ///  获取汉字字节数组
        /// </summary>
        /// <param name="str"></param>
        /// <param name="length">总长度，不够补零，超出截断</param>
        /// <returns></returns>
        public static byte[] GetCharactersByte(string str, int length)
        {
            List<byte> result = new List<byte>();
            byte[] arr = System.Text.Encoding.Default.GetBytes(str);
            if (arr.Length <= length)
            {
                result.AddRange(arr);
                //补零
                for (int j = 0; j < length - arr.Length; j++)
                {
                    result.Add(0x00);
                }
            }
            else
            {
                //大于length直接截断
                byte[] temp = new byte[length];
                Array.Copy(arr, 0, temp, 0, length);
                result.AddRange(temp);
            }
            return result.ToArray();
        }
        #endregion

        #region 字节处理相关
        /// <summary>
        /// 获取空字节数组
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        public static byte[] GetEmptyByte(int length)
        {
            List<byte> result = new List<byte>();
            for (int j = 0; j < length; j++)
            {
                result.Add(0x00);
            }
            return result.ToArray();
        }

        #endregion
    }
}