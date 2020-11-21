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
namespace Risersoft.API.GSTN
{
	public class GSTNPublicAuthClient : GSTNApiClientBase, IGSTNAuthProvider


	{
        public string IPAddr { get; set; }

        public TokenResponseModel token { get; set; }
        public string AuthToken()
        {
            return token.auth_token;
        }

		public byte[] DecryptedKey { get; set; }

        

        public GSTNPublicAuthClient(string gsp, string env) : base( "", gsp,env, "PUBLIC")
        {
            this.SetPathTemplate("/commonapi/v0.2", "/authenticate", "PUBLIC");
         }
        public GSTNPublicAuthClient(string gsp, string env, string pathtemplate, string IPAddr) : base("", gsp,env, "PUBLIC")
        {
            this.SetPathTemplate(pathtemplate,"", "PUBLIC");
            this.IPAddr = IPAddr;
        }
        protected internal override void BuildHeaders(HttpClient client)
		{
            
            client.DefaultRequestHeaders.Add("clientid", credential.client_id);
            client.DefaultRequestHeaders.Add("client-secret", credential.client_secret);
            client.DefaultRequestHeaders.Add("ip-usr", this.IPAddr);
            client.DefaultRequestHeaders.Add("txn", System.Guid.NewGuid().ToString().Replace("-", ""));
            foreach (var kvp in this.credential.headers)
            {
                client.DefaultRequestHeaders.Add(kvp.Key, kvp.Value);
            }
            this.dicParams.Add("Headers", client.DefaultRequestHeaders.ToString());

        }



        public virtual GSTNResult<TokenResponseModel> RequestToken(string otp)
		{
			PwdTokenRequestModel model = new PwdTokenRequestModel {
				action = "ACCESSTOKEN",
				username = credential.username
                
			};
			model.app_key = EncryptionUtils.RsaEncrypt(this.GetAppKeyBytes(), credential.cert_file_rsa);
            byte[] dataToEncrypt = UTF8Encoding.UTF8.GetBytes(credential.password);
            model.password = EncryptionUtils.RsaEncrypt(dataToEncrypt, credential.cert_file_rsa);
            var output = this.Post<PwdTokenRequestModel, TokenResponseModel>(model);

			token = output.Data;
			this.DecryptedKey = EncryptionUtils.AesDecrypt(token.sek, this.GetAppKeyBytes());

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
        public virtual void PopulateHeaders(HttpClient client)
        {
            client.DefaultRequestHeaders.Add("auth-token", this.AuthToken());
            client.DefaultRequestHeaders.Add("ip-usr", this.IPAddr);
            client.DefaultRequestHeaders.Add("state-cd", "11");
            client.DefaultRequestHeaders.Add("clientid", this.credential.client_id);
            client.DefaultRequestHeaders.Add("client-secret", this.credential.client_secret);
            client.DefaultRequestHeaders.Add("username", this.credential.username);


        }
        public virtual string PathTemplate()
        {
            return "/commonapi/v{0}";
        }

        public GSTNResult<OTPResponseModel> Logout()
        {
            throw new NotImplementedException();
        }

        public GSTNResult<OTPResponseModel> RequestOTP()
        {
            throw new NotImplementedException();
        }
        public virtual bool Init()
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
