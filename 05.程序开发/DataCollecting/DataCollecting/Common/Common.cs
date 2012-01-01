using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;

namespace DataCollecting.Common
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
        cmd_Config = 0x03,
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

    public struct ClientInfo
    {
        public Socket socket;   //Socket of the client
        public int intDevnum;  //设备编号（设备编号从1开始，0为非法设备）
    }
}
