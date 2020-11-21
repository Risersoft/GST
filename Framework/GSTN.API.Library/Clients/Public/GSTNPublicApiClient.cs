using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Xml.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using Risersoft.API.GSTN;
using Risersoft.API.GSTN.GSTR1;
using Risersoft.API.GSTN.Auth;
using Newtonsoft.Json;
using Risersoft.API.GSTN.Public;
using System.Text;

namespace Risersoft.API.GSTN
{

    public class GSTNPublicApiClient : GSTNApiClientBase
    {
        public GSTNPublicApiClient(IGSTNAuthProvider provider) : base("", provider.credential().code,provider.credential().Env, "PUBLIC")
        {
            this.provider = provider;
        }


        protected internal override void BuildHeaders(HttpClient client)
        {
            client.DefaultRequestHeaders.Add("txn", System.Guid.NewGuid().ToString().Replace("-", ""));
            foreach (var kvp in this.credential.headers)
            {
                client.DefaultRequestHeaders.Add(kvp.Key, kvp.Value);
            }
            provider.PopulateHeaders(client);
            this.dicParams.Add("Headers", client.DefaultRequestHeaders.ToString());
        }

        public virtual GSTNResult<TaxPayerModel> SearchGSTIN(string gstin)
        {
            this.SetPathTemplate(provider.PathTemplate(), "/search", "PUBLIC");
            var dic = new Dictionary<string, string>();
            dic.Add("gstin", gstin);
            dic.Add("action", "TP");
            this.PrepareQueryString(dic);
            var info = this.Get<ResponseDataInfo>();
            var output = this.Decrypt<TaxPayerModel>(info.Data);
            var model = this.BuildResult<TaxPayerModel>(info, output);
            return model;
            

        }

        public virtual GSTNResult<TrackReturnModel> TrackReturns(string gstin, string fy)
        {
            this.SetPathTemplate(provider.PathTemplate(), "/returns", "PUBLIC");
            var dic = new Dictionary<string, string>();
            dic.Add("gstin", gstin);
            dic.Add("action", "RETTRACK");
            dic.Add("fy", fy);
            this.PrepareQueryString(dic);
            var info = this.Get<ResponseDataInfo>();
            var output = this.Decrypt<TrackReturnModel>(info.Data);
            var model = this.BuildResult(info, output);
            return model;


        }


        public virtual GSTNResult<TaxPayerModel> SearchPAN(string pan)
        {
            this.SetPathTemplate(provider.PathTemplate(), "/search", "PUBLIC");
            var dic = new Dictionary<string, string>();
            dic.Add("pan", pan);
            dic.Add("action", "TPPAN");
            this.PrepareQueryString(dic);
            var info = this.Get<ResponseDataInfo>();
            var output = this.Decrypt<TaxPayerModel>(info.Data);
            var model = this.BuildResult<TaxPayerModel>(info, output);
            return model;


        }
        protected internal T Decrypt<T>(ResponseDataInfo output)
        {
            T model = default(T);
            if ((output != null) && (output.data !=null))
            {
                try
                {
                   byte[] decodeJson = Convert.FromBase64String(output.data);
                    string finalJson = Encoding.UTF8.GetString(decodeJson);
                    dicParams["ResponsePayload"]= finalJson;
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
