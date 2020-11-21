using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;

namespace Risersoft.API.GSTN.GSTR1
{

    public class ExpInv
    {
        [Display(Name = "TaxPayer Action")]
        public string flag { get; set; }                    // This not match in table schema


        [Required]
        [Display(Name = "Supplier Invoice Number")]
        [MaxLength(16)]
        public string inum { get; set; }

        [Required]
        [Display(Name = "Supplier Invoice Date")]
        [RegularExpression("^((0[1-9]|[12][0-9]|3[01])[-](0[1-9]|1[012])[-]((19|20)\\d\\d))*$")]
        public string idt { get; set; }

        [Required]
        [Display(Name = "Supplier Invoice Value")]
        public double val { get; set; }

        [Required]
        [Display(Name = "Shipping Bill No. or Bill of Export No.")]
        [MaxLength(7)]
        public string sbnum { get; set; }

        [Required]
        [Display(Name = "Shipping Bill Date. or Bill of Export Date")]
        [RegularExpression("^((0[1-9]|[12][0-9]|3[01])[-](0[1-9]|1[012])[-]((19|20)\\d\\d))*$")]
        public string sbdt { get; set; }


        [Display(Name = "Differential percentage")]
        public double? diff_percent { get; set; }



        [Required]
        [Display(Name = "Item Details")]
        public List<ExpItem> itms { get; set; }

        [Required]
        [Display(Name = "Shipping Port Code")]
        [MaxLength(6)]
        public string sbpcode { get; set; }
        
        [Display(Name = "Invoice checksum value")]
        [MaxLength(64)]
        [MinLength(1)]
        [RegularExpression("^[a-zA-Z0-9]+$")]
        public string chksum { get; set; }
    }

    public class ExpItem
    {
        [Required]
        [Display(Name = "rate as per invoice")]
        public double rt { get; set; }

        
        [Required]
        [Display(Name = "Taxable value of Goods or Service as per invoice")]
        public double txval { get; set; }

        [Required]
        [Display(Name = "IGST Amount as per invoice")]
        public double iamt { get; set; }

    }

    public class Exp
    {
        [Display(Name = "Export Type : With / Without payment of GST")]
        [Required]
        [MaxLength(5)]
        [RegularExpression("^[WPAY/ WOPAY ]")]
        public string exp_typ { get; set; }

        [Required]
        [Display(Name = "Invoice Details")]
        public List<ExpInv> inv { get; set; }
        public string error_cd { get; set; }
        public string error_msg { get; set; }
    }

}