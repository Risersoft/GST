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
using Newtonsoft.Json.Linq;

namespace Risersoft.API.GSTN
{

    public class AspOnePublicApiClient : GSTNPublicApiClient
    {

        public AspOnePublicApiClient(IGSTNAuthProvider provider) : base(provider)
        {
        }

        protected internal override void BuildHeaders(HttpClient client)
        {
            client.DefaultRequestHeaders.Add("username", credential.username);
            client.DefaultRequestHeaders.Add("password", credential.password);
            client.DefaultRequestHeaders.Add("Token", provider.AuthToken());
            this.dicParams.Add("Headers", client.DefaultRequestHeaders.ToString());

        }

        public override GSTNResult<TaxPayerModel> SearchGSTIN(string gstin)
        {
            this.SetPathTemplate(provider.PathTemplate(), "/search", "PUBLIC");
            var dic = new Dictionary<string, string>();
            this.PrepareQueryString(dic);
            var data = new GSTINSearchModel();
            data.gstins = new string[] { gstin };
            var info = this.Post<GSTINSearchModel, JArray>(data);
            var model = new GSTNResult<TaxPayerModel>();
            foreach (JObject obj in info.Data)
            {
                if (obj.Property("error") == null)
                {
                    model.Data = JsonConvert.DeserializeObject<TaxPayerModel>(obj.ToString());
                }
                else
                {
                    model.Error = JsonConvert.DeserializeObject<ErrorInfo>(obj.ToString());
                }
            }

            return model;


        }
        public override GSTNResult<TaxPayerModel> SearchPAN(string pan)
        {
            this.SetPathTemplate(provider.PathTemplate(), "/search", "PUBLIC");
            var dic = new Dictionary<string, string>();
            this.PrepareQueryString(dic);
            var data = new GSTINSearchModel();
            data.gstins = new string[] { gstin };
            var info = this.Post<GSTINSearchModel, JArray>(data);
            var model = new GSTNResult<TaxPayerModel>();
            foreach (JObject obj in info.Data)
            {
                if (obj.Property("error") == null)
                {
                    model.Data = JsonConvert.DeserializeObject<TaxPayerModel>(obj.ToString());
                }
                else
                {
                    model.Error = JsonConvert.DeserializeObject<ErrorInfo>(obj.ToString());
                }
            }

            return model;


        }
    
      
    }

    public class GSTINSearchModel
    {
        public string[] gstins { get; set; }
        
    }

}
