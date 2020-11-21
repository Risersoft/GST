using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;

namespace Risersoft.API.GSTN.GSTR4
{
    
   
    public class GetTDSDetails
    {

       
        [Display(Name = "TDS Status")]
        [RegularExpression("^[A/R]$")]
        public string flag { get; set; }
        
        [Display(Name = "gstin of the deductor ")]
        [MaxLength(15)]
        public string gstin_ded { get; set; }

       
        [Display(Name = "Amount deducted ")]
        public double amt_ded { get; set; }
       
        [Display(Name = "Central tax amount")]
        public double camt { get; set; }

       
        [Display(Name = "State tax amount ")]
        public double samt { get; set; }

       
    }

}