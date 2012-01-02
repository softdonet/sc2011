using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataCollecting.Helper;

namespace DataCollecting.NetData
{
    /// <summary>
    /// 数据解码
    /// </summary>
    public class MessageBase
    {
        protected byte[] byteData = null;
        public MessageBase(byte[] data)
        {
            byteData = data;
            Header = new Head(byteData);
            VerifyData = BitConverter.ToUInt16(byteData, byteData.Length - 2);
        }
        public MessageBase()
        {
        }
        /// <summary>
        /// 数据报头
        /// </summary>
        public Head Header { get; set; }

        /// <summary>
        /// 校验内容
        /// </summary>
        public ushort VerifyData { get; set; }

        /// <summary>
        /// CRC16校验
        /// </summary>
        /// <returns></returns>
        public bool Verify()
        {
            byte[] arr = new byte[Header.CommandCount - 2];
            Array.Copy(byteData, 0, arr, 0, arr.Length);
            ushort ver = CRC16Helper.CalculateCrc16(arr);
            if (ver == VerifyData)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
