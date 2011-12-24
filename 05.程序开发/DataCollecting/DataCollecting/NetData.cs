//*********************************************************************
//               异步通讯命令解析数据类型
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
        Test,//发送哑数据  客户端到服务器
        Logout,//登出 客户端到服务器
        GetConfig,//配置请求 客户端到服务器
        RealTimeData,//设备实时信息 客户端到服务器
        UserEvent,//用户事件 客户端到服务器

        ReplyTest, //回复哑数据 服务器到客户端
        SendConfig //发送设备配置和天气预报信息 服务器到客户端
    }


    public class NetData
    {
        private byte[] data;

        #region 数据报头

        /// <summary>
        /// 设置发送命令头（2Byte）
        /// </summary>
        public void SetSendCmdHead()
        {
            data[0] = 0x55;
            data[1] = 0xAA;
        }

        /// <summary>
        /// 命令字(1Byte)
        /// </summary>
        /// <param name="cmdword"></param>
        public void SetCmdWord(byte cmdword)
        {
            data[3] = cmdword;
        }
        public byte GetCmdWord()
        {
            return data[3];
        }

        /// <summary>
        /// 数据上下文(2Byte)
        /// </summary>
        void SetDataContext(byte[] arr)
        {
            Array.Copy(arr, 0, data, 3, 2);
        }

        byte[] GetDataContext()
        {
            byte[] arr = new byte[2];
            Array.Copy(data, 3, arr, 0, 2);
            return arr;
        }

        /// <summary>
        /// 设备序列号(6Byte)
        /// </summary>
        /// <param name="sn"></param>
        void SetDeviceSN(string sn)
        {

        }

        string GetDeviceSN()
        {
            return null;
        }

        /// <summary>
        /// 状态位（1Byte）
        /// </summary>
        /// <param name="state"></param>
        void SetState(byte[] state)
        {

        }

        void GetState()
        {

        }

        /// <summary>
        /// 设置时间（7Byte）
        /// </summary>
        void SetDateTime()
        {
            byte[] year = BitConverter.GetBytes((ushort)DateTime.Now.Year);
            data[15] = year[0];
            data[16] = year[1];
            data[17] = (byte)DateTime.Now.Month;
            data[18] = (byte)DateTime.Now.Day;
            data[19] = (byte)DateTime.Now.Hour;
            data[20] = (byte)DateTime.Now.Minute;
            data[21] = (byte)DateTime.Now.Second;
        }

        /// <summary>
        /// 获取时间
        /// </summary>
        /// <returns></returns>
        public DateTime GetDateTime()
        {
            string strDateTIme = string.Format("{0}-{1}-{2} {3}:{4}:{5}",
                BitConverter.ToUInt16(data, 15),
                data[17].ToString(),
                data[18].ToString(),
                data[19].ToString(),
                data[20].ToString(),
                data[21].ToString());
            return Convert.ToDateTime(strDateTIme);
        }
        #endregion
    }
}
