using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace NetData
{
    /// <summary>
    /// 配置信息解包
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