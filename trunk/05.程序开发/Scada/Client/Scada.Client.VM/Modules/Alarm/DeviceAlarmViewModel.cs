using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Practices.Prism.ViewModel;
using System.Collections.Generic;
using Scada.Model.Entity;

namespace Scada.Client.VM.Modules.Alarm
{
    /// <summary>
    /// 数据实时列表：告警信息
    /// </summary>
    public class DeviceAlarmViewModel:NotificationObject
    {
        #region Variable

        private List<DeviceAlarm> deviceAlarm;

        #endregion

        #region Constructor

        public DeviceAlarmViewModel()
        {

        }

        #endregion
       

    }
}
