using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Utility;

namespace NetData
{
    /// <summary>
    /// 数据解包封包基类，完成报头检验等基础功能
    /// yanghk at 2012-1-3
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

        /// <summary>
        /// 构造报体字节数组
        /// </summary>
        /// <param name="result"></param>
        protected virtual void PushBodyByte(List<byte> result)
        { }

        /// <summary>
        /// 转化为字节数组
        /// </summary>
        /// <returns></returns>
        public byte[] ToByte()
        {
            List<byte> result = new List<byte>();
            //压入头
            result.AddRange(Header.ToByte());
            //构造报体
            PushBodyByte(result);
            //压入总长度
            result.InsertRange(5, BitConverter.GetBytes((ushort)(result.Count + 4)));
            //压入校验位
            result.AddRange(BitConverter.GetBytes(CRC16Helper.CalculateCrc16(result.ToArray())));
            return result.ToArray();
        }
    }
}
