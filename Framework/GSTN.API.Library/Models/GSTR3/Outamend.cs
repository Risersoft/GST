using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Risersoft.API.GSTN.GSTR3
{
    public class Outamend
    {
        


        [Required]
        [Display(Name = "Inter-state supplies to Registered taxpayers")]
        public IntrSupCon inter { get; set; }

        [Required]
        [Display(Name = "Intra-State Supplies to Registered taxpayers")]
        public IntraSupCon intra { get; set; }
    }
}