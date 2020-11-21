using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Risersoft.API.GSTN.Public
{
    public class TrackReturnModel
     {
            public List<ReturnFiledModel> EFiledlist { get; set; }
    }

    public partial class ReturnFiledModel
    {
        public string valid { get; set; }
        public string mof { get; set; }
        public string dof { get; set; }
        public string ret_prd { get; set; }
        public string rtntype { get; set; }
        public string arn { get; set; }
        public string status { get; set; }
    }

}
