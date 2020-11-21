using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;

namespace Risersoft.API.GSTN.GSTR4
{

    public class CDNURInvoice
    {


        [Required]
        [Display(Name = "Note Value")]
        public double val { get; set; }

        [Required]
        [Display(Name = "Credit note/debit note /  Refund Voucher Number")]
        [MaxLength(16)]
        public string nt_num { get; set; }


        [Required]
        [Display(Name = "Invoice Number")]
        [MaxLength(16)]
        public string inum { get; set; }
        [Required]
        [Display(Name = "Pre GST Regime Dr./ Cr. Notes     ")]
        [MaxLength(50)]
        public double? rsn { get; set; }


        [Required]
        [Display(Name = "Reason for Issuing Dr./ Cr. Notes")]
        public string p_gst { get; set; }

        [Required]
        [Display(Name = "Credit Note/Debit Note/  Refund Voucher date")]
        [RegularExpression("^((0[1-9]|[12][0-9]|3[01])[-](0[1-9]|1[012])[-]((19|20)\\d\\d))$")]
        public string nt_dt { get; set; }

        [Required]
        [Display(Name = "Invoice checksum value")]
        [MaxLength(64)]
        [RegularExpression("^[a-zA-Z0-9]+$")]
        public string chksum { get; set; }

        [Required]
        [Display(Name = "Credit/debit note type/ Refund Voucher")]
        [RegularExpression("[^C/D/R]")]
        public string ntty { get; set; }


        [Required]
        [Display(Name = "Invoice Date")]
        [RegularExpression("^((0[1-9]|[12][0-9]|3[01])[-](0[1-9]|1[012])[-]((19|20)\\d\\d))$")]
        public string idt { get; set; }
        [Required]
        [Display(Name = "Items")]
        public List<B2Bitem> itms { get; set; }
    }

  
}