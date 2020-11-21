using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Risersoft.API.GSTN.GSTR1
{





    public class itms
    {
        [Required]
        [Display(Name = "Serial no")]
        public int num { get; set; }
        [Required]
        [Display(Name = "Item Details")]
        public itmsDet itm_det { get; set; }
    }
    public class itmsDet
    {
        [Display(Name = "Rate")]
        public double rt { get; set; }
        [Display(Name = "Taxable value of Goods or Service as per invoice")]
        public double txval { get; set; }
        [Display(Name = "IGST Amount as per invoice")]
        public double iamt { get; set; }
        [Display(Name = "cess Amount as per invoice")]
        public double csamt { get; set; }
    }
    public class CDNUROutward
    {

        [Display(Name = "TaxPayer Action")]
        public string flag { get; set; }                    // This not match in table schema



        [Required]
        [Display(Name = "Invoice Check sum value")]
        [MaxLength(64)]
        public string chksum { get; set; }

        [Required]
        [Display(Name = "EXPWP/EXPWOP/B2CL")]
        [MaxLength(6)]
        public string typ { get; set; }

        [Required]
        [Display(Name = "Credit/debit note type/Refund Voucher")]
        [RegularExpression("^[C/D/R]")]
        public string ntty { get; set; }


        [Required]
        [Display(Name = "Credit Note/debit note/Refund Voucher Number")]
        [MaxLength(16)]
        public string nt_num { get; set; }

        [Required]
        [Display(Name = "Credit Note/Debit note/Refund Voucher date")]
        [RegularExpression("^((0[1-9]|[12][0-9]|3[01])[-](0[1-9]|1[012])[-]((19|20)\\d\\d))*$")]
        public string nt_dt { get; set; }




        [Display(Name = "Differential percentage")]
        public double? diff_percent { get; set; }

        [Required]
        [Display(Name = "Pre GST Regime Dr./Cr.Notes")]
        [RegularExpression("^[Y/N]")]
        public string p_gst { get; set; }

        [Required]
        [Display(Name = "Original invoice number")]
        [MaxLength(16)]
        public string inum { get; set; }





        [Required]
        [Display(Name = "Invoice date")]
        [RegularExpression("^((0[1-9]|[12][0-9]|3[01])[-](0[1-9]|1[012])[-]((19|20)\\d\\d))*$")]
        public string idt { get; set; }


        [Required]
        [Display(Name = "Total Note Value")]
        public double val { get; set; }

        [Required]
        [Display(Name = "Items")]
        public List<itms> itms { get; set; } 
    }
}
