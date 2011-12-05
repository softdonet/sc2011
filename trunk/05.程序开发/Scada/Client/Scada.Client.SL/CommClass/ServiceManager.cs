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
using Scada.Client.SL.WeatherWebService;
using Scada.Client.SL.DeviceRealTimeService;
using System.ServiceModel;
using System.ServiceModel.Channels;

namespace Scada.Client.SL.CommClass
{
    /// <summary>
    /// 服务管理
    /// </summary>
    public  class ServiceManager
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
                _deviceRealTimeServiceClient = new DeviceRealTimeServiceClient(binding, address);
            }
            return _deviceRealTimeServiceClient;
        }

        /// <summary>
        /// 天气预报WebService服务
        /// </summary>
        static string weatherWebServiceEndpointAddress = "WebServices/WeatherWebService.asmx";
        static WeatherWebServiceSoapClient _ws = null;
        public static WeatherWebServiceSoapClient GetWeatherWebService()
        {
            if (_ws == null)
            {
                System.ServiceModel.EndpointAddress address = new System.ServiceModel.EndpointAddress(GetAbsoluteUri(weatherWebServiceEndpointAddress));
                _ws = new WeatherWebServiceSoapClient("WeatherWebServiceSoap",address);
            }
            return _ws;
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
