using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Scada.DAL.Linq;

namespace Scada.BLL.Implement
{
    /// <summary>
    /// 设备状态帮助类
    /// </summary>
    public class DeviceStateHelper
    {
        private static SCADADataContext sCADtADataContext = new SCADADataContext();
        public static bool CheckOffLineState(Guid devID)
        {
            //设备状态：1：正常 2：离线 3：告警
            var item = sCADtADataContext.DeviceInfos.SingleOrDefault(e => e.ID == devID);
            bool isOffLine = false;
            float precision = 1.1f;//误差倍率
            if (item.DeviceRealTimes.Any())
            {
                DateTime? maxdt = item.DeviceRealTimes.Max(e => e.UpdateTime);
                var maxRealtimeDev = item.DeviceRealTimes.Where(e => e.UpdateTime == maxdt).First();
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
                            isOffLine = true;
                        }
                        break;
                    case 2:
                        //整点模式
                        //如果时间差>通讯发送周期周期,则表示离线
                        if (ts.TotalMinutes > item.FullTimeParam2.GetValueOrDefault(0) * precision)
                        {
                            isOffLine = true;
                        }
                        break;
                    case 3:
                        //优化模式
                        //如果时间差>最小发送间隔,则表示离线
                        if (ts.TotalMinutes > item.OptimizeParam2.GetValueOrDefault(0) * precision)
                        {
                            isOffLine = true;
                        }
                        break;
                    default:
                        break;
                };
            }
            return isOffLine;
        }
    }
}
