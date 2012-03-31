using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Scada.Client.VM.Modules.Query;
using Scada.Model.Entity;
using Scada.Utility.Common.SL;
using Scada.Model.Entity.Enums;

namespace Scada.Client.SL.Modules.Query
{
    public partial class AlarmQuery : UserControl
    {
        private static AlarmQuery instance;
        public static AlarmQuery GetInstance()
        {
            if (instance == null)
            {
                instance = new AlarmQuery();
            }
            return instance;
        }

        AlarmQueryViewModel alarmQueryViewModel;
        public AlarmQuery()
        {
            InitializeComponent();

            LoadEventType();

            alarmQueryViewModel = new AlarmQueryViewModel();
            this.DataContext = alarmQueryViewModel;
            alarmQueryViewModel.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(alarmQueryViewModel_PropertyChanged);
        }

        void alarmQueryViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName=="DeviceTreeSource")
            {
                this.comboBoxTreeView1.Source = alarmQueryViewModel.DeviceTreeSource;
            }
        }

        #region 初始化基本信息
        private void LoadEventType()
        {
            cmbEventType.Items.Clear();
            List<DeviceAlarm> deviceAlarmList = new List<DeviceAlarm>();
            //故障，超高，超低
            deviceAlarmList.Add(new DeviceAlarm() { EventType=1, EventTypeName=EnumHelper.Display<EventTypes>(1)});
            deviceAlarmList.Add(new DeviceAlarm() { EventType = 2, EventTypeName = EnumHelper.Display<EventTypes>(2) });
            deviceAlarmList.Add(new DeviceAlarm() { EventType = 3, EventTypeName = EnumHelper.Display<EventTypes>(3) });
            cmbEventType.ItemsSource = deviceAlarmList;
        }
        #endregion
    }
}
