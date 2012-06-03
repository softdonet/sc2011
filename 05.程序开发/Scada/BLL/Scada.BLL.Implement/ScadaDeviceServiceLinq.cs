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

        private SCADADataContext sCADADataContext = null;
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="deviceInfo"></param>
        /// <returns></returns>
        public Boolean AddDeviceInfo(string deviceInfo)
        {
            sCADADataContext = new SCADADataContext();
            Scada.Model.Entity.DeviceInfo deviceValue = BinaryObjTransfer.JsonDeserialize<Scada.Model.Entity.DeviceInfo>(deviceInfo);
            Scada.DAL.Linq.DeviceInfo linDeviceInfor = deviceValue.ConvertTo<Scada.DAL.Linq.DeviceInfo>(d =>
            {
                d.IsNew = true;
            });
            sCADADataContext.DeviceInfos.InsertOnSubmit(linDeviceInfor);
            sCADADataContext.SubmitChanges();
            return true;
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool Del(Guid id)
        {
            sCADADataContext = new SCADADataContext();
            var obj = sCADADataContext.DeviceInfos.SingleOrDefault(e => e.ID == id);
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
            sCADADataContext = new SCADADataContext();
            Scada.Model.Entity.DeviceInfo deviceValue = BinaryObjTransfer.JsonDeserialize<Scada.Model.Entity.DeviceInfo>(deviceInfo);

            var obj = sCADADataContext.DeviceInfos.SingleOrDefault(e => e.ID == deviceValue.ID);
            if (obj != null)
            {
                obj.DeviceNo = deviceValue.DeviceNo;
                obj.DeviceSN = deviceValue.DeviceSN;
                obj.HardType = deviceValue.HardType;
                obj.ProductDate = deviceValue.ProductDate;
                obj.DeviceMAC = deviceValue.DeviceMAC;
                obj.SIMNo = deviceValue.SIMNo;
                obj.ManageAreaID = deviceValue.ManageAreaID;
                obj.MaintenancePeopleID = deviceValue.MaintenancePeopleID;
                obj.InstallPlace = deviceValue.InstallPlace;
                obj.Longitude = deviceValue.Longitude;
                obj.Latitude = deviceValue.Latitude;
                obj.High = deviceValue.High;
                obj.Comment = deviceValue.Comment;
                obj.Windage = deviceValue.Windage;
                obj.HardwareVersion = deviceValue.HardwareVersion;
                obj.SoftWareVersion = deviceValue.SoftWareVersion;
                obj.LCDScreenDisplayType = deviceValue.LCDScreenDisplayType;
                obj.UrgencyBtnEnable = deviceValue.UrgencyBtnEnable;
                obj.InforBtnEnable = deviceValue.InforBtnEnable;

                obj.Temperature1AlarmValid = deviceValue.Temperature1AlarmValid;
                obj.Temperature1HighAlarm = deviceValue.Temperature1HighAlarm;
                obj.Temperature1LowAlarm = deviceValue.Temperature1LowAlarm;
                obj.Temperature2AlarmValid = deviceValue.Temperature2AlarmValid;
                obj.Temperature2HighAlarm = deviceValue.Temperature2HighAlarm;
                obj.Temperature2LowAlarm = deviceValue.Temperature2LowAlarm;

                obj.HumidityAlarmValid = deviceValue.HumidityAlarmValid;
                obj.HumidityHighAlarm = deviceValue.HumidityHighAlarm;
                obj.HumidityLowAlarm = deviceValue.HumidityLowAlarm;
                obj.SignalAlarmValid = deviceValue.SignalAlarmValid;
                obj.SignalHighAlarm = deviceValue.SignalHighAlarm;
                obj.SignalLowAlarm = deviceValue.SignalLowAlarm;

                obj.ElectricityAlarmValid = deviceValue.ElectricityAlarmValid;
                obj.ElectricityHighAlarm = deviceValue.ElectricityHighAlarm;
                obj.ElectricityLowAlarm = deviceValue.ElectricityLowAlarm;

                obj.CurrentModel = deviceValue.CurrentModel;
                obj.RealTimeParam = deviceValue.RealTimeParam;
                obj.FullTimeParam1 = deviceValue.FullTimeParam1;
                obj.FullTimeParam2 = deviceValue.FullTimeParam2;
                obj.OptimizeParam1 = deviceValue.OptimizeParam1;
                obj.OptimizeParam2 = deviceValue.OptimizeParam2;
                obj.OptimizeParam3 = deviceValue.OptimizeParam3;
                obj.IsNew = true;
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
        public string Get(Guid id)
        {
            sCADADataContext = new SCADADataContext();
            var obj = sCADADataContext.DeviceInfos.SingleOrDefault(e => e.ID == id);
            if (obj != null)
            {
                Scada.Model.Entity.DeviceInfo deviceInfo = obj.ConvertTo<Scada.Model.Entity.DeviceInfo>(d =>
                {
                    d.ManageAreaName = obj.DeviceTree.Name;
                    if (obj.DeviceRealTimes.Any())
                    {
                        d.RealTimeTemperature = obj.DeviceRealTimes.OrderByDescending(e => e.UpdateTime).First().Temperature1;
                    }
                });
                var result = BinaryObjTransfer.JsonSerializer<Scada.Model.Entity.DeviceInfo>(deviceInfo);
                return result;
            }
            return string.Empty;
        }

        public bool GetCountDeviceName(string deviceNo)
        {
            sCADADataContext = new SCADADataContext();
            //单条用SingleOrDefault，如果有多条则会报错
            var obj = sCADADataContext.DeviceInfos.SingleOrDefault(e => e.DeviceNo == deviceNo);
            if (obj == null)
            {
                return false;
            }
            else
            {
                return true;
            }
            ////多条可以用如下方法
            //var obj1 = sCADADataContext.DeviceInfos.Where(e => e.DeviceNo == deviceNo);
            //obj1.Any();

            ////////-------------------------
            //if (obj != null)
            //{
            //    List<DeviceInfo> deviceInfo = obj.Select(e => e.ConvertTo<DeviceInfo>()).ToList();
            //}
            return false;
        }

    }
}
