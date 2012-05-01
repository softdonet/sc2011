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
using Scada.Model.Entity;
using Microsoft.Practices.Prism.ViewModel;
using Scada.Client.VM.ScadaDeviceService;
using Scada.Client.VM.CommClass;
using System.IO;
using System.Windows.Media.Imaging;

namespace Scada.Client.VM.Modules.UserEventVM
{
    public class UserEventProcessViewModel : NotificationObject
    {
        #region Variable
        UserEventDealDetail userEventDealDetail;
        private ScadaDeviceServiceSoapClient scadaDeviceServiceSoapClient;
        #endregion

        #region Constructor
        public UserEventProcessViewModel(Guid deviceNo)
        {
            scadaDeviceServiceSoapClient = ServiceManager.GetScadaDeviceService();
            scadaDeviceServiceSoapClient.ViewDeviceInfoCompleted += new EventHandler<ViewDeviceInfoCompletedEventArgs>(scadaDeviceServiceSoapClient_ViewDeviceInfoCompleted);
            scadaDeviceServiceSoapClient.ViewDeviceInfoAsync(deviceNo);

            //维护人员
            scadaDeviceServiceSoapClient.ListDeviceMaintenancePeopleCompleted += new EventHandler<ListDeviceMaintenancePeopleCompletedEventArgs>(scadaDeviceServiceSoapClient_ListDeviceMaintenancePeopleCompleted);
            scadaDeviceServiceSoapClient.ListDeviceMaintenancePeopleAsync(deviceNo);
        }

        void scadaDeviceServiceSoapClient_ListDeviceMaintenancePeopleCompleted(object sender, ListDeviceMaintenancePeopleCompletedEventArgs e)
        {
            if (e.Error==null)
            {
                MaintenancePeople result = BinaryObjTransfer.BinaryDeserialize<MaintenancePeople>(e.Result);
                MaintenancePeople = result;

                byte[] image = MaintenancePeople.HeadImage;
                if (image != null)
                {
                    MemoryStream stream = new MemoryStream(image);
                    BitmapImage bitmapImage = new BitmapImage();
                    bitmapImage.SetSource(stream);
                    MyHeadImage = bitmapImage;
                }
            }
        }

        void scadaDeviceServiceSoapClient_ViewDeviceInfoCompleted(object sender, ViewDeviceInfoCompletedEventArgs e)
        {
            if (e.Error==null)
            {
                DeviceInfo result = BinaryObjTransfer.BinaryDeserialize<DeviceInfo>(e.Result);
                DeviceInfo = result;
            }
        }
        #endregion

        private DeviceInfo deviceInfo;

        public DeviceInfo DeviceInfo
        {
            get { return deviceInfo; }
            set 
            { 
                deviceInfo = value;
                this.RaisePropertyChanged("DeviceInfo");
            }
        }

        private MaintenancePeople maintenancePeople;

        public MaintenancePeople MaintenancePeople
        {
            get { return maintenancePeople; }
            set
            {
                maintenancePeople = value;
               
                this.RaisePropertyChanged("MaintenancePeople");
            }
        }


        private BitmapImage myHeadImage;

        public BitmapImage MyHeadImage
        {
            get { return myHeadImage; }
            set 
            { 
                myHeadImage = value;
                this.RaisePropertyChanged("MyHeadImage");
            }
        }

   
    }
}
