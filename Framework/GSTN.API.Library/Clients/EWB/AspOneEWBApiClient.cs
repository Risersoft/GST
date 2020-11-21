using Risersoft.API.GSTN;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using GSTN.API.Library.Models.EWB;
using Newtonsoft.Json;

namespace GSTN.API.Library.Clients
{
    public class AspOneEWBApiClient: EWBApiClientBase
    {
        public AspOneEWBApiClient(IGSTNAuthProvider AuthProvider, string gstin) : base(AuthProvider, gstin)
        {
           }

        protected internal override void BuildHeaders(HttpClient client)
        {
            client.DefaultRequestHeaders.Add("clientid", credential.client_id);
            client.DefaultRequestHeaders.Add("client-secret", credential.client_secret);
            client.DefaultRequestHeaders.Add("gstin", this.gstin);
            foreach (var kvp in this.credential.headers)
            {
                client.DefaultRequestHeaders.Add(kvp.Key, kvp.Value);
            }
            foreach (var kvp in this.dicHeaders)
            {
                client.DefaultRequestHeaders.Add(kvp.Key, kvp.Value);
            }

            this.dicParams.Add("Headers", client.DefaultRequestHeaders.ToString());

        }
        public override GSTNResult<EWBPostResponseInfo> Generate(string payload)
        {
            dicParams.Clear();
            dicParams.Add("RequestPayload", payload);
            this.PrepareQueryString(credential.base_url + this.path + "/generate_eway_bill", new Dictionary<string, string>());
            var output = this.Post<string, EWBPostResponseInfo>(payload);
            return output;
        }

        public override GSTNResult<EWBPostResponseInfo> Generate(GenerateEWBInfo data)
        {
            var model = new EWBPostRequestInfo { eway_bill = data };
            var payload = JsonConvert.SerializeObject(model,Newtonsoft.Json.Formatting.None,
                        new JsonSerializerSettings
                        {
                            NullValueHandling = NullValueHandling.Ignore
                        });
            return this.Generate(payload);
        }
        public override GSTNResult<EWBCancelResponseInfo> Cancel(string payload)
        {
            dicParams.Clear();
            dicParams.Add("RequestPayload", payload);
            this.PrepareQueryString(credential.base_url + this.path + "/cancel_eway_bill", new Dictionary<string, string>());
            var output = this.Post<string, EWBCancelResponseInfo>(payload);
            return output;
        }
        public override GSTNResult<EWBCancelResponseInfo> Cancel(EWBCancelRequestInfo data)
        {
            var model = new EWBCancelPostRequestInfo { eway_bill = data };
            var payload = JsonConvert.SerializeObject(model,Newtonsoft.Json.Formatting.None,
                        new JsonSerializerSettings
                        {
                            NullValueHandling = NullValueHandling.Ignore
                        });
            return this.Cancel(payload);
        }
        public override GSTNResult<EWBRejectResponseInfo> Reject(string payload)
        {
            dicParams.Clear();
            dicParams.Add("RequestPayload", payload);
            this.PrepareQueryString(credential.base_url + this.path + "/reject_eway_bill", new Dictionary<string, string>());
            var output = this.Post<string, EWBRejectResponseInfo>(payload);
            return output;
        }
        public override GSTNResult<EWBRejectResponseInfo> Reject(EWBRejectRequestInfo data)
        {
            var model = new EWBRejectPostRequestInfo { eway_bill = data };
            var payload = JsonConvert.SerializeObject(model,Newtonsoft.Json.Formatting.None,
                        new JsonSerializerSettings
                        {
                            NullValueHandling = NullValueHandling.Ignore
                        });
            return this.Reject(payload);
        }
        public override  GSTNResult<EWBUpdVehResponseInfo> UpdateVehicle(string payload)
        {
            dicParams.Clear();
            dicParams.Add("RequestPayload", payload);
            this.PrepareQueryString(credential.base_url + this.path + "/update_vehicle_number", new Dictionary<string, string>());
            var output = this.Post<string, EWBUpdVehResponseInfo>(payload);
            return output;
        }

        public override GSTNResult<EWBUpdVehResponseInfo> UpdateVehicle(EWBUpdVehRequestInfo data)
        {
            var model = new EWBUpdVehPostRequestInfo { eway_bill = data };
            var payload = JsonConvert.SerializeObject(model, Newtonsoft.Json.Formatting.None,
                        new JsonSerializerSettings
                        {
                            NullValueHandling = NullValueHandling.Ignore
                        });
            return this.UpdateVehicle(payload);
        }

        public override GSTNResult<EWBInfo> GetDetails(long bill_number)
        {
            dicParams.Clear();
            this.dicHeaders.Clear();
            this.PrepareQueryString(credential.base_url + this.path + "/eway_bill", new Dictionary<string, string> {
            {"bill_number",bill_number.ToString()}
            });

            this.dicHeaders.Add("bill_number", bill_number.ToString());
            var output = this.Get<EWBInfo>();
            return output;
        }
        public override GSTNResult<List<EWBInfo>> GetOtherEWB(DateTime Dated)
        {
            dicParams.Clear();
            this.dicHeaders.Clear();
            this.PrepareQueryString(credential.base_url + this.path + "/eway_bils_generated_by_others", new Dictionary<string, string> {
            {"date",Dated.ToString("dd/MM/YYYY")}
            });

            this.dicHeaders.Add("Date", Dated.ToString("dd/MM/YYYY"));
            var output = this.Get<List<EWBInfo>>();
            return output;
        }

        public override GSTNResult<EWBExtendResponseInfo> Extend(string payload)
        {
            throw new NotImplementedException();
        }

        public override GSTNResult<EWBExtendResponseInfo> Extend(EWBExtendRequestInfo data)
        {
            throw new NotImplementedException();
        }
    }
}
