//*********************************************************************
//               异步通讯命令数据报头解析类型（报头21Byte）
//               Create by yanghk at 2011-12-18
//*********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataCollecting
{
    /// <summary>
    /// 通讯命令枚举
    /// </summary>
    public enum Command
    {
        /// <summary>
        ///设备链路测试
        /// </summary>
        cmd_Test = 0x01,
        /// <summary>
        /// 设备主动关闭告知
        /// </summary>
        cmd_Logout = 0x02,
        /// <summary>
        /// 设备请求配置信息
        /// </summary>
        cmd_GetConfig = 0x03,
        /// <summary>
        /// 设备主动发送实时数据
        /// </summary>
        cmd_RealTimeDate = 0x04,
        /// <summary>
        /// 设备侧发送用户事件
        /// </summary>
        cmd_UserEvent = 0x05,
        /// <summary>
        /// 设备到厂家服务器注册（固定IP+默认端口)
        /// </summary>
        cmd_Register = 0xFD,
        /// <summary>
        /// 设备请求固件更新
        /// </summary>
        cmd_FirmwareRequest = 0xFE,
        /// <summary>
        /// 没有命令
        /// </summary>
        cmd_null
    }

    public class NetData
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

        public NetData(byte[] data)
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
            byte[] ldeviceSN = new byte[4];
            Array.Copy(data, 7, ldeviceSN, 0, 4);
            deviceSN = DataToStr(ldeviceSN) + BitConverter.ToUInt16(data, 11).ToString("0000");
            //状态(13)
            state = data[13];
            //时间戳(14-20)
            sateTimeMark = Convert.ToDateTime(string.Format("{0}-{1}-{2} {3}:{4}:{5}",
                BitConverter.ToUInt16(data, 14),
                data[16].ToString(),
                data[17].ToString(),
                data[18].ToString(),
                data[19].ToString(),
                data[20].ToString()));
        }

        /// <summary>
        /// 字节转化为字符
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public string DataToStr(byte[] data)
        {
            string result = "";
            for (int i = 0; i < data.Length; i++)
            {
                string temp = Convert.ToString(data[i], 16);
                if (temp.Length == 1)
                {
                    temp = "0" + temp;
                }
                result = result + temp;
            }
            return result;
        }
    }
}
