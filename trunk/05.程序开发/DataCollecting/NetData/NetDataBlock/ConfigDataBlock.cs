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

            DisplayMode = data[15];
            InstancyBtnEnable = (data[16] == 1 ? true : false);
            InfoBtnEnable = (data[17] == 1 ? true : false);
            RepairTel = StringHelper.GetDefulatStringByByteArr(data, 18, 11);
            MainDNS = StringHelper.DataToIPStr(data, 29, 4);
            ReserveDNS = StringHelper.DataToIPStr(data, 33, 4);
            ServerIP = StringHelper.DataToIPStr(data, 37, 4);
            DomainNameLength = BitConverter.ToUInt16(data, 41);
            DomainName = StringHelper.GetASCIIStringByByteArr(data, 43, DomainNameLength);
            Port = BitConverter.ToUInt16(data, 73);
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
        public string ServerIP { get; set; }

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

        public byte[] ToByte()
        {
            List<byte> result = new List<byte>();
            result.Add(RunMode);
            result.AddRange(BitConverter.GetBytes(Argument1));
            result.AddRange(BitConverter.GetBytes(Argument2));
            result.AddRange(BitConverter.GetBytes(Argument3));
            result.AddRange(System.Text.Encoding.ASCII.GetBytes(DeviceNo));
            result.Add(DisplayMode);
            result.Add((byte)(InstancyBtnEnable ? 1 : 0));
            result.Add((byte)(InfoBtnEnable ? 1 : 0));
            result.AddRange(System.Text.Encoding.ASCII.GetBytes(RepairTel));
            result.AddRange(StringHelper.IPStrData(MainDNS));
            result.AddRange(StringHelper.IPStrData(ReserveDNS));
            result.AddRange(StringHelper.IPStrData(ServerIP));
            result.AddRange(BitConverter.GetBytes((ushort)DomainName.Length));
            result.AddRange(GetDomainByte(DomainName));
            result.AddRange(BitConverter.GetBytes(Port));
            //预留50个字节
            for (int j = 0; j < 50; j++)
            {
                result.Add(0x00);
            }
            return result.ToArray();
        }

        /// <summary>
        /// 获取域名字节数组
        /// </summary>
        /// <param name="dm"></param>
        /// <returns></returns>
        private byte[] GetDomainByte(string dm)
        {
            List<byte> result = new List<byte>();
            byte[] arr = System.Text.Encoding.Default.GetBytes(dm);
            if (arr.Length <= 30)
            {
                result.AddRange(arr);
                //补零
                for (int j = 0; j < 30 - arr.Length; j++)
                {
                    result.Add(0x00);
                }
            }
            else
            {
                //大于30直接截断
                byte[] temp = new byte[30];
                Array.Copy(arr, 0, temp, 0, 30);
                result.AddRange(temp);
            }
            return result.ToArray();
        }
    }
}
