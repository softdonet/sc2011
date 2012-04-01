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
using Scada.Client.VM.Modules.Query;

namespace Scada.Client.SL.Modules.Query
{
    public partial class UserEventQuery : UserControl
    {
        private static UserEventQuery instance;
        public static UserEventQuery GetInstance()
        {
            if (instance==null)
            {
                instance = new UserEventQuery();
            }
            return instance;
        }

        UserEventQueryViewModel userEventQueryViewModel;
        private UserEventQuery()
        {
            InitializeComponent();
            userEventQueryViewModel = new UserEventQueryViewModel();
            this.DataContext = userEventQueryViewModel;
            userEventQueryViewModel.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(userEventQueryViewModel_PropertyChanged);

        }

        void userEventQueryViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "DeviceTreeSource")
            {
                this.comboBoxTreeView1.Source = userEventQueryViewModel.DeviceTreeSource;
            }
        }

        
    }
}
