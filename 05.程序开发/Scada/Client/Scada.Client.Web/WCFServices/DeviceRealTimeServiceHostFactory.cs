using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.ServiceModel.Channels;
using System.ServiceModel.Activation;

namespace Scada.Client.Web.WCFServices
{
    public class DeviceRealTimeServiceHostFactory : ServiceHostFactoryBase
    {
        public override ServiceHostBase CreateServiceHost(string constructorString, Uri[] baseAddresses)
        {
            return new DeviceRealTimeServiceServiceHost(baseAddresses);
        }
        class DeviceRealTimeServiceServiceHost : ServiceHost
        {
            public DeviceRealTimeServiceServiceHost(params Uri[] addresses)
            {
                base.InitializeDescription(typeof(DeviceRealTimeService), new UriSchemeKeyedCollection(addresses));
                base.Description.Behaviors.Add(new ServiceMetadataBehavior());
            }
            protected override void InitializeRuntime()
            {
                //给服务添加一个终结点
                this.AddServiceEndpoint(typeof(IDeviceRealTimeService),
                    new CustomBinding(
                        new PollingDuplexBindingElement(),
                        new BinaryMessageEncodingBindingElement(),
                        new HttpTransportBindingElement()
                    ), "");

                //添加元数据终结点
                this.AddServiceEndpoint(typeof(IMetadataExchange), MetadataExchangeBindings.CreateMexHttpBinding(), "mex");
                base.InitializeRuntime();
            }
        }
    }
}