using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSTN.API.Library.Models.GstNirvana
{
    public class GstPaymentCustomerItemInfo
    {


        public Decimal AD_AMT { get; set; }
        public Int32 LineSNum { get; set; }
        public Decimal I_RT { get; set; }
        public Decimal IAMT { get; set; }
        public Decimal C_RT { get; set; }
        public Decimal CAMT { get; set; }
        public Decimal S_RT { get; set; }
        public Decimal SAMT { get; set; }
        public Decimal Cess_Rt { get; set; }
        public Decimal CSAMT { get; set; }
    }
}
