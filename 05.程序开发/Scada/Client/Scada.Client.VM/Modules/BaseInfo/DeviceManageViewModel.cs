using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Collections.Generic;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Practices.Prism.ViewModel;
using Scada.Client.VM.DeviceRealTimeService;
using Scada.Client.VM.ScadaDeviceService;
using Scada.Model.Entity;
using Scada.Client.VM.CommClass;
using System.Collections.ObjectModel;
using Microsoft.Practices.Prism.Commands;

namespace Scada.Client.VM.Modules.BaseInfo
{
    public class DeviceManageViewModel : NotificationObject
    {
        public DelegateCommand AddCommand { get; set; }
        public DelegateCommand UpdateCommand { get; set; }
        public DelegateCommand DeleteCommand { get; set; }
        private ScadaDeviceServiceSoapClient scadaDeviceServiceSoapClient = null;
        DeviceInfo deviceInfo;
        string myDeviceId;
        public DeviceManageViewModel()
        {
            scadaDeviceServiceSoapClient = ServiceManager.GetScadaDeviceService();

            scadaDeviceServiceSoapClient.ListDeviceTreeViewCompleted += new EventHandler<ListDeviceTreeViewCompletedEventArgs>(scadaDeviceServiceSoapClient_ListDeviceTreeViewCompleted);
            scadaDeviceServiceSoapClient.ListDeviceTreeViewAsync();

            //scadaDeviceServiceSoapClient.AddDeviceInfoCompleted += new EventHandler<AddDeviceInfoCompletedEventArgs>(scadaDeviceServiceSoapClient_AddDeviceInfoCompleted);

            //this.AddCommand = new DelegateCommand(new Action(this.AddDeviceInfo));

        }

        public DeviceManageViewModel(string DeviceId)
        {
            //查看设备树
            myDeviceId = DeviceId;
            scadaDeviceServiceSoapClient = ServiceManager.GetScadaDeviceService();
            scadaDeviceServiceSoapClient.ViewDeviceInfoCompleted += new EventHandler<ViewDeviceInfoCompletedEventArgs>(scadaDeviceServiceSoapClient_ViewDeviceInfoCompleted);
            scadaDeviceServiceSoapClient.ViewDeviceInfoAsync(myDeviceId);

            //添加设备
            scadaDeviceServiceSoapClient.AddDeviceInfoCompleted += new EventHandler<AddDeviceInfoCompletedEventArgs>(scadaDeviceServiceSoapClient_AddDeviceInfoCompleted);
            this.AddCommand = new DelegateCommand(new Action(this.AddDeviceInfo));
            
            //修改设备
            this.scadaDeviceServiceSoapClient.UpdateDeviceAlarmInfoCompleted += new EventHandler<UpdateDeviceAlarmInfoCompletedEventArgs>(scadaDeviceServiceSoapClient_UpdateDeviceAlarmInfoCompleted);
            this.UpdateCommand = new DelegateCommand(new Action(this.UpdateDeviceInfo));
            
            //删除设备
            this.scadaDeviceServiceSoapClient.DeleteDeviceInfoCompleted += new EventHandler<DeleteDeviceInfoCompletedEventArgs>(scadaDeviceServiceSoapClient_DeleteDeviceInfoCompleted);
            this.DeleteCommand = new DelegateCommand(new Action(this.DeleteDeviceInfo));
        }


        void scadaDeviceServiceSoapClient_AddDeviceInfoCompleted(object sender, AddDeviceInfoCompletedEventArgs e)
        {
            bool flag = e.Result;
            if (flag)
            {
                MessageBox.Show("添加新设备成功!");
                //scadaDeviceServiceSoapClient.ListDeviceTreeViewAsync();
            }
            else
            {
                MessageBox.Show("添加新设备失败!");
            }
        }
        private void AddDeviceInfo()
        {

            //--------------------
            DeviceInfoList.ID = Guid.NewGuid();
            //string deviceInfo = BinaryObjTransfer.BinarySerialize(addDevice);
            string deviceInfo = BinaryObjTransfer.BinarySerialize(DeviceInfoList);
            scadaDeviceServiceSoapClient.AddDeviceInfoAsync(deviceInfo);

        }

