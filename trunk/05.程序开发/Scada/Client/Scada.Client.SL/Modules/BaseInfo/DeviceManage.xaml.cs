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


using Scada.Model.Entity;
using Scada.Client.SL.CommClass;
using Scada.Client.SL.ScadaDeviceService;



namespace Scada.Client.SL.Modules.BaseInfo
{
    public partial class DeviceManage : UserControl
    {

        #region 变量声明

        private ScadaDeviceServiceSoapClient scadaDeviceServiceSoapClient = null;

        //默认修改
        private Boolean IsAddUpdateType = false;
        private DeviceTreeNode _userSelTreeNode = null;
        private DeviceInfo _userSelDeviceInfo = null;

        #endregion

        #region 构造函数

        public DeviceManage()
        {

            InitializeComponent();
            this.scadaDeviceServiceSoapClient = ServiceManager.GetScadaDeviceService();

            //加载树型
            this.LoadTreeViewInfo();

            //增加设备
            this.LoadAddDeviceInfo();

            //修改设备
            this.LoadUpdateDeviceInfo();

            //删除设备




        }

        #endregion

        #region 加载树型结构

        private void LoadTreeViewInfo()
        {

            //刷新树结构
            scadaDeviceServiceSoapClient.ListDeviceTreeViewCompleted +=
                new EventHandler<ListDeviceTreeViewCompletedEventArgs>(scadaDeviceServiceSoapClient_ListDeviceTreeViewCompleted);
            scadaDeviceServiceSoapClient.ListDeviceTreeViewAsync();

            //列设备信息
            scadaDeviceServiceSoapClient.ViewDeviceInfoCompleted +=
                new EventHandler<ViewDeviceInfoCompletedEventArgs>(scadaDeviceServiceSoapClient_ViewDeviceInfoCompleted);

        }

        private void scadaDeviceServiceSoapClient_ListDeviceTreeViewCompleted(object sender, ListDeviceTreeViewCompletedEventArgs e)
        {
            this.treeViewList1.Source = BinaryObjTransfer.BinaryDeserialize<List<DeviceTreeNode>>(e.Result);
        }

        private void treeViewList1_OnTreeSelectItemClick(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (sender == null) { return; }
            DeviceTreeNode node = e.NewValue as DeviceTreeNode;
            this._userSelTreeNode = node;

            this.butOut.IsEnabled = true;
            if (node.NodeType == 1)
            {
                this.butAdd.IsEnabled = false;
                this.butDel.IsEnabled = false;
                this.butSave.IsEnabled = false;
            }
            else if (node.NodeType == 2)
            {
                this.butAdd.IsEnabled = true;
                this.butDel.IsEnabled = false;
                this.butSave.IsEnabled = true;
            }
            else if (node.NodeType == 3)
            {
                this.butAdd.IsEnabled = true;
                this.butDel.IsEnabled = true;
                this.butSave.IsEnabled = true;
                scadaDeviceServiceSoapClient.ViewDeviceInfoAsync(node.NodeKey.ToString().ToUpper());
            }
        }

        private void scadaDeviceServiceSoapClient_ViewDeviceInfoCompleted(object sender, ViewDeviceInfoCompletedEventArgs e)
        {
            string result = e.Result;
            if (string.IsNullOrEmpty(result)) { return; }
            _userSelDeviceInfo = BinaryObjTransfer.BinaryDeserialize<DeviceInfo>(result);
            if (_userSelDeviceInfo == null) { return; }

            //设备MAC
            this.textBox1.Text = _userSelDeviceInfo.DeviceMAC;
            this.textBox2.Text = _userSelDeviceInfo.SIMNo;
            this.textBox3.Text = _userSelTreeNode.NodeValue;
            this.textBox4.Text = _userSelDeviceInfo.InstallPlace;
            this.textBox5.Text = _userSelDeviceInfo.Comment;

            this.textBox12.Text = _userSelDeviceInfo.ConnectPoint;
            this.textBox13.Text = _userSelDeviceInfo.UserName;
            this.textBox14.Text = _userSelDeviceInfo.Password;
            this.textBox15.Text = _userSelDeviceInfo.Coordinate;

            this.textBox16.Text = "";
            if (_userSelDeviceInfo.ConnectType != null)
                this.textBox16.Text = _userSelDeviceInfo.ConnectType.ToString();

            this.textBox17.Text = _userSelDeviceInfo.MainDNS;
            this.textBox18.Text = _userSelDeviceInfo.SecondDNS;
            this.textBox19.Text = _userSelDeviceInfo.CenterIP;
            this.textBox20.Text = _userSelDeviceInfo.Domain;

            this.textBox21.Text = "";
            if (_userSelDeviceInfo.port != null)
                this.textBox21.Text = _userSelDeviceInfo.port.ToString();

            //定时表类型
            Int32? intType = _userSelDeviceInfo.TimeType;
            if (intType != null)
            {
                if (intType == 1)
                    this.radioButton1.IsChecked = true;
                else if (intType == 2)
                    this.radioButton2.IsChecked = true;
                else if (intType == 3)
                    this.radioButton3.IsChecked = true;
                else if (intType == 4)
                    this.radioButton4.IsChecked = true;
                else
                    this.radioButton5.IsChecked = true;
            }

            this.textBox11.Text = _userSelDeviceInfo.Version;

            //硬件配置
            intType = _userSelDeviceInfo.CollectFreq;
            if (intType != null)
                this.textBox6.Text = intType.ToString();

            intType = _userSelDeviceInfo.ReportInterval;
            if (intType != null)
                this.textBox7.Text = intType.ToString();

            decimal? decValue = _userSelDeviceInfo.AlarmTop;
            if (decValue != null)
                this.textBox8.Text = decValue.ToString();

            decValue = _userSelDeviceInfo.AlarmDown;
            if (decValue != null)
                this.textBox9.Text = decValue.ToString();

            decValue = _userSelDeviceInfo.Windage;
            if (decValue != null)
                this.textBox10.Text = decValue.ToString();

        }

