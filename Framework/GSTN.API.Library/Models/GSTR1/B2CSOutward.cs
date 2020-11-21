using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;

namespace Risersoft.API.GSTN.GSTR1
{
    public class B2csOutward
    {
        [Display(Name = "TaxPayer Action")]
        public string flag { get; set; }                    // This not match in table schema


        [Required]
        [Display(Name = "Invoice Check sum value")]
        [MaxLength(64)]
        public string chksum { get; set; }

       
        [Display(Name = "Actual Place of Supply")]
        [MaxLength(2)]
        [MinLength(2)]
        public string pos { get; set; }
              
        [Required]
        [Display(Name = "Supply Type of invoice")]
        [RegularExpression("^[INTER/INTRA]")]
        public string sply_ty { get; set; }

        [Required]
        [Display(Name = "Taxable value of Goods or Service as per invoice")]
        public double txval { get; set; }

        [Required]
        [Display(Name = "Rate as per invoice")]
        public double rt { get; set; }
        
        [Display(Name = "IGST Amount as per invoice")]
        public double iamt { get; set; }
        
       


        [Display(Name = "CGST Amount as per invoice")]
        public double camt { get; set; }
        
      
        [Display(Name = "SGST Amount as per invoice")]
        public double samt { get; set; }


        
        [Display(Name = "Cess Amount as per invoice")]
        public double csamt { get; set; }


        [Display(Name = "Differential percentage")]
        public double? diff_percent { get; set; }
       
        [Display(Name = "EcomOperator Gstin")]
        [MaxLength(15)]
        [MinLength(15)]
        [RegularExpression("^[a-zA-Z0-9._\\s]+$")]
        public string etin { get; set; }

        [Required]
        [Display(Name = "Type of Ecom/ non-Ecom")]
        [RegularExpression("^[E/OE]")]
        public string typ { get; set; }

    }

}