        void scadaDeviceServiceSoapClient_UpdateDeviceAlarmInfoCompleted(object sender, UpdateDeviceAlarmInfoCompletedEventArgs e)
        {
            bool flag = e.Result;
            if (flag)
            {
                MessageBox.Show("修改设备信息成功!");
            }
            else
            {
                MessageBox.Show("修改设备信息失败!");
            }
        }

        private void UpdateDeviceInfo()
        {
            string deviceInfo = BinaryObjTransfer.BinarySerialize(DeviceInfoList);
            scadaDeviceServiceSoapClient.UpdateDeviceInfoAsync(deviceInfo);
        }

        void scadaDeviceServiceSoapClient_DeleteDeviceInfoCompleted(object sender, DeleteDeviceInfoCompletedEventArgs e)
        {
            bool flag = e.Result;
            if (flag)
            {
                MessageBox.Show("删除设备信息成功!");
            }
            else
            {
                MessageBox.Show("删除设备信息失败!");
            }
        }

        private void DeleteDeviceInfo()
        {
            scadaDeviceServiceSoapClient.DeleteDeviceInfoAsync(DeviceInfoList.ID);
        }

        #region 设备树

        void scadaDeviceServiceSoapClient_ListDeviceTreeViewCompleted(object sender, ListDeviceTreeViewCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                List<DeviceTreeNode> result = BinaryObjTransfer.BinaryDeserialize<List<DeviceTreeNode>>(e.Result);
                DeviceTreeNodeList = result;
            }
        }

        private List<DeviceTreeNode> deviceTreeNodeList;

        public List<DeviceTreeNode> DeviceTreeNodeList
        {
            get { return deviceTreeNodeList; }
            set
            {
                deviceTreeNodeList = value;
                this.RaisePropertyChanged("DeviceTreeNodeList");
            }
        }

        #endregion

        void scadaDeviceServiceSoapClient_ViewDeviceInfoCompleted(object sender, ViewDeviceInfoCompletedEventArgs e)
        {
            string result = e.Result;
            if (e.Error == null)
            {
                deviceInfo = BinaryObjTransfer.BinaryDeserialize<DeviceInfo>(e.Result);
                DeviceInfoList = deviceInfo;
            }
        }

        /// <summary>
        /// 选择的设备
        /// </summary>
        private DeviceTreeNode selectDeviceTreeNode;
        public DeviceTreeNode SelectDeviceTreeNode
        {
            get { return selectDeviceTreeNode; }
            set
            {
                selectDeviceTreeNode = value;
                this.RaisePropertyChanged("SelectDeviceTreeNode");
            }
        }
        /// <summary>
        /// 设备信息
        /// </summary>
        private DeviceInfo deviceInfoList;
        public DeviceInfo DeviceInfoList
        {
            get { return deviceInfoList; }
            set
            {
                deviceInfoList = value;
                this.RaisePropertyChanged("DeviceInfoList");
            }
        }

        //---------------------------------------------------------------
        #region 设备属性

        private Guid id;
        public Guid ID
        {
            get { return id; }
            set
            {
                id = value;
                this.RaisePropertyChanged("ID");
            }
        }

        private string deviceNo;
        public string DeviceNo
        {
            get { return deviceNo; }
            set
            {
                deviceNo = value;
                this.RaisePropertyChanged("DeviceNo");
            }
        }

        private string deviceMAC;
        public string DeviceMAC
        {
            get { return deviceMAC; }
            set
            {
                deviceMAC = value;
                this.RaisePropertyChanged("DeviceMAC");
            }
        }
        //设备SN号
        private string deviceSN;
        public string DeviceSN
        {
            get { return deviceSN; }
            set
            {
                deviceSN = value;
                this.RaisePropertyChanged("DeviceSN");
            }
        }

        //硬件类型
        private string hardType;
        public string HardType
        {
            get { return hardType; }
            set
            {
                hardType = value;
                this.RaisePropertyChanged("HardType");
            }
        }
        //生产日期
        private DateTime productDate;
        public DateTime ProductDate
        {
            get { return productDate; }
            set
            {
                productDate = value;
                this.RaisePropertyChanged("ProductDate");
            }
        }

        private string sIMNo;
        public string SIMNo
        {
            get { return sIMNo; }
            set
            {
                sIMNo = value;
                this.RaisePropertyChanged("SIMNo");
            }
        }

