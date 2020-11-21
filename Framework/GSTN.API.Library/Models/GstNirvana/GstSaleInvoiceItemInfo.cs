using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSTN.API.Library.Models.GstNirvana
{

  
    public class GstSaleInvoiceItemInfo
    {



        public Int32 LineSNum { get; set; }
        public string TY { get; set; }
        public string GstTaxType { get; set; }
        public decimal GrossValue { get; set; }
        public decimal ChargeBeforeTax { get; set; }
        public decimal DiscountBeforeTax { get; set; }
        public decimal TXVAL { get; set; }
        public decimal I_RT { get; set; }
        public decimal IAMT { get; set; }
        public decimal C_RT { get; set; }
        public decimal CAMT { get; set; }
        public decimal S_RT { get; set; }
        public decimal SAMT { get; set; }
        public decimal Cess_Rt { get; set; }
        public decimal CSAMT { get; set; }
        public string Hsn_Sc { get; set; }
        public string Hsn_Desc { get; set; }
        public string Description { get; set; }
        public string Uqc { get; set; }
        public decimal Qty { get; set; }





        

    }

 
}
