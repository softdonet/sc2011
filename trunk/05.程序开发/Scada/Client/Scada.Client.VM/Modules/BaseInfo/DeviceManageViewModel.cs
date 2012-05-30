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
using Scada.Model.Entity.Enums;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using Scada.Utility.Common.SL.Enums;

namespace Scada.Client.VM.Modules.BaseInfo
{
    public class DeviceManageViewModel : NotificationObject
    {
        public DelegateCommand AddCommand { get; set; }
        public DelegateCommand UpdateCommand { get; set; }
        public DelegateCommand DeleteCommand { get; set; }
        private ScadaDeviceServiceSoapClient scadaDeviceServiceSoapClient = null;

        public Guid CurrentNodeGuid { get; set; }
        public int CurrentNodeLevel { get; set; }


        //public List<DeviceTreeNode> DeviceTreeNodeResult { get; set; }
        DeviceInfo deviceInfo;
        string myDeviceId;
        public DeviceManageViewModel()
        {
            LoadDisplayType();
            LoadCurrentModel();
            LoadTemperatureGatherTerm();
            LoadCommunacationSendTerm();
            LoadQuickTemperatureGether();
            LoadMinSendTerm();


            scadaDeviceServiceSoapClient = ServiceManager.GetScadaDeviceService();

            scadaDeviceServiceSoapClient.ListDeviceTreeViewCompleted += new EventHandler<ListDeviceTreeViewCompletedEventArgs>(scadaDeviceServiceSoapClient_ListDeviceTreeViewCompleted);
            scadaDeviceServiceSoapClient.ListDeviceTreeViewAsync();

            //查看是否有重复名字的设备By DeviceId
            this.scadaDeviceServiceSoapClient.CheckDeviceInfoByDeviceNoCompleted += new EventHandler<CheckDeviceInfoByDeviceNoCompletedEventArgs>(scadaDeviceServiceSoapClient_CheckDeviceInfoByDeviceNoCompleted);

            //维护人员
            scadaDeviceServiceSoapClient.GetMaintenancePeopleCompleted += new EventHandler<GetMaintenancePeopleCompletedEventArgs>(scadaDeviceServiceSoapClient_GetMaintenancePeopleCompleted);
            scadaDeviceServiceSoapClient.GetMaintenancePeopleAsync();

            //查看设备
            scadaDeviceServiceSoapClient.ViewDeviceInfoCompleted += new EventHandler<ViewDeviceInfoCompletedEventArgs>(scadaDeviceServiceSoapClient_ViewDeviceInfoCompleted);

            //添加设备
            scadaDeviceServiceSoapClient.AddDeviceInfoCompleted += new EventHandler<AddDeviceInfoCompletedEventArgs>(scadaDeviceServiceSoapClient_AddDeviceInfoCompleted);
            this.AddCommand = new DelegateCommand(new Action(this.AddDeviceInfo));

            //修改设备
            this.scadaDeviceServiceSoapClient.UpdateDeviceInfoCompleted += new EventHandler<UpdateDeviceInfoCompletedEventArgs>(scadaDeviceServiceSoapClient_UpdateDeviceInfoCompleted);
            this.UpdateCommand = new DelegateCommand(new Action(this.UpdateDeviceInfo));

            //删除设备
            this.scadaDeviceServiceSoapClient.DeleteDeviceInfoCompleted += new EventHandler<DeleteDeviceInfoCompletedEventArgs>(scadaDeviceServiceSoapClient_DeleteDeviceInfoCompleted);
            this.DeleteCommand = new DelegateCommand(new Action(this.DeleteDeviceInfo));

        }



        bool flag;
        void scadaDeviceServiceSoapClient_CheckDeviceInfoByDeviceNoCompleted(object sender, CheckDeviceInfoByDeviceNoCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                flag = e.Result;
            }
            if (flag == true)
            {
                MessageBox.Show("该设备名称已存在，请选择其他的名字!");
                return;
            }
            else
            {
                if (DeviceInfoList != null)
                {
                    if (CurrentNodeLevel == 2)//组
                    {
                        DeviceInfoList.ManageAreaID = CurrentNodeGuid;
                    }
                    string deviceNo = DeviceInfoList.DeviceNo;
                    //--------------------
                    DeviceInfoList.ID = Guid.NewGuid();
                    //string deviceInfo = BinaryObjTransfer.BinarySerialize(addDevice);
                    string deviceInfo = BinaryObjTransfer.BinarySerialize(DeviceInfoList);
                    scadaDeviceServiceSoapClient.AddDeviceInfoAsync(deviceInfo);
                    scadaDeviceServiceSoapClient.ListDeviceTreeViewAsync();
                }
            }
        }

