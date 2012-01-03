using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataCollecting.Helper;

namespace DataCollecting.NetData
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
            Argument1 = data[1];
            Argument2 = data[2];
            Argument3 = data[3];
            DeviceNo = StringHelper.GetDefulatStringByByteArr(data,4,8);

            DisplayMode = data[12];
            InstancyBtnEnable = (data[13] == 1 ? true : false);
            InfoBtnEnable = (data[14] == 1 ? true : false);
            RepairTel = StringHelper.GetDefulatStringByByteArr(data,15,11);
            MainDNS = StringHelper.DataToIPStr(data, 26, 8);
            ReserveDNS = StringHelper.DataToIPStr(data, 34, 8);
            ServerIP = StringHelper.DataToIPStr(data, 42, 8);
            DomainName = StringHelper.GetASCIIStringByByteArr(data, 50, 30);
            Port = BitConverter.ToUInt16(data, 80);
        }

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
        public string ServerIP { get; set; }

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
            result.Add(Argument1);
            result.Add(Argument2);
            result.Add(Argument3);
            result.AddRange(System.Text.Encoding.ASCII.GetBytes(DeviceNo));
            result.Add(DisplayMode);
            result.Add((byte)(InstancyBtnEnable ? 1 : 0));
            result.Add((byte)(InfoBtnEnable ? 1 : 0));
            result.AddRange(System.Text.Encoding.ASCII.GetBytes(RepairTel));
            result.AddRange(StringHelper.IPStrData(MainDNS));
            result.AddRange(StringHelper.IPStrData(ReserveDNS));
            result.AddRange(StringHelper.IPStrData(ServerIP));
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
