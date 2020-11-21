using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Risersoft.API.GSTN.GSTR3
{
    public class Latefee
    {
        

        [Required]
        [Display(Name = "State / Union Territory  Amount")]
        public double samt { get; set; }
        [Required]
        [Display(Name = "Central Amount")]
        public double camt { get; set; }

    }
}