        void scadaDeviceServiceSoapClient_GetMaintenancePeopleCompleted(object sender, GetMaintenancePeopleCompletedEventArgs e)
        {
            string result = e.Result;
            if (e.Error == null)
            {
                List<MaintenancePeople> maintenancePeopleList = BinaryObjTransfer.BinaryDeserialize<List<MaintenancePeople>>(e.Result);

                if (maintenancePeopleList == null)
                {
                    maintenancePeopleList = new List<MaintenancePeople>();
                }
                MaintenancePeopleList = maintenancePeopleList;
            }
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

        public void ViewDeviceInfoById(Guid myDeviceId)
        {
            scadaDeviceServiceSoapClient.ViewDeviceInfoAsync(myDeviceId);

        }
        private void AddDeviceInfo()
        {
            if (DeviceInfoList != null)
            {
                if (!ValidationProprety()) return;

                CheckChoosedWhichWorkMode();
                string deviceNo = DeviceInfoList.DeviceNo;
                this.scadaDeviceServiceSoapClient.CheckDeviceInfoByDeviceNoAsync(deviceNo);
                //--------------------
                //DeviceInfoList.ID = Guid.NewGuid();
                ////string deviceInfo = BinaryObjTransfer.BinarySerialize(addDevice);
                //string deviceInfo = BinaryObjTransfer.BinarySerialize(DeviceInfoList);
                //scadaDeviceServiceSoapClient.AddDeviceInfoAsync(deviceInfo);
                //scadaDeviceServiceSoapClient.ListDeviceTreeViewAsync();
            }
            //else
            //{
            //    DeviceInfoList = new DeviceInfo();

            //    DeviceInfoList.ID = Guid.NewGuid();
            //    DeviceInfoList.DeviceNo = DeviceNo;
            //    DeviceInfoList.DeviceSN = DeviceSN;

            //}
        }

        void scadaDeviceServiceSoapClient_UpdateDeviceInfoCompleted(object sender, UpdateDeviceInfoCompletedEventArgs e)
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
            scadaDeviceServiceSoapClient.ListDeviceTreeViewAsync();

        }

        private void UpdateDeviceInfo()
        {
            if (!ValidationProprety()) return;
            CheckChoosedWhichWorkMode();
            string deviceInfo = BinaryObjTransfer.BinarySerialize(DeviceInfoList);

            scadaDeviceServiceSoapClient.UpdateDeviceInfoAsync(deviceInfo);
        }

        void scadaDeviceServiceSoapClient_DeleteDeviceInfoCompleted(object sender, DeleteDeviceInfoCompletedEventArgs e)
        {
            bool flag = e.Result;
            if (flag)
            {
                MessageBox.Show("删除设备成功!");
                DeviceInfoList = null;
            }
            else
            {
                MessageBox.Show("删除设备失败或该设备不能被删除!");
            }

            scadaDeviceServiceSoapClient.ListDeviceTreeViewAsync();

        }

        private void DeleteDeviceInfo()
        {
            scadaDeviceServiceSoapClient.DeleteDeviceInfoAsync(DeviceInfoList.ID);
            // scadaDeviceServiceSoapClient.ListDeviceTreeViewAsync();
        }

        #region 设备树

