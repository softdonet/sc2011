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
using Scada.Model.Entity.Enums;
using Scada.Client.VM.Modules.BingMaps;

namespace Scada.Client.SL.Modules.BingMaps
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
        /// 设备ID
        /// </summary>
        public Guid DeviceID { get; set; }

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

        /// <summary>
        /// 设备状态
        /// </summary>
        private DeviceStates devState;
        public DeviceStates DevState
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
                    case DeviceStates.Normal:
                        brush.ImageSource = new BitmapImage(new Uri("Image/BLUE-base1.png", UriKind.Relative));

                        break;
                    case DeviceStates.Escape:
                        brush.ImageSource = new BitmapImage(new Uri("Image/GRAY-base1.png", UriKind.Relative));

                        break;
                    case DeviceStates.Alert:
                        brush.ImageSource = new BitmapImage(new Uri("Image/ALERT-base1.png", UriKind.Relative));

                        break;

                }


                this.LayoutRoot.Background = brush;
                devState = value;
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
        public delegate void PushPinDeviceRoutedEventHander(object sender, PushPinRoutedEventArgs e);
        public event PushPinDeviceRoutedEventHander onclickDetails;
        private void hlUrl_Click(object sender, RoutedEventArgs e)
        {
            if (onclickDetails != null)
            {
                var obj = this.DataContext as PushPinDeviceViewModel;
                this.onclickDetails(this, new PushPinRoutedEventArgs() { ID = obj.NodeKey });
            }
        }
    }

    /// <summary>
    /// 单击设备详情事件
    /// </summary>
    public class PushPinRoutedEventArgs : RoutedEventArgs
    {
        public Guid ID { get; set; }
    }
}
