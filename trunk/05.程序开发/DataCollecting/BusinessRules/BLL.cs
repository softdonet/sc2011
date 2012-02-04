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

            //请根据系统配置参数对告警进行处理
            SCADADataContext DataContext = new SCADADataContext();
            DeviceInfo deviceInfor = DataContext.DeviceInfos.SingleOrDefault(e => e.DeviceNo == realTimeData_R.Header.DeviceSN);
            foreach (RealTimeDataBlock item in realTimeData_R.RealTimeDataBlocks)
            {
                DeviceRealTime drt = new DeviceRealTime();
                drt.ID = Guid.NewGuid();
                drt.DeviceID = deviceInfor.ID;
                drt.DeviceNo = deviceInfor.DeviceNo;
                //此处和数据库不太一样，需要调整和修改
                //drt.Temperature =item.Temperature1;
                //drt.Electricity =item.Electric;
                //drt.Signal =item.Signal;
                drt.UpdateTime = item.SateTimeMark;
                DataContext.DeviceRealTimes.Attach(drt);
                //TODO:判断告警的逻辑
                if (true)
                {
                    DeviceAlarm da = new DeviceAlarm();
                    da.ID = Guid.NewGuid();
                    DataContext.DeviceAlarms.Attach(da);
                }
            }

            DataContext.SubmitChanges();
            return true;
        }

        /// <summary>
        /// 保存用户事件数据
        /// </summary>
        /// <param name="realTimeData_R"></param>
        /// <returns></returns>
        public bool SaveUserEvent(UserEvent_R userEvent_R)
        {
            //逻辑较复杂些
            return true;
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