using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Scada.DAL.Linq;
using Scada.Utility.Common.Transfer;
using Scada.Utility.Common.Extensions;
namespace Scada.BLL.Implement
{
    public class ScadaDeviceServiceLinq
    {
        public Boolean AddDeviceInfo(string deviceInfo)
        {
            SCADADataContext sCADADataContext = new SCADADataContext();
            Scada.Model.Entity.DeviceInfo deviceValue = BinaryObjTransfer.JsonDeserialize<Scada.Model.Entity.DeviceInfo>(deviceInfo);
            Scada.DAL.Linq.DeviceInfo linDeviceInfor = deviceValue.ConvertTo<Scada.DAL.Linq.DeviceInfo>();
            sCADADataContext.DeviceInfos.InsertOnSubmit(linDeviceInfor);
            sCADADataContext.SubmitChanges();
            return true;
        }

    }
}
