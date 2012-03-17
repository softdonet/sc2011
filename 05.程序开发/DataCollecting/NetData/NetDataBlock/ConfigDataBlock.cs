using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Utility;


namespace NetData
{
    /// <summary>
    /// 配置信息数据块
    /// </summary>
    public class ConfigDataBlock
    {
        public ConfigDataBlock()
        {

        }

        public ConfigDataBlock(byte[] data)
        {
            RunMode = data[0];
            Argument1 = BitConverter.ToUInt16(data, 1);
            Argument2 = BitConverter.ToUInt16(data, 3);
            Argument3 = BitConverter.ToUInt16(data, 5);
            DeviceNo = StringHelper.GetDefulatStringByByteArr(data, 7, 8);
            //设备安装地点40字节
            InstalPlaceLength = BitConverter.ToUInt16(data, 15);
            InstalPlace = StringHelper.GetDefulatStringByByteArr(data, 17, InstalPlaceLength);
            DisplayMode = data[57];
            InstancyBtnEnable = (data[58] == 1 ? true : false);
            InfoBtnEnable = (data[59] == 1 ? true : false);
            RepairTel = StringHelper.GetDefulatStringByByteArr(data, 60, 11);
            MainIP = StringHelper.DataToIPStr(data, 71, 4);
            ReserveIP = StringHelper.DataToIPStr(data, 75, 4);
            DomainNameLength = BitConverter.ToUInt16(data, 79);
            DomainName = StringHelper.GetASCIIStringByByteArr(data, 81, DomainNameLength);
            Port = BitConverter.ToUInt16(data, 111);
            ConnectionType = data[113];
            ConnectNameLength = BitConverter.ToUInt16(data, 114);
            ConnectName = StringHelper.GetASCIIStringByByteArr(data, 116, ConnectNameLength);
        }

        /// <summary>
        /// 模式
        /// </summary>
        public byte RunMode { get; set; }
        /// <summary>
        /// 参数1
        /// </summary>
        public ushort Argument1 { get; set; }
        /// <summary>
        /// 参数2
        /// </summary>
        public ushort Argument2 { get; set; }
        /// <summary>
        /// 参数3
        /// </summary>
        public ushort Argument3 { get; set; }

        /// <summary>
        /// 设备编号
        /// </summary>
        public string DeviceNo { get; set; }

        /// <summary>
        /// 设备安装地点长度
        /// </summary>
        public ushort InstalPlaceLength { get; set; }

        /// <summary>
        /// 设备安装地点
        /// </summary>
        public string InstalPlace { get; set; }

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
        public string MainIP { get; set; }

        /// <summary>
        /// 备用DNS
        /// </summary>
        public string ReserveIP { get; set; }

        /// <summary>
        /// 域名长度
        /// </summary>
        public ushort DomainNameLength { get; set; }

        /// <summary>
        /// 域名
        /// </summary>
        public string DomainName { get; set; }

        /// <summary>
        /// 端口号
        /// </summary>
        public ushort Port { get; set; }

        /// <summary>
        /// 连接方式
        /// </summary>
        public byte ConnectionType { get; set; }

        /// <summary>
        /// 接入点名称长度
        /// </summary>
        public ushort ConnectNameLength { get; set; }

        /// <summary>
        /// 接入点名称
        /// </summary>
        public string ConnectName { get; set; }

        public byte[] ToByte()
        {
            List<byte> result = new List<byte>();
            result.Add(RunMode);
            result.AddRange(BitConverter.GetBytes(Argument1));
            result.AddRange(BitConverter.GetBytes(Argument2));
            result.AddRange(BitConverter.GetBytes(Argument3));
            result.AddRange(System.Text.Encoding.ASCII.GetBytes(DeviceNo));
            result.AddRange(BitConverter.GetBytes(StringHelper.GetLength(InstalPlace)));
            result.AddRange(StringHelper.GetCharactersByte(InstalPlace, 40));
            result.Add(DisplayMode);
            result.Add((byte)(InstancyBtnEnable ? 1 : 0));
            result.Add((byte)(InfoBtnEnable ? 1 : 0));
            result.AddRange(System.Text.Encoding.ASCII.GetBytes(RepairTel));
            result.AddRange(StringHelper.IPStrData(MainIP));
            result.AddRange(StringHelper.IPStrData(ReserveIP));
            result.AddRange(BitConverter.GetBytes((ushort)DomainName.Length));
            result.AddRange(StringHelper.GetCharactersByte(DomainName, 30));
            result.AddRange(BitConverter.GetBytes(Port));
            result.Add(ConnectionType);
            result.AddRange(BitConverter.GetBytes(StringHelper.GetLength(ConnectName)));
            result.AddRange(StringHelper.GetCharactersByte(ConnectName, 10));
            //预留10字节
            result.AddRange(StringHelper.GetEmptyByte(10));
            return result.ToArray();
        }
    }
}
