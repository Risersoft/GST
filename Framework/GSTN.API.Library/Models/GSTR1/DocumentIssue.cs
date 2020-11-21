using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Risersoft.API.GSTN.GSTR1
{
    public class Docs
    {

        [Required]
        [Display(Name = "Serial Number")]
        public int num { get; set; }

        [Required]
        [Display(Name = "From serial number")]
        [MaxLength(16)]
        public string from { get; set; }

        [Required]
        [Display(Name = "To serial number")]
        [MaxLength(16)]
        public string to { get; set; }

        [Required]
        [Display(Name = "Total Number")]
        public int totnum { get; set; }

        [Required]
        [Display(Name = "Cancelled")]
        public int cancel { get; set; }

        [Required]
        [Display(Name = "Net issued")]
        public int net_issue { get; set; }



    }
    public class DocumentIssue

    {
        [Display(Name = "TaxPayer Action")]
        public string flag { get; set; }                    // This not match in table schema


        public List<DocDet> doc_det { get; set; }
    }



    public class DocDet
    {

        [Display(Name = "Document Number")]
        public int? doc_num { get; set; }


        //1=Invoices for outward supply
        //2=Invoices for inward supply from unregistered person 
        //3=Revised Invoice
        //4=Debit Note
        //5=Credit Note
        //6=Receipt voucher


        //7=Payment Voucher
        //8=Refund voucher
        //9=Delivery Challan for job work
        //10=Delivery Challan for supply on approval
        //11=Delivery Challan in case of liquid gas
        //12=Delivery Challan in cases other than by way of supply (excluding at S no. 9 to 11) 
        [Required]
        public List<Docs> docs { get; set; }

    }
}
