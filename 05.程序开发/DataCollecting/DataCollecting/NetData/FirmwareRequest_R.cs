using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataCollecting.NetData
{
    /// <summary>
    /// 设备请求固件更新
    /// </summary>
    public class FirmwareRequest_R : RequestBase_R
    {
        public FirmwareRequest_R()
        {
        }

        public FirmwareRequest_R(byte[] data)
            : base(data)
        {

        }
    }
}