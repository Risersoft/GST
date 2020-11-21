using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Risersoft.API.GSTN.GSTR1
{
    public class CDNRInv
    {

        [Required]
        [Display(Name = "Invoice Status")]
        [RegularExpression("^[A/R/N/U/P]")]
        public string flag { get; set; }

        [Required]
        [Display(Name = "Counter Party Flag")]
        [RegularExpression("^[A/R/N/U/P]")]
        public string cflag { get; set; }

       
        [Display(Name = "Original Period")]
        [RegularExpression("^(((19|20)\\d\\d)-(0[1-9]|1[012]))$")]
        public string opd { get; set; }                           //This is not match table schema

        [Required]
        [Display(Name = "Invoice checksum")]
        [MaxLength(64)]
        [MinLength(1)]
        [RegularExpression("^[a-zA-Z0-9]+$")]
        public string chksum { get; set; }

        [Required]
        [Display(Name = "Supplier Invoice Number")]
        [MaxLength(16)]
        public string inum { get; set; }


        [Required]
        [Display(Name = "Credit/debit note type/ Refund Voucher")]
        [RegularExpression("^[C/D/R]")]
        public string ntty { get; set; }

        [Display(Name = "Differential percentage")]
        public double? diff_percent { get; set; }



        [Required]
        [Display(Name = "Supplier Invoice Date")]
        [RegularExpression("^((0[1-9]|[12][0-9]|3[01])[-](0[1-9]|1[012])[-]((19|20)\\d\\d))*$")]
        public string idt { get; set; }

        [Required]
        [Display(Name = "Uploaded By")]
        [RegularExpression("^[R/S]")]
        public string updby { get; set; }


        [Required]
        [Display(Name = "Pre GST Regime Dr./ Cr. Notes")]
        [MaxLength(1)]
        [RegularExpression("^[Y/N]")]
        public string p_gst { get; set; }

        [Required]
        [Display(Name = "Supplier Invoice Value")]
        public double val { get; set; }


        [Required]
        public List<B2Bitem> itms { get; set; }

        [Required]
        [Display(Name = "Credit note/debit note / Refund Voucher Number")]
        [MaxLength(30)]
        [RegularExpression("^[a-zA-Z0-9]+$")]
        public string nt_num { get; set; }

        [Required]
        [Display(Name = "Credit Note/Debit Note/  Refund Voucher date")]
        [RegularExpression("^((0[1-9]|[12][0-9]|3[01])[-](0[1-9]|1[012])[-]((19|20)\\d\\d))*$")]
        public string nt_dt { get; set; }
    }
    public class CDNROutward

    {

        [Required]                                 
        [Display(Name = "Counter party GSTIN")]
        [MaxLength(15)]
        [MinLength(15)]
        [RegularExpression("^[a-zA-Z0-9._\\s]+$")]
        public string ctin { get; set; }


      

        [Required]
        [Display(Name = "GSTR2 filing status of counter party")]
        public string cfs { get; set; }



        [Required]
        public List<CDNRInv> nt { get; set; }
        public string error_cd { get; set; }
        public string error_msg { get; set; }
    }
}
