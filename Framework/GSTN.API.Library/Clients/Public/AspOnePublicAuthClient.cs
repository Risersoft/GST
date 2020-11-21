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
	public class AspOnePublicAuthClient : GSTNPublicAuthClient

	{
        
        public AspOnePublicAuthClient(string env) : base("ASPONE",env,"/api/public_apis/common_authenticate","")
		{

        }
		protected internal override void BuildHeaders(HttpClient client)
		{
            
            client.DefaultRequestHeaders.Add("username", credential.username);
            client.DefaultRequestHeaders.Add("password", credential.password);
            this.dicParams.Add("Headers", client.DefaultRequestHeaders.ToString());

        }



        public override GSTNResult<TokenResponseModel> RequestToken(string otp)
		{
			PwdTokenRequestModel model = new PwdTokenRequestModel {
                
			};
            var output = this.Post<PwdTokenRequestModel, TokenResponseModel>(model);

			token = output.Data;
			return output;

		}
        public override string PathTemplate()
        {
            return "/api/public_apis/search_taxpayer";
        }
    }
}
