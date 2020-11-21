using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Xml.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using Risersoft.API.GSTN;
using GSTN.API.Library.Models;

namespace Risersoft.API.GSTN
{

    public class GSTNReturnsClient : GSTNApiClientBase
    {
        protected internal string ret_period;
        protected internal string username;
       public virtual string SaveAction { get; set; } = "RETSAVE";


        public GSTNReturnsClient(IGSTNAuthProvider AuthProvider, string pathsuffix, string gstin, string userid, string ret_period, string apicategory) : base(gstin, AuthProvider.credential().code, AuthProvider.credential().Env, "TaxPayer")
        {
            this.username = userid.Trim();
            this.ret_period = ret_period;
            provider = AuthProvider;
            this.SetPathTemplate(provider.PathTemplate(), pathsuffix, apicategory);

        }

        protected internal override void BuildHeaders(HttpClient client)
        {
            client.DefaultRequestHeaders.Add("clientid", credential.client_id);
            client.DefaultRequestHeaders.Add("client-secret", credential.client_secret);
            client.DefaultRequestHeaders.Add("ip-usr", provider.IPAddr);
            client.DefaultRequestHeaders.Add("state-cd", this.gstin.Substring(0, 2));
            client.DefaultRequestHeaders.Add("gstin", this.gstin);
            if (!string.IsNullOrEmpty(this.ret_period)) client.DefaultRequestHeaders.Add("ret_period", this.ret_period);
            client.DefaultRequestHeaders.Add("txn", System.Guid.NewGuid().ToString().Replace("-", ""));
            client.DefaultRequestHeaders.Add("username", this.username);

            foreach (var kvp in this.credential.headers)
            {
                client.DefaultRequestHeaders.Add(kvp.Key, kvp.Value);
            }

            provider.PopulateHeaders(client);

            this.dicParams.Add("Headers", client.DefaultRequestHeaders.ToString());

        }


        protected internal UnsignedDataInfo Encrypt<T>(T input)
        {
            string finalJson = this.Serialize(input);
            UnsignedDataInfo info = this.EncryptPayload(finalJson);
            return info;
        }

        protected internal UnsignedDataInfo EncryptPayload(string finalJson)
        {
            UnsignedDataInfo info = new UnsignedDataInfo();
            dicParams.Add("RequestPayload", finalJson);
            byte[] encodeJson = UTF8Encoding.UTF8.GetBytes(finalJson);
            string base64Payload = Convert.ToBase64String(encodeJson);
            return this.EncryptBase64(base64Payload);
        }
        protected internal UnsignedDataInfo EncryptBase64(string base64Payload)
        {
            UnsignedDataInfo info = new UnsignedDataInfo();
            byte[] jsonData = UTF8Encoding.UTF8.GetBytes(base64Payload);
            info.data = EncryptionUtils.AesEncrypt(jsonData, provider.DecryptedKey);
            info.hmac = EncryptionUtils.GenerateHMAC(jsonData, provider.DecryptedKey);
            return info;
        }


        protected internal virtual GSTNResult<FileInfo> FilePayload(string payload, string sign, string st, string sid)
        {
            SignedDataInfo model = new SignedDataInfo
            {
                action = "RETFILE",
                data = payload,
                sign = sign,
                st = st,
                sid = sid
            };
            var str1 = JsonConvert.SerializeObject(model);
            dicParams["RequestPayload"] = str1;

            var info = this.Post<SignedDataInfo, ResponseDataInfo>(model);
            var output = this.Decrypt<FileInfo>(info.Data);
            var model2 = this.BuildResult<FileInfo>(info, output);
            if ((model2 != null) && (model2.Data != null)) this.LogMessage("Obtained Result:" + model2.Data.ack_num + System.Environment.NewLine);
            return model2;

        }
        public GSTNResult<SaveInfo> Submit()
        {
            dicParams.Clear();
            GenerateRequestInfo model = new GenerateRequestInfo()
            {
                gstin = gstin,
                ret_period = ret_period
            };

            var model2 = this.Encrypt(model);
            model2.action = "RETSUBMIT";

            var info = this.Post<UnsignedDataInfo, ResponseDataInfo>(model2);
            var output = this.Decrypt<SaveInfo>(info.Data);
            if (output != null)
            {
                this.dicParams.Add("reference_id", output.reference_id);
                this.LogMessage("Obtained Result:" + output.reference_id + System.Environment.NewLine);

            }
            var result = this.BuildResult<SaveInfo>(info, output);
            return result;

        }

        //This API Is To Get File Details
        public GSTNResult<FileDetInfo> GetFileDetails(string token)
        {
            dicParams.Clear();
            this.PrepareQueryString(credential.base_url + this.BasePath, new Dictionary<string, string> {
            {"gstin",gstin},
            {"action","FILEDET"},
            {"ret_period",ret_period},
            {"token" , token}
            });
            var info = this.Get<ResponseDataInfo>();
            var output = this.Decrypt<FileDetInfo>(info.Data);
            var model = this.BuildResult<FileDetInfo>(info, output);
            return model;

        }

