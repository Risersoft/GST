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
using Risersoft.API.GSTN.GSTR8;
using System.Net;

namespace Risersoft.API.GSTN
{

    public class GSTR8ApiClient : GSTNReturnsClient<SaveGSTR8,GSTR8Summary>
    {
        //action_required=“Y|N“
        public GSTR8ApiClient(IGSTNAuthProvider provider,string gstin, string userid,string ret_period) : base(provider, "/GSTR8", gstin,userid,ret_period, "GSTR8")
        {
        }


    }
   
}
