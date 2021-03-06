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

namespace Risersoft.API.GSTN
{

    public class GSTNDSClient : GSTNApiClientBase
    {

        public GSTNDSClient(IGSTNAuthProvider provider, string gstin) : base( gstin, provider.credential().code,provider.credential().Env,"TaxPayer")
        {
            this.provider = provider;
            this.SetPathTemplate(provider.PathTemplate(), "/registerdsc", "DSC");
        }

        protected internal override void BuildHeaders(HttpClient client)
        {
            client.DefaultRequestHeaders.Add("clientid", credential.client_id);
            client.DefaultRequestHeaders.Add("client-secret", credential.client_secret);
            client.DefaultRequestHeaders.Add("ip-usr", provider.IPAddr);
            client.DefaultRequestHeaders.Add("state-cd", "11");
            client.DefaultRequestHeaders.Add("txn", System.Guid.NewGuid().ToString().Replace("-", ""));
            client.DefaultRequestHeaders.Add("username", provider.credential().username);
            client.DefaultRequestHeaders.Add("auth-token", provider.AuthToken());
            foreach (var kvp in this.credential.headers)
            {
                client.DefaultRequestHeaders.Add(kvp.Key, kvp.Value);
            }
            this.dicParams.Add("Headers", client.DefaultRequestHeaders.ToString());

        }

        public GSTNResult<string> RegisterDSC(string pannum, string sign)
        {
            RegisterDSCModel model = new RegisterDSCModel
            {
                data = pannum,
                sign = sign
            };

            var output = this.Post<RegisterDSCModel, string>(model);

            return output;

        }

    }
}
