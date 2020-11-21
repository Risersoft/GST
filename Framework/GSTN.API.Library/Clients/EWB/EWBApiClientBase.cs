using Risersoft.API.GSTN;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using GSTN.API.Library.Models.EWB;
using Newtonsoft.Json;

namespace GSTN.API.Library.Clients
{
    public abstract class EWBApiClientBase: GSTNApiClientBase
    {
        protected internal Dictionary<string, string> dicHeaders;
        public EWBApiClientBase(IGSTNAuthProvider AuthProvider, string gstin) : base( gstin, AuthProvider.credential().code,AuthProvider.credential().Env,"EWB")
        {
            this.provider = AuthProvider;
            dicHeaders = new Dictionary<string, string>();
            this.SetPathTemplate(provider.PathTemplate(),"", "EWB");
        }

        public abstract GSTNResult<EWBPostResponseInfo> Generate(string payload);

        public abstract GSTNResult<EWBPostResponseInfo> Generate(GenerateEWBInfo data);

        public abstract GSTNResult<EWBCancelResponseInfo> Cancel(string payload);

        public abstract GSTNResult<EWBCancelResponseInfo> Cancel(EWBCancelRequestInfo data);

        public abstract GSTNResult<EWBExtendResponseInfo> Extend(string payload);
        public abstract GSTNResult<EWBExtendResponseInfo> Extend(EWBExtendRequestInfo data);
        public abstract GSTNResult<EWBRejectResponseInfo> Reject(string payload);

        public abstract GSTNResult<EWBRejectResponseInfo> Reject(EWBRejectRequestInfo data);

        public abstract GSTNResult<EWBUpdVehResponseInfo> UpdateVehicle(string payload);

        public abstract GSTNResult<EWBUpdVehResponseInfo> UpdateVehicle(EWBUpdVehRequestInfo data);


        public abstract GSTNResult<EWBInfo> GetDetails(long bill_number);
        public abstract GSTNResult<List<EWBInfo>> GetOtherEWB(DateTime Dated);

        protected internal EwayBillApiRequest EncryptPayload(string finalJson)
        {
            dicParams.Add("RequestPayload", finalJson);
            byte[] encodeJson = UTF8Encoding.UTF8.GetBytes(finalJson);
            string base64Payload = Convert.ToBase64String(encodeJson);
            return this.EncryptBase64(base64Payload);
        }
        protected internal EwayBillApiRequest EncryptBase64(string base64Payload)
        {
            EwayBillApiRequest info = new EwayBillApiRequest();
            byte[] jsonData = Convert.FromBase64String(base64Payload);
            info.data = EncryptionUtils.AesEncrypt(jsonData, provider.DecryptedKey);
            return info;
        }
        
        protected internal T Decrypt<T>(string data)
        {
            T model = default(T);
            if (!String.IsNullOrEmpty(data))
            {
                try
                {
                    byte[] jsonData = EncryptionUtils.AesDecrypt(data, provider.DecryptedKey);
                    string finalJson = Encoding.UTF8.GetString(jsonData);
                    dicParams["ResponsePayload"] = finalJson;
                    model = Newtonsoft.Json.JsonConvert.DeserializeObject<T>(finalJson);
                }
                catch (Exception ex)
                {
                    dicParams.Add("ResponseError", ex.Message);
                }
            }
            return model;
        }



    }
}
