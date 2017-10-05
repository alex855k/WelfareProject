﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TestClient.LogConverterService {
    using System.Runtime.Serialization;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="AlarmType", Namespace="http://schemas.datacontract.org/2004/07/LogDataConversionServiceApplication")]
    public enum AlarmType : int {
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        DoorOpen = 0,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        BedSensor = 1,
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="LogConverterService.IService")]
    public interface IService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService/ParseLog", ReplyAction="http://tempuri.org/IService/ParseLogResponse")]
        string[][] ParseLog(string[][] value);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService/ParseLog", ReplyAction="http://tempuri.org/IService/ParseLogResponse")]
        System.Threading.Tasks.Task<string[][]> ParseLogAsync(string[][] value);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService/ParseLog2", ReplyAction="http://tempuri.org/IService/ParseLog2Response")]
        string[][] ParseLog2(string[][] value, string Parser);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService/ParseLog2", ReplyAction="http://tempuri.org/IService/ParseLog2Response")]
        System.Threading.Tasks.Task<string[][]> ParseLog2Async(string[][] value, string Parser);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService/TestMethod", ReplyAction="http://tempuri.org/IService/TestMethodResponse")]
        TestClient.LogConverterService.AlarmType TestMethod();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService/TestMethod", ReplyAction="http://tempuri.org/IService/TestMethodResponse")]
        System.Threading.Tasks.Task<TestClient.LogConverterService.AlarmType> TestMethodAsync();
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IServiceChannel : TestClient.LogConverterService.IService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class ServiceClient : System.ServiceModel.ClientBase<TestClient.LogConverterService.IService>, TestClient.LogConverterService.IService {
        
        public ServiceClient() {
        }
        
        public ServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public ServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public string[][] ParseLog(string[][] value) {
            return base.Channel.ParseLog(value);
        }
        
        public System.Threading.Tasks.Task<string[][]> ParseLogAsync(string[][] value) {
            return base.Channel.ParseLogAsync(value);
        }
        
        public string[][] ParseLog2(string[][] value, string Parser) {
            return base.Channel.ParseLog2(value, Parser);
        }
        
        public System.Threading.Tasks.Task<string[][]> ParseLog2Async(string[][] value, string Parser) {
            return base.Channel.ParseLog2Async(value, Parser);
        }
        
        public TestClient.LogConverterService.AlarmType TestMethod() {
            return base.Channel.TestMethod();
        }
        
        public System.Threading.Tasks.Task<TestClient.LogConverterService.AlarmType> TestMethodAsync() {
            return base.Channel.TestMethodAsync();
        }
    }
}
