using System;
using System.IO;
using System.Net;
using System.Linq;
using System.Collections.Generic;


using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Media.Imaging;






namespace Scada.Client.SL.Modules.BaseInfo
{


    /// <summary>
    /// 维护人员列表
    /// </summary>
    public partial class RepairUserManage : UserControl
    {

        #region 变量声明

        private OpenFileDialog _ofd;

        #endregion



        #region 构造函数

        public RepairUserManage()
        {
            InitializeComponent();
        }

        #endregion


        #region 事件处理

        private void butOpen_Click(object sender, RoutedEventArgs e)
        {
            _ofd = new OpenFileDialog() { Filter = "Image files (*.jpg)|*.jpg" };
            if (_ofd.ShowDialog() != true) return;
            Stream fileStream = _ofd.File.OpenRead();
            Stream inStream = new MemoryStream(new BinaryReader(fileStream).ReadBytes((int)fileStream.Length));
            BitmapImage imageIn = new BitmapImage();
            imageIn.SetSource(inStream);
            this.imageInput.Source = imageIn;
        }

        private void butClear_Click(object sender, RoutedEventArgs e)
        {
            this.imageInput.Source = null;
        }


        #endregion

       

        #region 私有方法



        #endregion


    }
}
