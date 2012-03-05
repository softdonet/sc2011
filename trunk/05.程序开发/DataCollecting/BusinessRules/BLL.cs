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
             *     成功则返回true
             *     没有找到或者执行失败返回false
            */
            SCADADataContext DataContext = new SCADADataContext();
            var obj = DataContext.DeviceInfos.SingleOrDefault(e => e.DeviceNo == dc_r.Header.DeviceSN);
            if (obj != null)
            {
                obj.DeviceMAC = dc_r.MAC;
                obj.SIMNo = dc_r.SIM;
                obj.HardType = dc_r.DeviveType;
                obj.Version = string.Format("{0}.{1}", dc_r.HardwareVersionMain.ToString(), dc_r.HardwareVersionChild.ToString());
                //TODO:数据库字段不全。其他相关属性
                DataContext.SubmitChanges();
                return true;
            }
            else
            {
                return false;
            }
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
            configDataBlock.DisplayMode = 1;
            configDataBlock.InstancyBtnEnable = true;
            configDataBlock.InfoBtnEnable = true;
            configDataBlock.RepairTel = "13801112222";
            configDataBlock.MainDNS = "202.106.42.1";
            configDataBlock.ReserveDNS = "202.106.42.2";
            configDataBlock.ServerIP = "192.168.0.1";
            configDataBlock.DomainName = "xyz.dddd.com";
            configDataBlock.Port = 1789;
            rs.ConfigData = configDataBlock;

            //构造天气预报块
            WeatherDataBlock weatherDataBlock = new WeatherDataBlock();
            weatherDataBlock.TodayMark = 0;
            weatherDataBlock.TodayWeather = "晴转多云，有阵雨。";
            weatherDataBlock.TomorrowMark = 1;
            weatherDataBlock.TomorrowWeather = "温度很低，有大雪，注意防寒。";
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

            //TODO:请根据系统配置参数对告警进行处理
            DeviceInfo deviceInfor = DataContext.DeviceInfos.SingleOrDefault(e => e.DeviceSN == realTimeData_R.Header.DeviceSN);
            if (deviceInfor != null)
            {
                foreach (RealTimeDataBlock item in realTimeData_R.RealTimeDataBlocks)
                {
                    DeviceRealTime drt = new DeviceRealTime();
                    drt.ID = Guid.NewGuid();
                    drt.DeviceID = deviceInfor.ID;
                    drt.DeviceNo = deviceInfor.DeviceNo;
                    //此处和数据库不太一样，需要调整和修改
                    //---------------------------------------------------
                    //温度
                    drt.Temperature = (double)item.Temperature1;
                     //湿度  需要添加字段
                    //信号
                    //0---31  5以下不能用  界面量程（信号(0--4)）
                  


                    if (item.Signal.HasValue)
                    {
                        if (item.Signal.Value < 5)
                        {
                            drt.Signal = 0;
                        }
                        else if (item.Signal.Value > 5 && item.Signal.Value < 10)
                        {
                            drt.Signal = 1;
                        }
                        else if (item.Signal.Value > 10 && item.Signal.Value < 15)
                        {
                            drt.Signal = 2;
                        }
                        else if (item.Signal.Value > 15 && item.Signal.Value < 25)
                        {
                            drt.Signal = 3;
                        }
                        else if (item.Signal.Value>25)
                        {
                            drt.Signal = 4;
                        }
                    }
                    else
                    {
                        drt.Signal = 0;
                    }

                    //电量 0--400 //界面量程(1--3)
                    if (item.Electric.HasValue)
                    {
                        if (item.Electric.Value < 50)
                        {
                            drt.Electricity = 0;
                        }
                        else if (item.Electric.Value > 50 && item.Electric.Value < 200)
                        {
                            drt.Electricity = 1;
                        }
                        else if (item.Electric.Value > 200 && item.Electric.Value < 300)
                        {
                            drt.Electricity = 2;
                        }
                        else if (item.Electric.Value > 300 )
                        {
                            drt.Electricity = 3;
                        }
                    }
                    else
                    {
                        drt.Electricity = 0;
                    }

                    //设备状态
                    drt.Status = 1;//设备状态
                    drt.UpdateTime = DateTime.Now;//item.SateTimeMark;
                    DataContext.DeviceRealTimes.InsertOnSubmit(drt);
                    //判断告警的逻辑
                    if (drt.Temperature.Value > 21 || drt.Temperature.Value < 16)
                    {
                        drt.Status = 3;//设备告警
                        DeviceAlarm da = new DeviceAlarm();
                        da.ID = Guid.NewGuid();
                        da.DeviceID = deviceInfor.ID;
                        da.DeviceNo = deviceInfor.DeviceNo;
                        da.StartTime = DateTime.Now;//item.SateTimeMark;
                        if (drt.Temperature.Value > 50)
                        {
                            //超高
                            da.EventType = 1;
                        }
                        else
                        {
                            //超低
                            da.EventType = 2;
                        }
                        da.EventLevel = 1;
                        DataContext.DeviceAlarms.InsertOnSubmit(da);
                    }
                }
                DataContext.SubmitChanges();
                return true;
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
 * 
 * 温度、电量 湿度 存储的类型不太一直
 * 设备SN号在数据库中貌似没有存储
 * 需要重新生成数据库结构，添加了部门表的自关联、修改了硬件版本和软件版本字段
*/