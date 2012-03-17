using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Utility;

namespace NetData
{
    /// <summary>
    /// 实时数据块实体
    /// </summary>
    public class RealTimeDataBlock
    {
        public RealTimeDataBlock()
        {

        }

        public RealTimeDataBlock(byte[] data)
        {
            //放大比例
            decimal ratio = 10;
            //块序号
            BlockNo = data[0];
            //时间戳
            SateTimeMark = StringHelper.ByteToDateTime(data, 1, 7);
            //温度1
            Temperature1 = BitConverter.ToUInt16(data, 8) / ratio;
            //温度2
            Temperature2 = BitConverter.ToUInt16(data, 10) / ratio;
            //湿度
            Humidity = BitConverter.ToUInt16(data, 12) / ratio;
            //电量
            Electric = BitConverter.ToUInt16(data, 14) ;
            //信号
            Signal = BitConverter.ToUInt16(data, 16);
        }
        /// <summary>
        /// 数据序号
        /// </summary>
        public byte BlockNo { get; set; }

        /// <summary>
        /// 时间戳
        /// </summary>
        public DateTime SateTimeMark { get; set; }

        /// <summary>
        /// 温度1
        /// </summary>
        public decimal? Temperature1 { get; set; }

        /// <summary>
        /// 温度2
        /// </summary>
        public decimal? Temperature2 { get; set; }

        /// <summary>
        /// 湿度
        /// </summary>
        public decimal? Humidity { get; set; }

        /// <summary>
        /// 电量
        /// </summary>
        public  ushort?  Electric { get; set; }

        /// <summary>
        /// 信号
        /// </summary>
        public ushort? Signal { get; set; }

        public byte[] ToByte()
        {
            List<byte> result = new List<byte>();
            result.Add(BlockNo);
            result.AddRange(StringHelper.DateTimeToByte(SateTimeMark));
            result.AddRange(BitConverter.GetBytes((ushort)(Temperature1 * 10)));
            result.AddRange(BitConverter.GetBytes((ushort)(Temperature2 * 10)));
            result.AddRange(BitConverter.GetBytes((ushort)(Humidity * 10)));
            result.AddRange(BitConverter.GetBytes(Electric.Value));
            result.AddRange(BitConverter.GetBytes(Signal.Value ));
            //每个块大小 38个字节，有效数据18字节,剩余补零
            result.AddRange(StringHelper.GetEmptyByte(38 - 18));
            return result.ToArray();
        }
    }
}
