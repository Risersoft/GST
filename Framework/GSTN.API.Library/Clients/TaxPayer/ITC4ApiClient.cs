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
using Risersoft.API.GSTN.ITC4;
using System.Net;

namespace Risersoft.API.GSTN
{

    public class ITC4ApiClient : GSTNReturnsClient<SaveITC4,ITC4Summary>
    {
        public override string SaveAction { get; set; } = "SAVE";
        //action_required=“Y|N“
        public ITC4ApiClient(IGSTNAuthProvider provider,string gstin, string userid,string ret_period) : base(provider, "/itc04", gstin,userid,ret_period, "ITC04")
        {
        }


    }
   
}
