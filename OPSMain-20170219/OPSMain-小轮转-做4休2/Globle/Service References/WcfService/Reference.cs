﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.42000
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

namespace Globle.WcfService {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="WcfService.ISkyDataService")]
    public interface ISkyDataService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISkyDataService/DataGetTable", ReplyAction="http://tempuri.org/ISkyDataService/DataGetTableResponse")]
        System.Data.DataTable DataGetTable(string sTableName, string sSQLText);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISkyDataService/DataGetTable2", ReplyAction="http://tempuri.org/ISkyDataService/DataGetTable2Response")]
        System.Data.DataTable DataGetTable2(string sSQLText);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISkyDataService/DataGetCount", ReplyAction="http://tempuri.org/ISkyDataService/DataGetCountResponse")]
        int DataGetCount(string sTableName, string sSQLText);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISkyDataService/GetNowTime", ReplyAction="http://tempuri.org/ISkyDataService/GetNowTimeResponse")]
        System.DateTime GetNowTime();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISkyDataService/DataUpdate", ReplyAction="http://tempuri.org/ISkyDataService/DataUpdateResponse")]
        void DataUpdate(string sTableName, string sSQLText);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISkyDataService/DataUpdateInByte", ReplyAction="http://tempuri.org/ISkyDataService/DataUpdateInByteResponse")]
        void DataUpdateInByte(string sSQLText, byte[] Bt);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISkyDataService/DataUpdate2", ReplyAction="http://tempuri.org/ISkyDataService/DataUpdate2Response")]
        void DataUpdate2(string sSQLText);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISkyDataService/WCFLinkTest", ReplyAction="http://tempuri.org/ISkyDataService/WCFLinkTestResponse")]
        string WCFLinkTest();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISkyDataService/DataConnectionTest", ReplyAction="http://tempuri.org/ISkyDataService/DataConnectionTestResponse")]
        string DataConnectionTest();
        
        // CODEGEN: 操作 UploadFile 以后生成的消息协定不是 RPC，也不是换行文档。
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISkyDataService/UploadFile", ReplyAction="http://tempuri.org/ISkyDataService/UploadFileResponse")]
        Globle.WcfService.UploadFileResponse UploadFile(Globle.WcfService.FileUploadMessage request);
        
        // CODEGEN: 操作 UploadLargeFile 以后生成的消息协定不是 RPC，也不是换行文档。
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISkyDataService/UploadLargeFile", ReplyAction="http://tempuri.org/ISkyDataService/UploadLargeFileResponse")]
        Globle.WcfService.UploadLargeFileResponse UploadLargeFile(Globle.WcfService.FileUploadMessage request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISkyDataService/IfFileExist", ReplyAction="http://tempuri.org/ISkyDataService/IfFileExistResponse")]
        bool IfFileExist(string sSavePath, string sFileName);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISkyDataService/DownLoadFile", ReplyAction="http://tempuri.org/ISkyDataService/DownLoadFileResponse")]
        System.IO.Stream DownLoadFile(string sSavePath, string sFileName);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISkyDataService/DownLoadLargeFile", ReplyAction="http://tempuri.org/ISkyDataService/DownLoadLargeFileResponse")]
        System.IO.Stream DownLoadLargeFile(string sSavePath, string sFileName, int nFirID, int nLength);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISkyDataService/DownLoadFileLength", ReplyAction="http://tempuri.org/ISkyDataService/DownLoadFileLengthResponse")]
        long DownLoadFileLength(string sSavePath, string sFileName);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISkyDataService/DeleteFile", ReplyAction="http://tempuri.org/ISkyDataService/DeleteFileResponse")]
        string DeleteFile(string sSavePath, string sFileName);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISkyDataService/RenameFile", ReplyAction="http://tempuri.org/ISkyDataService/RenameFileResponse")]
        string RenameFile(string sSavePath, string sFileName, string sNewFileName);
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName="FileUploadMessage", WrapperNamespace="http://tempuri.org/", IsWrapped=true)]
    public partial class FileUploadMessage {
        
        [System.ServiceModel.MessageHeaderAttribute(Namespace="http://tempuri.org/")]
        public string FileName;
        
        [System.ServiceModel.MessageHeaderAttribute(Namespace="http://tempuri.org/")]
        public string SavePath;
        
        [System.ServiceModel.MessageHeaderAttribute(Namespace="http://tempuri.org/")]
        public int nState;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://tempuri.org/", Order=0)]
        public System.IO.Stream FileData;
        
        public FileUploadMessage() {
        }
        
        public FileUploadMessage(string FileName, string SavePath, int nState, System.IO.Stream FileData) {
            this.FileName = FileName;
            this.SavePath = SavePath;
            this.nState = nState;
            this.FileData = FileData;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class UploadFileResponse {
        
        public UploadFileResponse() {
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class UploadLargeFileResponse {
        
        public UploadLargeFileResponse() {
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface ISkyDataServiceChannel : Globle.WcfService.ISkyDataService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class SkyDataServiceClient : System.ServiceModel.ClientBase<Globle.WcfService.ISkyDataService>, Globle.WcfService.ISkyDataService {
        
        public SkyDataServiceClient() {
        }
        
        public SkyDataServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public SkyDataServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public SkyDataServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public SkyDataServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public System.Data.DataTable DataGetTable(string sTableName, string sSQLText) {
            return base.Channel.DataGetTable(sTableName, sSQLText);
        }
        
        public System.Data.DataTable DataGetTable2(string sSQLText) {
            return base.Channel.DataGetTable2(sSQLText);
        }
        
        public int DataGetCount(string sTableName, string sSQLText) {
            return base.Channel.DataGetCount(sTableName, sSQLText);
        }
        
        public System.DateTime GetNowTime() {
            return base.Channel.GetNowTime();
        }
        
        public void DataUpdate(string sTableName, string sSQLText) {
            base.Channel.DataUpdate(sTableName, sSQLText);
        }
        
        public void DataUpdateInByte(string sSQLText, byte[] Bt) {
            base.Channel.DataUpdateInByte(sSQLText, Bt);
        }
        
        public void DataUpdate2(string sSQLText) {
            base.Channel.DataUpdate2(sSQLText);
        }
        
        public string WCFLinkTest() {
            return base.Channel.WCFLinkTest();
        }
        
        public string DataConnectionTest() {
            return base.Channel.DataConnectionTest();
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        Globle.WcfService.UploadFileResponse Globle.WcfService.ISkyDataService.UploadFile(Globle.WcfService.FileUploadMessage request) {
            return base.Channel.UploadFile(request);
        }
        
        public void UploadFile(string FileName, string SavePath, int nState, System.IO.Stream FileData) {
            Globle.WcfService.FileUploadMessage inValue = new Globle.WcfService.FileUploadMessage();
            inValue.FileName = FileName;
            inValue.SavePath = SavePath;
            inValue.nState = nState;
            inValue.FileData = FileData;
            Globle.WcfService.UploadFileResponse retVal = ((Globle.WcfService.ISkyDataService)(this)).UploadFile(inValue);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        Globle.WcfService.UploadLargeFileResponse Globle.WcfService.ISkyDataService.UploadLargeFile(Globle.WcfService.FileUploadMessage request) {
            return base.Channel.UploadLargeFile(request);
        }
        
        public void UploadLargeFile(string FileName, string SavePath, int nState, System.IO.Stream FileData) {
            Globle.WcfService.FileUploadMessage inValue = new Globle.WcfService.FileUploadMessage();
            inValue.FileName = FileName;
            inValue.SavePath = SavePath;
            inValue.nState = nState;
            inValue.FileData = FileData;
            Globle.WcfService.UploadLargeFileResponse retVal = ((Globle.WcfService.ISkyDataService)(this)).UploadLargeFile(inValue);
        }
        
        public bool IfFileExist(string sSavePath, string sFileName) {
            return base.Channel.IfFileExist(sSavePath, sFileName);
        }
        
        public System.IO.Stream DownLoadFile(string sSavePath, string sFileName) {
            return base.Channel.DownLoadFile(sSavePath, sFileName);
        }
        
        public System.IO.Stream DownLoadLargeFile(string sSavePath, string sFileName, int nFirID, int nLength) {
            return base.Channel.DownLoadLargeFile(sSavePath, sFileName, nFirID, nLength);
        }
        
        public long DownLoadFileLength(string sSavePath, string sFileName) {
            return base.Channel.DownLoadFileLength(sSavePath, sFileName);
        }
        
        public string DeleteFile(string sSavePath, string sFileName) {
            return base.Channel.DeleteFile(sSavePath, sFileName);
        }
        
        public string RenameFile(string sSavePath, string sFileName, string sNewFileName) {
            return base.Channel.RenameFile(sSavePath, sFileName, sNewFileName);
        }
    }
}
