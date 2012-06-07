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
using Scada.Utility.Common.SL.Enums;
using Scada.Model.Entity.Enums;
using Telerik.Windows.Controls;
using System.IO;

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

        private void btnExport_Click(object sender, RoutedEventArgs e)
        {
            string extension = "xls"; ;
            ExportFormat format = ExportFormat.Html;
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.DefaultExt = extension;
            dialog.Filter = String.Format("{1} files (*.{0})|*.{0}|All files (*.*)|*.*", extension, "Excel");
            dialog.FilterIndex = 1;
            if (dialog.ShowDialog() == true)
            {
                using (Stream stream = dialog.OpenFile())
                {
                    GridViewExportOptions exportOptions = new GridViewExportOptions();
                    exportOptions.Format = format;
                    exportOptions.ShowColumnFooters = false;
                    exportOptions.ShowColumnHeaders = true;
                    exportOptions.ShowGroupFooters = true;
                    RadGridView1.Export(stream, exportOptions);
                }
            }
        }

        private void RadGridView1_Exporting(object sender, GridViewExportEventArgs e)
        {
            if (e.Element == ExportElement.HeaderRow)
            {
                e.FontWeight = FontWeights.Bold;
                e.TextAlignment = TextAlignment.Center;
                e.Background = Colors.Gray;
            }
        }
    }
}
