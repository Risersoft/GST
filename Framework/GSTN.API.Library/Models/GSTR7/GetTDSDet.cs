using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;

namespace Risersoft.API.GSTN.GSTR7
{
   
   
    public class GetTDSDet
    {


        [Display(Name = "List of TDS ")]
        public List<TDSDeta> tds { get; set; }

    }

    public class TDSDeta
    {

        
        [Required]
        [Display(Name = "revised gstin of the deductee ")]
        [MaxLength(15)]
        [MinLength(15)]
        [RegularExpression("^[0-9]{2}[a-zA-Z]{5}[0-9]{4}[a-zA-Z]{1}[1-9A-Za-z]{1}[Zz1-9A-Ja-j]{1}[0-9a-zA-Z]{1}$")]
        public string gstin_ded { get; set; }


        [Required]
        [Display(Name = "revised Amount deducted ")]
        public decimal amt_ded { get; set; }

        [Required]
        [Display(Name = "IGST TDS amount")]
        public decimal iamt { get; set; }



        [Required]
        [Display(Name = "CGST TDS amount")]
        public decimal camt { get; set; }

        [Required]
        [Display(Name = "SGST TDS amount")]
        public decimal samt { get; set; }

        [Required]
        [Display(Name = "Flag to indicate add or delete")]
        [RegularExpression("^[A/D]$")]
        public string flag { get; set; }
    }

   

}