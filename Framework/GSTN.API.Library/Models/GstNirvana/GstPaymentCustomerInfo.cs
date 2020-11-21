using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSTN.API.Library.Models.GstNirvana
{
    public class GstPaymentCustomerInfo
    {


        public string GSTIN { get; set; }
        public string ReturnPeriod { get; set; }
        public string DocumentType { get; set; }
        public string VouchNum { get; set; }
        public string Dated { get; set; }
        public string POS { get; set; }
       
       
        
        public Decimal? diff_percent { get; set; }
        public string OrigVouchNum { get; set; }
        public string OrigDated { get; set; }

        public string CTIN { get; set; }
        public string PartyName { get; set; }
        public string PartyTaxAreaCode { get; set; }
        public string PartyGstRegType { get; set; }

        public string Remark { get; set; }


        public List<GstPaymentCustomerItemInfo> Items { get; set; }
    }
}
