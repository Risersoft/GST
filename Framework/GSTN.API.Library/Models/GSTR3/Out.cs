using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Risersoft.API.GSTN.GSTR3
{


    public class Out
    {
        [Required]
        [Display(Name = "Inter-state supplies to Registered taxpayers")]
        public List<IntrSupConReg> inter { get; set; }

        [Required]
        [Display(Name = "Intra-State Supplies to Registered taxpayers")]
        public List<IntraSupConReg> intra { get; set; }

       

        [Required]
        [Display(Name = "Tax Amendment -Outward supplies")]
        public Outamend out_amend { get; set; }
       
    }
}