        void scadaDeviceServiceSoapClient_ListDeviceTreeViewCompleted(object sender, ListDeviceTreeViewCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                List<DeviceTreeNode> result = BinaryObjTransfer.BinaryDeserialize<List<DeviceTreeNode>>(e.Result);
                DeviceTreeNodeList = result;

                //DeviceTreeNodeResult = result;
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
                if (deviceInfo == null)
                {
                    deviceInfo = new DeviceInfo();
                }
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
        /// 绑定维护人员信息
        /// </summary>
        private List<MaintenancePeople> maintenancePeopleList;
        public List<MaintenancePeople> MaintenancePeopleList
        {
            get { return maintenancePeopleList; }
            set
            {
                maintenancePeopleList = value;
                this.RaisePropertyChanged("MaintenancePeopleList");
            }
        }

        /// <summary>
        /// 获取选择的维护人员信息
        /// </summary>
        private MaintenancePeople selectedMaintenancePeople;
        public MaintenancePeople SelectedMaintenancePeople
        {
            get
            {
                if (DeviceInfoList != null)
                {
                    if (MaintenancePeopleList != null)
                    {
                        selectedMaintenancePeople = MaintenancePeopleList.Where(e => e.ID == DeviceInfoList.MaintenancePeopleID).SingleOrDefault();
                    }
                }
                return selectedMaintenancePeople;
            }
            set
            {
                selectedMaintenancePeople = value;
                this.RaisePropertyChanged("SelectedMaintenancePeople");
                if (DeviceInfoList != null && selectedMaintenancePeople != null)
                {
                    DeviceInfoList.MaintenancePeopleID = selectedMaintenancePeople.ID;
                }
            }
        }


        /// <summary>
        /// 设备信息
        /// </summary>
        private DeviceInfo deviceInfoList;
        public DeviceInfo DeviceInfoList
        {
            get
            {
                //if (selectedItem != null)
                //{
                //    deviceInfoList.LCDScreenDisplayType = selectedItem.LCDScreenDisplayType;
                //}
                return deviceInfoList;
            }
            set
            {
                deviceInfoList = value;
                this.RaisePropertyChanged("DeviceInfoList");

                SelectedLcdItem = deviceInfoList;
                SelectedCurrentModelItem = deviceInfoList;

                SelectedFullTimeParam1Item = deviceInfoList;
                SelectedFullTimeParam2Item = deviceInfoList;
                SelectedOptimize1Item = deviceInfoList;
                SelectedOptimize2Item = deviceInfoList;
                SelectedMaintenancePeople = selectedMaintenancePeople;//用于设置界面上的显示

                // DeviceNo = deviceInfoList.DeviceNo;

                this.RaisePropertyChanged("DeviceNo");
                this.RaisePropertyChanged("Longitude");
                this.RaisePropertyChanged("Latitude");
                this.RaisePropertyChanged("MaintenancePeopleID");
                this.RaisePropertyChanged("RealTimeParam");

                //this.RaisePropertyChanged("SelectedFullTimeParam1Item");
                //this.RaisePropertyChanged("SelectedFullTimeParam2Item");
                //this.RaisePropertyChanged("SelectedOptimize1Item");
                //this.RaisePropertyChanged("SelectedOptimize1Item");
            }
        }

        private List<DeviceInfo> deviceInfoLcdList;
        public List<DeviceInfo> DeviceInfoLcdList
        {
            get { return deviceInfoLcdList; }
            set
            {
                deviceInfoLcdList = value;
                this.RaisePropertyChanged("DeviceInfoLcdList");
            }
        }
        /// <summary>
        /// 当前选择的绑定选择的对象
        /// LCD
        /// </summary>
        private DeviceInfo selectedLcdItem;
        public DeviceInfo SelectedLcdItem
        {
            get
            {
                if (DeviceInfoList != null)
                {
                    selectedLcdItem = DeviceInfoLcdList.Where(e => e.LCDScreenDisplayType == DeviceInfoList.LCDScreenDisplayType).SingleOrDefault();
                }
                return selectedLcdItem;
            }
            set
            {
                selectedLcdItem = value;
                this.RaisePropertyChanged("SelectedLcdItem");
                if (DeviceInfoList != null && selectedLcdItem != null)
                {
                    DeviceInfoList.LCDScreenDisplayType = selectedLcdItem.LCDScreenDisplayType;
                }
            }
        }

        private List<DeviceInfo> deviceInfoCurrentModelList;
        /// <summary>
        /// 当前的工作模式
        /// CurrentModel
        /// </summary>
        public List<DeviceInfo> DeviceInfoCurrentModelList
        {
            get { return deviceInfoCurrentModelList; }
            set
            {
                deviceInfoCurrentModelList = value;
                this.RaisePropertyChanged("DeviceInfoCurrentModelList");
            }
        }

        private DeviceInfo selectedCurrentModelItem;
        /// <summary>
        /// 当前选择的工作模式
        /// CurrentModel
        /// </summary>
        public DeviceInfo SelectedCurrentModelItem
        {
            get
            {
                if (DeviceInfoList != null)
                {
                    selectedCurrentModelItem = DeviceInfoCurrentModelList.Where(e => e.CurrentModel == DeviceInfoList.CurrentModel).SingleOrDefault();
                }
                return selectedCurrentModelItem;
            }
            set
            {
                selectedCurrentModelItem = value;
                this.RaisePropertyChanged("SelectedCurrentModelItem");

                if (DeviceInfoList != null && selectedCurrentModelItem != null)
                {
                    DeviceInfoList.CurrentModel = selectedCurrentModelItem.CurrentModel;
                }
            }
        }
        #region 整点模式参数1的值

        private List<DeviceInfo> deviceInfoFullTimeParam1List;
        /// <summary>
        /// 温度采集周期
        /// </summary>
        public List<DeviceInfo> DeviceInfoFullTimeParam1List
        {
            get { return deviceInfoFullTimeParam1List; }
            set
            {
                deviceInfoFullTimeParam1List = value;
                this.RaisePropertyChanged("DeviceInfoFullTimeParam1List");
            }
        }

        private DeviceInfo selectedFullTimeParam1Item;
        /// <summary>
        /// 当前选择的整点模式参数1的值
        /// </summary>
        public DeviceInfo SelectedFullTimeParam1Item
        {
            get
            {
                if (DeviceInfoList != null)
                {
                    selectedFullTimeParam1Item = DeviceInfoFullTimeParam1List.Where(e => e.FullTimeParam1 == DeviceInfoList.FullTimeParam1).SingleOrDefault();
                }
                return selectedFullTimeParam1Item;
            }
            set
            {
                selectedFullTimeParam1Item = value;
                this.RaisePropertyChanged("SelectedFullTimeParam1Item");
                if (DeviceInfoList != null && selectedFullTimeParam1Item != null)
                {
                    DeviceInfoList.FullTimeParam1 = selectedFullTimeParam1Item.FullTimeParam1;
                }
            }
        }

        #endregion

        #region 整点模式参数2

        private List<DeviceInfo> deviceInfoFullTimeParam2List;
        /// <summary>
        /// 通讯发送周期
        /// </summary>
        public List<DeviceInfo> DeviceInfoFullTimeParam2List
        {
            get { return deviceInfoFullTimeParam2List; }
            set
            {
                deviceInfoFullTimeParam2List = value;
                this.RaisePropertyChanged("DeviceInfoFullTimeParam2List");
            }
        }

        private DeviceInfo selectedFullTimeParam2Item;
        /// <summary>
        /// 当前选择的整点模式参数1的值
        /// </summary>
        public DeviceInfo SelectedFullTimeParam2Item
        {
            get
            {
                if (DeviceInfoList != null)
                {
                    selectedFullTimeParam2Item = DeviceInfoFullTimeParam2List.Where(e => e.FullTimeParam2 == DeviceInfoList.FullTimeParam2).SingleOrDefault();
                }
                return selectedFullTimeParam2Item;
            }
            set
            {
                selectedFullTimeParam2Item = value;
                this.RaisePropertyChanged("SelectedFullTimeParam2Item");
                if (DeviceInfoList != null && selectedFullTimeParam2Item != null)
                {
                    DeviceInfoList.FullTimeParam2 = selectedFullTimeParam2Item.FullTimeParam2;
                }
            }
        }

        #endregion

        #region 优化模式参数1

        private List<DeviceInfo> deviceInfoOptimize1List;
        /// <summary>
        /// 快速温度采样
        /// </summary>
        public List<DeviceInfo> DeviceInfoOptimize1List
        {
            get { return deviceInfoOptimize1List; }
            set
            {
                deviceInfoOptimize1List = value;
                this.RaisePropertyChanged("DeviceInfoOptimize1List");
            }
        }
        private DeviceInfo selectedOptimize1Item;
        /// <summary>
        /// 当前选择的优化模式参数1的值
        /// </summary>
        public DeviceInfo SelectedOptimize1Item
        {
            get
            {
                if (DeviceInfoList != null)
                {
                    selectedOptimize1Item = DeviceInfoOptimize1List.Where(e => e.OptimizeParam1 == DeviceInfoList.OptimizeParam1).SingleOrDefault();
                }
                return selectedOptimize1Item;
            }
            set
            {
                selectedOptimize1Item = value;
                this.RaisePropertyChanged("SelectedOptimize1Item");
                if (DeviceInfoList != null && selectedOptimize1Item != null)
                {
                    DeviceInfoList.OptimizeParam1 = selectedOptimize1Item.OptimizeParam1;
                }
            }
        }
        #endregion

        #region 优化模式参数2

        private List<DeviceInfo> deviceInfoOptimize2List;
        /// <summary>
        /// 快速温度采样
        /// </summary>
        public List<DeviceInfo> DeviceInfoOptimize2List
        {
            get { return deviceInfoOptimize2List; }
            set
            {
                deviceInfoOptimize2List = value;
                this.RaisePropertyChanged("DeviceInfoOptimize2List");
            }
        }

        private DeviceInfo selectedOptimize2Item;
        /// <summary>
        /// 当前选择的优化模式参数2的值
        /// </summary>
        public DeviceInfo SelectedOptimize2Item
        {
            get
            {
                if (DeviceInfoList != null)
                {
                    selectedOptimize2Item = DeviceInfoOptimize2List.Where(e => e.OptimizeParam2 == DeviceInfoList.OptimizeParam2).SingleOrDefault();
                }
                return selectedOptimize2Item;
            }
            set
            {
                selectedOptimize2Item = value;
                this.RaisePropertyChanged("SelectedOptimize2Item");
                if (DeviceInfoList != null && selectedOptimize2Item != null)
                {
                    DeviceInfoList.OptimizeParam2 = selectedOptimize2Item.OptimizeParam2;
                }
            }
        }
        #endregion

        /// <summary>
        /// 液晶屏显示类型数据源
        /// </summary>
        private void LoadDisplayType()
        {
            List<DeviceInfo> temp = new List<DeviceInfo>();
            temp.Clear();
            temp.Add(new DeviceInfo() { LCDScreenDisplayType = 1, LCDScreenDisplayTypeName = "完整显示" });
            temp.Add(new DeviceInfo() { LCDScreenDisplayType = 2, LCDScreenDisplayTypeName = "基本显示" });
            temp.Add(new DeviceInfo() { LCDScreenDisplayType = 3, LCDScreenDisplayTypeName = "天气预报" });
            temp.Add(new DeviceInfo() { LCDScreenDisplayType = 4, LCDScreenDisplayTypeName = "不显示" });
            DeviceInfoLcdList = temp;
        }
        /// <summary>
        /// 加载工作模式
        /// </summary>
        private void LoadCurrentModel()
        {
            List<DeviceInfo> temp = new List<DeviceInfo>();
            temp.Clear();
            temp.Add(new DeviceInfo() { CurrentModel = 1, CurrentModelName = EnumHelper.Display<CurrentModel>(1) });
            temp.Add(new DeviceInfo() { CurrentModel = 2, CurrentModelName = EnumHelper.Display<CurrentModel>(2) });
            temp.Add(new DeviceInfo() { CurrentModel = 3, CurrentModelName = EnumHelper.Display<CurrentModel>(3) });
            DeviceInfoCurrentModelList = temp;
        }

        /// <summary>
        /// 加载整点模式参数1-温度采集周期--数据源
        /// FullTimeParam1, FullTimeParam1Name
        /// </summary>
        private void LoadTemperatureGatherTerm()
        {
            List<DeviceInfo> temp = new List<DeviceInfo>();
            temp.Clear();
            temp.Add(new DeviceInfo() { FullTimeParam1 = 1, FullTimeParam1Name = EnumHelper.Display<FullTime1>(1) });
            temp.Add(new DeviceInfo() { FullTimeParam1 = 2, FullTimeParam1Name = EnumHelper.Display<FullTime1>(2) });
            temp.Add(new DeviceInfo() { FullTimeParam1 = 3, FullTimeParam1Name = EnumHelper.Display<FullTime1>(3) });
            temp.Add(new DeviceInfo() { FullTimeParam1 = 4, FullTimeParam1Name = EnumHelper.Display<FullTime1>(4) });
            temp.Add(new DeviceInfo() { FullTimeParam1 = 5, FullTimeParam1Name = EnumHelper.Display<FullTime1>(5) });
            DeviceInfoFullTimeParam1List = temp;
        }
        /// <summary>
        /// 加载整点模式参数2-通讯发送周期--数据源
        /// FullTimeParam2, FullTimeParam2Name
        /// </summary>
        private void LoadCommunacationSendTerm()
        {
            List<DeviceInfo> temp = new List<DeviceInfo>();
            temp.Clear();
            temp.Add(new DeviceInfo() { FullTimeParam2 = 1, FullTimeParam2Name = EnumHelper.Display<FullTime2>(1) });
            temp.Add(new DeviceInfo() { FullTimeParam2 = 2, FullTimeParam2Name = EnumHelper.Display<FullTime2>(2) });
            temp.Add(new DeviceInfo() { FullTimeParam2 = 3, FullTimeParam2Name = EnumHelper.Display<FullTime2>(3) });
            temp.Add(new DeviceInfo() { FullTimeParam2 = 4, FullTimeParam2Name = EnumHelper.Display<FullTime2>(4) });
            temp.Add(new DeviceInfo() { FullTimeParam2 = 5, FullTimeParam2Name = EnumHelper.Display<FullTime2>(5) });
            DeviceInfoFullTimeParam2List = temp;

        }
        /// <summary>
        /// 加载逢变则报模式--参数1-快速温度采样:
        /// OptimizeParam1--OptimizeParam1Name
        /// </summary>
        private void LoadQuickTemperatureGether()
        {
            List<DeviceInfo> temp = new List<DeviceInfo>();
            temp.Clear();
            temp.Add(new DeviceInfo() { OptimizeParam1 = 1, OptimizeParam1Name = EnumHelper.Display<Optimize1>(1) });
            temp.Add(new DeviceInfo() { OptimizeParam1 = 2, OptimizeParam1Name = EnumHelper.Display<Optimize1>(2) });
            temp.Add(new DeviceInfo() { OptimizeParam1 = 3, OptimizeParam1Name = EnumHelper.Display<Optimize1>(3) });
            DeviceInfoOptimize1List = temp;
        }
        /// <summary>
        /// 加载逢变则报模式--参数1-最小发送间隔
        /// OptimizeParam2--OptimizeParam2Name
        /// </summary>
        private void LoadMinSendTerm()
        {
            List<DeviceInfo> temp = new List<DeviceInfo>();
            temp.Clear();
            temp.Add(new DeviceInfo() { OptimizeParam2 = 1, OptimizeParam2Name = EnumHelper.Display<Optimize2>(1) });
            temp.Add(new DeviceInfo() { OptimizeParam2 = 2, OptimizeParam2Name = EnumHelper.Display<Optimize2>(2) });
            temp.Add(new DeviceInfo() { OptimizeParam2 = 3, OptimizeParam2Name = EnumHelper.Display<Optimize2>(3) });
            DeviceInfoOptimize2List = temp;
        }
        //---------------------------------------------------------------
        [Required(ErrorMessage = "设备编号不能为空!")]
        public string DeviceNo
        {
            get
            {
                if (DeviceInfoList == null)
                {
                    DeviceInfoList = new DeviceInfo();
                }
                return DeviceInfoList.DeviceNo;
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new Exception("设备编号不能为空!");

                }
                DeviceInfoList.DeviceNo = value;
                //this.RaisePropertyChanged("DeviceNo");
                //Validator.ValidateProperty(value, new ValidationContext(this, null, null) { MemberName = "DeviceNo" });
            }
        }

