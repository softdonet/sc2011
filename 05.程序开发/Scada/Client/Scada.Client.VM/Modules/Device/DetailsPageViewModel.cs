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
using Scada.Client.VM.CommClass;
using Scada.Client.VM.ScadaDeviceService;

namespace Scada.Client.VM.Modules.Device
{
    public class DetailsPageViewModel : NotificationObject
    {
        private ScadaDeviceServiceSoapClient scadaDeviceServiceSoapClient = null;
        public DetailsPageViewModel(Guid id)
        {
            this.scadaDeviceServiceSoapClient = ServiceManager.GetScadaDeviceService();
            //加载设备信息
            scadaDeviceServiceSoapClient.ViewDeviceInfoAsync(id);
            scadaDeviceServiceSoapClient.ViewDeviceInfoCompleted += (sender, e) =>
            {
                string result = e.Result;
                if (e.Error == null)
                {
                    DeviceInfo = BinaryObjTransfer.BinaryDeserialize<DeviceInfo>(e.Result);
                }
            };

            maintenancePeople = new MaintenancePeople();
            maintenancePeople.Name = "杨红康";
            maintenancePeople.Telephone = "13800000000";
            maintenancePeople.Email = "yhk@gamil.com";
            maintenancePeople.Address = "北京市大兴区";
        }


        /// <summary>
        /// 设备基本信息
        /// </summary>
        private DeviceInfo deviceInfo;
        public DeviceInfo DeviceInfo
        {
            get { return deviceInfo; }
            set
            {
                deviceInfo = value;
                RaisePropertyChanged("DeviceInfo");
            }
        }


        //设备维护人员信息
        private MaintenancePeople maintenancePeople;
        public MaintenancePeople MaintenancePeople
        {
            get { return maintenancePeople; }
            set
            {
                maintenancePeople = value;
                RaisePropertyChanged("MaintenancePeople");
            }
        }
    }
}
