﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.239
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// 
// This code was auto-generated by Microsoft.Silverlight.ServiceReference, version 4.0.50826.0
// 
namespace Scada.Client.SL.SystemManagerService {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="SystemManagerService.SystemManagerServiceSoap")]
    public interface SystemManagerServiceSoap {
        
        [System.ServiceModel.OperationContractAttribute(AsyncPattern=true, Action="http://tempuri.org/AddDD", ReplyAction="*")]
        System.IAsyncResult BeginAddDD(int x, int y, System.AsyncCallback callback, object asyncState);
        
        int EndAddDD(System.IAsyncResult result);
        
        [System.ServiceModel.OperationContractAttribute(AsyncPattern=true, Action="http://tempuri.org/GetLoginResultType", ReplyAction="*")]
        System.IAsyncResult BeginGetLoginResultType(Scada.Client.SL.SystemManagerService.GetLoginResultTypeRequest request, System.AsyncCallback callback, object asyncState);
        
        Scada.Client.SL.SystemManagerService.GetLoginResultTypeResponse EndGetLoginResultType(System.IAsyncResult result);
        
        [System.ServiceModel.OperationContractAttribute(AsyncPattern=true, Action="http://tempuri.org/GetWeather", ReplyAction="*")]
        System.IAsyncResult BeginGetWeather(Scada.Client.SL.SystemManagerService.GetWeatherRequest request, System.AsyncCallback callback, object asyncState);
        
        Scada.Client.SL.SystemManagerService.GetWeatherResponse EndGetWeather(System.IAsyncResult result);
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class GetLoginResultTypeRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="GetLoginResultType", Namespace="http://tempuri.org/", Order=0)]
        public Scada.Client.SL.SystemManagerService.GetLoginResultTypeRequestBody Body;
        
        public GetLoginResultTypeRequest() {
        }
        
        public GetLoginResultTypeRequest(Scada.Client.SL.SystemManagerService.GetLoginResultTypeRequestBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://tempuri.org/")]
    public partial class GetLoginResultTypeRequestBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string username;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=1)]
        public string userpwd;
        
        public GetLoginResultTypeRequestBody() {
        }
        
        public GetLoginResultTypeRequestBody(string username, string userpwd) {
            this.username = username;
            this.userpwd = userpwd;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class GetLoginResultTypeResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="GetLoginResultTypeResponse", Namespace="http://tempuri.org/", Order=0)]
        public Scada.Client.SL.SystemManagerService.GetLoginResultTypeResponseBody Body;
        
        public GetLoginResultTypeResponse() {
        }
        
        public GetLoginResultTypeResponse(Scada.Client.SL.SystemManagerService.GetLoginResultTypeResponseBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://tempuri.org/")]
    public partial class GetLoginResultTypeResponseBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=0)]
        public int GetLoginResultTypeResult;
        
        public GetLoginResultTypeResponseBody() {
        }
        
        public GetLoginResultTypeResponseBody(int GetLoginResultTypeResult) {
            this.GetLoginResultTypeResult = GetLoginResultTypeResult;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class GetWeatherRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="GetWeather", Namespace="http://tempuri.org/", Order=0)]
        public Scada.Client.SL.SystemManagerService.GetWeatherRequestBody Body;
        
        public GetWeatherRequest() {
        }
        
        public GetWeatherRequest(Scada.Client.SL.SystemManagerService.GetWeatherRequestBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://tempuri.org/")]
    public partial class GetWeatherRequestBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string cityName;
        
        public GetWeatherRequestBody() {
        }
        
        public GetWeatherRequestBody(string cityName) {
            this.cityName = cityName;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class GetWeatherResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="GetWeatherResponse", Namespace="http://tempuri.org/", Order=0)]
        public Scada.Client.SL.SystemManagerService.GetWeatherResponseBody Body;
        
        public GetWeatherResponse() {
        }
        
        public GetWeatherResponse(Scada.Client.SL.SystemManagerService.GetWeatherResponseBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://tempuri.org/")]
    public partial class GetWeatherResponseBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string GetWeatherResult;
        
        public GetWeatherResponseBody() {
        }
        
        public GetWeatherResponseBody(string GetWeatherResult) {
            this.GetWeatherResult = GetWeatherResult;
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface SystemManagerServiceSoapChannel : Scada.Client.SL.SystemManagerService.SystemManagerServiceSoap, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class AddDDCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        public AddDDCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        public int Result {
            get {
                base.RaiseExceptionIfNecessary();
                return ((int)(this.results[0]));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class GetLoginResultTypeCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        public GetLoginResultTypeCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        public int Result {
            get {
                base.RaiseExceptionIfNecessary();
                return ((int)(this.results[0]));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class GetWeatherCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        public GetWeatherCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        public string Result {
            get {
                base.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class SystemManagerServiceSoapClient : System.ServiceModel.ClientBase<Scada.Client.SL.SystemManagerService.SystemManagerServiceSoap>, Scada.Client.SL.SystemManagerService.SystemManagerServiceSoap {
        
        private BeginOperationDelegate onBeginAddDDDelegate;
        
        private EndOperationDelegate onEndAddDDDelegate;
        
        private System.Threading.SendOrPostCallback onAddDDCompletedDelegate;
        
        private BeginOperationDelegate onBeginGetLoginResultTypeDelegate;
        
        private EndOperationDelegate onEndGetLoginResultTypeDelegate;
        
        private System.Threading.SendOrPostCallback onGetLoginResultTypeCompletedDelegate;
        
        private BeginOperationDelegate onBeginGetWeatherDelegate;
        
        private EndOperationDelegate onEndGetWeatherDelegate;
        
        private System.Threading.SendOrPostCallback onGetWeatherCompletedDelegate;
        
        private BeginOperationDelegate onBeginOpenDelegate;
        
        private EndOperationDelegate onEndOpenDelegate;
        
        private System.Threading.SendOrPostCallback onOpenCompletedDelegate;
        
        private BeginOperationDelegate onBeginCloseDelegate;
        
        private EndOperationDelegate onEndCloseDelegate;
        
        private System.Threading.SendOrPostCallback onCloseCompletedDelegate;
        
        public SystemManagerServiceSoapClient() {
        }
        
        public SystemManagerServiceSoapClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public SystemManagerServiceSoapClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public SystemManagerServiceSoapClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public SystemManagerServiceSoapClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
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
        
        public event System.EventHandler<AddDDCompletedEventArgs> AddDDCompleted;
        
        public event System.EventHandler<GetLoginResultTypeCompletedEventArgs> GetLoginResultTypeCompleted;
        
        public event System.EventHandler<GetWeatherCompletedEventArgs> GetWeatherCompleted;
        
        public event System.EventHandler<System.ComponentModel.AsyncCompletedEventArgs> OpenCompleted;
        
        public event System.EventHandler<System.ComponentModel.AsyncCompletedEventArgs> CloseCompleted;
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.IAsyncResult Scada.Client.SL.SystemManagerService.SystemManagerServiceSoap.BeginAddDD(int x, int y, System.AsyncCallback callback, object asyncState) {
            return base.Channel.BeginAddDD(x, y, callback, asyncState);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        int Scada.Client.SL.SystemManagerService.SystemManagerServiceSoap.EndAddDD(System.IAsyncResult result) {
            return base.Channel.EndAddDD(result);
        }
        
        private System.IAsyncResult OnBeginAddDD(object[] inValues, System.AsyncCallback callback, object asyncState) {
            int x = ((int)(inValues[0]));
            int y = ((int)(inValues[1]));
            return ((Scada.Client.SL.SystemManagerService.SystemManagerServiceSoap)(this)).BeginAddDD(x, y, callback, asyncState);
        }
        
        private object[] OnEndAddDD(System.IAsyncResult result) {
            int retVal = ((Scada.Client.SL.SystemManagerService.SystemManagerServiceSoap)(this)).EndAddDD(result);
            return new object[] {
                    retVal};
        }
        
        private void OnAddDDCompleted(object state) {
            if ((this.AddDDCompleted != null)) {
                InvokeAsyncCompletedEventArgs e = ((InvokeAsyncCompletedEventArgs)(state));
                this.AddDDCompleted(this, new AddDDCompletedEventArgs(e.Results, e.Error, e.Cancelled, e.UserState));
            }
        }
        
        public void AddDDAsync(int x, int y) {
            this.AddDDAsync(x, y, null);
        }
        
        public void AddDDAsync(int x, int y, object userState) {
            if ((this.onBeginAddDDDelegate == null)) {
                this.onBeginAddDDDelegate = new BeginOperationDelegate(this.OnBeginAddDD);
            }
            if ((this.onEndAddDDDelegate == null)) {
                this.onEndAddDDDelegate = new EndOperationDelegate(this.OnEndAddDD);
            }
            if ((this.onAddDDCompletedDelegate == null)) {
                this.onAddDDCompletedDelegate = new System.Threading.SendOrPostCallback(this.OnAddDDCompleted);
            }
            base.InvokeAsync(this.onBeginAddDDDelegate, new object[] {
                        x,
                        y}, this.onEndAddDDDelegate, this.onAddDDCompletedDelegate, userState);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.IAsyncResult Scada.Client.SL.SystemManagerService.SystemManagerServiceSoap.BeginGetLoginResultType(Scada.Client.SL.SystemManagerService.GetLoginResultTypeRequest request, System.AsyncCallback callback, object asyncState) {
            return base.Channel.BeginGetLoginResultType(request, callback, asyncState);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        private System.IAsyncResult BeginGetLoginResultType(string username, string userpwd, System.AsyncCallback callback, object asyncState) {
            Scada.Client.SL.SystemManagerService.GetLoginResultTypeRequest inValue = new Scada.Client.SL.SystemManagerService.GetLoginResultTypeRequest();
            inValue.Body = new Scada.Client.SL.SystemManagerService.GetLoginResultTypeRequestBody();
            inValue.Body.username = username;
            inValue.Body.userpwd = userpwd;
            return ((Scada.Client.SL.SystemManagerService.SystemManagerServiceSoap)(this)).BeginGetLoginResultType(inValue, callback, asyncState);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        Scada.Client.SL.SystemManagerService.GetLoginResultTypeResponse Scada.Client.SL.SystemManagerService.SystemManagerServiceSoap.EndGetLoginResultType(System.IAsyncResult result) {
            return base.Channel.EndGetLoginResultType(result);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        private int EndGetLoginResultType(System.IAsyncResult result) {
            Scada.Client.SL.SystemManagerService.GetLoginResultTypeResponse retVal = ((Scada.Client.SL.SystemManagerService.SystemManagerServiceSoap)(this)).EndGetLoginResultType(result);
            return retVal.Body.GetLoginResultTypeResult;
        }
        
        private System.IAsyncResult OnBeginGetLoginResultType(object[] inValues, System.AsyncCallback callback, object asyncState) {
            string username = ((string)(inValues[0]));
            string userpwd = ((string)(inValues[1]));
            return this.BeginGetLoginResultType(username, userpwd, callback, asyncState);
        }
        
        private object[] OnEndGetLoginResultType(System.IAsyncResult result) {
            int retVal = this.EndGetLoginResultType(result);
            return new object[] {
                    retVal};
        }
        
        private void OnGetLoginResultTypeCompleted(object state) {
            if ((this.GetLoginResultTypeCompleted != null)) {
                InvokeAsyncCompletedEventArgs e = ((InvokeAsyncCompletedEventArgs)(state));
                this.GetLoginResultTypeCompleted(this, new GetLoginResultTypeCompletedEventArgs(e.Results, e.Error, e.Cancelled, e.UserState));
            }
        }
        
        public void GetLoginResultTypeAsync(string username, string userpwd) {
            this.GetLoginResultTypeAsync(username, userpwd, null);
        }
        
        public void GetLoginResultTypeAsync(string username, string userpwd, object userState) {
            if ((this.onBeginGetLoginResultTypeDelegate == null)) {
                this.onBeginGetLoginResultTypeDelegate = new BeginOperationDelegate(this.OnBeginGetLoginResultType);
            }
            if ((this.onEndGetLoginResultTypeDelegate == null)) {
                this.onEndGetLoginResultTypeDelegate = new EndOperationDelegate(this.OnEndGetLoginResultType);
            }
            if ((this.onGetLoginResultTypeCompletedDelegate == null)) {
                this.onGetLoginResultTypeCompletedDelegate = new System.Threading.SendOrPostCallback(this.OnGetLoginResultTypeCompleted);
            }
            base.InvokeAsync(this.onBeginGetLoginResultTypeDelegate, new object[] {
                        username,
                        userpwd}, this.onEndGetLoginResultTypeDelegate, this.onGetLoginResultTypeCompletedDelegate, userState);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.IAsyncResult Scada.Client.SL.SystemManagerService.SystemManagerServiceSoap.BeginGetWeather(Scada.Client.SL.SystemManagerService.GetWeatherRequest request, System.AsyncCallback callback, object asyncState) {
            return base.Channel.BeginGetWeather(request, callback, asyncState);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        private System.IAsyncResult BeginGetWeather(string cityName, System.AsyncCallback callback, object asyncState) {
            Scada.Client.SL.SystemManagerService.GetWeatherRequest inValue = new Scada.Client.SL.SystemManagerService.GetWeatherRequest();
            inValue.Body = new Scada.Client.SL.SystemManagerService.GetWeatherRequestBody();
            inValue.Body.cityName = cityName;
            return ((Scada.Client.SL.SystemManagerService.SystemManagerServiceSoap)(this)).BeginGetWeather(inValue, callback, asyncState);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        Scada.Client.SL.SystemManagerService.GetWeatherResponse Scada.Client.SL.SystemManagerService.SystemManagerServiceSoap.EndGetWeather(System.IAsyncResult result) {
            return base.Channel.EndGetWeather(result);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        private string EndGetWeather(System.IAsyncResult result) {
            Scada.Client.SL.SystemManagerService.GetWeatherResponse retVal = ((Scada.Client.SL.SystemManagerService.SystemManagerServiceSoap)(this)).EndGetWeather(result);
            return retVal.Body.GetWeatherResult;
        }
        
        private System.IAsyncResult OnBeginGetWeather(object[] inValues, System.AsyncCallback callback, object asyncState) {
            string cityName = ((string)(inValues[0]));
            return this.BeginGetWeather(cityName, callback, asyncState);
        }
        
        private object[] OnEndGetWeather(System.IAsyncResult result) {
            string retVal = this.EndGetWeather(result);
            return new object[] {
                    retVal};
        }
        
        private void OnGetWeatherCompleted(object state) {
            if ((this.GetWeatherCompleted != null)) {
                InvokeAsyncCompletedEventArgs e = ((InvokeAsyncCompletedEventArgs)(state));
                this.GetWeatherCompleted(this, new GetWeatherCompletedEventArgs(e.Results, e.Error, e.Cancelled, e.UserState));
            }
        }
        
        public void GetWeatherAsync(string cityName) {
            this.GetWeatherAsync(cityName, null);
        }
        
        public void GetWeatherAsync(string cityName, object userState) {
            if ((this.onBeginGetWeatherDelegate == null)) {
                this.onBeginGetWeatherDelegate = new BeginOperationDelegate(this.OnBeginGetWeather);
            }
            if ((this.onEndGetWeatherDelegate == null)) {
                this.onEndGetWeatherDelegate = new EndOperationDelegate(this.OnEndGetWeather);
            }
            if ((this.onGetWeatherCompletedDelegate == null)) {
                this.onGetWeatherCompletedDelegate = new System.Threading.SendOrPostCallback(this.OnGetWeatherCompleted);
            }
            base.InvokeAsync(this.onBeginGetWeatherDelegate, new object[] {
                        cityName}, this.onEndGetWeatherDelegate, this.onGetWeatherCompletedDelegate, userState);
        }
        
        private System.IAsyncResult OnBeginOpen(object[] inValues, System.AsyncCallback callback, object asyncState) {
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
        
        protected override Scada.Client.SL.SystemManagerService.SystemManagerServiceSoap CreateChannel() {
            return new SystemManagerServiceSoapClientChannel(this);
        }
        
        private class SystemManagerServiceSoapClientChannel : ChannelBase<Scada.Client.SL.SystemManagerService.SystemManagerServiceSoap>, Scada.Client.SL.SystemManagerService.SystemManagerServiceSoap {
            
            public SystemManagerServiceSoapClientChannel(System.ServiceModel.ClientBase<Scada.Client.SL.SystemManagerService.SystemManagerServiceSoap> client) : 
                    base(client) {
            }
            
            public System.IAsyncResult BeginAddDD(int x, int y, System.AsyncCallback callback, object asyncState) {
                object[] _args = new object[2];
                _args[0] = x;
                _args[1] = y;
                System.IAsyncResult _result = base.BeginInvoke("AddDD", _args, callback, asyncState);
                return _result;
            }
            
            public int EndAddDD(System.IAsyncResult result) {
                object[] _args = new object[0];
                int _result = ((int)(base.EndInvoke("AddDD", _args, result)));
                return _result;
            }
            
            public System.IAsyncResult BeginGetLoginResultType(Scada.Client.SL.SystemManagerService.GetLoginResultTypeRequest request, System.AsyncCallback callback, object asyncState) {
                object[] _args = new object[1];
                _args[0] = request;
                System.IAsyncResult _result = base.BeginInvoke("GetLoginResultType", _args, callback, asyncState);
                return _result;
            }
            
            public Scada.Client.SL.SystemManagerService.GetLoginResultTypeResponse EndGetLoginResultType(System.IAsyncResult result) {
                object[] _args = new object[0];
                Scada.Client.SL.SystemManagerService.GetLoginResultTypeResponse _result = ((Scada.Client.SL.SystemManagerService.GetLoginResultTypeResponse)(base.EndInvoke("GetLoginResultType", _args, result)));
                return _result;
            }
            
            public System.IAsyncResult BeginGetWeather(Scada.Client.SL.SystemManagerService.GetWeatherRequest request, System.AsyncCallback callback, object asyncState) {
                object[] _args = new object[1];
                _args[0] = request;
                System.IAsyncResult _result = base.BeginInvoke("GetWeather", _args, callback, asyncState);
                return _result;
            }
            
            public Scada.Client.SL.SystemManagerService.GetWeatherResponse EndGetWeather(System.IAsyncResult result) {
                object[] _args = new object[0];
                Scada.Client.SL.SystemManagerService.GetWeatherResponse _result = ((Scada.Client.SL.SystemManagerService.GetWeatherResponse)(base.EndInvoke("GetWeather", _args, result)));
                return _result;
            }
        }
    }
}
