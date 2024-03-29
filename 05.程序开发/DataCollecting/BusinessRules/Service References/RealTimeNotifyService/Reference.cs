﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.1
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BusinessRules.RealTimeNotifyService {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="RealTimeNotifyService.RealTimeNotifyServiceSoap")]
    public interface RealTimeNotifyServiceSoap {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/UserEventDataReceive", ReplyAction="*")]
        void UserEventDataReceive();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ReaTimeDataReceivedReceive", ReplyAction="*")]
        void ReaTimeDataReceivedReceive();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/AlarmDataReceived", ReplyAction="*")]
        void AlarmDataReceived();
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface RealTimeNotifyServiceSoapChannel : BusinessRules.RealTimeNotifyService.RealTimeNotifyServiceSoap, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class RealTimeNotifyServiceSoapClient : System.ServiceModel.ClientBase<BusinessRules.RealTimeNotifyService.RealTimeNotifyServiceSoap>, BusinessRules.RealTimeNotifyService.RealTimeNotifyServiceSoap {
        
        public RealTimeNotifyServiceSoapClient() {
        }
        
        public RealTimeNotifyServiceSoapClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public RealTimeNotifyServiceSoapClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public RealTimeNotifyServiceSoapClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public RealTimeNotifyServiceSoapClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public void UserEventDataReceive() {
            base.Channel.UserEventDataReceive();
        }
        
        public void ReaTimeDataReceivedReceive() {
            base.Channel.ReaTimeDataReceivedReceive();
        }
        
        public void AlarmDataReceived() {
            base.Channel.AlarmDataReceived();
        }
    }
}
