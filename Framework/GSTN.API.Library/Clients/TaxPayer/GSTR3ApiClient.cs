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
using Risersoft.API.GSTN.GSTR3;
namespace Risersoft.API.GSTN
{

	public class GSTR3ApiClient : GSTNReturnsClient
	{

		public GSTR3ApiClient(IGSTNAuthProvider provider, string gstin,string userid, string ret_period) : base(provider, "/gstr3", gstin,userid,ret_period, "GSTR3")
		{
		}

		//API call for generating GSTR3 returns
		public GSTNResult<GenerateResponseInfo> Generate(string ret_prd)
		{
            dicParams.Clear();
            GenerateRequestInfo data = new GenerateRequestInfo {
				gstin = gstin,
				ret_period = ret_prd
			};
			var model = this.Encrypt(data);
			model.action = "GENERATE";
			var info = this.Post<UnsignedDataInfo, ResponseDataInfo>(model);
			var output = this.Decrypt<GenerateResponseInfo>(info.Data);
			var model2 = this.BuildResult<GenerateResponseInfo>(info, output);
			return model2;
		}
		//API call for getting all GSTR3 details
		public GSTNResult<GSTR3Total> GetDetails(string ret_prd)
		{
            dicParams.Clear();
            this.PrepareQueryString(new Dictionary<string, string> {
				{
					"gstin",
					gstin
				},
				{
					"action",
					"RETDET"
				},
				{
					"ret_period",
					ret_prd
				}
			});
			var info = this.Get<ResponseDataInfo>();
			var output = this.Decrypt<GSTR3Total>(info.Data);
			var model = this.BuildResult<GSTR3Total>(info, output);
			return model;
		}
		//This API Is used To save entire GSTR3 invoices
		public GSTNResult<SaveInfo> Save(GSTR3SaveModel data)
		{
            dicParams.Clear();
            var model = this.Encrypt(data);
			model.action = "RETSAVE";
			var info = this.Post<UnsignedDataInfo, ResponseDataInfo>(model);
			var output = this.Decrypt<SaveInfo>(info.Data);
			var model2 = this.BuildResult<SaveInfo>(info, output);
			return model2;
		}
		//This API Is used To submit GSTR3 return
		public GSTNResult<FileInfo> File(GSTR3Total data, string sign, string st, string sid)
		{
            dicParams.Clear();
            var model = this.Encrypt(data);
			var model2 = this.FilePayload(model.data, sign, st, sid);
			return model2;

		}
	}
}
