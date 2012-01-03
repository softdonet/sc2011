using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataCollecting.Common;

namespace DataCollecting.NetData
{
    /// <summary>
    /// 实时数据封包
    /// </summary>
    public class RealTimeData_S : ReplyBase_S
    {
        public RealTimeData_S()
        { }

        public RealTimeData_S(byte[] data)
            : base(data)
        {
        }

    }
}