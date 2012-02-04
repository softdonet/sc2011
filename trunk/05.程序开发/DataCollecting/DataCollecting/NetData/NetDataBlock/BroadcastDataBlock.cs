using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataCollecting.Helper;

namespace DataCollecting.NetData
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
        /// 广播内容
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
            byte[] arr = System.Text.Encoding.Default.GetBytes(Msg);
            if (arr.Length <= 100)
            {
                result.AddRange(arr);
                //补零
                for (int j = 0; j < 100 - arr.Length; j++)
                {
                    result.Add(0x00);
                }
            }
            else
            {
                //大于100直接截断
                byte[] temp = new byte[100];
                Array.Copy(arr, 0, temp, 0, 50);
                result.AddRange(temp);
            }
            return result.ToArray();
        }
    }
}
