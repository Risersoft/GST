using Risersoft.API.GSTN;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using GSTN.API.Library.Models.EWB;
using Newtonsoft.Json;
using risersoft.shared;

namespace GSTN.API.Library.Clients
{
    public class KpmgEWBApiClient: EWBApiClientBase
    {
        public KpmgEWBApiClient(IGSTNAuthProvider AuthProvider, string gstin) : base(AuthProvider, gstin)
        {
            string template = "/test/ewb";
            if (myUtils.IsInList(this.credential.Env, "prod")) template = "/ewb";
            this.SetPathTemplate(template, "/ewayapi", "EWB");
        }

        protected internal override void BuildHeaders(HttpClient client)
        {
            client.DefaultRequestHeaders.Add("gstin", this.gstin);

            foreach (var kvp in this.credential.headers)
            {
                client.DefaultRequestHeaders.Add(kvp.Key, kvp.Value);
            }
            foreach (var kvp in this.dicHeaders)
            {
                client.DefaultRequestHeaders.Add(kvp.Key, kvp.Value);
            }
            provider.PopulateHeaders(client);

            this.dicParams.Add("Headers", client.DefaultRequestHeaders.ToString());

        }
        public T HandleResult<T>(GSTNResult<EwayBillApiResponse> info) where T : EWBResponseInfoBase
        {

            T output = Activator.CreateInstance<T>();
            if ((info.Data != null) && (info.Data.data != null)) output = this.Decrypt<T>(info.Data.data);
            if (!string.IsNullOrEmpty(info.Data.error))
            {
                output.errorCodes = Encoding.UTF8.GetString(Convert.FromBase64String(info.Data.error));
                if (this.dicParams.ContainsKey("ResponseError")) this.dicParams.Remove("ResponseError");
                this.dicParams.Add("ResponseError", output.errorCodes);
            }
            return output;
        }

        public override GSTNResult<EWBPostResponseInfo> Generate(string payload)
        {
            dicParams.Clear();
            var model = this.EncryptPayload(payload);
            model.action = "GENEWAYBILL";

            var info = this.Post<EwayBillApiRequest, EwayBillApiResponse>(model);
            var output = this.HandleResult<EWBPostResponseInfo>(info);
            var model2 = this.BuildResult(  System.Net.HttpStatusCode.OK, output);
            return model2;

        }

        public override GSTNResult<EWBPostResponseInfo> Generate(GenerateEWBInfo data)
        {
            //var model = new BulkEWBInfo();
            //model.billLists = new List<GenerateEWBInfo>();
            //model.billLists.Add(data);
            var model = data;
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
            var model = this.EncryptPayload(payload);
            model.action = "CANEWB";

            var info = this.Post<EwayBillApiRequest, EwayBillApiResponse>(model);
            var output = this.HandleResult<EWBCancelResponseInfo> (info);
            var model2 = this.BuildResult(System.Net.HttpStatusCode.OK, output);
            return model2;

        }
        public override GSTNResult<EWBCancelResponseInfo> Cancel(EWBCancelRequestInfo data)
        {
            var model =  data ;
            var payload = JsonConvert.SerializeObject(model,Newtonsoft.Json.Formatting.None,
                        new JsonSerializerSettings
                        {
                            NullValueHandling = NullValueHandling.Ignore
                        });
            return this.Cancel(payload);
        }
        public override GSTNResult<EWBExtendResponseInfo> Extend(string payload)
        {
            dicParams.Clear();
            var model = this.EncryptPayload(payload);
            model.action = "EXTENDVALIDITY";

            var info = this.Post<EwayBillApiRequest, EwayBillApiResponse>(model);
            var output = this.HandleResult<EWBExtendResponseInfo>(info);
            var model2 = this.BuildResult(System.Net.HttpStatusCode.OK, output);
            return model2;

        }
        public override GSTNResult<EWBExtendResponseInfo> Extend(EWBExtendRequestInfo data)
        {
            var model = data;
            var payload = JsonConvert.SerializeObject(model, Newtonsoft.Json.Formatting.None,
                        new JsonSerializerSettings
                        {
                            NullValueHandling = NullValueHandling.Ignore
                        });
            return this.Extend(payload);
        }
        public override GSTNResult<EWBRejectResponseInfo> Reject(string payload)
        {
            dicParams.Clear();
            var model = this.EncryptPayload(payload);
            model.action = "REJEWB";

            var info = this.Post<EwayBillApiRequest, EwayBillApiResponse>(model);
            var output = this.HandleResult<EWBRejectResponseInfo>(info);
            var model2 = this.BuildResult(System.Net.HttpStatusCode.OK, output);
            return model2;


        }
        public override GSTNResult<EWBRejectResponseInfo> Reject(EWBRejectRequestInfo data)
        {
            var model = data ;
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
            var model = this.EncryptPayload(payload);
            model.action = "VEHEWB";

            var info = this.Post<EwayBillApiRequest, EwayBillApiResponse>(model);
            var output = this.HandleResult<EWBUpdVehResponseInfo>(info);
            var model2 = this.BuildResult(System.Net.HttpStatusCode.OK, output);
            return model2;

        }

        public override GSTNResult<EWBUpdVehResponseInfo> UpdateVehicle(EWBUpdVehRequestInfo data)
        {
            var model = data;
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
            this.PrepareQueryString(credential.base_url + this.path + "/GetEwayBill", new Dictionary<string, string> {
            {"ewbNo",bill_number.ToString()}
            });

            this.dicHeaders.Add("ewbNo", bill_number.ToString());
            var output = this.Get<EWBInfo>();
            return output;
        }
        public override GSTNResult<List<EWBInfo>> GetOtherEWB(DateTime Dated)
        {
            dicParams.Clear();
            this.dicHeaders.Clear();
            this.PrepareQueryString(credential.base_url + this.path + "/GetEwayBillsofOtherParty", new Dictionary<string, string> {
            {"date",Dated.ToString("dd/MM/YYYY")}
            });

            this.dicHeaders.Add("Date", Dated.ToString("dd/MM/YYYY"));
            var output = this.Get<List<EWBInfo>>();
            return output;
        }

    }
}
