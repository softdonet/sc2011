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

        #region Constroctor

        public UserEventViewModel()
        {
            DeviceRealTimeServiceClient userEventService = ServiceManager.GetDeviceRealTimeService();
            userEventService.GetCallDataReceived += new EventHandler<GetCallDataReceivedEventArgs>(userEventService_GetCallDataReceived);

        }

        void userEventService_GetCallDataReceived(object sender, GetCallDataReceivedEventArgs e)
        {
            if (e.Error == null)
            {
                List<UserEventModel> result = BinaryObjTransfer.BinaryDeserialize<List<UserEventModel>>(e.data);
                userEventList = result;
                userEventListTop = result;
            }
        }



        private List<UserEventModel> userEventList;

        public List<UserEventModel> UserEventList
        {
            get { return userEventList; }
            set
            {
                userEventList = value;
                this.RaisePropertyChanged("userEventList");
            }
        }

        private List<UserEventModel> userEventListTop;

        public List<UserEventModel> UserEventListTop
        {
            get { return userEventListTop; }
            set
            {
                userEventListTop = value;
                userEventListTop = userEventListTop.OrderBy(e => e.RequestTime).Take(3).ToList();
                this.RaisePropertyChanged("userEventListTop");
            }
        }

        #endregion
    }
}
