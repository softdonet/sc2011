using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataCollecting.Common
{
    public class ConfigDataBlock
    {
        /// <summary>
        /// 模式
        /// </summary>
        public byte RunMode { get; set; }
        /// <summary>
        /// 参数1
        /// </summary>
        public byte Argument1 { get; set; }
        /// <summary>
        /// 参数2
        /// </summary>
        public byte Argument2 { get; set; }
        /// <summary>
        /// 参数3
        /// </summary>
        public byte Argument3 { get; set; }

        /// <summary>
        /// 设备编号
        /// </summary>
        public string DeviceNo { get; set; }

        /// <summary>
        /// 显示模式
        /// </summary>
        public byte DisplayMode { get; set; }

        /// <summary>
        /// 是否启用紧急按钮
        /// </summary>
        public bool InstancyBtnEnable { get; set; }

        /// <summary>
        /// 是否启用信息按钮
        /// </summary>
        public bool InfoBtnEnable { get; set; }

        /// <summary>
        /// 维护人员手机号码
        /// </summary>
        public string RepairTel { get; set; }

        /// <summary>
        /// 主DNS
        /// </summary>
        public string MainDNS { get; set; }

        /// <summary>
        /// 备用DNS
        /// </summary>
        public string ReserveDNS { get; set; }

        /// <summary>
        /// 服务器IP地址
        /// </summary>
        public string IP { get; set; }

        /// <summary>
        /// 域名
        /// </summary>
        public string DomainName { get; set; }

        /// <summary>
        /// 端口号
        /// </summary>
        public ushort Port { get; set; }

        /// <summary>
        /// 预留字节
        /// </summary>
        public string FreeByte { get; set; }

        public byte[] ToByte()
        {
            return null;
        }
    }
}
