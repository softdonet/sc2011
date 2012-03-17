using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Utility;

namespace NetData
{
    public class BroadcastDataBlock
    {
        public BroadcastDataBlock()
        {

        }

        public BroadcastDataBlock(byte[] data)
        {
            MsgLength = BitConverter.ToUInt16(data, 0);
            Msg = StringHelper.GetDefulatStringByByteArr(data, 2, MsgLength);
        }

        /// <summary>
        /// 广播内容长度
        /// </summary>
        public ushort MsgLength { get; set; }

        /// <summary>
        /// 广播内容
        /// </summary>
        public string Msg { get; set; }

        public byte[] ToByte()
        {
            List<byte> result = new List<byte>();
            result.AddRange(BitConverter.GetBytes(StringHelper.GetLength(Msg)));
            result.AddRange(StringHelper.GetCharactersByte(Msg, 50));
            return result.ToArray();
        }
    }
}
