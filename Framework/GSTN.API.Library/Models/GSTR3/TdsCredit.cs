using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Risersoft.API.GSTN.GSTR3
{
    public class TdsCredit
    {
        

        [Required]
        [Display(Name = "IGST_Tax Amount of TDS")]
        public double itx { get; set; }

        [Required]
        [Display(Name = "CGST_Tax Amount of TDS")]
        public double ctx { get; set; }

        [Required]
        [Display(Name = "SGST_Tax Amount of TDS")]
        public double stx { get; set; }

     
    }
}