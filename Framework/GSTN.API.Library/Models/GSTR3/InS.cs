using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Risersoft.API.GSTN.GSTR3
{
    public class InS
    {
       
       

        [Required]
        [Display(Name = "Tax payable on Reverse charge - Inward Supplies")]
        public Txpyrc txpy_rc { get; set; }

        [Required]
        [Display(Name = "Tax amendment - Reverse charge")]
        public Txamendrc txamend_rc { get; set; }

       
    }

}
