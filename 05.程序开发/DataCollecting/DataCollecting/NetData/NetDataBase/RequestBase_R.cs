using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataCollecting.Helper;

namespace DataCollecting.NetData
{
    /// <summary>
    /// 设备请求信息基类型
    /// </summary>
    public class RequestBase_R : MessageBase
    {
        public RequestBase_R()
        {
        }

        public RequestBase_R(byte[] data)
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
            //其他信息
            hardwareVersionMain = data[60];
            hardwareVersionChild = data[61];
            softwareVersionMain = data[62];
            softwareVersionChild = data[63];
            workstateMain = data[64];
            workstateChild = data[65];
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
        /// 硬件版本号(主)
        /// </summary>
        private byte hardwareVersionMain;
        public byte HardwareVersionMain
        {
            get { return hardwareVersionMain; }
            set { hardwareVersionMain = value; }
        }

        /// <summary>
        /// 硬件版本号(子)
        /// </summary>
        private byte hardwareVersionChild;
        public byte HardwareVersionChild
        {
            get { return hardwareVersionChild; }
            set { hardwareVersionChild = value; }
        }


        /// <summary>
        /// 软件版本号（主）
        /// </summary>
        private byte softwareVersionMain;
        public byte SoftwareVersionMain
        {
            get { return softwareVersionMain; }
            set { softwareVersionMain = value; }
        }

        /// <summary>
        /// 软件版本号（子）
        /// </summary>
        private byte softwareVersionChild;
        public byte SoftwareVersionChild
        {
            get { return softwareVersionChild; }
            set { softwareVersionChild = value; }
        }

        /// <summary>
        /// 工作状态(主)
        /// </summary>
        private byte workstateMain;
        public byte WorkstateMain
        {
            get { return workstateMain; }
            set { workstateMain = value; }
        }

        /// <summary>
        /// 工作状态（子）
        /// </summary>
        private byte workstateChild;
        public byte WorkstateChild
        {
            get { return workstateChild; }
            set { workstateChild = value; }
        }

        protected override void PushBodyByte(List<byte> result)
        {
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
            result.Add(hardwareVersionMain);
            result.Add(hardwareVersionChild);
            result.Add(softwareVersionMain);
            result.Add(softwareVersionChild);
            result.Add(workstateMain);
            result.Add(workstateChild);
        }
    }
}