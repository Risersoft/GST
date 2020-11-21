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
using risersoft.shared.portable.Contracts;

namespace Risersoft.API.GSTN
{
	public class GSTNAuthClient : GSTNApiClientBase, IGSTNAuthProvider

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

        public GSTNAuthClient(string gstin, string userid, string gsp, string env, string IPAddr) : base(gstin, gsp,env,"TaxPayer")
		{
            this.username = userid;
            this.IPAddr = IPAddr;
            this.SetPathTemplate("/taxpayerapi/v{0}", "/authenticate", "AUTH");
		}
        public virtual bool Init()
        {
            return true;

        }


        protected internal override void BuildHeaders(HttpClient client)
		{
			client.DefaultRequestHeaders.Add("clientid", credential.client_id);
			client.DefaultRequestHeaders.Add("client-secret", credential.client_secret);
			client.DefaultRequestHeaders.Add("ip-usr", this.IPAddr);
			client.DefaultRequestHeaders.Add("state-cd", this.gstin.Substring(0,2));
            client.DefaultRequestHeaders.Add("txn", System.Guid.NewGuid().ToString().Replace("-", ""));
            foreach (var kvp in this.credential.headers)
            {
                client.DefaultRequestHeaders.Add(kvp.Key,kvp.Value);
            }
            this.dicParams.Add("Headers", client.DefaultRequestHeaders.ToString());
        }


        public GSTNResult<OTPResponseModel> RequestOTP()
		{
            dicParams.Clear();
            OTPRequestModel model = new OTPRequestModel {
				action = "OTPREQUEST",
				username = this.username,
                app_key = EncryptionUtils.RsaEncrypt(this.GetAppKeyBytes(), credential.cert_file_rsa)
        };
            string finalJson = this.Serialize(model);
            this.LogMessage(finalJson);
            dicParams.Add("RequestPayload", finalJson);
            var output = this.Post<OTPRequestModel, OTPResponseModel>(model);
            return output;
		}
		public GSTNResult<TokenResponseModel> RequestToken(string otp)
		{
            dicParams.Clear();
            OtpTokenRequestModel model = new OtpTokenRequestModel {
				action = "AUTHTOKEN",
				username = this.username
			};
			model.app_key = EncryptionUtils.RsaEncrypt(this.GetAppKeyBytes(), credential.cert_file_rsa);
            byte[] dataToEncrypt = UTF8Encoding.UTF8.GetBytes(otp);
            model.otp = EncryptionUtils.AesEncrypt(dataToEncrypt, this.GetAppKeyBytes());
            string finalJson = this.Serialize(model);
            this.LogMessage(finalJson);
            dicParams.Add("RequestPayload", finalJson);
            var output = this.Post<OtpTokenRequestModel, TokenResponseModel>(model);
            try
            {
                this.token = output.Data;
                this.DecryptedKey = EncryptionUtils.AesDecrypt(token.sek, this.GetAppKeyBytes());
                this.LogMessage(this.token.auth_token);
            }
            catch (Exception ex)
            {
                output = this.BuildExceptionResult<TokenResponseModel>(ex);
            }

			return output;

		}

        public GSTNResult<TokenResponseModel> RefreshToken()
        {
            dicParams.Clear();
            RefreshTokenModel model = new RefreshTokenModel
            {
                action = "REFRESHTOKEN",
                username = this.username
            };
            model.app_key = EncryptionUtils.AesEncrypt(this.GetAppKeyBytes(), this.DecryptedKey);
            model.auth_token = this.AuthToken();
            string finalJson = this.Serialize(model);
            dicParams.Add("RequestPayload", finalJson);
            var output = this.Post<RefreshTokenModel, TokenResponseModel>(model);

            this.token = output.Data;
            this.DecryptedKey = EncryptionUtils.AesDecrypt(token.sek, this.GetAppKeyBytes());
            var Decipher = System.Text.Encoding.UTF8.GetString(DecryptedKey);

            return output;

        }
        public GSTNResult<OTPResponseModel> Logout()
        {
            dicParams.Clear();
            RefreshTokenModel model = new RefreshTokenModel
            {
                action = "LOGOUT",
                username = this.username
            };
            model.app_key = EncryptionUtils.RsaEncrypt(this.GetAppKeyBytes(), credential.cert_file_rsa);
            model.auth_token = this.AuthToken();
            string finalJson = this.Serialize(model);
            dicParams.Add("RequestPayload", finalJson);
            var output = this.Post<RefreshTokenModel, OTPResponseModel>(model);

            return output;

        }
        public GSTNResult<OTPResponseModel> Logout(string app_key, string auth_token)
        {
            dicParams.Clear();
            RefreshTokenModel model = new RefreshTokenModel
            {
                action = "LOGOUT",
                username = this.username
            };
            model.app_key = app_key;
            model.auth_token = auth_token;
            string finalJson = this.Serialize(model);
            dicParams.Add("RequestPayload", finalJson);
            var output = this.Post<RefreshTokenModel, OTPResponseModel>(model);

            return output;

        }

        GSPCredentials IGSTNAuthProvider.credential()
        {
            return this.credential;
        }
        public virtual void PopulateHeaders(HttpClient client)
        {
            client.DefaultRequestHeaders.Add("auth-token", this.AuthToken());

        }

        public virtual string PathTemplate()
        {
            return "/taxpayerapi/v{0}/returns";
        }

        public virtual IDictionary<string, string> GetAuthorizationHeader()
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("auth-token", this.AuthToken());
            return dic;
        }

        public Task<string> GetRefreshToken()
        {
           var output = this.RefreshToken();
            var token = output.Data.authtoken;
            return Task.FromResult(token);
        }
    }
}


