using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;

namespace Risersoft.API.GSTN.GSTR2
{
   

    public class SummaryData
    {
        [Required]
        [Display(Name = "Item number")]
        public int num { get; set; }


        [Required]
        [Display(Name = "HSN of Goods or Services as per Invoice line items")]
        public string hsn_sc { get; set; }


        
        [Display(Name = "Goods Description")]
        [MaxLength(30)]
        public string desc { get; set; }


        [Required]
        [Display(Name = "UQC (Unit of Measure) of goods purchased")]
        [MaxLength(30)]
        public string uqc { get; set; }


        [Required]
        [Display(Name = "Quantity of goods purchased")]
        public double qty { get; set; }


        [Required]
        [Display(Name = "Total value")]
        public double val { get; set; }

        [Required]
        [Display(Name = "Taxable Value/Value of inward supply")]
        public double txval { get; set; }

        
        [Display(Name = "IGST Amount as per invoice")]
        public double iamt { get; set; }

       
        [Display(Name = "CGST Amount as per invoice")]
        public double camt { get; set; }

        
        [Display(Name = "SGST Amount as per invoice")]
        public double samt { get; set; }

       
        [Display(Name = "Cess Amount as per invoice")]
        public double csamt { get; set; }

       
    }

    public class HSNSummaryInward
    {

        [Required]
        [Display(Name = "HSN Item Details")]
        public List<SummaryData> det { get; set; }

        [Required]
        [Display(Name = "Invoice Check sum value ")]
        [MaxLength(64)]
        [RegularExpression("^[a-zA-Z0-9]+$")]
        public string chksum { get; set; }
    }
}