        //管理分区
        private Guid manageAreaID;
        public Guid ManageAreaID
        {
            get { return manageAreaID; }
            set
            {
                manageAreaID = value;
                this.RaisePropertyChanged("ManageAreaID");
            }
        }


        //维修人员ID
        private Guid maintenancePeopleID;
        public Guid MaintenancePeopleID
        {
            get { return maintenancePeopleID; }
            set
            {
                maintenancePeopleID = value;
                this.RaisePropertyChanged("MaintenancePeopleID");
            }
        }

        //安装地点
        private string installPlace;
        public string InstallPlace
        {
            get { return installPlace; }
            set
            {
                installPlace = value;
                this.RaisePropertyChanged("InstallPlace");
            }
        }

        //经度
        private decimal longitude;
        public decimal Longitude
        {
            get { return longitude; }
            set
            {
                longitude = value;
                this.RaisePropertyChanged("Longitude");
            }
        }

        //维度
        private decimal latitude;
        public decimal Latitude
        {
            get { return latitude; }
            set
            {
                latitude = value;
                this.RaisePropertyChanged("Latitude");
            }
        }

        private decimal high;
        public decimal High
        {
            get { return high; }
            set
            {
                high = value;
                this.RaisePropertyChanged("High");
            }
        }

        private string comment;
        public string Comment
        {
            get { return comment; }
            set
            {
                comment = value;
                this.RaisePropertyChanged("Comment");
            }
        }

        //偏差
        private int windage;
        public int Windage
        {
            get { return windage; }
            set
            {
                windage = value;
                this.RaisePropertyChanged("Windage");
            }
        }

        //硬件版本
        private string hardwareVersion;
        public string HardwareVersion
        {
            get { return hardwareVersion; }
            set
            {
                hardwareVersion = value;
                this.RaisePropertyChanged("HardwareVersion");
            }
        }

        //软件版本
        private string softWareVersion;
        public string SoftWareVersion
        {
            get { return softWareVersion; }
            set
            {
                softWareVersion = value;
                this.RaisePropertyChanged("SoftWareVersion");
            }
        }

        //LCD显示类型
        private int lCDScreenDisplayType;
        public int LCDScreenDisplayType
        {
            get { return lCDScreenDisplayType; }
            set
            {
                lCDScreenDisplayType = value;
                this.RaisePropertyChanged("LCDScreenDisplayType");
            }
        }

        private bool urgencyBtnEnable;
        public bool UrgencyBtnEnable
        {
            get { return urgencyBtnEnable; }
            set
            {
                urgencyBtnEnable = value;
                this.RaisePropertyChanged("UrgencyBtnEnable");
            }
        }

        private bool inforBtnEnable;
        public bool InforBtnEnable
        {
            get { return inforBtnEnable; }
            set
            {
                inforBtnEnable = value;
                this.RaisePropertyChanged("InforBtnEnable");
            }
        }

        //主温度设置有效

        private bool temperature1AlarmValid;
        public bool Temperature1AlarmValid
        {
            get { return temperature1AlarmValid; }
            set
            {
                temperature1AlarmValid = value;
                this.RaisePropertyChanged("Temperature1AlarmValid");
            }
        }

        //主温度高报警
        private decimal temperature1HighAlarm;
        public decimal Temperature1HighAlarm
        {
            get { return temperature1HighAlarm; }
            set
            {
                temperature1HighAlarm = value;
                this.RaisePropertyChanged("Temperature1HighAlarm");
            }
        }

        //主温度低报警
        private decimal temperature1LowAlarm;
        public decimal Temperature1LowAlarm
        {
            get { return temperature1LowAlarm; }
            set
            {
                temperature1LowAlarm = value;
                this.RaisePropertyChanged("Temperature1LowAlarm");
            }
        }

        //从温度设置有效
        private bool temperature2AlarmValid;
        public bool Temperature2AlarmValid
        {
            get { return temperature2AlarmValid; }
            set
            {
                temperature2AlarmValid = value;
                this.RaisePropertyChanged("Temperature2AlarmValid");
            }
        }

        //从温度高报警
        private decimal temperature2HighAlarm;
        public decimal Temperature2HighAlarm
        {
            get { return temperature2HighAlarm; }
            set
            {
                temperature2HighAlarm = value;
                this.RaisePropertyChanged("Temperature2HighAlarm");
            }
        }

