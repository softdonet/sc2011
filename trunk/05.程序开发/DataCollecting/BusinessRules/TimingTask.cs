using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccess;
using Utility;

namespace BusinessRules
{
    /// <summary>
    /// 检测设备离线
    /// yanghk at 2012-7-11
    /// </summary>
    public class TimingTask
    {
        SCADADataContext DataContext = new SCADADataContext();
        private RealTimeNotify realTimeNotify = new RealTimeNotify();
        public void OffLineCheck()
        {
            //设备状态：1：正常 2：离线 3：告警
            foreach (var item in DataContext.DeviceInfos)
            {
                bool isOffLine = false;
                float precision = 1.5f;//误差倍率
                if (item.DeviceRealTimes.Where(e => e.Status.Value != 2).OrderByDescending(e => e.UpdateTime).Any())
                {
                    var maxRealtimeDev = item.DeviceRealTimes.Where(e => e.Status.Value != 2).OrderByDescending(e => e.UpdateTime).First();
                    if (maxRealtimeDev != null)
                    {
                        TimeSpan ts = DateTime.Now - maxRealtimeDev.UpdateTime.Value;
                        //获取当前设备的运行模式
                        int model = item.CurrentModel.GetValueOrDefault(1);
                        switch (model)
                        {
                            case 1:
                                //实时模式
                                //如果时间差>实时采集周期,则表示离线
                                if (ts.TotalSeconds > item.RealTimeParam.GetValueOrDefault(0) * precision)
                                {
                                    LogHelper.WriteInformationLog(
                                        string.Format("编号:{0},模式：实时，间隔{1}秒，{2}", item.DeviceNo, ts.TotalSeconds.ToString(),maxRealtimeDev.UpdateTime.Value.ToString()));
                                    isOffLine = true;
                                }
                                break;
                            case 2:
                                //整点模式
                                //如果时间差>通讯发送周期周期,则表示离线
                                if (ts.TotalMinutes > item.FullTimeParam2.GetValueOrDefault(0) * precision)
                                {
                                    LogHelper.WriteInformationLog(
                                      string.Format("编号:{0},模式：整点，间隔{1}分钟，{2}", item.DeviceNo, ts.TotalMinutes.ToString(), maxRealtimeDev.UpdateTime.Value.ToString()));
                                    isOffLine = true;
                                }
                                break;
                            case 3:
                                //整点模式
                                //如果时间差>最小发送间隔,则表示离线
                                if (ts.TotalMinutes > item.OptimizeParam2.GetValueOrDefault(0) * precision)
                                {
                                    LogHelper.WriteInformationLog(
                                      string.Format("编号:{0},模式：优化，间隔{1}分钟，{2}", item.DeviceNo, ts.TotalMinutes.ToString(), maxRealtimeDev.UpdateTime.Value.ToString()));
                                    isOffLine = true;
                                }
                                break;
                            default:
                                //没有配置模式，则默认为实时模式
                                if (ts.TotalSeconds > item.RealTimeParam.GetValueOrDefault(0) * precision)
                                {
                                    isOffLine = true;
                                }
                                break;
                        };
                    }
                    else
                    {
                        //如果没有实时数据，则直接标记为离线
                        isOffLine = true;
                    }
                }
                else
                {
                    //如果没有实时数据，则直接标记为离线
                    isOffLine = true;
                }

                if (isOffLine)
                {

                    //写入一条离线数据
                    DeviceRealTime drt = new DeviceRealTime();
                    drt.ID = Guid.NewGuid();
                    drt.DeviceID = item.ID;
                    drt.DeviceNo = item.DeviceNo;

                    //温度1
                    drt.Temperature1 = 0;
                    //温度2
                    drt.Temperature2 = 0;
                    //信号
                    drt.Signal = 0;
                    //湿度
                    drt.Humidity = 0;
                    //电量
                    drt.Electricity = 0;
                    //设备状态离线
                    drt.Status = 2;
                    drt.UpdateTime = DateTime.Now;
                    DataContext.DeviceRealTimes.InsertOnSubmit(drt);
                }
            }
            DataContext.SubmitChanges();
            realTimeNotify.Notify(MessageType.RealTimeMsg);
        }
    }
}

