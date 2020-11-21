using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Risersoft.API.GSTN.GSTR3
{
    public class InwSup
    {
       
       

        [Required]
        [Display(Name = "Inward Supply Details")]
        public List<Isupdet> isup_details { get; set; }

       

       
    }
    public class Isupdet
    {
        [Required]
        [Display(Name = "type")]
        public string ty { get; set; }

        [Required]
        [Display(Name = "Inter - State supplies")]
        public double inter { get; set; }

        [Required]
        [Display(Name = "Intra - State supplies")]
        public double intra { get; set; }

    }
}
