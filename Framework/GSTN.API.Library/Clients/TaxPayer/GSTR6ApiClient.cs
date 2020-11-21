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
using Risersoft.API.GSTN.GSTR6;
using System.Net;

namespace Risersoft.API.GSTN
{

    public class GSTR6ApiClient : GSTNReturnsClient<GSTR6Total,Summary>
    {

        //action_required=“Y|N“
        public GSTR6ApiClient(IGSTNAuthProvider provider,string gstin, string userid,string ret_period) : base(provider, "/gstr6",gstin,userid,ret_period, "GSTR6")
        {
        }


    }
    public class GSTR6AApiClient : GSTNReturnsClient<GSTR6Total, Summary>
    {

        //action_required=“Y|N“
        public GSTR6AApiClient(IGSTNAuthProvider provider, string gstin, string userid, string ret_period) : base(provider, "/gstr6A", gstin, userid, ret_period, "GSTR6")
        {
        }


    }
}
