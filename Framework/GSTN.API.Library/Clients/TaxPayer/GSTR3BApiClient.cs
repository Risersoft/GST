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
using Risersoft.API.GSTN.GSTR3B;
using System.Net;
using Risersoft.API.GSTN.Ledger;

namespace Risersoft.API.GSTN
{

    public class GSTR3BApiClient : GSTNReturnsClient<GSTR3BTotal, GSTR3BTotal>
    {
        //action_required=“Y|N“
        public GSTR3BApiClient(IGSTNAuthProvider provider, string gstin, string userid, string ret_period) : base(provider, "/gstr3b", gstin, userid, ret_period, "GSTR3B")
        {
        }

      

        public GSTNResult<ErrorDetails> Offset(Offsetliability data)
        {
            dicParams.Clear();
            var model = this.Encrypt(data);
            model.action = "RETOFFSET";
            var info = this.Put<UnsignedDataInfo, ResponseDataInfo>(model);
            var output = this.Decrypt<ErrorDetails>(info.Data);
            var model2 = this.BuildResult<ErrorDetails>(info, output);
            return model2;
        }

    }
}
