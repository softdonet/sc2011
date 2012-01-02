using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataCollecting.NetData
{
    /// <summary>
    /// 获取配置信息解包
    /// </summary>
    public class Config_R : MessageBase
    {
        public Config_R(byte[] data)
            : base(data)
        {
          
        }
    }
}