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

using Scada.Client.VM.Modules.DiagramAnalysis;


using Scada.Client.SL.CommClass;




namespace Scada.Client.SL.Modules.DiagramAnalysis
{


    /// <summary>
    /// 按日期对比(同设备不同时间段)
    /// </summary>
    public partial class CompareByTime : UserControl
    {


        #region 变量声明



        #endregion


        #region 静态构造

        private static CompareByTime instance;
        public static CompareByTime GetInstance()
        {
            if (instance == null)
            {
                instance = new CompareByTime();
            }
            return instance;
        }

        #endregion


        #region 构造函数

        public CompareByTime()
        {

            InitializeComponent();

            CompareByTimeViewModel vm = new CompareByTimeViewModel();
            this.DataContext = vm;
           
        }

        #endregion


        #region 界面初期化

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            this.init();
            this.cmbSelDevice.SelectedValuePath = "NodeKey";
            this.cmbSelDevice.DisplayMemberPath = "NodeValue";
            
        }


        private void init()
        {
            this.cmbSelDateMode.Items.Clear();
            object[] selMode = EnumHelper.GetValues(typeof(DateSelMode));
            foreach (var item in selMode)
            {
                this.cmbSelDateMode.Items.Add(item.ToString());
            }

            if (this.cmbSelDateMode.Items.Count > 0)
                this.cmbSelDateMode.SelectedIndex = 0;

        }

        #endregion

     



    }



    public enum DateSelMode
    {
        天 = 0,
        周 = 1,
        月 = 2,
    }
}
