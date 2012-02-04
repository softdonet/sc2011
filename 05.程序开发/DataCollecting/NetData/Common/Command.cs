using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;

namespace NetData
{

    /// <summary>
    /// 通讯命令枚举
    /// </summary>
    public enum Command
    {
        /// <summary>
        ///设备链路测试
        /// </summary>
        cmd_Test_R = 0x01,
        /// <summary>
        /// 设备主动关闭告知
        /// </summary>
        cmd_Logout_R = 0x02,
        /// <summary>
        /// 设备请求配置信息
        /// </summary>
        cmd_Config_R = 0x03,
        /// <summary>
        /// 设备主动发送实时数据
        /// </summary>
        cmd_RealTimeDate_R = 0x04,
        /// <summary>
        /// 设备侧发送用户事件
        /// </summary>
        cmd_UserEvent_R = 0x05,
        /// <summary>
        /// 设备到厂家服务器注册（固定IP+默认端口)
        /// </summary>
        cmd_Register_R = 0xFD,
        /// <summary>
        /// 设备请求固件更新
        /// </summary>
        cmd_FirmwareRequest_R = 0xFE,
        /// <summary>
        /// 设备回复信息
        /// </summary>
        cmd_Reply = 0x30,
        /// <summary>
        /// 没有命令
        /// </summary>
        cmd_null
    }
}
