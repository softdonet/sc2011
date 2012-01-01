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
}
