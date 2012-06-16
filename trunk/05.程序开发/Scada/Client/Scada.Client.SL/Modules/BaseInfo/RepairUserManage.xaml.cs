using System;
using System.IO;
using System.Net;
using System.Linq;
using System.ComponentModel;
using System.Collections.Generic;
using System.Collections.ObjectModel;


using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Media.Imaging;


using Scada.Model.Entity;
using Scada.Client.SL.CommClass;
using Scada.Client.SL.ScadaDeviceService;


using Telerik.Windows.Controls.GridView;




namespace Scada.Client.SL.Modules.BaseInfo
{


    /// <summary>
    /// 维护人员列表
    /// </summary>
    public partial class RepairUserManage : UserControl
    {

        #region 变量声明

        private OpenFileDialog _ofd;

        private MaintenancePeople _mainPeopleItem;

        private ObservableCollection<MaintenancePeople> _mainPeopleList;

        private ScadaDeviceServiceSoapClient _scadaDeviceServiceSoapClient;

        #endregion

        #region 构造函数

        public RepairUserManage()
        {
            InitializeComponent();

            this._mainPeopleList = new ObservableCollection<MaintenancePeople>();

            this._scadaDeviceServiceSoapClient = ServiceManager.GetScadaDeviceService();
            this._scadaDeviceServiceSoapClient.GetMaintenancePeopleCompleted +=
                                            new EventHandler<GetMaintenancePeopleCompletedEventArgs>
                                                (scadaDeviceServiceSoapClient_GetMaintenancePeopleCompleted);
            this._scadaDeviceServiceSoapClient.GetMaintenancePeopleAsync();


            this._scadaDeviceServiceSoapClient.AddMaintenancePeopleCompleted +=
                             new EventHandler<AddMaintenancePeopleCompletedEventArgs>
                             (scadaDeviceServiceSoapClient_AddMaintenancePeopleCompleted);

            this._scadaDeviceServiceSoapClient.UpdateMaintenancePeopleCompleted +=
                                new EventHandler<UpdateMaintenancePeopleCompletedEventArgs>
                                    (scadaDeviceServiceSoapClient_UpdateMaintenancePeopleCompleted);

            this._scadaDeviceServiceSoapClient.ClearMaintenancePeopleHeadFaceCompleted +=
                                new EventHandler<ClearMaintenancePeopleHeadFaceCompletedEventArgs>
                                    (scadaDeviceServiceSoapClient_ClearMaintenancePeopleHeadFaceCompleted);


        }

        #endregion

        #region 事件处理

        private void butOpen_Click(object sender, RoutedEventArgs e)
        {
            _ofd = new OpenFileDialog() { Filter = "Image files (*.jpg)|*.jpg" };
            if (_ofd.ShowDialog() != true) return;
            Stream fileStream = _ofd.File.OpenRead();
            Stream inStream = new MemoryStream(new BinaryReader(fileStream).ReadBytes((int)fileStream.Length));
            BitmapImage imageIn = new BitmapImage();
            imageIn.SetSource(inStream);
            this.imageInput.Source = imageIn;
        }

        private void butClear_Click(object sender, RoutedEventArgs e)
        {
            if (this._mainPeopleItem == null) { return; }
            this.imageInput.Source = null;
            this._mainPeopleItem.ImagePath = String.Empty;
            this._mainPeopleItem.ImageUrl = String.Empty;
            this._scadaDeviceServiceSoapClient.ClearMaintenancePeopleHeadFaceAsync(this._mainPeopleItem.ID);
        }

        private void RadGridViewAlarm_RowActivated(object sender, Telerik.Windows.Controls.GridView.RowEventArgs e)
        {
            this._mainPeopleItem = e.Row.DataContext as MaintenancePeople;
            if (this._mainPeopleItem == null) { return; }
            this.refMaintenancePeople(this._mainPeopleItem);
        }

        private void butAdd_Click(object sender, RoutedEventArgs e)
        {

            //校验姓名
            string checkName = this.txtName.Text;
            if (string.IsNullOrEmpty(checkName))
            {
                ScadaMessageBox.ShowWarnMessage("维护人员名称不允许为空！", "警告信息");
                return;
            }
            IEnumerable<MaintenancePeople> people = _mainPeopleList.Where(x => x.Name == checkName);
            if (people.Count() > 0)
            {
                ScadaMessageBox.ShowWarnMessage("维护人员名称存在重复！", "警告信息");
                return;
            }

            this._mainPeopleItem = new MaintenancePeople();
            this._mainPeopleItem.ID = Guid.NewGuid();
            this.SetValuePeople(this._mainPeopleItem);
            string peppleValue = BinaryObjTransfer.BinarySerialize(this._mainPeopleItem);
            this._scadaDeviceServiceSoapClient.AddMaintenancePeopleAsync(peppleValue);

        }

        private void butDel_Click(object sender, RoutedEventArgs e)
        {
            string msgInfo = string.Format("是否删除维护人员:{0}的信息", this._mainPeopleItem.Name);
            if (ScadaMessageBox.ShowOKCancelMessage(msgInfo, "删除维护人") == MessageBoxResult.Cancel) { return; }
            this._scadaDeviceServiceSoapClient.DeleteMaintenancePeopleCompleted +=
                           new EventHandler<DeleteMaintenancePeopleCompletedEventArgs>
                           (scadaDeviceServiceSoapClient_DeleteMaintenancePeopleCompleted);
            this._scadaDeviceServiceSoapClient.DeleteMaintenancePeopleAsync(this._mainPeopleItem.ID);

        }

