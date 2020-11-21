using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSTN.API.Library.Models.GstNirvana
{
    public class GstPurchaseInvoiceInfo
    {


        
        public string GSTIN { get; set; }
        public string ReturnPeriod { get; set; }
        public string TransactionType { get; set; }
        public string InvoiceType { get; set; }
        public string InvoiceNum { get; set; }
        public string InvoiceDate { get; set; }
       
        
        public string CTIN { get; set; }
        public string PartyName { get; set; }
        public string PartyTaxAreaCode { get; set; }
        public string PartyGstRegType { get; set; }
        public string ShipFrom { get; set; }
        public string ShipTo { get; set; }
        public string POS { get; set; }
        public string RCHRG { get; set; }
        public decimal VAL { get; set; }
       
        public string Port_Code { get; set; }

        public Int32? boe_num { get; set; }
        public string boe_dt { get; set; }
        public decimal boe_val { get; set; }
        public string OrigInvoiceTransactionType { get; set; }
        public string OrigInvoiceCTIN { get; set; }
        public string OrigInvoiceNum { get; set; }
        public string OrigInvoiceDate { get; set; }
        public string P_gst { get; set; }
        public string Rsn { get; set; }
        public string OrigNoteNum { get; set; }
        public string OrigNoteDate { get; set; }
        public string CancelDate { get; set; }
        public string IsForeignCurrency { get; set; }
        public decimal Ex_Rt { get; set; }
        public string Remark { get; set; }
       

        public List<GstPurchaseInvoiceItemInfo> Items { get; set; }
    }
}
