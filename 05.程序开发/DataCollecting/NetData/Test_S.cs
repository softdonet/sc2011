using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetData
{
    /// <summary>
    /// 设备测试封包
    /// </summary>
    public class Test_S : MessageBase
    {
        public Test_S()
        {
        }

        public Test_S(byte[] data)
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


        protected override void PushBodyByte(List<byte> result)
        {
            //压入数据内容
            result.AddRange(BitConverter.GetBytes(content));
        }
    }
}