        #endregion

        #region 增加设备

        private void LoadAddDeviceInfo()
        {
            scadaDeviceServiceSoapClient.AddDeviceInfoCompleted
                             += new EventHandler<AddDeviceInfoCompletedEventArgs>
                                            (scadaDeviceServiceSoapClient_AddDeviceInfoCompleted);
        }

        private void scadaDeviceServiceSoapClient_AddDeviceInfoCompleted(object sender, AddDeviceInfoCompletedEventArgs e)
        {
            bool Flag = BinaryObjTransfer.BinaryDeserialize<Boolean>(e.Result.ToString());
        }

        #endregion



        #region 修改设备

        private void LoadUpdateDeviceInfo()
        {
            scadaDeviceServiceSoapClient.UpdateDeviceInfoCompleted
                            += new EventHandler<UpdateDeviceInfoCompletedEventArgs>
                                        (scadaDeviceServiceSoapClient_UpdateDeviceInfoCompleted);
        }

        private void scadaDeviceServiceSoapClient_UpdateDeviceInfoCompleted(object sender, UpdateDeviceInfoCompletedEventArgs e)
        {
            string result = e.Result.ToString();
        }



        #endregion

        #region 删除设备
        #endregion

        #region 事件处理

        private void butAdd_Click(object sender, RoutedEventArgs e)
        {

            this.IsAddUpdateType = true;

            


        }

        private void butDel_Click(object sender, RoutedEventArgs e)
        {

        }

        private void butSave_Click(object sender, RoutedEventArgs e)
        {

            if (IsAddUpdateType)
                _userSelDeviceInfo.ID = new Guid();

            this.AddDeviceProperty(_userSelDeviceInfo);

            string deviceInfo = string.Empty;
            if (IsAddUpdateType)
            {
                deviceInfo = BinaryObjTransfer.BinarySerialize<DeviceInfo>(_userSelDeviceInfo);
                scadaDeviceServiceSoapClient.AddDeviceInfoAsync(deviceInfo);
            }
            else
            {
                deviceInfo = BinaryObjTransfer.BinarySerialize<DeviceInfo>(_userSelDeviceInfo);
                scadaDeviceServiceSoapClient.UpdateDeviceInfoAsync(deviceInfo);
            }
        }

        private void AddDeviceProperty(DeviceInfo deviceInfo)
        {

            deviceInfo.DeviceMAC = this.textBox1.Text;
            deviceInfo.SIMNo = this.textBox2.Text;

            deviceInfo.InstallPlace = this.textBox4.Text;
            deviceInfo.Comment = this.textBox5.Text;

            deviceInfo.ConnectPoint = this.textBox12.Text;
            deviceInfo.UserName = this.textBox13.Text;
            deviceInfo.Password = this.textBox14.Text;
            deviceInfo.Coordinate = this.textBox15.Text;

            deviceInfo.ConnectType = 1;

            deviceInfo.MainDNS = this.textBox17.Text;
            deviceInfo.SecondDNS = this.textBox18.Text;
            deviceInfo.CenterIP = this.textBox19.Text;
            deviceInfo.Domain = this.textBox20.Text;

            string strValue = this.textBox21.Text;
            if (!string.IsNullOrEmpty(strValue))
                deviceInfo.port = Convert.ToInt32(strValue);

            //定时表类型
            Int32? intType = null;
            if ((Boolean)this.radioButton1.IsChecked)
                intType = 1;
            else if ((Boolean)this.radioButton2.IsChecked)
                intType = 2;
            else if ((Boolean)this.radioButton3.IsChecked)
                intType = 3;
            else if ((Boolean)this.radioButton4.IsChecked)
                intType = 4;
            else if ((Boolean)this.radioButton5.IsChecked)
                intType = 5;
            deviceInfo.TimeType = intType;
            deviceInfo.Version = this.textBox11.Text;

            //硬件配置
            strValue = this.textBox6.Text;
            if (!string.IsNullOrEmpty(strValue))
                deviceInfo.CollectFreq = Convert.ToInt32(strValue);

            strValue = this.textBox7.Text;
            if (!string.IsNullOrEmpty(strValue))
                deviceInfo.ReportInterval = Convert.ToInt32(strValue);

            strValue = this.textBox8.Text;
            if (!string.IsNullOrEmpty(strValue))
                deviceInfo.AlarmTop = Convert.ToDecimal(strValue);

            strValue = this.textBox9.Text;
            if (!string.IsNullOrEmpty(strValue))
                deviceInfo.AlarmDown = Convert.ToDecimal(strValue);

            strValue = this.textBox10.Text;
            if (!string.IsNullOrEmpty(strValue))
                deviceInfo.Windage = Convert.ToDecimal(strValue);


        }

        #endregion


        #region 私有方法



        #endregion




    }
}
