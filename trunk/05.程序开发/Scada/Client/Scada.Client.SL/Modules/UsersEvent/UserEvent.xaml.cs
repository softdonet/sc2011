using System;
using System.Linq;
using System.Collections.Generic;


using System.Net;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media.Animation;


using Telerik.Windows.Controls;
using Telerik.Windows.Controls.GridView;

using Scada.Model.Entity;
using Scada.Client.SL.CommClass;
using Scada.Client.SL.ScadaDeviceService;

using Scada.Utility.Common.SL;
using Scada.Client.VM.Modules.UserEventVM;


namespace Scada.Client.SL.Modules.UsersEvent
{

    /// <summary>
    /// 用户事件
    /// </summary>
    public partial class UserEvent : UserControl
    {

        #region 变量声明

        private ScadaDeviceServiceSoapClient _scadaDeviceServiceSoapClient;

        #endregion


        #region 构造函数

        private static UserEvent instance;

        public static UserEvent GetInstance()
        {
            if (instance==null)
            {
                instance = new UserEvent();
            }
            return instance;
        }

        UserEventViewModel UserEventViewModel = null;
        public UserEvent()
        {

            InitializeComponent();
            UserEventViewModel = new UserEventViewModel();
            this.DataContext = UserEventViewModel;
            MyContent.CloseBtn += new EventHandler(MyContent_CloseBtn);

        }


        void MyContent_CloseBtn(object sender, EventArgs e)
        {
            Storyboard2.Begin();
            ViewHost.Visibility = Visibility.Collapsed;
            UserEventViewModel.GetData();
        }

        #endregion


        #region 事件处理

        private void RadGridView1_RowLoaded(object sender, Telerik.Windows.Controls.GridView.RowLoadedEventArgs e)
        {
      
        }

        Dictionary<GridViewRowItem, GridViewRowItem> dicDr = new Dictionary<GridViewRowItem, GridViewRowItem>();
        private void AddAlert(GridViewRowItem dr)
        {
            if (!dicDr.ContainsKey(dr))
            {
                dicDr.Add(dr, dr);
            }
        }

        private void hlUrl_Click(object sender, RoutedEventArgs e)
        {
            RadWindow.Prompt("请输入备注：", new EventHandler<WindowClosedEventArgs>(OnClosed));
        }

        private void OnClosed(object sender, WindowClosedEventArgs e)
        {
           
        }

        private Guid id;
        private int? state;
        private void hlBtn_Click(object sender, RoutedEventArgs e)
        {
            HyperlinkButton hlB = sender as HyperlinkButton;
            UserEventModel userEvent = hlB.DataContext as UserEventModel;
            id = (hlB.DataContext as UserEventModel).ID;
            state = (hlB.DataContext as UserEventModel).State;
            Storyboard1.Begin();
            MyContent.Content = new UserEventProcess(userEvent);
            MyContent.Title = "用户事件流程";
        }
        #endregion

       

    }
}
