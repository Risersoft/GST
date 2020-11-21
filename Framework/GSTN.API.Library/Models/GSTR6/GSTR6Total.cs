using GSTN.API.Library.Models;
using Risersoft.API.GSTN.GSTR6;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Risersoft.API.GSTN.GSTR6
{
    public class GSTR6Total:GSTRBase
    {


        public List<B2bInvoice> b2b { get; set; }
        public List<B2bAInvoice> b2ba { get; set; }
        public List<CDNInvoice> cdn { get; set; }
        public List<CDNAInvoice> cdna { get; set; }
        public List<ISDdetails> isd { get; set; }
        public List<ISDAdetails> isda { get; set; }
    }
}
