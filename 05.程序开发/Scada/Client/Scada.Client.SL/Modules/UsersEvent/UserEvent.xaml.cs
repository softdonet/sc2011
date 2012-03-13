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

        public UserEvent()
        {

            InitializeComponent();

            UserEventViewModel userEventViewModel = new UserEventViewModel();
            this.DataContext = userEventViewModel;
            userEventViewModel.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(userEventViewModel_PropertyChanged);


            MyContent.CloseBtn += new EventHandler(MyContent_CloseBtn);


            //this._scadaDeviceServiceSoapClient = ServiceManager.GetScadaDeviceService();

            //this._scadaDeviceServiceSoapClient.GetListUserEventInfoCompleted +=
            //    new EventHandler<GetListUserEventInfoCompletedEventArgs>(scadaDeviceServiceSoapClient_ListUserEventInfoCompleted);

            //this._scadaDeviceServiceSoapClient.GetListUserEventInfoAsync();

        }

        void userEventViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            //throw new NotImplementedException();
        }

        void MyContent_CloseBtn(object sender, EventArgs e)
        {
            Storyboard2.Begin();
            ViewHost.Visibility = Visibility.Collapsed;
        }

        //private void scadaDeviceServiceSoapClient_ListUserEventInfoCompleted(object sender, GetListUserEventInfoCompletedEventArgs e)
        //{
        //    List<UserEventTab> userEventTab = BinaryObjTransfer.BinaryDeserialize<List<UserEventTab>>(e.Result);
        //    this.RadGridView1.ItemsSource = userEventTab;
        //}


        #endregion


        #region 事件处理

        private void RadGridView1_RowLoaded(object sender, Telerik.Windows.Controls.GridView.RowLoadedEventArgs e)
        {
            //TextBlock state = (e.Row.Cells[RadGridView1.Columns.Count - 2].Content as FrameworkElement) as TextBlock;
            //HyperlinkButton hlUrl = (e.Row.Cells[RadGridView1.Columns.Count - 1].Content as FrameworkElement).FindName("hlUrl") as HyperlinkButton;
            //if (state.Text.Trim() == "未确认")
            //{
            //    e.Row.Background = new SolidColorBrush(Colors.Red);
            //    e.Row.Cells[RadGridView1.Columns.Count - 1].Background = new SolidColorBrush(Colors.White);
            //    hlUrl.IsEnabled = true;
            //    AddAlert(e.Row);
            //}
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
            //RadWindow.Alert(String.Format("DialogResult: {0}, PromptResult: {1}", e.DialogResult, e.PromptResult));
        }

        private Guid id, deviceid;
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
