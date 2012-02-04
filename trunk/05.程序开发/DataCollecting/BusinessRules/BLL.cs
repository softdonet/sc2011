using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NetData;

namespace BusinessRules
{
    public class BLL
    {
        /// <summary>
        /// 更新设备
        /// </summary>
        /// <param name="dc_r"></param>
        /// <returns></returns>
        public bool RefreshData(RequestBase_R dc_r)
        {    /*业务逻辑：
             根据设备SN号在数据库中查找进行对比。
                    如果有该设备，则更新设备属性
                    {
                         MAC地址
                         SIM地址
                         产品型号长度
                         产品型号
                         硬件版本
                         固件版本
                         工作状态
                     }
              *     成功则返回true
              *     没有找到或者执行失败返回false
             */
            return true;
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
            weatherDataBlock.TodayWeather = "晴转XXX多云，有阵雨。";
            weatherDataBlock.TomorrowMark = 1;
            weatherDataBlock.TomorrowWeather = "温度很低，有大雪，注意防寒。";
            rs.WeatherData = weatherDataBlock;
            
            //构造广播信息块
            BroadcastDataBlock broadcastDataBlock = new BroadcastDataBlock();
            broadcastDataBlock.Msg = "通知：明天下午开会。不要迟到，知道一分钟扣100元。";
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
            return true;
        }

        /// <summary>
        /// 保存用户事件数据
        /// </summary>
        /// <param name="realTimeData_R"></param>
        /// <returns></returns>
        public bool SaveUserEvent(UserEvent_R userEvent_R)
        {
            return true;
        }
    }
}
