using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Utility;

namespace NetData
{
    /// <summary>
    /// 报头定义21Byte
    /// yanghk at 2011-12-18
    /// </summary>
    public class Head
    {
        #region 报头

        /// <summary>
        /// 报文头
        /// </summary>
        private ushort cmdHeader;
        public ushort CmdHeader
        {
            get { return cmdHeader; }
            set { cmdHeader = value; }
        }

        /// <summary>
        /// 功能码
        /// </summary>
        private Command cmdCommand;
        public Command CmdCommand
        {
            get { return cmdCommand; }
            set { cmdCommand = value; }
        }

        /// <summary>
        /// 数据上下文
        /// </summary>
        private ushort dataContext;
        public ushort DataContext
        {
            get { return dataContext; }
            set { dataContext = value; }
        }

        /// <summary>
        /// 数据包长度
        /// </summary>
        private ushort commandCount;
        public ushort CommandCount
        {
            get { return commandCount; }
            set { commandCount = value; }
        }

        /// <summary>
        /// 设备序列号
        /// </summary>
        private string deviceSN;
        public string DeviceSN
        {
            get { return deviceSN; }
            set { deviceSN = value; }
        }

        /// <summary>
        /// 状态
        /// </summary>
        private byte state;
        public byte State
        {
            get { return state; }
            set { state = value; }
        }

        /// <summary>
        /// 时间戳
        /// </summary>
        private DateTime sateTimeMark;
        public DateTime SateTimeMark
        {
            get { return sateTimeMark; }
            set { sateTimeMark = value; }
        }

        #endregion
        public Head()
        {
        }
        public Head(byte[] data)
        {
            //命令头(0-1)
            cmdHeader = BitConverter.ToUInt16(data, 0);
            //功能码(2)
            cmdCommand = (Command)data[2];
            //数据上下文(3-4)
            dataContext = BitConverter.ToUInt16(data, 3);
            //报文长度(5-6)
            commandCount = BitConverter.ToUInt16(data, 5);
            //设备序列号(7-12)
            deviceSN = StringHelper.DataToStr(data, 7, 4) + BitConverter.ToUInt16(data, 11).ToString("0000");
            //状态(13)
            state = data[13];
            //时间戳(14-20)
            sateTimeMark = StringHelper.ByteToDateTime(data, 14, 7);
        }

        /// <summary>
        /// 转化为字节数组
        /// </summary>
        /// <returns></returns>
        public byte[] ToByte()
        {
            List<byte> result = new List<byte>();
            //压入报头字符
            result.AddRange(BitConverter.GetBytes(cmdHeader));
            //压入功能吗
            result.Add((byte)cmdCommand);
            //压入数据上下文
            result.AddRange(BitConverter.GetBytes(dataContext));
            //------------------------------------------
            //压入报文长度
            //ushort报文总长度（5--6）。插入报文长度。待数据报文生成之后。InsertRange插入。
            //------------------------------------------
            //压入设备序列号
            //-----------------------------------------------
            //省级
            result.Add(Convert.ToByte((deviceSN.Substring(0, 2)),16));
            //市级
            result.Add(Convert.ToByte((deviceSN.Substring(2, 2)), 16));
            //区县
            result.Add(Convert.ToByte((deviceSN.Substring(4, 2)), 16));
            //公司编号及设备类型
            result.Add(Convert.ToByte((deviceSN.Substring(6, 2)), 16));
            //公司编号及设备类型
            result.AddRange(BitConverter.GetBytes(Convert.ToUInt16(deviceSN.Substring(8, 4))));
            //-------------------------------------------------
            //压入状态
            result.Add(state);
            //压入时间
            result.AddRange(StringHelper.DateTimeToByte(DateTime.Now));
            return result.ToArray();
        }
    }
}
