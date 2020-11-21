using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSTN.API.Library.Models.GstNirvana
{

  
    public class GstInvoiceItemInfo
    {


        public int InvoiceItemGstID { get; set; }
        public Nullable<int> InvoiceID { get; set; }
        public string Description { get; set; }
        public Nullable<int> SortIndex { get; set; }
        public string TY { get; set; }
        public Nullable<decimal> TXVAL { get; set; }
        public Nullable<decimal> RT { get; set; }
        public Nullable<decimal> IAMT { get; set; }
        public Nullable<decimal> CAMT { get; set; }
        public Nullable<decimal> SAMT { get; set; }
        public Nullable<decimal> CSAMT { get; set; }
        public string Uqc { get; set; }
        public Nullable<decimal> Qty { get; set; }
        public Nullable<decimal> BasicRate { get; set; }
        public Nullable<decimal> tx_i { get; set; }
        public Nullable<decimal> tx_c { get; set; }
        public Nullable<decimal> tx_s { get; set; }
        public Nullable<decimal> tx_cs { get; set; }
        public string elg { get; set; }
        public string Hsn_Sc { get; set; }
        public Nullable<decimal> txpd { get; set; }
        public string ZeroTax_Type { get; set; }

 
    }

 
}
