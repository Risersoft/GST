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
    public class KpmgPublicAuthClient : GSTNPublicAuthClient

    {
        public clsTokenResponse GspToken;


        public KpmgPublicAuthClient(string env) : base(  "KPMG", env)
        {
            string template= "/test/gstn";
            if (myUtils.IsInList(env, "prod")) template = "/gstn";
            this.SetPathTemplate(template, "/authenticate", "AUTH");
           
        }
        public override bool Init()
        {

            this.GspToken = KpmgGspAuthClient.GetGspToken(this.credential);
            return (this.GspToken != null);
        }

        public override GSTNResult<TokenResponseModel> RequestToken(string otp)
        {
            var obj = new GSTNResult<TokenResponseModel>();
            this.token = new TokenResponseModel() { auth_token = this.GspToken.AccessToken, expiry = this.GspToken.ExpirationTime };
            obj.Data = this.token;
            return obj;
        }
        protected internal override void BuildHeaders(HttpClient client)
        {
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + this.token.auth_token);
            client.DefaultRequestHeaders.Add("ip-usr", this.IPAddr);
            client.DefaultRequestHeaders.Add("txn", System.Guid.NewGuid().ToString().Replace("-", ""));
            foreach (var kvp in this.credential.headers)
            {
                client.DefaultRequestHeaders.Add(kvp.Key, kvp.Value);
            }
            this.dicParams.Add("Headers", client.DefaultRequestHeaders.ToString());
            var str1 = client.DefaultRequestHeaders.ToString();
            this.LogMessage(str1);

        }



        public override void PopulateHeaders(HttpClient client)
        {
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + this.token.auth_token);
        }

        public override string PathTemplate()
        {
            return "/gstn/commonapi";
        }

    }
}


