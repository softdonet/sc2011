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
using Scada.Model.Entity;

namespace Scada.Client.VM.Modules.Alarm
{
    /// <summary>
    /// 告警实体类VM层
    /// </summary>
    public class DeviceAlarmViewModel : NotificationObject
    {
        public DeviceAlarm DeviceAlarm { get; set; }
        /// <summary>
        /// 告警备注
        /// </summary>
        public string Comment
        {
            get { return DeviceAlarm.Comment; }
            set
            {
                if (DeviceAlarm.Comment != value)
                {
                    DeviceAlarm.Comment = value;
                    RaisePropertyChanged("Comment");
                }
            }
        }
    }
}
