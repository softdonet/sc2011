using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NetData;
using DataAccess;

namespace BusinessRules
{
    /// <summary>
    /// 业务逻辑类
    /// yanghk at 2012-2-4
    /// </summary>
    public class BLL
    {
        SCADADataContext DataContext = new SCADADataContext();
        /// <summary>
        /// 更新设备
        /// </summary>
        /// <param name="dc_r"></param>
        /// <returns></returns>
        public bool RefreshData(RequestBase_R dc_r)
        {
            //    /*业务逻辑：
            //    根据设备SN号在数据库中查找进行对比。
            //           如果有该设备，则更新设备属性
            //           {
            //                MAC地址
            //                SIM地址
            //                产品型号
            //                硬件版本
            //                固件版本
            //                工作状态
            //            }
            //          成功则返回true
            //          没有找到或者执行失败返回false
            //    
            using (SCADADataContext DataContext = new SCADADataContext())
            {
                DeviceInfo deviceInfor = DataContext.DeviceInfos.SingleOrDefault(e => e.DeviceSN == dc_r.Header.DeviceSN);
                if (deviceInfor != null)
                {
                    deviceInfor.DeviceMAC = dc_r.MAC;
                    deviceInfor.SIMNo = dc_r.SIM;
                    deviceInfor.HardType = dc_r.DeviveType;
                    deviceInfor.HardwareVersion = string.Format("{0}.{1}", dc_r.HardwareVersionMain.ToString(),
                        dc_r.HardwareVersionChild);
                    deviceInfor.SoftWareVersion = string.Format("{0}.{1}", dc_r.SoftwareVersionMain.ToString(),
                        dc_r.SoftwareVersionMain);
                    DataContext.SubmitChanges();
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 获取设备配置信息、天气预报、广播等
        /// </summary>
        /// <param name="deviceSN"></param>
        /// <returns></returns>
        public ReplyBase_S GetDeviceInfor(string deviceSN)
        {
            //业务逻辑：
            //如果设备配置、天气预报、广播信息有更新。
            //则回复配置信息+天气预报+广播信息，并标记。
            //示例代码：
            ReplyBase_S rs = new ReplyBase_S();
            rs.HaveBroadcastInfo = true;
            rs.HaveConfigInfo = true;
            rs.HaveWeatherInfo = true;

            //构造设备配置信息块
            ConfigDataBlock configDataBlock = new ConfigDataBlock();
            configDataBlock.RunMode = 1;
            configDataBlock.Argument1 = 1500;
            configDataBlock.Argument2 = 1600;
            configDataBlock.Argument3 = 1700;
            configDataBlock.DeviceNo = "P-100100";
            configDataBlock.InstalPlace = "北京市朝阳区双井街道";
            configDataBlock.DisplayMode = 1;
            configDataBlock.InstancyBtnEnable = true;
            configDataBlock.InfoBtnEnable = true;
            configDataBlock.RepairTel = "13801112222";
            configDataBlock.MainIP = "202.106.42.1";
            configDataBlock.ReserveIP = "202.106.42.2";

            configDataBlock.DomainName = "xyz.dddd.com";
            configDataBlock.Port = 1789;
            configDataBlock.ConnectionType = 0;
            configDataBlock.ConnectName = "CMNET";
            rs.ConfigData = configDataBlock;

            //构造天气预报块
            WeatherDataBlock weatherDataBlock = new WeatherDataBlock();
            weatherDataBlock.TodayWeather = "晴转多云，有阵雨。";
            rs.WeatherData = weatherDataBlock;

            //构造广播信息块
            BroadcastDataBlock broadcastDataBlock = new BroadcastDataBlock();
            broadcastDataBlock.Msg = "通知：明天下午开会。不要迟到，迟到一分钟扣100元。";
            rs.BroadcastData = broadcastDataBlock;

            return rs;
        }

        /// <summary>
        /// 保存实时数据
        /// </summary>
        /// <param name="realTimeData_R"></param>
        /// <returns></returns>
        public bool SaveRealTimeData(RealTimeData_R realTimeData_R)
        {
            using (SCADADataContext DataContext = new SCADADataContext())
            {
                DeviceInfo deviceInfor = DataContext.DeviceInfos.SingleOrDefault(e => e.DeviceSN == realTimeData_R.Header.DeviceSN);
                if (deviceInfor != null)
                {
                    foreach (RealTimeDataBlock item in realTimeData_R.RealTimeDataBlocks)
                    {
                        DeviceRealTime drt = new DeviceRealTime();
                        drt.ID = Guid.NewGuid();
                        drt.DeviceID = deviceInfor.ID;
                        drt.DeviceNo = deviceInfor.DeviceNo;
                        //温度1
                        drt.Temperature1 = item.Temperature1;
                        //温度2
                        drt.Temperature2 = item.Temperature2;
                        //信号
                        drt.Signal = item.Signal;
                        //湿度
                        drt.Humidity = item.Humidity;
                        //电量
                        drt.Electricity = item.Electric;
                        //设备状态
                        drt.Status = 1;
                        drt.UpdateTime = item.SateTimeMark;
                        DataContext.DeviceRealTimes.InsertOnSubmit(drt);
                        //判断告警的逻辑
                        if (deviceInfor.Temperature1AlarmValid.Value)
                        {
                            if (drt.Temperature1.Value > deviceInfor.Temperature1HighAlarm.Value ||
                                drt.Temperature1.Value < deviceInfor.Temperature1LowAlarm.Value)
                            {
                                //设备告警
                                drt.Status = 3;
                                DeviceAlarm da = new DeviceAlarm();
                                da.ID = Guid.NewGuid();
                                da.DeviceID = deviceInfor.ID;
                                da.DeviceNo = deviceInfor.DeviceNo;
                                da.StartTime = item.SateTimeMark;
                                if (drt.Temperature1.Value > deviceInfor.Temperature1HighAlarm.Value)
                                {
                                    //超高报警
                                    da.EventType = 1;
                                }
                                else
                                {
                                    //超低报警
                                    da.EventType = 2;
                                }
                                da.EventLevel = 1;
                                DataContext.DeviceAlarms.InsertOnSubmit(da);
                            }
                        }
                    }
                    DataContext.SubmitChanges();
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 保存用户事件数据
        /// </summary>
        /// <param name="realTimeData_R"></param>
        /// <returns></returns>
        public bool SaveUserEvent(UserEvent_R userEvent_R)
        {
            //逻辑较复杂些
            DeviceInfo deviceInfor = DataContext.DeviceInfos.SingleOrDefault(e => e.DeviceSN == userEvent_R.Header.DeviceSN);
            if (deviceInfor != null)
            {
                var obj = DataContext.UserEvents.SingleOrDefault(e => e.DeviceID == deviceInfor.ID && e.State == 1 && e.EventType == userEvent_R.WorkstateChild);
                if (obj != null)
                {
                    obj.Count = obj.Count.GetValueOrDefault(0) + 1;
                    obj.RequestTime = DateTime.Now;
                }
                else
                {
                    UserEvent ue = new UserEvent();
                    ue.ID = Guid.NewGuid();
                    ue.EventNo = "eventNo" + DataContext.UserEvents.Count().ToString();
                    ue.DeviceID = deviceInfor.ID;
                    ue.DeviceNo = deviceInfor.DeviceNo;
                    ue.RequestTime = DateTime.Now;
                    ue.State = 1;
                    ue.Count = 1;
                    ue.EventType = userEvent_R.WorkstateChild;
                    DataContext.UserEvents.InsertOnSubmit(ue);
                }
                DataContext.SubmitChanges();
                return true;
            }
            return false;
        }
    }
}

//需要讨论的问题2012-2-4
/*
天气预报信息和广播信息往哪里存储
设备属性如果有更新，该如何标记
*/