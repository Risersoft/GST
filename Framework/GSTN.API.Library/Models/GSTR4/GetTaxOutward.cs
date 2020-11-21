using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;

namespace Risersoft.API.GSTN.GSTR4
{






    public class GetTaxOutward
    {

        [Required]
        [Display(Name = "Rate of Invoice")]
        public double rt { get; set; }



        [Required]
        [Display(Name = "Central Tax")]
        public double camt { get; set; }
        [Required]
        [Display(Name = "Turnover of current tax period")]
        public double trnovr { get; set; }

        
        [Required]
        [Display(Name = "Invoice Check sum value ")]
        [MaxLength(64)]
        public string chksum { get; set; }
        [Required]
        [Display(Name = "State/UT Tax")]
        public double samt { get; set; }
        
    }

   
}