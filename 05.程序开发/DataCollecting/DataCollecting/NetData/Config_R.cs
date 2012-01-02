using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataCollecting.Helper;

namespace DataCollecting.NetData
{
    /// <summary>
    /// 获取配置信息解包
    /// </summary>
    public class Config_R : MessageBase
    {
        public Config_R()
        {
        }

        public Config_R(byte[] data)
            : base(data)
        {
            //MAC地址
            byte[] arrMac = new byte[7];
            Array.Copy(data, 21, arrMac, 0, 7);
            mac = StringHelper.DataToMACStr(arrMac);
            //SIM卡号
            byte[] arrSim = new byte[20];
            Array.Copy(data, 28, arrSim, 0, 20);
            sim = System.Text.Encoding.ASCII.GetString(arrSim);
            //产品型号
            byte[] arrType = new byte[12];
            Array.Copy(data, 48, arrType, 0, 12);
            deviveType = System.Text.Encoding.ASCII.GetString(arrType);
        }

        /// <summary>
        /// MAC地址
        /// </summary>
        private string mac;
        public string MAC
        {
            get { return mac; }
            set { mac = value; }
        }

        /// <summary>
        /// SIM卡号
        /// </summary>
        private string sim;
        public string SIM
        {
            get { return sim; }
            set { sim = value; }
        }

        /// <summary>
        /// 产品型号
        /// </summary>
        private string deviveType;
        public string DeviveType
        {
            get { return deviveType; }
            set { deviveType = value; }
        }

        /// <summary>
        /// 硬件版本号
        /// </summary>
        private float hardwareVersions;
        public float HardwareVersions
        {
            get { return hardwareVersions; }
            set { hardwareVersions = value; }
        }

        /// <summary>
        /// 软件版本号
        /// </summary>
        private float softwareVersions;
        public float SoftwareVersions
        {
            get { return softwareVersions; }
            set { softwareVersions = value; }
        }

        /// <summary>
        /// 工作状态
        /// </summary>
        private float workstate;
        public float Workstate
        {
            get { return workstate; }
            set { workstate = value; }
        }


        /// <summary>
        /// 转化为字节数组
        /// </summary>
        /// <returns></returns>
        public byte[] ToByte()
        {
            List<byte> result = new List<byte>();
            //压入头
            result.AddRange(Header.ToByte());
            //压入MAC地址
            string[] arr = mac.Split('-');
            foreach (string item in arr)
            {
                result.Add(Convert.ToByte(item, 16));
            }
            //压入SIM卡号
            result.AddRange(System.Text.Encoding.ASCII.GetBytes(sim));
            //压入产品型号12位
            byte[] arrtype = System.Text.Encoding.ASCII.GetBytes(deviveType);
            result.AddRange(arrtype);
            for (int i = 0; i < 12 - arrtype.Length; i++)
            {
                result.Add(0x00);
            }
            //压入其他信息
            //------------------------
            //-----------------------
            //压入总长度
            result.InsertRange(5, BitConverter.GetBytes((ushort)(result.Count + 4)));
            //压入校验位
            result.AddRange(BitConverter.GetBytes(CRC16Helper.CalculateCrc16(result.ToArray())));
            return result.ToArray();
        }
    }
}