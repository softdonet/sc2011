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
    public class Config_R : RequestBase_R
    {
        public Config_R()
        {
        }

        public Config_R(byte[] data)
            : base(data)
        {

        }

    }
}