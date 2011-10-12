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
using System.Windows.Media.Imaging;
using Microsoft.Maps.MapControl;

namespace SCADA.UI.Modules.BingMaps
{
    public partial class pushPinDevice : UserControl
    {

        public pushPinDevice()
        {
            InitializeComponent();
            //防止图钉重叠
            this.MouseEnter += (sender, e) =>
            {
                var item = sender as UserControl;
                var plane = item.Parent as MapLayer;
                plane.Children.Remove(item);
                plane.Children.Add(item);
            };
        }

        /// <summary>
        /// 设备状态
        /// </summary>
        private DeviceState devState;
        public DeviceState DevState
        {
            get
            {
                return devState;
            }
            set
            {
                ImageBrush brush = new ImageBrush();
                switch (value)
                {
                    case DeviceState.Normal:
                        brush.ImageSource = new BitmapImage(new Uri("Image/BLUE-base.png", UriKind.Relative));

                        break;
                    case DeviceState.Escape:
                        brush.ImageSource = new BitmapImage(new Uri("Image/GRAY-base.png", UriKind.Relative));

                        break;
                    case DeviceState.Alert:
                        brush.ImageSource = new BitmapImage(new Uri("Image/BLUE-base.png", UriKind.Relative));

                        break;

                }


                this.LayoutRoot.Background = brush;
                devState = value;
            }
        }

        /// <summary>
        /// 设备名称
        /// </summary>
        private string deviceName;
        public string DeviceName
        {
            get { return deviceName; }
            set
            {
                deviceName = value;
                this.txtDeviceName.Text = deviceName;
                this.txtTitle.Text = deviceName;
            }
        }

        /// <summary>
        /// 设备温度
        /// </summary>
        private string deviceTemp;
        public string DeviceTemp
        {
            get { return deviceTemp; }
            set
            {
                deviceTemp = value;
                this.txtTemp.Text = deviceTemp;
            }
        }

        private void GoToEnter(object sender, MouseEventArgs e)
        {
            VisualStateManager.GoToState(this, "Enter", false);
        }

        private void GoToLeave(object sender, MouseEventArgs e)
        {
            VisualStateManager.GoToState(this, "Leave", false);
        }
    }
}
