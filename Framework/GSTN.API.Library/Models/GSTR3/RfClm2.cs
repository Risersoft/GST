using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Risersoft.API.GSTN.GSTR3
{
    public class RfClm2
    {

      
        [Display(Name = "Minor Head details")]
        public ICSC igrfclm { get; set; }

       
        [Display(Name = "Minor Head details")]
        public ICSC cgrfclm { get; set; }

       
        [Display(Name = "Minor Head details")]
        public ICSC sgrfclm { get; set; }

       
        [Display(Name = "Minor Head details")]
        public ICSC csrfclm { get; set; }

       
        [Display(Name = "Bank account Number")]
        [MaxLength(16)]
        public double bankacc { get; set; }

       
    }
}