using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetData
{
    public class UserEvent_S: ReplyBase_S
    {
        public UserEvent_S()
        { }

        public UserEvent_S(byte[] data)
            : base(data)
        {
        }

    }
}