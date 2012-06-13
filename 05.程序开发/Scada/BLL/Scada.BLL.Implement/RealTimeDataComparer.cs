using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Scada.Model.Entity;

namespace Scada.BLL.Implement
{
    public class RealTimeDataComparer : IEqualityComparer<ChartSource>
    {
        #region IEqualityComparer<ChartSource> Members

        public bool Equals(ChartSource x, ChartSource y)
        {
            return x.DeviceDate == y.DeviceDate;
        }

        public int GetHashCode(ChartSource obj)
        {
            return typeof(ChartSource).GetHashCode();
        }

        #endregion
    }

}
