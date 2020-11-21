using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSTN.API.Library.Models.GstNirvana
{

    public  class GstInvoiceInfo
    {

        public int InvoiceID { get; set; }
        public string CTIN { get; set; }
        public string DocType { get; set; }
        public string Sply_Ty { get; set; }
        public string inv_typ { get; set; }
        public string B2C_typ { get; set; }
        public string BillOf { get; set; }
        public string RCHRG { get; set; }
        public string INUM { get; set; }
        public Nullable<System.DateTime> IDT { get; set; }
        public Nullable<decimal> VAL { get; set; }
        public string Remark { get; set; }
        public string OINum { get; set; }
        public Nullable<System.DateTime> OIdt { get; set; }
        public string ETIN { get; set; }
        public string SBNUM { get; set; }
        public Nullable<System.DateTime> SBDT { get; set; }
        public string boe_num { get; set; }
        public Nullable<System.DateTime> boe_dt { get; set; }
        public string boe_val { get; set; }
        public string oboe_num { get; set; }
        public Nullable<System.DateTime> obe_dt { get; set; }
        public string ntty { get; set; }
        public Nullable<int> CampusID { get; set; }
        public Nullable<int> CustomerID { get; set; }
        public Nullable<int> VendorID { get; set; }
        
        public string exp_typ { get; set; }
        public Nullable<bool> IsAmendment { get; set; }
        public string OTIN { get; set; }
        public string is_sez { get; set; }
        public string stin { get; set; }
        public string p_gst { get; set; }
        public string pos { get; set; }
        public string GSTInvoiceType { get; set; }

      

        public List<GstInvoiceItemInfo> InvoiceItems { get; set; }

    }


}
