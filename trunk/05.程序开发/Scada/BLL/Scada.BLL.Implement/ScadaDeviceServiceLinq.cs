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
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="deviceInfo"></param>
        /// <returns></returns>
        public Boolean AddDeviceInfo(string deviceInfo)
        {
            SCADADataContext sCADADataContext = new SCADADataContext();
            Scada.Model.Entity.DeviceInfo deviceValue = BinaryObjTransfer.JsonDeserialize<Scada.Model.Entity.DeviceInfo>(deviceInfo);
            Scada.DAL.Linq.DeviceInfo linDeviceInfor = deviceValue.ConvertTo<Scada.DAL.Linq.DeviceInfo>();
            sCADADataContext.DeviceInfos.InsertOnSubmit(linDeviceInfor);
            sCADADataContext.SubmitChanges();
            return true;
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool Del(string id)
        {
            SCADADataContext sCADADataContext = new SCADADataContext();
            var obj = sCADADataContext.DeviceInfos.SingleOrDefault(e => e.ID == new Guid(id));
            sCADADataContext.DeviceInfos.DeleteOnSubmit(obj);
            sCADADataContext.SubmitChanges();
            return true;
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="deviceInfo"></param>
        /// <returns></returns>
        public bool Update(string deviceInfo)
        {
            SCADADataContext sCADADataContext = new SCADADataContext();
            Scada.Model.Entity.DeviceInfo deviceValue = BinaryObjTransfer.JsonDeserialize<Scada.Model.Entity.DeviceInfo>(deviceInfo);

            var obj = sCADADataContext.DeviceInfos.SingleOrDefault(e => e.ID == deviceValue.ID);
            if (obj != null)
            {
                obj.DeviceNo = deviceValue.DeviceNo;
                obj.DeviceSN = deviceValue.DeviceSN;
                //Todo:其他属性
            }
            sCADADataContext.SubmitChanges();
            return true;
        }

        /// <summary>
        /// 获取单体设备
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string Get(string id)
        {
            SCADADataContext sCADADataContext = new SCADADataContext();
            var obj = sCADADataContext.DeviceInfos.SingleOrDefault(e => e.ID == new Guid(id));
            if (obj != null)
            {
                Scada.Model.Entity.DeviceInfo deviceInfo = obj.ConvertTo<Scada.Model.Entity.DeviceInfo>();
                var result = BinaryObjTransfer.JsonSerializer<Scada.Model.Entity.DeviceInfo>(deviceInfo);
                return result;
            }
            return string.Empty;
        }
    }
}