        private void butUpdate_Click(object sender, RoutedEventArgs e)
        {
            this.SetValuePeople(this._mainPeopleItem);
            string peppleValue = BinaryObjTransfer.BinarySerialize(this._mainPeopleItem);
            this._scadaDeviceServiceSoapClient.UpdateMaintenancePeopleAsync(peppleValue);
        }

        #endregion

        #region 私有方法

        private void ClearPeople()
        {
            this.txtName.Text = "";
            this.txtAddress.Text = "";
            this.txtEmail.Text = "";
            this.txtMobile.Text = "";
            this.txtMsn.Text = "";
            this.txtQQ.Text = "";
            this.txtTelephone.Text = "";
            this.imageInput.Source = null;

        }

        private void SetValuePeople(MaintenancePeople people)
        {
            people.Name = this.txtName.Text;
            people.Address = this.txtAddress.Text;
            people.Email = this.txtEmail.Text;
            people.Mobile = this.txtMobile.Text;
            people.MSN = this.txtMsn.Text;
            people.QQ = this.txtQQ.Text;
            people.Telephone = this.txtTelephone.Text;
            if (imageInput.Source != null)
            {
                try
                {
                    WriteableBitmap wb = new WriteableBitmap(imageInput.Source as BitmapSource);
                    byte[] bytes = Convert.FromBase64String(BitmapImageByte.GetBase64Image(wb));
                    people.HeadImage = bytes;
                    if (this._ofd.File != null)
                        people.ImagePath = this._ofd.File.Name;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        private void refMaintenancePeople(MaintenancePeople people)
        {
            this.txtName.Text = people.Name;
            this.txtAddress.Text = people.Address;
            this.txtEmail.Text = people.Email;
            this.txtMobile.Text = people.Mobile;
            this.txtMsn.Text = people.MSN;
            this.txtQQ.Text = people.QQ;
            this.txtTelephone.Text = people.Telephone;
            this.imageInput.Source = null;
            if (people.ImageUrl != null)
            {
                this.imageInput.Source = null;
                string filePath = people.ImageUrl;
                if (filePath.Length > 0)
                {
                    this.imageInput.Source = new BitmapImage(new Uri(filePath, UriKind.Absolute));
                }
            }

        }

        private void scadaDeviceServiceSoapClient_AddMaintenancePeopleCompleted(object sender, AddMaintenancePeopleCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                string result = e.Result;
                if (result.Length > 0)
                {
                    ScadaMessageBox.ShowWarnMessage("添加维护人员成功！", "提示信息");
                    this._mainPeopleItem = BinaryObjTransfer.BinaryDeserialize<MaintenancePeople>(result); ;
                    this._mainPeopleList.Add(this._mainPeopleItem);
                }
                else
                    ScadaMessageBox.ShowWarnMessage("添加维护人员失败！", "提示信息");
            }
            else
                ScadaMessageBox.ShowWarnMessage("获取数据失败！", "警告信息");
        }


        private void scadaDeviceServiceSoapClient_ClearMaintenancePeopleHeadFaceCompleted(object sender, ClearMaintenancePeopleHeadFaceCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                Boolean result = e.Result;
            }
            else
                ScadaMessageBox.ShowWarnMessage("获取数据失败！", "警告信息");
        }


        private void scadaDeviceServiceSoapClient_UpdateMaintenancePeopleCompleted(object sender, UpdateMaintenancePeopleCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                string result = e.Result;
                if (result.Length > 0)
                {
                    ScadaMessageBox.ShowWarnMessage("修改维护人员成功！", "提示信息");
                    MaintenancePeople people = BinaryObjTransfer.BinaryDeserialize<MaintenancePeople>(result); ;
                    this._mainPeopleItem.ImagePath = people.ImagePath;
                    this._mainPeopleItem.ImageUrl = people.ImageUrl;

                    //刷新界面
                    this._scadaDeviceServiceSoapClient.GetMaintenancePeopleAsync();
                }
                else
                    ScadaMessageBox.ShowWarnMessage("修改维护人员失败！", "提示信息");
            }
            else
                ScadaMessageBox.ShowWarnMessage("获取数据失败！", "警告信息");
        }

        private void scadaDeviceServiceSoapClient_DeleteMaintenancePeopleCompleted(object sender, DeleteMaintenancePeopleCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                Console.WriteLine(e.Result);
                this._mainPeopleList.Remove(this._mainPeopleItem);
            }
            else
                ScadaMessageBox.ShowWarnMessage("获取数据失败！", "警告信息");
        }

        private void scadaDeviceServiceSoapClient_GetMaintenancePeopleCompleted(object sender, GetMaintenancePeopleCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                string msgInfo = e.Result;
                List<MaintenancePeople> devicTreeList = BinaryObjTransfer.BinaryDeserialize<List<MaintenancePeople>>(msgInfo);
                this._mainPeopleList = new ObservableCollection<MaintenancePeople>(devicTreeList);
                this.RadGridViewAlarm.ItemsSource = this._mainPeopleList;

                //默认刷新首行数据
                if (devicTreeList.Count == 0) { return; }
                this._mainPeopleItem = this._mainPeopleList[0];
                this.refMaintenancePeople(this._mainPeopleItem);

            }
            else
                ScadaMessageBox.ShowWarnMessage("获取数据失败！", "警告信息");
        }

        #endregion


    }
}
