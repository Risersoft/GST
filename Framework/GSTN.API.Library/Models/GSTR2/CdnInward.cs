using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
namespace Risersoft.API.GSTN.GSTR2
{


    public class CdnInv
    {
        [Required]
        [Display(Name = "flag for accepting or rejecting a invoice")]
        [MaxLength(1)]
        public string flag { get; set; }



        [Required]
        [Display(Name = "Supplier action flag")]
        public string cflag { get; set; }

        [Required]
        [Display(Name = "Note Value")]
        public double val { get; set; }

        [Required]
        [Display(Name = "Credit/debit note type/ Refund Voucher")]
        [MaxLength(1)]
        [MinLength(1)]
        public string ntty { get; set; }

        [Required]
        [Display(Name = "Credit note/debit note /  Refund Voucher Number")]
        [MaxLength(16)]
        public string nt_num { get; set; }

        [Required]
        [Display(Name = "Credit Note/Debit Note/  Refund Voucher date")]
        [RegularExpression("^((0[1-9]|[12][0-9]|3[01])[-](0[1-9]|1[012])[-]((19|20)\\d\\d))*$")]
        public string nt_dt { get; set; }

        [Required]
        [Display(Name = "Invoice Number")]
        [MaxLength(50)]
        [RegularExpression("^[a-zA-Z0-9]+$")]
        public string inum { get; set; }

        [Required]
        [Display(Name = "Invoice Date")]
        [RegularExpression("^((0[1-9]|[12][0-9]|3[01])[-](0[1-9]|1[012])[-]((19|20)\\d\\d))*$")]
        public string idt { get; set; }

        [Required]
        [Display(Name = "Reason for Issuing Dr./ Cr. Notes")]
        [MaxLength(1)]
        public string rsn { get; set; }



        [Required]
        [Display(Name = "Pre GST Regime Dr./ Cr. Notes")]
        public string p_gst { get; set; }





        [Required]
        [Display(Name = "Original period")]
        public string opd { get; set; }

       
       
        [Required]
        [Display(Name = "Invoice Uploader")]
        public string updby { get; set; }

        [Required]
        [Display(Name = "Items")]
        public List<Itm> itms { get; set; }



      
        [Required]
        [Display(Name = "Invoice Check sum value")]
        [MaxLength(64)]
        public string chksum { get; set; }
    }

    public class CdnInward
    {

        [Required]
        [Display(Name = "Counter party GSTIN")]
        [MaxLength(15)]
        [MinLength(15)]
        [RegularExpression("^[a-zA-Z0-9]+$")]
        public string ctin { get; set; }

        [Required]
        [Display(Name = "counter party Filing Status")]
        public string cfs { get; set; }

        [Required]
        [Display(Name = "Credit/Debit Notes")]
        public List<CdnInv> nt { get; set; }
    }



}