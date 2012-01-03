using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataCollecting.Helper;

namespace DataCollecting.Common
{
    /// <summary>
    /// 实时数据块实体
    /// </summary>
    public class RealTimeDataBlock
    {
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
