using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataCollecting.NetData
{
    /// <summary>
    /// 用户事件
    /// </summary>
    public class UserEvent_R : RequestBase_R
    {
        public UserEvent_R()
        {
        }

        public UserEvent_R(byte[] data)
            : base(data)
        {

        }
    }
}