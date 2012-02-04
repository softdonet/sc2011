using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace NetData
{
    /// <summary>
    /// 设备实时数据解包
    /// </summary>
    public class RealTimeData_R : MessageBase
    {
        public RealTimeData_R(byte[] data)
            : base(data)
        {
            //每个块占字节数
            int blockSize = 48;
            //数据块数
            blockCount = Convert.ToInt32(data[21]);
            //数据块长度
            int blockLength = blockCount * 48;
            //取出数据块内容
            byte[] dataBlock = new byte[blockLength];
            Array.Copy(data, 22, dataBlock, 0, blockLength);
            for (int i = 0; i < blockCount; i++)
            {
                byte[] singleBlock = new byte[blockSize];
                Array.Copy(dataBlock, i * blockSize, singleBlock, 0, blockSize);
                RealTimeDataBlock realTimeDataBlock = new RealTimeDataBlock(singleBlock);
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

        protected override void PushBodyByte(List<byte> result)
        {
            //压入块长度
            result.Add((byte)realTimeDataBlocks.Count);
            //压入数据块
            for (byte i = 0; i < 3; i++)
            {
                result.AddRange(realTimeDataBlocks[i].ToByte());
            }
        }
    }
}
