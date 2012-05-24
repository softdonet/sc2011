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
using Scada.Client.VM.DeviceRealTimeService;
using Scada.Client.VM.CommClass;
using System.Linq;


namespace Scada.Client.VM.Modules.UserEventVM
{
    public class UserEventViewModel : NotificationObject
    {
        #region Variable

        private List<UserEventModel> userEvent;

        #endregion

        #region Constructor
        DeviceRealTimeServiceClient deviceRealTimeServiceClient = ServiceManager.GetDeviceRealTimeService();
        public UserEventViewModel()
        {
             deviceRealTimeServiceClient = ServiceManager.GetDeviceRealTimeService();
             deviceRealTimeServiceClient.GetUserEventDataReceived += new EventHandler<GetUserEventDataReceivedEventArgs>(userEventService_GetUserEventDataReceived);
             deviceRealTimeServiceClient.GetUserEventDataListCompleted += (sender, e) => { };
             deviceRealTimeServiceClient.GetUserEventDataListAsync();
        }

        public void GetData()
        {
            deviceRealTimeServiceClient.GetUserEventDataListAsync();
        }

        void userEventService_GetUserEventDataReceived(object sender, GetUserEventDataReceivedEventArgs e)
        {
            if (e.Error == null)
            {
                List<UserEventModel> result = BinaryObjTransfer.BinaryDeserialize<List<UserEventModel>>(e.data);
                UserEventList = result;
                UserEventListTop = result;
            }
        }

        private List<UserEventModel> userEventList;

        public List<UserEventModel> UserEventList
        {
            get { return userEventList; }
            set
            {
                userEventList = value;
                this.RaisePropertyChanged("UserEventList");
            }
        }

        private List<UserEventModel> userEventListTop;

        public List<UserEventModel> UserEventListTop
        {
            get { return userEventListTop; }
            set
            {
                userEventListTop = value;
                userEventListTop = userEventListTop.OrderByDescending(e => e.RequestTime).Take(3).ToList();
                this.RaisePropertyChanged("UserEventListTop");
            }
        }

        #endregion
    }
}
