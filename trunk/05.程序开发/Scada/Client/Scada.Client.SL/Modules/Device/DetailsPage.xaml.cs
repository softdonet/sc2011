using System;
using System.Linq;
using System.Collections.Generic;


using System.Net;
using System.Windows;
using System.Windows.Shapes;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;




namespace Scada.Client.SL.Modules.Device
{


    /// <summary>
    /// 设备详细信息
    /// </summary>
    public partial class DetailsPage : UserControl
    {


        #region 变量声明

        private Guid _deviceKey;

        #endregion


        #region 构造函数

        public DetailsPage()
        {
            InitializeComponent();
        }

        public DetailsPage(Guid deviceKey)
        {
            InitializeComponent();
            this._deviceKey = deviceKey;
        }

        #endregion


        #region 界面初期化
        #endregion


        #region 私有方法
        #endregion


    }

}
