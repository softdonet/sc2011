using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NetData;
using DataAccess;
using System.ServiceModel;
using System.ServiceModel.Channels;
using Utility;


namespace BusinessRules
{

    /// <summary>
    /// 消息类别
    /// </summary>
    public enum MessageType
    {
        RealTimeMsg = 0,
        AlarmMsg = 1,
        UserEvent = 2
    }

    /// <summary>
    /// 业务逻辑类
    /// yanghk at 2012-2-4
    /// </summary>
    public class BLL
    {
        SCADADataContext DataContext = new SCADADataContext();
        private RealTimeNotify realTimeNotify = new RealTimeNotify();
      
        /// <summary>
        /// 更新设备
        /// </summary>
        /// <param name="dc_r"></param>
        /// <returns></returns>
        public bool RefreshData(RequestBase_R dc_r)
        {
            /*业务逻辑：
            根据设备SN号在数据库中查找进行对比。
                   如果有该设备，则更新设备属性
                   {
                        MAC地址
                        SIM地址
                        产品型号
                        硬件版本
                        固件版本
                        工作状态
                    }
                  成功则返回true
                  没有找到或者执行失败返回false
             */

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
                        dc_r.SoftwareVersionChild);
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
            //如果设备配置有更新。责回复
            //天气预报和广播每次都回复
            //则回复配置信息+天气预报+广播信息，并标记。
            ReplyBase_S rs = new ReplyBase_S();
            SCADADataContext DataContext = new SCADADataContext();
            DeviceInfo deviceInfor = DataContext.DeviceInfos.SingleOrDefault(e => e.DeviceSN == deviceSN);
            if (deviceInfor != null)
            {
                rs.HaveBroadcastInfo = true;
                rs.HaveWeatherInfo = true;
                rs.HaveConfigInfo = deviceInfor.IsNew;
                //系统公共参数
                PublicParameter objParameter = DataContext.PublicParameters.First();
                if (deviceInfor.IsNew)
                {
                    //构造设备配置信息块
                    ConfigDataBlock configDataBlock = new ConfigDataBlock();
                    configDataBlock.RunMode = (byte)deviceInfor.CurrentModel.Value;
                    switch (configDataBlock.RunMode)
                    {
                        case 1:
                            configDataBlock.Argument1 = (ushort)deviceInfor.RealTimeParam.Value;
                            configDataBlock.Argument2 = 0;
                            configDataBlock.Argument3 = 0;
                            break;
                        case 2:
                            configDataBlock.Argument1 = (ushort)deviceInfor.FullTimeParam1.Value;
                            configDataBlock.Argument2 = (ushort)deviceInfor.FullTimeParam2.Value;
                            configDataBlock.Argument3 = 0;
                            break;
                        case 3:
                            configDataBlock.Argument1 = (ushort)deviceInfor.OptimizeParam1.Value;
                            configDataBlock.Argument2 = (ushort)deviceInfor.OptimizeParam2.Value;
                            configDataBlock.Argument3 = (ushort)deviceInfor.OptimizeParam3.Value;
                            break;
                        default:
                            configDataBlock.Argument1 = 0;
                            configDataBlock.Argument2 = 0;
                            configDataBlock.Argument3 = 0;
                            break;
                    }
                    configDataBlock.DeviceNo = deviceInfor.DeviceNo;
                    configDataBlock.InstalPlace = deviceInfor.InstallPlace;
                    configDataBlock.DisplayMode = (byte)deviceInfor.LCDScreenDisplayType.Value;
                    configDataBlock.InstancyBtnEnable = deviceInfor.UrgencyBtnEnable.GetValueOrDefault(false);
                    configDataBlock.InfoBtnEnable = deviceInfor.InforBtnEnable.GetValueOrDefault(false);
                    configDataBlock.RepairTel = deviceInfor.MaintenancePeople.Mobile;
                    configDataBlock.DomainName = objParameter.Domain;
                    configDataBlock.MainIP = objParameter.MainDNS;
                    configDataBlock.ReserveIP = objParameter.SecondDNS;
                    configDataBlock.Port = (ushort)objParameter.Port.Value;
                    configDataBlock.ConnectionType = (byte)objParameter.ConnectType.Value;
                    configDataBlock.ConnectName = objParameter.ConnectName;
                    rs.ConfigData = configDataBlock;
                    //将设备更新标记置为false
                    deviceInfor.IsNew = false;
                    DataContext.SubmitChanges();
                }
                //构造天气预报块
                WeatherDataBlock weatherDataBlock = new WeatherDataBlock()
                {
                    TodayWeather = objParameter.Weather
                };
                rs.WeatherData = weatherDataBlock;

                //构造广播信息块
                BroadcastDataBlock broadcastDataBlock = new BroadcastDataBlock()
                {
                    Msg = objParameter.Broadcast
                };
                rs.BroadcastData = broadcastDataBlock;
            }
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
                bool haveAlarm = false;
                if (deviceInfor != null)
                {
                    //当前时间
                    DateTime curTime = DateTime.Now; //item.SateTimeMark;
                    //当前模式（1实时模式；2整点模式；3逢变则报模式）
                    int curMode = deviceInfor.CurrentModel.Value;
                    foreach (RealTimeDataBlock item in realTimeData_R.RealTimeDataBlocks)
                    {
                        DateTime nowTime;
                        //如果是整点模式，计算采集时间 =当前时间-（块序号*整点模式采集频率*60）
                        if (curMode == 2)
                        {
                            double delay = (item.BlockNo - 1) * deviceInfor.FullTimeParam1.Value * 60 * (-1.0);
                            nowTime = curTime.AddSeconds(delay);
                        }
                        else
                        {
                            nowTime = curTime;
                        }
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
                        drt.UpdateTime = nowTime;
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
                                da.StartTime = nowTime;
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
                                haveAlarm = true;
                            }
                        }
                    }
                    DataContext.SubmitChanges();
                    realTimeNotify.Notify(MessageType.RealTimeMsg);
                    if (haveAlarm)
                    {
                        realTimeNotify.Notify(MessageType.AlarmMsg);
                    }
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
                    ue.EventNo = DateTime.Now.Date.ToString("yyMMdd") + DataContext.UserEvents.Where(e => e.RequestTime.Value.Date == DateTime.Now.Date).Count().ToString("0000");
                    ue.DeviceID = deviceInfor.ID;
                    ue.DeviceNo = deviceInfor.DeviceNo;
                    ue.RequestTime = DateTime.Now;
                    ue.State = 1;
                    ue.Count = 1;
                    ue.EventType = userEvent_R.WorkstateChild;
                    DataContext.UserEvents.InsertOnSubmit(ue);
                }
                DataContext.SubmitChanges();
                realTimeNotify.Notify(MessageType.UserEvent);
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