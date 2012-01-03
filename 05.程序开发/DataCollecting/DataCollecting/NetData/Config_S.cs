using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataCollecting.NetData
{
    /// <summary>
    /// 配置信息封包
    /// </summary>
    public class Config_S : ReplyBase_S
    {
        public Config_S()
        { }

        public Config_S(byte[] data)
            : base(data)
        {
        }

    }
}