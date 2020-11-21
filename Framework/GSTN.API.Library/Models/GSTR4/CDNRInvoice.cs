using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;

namespace Risersoft.API.GSTN.GSTR4
{
    
  
    public class CDNRnt
    {



        [Display(Name = "flag for accepting or rejecting a invoice")]
        public string flag { get; set; }


        [Required]
        [Display(Name = "Invoice Uploader")]
        public string updby { get; set; }

        [Required]
        [Display(Name = "Supplier action flag")]
        public string cflag { get; set; }


        [Required]
        [Display(Name = "Invoice Number")]
        [MaxLength(16)]
        public string inum { get; set; }


        [Display(Name = "Invoice checksum value")]
        [MaxLength(64)]
        [RegularExpression("^[a-zA-Z0-9]+$")]
        public string chksum { get; set; }


        [Required]
        [Display(Name = "Invoice Date")]
        [RegularExpression("^((0[1-9]|[12][0-9]|3[01])[-](0[1-9]|1[012])[-]((19|20)\\d\\d))$")]
        public string idt { get; set; }


        [Required]
        [Display(Name = "Credit note/debit note /  Refund Voucher Number")]
        public string nt_num { get; set; }

        [Required]
        [Display(Name = "Credit Note/Debit Note/  Refund Voucher date")]
        [RegularExpression("^((0[1-9]|[12][0-9]|3[01])[-](0[1-9]|1[012])[-]((19|20)\\d\\d))$")]
        public string nt_dt { get; set; }


        [Required]
        [Display(Name = "Credit/debit note type/ Refund Voucher")]
        public string ntty { get; set; }

        [Required]
        [Display(Name = "Note Value")]
        public double val { get; set; }

        [Required]
        [Display(Name = "Pre GST Regime Dr./ Cr. Notes     ")]
        public string rsn { get; set; }

        [Required]
        [Display(Name = "Reason for Issuing Dr./ Cr. Notes")]
        public string p_gst { get; set; }

        [Display(Name = "Original Period")]
        [RegularExpression("^((0[1-9]|1[012])[-]((19|20)\\d\\d))*$")]
        public string opd { get; set; }

        [Required]
        [Display(Name = "Items")]
        public List<B2Bitem> itms { get; set; }


    }





    public class CDNRInvoice
    {

        [Required]
        [Display(Name = "Counter party GSTIN")]
        [MaxLength(15)]
        [MinLength(15)]
        [RegularExpression("^[a-zA-Z0-9._\\s]+$")]
        public string ctin { get; set; }


        [Required]
        [Display(Name = "counter party Filing Status")]
        public string cfs { get; set; }

        [Required]
        [Display(Name = "Credit/Debit Notes  Details ")]
        public List<CDNRnt> nt { get; set; }
    }

}