using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;

namespace Risersoft.API.GSTN.GSTR2
{
    public class CDNURInward
    {
        [Required]
        [Display(Name = "List of CDN ")]
        public List<CDNURInv> cdnur { get; set; }
    }
    public class CDNURInv
    {
        [Required]
        [Display(Name = "Invoice Check sum value ")]
        [MaxLength(64)]
        public string chksum { get; set; }

        [Required]
        [Display(Name = "Invoice Number")]
        [MaxLength(16)]
        public string inum { get; set; }

        [Required]
        [Display(Name = "Invoice Date")]
        [RegularExpression("^((0[1-9]|[12][0-9]|3[01])[-](0[1-9]|1[012])[-]((19|20)\\d\\d))*$")]
        public string idt { get; set; }

        
        [Display(Name = "Receiver Gstin")]
        public string rtin { get; set; }

        [Required]
        [Display(Name = "Credit/debit note type/ Refund Voucher")]
        public string ntty { get; set; }



        [Required]
        [Display(Name = "Credit note/debit note /  Refund Voucher Number")]
        [MaxLength(16)]
        public string nt_num { get; set; }




        [Required]
        [Display(Name = "Reason for Issuing Dr./ Cr. Notes")]
        [MaxLength(1)]
        public string rsn { get; set; }
        [Required]
        [Display(Name = "Pre GST Regime Dr./ Cr. Notes")]
        public string p_gst { get; set; }



        [Required]
        [Display(Name = "Credit Note/Debit Note/  Refund Voucher date")]
        [RegularExpression("^((0[1-9]|[12][0-9]|3[01])[-](0[1-9]|1[012])[-]((19|20)\\d\\d))*$")]
        public string nt_dt { get; set; }








        [Required]
        [Display(Name = "Invoice Type")]
        public string inv_typ { get; set; }




        [Required]
        [Display(Name = "Note Value")]
        public double val { get; set; }
       
        
        [Required]
        [Display(Name = "Items")]
        public List<Item> itms { get; set; }
    }



}