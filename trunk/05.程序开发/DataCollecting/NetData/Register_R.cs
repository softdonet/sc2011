using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetData
{
    public class Register_R : RequestBase_R
    {
        public Register_R()
        {
        }

        public Register_R(byte[] data)
            : base(data)
        {

        }
    }
}