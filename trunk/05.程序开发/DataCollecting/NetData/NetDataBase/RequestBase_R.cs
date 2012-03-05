using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Utility;


namespace NetData
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
            mac = StringHelper.DataToMACStr(data, 21, 7);
            //SIM卡号
            sim = System.Text.Encoding.ASCII.GetString(data, 28, 20);
            //产品型号长度
            deviveTypeLength = BitConverter.ToUInt16(data, 48);
            //产品型号
            deviveType = System.Text.Encoding.ASCII.GetString(data, 50, deviveTypeLength);

            int index = 50 + deviveTypeLength;
            //硬件住版本号
            hardwareVersionMain = data[index];
            //硬件子版本号
            hardwareVersionChild = data[index + 1];
            //软件主版本号
            softwareVersionMain = data[index + 2];
            //软件子版本号
            softwareVersionChild = data[index + 3];
            //工作状态主信息
            workstateMain = data[index + 4];
            //工作状态子信息
            workstateChild = data[index + 5];
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
        /// 产品型号长度
        /// </summary>
        private ushort deviveTypeLength;
        public ushort DeviveTypeLength
        {
            get { return deviveTypeLength; }
            set { deviveTypeLength = value; }
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
            result.AddRange(StringHelper.MACStrData(mac));
            //压入SIM卡号
            result.AddRange(System.Text.Encoding.ASCII.GetBytes(sim));
            //压入产品型号长度
            result.AddRange(BitConverter.GetBytes((ushort)deviveType.Length));
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