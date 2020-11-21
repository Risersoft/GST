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
using System.Net;
using Risersoft.API.GSTN.ANX01;
using Risersoft.API.GSTN.ANX02;

namespace Risersoft.API.GSTN
{

    public class GSTR1ApiClient : GSTNReturnsClient<GSTR1Total,SummaryOutward>
    {

        //action_required=“Y|N“
        public GSTR1ApiClient(IGSTNAuthProvider provider,string gstin, string userid,string ret_period) : base(provider, "/gstr1",gstin,userid,ret_period, "GSTR1")
        {
        }


    }
    public class GSTR1AApiClient : GSTNReturnsClient<GSTR1Total, SummaryOutward>
    {

        //action_required=“Y|N“
        public GSTR1AApiClient(IGSTNAuthProvider provider, string gstin, string userid, string ret_period) : base(provider, "/gstr1A", gstin, userid, ret_period, "GSTR1")
        {
        }


    }

    public class ANX01ApiClient : GSTNReturnsClient<SaveAnx01, Anx01Summary>
    {

        //action_required=“Y|N“
        public ANX01ApiClient(IGSTNAuthProvider provider, string gstin, string userid, string ret_period) : base(provider, "/anx01", gstin, userid, ret_period, "ANX01")
        {
        }


    }

}