        [Required(ErrorMessage = "经度不能为空!")]
        public decimal Longitude
        {
            get
            {
                if (DeviceInfoList == null)
                {
                    DeviceInfoList = new DeviceInfo();
                }
                return DeviceInfoList.Longitude;
            }

            set
            {
                DeviceInfoList.Longitude = value;
                this.RaisePropertyChanged("Longitude");
                Validator.ValidateProperty(value, new ValidationContext(this, null, null) { MemberName = "Longitude" });
            }
        }

        [Required(ErrorMessage = "维度不能为空!")]
        public decimal Latitude
        {
            get
            {
                if (DeviceInfoList == null)
                {
                    DeviceInfoList = new DeviceInfo();
                }
                return DeviceInfoList.Latitude;
            }
            set
            {
                DeviceInfoList.Latitude = value;
                this.RaisePropertyChanged("Latitude");
                Validator.ValidateProperty(value, new ValidationContext(this, null, null) { MemberName = "Latitude" });
            }
        }

        [Required(ErrorMessage = "请选择维修人员!")]
        public Guid MaintenancePeopleID
        {
            get
            {
                if (DeviceInfoList == null)
                {
                    DeviceInfoList = new DeviceInfo();
                }
                return DeviceInfoList.MaintenancePeopleID;
            }
            set
            {
                DeviceInfoList.MaintenancePeopleID = value;
                this.RaisePropertyChanged("MaintenancePeopleID");
                Validator.ValidateProperty(value, new ValidationContext(this, null, null) { MemberName = "MaintenancePeopleID" });
            }
        }

