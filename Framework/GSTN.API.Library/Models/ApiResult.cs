using risersoft.shared.portable.Models.Nav;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Risersoft.API.GSTN
{
    public class StatusInfo<TModel>
    {

        //{"form_typ":"R1","status_cd":"ER","action":"SAVE","error_report":{"error_msg":"File could not be uploaded! Download the latest version of Offline tool to generate the JSON file or ensure to validate your uploaded file against the template published at Specification Portal.","error_cd":"RET191106"}}
    
        public string form_typ { get; set; }
        public string status_cd { get; set; }
        public TModel error_report { get; set; }
    }
    public class SaveInfo
    {
        public string trans_id { get; set; }
        public string reference_id { get; set; }
    }
    public class GenerateRequestInfo
    {
        public string gstin { get; set; }
        public string ret_period { get; set; }
    }
    public class GenerateResponseInfo
    {
        public string status { get; set; }
        public string trans_id { get; set; }
    }
    public class FileInfo
    {
        public string status { get; set; }
        public string ack_num { get; set; }
    }

    public class ErrorDetails
    {
        public string error_cd { get; set; }
        public string message { get; set; }
    }

    public class ErrorInfo
    {
        public string status_cd { get; set; }
        public ErrorDetails error { get; set; }
    }
    public class ResponseDataInfo:ErrorInfo
    {
        
        public string data { get; set; }
        public string rek { get; set; }
        public string hmac { get; set; }
    }
    public class UnsignedDataInfo
    {
        public string data { get; set; }
        public string action { get; set; }
        public string hmac {get; set;}
    }
    public class SignedDataInfo
    {
        public string action { get; set; }
        public string data { get; set; }
        public string sign { get; set; }
        public string st { get; set; }
        public string sid { get; set; }
    }
    public class FileDetInfo
    {
        public int fc { get; set; }
        public string ek { get; set; }

        public List<FileDetInfoUrl> urls { get; set; }
    }
    public class FileDetInfoUrl
    {
        public string ul { get; set; }
        public int ic { get; set; }
        public string hash { get; set; }

    }
    public class GSTNPayload<T>
    {
        public clsCollecString<List<int>> IDList { get; set; }
        public T Data { get; set; }

        public GSTNPayload()
        {
            IDList = new clsCollecString<List<int>>();
            Data = (T)Activator.CreateInstance(typeof(T));
        }
    }
    public class GSTNResult<T>
    {
        public int HttpStatusCode { get; set; }

        public T Data { get; set; }

        public string Message { get; set; }

        public string Status { get; set; }

        public ErrorInfo Error { get; set; }

        public string ErrorMessage()
        {
            if ((Error != null) && (Error.error != null)) return Error.error.message; else return "";
        }
    }
}
