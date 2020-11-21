using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Risersoft.API.GSTN.GSTR3
{
    public class Ttltx
    {
        

        [Required]
        [Display(Name = "Total Taxable Value")]
        public double txval { get; set; }

        [Required]
        [Display(Name = "Tax Rate")]
        public double rt { get; set; }

        [Required]
        [Display(Name = "Integrated Tax Amount")]
        public double itx { get; set; }
        [Required]
        [Display(Name = "Central Tax Amount")]
        public double ctx { get; set; }

        [Required]
        [Display(Name = "State / Union Territory Tax Amount")]
        public double stx { get; set; }

        [Required]
        [Display(Name = "Cess Amount")]
        public double cstx { get; set; }


    }
}