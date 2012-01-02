using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataCollecting.Common;
using DataCollecting.Helper;

namespace DataCollecting.NetData
{
    /// <summary>
    /// 设备实时数据解包
    /// </summary>
    public class RealTimeData_R : MessageBase
    {
        public RealTimeData_R(byte[] data)
        {
            //取出头部
            this.Header = new Head(data);
            //每个块占字节数
            int blockSize = 48;
            //数据块数
            blockCount = Convert.ToInt32(data[21]);
            //数据块长度
            int blockLength = blockCount * 48;
            //取出数据块内容
            byte[] dataBlock = new byte[blockLength];
            Array.Copy(data, 22, dataBlock, 0, blockLength);
            //放大比例
            decimal ratio = 100;
            for (int i = 0; i < blockCount; i++)
            {
                RealTimeDataBlock realTimeDataBlock = new RealTimeDataBlock();
                //块序号
                realTimeDataBlock.BlockNo = dataBlock[i * blockSize];
                //时间戳
                byte[] datetime = new byte[7];
                Array.Copy(dataBlock, i * blockSize + 1, datetime, 0, 7);
                realTimeDataBlock.SateTimeMark = StringHelper.ByteToDateTime(datetime);
                //温度1
                realTimeDataBlock.Temperature1 = BitConverter.ToUInt16(dataBlock, i * blockSize + 8) / ratio;
                //温度2
                realTimeDataBlock.Temperature2 = BitConverter.ToUInt16(dataBlock, i * blockSize + 10) / ratio;
                //温度3
                realTimeDataBlock.Temperature3 = BitConverter.ToUInt16(dataBlock, i * blockSize + 12) / ratio;
                //温度4
                realTimeDataBlock.Temperature4 = BitConverter.ToUInt16(dataBlock, i * blockSize + 14) / ratio;
                //温度5
                realTimeDataBlock.Temperature5 = BitConverter.ToUInt16(dataBlock, i * blockSize + 16) / ratio;
                //湿度
                realTimeDataBlock.Humidity = BitConverter.ToUInt16(dataBlock, i * blockSize + 18) / ratio;
                //电量
                realTimeDataBlock.Electric = BitConverter.ToUInt16(dataBlock, i * blockSize + 20) / ratio;
                //信号
                realTimeDataBlock.Signal = BitConverter.ToUInt16(dataBlock, i * blockSize + 22) / ratio;
                //加载到集合
                realTimeDataBlocks.Add(realTimeDataBlock);
            }
        }

        public RealTimeData_R()
        {
        }
        /// <summary>
        /// 数据块个数
        /// </summary>
        int blockCount;
        public int BlockCount
        {
            get { return blockCount; }
        }

        /// <summary>
        /// 数据块集合
        /// </summary>
        List<RealTimeDataBlock> realTimeDataBlocks = new List<RealTimeDataBlock>();
        public List<RealTimeDataBlock> RealTimeDataBlocks
        {
            get { return realTimeDataBlocks; }
            set { realTimeDataBlocks = value; }
        }

        /// <summary>
        /// 转化为字节数组
        /// </summary>
        /// <returns></returns>
        public byte[] ToByte()
        {
            List<byte> result = new List<byte>();
            result.AddRange(Header.ToByte());
            result.Add((byte)realTimeDataBlocks.Count);
            for (byte i = 0; i < 3; i++)
            {
                result.Add(realTimeDataBlocks[i].BlockNo);
                result.AddRange(StringHelper.DateTimeToByte(realTimeDataBlocks[i].SateTimeMark));
                result.AddRange(BitConverter.GetBytes((ushort)(realTimeDataBlocks[i].Temperature1 * 100)));
                result.AddRange(BitConverter.GetBytes((ushort)(realTimeDataBlocks[i].Temperature2 * 100)));
                result.AddRange(BitConverter.GetBytes((ushort)(realTimeDataBlocks[i].Temperature3 * 100)));
                result.AddRange(BitConverter.GetBytes((ushort)(realTimeDataBlocks[i].Temperature4 * 100)));
                result.AddRange(BitConverter.GetBytes((ushort)(realTimeDataBlocks[i].Temperature5 * 100)));
                result.AddRange(BitConverter.GetBytes((ushort)(realTimeDataBlocks[i].Humidity * 100)));
                result.AddRange(BitConverter.GetBytes((ushort)(realTimeDataBlocks[i].Electric * 100)));
                result.AddRange(BitConverter.GetBytes((ushort)(realTimeDataBlocks[i].Signal * 100)));
                //补零
                for (int j = 0; j < 48 - 24; j++)
                {
                    result.Add(0x00);
                }
            }
            //插入总长度
            result.InsertRange(5, BitConverter.GetBytes((ushort)(result.Count + 4)));
            //压入校验位
            result.AddRange(BitConverter.GetBytes(CRC16Helper.CalculateCrc16(result.ToArray())));
            return result.ToArray();
        }
    }
}
