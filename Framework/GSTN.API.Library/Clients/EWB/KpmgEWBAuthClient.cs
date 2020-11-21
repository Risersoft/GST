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
    public class KpmgEWBAuthClient : GSTNApiClientBase, IGSTNAuthProvider

    {
        public clsTokenResponse GspToken;
        public string IPAddr { get; set; }

        public TokenResponseModel token { get; set; }
        public string AuthToken()
        {
            return token.auth_token;
        }
        public byte[] DecryptedKey { get; set; }
        public string username { get; set; }
        public string password { get; set; }

        public KpmgEWBAuthClient(string gstin, string userid, string password, string env) : base(gstin, "KPMG", env, "EWB")
        {
            this.username = userid;
            this.password = password;
            string template = "/test/ewb";
            if (myUtils.IsInList(env, "prod")) template = "/ewb";

            this.SetPathTemplate(template, "/authenticate", "AUTH");
        }
        public bool Init()
        {

            this.GspToken = KpmgGspAuthClient.GetGspToken(this.credential);
            return (this.GspToken != null);
        }

        protected internal override void BuildHeaders(HttpClient client)
        {
            //client.DefaultRequestHeaders.Add("clientid", credential.client_id);
            //client.DefaultRequestHeaders.Add("client-secret", credential.client_secret);
            //client.DefaultRequestHeaders.Add("username", this.username);
            //client.DefaultRequestHeaders.Add("password", this.password);

            //client.DefaultRequestHeaders.Add("Content-Type", "application/json");
            //client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            client.DefaultRequestHeaders.Add("gstin", this.gstin);
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + GspToken.AccessToken);
            foreach (var kvp in this.credential.headers)
            {
                client.DefaultRequestHeaders.Add(kvp.Key, kvp.Value);
            }
            this.dicParams.Add("Headers", client.DefaultRequestHeaders.ToString());
        }


        public GSTNResult<TokenResponseModel> RequestToken(string otp)
        {
            dicParams.Clear();
            PwdTokenRequestModel model = new PwdTokenRequestModel() {
                app_key = EncryptionUtils.RsaEncrypt(this.GetAppKeyBytes(), credential.cert_file_rsa),
                action = "ACCESSTOKEN",
                username = this.username,
                password = EncryptionUtils.RsaEncrypt(UTF8Encoding.UTF8.GetBytes(this.password), credential.cert_file_rsa)
            };
            string finalJson = this.Serialize(model);
            this.LogMessage(finalJson);
            dicParams.Add("RequestPayload", finalJson);
            var output = this.Post<PwdTokenRequestModel, TokenResponseModel>(model);

            try
            {
                if (output.Data != null)
                {
                    output.Data.auth_token = output.Data.authtoken;
                    this.token = output.Data;
                    this.DecryptedKey = EncryptionUtils.AesDecrypt(token.sek, this.GetAppKeyBytes());
                    this.LogMessage(this.token.auth_token);

                    if (myUtils.cValTN(output.Data.expiry) == 0 && output.Data.auth_token_expires_at.HasValue)
                        output.Data.expiry = (int)((output.Data.auth_token_expires_at.Value - DateTime.Now).TotalMinutes);
                    if (output.Data.expiry <= 0) output.Data.expiry = 360;

                }

            }
            catch (Exception ex)
            {
                output = this.BuildExceptionResult<TokenResponseModel>(ex);
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
            return "/api/ewayapi";
        }

        public void PopulateHeaders(HttpClient client)
        {
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + this.GspToken.AccessToken);
            client.DefaultRequestHeaders.Add("authtoken", this.token.auth_token);
        }

        public GSTNResult<OTPResponseModel> Logout()
        {
            var obj = new GSTNResult<OTPResponseModel>();
            return obj;
        }

        public GSTNResult<OTPResponseModel> RequestOTP()
        {
            var obj = new GSTNResult<OTPResponseModel>();
            return obj;

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
