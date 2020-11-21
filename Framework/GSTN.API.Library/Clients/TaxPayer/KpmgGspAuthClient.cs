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
using Risersoft.API.GSTN;
using Risersoft.API.GSTN.Auth;
using risersoft.shared;

namespace Risersoft.API.GSTN
{
    public class KpmgGspAuthClient : GSTNAuthClient

    {

        public clsTokenResponse GspToken;

        public KpmgGspAuthClient(string gstin, string userid, string env, string IPAddr) : base( gstin, userid, "KPMG", env, IPAddr)
        {
            string template= "/test/gstn";
            if (myUtils.IsInList(env, "prod")) template = "/gstn";
            this.SetPathTemplate(template, "/authenticate", "AUTH");
           
        }
        public override bool Init()
        {

            this.GspToken = GetGspToken(this.credential);
            return (this.GspToken != null);
        }
        protected internal override void BuildHeaders(HttpClient client)
        {
            client.DefaultRequestHeaders.Add("clientid", credential.client_id);
            client.DefaultRequestHeaders.Add("client-secret", credential.client_secret);
            client.DefaultRequestHeaders.Add("gstin", this.gstin);
            client.DefaultRequestHeaders.Add("username", this.username);
            //client.DefaultRequestHeaders.Add("ret_period", this.ret_period);
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + this.GspToken.AccessToken);
            client.DefaultRequestHeaders.Add("ip-usr", this.IPAddr);
            client.DefaultRequestHeaders.Add("state-cd", this.gstin.Substring(0, 2));
            client.DefaultRequestHeaders.Add("txn", System.Guid.NewGuid().ToString().Replace("-", ""));
            foreach (var kvp in this.credential.headers)
            {
                client.DefaultRequestHeaders.Add(kvp.Key, kvp.Value);
            }
            this.dicParams.Add("Headers", client.DefaultRequestHeaders.ToString());
       
        }


        public override IDictionary<string, string> GetAuthorizationHeader()
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("Authorization", "Bearer " + this.GspToken.AccessToken);
            return dic;
        }

        public override void PopulateHeaders(HttpClient client)
        {
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + this.GspToken.AccessToken);
        }

        public override string PathTemplate()
        {
            string template= "/test/gstn/returns";
            if (myUtils.IsInList(this.credential.Env, "prod")) template= "/gstn/returns";
            return template;

        }
        public static clsTokenResponse GetGspToken(GSPCredentials credential)
        {
            clsTokenResponse token=null;
            try
            {
                var url = credential.base_url + "/gsp/authenticate?grant_type=token";
                var client = new WebApiClientToken("", "");
                if (myUtils.IsInList(credential.Env, "test"))
                {
                    client.BuildHeaders = _client =>
                    {
                        _client.DefaultRequestHeaders.Add("gspappid", "6BF555F9196F44EA9F4F74AD67EE9F17");
                        _client.DefaultRequestHeaders.Add("gspappsecret", "2517BCE5G0D1FG47E0GA744GA1F15599D947");
                    };
                }
                else
                {
                    client.BuildHeaders = _client =>
                    {
                        _client.DefaultRequestHeaders.Add("gspappid", "260034364AC149F98DF902C3A85566BA");
                        _client.DefaultRequestHeaders.Add("gspappsecret", "58B3D4EAGA57EG4784GBC26GE230536DFF9C");
                    };

                }
                client.PrepareQueryString(url, new Dictionary<string, string>());
                var values = new List<KeyValuePair<string, string>>();
                var requestContent = new FormUrlEncodedContent(values);
                token = client.Post<clsTokenResponse>(requestContent);
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.Message);
            }
            return token;
        }
    }
}


