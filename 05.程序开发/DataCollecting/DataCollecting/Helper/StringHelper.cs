﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataCollecting.Helper
{
    /// <summary>
    /// 字节字符串转化帮助类
    /// yanghk at 2012-1-3
    /// </summary>
    public class StringHelper
    {
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
        public static string DataToStr(byte[] data,int startIndex,int length)
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
            return result.ToUpper().TrimEnd('-') ;
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
            result.AddRange(BitConverter.GetBytes((ushort)DateTime.Now.Year));
            result.Add((byte)DateTime.Now.Month);
            result.Add((byte)DateTime.Now.Day);
            result.Add((byte)DateTime.Now.Hour);
            result.Add((byte)DateTime.Now.Minute);
            result.Add((byte)DateTime.Now.Second);
            return result.ToArray();
        }

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
                result.Add(Convert.ToByte(item, 16));
            }
            return result.ToArray();
        }

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
    }
}