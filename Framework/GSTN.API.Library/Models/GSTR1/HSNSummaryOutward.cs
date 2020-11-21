using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;

namespace Risersoft.API.GSTN.GSTR1
{
   

    public class SummaryData
    {
        [Required]
        [Display(Name = "Serial Number")]
        [MinLength(1)]
        public int num { get; set; }


        [Required]
        [Display(Name = "HSN of Goods or Services as per Invoice line items")]
        public string hsn_sc { get; set; }


        [Required]
        [Display(Name = "Description of goods sold")]
        [MaxLength(30)]
        public string desc { get; set; }


        [Required]
        [Display(Name = "UQC (Unit of Measure) of goods sold")]
        [MaxLength(30)]
        public string uqc { get; set; }


        [Required]
        [Display(Name = "Quantity of goods sold")]
        public double qty { get; set; }


        [Required]
        [Display(Name = "Total value")]
        public double val { get; set; }

        [Required]
        [Display(Name = "Taxable value of Goods or Service as per invoice")]
        public double txval { get; set; }

        [Required]
        [Display(Name = "IGST Amount as per invoice")]
        public double iamt { get; set; }

        [Required]
        [Display(Name = "CGST Amount as per invoice")]
        public double camt { get; set; }

        [Required]
        [Display(Name = "SGST Amount as per invoice")]
        public double samt { get; set; }

        [Required]
        [Display(Name = "Cess Amount as per invoice")]
        public double csamt { get; set; }

       
    }

    public class HSNSummaryOutward
    {
        [Display(Name = "TaxPayer Action")]
        public string flag { get; set; }                    // This not match in table schema


        [Required]
        [Display(Name = "HSN Summary of outward supplies")]
        public List<SummaryData> data { get; set; }

        [Required]
        [Display(Name = "Invoice Check sum value ")]
        [MaxLength(64)]
        [RegularExpression("^[a-zA-Z0-9]+$")]
        public string chksum { get; set; }
    }
}