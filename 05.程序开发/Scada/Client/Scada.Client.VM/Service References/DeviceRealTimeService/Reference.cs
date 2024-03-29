﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.269
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// 
// This code was auto-generated by Microsoft.Silverlight.ServiceReference, version 4.0.50826.0
// 
namespace Scada.Client.VM.DeviceRealTimeService {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="", ConfigurationName="DeviceRealTimeService.IDeviceRealTimeService", CallbackContract=typeof(Scada.Client.VM.DeviceRealTimeService.IDeviceRealTimeServiceCallback))]
    public interface IDeviceRealTimeService {
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, AsyncPattern=true, Action="urn:IDeviceRealTimeService/InitData")]
        System.IAsyncResult BeginInitData(System.AsyncCallback callback, object asyncState);
        
        void EndInitData(System.IAsyncResult result);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, AsyncPattern=true, Action="urn:IDeviceRealTimeService/GetRealTimeDataList")]
        System.IAsyncResult BeginGetRealTimeDataList(System.AsyncCallback callback, object asyncState);
        
        void EndGetRealTimeDataList(System.IAsyncResult result);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, AsyncPattern=true, Action="urn:IDeviceRealTimeService/GetAlarmDataList")]
        System.IAsyncResult BeginGetAlarmDataList(System.AsyncCallback callback, object asyncState);
        
        void EndGetAlarmDataList(System.IAsyncResult result);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, AsyncPattern=true, Action="urn:IDeviceRealTimeService/GetUserEventDataList")]
        System.IAsyncResult BeginGetUserEventDataList(System.AsyncCallback callback, object asyncState);
        
        void EndGetUserEventDataList(System.IAsyncResult result);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, AsyncPattern=true, Action="urn:IDeviceRealTimeService/GetWeatherDataMsg")]
        System.IAsyncResult BeginGetWeatherDataMsg(System.AsyncCallback callback, object asyncState);
        
        void EndGetWeatherDataMsg(System.IAsyncResult result);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IDeviceRealTimeServiceCallback {
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="urn:IDeviceRealTimeService/GetRealTimeData")]
        void GetRealTimeData(string data);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="urn:IDeviceRealTimeService/GetAlarmData")]
        void GetAlarmData(string data);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="urn:IDeviceRealTimeService/GetUserEventData")]
        void GetUserEventData(string data);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="urn:IDeviceRealTimeService/GetWeatherData")]
        void GetWeatherData(string data);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IDeviceRealTimeServiceChannel : Scada.Client.VM.DeviceRealTimeService.IDeviceRealTimeService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class DeviceRealTimeServiceClient : System.ServiceModel.DuplexClientBase<Scada.Client.VM.DeviceRealTimeService.IDeviceRealTimeService>, Scada.Client.VM.DeviceRealTimeService.IDeviceRealTimeService {
        
        private BeginOperationDelegate onBeginInitDataDelegate;
        
        private EndOperationDelegate onEndInitDataDelegate;
        
        private System.Threading.SendOrPostCallback onInitDataCompletedDelegate;
        
        private BeginOperationDelegate onBeginGetRealTimeDataListDelegate;
        
        private EndOperationDelegate onEndGetRealTimeDataListDelegate;
        
        private System.Threading.SendOrPostCallback onGetRealTimeDataListCompletedDelegate;
        
        private BeginOperationDelegate onBeginGetAlarmDataListDelegate;
        
        private EndOperationDelegate onEndGetAlarmDataListDelegate;
        
        private System.Threading.SendOrPostCallback onGetAlarmDataListCompletedDelegate;
        
        private BeginOperationDelegate onBeginGetUserEventDataListDelegate;
        
        private EndOperationDelegate onEndGetUserEventDataListDelegate;
        
        private System.Threading.SendOrPostCallback onGetUserEventDataListCompletedDelegate;
        
        private BeginOperationDelegate onBeginGetWeatherDataMsgDelegate;
        
        private EndOperationDelegate onEndGetWeatherDataMsgDelegate;
        
        private System.Threading.SendOrPostCallback onGetWeatherDataMsgCompletedDelegate;
        
        private bool useGeneratedCallback;
        
        private BeginOperationDelegate onBeginOpenDelegate;
        
        private EndOperationDelegate onEndOpenDelegate;
        
        private System.Threading.SendOrPostCallback onOpenCompletedDelegate;
        
        private BeginOperationDelegate onBeginCloseDelegate;
        
        private EndOperationDelegate onEndCloseDelegate;
        
        private System.Threading.SendOrPostCallback onCloseCompletedDelegate;
        
        public DeviceRealTimeServiceClient(System.ServiceModel.InstanceContext callbackInstance) : 
                base(callbackInstance) {
        }
        
        public DeviceRealTimeServiceClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName) : 
                base(callbackInstance, endpointConfigurationName) {
        }
        
        public DeviceRealTimeServiceClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName, string remoteAddress) : 
                base(callbackInstance, endpointConfigurationName, remoteAddress) {
        }
        
        public DeviceRealTimeServiceClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(callbackInstance, endpointConfigurationName, remoteAddress) {
        }
        
        public DeviceRealTimeServiceClient(System.ServiceModel.InstanceContext callbackInstance, System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(callbackInstance, binding, remoteAddress) {
        }
        
        public DeviceRealTimeServiceClient(string endpointConfigurationName) : 
                this(new DeviceRealTimeServiceClientCallback(), endpointConfigurationName) {
        }
        
        private DeviceRealTimeServiceClient(DeviceRealTimeServiceClientCallback callbackImpl, string endpointConfigurationName) : 
                this(new System.ServiceModel.InstanceContext(callbackImpl), endpointConfigurationName) {
            useGeneratedCallback = true;
            callbackImpl.Initialize(this);
        }
        
        public DeviceRealTimeServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                this(new DeviceRealTimeServiceClientCallback(), binding, remoteAddress) {
        }
        
        private DeviceRealTimeServiceClient(DeviceRealTimeServiceClientCallback callbackImpl, System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                this(new System.ServiceModel.InstanceContext(callbackImpl), binding, remoteAddress) {
            useGeneratedCallback = true;
            callbackImpl.Initialize(this);
        }
        
        public DeviceRealTimeServiceClient() : 
                this(new DeviceRealTimeServiceClientCallback()) {
        }
        
        private DeviceRealTimeServiceClient(DeviceRealTimeServiceClientCallback callbackImpl) : 
                this(new System.ServiceModel.InstanceContext(callbackImpl)) {
            useGeneratedCallback = true;
            callbackImpl.Initialize(this);
        }
        
        public System.Net.CookieContainer CookieContainer {
            get {
                System.ServiceModel.Channels.IHttpCookieContainerManager httpCookieContainerManager = this.InnerChannel.GetProperty<System.ServiceModel.Channels.IHttpCookieContainerManager>();
                if ((httpCookieContainerManager != null)) {
                    return httpCookieContainerManager.CookieContainer;
                }
                else {
                    return null;
                }
            }
            set {
                System.ServiceModel.Channels.IHttpCookieContainerManager httpCookieContainerManager = this.InnerChannel.GetProperty<System.ServiceModel.Channels.IHttpCookieContainerManager>();
                if ((httpCookieContainerManager != null)) {
                    httpCookieContainerManager.CookieContainer = value;
                }
                else {
                    throw new System.InvalidOperationException("Unable to set the CookieContainer. Please make sure the binding contains an HttpC" +
                            "ookieContainerBindingElement.");
                }
            }
        }
        
        public event System.EventHandler<System.ComponentModel.AsyncCompletedEventArgs> InitDataCompleted;
        
        public event System.EventHandler<System.ComponentModel.AsyncCompletedEventArgs> GetRealTimeDataListCompleted;
        
        public event System.EventHandler<System.ComponentModel.AsyncCompletedEventArgs> GetAlarmDataListCompleted;
        
        public event System.EventHandler<System.ComponentModel.AsyncCompletedEventArgs> GetUserEventDataListCompleted;
        
        public event System.EventHandler<System.ComponentModel.AsyncCompletedEventArgs> GetWeatherDataMsgCompleted;
        
        public event System.EventHandler<GetRealTimeDataReceivedEventArgs> GetRealTimeDataReceived;
        
        public event System.EventHandler<GetAlarmDataReceivedEventArgs> GetAlarmDataReceived;
        
        public event System.EventHandler<GetUserEventDataReceivedEventArgs> GetUserEventDataReceived;
        
        public event System.EventHandler<GetWeatherDataReceivedEventArgs> GetWeatherDataReceived;
        
        public event System.EventHandler<System.ComponentModel.AsyncCompletedEventArgs> OpenCompleted;
        
        public event System.EventHandler<System.ComponentModel.AsyncCompletedEventArgs> CloseCompleted;
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.IAsyncResult Scada.Client.VM.DeviceRealTimeService.IDeviceRealTimeService.BeginInitData(System.AsyncCallback callback, object asyncState) {
            return base.Channel.BeginInitData(callback, asyncState);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        void Scada.Client.VM.DeviceRealTimeService.IDeviceRealTimeService.EndInitData(System.IAsyncResult result) {
            base.Channel.EndInitData(result);
        }
        
        private System.IAsyncResult OnBeginInitData(object[] inValues, System.AsyncCallback callback, object asyncState) {
            this.VerifyCallbackEvents();
            return ((Scada.Client.VM.DeviceRealTimeService.IDeviceRealTimeService)(this)).BeginInitData(callback, asyncState);
        }
        
        private object[] OnEndInitData(System.IAsyncResult result) {
            ((Scada.Client.VM.DeviceRealTimeService.IDeviceRealTimeService)(this)).EndInitData(result);
            return null;
        }
        
        private void OnInitDataCompleted(object state) {
            if ((this.InitDataCompleted != null)) {
                InvokeAsyncCompletedEventArgs e = ((InvokeAsyncCompletedEventArgs)(state));
                this.InitDataCompleted(this, new System.ComponentModel.AsyncCompletedEventArgs(e.Error, e.Cancelled, e.UserState));
            }
        }
        
        public void InitDataAsync() {
            this.InitDataAsync(null);
        }
        
        public void InitDataAsync(object userState) {
            if ((this.onBeginInitDataDelegate == null)) {
                this.onBeginInitDataDelegate = new BeginOperationDelegate(this.OnBeginInitData);
            }
            if ((this.onEndInitDataDelegate == null)) {
                this.onEndInitDataDelegate = new EndOperationDelegate(this.OnEndInitData);
            }
            if ((this.onInitDataCompletedDelegate == null)) {
                this.onInitDataCompletedDelegate = new System.Threading.SendOrPostCallback(this.OnInitDataCompleted);
            }
            base.InvokeAsync(this.onBeginInitDataDelegate, null, this.onEndInitDataDelegate, this.onInitDataCompletedDelegate, userState);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.IAsyncResult Scada.Client.VM.DeviceRealTimeService.IDeviceRealTimeService.BeginGetRealTimeDataList(System.AsyncCallback callback, object asyncState) {
            return base.Channel.BeginGetRealTimeDataList(callback, asyncState);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        void Scada.Client.VM.DeviceRealTimeService.IDeviceRealTimeService.EndGetRealTimeDataList(System.IAsyncResult result) {
            base.Channel.EndGetRealTimeDataList(result);
        }
        
        private System.IAsyncResult OnBeginGetRealTimeDataList(object[] inValues, System.AsyncCallback callback, object asyncState) {
            this.VerifyCallbackEvents();
            return ((Scada.Client.VM.DeviceRealTimeService.IDeviceRealTimeService)(this)).BeginGetRealTimeDataList(callback, asyncState);
        }
        
        private object[] OnEndGetRealTimeDataList(System.IAsyncResult result) {
            ((Scada.Client.VM.DeviceRealTimeService.IDeviceRealTimeService)(this)).EndGetRealTimeDataList(result);
            return null;
        }
        
        private void OnGetRealTimeDataListCompleted(object state) {
            if ((this.GetRealTimeDataListCompleted != null)) {
                InvokeAsyncCompletedEventArgs e = ((InvokeAsyncCompletedEventArgs)(state));
                this.GetRealTimeDataListCompleted(this, new System.ComponentModel.AsyncCompletedEventArgs(e.Error, e.Cancelled, e.UserState));
            }
        }
        
        public void GetRealTimeDataListAsync() {
            this.GetRealTimeDataListAsync(null);
        }
        
        public void GetRealTimeDataListAsync(object userState) {
            if ((this.onBeginGetRealTimeDataListDelegate == null)) {
                this.onBeginGetRealTimeDataListDelegate = new BeginOperationDelegate(this.OnBeginGetRealTimeDataList);
            }
            if ((this.onEndGetRealTimeDataListDelegate == null)) {
                this.onEndGetRealTimeDataListDelegate = new EndOperationDelegate(this.OnEndGetRealTimeDataList);
            }
            if ((this.onGetRealTimeDataListCompletedDelegate == null)) {
                this.onGetRealTimeDataListCompletedDelegate = new System.Threading.SendOrPostCallback(this.OnGetRealTimeDataListCompleted);
            }
            base.InvokeAsync(this.onBeginGetRealTimeDataListDelegate, null, this.onEndGetRealTimeDataListDelegate, this.onGetRealTimeDataListCompletedDelegate, userState);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.IAsyncResult Scada.Client.VM.DeviceRealTimeService.IDeviceRealTimeService.BeginGetAlarmDataList(System.AsyncCallback callback, object asyncState) {
            return base.Channel.BeginGetAlarmDataList(callback, asyncState);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        void Scada.Client.VM.DeviceRealTimeService.IDeviceRealTimeService.EndGetAlarmDataList(System.IAsyncResult result) {
            base.Channel.EndGetAlarmDataList(result);
        }
        
        private System.IAsyncResult OnBeginGetAlarmDataList(object[] inValues, System.AsyncCallback callback, object asyncState) {
            this.VerifyCallbackEvents();
            return ((Scada.Client.VM.DeviceRealTimeService.IDeviceRealTimeService)(this)).BeginGetAlarmDataList(callback, asyncState);
        }
        
        private object[] OnEndGetAlarmDataList(System.IAsyncResult result) {
            ((Scada.Client.VM.DeviceRealTimeService.IDeviceRealTimeService)(this)).EndGetAlarmDataList(result);
            return null;
        }
        
        private void OnGetAlarmDataListCompleted(object state) {
            if ((this.GetAlarmDataListCompleted != null)) {
                InvokeAsyncCompletedEventArgs e = ((InvokeAsyncCompletedEventArgs)(state));
                this.GetAlarmDataListCompleted(this, new System.ComponentModel.AsyncCompletedEventArgs(e.Error, e.Cancelled, e.UserState));
            }
        }
        
        public void GetAlarmDataListAsync() {
            this.GetAlarmDataListAsync(null);
        }
        
        public void GetAlarmDataListAsync(object userState) {
            if ((this.onBeginGetAlarmDataListDelegate == null)) {
                this.onBeginGetAlarmDataListDelegate = new BeginOperationDelegate(this.OnBeginGetAlarmDataList);
            }
            if ((this.onEndGetAlarmDataListDelegate == null)) {
                this.onEndGetAlarmDataListDelegate = new EndOperationDelegate(this.OnEndGetAlarmDataList);
            }
            if ((this.onGetAlarmDataListCompletedDelegate == null)) {
                this.onGetAlarmDataListCompletedDelegate = new System.Threading.SendOrPostCallback(this.OnGetAlarmDataListCompleted);
            }
            base.InvokeAsync(this.onBeginGetAlarmDataListDelegate, null, this.onEndGetAlarmDataListDelegate, this.onGetAlarmDataListCompletedDelegate, userState);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.IAsyncResult Scada.Client.VM.DeviceRealTimeService.IDeviceRealTimeService.BeginGetUserEventDataList(System.AsyncCallback callback, object asyncState) {
            return base.Channel.BeginGetUserEventDataList(callback, asyncState);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        void Scada.Client.VM.DeviceRealTimeService.IDeviceRealTimeService.EndGetUserEventDataList(System.IAsyncResult result) {
            base.Channel.EndGetUserEventDataList(result);
        }
        
        private System.IAsyncResult OnBeginGetUserEventDataList(object[] inValues, System.AsyncCallback callback, object asyncState) {
            this.VerifyCallbackEvents();
            return ((Scada.Client.VM.DeviceRealTimeService.IDeviceRealTimeService)(this)).BeginGetUserEventDataList(callback, asyncState);
        }
        
        private object[] OnEndGetUserEventDataList(System.IAsyncResult result) {
            ((Scada.Client.VM.DeviceRealTimeService.IDeviceRealTimeService)(this)).EndGetUserEventDataList(result);
            return null;
        }
        
        private void OnGetUserEventDataListCompleted(object state) {
            if ((this.GetUserEventDataListCompleted != null)) {
                InvokeAsyncCompletedEventArgs e = ((InvokeAsyncCompletedEventArgs)(state));
                this.GetUserEventDataListCompleted(this, new System.ComponentModel.AsyncCompletedEventArgs(e.Error, e.Cancelled, e.UserState));
            }
        }
        
        public void GetUserEventDataListAsync() {
            this.GetUserEventDataListAsync(null);
        }
        
        public void GetUserEventDataListAsync(object userState) {
            if ((this.onBeginGetUserEventDataListDelegate == null)) {
                this.onBeginGetUserEventDataListDelegate = new BeginOperationDelegate(this.OnBeginGetUserEventDataList);
            }
            if ((this.onEndGetUserEventDataListDelegate == null)) {
                this.onEndGetUserEventDataListDelegate = new EndOperationDelegate(this.OnEndGetUserEventDataList);
            }
            if ((this.onGetUserEventDataListCompletedDelegate == null)) {
                this.onGetUserEventDataListCompletedDelegate = new System.Threading.SendOrPostCallback(this.OnGetUserEventDataListCompleted);
            }
            base.InvokeAsync(this.onBeginGetUserEventDataListDelegate, null, this.onEndGetUserEventDataListDelegate, this.onGetUserEventDataListCompletedDelegate, userState);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.IAsyncResult Scada.Client.VM.DeviceRealTimeService.IDeviceRealTimeService.BeginGetWeatherDataMsg(System.AsyncCallback callback, object asyncState) {
            return base.Channel.BeginGetWeatherDataMsg(callback, asyncState);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        void Scada.Client.VM.DeviceRealTimeService.IDeviceRealTimeService.EndGetWeatherDataMsg(System.IAsyncResult result) {
            base.Channel.EndGetWeatherDataMsg(result);
        }
        
        private System.IAsyncResult OnBeginGetWeatherDataMsg(object[] inValues, System.AsyncCallback callback, object asyncState) {
            this.VerifyCallbackEvents();
            return ((Scada.Client.VM.DeviceRealTimeService.IDeviceRealTimeService)(this)).BeginGetWeatherDataMsg(callback, asyncState);
        }
        
        private object[] OnEndGetWeatherDataMsg(System.IAsyncResult result) {
            ((Scada.Client.VM.DeviceRealTimeService.IDeviceRealTimeService)(this)).EndGetWeatherDataMsg(result);
            return null;
        }
        
        private void OnGetWeatherDataMsgCompleted(object state) {
            if ((this.GetWeatherDataMsgCompleted != null)) {
                InvokeAsyncCompletedEventArgs e = ((InvokeAsyncCompletedEventArgs)(state));
                this.GetWeatherDataMsgCompleted(this, new System.ComponentModel.AsyncCompletedEventArgs(e.Error, e.Cancelled, e.UserState));
            }
        }
        
        public void GetWeatherDataMsgAsync() {
            this.GetWeatherDataMsgAsync(null);
        }
        
        public void GetWeatherDataMsgAsync(object userState) {
            if ((this.onBeginGetWeatherDataMsgDelegate == null)) {
                this.onBeginGetWeatherDataMsgDelegate = new BeginOperationDelegate(this.OnBeginGetWeatherDataMsg);
            }
            if ((this.onEndGetWeatherDataMsgDelegate == null)) {
                this.onEndGetWeatherDataMsgDelegate = new EndOperationDelegate(this.OnEndGetWeatherDataMsg);
            }
            if ((this.onGetWeatherDataMsgCompletedDelegate == null)) {
                this.onGetWeatherDataMsgCompletedDelegate = new System.Threading.SendOrPostCallback(this.OnGetWeatherDataMsgCompleted);
            }
            base.InvokeAsync(this.onBeginGetWeatherDataMsgDelegate, null, this.onEndGetWeatherDataMsgDelegate, this.onGetWeatherDataMsgCompletedDelegate, userState);
        }
        
        private void OnGetRealTimeDataReceived(object state) {
            if ((this.GetRealTimeDataReceived != null)) {
                object[] results = ((object[])(state));
                this.GetRealTimeDataReceived(this, new GetRealTimeDataReceivedEventArgs(results, null, false, null));
            }
        }
        
        private void OnGetAlarmDataReceived(object state) {
            if ((this.GetAlarmDataReceived != null)) {
                object[] results = ((object[])(state));
                this.GetAlarmDataReceived(this, new GetAlarmDataReceivedEventArgs(results, null, false, null));
            }
        }
        
        private void OnGetUserEventDataReceived(object state) {
            if ((this.GetUserEventDataReceived != null)) {
                object[] results = ((object[])(state));
                this.GetUserEventDataReceived(this, new GetUserEventDataReceivedEventArgs(results, null, false, null));
            }
        }
        
        private void OnGetWeatherDataReceived(object state) {
            if ((this.GetWeatherDataReceived != null)) {
                object[] results = ((object[])(state));
                this.GetWeatherDataReceived(this, new GetWeatherDataReceivedEventArgs(results, null, false, null));
            }
        }
        
        private void VerifyCallbackEvents() {
            if (((this.useGeneratedCallback != true) 
                        && ((((this.GetRealTimeDataReceived != null) 
                        || (this.GetAlarmDataReceived != null)) 
                        || (this.GetUserEventDataReceived != null)) 
                        || (this.GetWeatherDataReceived != null)))) {
                throw new System.InvalidOperationException("Callback events cannot be used when the callback InstanceContext is specified. Pl" +
                        "ease choose between specifying the callback InstanceContext or subscribing to th" +
                        "e callback events.");
            }
        }
        
        private System.IAsyncResult OnBeginOpen(object[] inValues, System.AsyncCallback callback, object asyncState) {
            this.VerifyCallbackEvents();
            return ((System.ServiceModel.ICommunicationObject)(this)).BeginOpen(callback, asyncState);
        }
        
        private object[] OnEndOpen(System.IAsyncResult result) {
            ((System.ServiceModel.ICommunicationObject)(this)).EndOpen(result);
            return null;
        }
        
        private void OnOpenCompleted(object state) {
            if ((this.OpenCompleted != null)) {
                InvokeAsyncCompletedEventArgs e = ((InvokeAsyncCompletedEventArgs)(state));
                this.OpenCompleted(this, new System.ComponentModel.AsyncCompletedEventArgs(e.Error, e.Cancelled, e.UserState));
            }
        }
        
        public void OpenAsync() {
            this.OpenAsync(null);
        }
        
        public void OpenAsync(object userState) {
            if ((this.onBeginOpenDelegate == null)) {
                this.onBeginOpenDelegate = new BeginOperationDelegate(this.OnBeginOpen);
            }
            if ((this.onEndOpenDelegate == null)) {
                this.onEndOpenDelegate = new EndOperationDelegate(this.OnEndOpen);
            }
            if ((this.onOpenCompletedDelegate == null)) {
                this.onOpenCompletedDelegate = new System.Threading.SendOrPostCallback(this.OnOpenCompleted);
            }
            base.InvokeAsync(this.onBeginOpenDelegate, null, this.onEndOpenDelegate, this.onOpenCompletedDelegate, userState);
        }
        
        private System.IAsyncResult OnBeginClose(object[] inValues, System.AsyncCallback callback, object asyncState) {
            return ((System.ServiceModel.ICommunicationObject)(this)).BeginClose(callback, asyncState);
        }
        
        private object[] OnEndClose(System.IAsyncResult result) {
            ((System.ServiceModel.ICommunicationObject)(this)).EndClose(result);
            return null;
        }
        
        private void OnCloseCompleted(object state) {
            if ((this.CloseCompleted != null)) {
                InvokeAsyncCompletedEventArgs e = ((InvokeAsyncCompletedEventArgs)(state));
                this.CloseCompleted(this, new System.ComponentModel.AsyncCompletedEventArgs(e.Error, e.Cancelled, e.UserState));
            }
        }
        
        public void CloseAsync() {
            this.CloseAsync(null);
        }
        
        public void CloseAsync(object userState) {
            if ((this.onBeginCloseDelegate == null)) {
                this.onBeginCloseDelegate = new BeginOperationDelegate(this.OnBeginClose);
            }
            if ((this.onEndCloseDelegate == null)) {
                this.onEndCloseDelegate = new EndOperationDelegate(this.OnEndClose);
            }
            if ((this.onCloseCompletedDelegate == null)) {
                this.onCloseCompletedDelegate = new System.Threading.SendOrPostCallback(this.OnCloseCompleted);
            }
            base.InvokeAsync(this.onBeginCloseDelegate, null, this.onEndCloseDelegate, this.onCloseCompletedDelegate, userState);
        }
        
        protected override Scada.Client.VM.DeviceRealTimeService.IDeviceRealTimeService CreateChannel() {
            return new DeviceRealTimeServiceClientChannel(this);
        }
        
        private class DeviceRealTimeServiceClientCallback : object, IDeviceRealTimeServiceCallback {
            
            private DeviceRealTimeServiceClient proxy;
            
            public void Initialize(DeviceRealTimeServiceClient proxy) {
                this.proxy = proxy;
            }
            
            public void GetRealTimeData(string data) {
                this.proxy.OnGetRealTimeDataReceived(new object[] {
                            data});
            }
            
            public void GetAlarmData(string data) {
                this.proxy.OnGetAlarmDataReceived(new object[] {
                            data});
            }
            
            public void GetUserEventData(string data) {
                this.proxy.OnGetUserEventDataReceived(new object[] {
                            data});
            }
            
            public void GetWeatherData(string data) {
                this.proxy.OnGetWeatherDataReceived(new object[] {
                            data});
            }
        }
        
        private class DeviceRealTimeServiceClientChannel : ChannelBase<Scada.Client.VM.DeviceRealTimeService.IDeviceRealTimeService>, Scada.Client.VM.DeviceRealTimeService.IDeviceRealTimeService {
            
            public DeviceRealTimeServiceClientChannel(System.ServiceModel.DuplexClientBase<Scada.Client.VM.DeviceRealTimeService.IDeviceRealTimeService> client) : 
                    base(client) {
            }
            
            public System.IAsyncResult BeginInitData(System.AsyncCallback callback, object asyncState) {
                object[] _args = new object[0];
                System.IAsyncResult _result = base.BeginInvoke("InitData", _args, callback, asyncState);
                return _result;
            }
            
            public void EndInitData(System.IAsyncResult result) {
                object[] _args = new object[0];
                base.EndInvoke("InitData", _args, result);
            }
            
            public System.IAsyncResult BeginGetRealTimeDataList(System.AsyncCallback callback, object asyncState) {
                object[] _args = new object[0];
                System.IAsyncResult _result = base.BeginInvoke("GetRealTimeDataList", _args, callback, asyncState);
                return _result;
            }
            
            public void EndGetRealTimeDataList(System.IAsyncResult result) {
                object[] _args = new object[0];
                base.EndInvoke("GetRealTimeDataList", _args, result);
            }
            
            public System.IAsyncResult BeginGetAlarmDataList(System.AsyncCallback callback, object asyncState) {
                object[] _args = new object[0];
                System.IAsyncResult _result = base.BeginInvoke("GetAlarmDataList", _args, callback, asyncState);
                return _result;
            }
            
            public void EndGetAlarmDataList(System.IAsyncResult result) {
                object[] _args = new object[0];
                base.EndInvoke("GetAlarmDataList", _args, result);
            }
            
            public System.IAsyncResult BeginGetUserEventDataList(System.AsyncCallback callback, object asyncState) {
                object[] _args = new object[0];
                System.IAsyncResult _result = base.BeginInvoke("GetUserEventDataList", _args, callback, asyncState);
                return _result;
            }
            
            public void EndGetUserEventDataList(System.IAsyncResult result) {
                object[] _args = new object[0];
                base.EndInvoke("GetUserEventDataList", _args, result);
            }
            
            public System.IAsyncResult BeginGetWeatherDataMsg(System.AsyncCallback callback, object asyncState) {
                object[] _args = new object[0];
                System.IAsyncResult _result = base.BeginInvoke("GetWeatherDataMsg", _args, callback, asyncState);
                return _result;
            }
            
            public void EndGetWeatherDataMsg(System.IAsyncResult result) {
                object[] _args = new object[0];
                base.EndInvoke("GetWeatherDataMsg", _args, result);
            }
        }
    }
    
    public class GetRealTimeDataReceivedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        public GetRealTimeDataReceivedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        public string data {
            get {
                base.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }
    
    public class GetAlarmDataReceivedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        public GetAlarmDataReceivedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        public string data {
            get {
                base.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }
    
    public class GetUserEventDataReceivedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        public GetUserEventDataReceivedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        public string data {
            get {
                base.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }
    
    public class GetWeatherDataReceivedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        public GetWeatherDataReceivedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        public string data {
            get {
                base.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }
}
