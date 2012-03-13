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


namespace Scada.Client.VM.Modules.UserEventMV
{
    public class UserEventViewModel : NotificationObject
    {
        #region Variable

        private List<UserEvent> userEvent;

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
                List<UserEvent> result = BinaryObjTransfer.BinaryDeserialize<List<UserEvent>>(e.data);
                userEventList = result;
                userEventListTop = result;
            }
        }



        private List<UserEvent> userEventList;

        public List<UserEvent> UserEventList
        {
            get { return userEventList; }
            set
            {
                userEventList = value;
                this.RaisePropertyChanged("userEventList");
            }
        }

        private List<UserEvent> userEventListTop;

        public List<UserEvent> UserEventListTop
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
