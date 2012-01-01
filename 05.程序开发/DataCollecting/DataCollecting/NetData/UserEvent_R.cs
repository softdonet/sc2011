using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataCollecting.NetData
{
    public class UserEvent_R : MessageBase
    {
        public UserEvent_R(byte[] data)
        {
            this.Header = new Head(data);
        }

    }
}
