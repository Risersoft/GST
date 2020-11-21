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
    public class AspOneEWBAuthClient : GSTNApiClientBase, IGSTNAuthProvider

    {
        public string IPAddr { get; set; }

        public TokenResponseModel token { get; set; }
        public string AuthToken()
        {
            return token.auth_token;
        }
        public byte[] DecryptedKey { get; set; }
        public string username { get; set; }
        public string password { get; set; }

        public AspOneEWBAuthClient(string gstin, string userid, string password, string env) : base( gstin, "ASPONE", env, "EWB")
        {
            this.username = userid;
            this.password = password;
            this.SetPathTemplate("/api/ewaybills", "/authenticate", "AUTH");
        }
        protected internal override void BuildHeaders(HttpClient client)
        {
            client.DefaultRequestHeaders.Add("clientid", credential.client_id);
            client.DefaultRequestHeaders.Add("client-secret", credential.client_secret);
            client.DefaultRequestHeaders.Add("username", this.username);
            client.DefaultRequestHeaders.Add("password", this.password);
            client.DefaultRequestHeaders.Add("gstin", this.gstin);
            foreach (var kvp in this.credential.headers)
            {
                client.DefaultRequestHeaders.Add(kvp.Key, kvp.Value);
            }
            this.dicParams.Add("Headers", client.DefaultRequestHeaders.ToString());
        }


        public GSTNResult<TokenResponseModel> RequestToken(string otp)
        {
            dicParams.Clear();
            PwdTokenRequestModel model = new PwdTokenRequestModel();
            var output = this.Post<PwdTokenRequestModel, TokenResponseModel>(model);
            if (output.Data != null)
            {
                if (myUtils.cValTN(output.Data.expiry) == 0 && output.Data.auth_token_expires_at.HasValue)
                    output.Data.expiry = (int)((output.Data.auth_token_expires_at.Value - DateTime.Now).TotalMinutes);
                if (output.Data.expiry <= 0) output.Data.expiry = 360;

            }
            return output;
        }

        GSPCredentials IGSTNAuthProvider.credential()
        {
            return this.credential;
        }

        public GSTNResult<TokenResponseModel> RefreshToken()
        {
            throw new NotImplementedException();
        }
        public virtual string PathTemplate()
        {
            return "/api/ewaybills";
        }
        
        public void PopulateHeaders(HttpClient client)
        {
            //nothing to do
        }

        public GSTNResult<OTPResponseModel> Logout()
        {
            throw new NotImplementedException();
        }

        public GSTNResult<OTPResponseModel> RequestOTP()
        {
            throw new NotImplementedException();
        }
        public bool Init()
        {
            return true;

        }

        public IDictionary<string, string> GetAuthorizationHeader()
        {
            throw new NotImplementedException();
        }

        public Task<string> GetRefreshToken()
        {
            throw new NotImplementedException();
        }
    }
}
