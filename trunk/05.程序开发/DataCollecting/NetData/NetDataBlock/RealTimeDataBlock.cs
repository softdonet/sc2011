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
            decimal ratio = 100;
            //块序号
            BlockNo = data[0];
            //时间戳
            SateTimeMark = StringHelper.ByteToDateTime(data, 1, 7);
            //温度1
            Temperature1 = BitConverter.ToUInt16(data, 8) / ratio;
            //温度2
            Temperature2 = BitConverter.ToUInt16(data, 10) / ratio;
            //温度3
            Temperature3 = BitConverter.ToUInt16(data, 12) / ratio;
            //温度4
            Temperature4 = BitConverter.ToUInt16(data, 14) / ratio;
            //温度5
            Temperature5 = BitConverter.ToUInt16(data, 16) / ratio;
            //湿度
            Humidity = BitConverter.ToUInt16(data, 18) / ratio;
            //电量
            Electric = BitConverter.ToUInt16(data, 20) / ratio;
            //信号
            Signal = BitConverter.ToUInt16(data, 22) / ratio;
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
        /// 温度3
        /// </summary>
        public decimal? Temperature3 { get; set; }

        /// <summary>
        /// 温度4
        /// </summary>
        public decimal? Temperature4 { get; set; }

        /// <summary>
        /// 温度5
        /// </summary>
        public decimal? Temperature5 { get; set; }

        /// <summary>
        /// 湿度
        /// </summary>
        public decimal? Humidity { get; set; }

        /// <summary>
        /// 电量
        /// </summary>
        public decimal? Electric { get; set; }

        /// <summary>
        /// 信号
        /// </summary>
        public decimal? Signal { get; set; }

        public byte[] ToByte()
        {
            List<byte> result = new List<byte>();
            result.Add(BlockNo);
            result.AddRange(StringHelper.DateTimeToByte(SateTimeMark));
            result.AddRange(BitConverter.GetBytes((ushort)(Temperature1 * 100)));
            result.AddRange(BitConverter.GetBytes((ushort)(Temperature2 * 100)));
            result.AddRange(BitConverter.GetBytes((ushort)(Temperature3 * 100)));
            result.AddRange(BitConverter.GetBytes((ushort)(Temperature4 * 100)));
            result.AddRange(BitConverter.GetBytes((ushort)(Temperature5 * 100)));
            result.AddRange(BitConverter.GetBytes((ushort)(Humidity * 100)));
            result.AddRange(BitConverter.GetBytes((ushort)(Electric * 100)));
            result.AddRange(BitConverter.GetBytes((ushort)(Signal * 100)));
            //补零
            for (int j = 0; j < 48 - 24; j++)
            {
                result.Add(0x00);
            }
            return result.ToArray();
        }

    }
}
