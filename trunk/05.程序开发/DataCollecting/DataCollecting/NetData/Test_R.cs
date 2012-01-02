using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataCollecting.Helper;

namespace DataCollecting.NetData
{
    /// <summary>
    /// 设备测试解包
    /// </summary>
    public class Test_R : MessageBase
    {
        public Test_R()
        {
        }

        public Test_R(byte[] data)
            : base(data)
        {
            content = BitConverter.ToUInt16(data, 21);
        }

        /// <summary>
        /// 报文内容
        /// </summary>
        private ushort content;
        public ushort Content
        {
            get { return content; }
            set { content = value; }
        }

        /// <summary>
        /// 转化为字节数组
        /// </summary>
        /// <returns></returns>
        public byte[] ToByte()
        {
            List<byte> result = new List<byte>();
            //压入头
            result.AddRange(Header.ToByte());
            //压入数据内容
            result.AddRange(BitConverter.GetBytes(content));
            //压入总长度
            result.InsertRange(5, BitConverter.GetBytes((ushort)(result.Count + 4)));
            //压入校验位
            result.AddRange(BitConverter.GetBytes(CRC16Helper.CalculateCrc16(result.ToArray())));
            return result.ToArray();
        }
    }
}
