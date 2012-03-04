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

namespace Scada.Client.VM.Modules.UserEvent
{
    public class UserEventViewModel:NotificationObject
    {
        #region Variable

        private List<UserEventTab> userEventTab;

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
                List<UserEventTab> result = BinaryObjTransfer.BinaryDeserialize<List<UserEventTab>>(e.data);
                UserEventTabList = result;
                UserEventTabListTop = result;
            }
        }

        

        private List<UserEventTab> userEventTabList;

        public List<UserEventTab> UserEventTabList
        {
            get { return userEventTabList; }
            set 
            { 
                userEventTabList = value;
                this.RaisePropertyChanged("UserEventTabList");
            }
        }

        private List<UserEventTab> userEventTabListTop;

        public List<UserEventTab> UserEventTabListTop
        {
            get { return userEventTabListTop; }
            set
            {
                userEventTabListTop = value;
                userEventTabListTop = userEventTabListTop.OrderBy(e => e.RequestTime).Take(3).ToList();
              
              //var aa= from  p in  userEventTabList select p //.Items[0]
                    
                this.RaisePropertyChanged("UserEventTabListTop");
            }
        }

        #endregion
    }
}
