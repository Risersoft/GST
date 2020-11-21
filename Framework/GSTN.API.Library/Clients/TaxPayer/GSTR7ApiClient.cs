using Risersoft.API.GSTN.GSTR7;

namespace Risersoft.API.GSTN
{

    public class GSTR7ApiClient : GSTNReturnsClient<SaveGSTR7,GSTR7Summary>
    {
        //action_required=“Y|N“
        public GSTR7ApiClient(IGSTNAuthProvider provider,string gstin, string userid,string ret_period) : base(provider, "/GSTR7", gstin,userid,ret_period, "GSTR7")
        {
        }


    }
   
}