        [Required(ErrorMessage = "实时采集周期格式不对!")]
        public int? RealTimeParam
        {
            get
            {
                if (DeviceInfoList == null)
                {
                    DeviceInfoList = new DeviceInfo();
                }
                return DeviceInfoList.RealTimeParam;
            }
            set
            {
                DeviceInfoList.RealTimeParam = value;
                this.RaisePropertyChanged("RealTimeParam");
                Validator.ValidateProperty(value, new ValidationContext(this, null, null) { MemberName = "RealTimeParam" });
            }
        }


        public int? FullTimeParam1
        {
            get
            {
                if (DeviceInfoList == null)
                {
                    DeviceInfoList = new DeviceInfo();
                }
                return DeviceInfoList.FullTimeParam1;
            }
            set
            {
                DeviceInfoList.FullTimeParam1 = value;
                this.RaisePropertyChanged("FullTimeParam1");
            }
        }


        /// <summary>
        /// 验证对象中属性不能为空
        /// </summary>
        private bool ValidationProprety()
        {
            //设备编号
            if (string.IsNullOrEmpty(DeviceInfoList.DeviceNo))
            {
                MessageBox.Show("设备编号不能为空!");
                return false;

            }
            if (DeviceInfoList.DeviceNo.Trim().Length != 8)
            {
                MessageBox.Show("请输入8位长度的设备编号! 如P-123456");
                return false;
            }

            #region 设备SN号

            //判断设备SN号：
            //规则：1 12位长度
            //      2 前8位必须是数字和字母组合
            //      3 后四位必须是数组
            if (string.IsNullOrEmpty(DeviceInfoList.DeviceSN))
            {
                MessageBox.Show("设备SN号不能为空!");
                return false;
            }
            if (DeviceInfoList.DeviceSN.Trim().Length != 12)
            {
                MessageBox.Show("请输入12位长度的设备SN号! 如P1234567S1234");
                return false;
            }
            string split1 = DeviceInfoList.DeviceSN.Trim().Substring(0, 8);
            string split2 = DeviceInfoList.DeviceSN.Trim().Substring(8, 4);
            char[] char1 = split1.ToCharArray();
            int isInt = 0;
            int isLetter = 0;
            foreach (char item in char1)
            {
                if (Char.IsLetter(item))
                {
                    if (char.IsUpper(item))
                    {
                        isLetter++;
                    }
                    else
                    {
                        MessageBox.Show("设备SN号中不能有小写字母!格式为:P1234567S1234");
                        return false;
                    }
                }
                if (Char.IsNumber(item))
                {
                    isInt++;
                }
            }
            if (isLetter > 0 && isInt > 0)
            {
                int tryResult;
                if (int.TryParse(split2, out tryResult))
                {

                }
                else
                {
                    MessageBox.Show("您输入的设备SN号后四位只能是数字", "温馨提示", MessageBoxButton.OK);

                    return false;
                }
            }
            else
            {
                MessageBox.Show("您输入的设备SN号前8位必须是数字和字母组合!如P1234567S1234", "温馨提示", MessageBoxButton.OK);
                return false;
            }
            #endregion

            //----------------------------------------------
            //安装地点
            if (!string.IsNullOrEmpty(deviceInfoList.InstallPlace) && DeviceInfoList.InstallPlace.ToString().Length > 15)
            {
                MessageBox.Show("安装地点过长，请输入小于15长度!");
                return false;
            }
            decimal result;
            if (!decimal.TryParse(DeviceInfoList.Longitude.ToString(), out result))
            {
                MessageBox.Show("经度，请填写正确的格式!");
                return false;
            }
            if (!decimal.TryParse(DeviceInfoList.Latitude.ToString(), out result))
            {
                MessageBox.Show("维度，请填写正确的格式!");
                return false;
            }
            if (DeviceInfoList.High != null && !decimal.TryParse(DeviceInfoList.High.ToString(), out result))
            {
                MessageBox.Show("高度，请填写正确的格式!");
                return false;
            }
            int value;
            if (DeviceInfoList.Windage != null && !int.TryParse(DeviceInfoList.Windage.ToString(), out value))
            {
                MessageBox.Show("偏差，请填写正确的格式!");
                return false;
            }
            //主温度
            if (DeviceInfoList.Temperature1HighAlarm != null && !decimal.TryParse(DeviceInfoList.Temperature1HighAlarm.ToString(), out result))
            {
                MessageBox.Show("主温度高报警值，请填写正确的格式!");
                return false;
            }
            if (DeviceInfoList.Temperature1LowAlarm != null && !decimal.TryParse(DeviceInfoList.Temperature1LowAlarm.ToString(), out result))
            {
                MessageBox.Show("主温度低报警值，请填写正确的格式!");
                return false;
            }
            //从温度
            if (DeviceInfoList.Temperature2HighAlarm != null && !decimal.TryParse(DeviceInfoList.Temperature2HighAlarm.ToString(), out result))
            {
                MessageBox.Show("从温度高报警值，请填写正确的格式!");
                return false;
            }

            if (DeviceInfoList.Temperature2LowAlarm != null && !decimal.TryParse(DeviceInfoList.Temperature2LowAlarm.ToString(), out result))
            {
                MessageBox.Show("从温度低报警值，请填写正确的格式!");
                return false;
            }
            //湿度
            if (DeviceInfoList.HumidityHighAlarm != null && !decimal.TryParse(DeviceInfoList.HumidityHighAlarm.ToString(), out result))
            {
                MessageBox.Show("湿度高报警值，请填写正确的格式!");
                return false;
            }

            if (DeviceInfoList.HumidityLowAlarm != null && !decimal.TryParse(DeviceInfoList.HumidityLowAlarm.ToString(), out result))
            {
                MessageBox.Show("湿度低报警值，请填写正确的格式!");
                return false;
            }
            //信号
            if (DeviceInfoList.SignalHighAlarm != null && !decimal.TryParse(DeviceInfoList.SignalHighAlarm.ToString(), out result))
            {
                MessageBox.Show("信号高报警值，请填写正确的格式!");
                return false;
            }

            if (DeviceInfoList.SignalLowAlarm != null && !decimal.TryParse(DeviceInfoList.SignalLowAlarm.ToString(), out result))
            {
                MessageBox.Show("信号低报警值，请填写正确的格式!");
                return false;
            }
            //电量
            if (DeviceInfoList.ElectricityHighAlarm != null && !decimal.TryParse(DeviceInfoList.ElectricityHighAlarm.ToString(), out result))
            {
                MessageBox.Show("电量高报警值，请填写正确的格式!");
                return false;
            }

            if (DeviceInfoList.ElectricityLowAlarm != null && !decimal.TryParse(DeviceInfoList.ElectricityLowAlarm.ToString(), out result))
            {
                MessageBox.Show("电量低报警值，请填写正确的格式!");
                return false;
            }

            if (!DeviceInfoList.LCDScreenDisplayType.HasValue)
            {
                MessageBox.Show("请选择液晶屏显示类型!");
                return false;
            }
            if (!DeviceInfoList.CurrentModel.HasValue)
            {
                MessageBox.Show("请选择工作模式!");
                return false;
            }
            else
            {
                switch (DeviceInfoList.CurrentModel)
                {
                    case 1:
                        if (!DeviceInfoList.RealTimeParam.HasValue)
                        {
                            MessageBox.Show("请输入实时采集周期!");
                            return false;
                        }
                        break;
                    case 2:
                        if (!DeviceInfoList.FullTimeParam1.HasValue)
                        {
                            MessageBox.Show("请选择温度采集周期!");
                            return false;
                        }
                        if (!DeviceInfoList.FullTimeParam2.HasValue)
                        {
                            MessageBox.Show("请选择通讯发送周期!");
                            return false;
                        }
                        break;
                    case 3:
                        if (!DeviceInfoList.OptimizeParam1.HasValue)
                        {
                            MessageBox.Show("请选择快速温度采样!");
                            return false;
                        }
                        if (!DeviceInfoList.OptimizeParam2.HasValue)
                        {
                            MessageBox.Show("请选择最小发送周期!");
                            return false;
                        }
                        if (!DeviceInfoList.OptimizeParam3.HasValue)
                        {
                            MessageBox.Show("请输入温度变化限值!");
                            return false;
                        }
                        break;
                    default:
                        break;
                }

            }
            if (DeviceInfoList.MaintenancePeopleID == new Guid())
            {
                MessageBox.Show("请选择维护人员!");
                return false;
            }
            return true;
        }
        

        /// <summary>
        /// 检查选择的是当前的哪种工作模式，选择一种后，其他2种的值清空
        /// 1.实时采集模式
        /// 2.整点模式
        /// 3. 逢变则报模式
        /// </summary>
        private void CheckChoosedWhichWorkMode()
        {
            switch (DeviceInfoList.CurrentModel)
            {
                case 1:
                    DeviceInfoList.FullTimeParam1 = null;
                    DeviceInfoList.FullTimeParam2 = null;
                    DeviceInfoList.OptimizeParam1 = null;
                    DeviceInfoList.OptimizeParam2 = null;
                    DeviceInfoList.OptimizeParam3 = null;
                    break;
                case 2:
                    DeviceInfoList.RealTimeParam = null;
                    DeviceInfoList.OptimizeParam1 = null;
                    DeviceInfoList.OptimizeParam2 = null;
                    DeviceInfoList.OptimizeParam3 = null;
                    break;
                case 3:
                    DeviceInfoList.RealTimeParam = null;
                     DeviceInfoList.FullTimeParam1 = null;
                    DeviceInfoList.FullTimeParam2 = null;
                    break;
                default:
                    break;
            }
        }
    }
}
