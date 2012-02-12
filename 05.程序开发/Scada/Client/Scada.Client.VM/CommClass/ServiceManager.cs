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
using System.ServiceModel;
using System.ServiceModel.Channels;
using Scada.Client.VM.DeviceRealTimeService;
using Scada.Client.VM.ScadaDeviceService;


namespace Scada.Client.VM.CommClass
{
    /// <summary>
    /// 服务管理
    /// </summary>
    public class ServiceManager
    {
        /// <summary>
        /// 获取设备实时信息WCF服务
        /// </summary>
        static string deviceRealTimeServiceEndpointAddress = "WCFServices/DeviceRealTimeService.svc";
        static DeviceRealTimeServiceClient _deviceRealTimeServiceClient = null;
        public static DeviceRealTimeServiceClient GetDeviceRealTimeService()
        {
            if (_deviceRealTimeServiceClient == null)
            {
                EndpointAddress address = new EndpointAddress(GetAbsoluteUri(deviceRealTimeServiceEndpointAddress));
                CustomBinding binding = new CustomBinding(
                    new PollingDuplexBindingElement(),
                    new BinaryMessageEncodingBindingElement(),
                    new HttpTransportBindingElement());
                binding.SendTimeout = new TimeSpan(0, 1, 0);
                binding.ReceiveTimeout  = new TimeSpan(0, 1, 0);
                _deviceRealTimeServiceClient = new DeviceRealTimeServiceClient(binding, address);
                //初始化连接
                _deviceRealTimeServiceClient.InitDataAsync();
            }
            return _deviceRealTimeServiceClient;
        }
        /// <summary>
        /// 设备信息WebService服务
        /// </summary>
        static string scadaDeviceServiceEndpointAddress = "WebServices/ScadaDeviceService.asmx";
        static ScadaDeviceServiceSoapClient _sds = null;
        public static ScadaDeviceServiceSoapClient GetScadaDeviceService()
        {
            if (_sds == null)
            {
                System.ServiceModel.EndpointAddress address = new System.ServiceModel.EndpointAddress(GetAbsoluteUri(scadaDeviceServiceEndpointAddress));
                _sds = new ScadaDeviceServiceSoapClient("ScadaDeviceServiceSoap", address);
            }
            return _sds;
        }

        /// <summary>
        /// 获取网站路径
        /// </summary>
        /// <param name="relativeUri"></param>
        /// <returns></returns>
        public static string GetAbsoluteUri(string relativeUri)
        {
            Uri uri = System.Windows.Browser.HtmlPage.Document.DocumentUri;
            string uriString = uri.AbsoluteUri;
            // replace page name with relative service Uri
            int ls = uriString.LastIndexOf('/');
            return uriString.Substring(0, ls + 1) + relativeUri;
        }

    }
}