        //从温度低报警
        private decimal temperature2LowAlarm;
        public decimal Temperature2LowAlarm
        {
            get { return temperature2LowAlarm; }
            set
            {
                temperature2LowAlarm = value;
                this.RaisePropertyChanged("Temperature2LowAlarm");
            }
        }

        //湿度设置有效
        private bool humidityAlarmValid;
        public bool HumidityAlarmValid
        {
            get { return humidityAlarmValid; }
            set
            {
                humidityAlarmValid = value;
                this.RaisePropertyChanged("HumidityAlarmValid");
            }
        }

        //湿度高报警
        private decimal humidityHighAlarm;
        public decimal HumidityHighAlarm
        {
            get { return humidityHighAlarm; }
            set
            {
                humidityHighAlarm = value;
                this.RaisePropertyChanged("HumidityHighAlarm");
            }
        }

        //湿度低报警
        private decimal humidityLowAlarm;
        public decimal HumidityLowAlarm
        {
            get { return humidityLowAlarm; }
            set
            {
                humidityLowAlarm = value;
                this.RaisePropertyChanged("HumidityLowAlarm");
            }
        }

        //信号设置有效
        private bool signalAlarmValid;
        public bool SignalAlarmValid
        {
            get { return signalAlarmValid; }
            set
            {
                signalAlarmValid = value;
                this.RaisePropertyChanged("SignalAlarmValid");
            }
        }
        private int signalHighAlarm;
        public int SignalHighAlarm
        {
            get { return signalHighAlarm; }
            set
            {
                signalHighAlarm = value;
                this.RaisePropertyChanged("SignalHighAlarm");
            }
        }

        private int signalLowAlarm;
        public int SignalLowAlarm
        {
            get { return signalLowAlarm; }
            set
            {
                signalLowAlarm = value;
                this.RaisePropertyChanged("SignalLowAlarm");
            }
        }

        //电量设置有效
        private bool electricityAlarmValid;
        public bool ElectricityAlarmValid
        {
            get { return electricityAlarmValid; }
            set
            {
                electricityAlarmValid = value;
                this.RaisePropertyChanged("ElectricityAlarmValid");
            }
        }

        private int electricityHighAlarm;
        public int ElectricityHighAlarm
        {
            get { return electricityHighAlarm; }
            set
            {
                electricityHighAlarm = value;
                this.RaisePropertyChanged("ElectricityHighAlarm");
            }
        }

        private int electricityLowAlarm;
        public int ElectricityLowAlarm
        {
            get { return electricityLowAlarm; }
            set
            {
                electricityLowAlarm = value;
                this.RaisePropertyChanged("ElectricityLowAlarm");
            }
        }

        //当前模式
        private int currentModel;
        public int CurrentModel
        {
            get { return currentModel; }
            set
            {
                currentModel = value;
                this.RaisePropertyChanged("CurrentModel");
            }
        }

        //实时模式参数
        private int realTimeParam;
        public int RealTimeParam
        {
            get { return realTimeParam; }
            set
            {
                realTimeParam = value;
                this.RaisePropertyChanged("RealTimeParam");
            }
        }

        //整点模式参数1
        private int fullTimeParam1;
        public int FullTimeParam1
        {
            get { return fullTimeParam1; }
            set
            {
                fullTimeParam1 = value;
                this.RaisePropertyChanged("FullTimeParam1");
            }
        }

        //整点模式参数2
        private int fullTimeParam2;
        public int FullTimeParam2
        {
            get { return fullTimeParam2; }
            set
            {
                fullTimeParam2 = value;
                this.RaisePropertyChanged("FullTimeParam2");
            }
        }

        //逢变则报参数1
        private int optimizeParam1;
        public int OptimizeParam1
        {
            get { return optimizeParam1; }
            set
            {
                optimizeParam1 = value;
                this.RaisePropertyChanged("OptimizeParam1");
            }
        }

        //逢变则报参数1
        private int optimizeParam2;
        public int OptimizeParam2
        {
            get { return optimizeParam2; }
            set
            {
                optimizeParam2 = value;
                this.RaisePropertyChanged("OptimizeParam2");
            }
        }

        //逢变则报参数1
        private int optimizeParam3;
        public int OptimizeParam3
        {
            get { return optimizeParam3; }
            set
            {
                optimizeParam3 = value;
                this.RaisePropertyChanged("OptimizeParam3");
            }
        }

        #endregion

    }
}
