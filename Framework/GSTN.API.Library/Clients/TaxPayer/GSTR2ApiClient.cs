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
using Risersoft.API.GSTN.GSTR2;
using Risersoft.API.GSTN.ANX02;
namespace Risersoft.API.GSTN
{

	public class GSTR2ApiClient : GSTNReturnsClient<GSTR2Total,SummaryInward>
	{

		//action_required=“Y|N“
		public GSTR2ApiClient(IGSTNAuthProvider provider, string gstin,string userid, string ret_period) : base(provider, "/gstr2", gstin,userid,ret_period, "GSTR2")
		{
		}


    


	}
    public class GSTR2AApiClient : GSTNReturnsClient<GSTR2Total, SummaryInward>
    {

        //action_required=“Y|N“
        public GSTR2AApiClient(IGSTNAuthProvider provider, string gstin, string userid, string ret_period) : base(provider, "/gstr2a", gstin, userid, ret_period, "GSTR2A")
        {
        }





    }

    public class ANX02ApiClient : GSTNReturnsClient<SaveAnx02, Anx02Summary>
    {

        //action_required=“Y|N“
        public ANX02ApiClient(IGSTNAuthProvider provider, string gstin, string userid, string ret_period) : base(provider, "/anx02", gstin, userid, ret_period, "ANX02")
        {
        }


    }
}