        protected internal T Decrypt<T>(ResponseDataInfo output)
        {
            T model = default(T);
            if (output != null)
            {
                model = this.Decrypt<T>(output.data, output.rek, output.hmac, output.error);
            }
            return model;
        }
        protected internal T Decrypt<T>(string data, string ek, string hmac, ErrorDetails error)
        {
            T model = default(T);
            if (String.IsNullOrEmpty(data))
            {
                if (error != null) dicParams.Add("ResponseError", error.message);

            }
            else
            {
                try
                {
                    byte[] decryptREK = EncryptionUtils.AesDecrypt(ek, provider.DecryptedKey);
                    byte[] jsonData = EncryptionUtils.AesDecrypt(data, decryptREK);
                    if (!String.IsNullOrEmpty(hmac))
                    {
                        string testHmac = EncryptionUtils.GenerateHMAC(jsonData, decryptREK);
                        this.LogMessage("HMAC Match:" + (hmac == testHmac));
                    }
                    string base64Payload = UTF8Encoding.UTF8.GetString(jsonData);
                    byte[] decodeJson = Convert.FromBase64String(base64Payload);
                    string finalJson = Encoding.UTF8.GetString(decodeJson);
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
    public class GSTNReturnsClient<TData, TSummary> : GSTNReturnsClient where TData : GSTRBase
    {
        public GSTNReturnsClient(IGSTNAuthProvider AuthProvider, string pathsuffix, string gstin, string userid, string ret_period, string apicategory) : base(AuthProvider, pathsuffix, gstin, userid, ret_period, apicategory)
        {

        }


        public GSTNResult<TData> BuildResult(GSTNResult<ResponseDataInfo> info)
        {
            GSTNResult<TData> model = new GSTNResult<TData>();
            TData output = default(TData);
            if (info.Data != null)
            {
                if (info.Data.error == null)
                    output = this.Decrypt<TData>(info.Data);
                else
                    this.dicParams.Add("ResponseError", info.Data.error.error_cd + " - " + info.Data.error.message);
            }
            if (output != null) this.dicParams.Add("token", output.token);
            model = this.BuildResult(info, output);
            return model;

        }
        //This API Is To Get status of return
        public GSTNResult<StatusInfo<TData>> GetStatus(string reference_id)
        {
            dicParams.Clear();
            this.PrepareQueryString(credential.base_url + this.BasePath, new Dictionary<string, string> {
            {"gstin",gstin},
            {"action","RETSTATUS"},
            {"ret_period",ret_period},
            {"ref_id" ,reference_id}
            });
            var info = this.Get<ResponseDataInfo>();
            var output = this.Decrypt<StatusInfo<TData>>(info.Data);
            var model = this.BuildResult(info, output);
            return model;

        }
        //API call for getting all ctin wise section for a return period. sec_nm = B2B / B2BA / CDNR / CDNRA
        public GSTNResult<TData> GetSection(string sec_nm, string ctin, string action_required, string from_time)
        {
            dicParams.Clear();
            var dic = new Dictionary<string, string>();
            dic.Add("gstin", this.gstin);
            dic.Add("ret_period", this.ret_period);
            dic.Add("action", sec_nm);
            if (!string.IsNullOrEmpty(action_required)) dic.Add("action_required", action_required);
            if (!string.IsNullOrEmpty(from_time)) dic.Add("from_time", from_time);
            if (!string.IsNullOrEmpty(ctin)) dic.Add("ctin", ctin);
            this.PrepareQueryString(dic);
            var info = this.Get<ResponseDataInfo>();
            var model = this.BuildResult(info);
            return model;
        }

        //API call  for getting all state wise section for a return period. sec_nm = B2CL / B2CLA / B2CS / B2CSA
        public GSTNResult<TData> GetSection(string sec_nm, string state_cd)
        {
            dicParams.Clear();
            var dic = new Dictionary<string, string>();
            dic.Add("gstin", this.gstin);
            dic.Add("ret_period", this.ret_period);
            dic.Add("action", sec_nm);
            dic.Add("state_cd", state_cd);
            this.PrepareQueryString(dic);
            var info = this.Get<ResponseDataInfo>();
            var model = this.BuildResult(info);
            return model;

        }
        //API call  for getting all overall section for a return period. sec_nm = NIL / EXP / EXPA / AT / ATA / TXP
        public GSTNResult<TData> GetSection(string sec_nm)
        {
            dicParams.Clear();
            var dic = new Dictionary<string, string>();
            dic.Add("gstin", this.gstin);
            dic.Add("ret_period", this.ret_period);
            dic.Add("action", sec_nm);
            this.PrepareQueryString(dic);
            var info = this.Get<ResponseDataInfo>();
            var model = this.BuildResult(info);
            return model;

        }



        //This API Is To Get the table wise summary Of GSTR1 data
        public GSTNResult<TSummary> GetSummary()
        {
            dicParams.Clear();
            var dic = new Dictionary<string, string>();
            dic.Add("gstin", this.gstin);
            dic.Add("ret_period", this.ret_period);
            dic.Add("action", "RETSUM");
            this.PrepareQueryString(dic);
            var info = this.Get<ResponseDataInfo>();
            var output = this.Decrypt<TSummary>(info.Data);
            var model = this.BuildResult<TSummary>(info, output);
            return model;

        }
        //This API Is used To save entire GSTR1 invoices
        public GSTNResult<SaveInfo> Save(TData data)
        {
            dicParams.Clear();
            var model = this.Encrypt(data);
            model.action = this.SaveAction;
            this.ret_period = data.fp;
            string RequestJson = this.Serialize(model);

            var info = this.Put<UnsignedDataInfo, ResponseDataInfo>(model);
            var output = this.Decrypt<SaveInfo>(info.Data);
            if (output != null) this.dicParams.Add("reference_id", output.reference_id);
            var model2 = this.BuildResult<SaveInfo>(info, output);
            return model2;
        }
        public GSTNResult<FileInfo> File(TSummary summary, string sign, string st, string sid)
        {
            var str1 = this.Serialize(summary);
            return this.File(str1, sign, st, sid);
        }

        //This API Is used To submit GSTR1 return
        public GSTNResult<FileInfo> File(string EncodedSummary, string sign, string st, string sid)
        {
            dicParams.Clear();
            var model = this.EncryptBase64(EncodedSummary);
            var model2 = this.FilePayload(model.data, sign, st, sid);
            return model2;
        }

       
    }
}
