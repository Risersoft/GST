using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Risersoft.API.GSTN.GSTR3
{
    public class Tod
    {

        

        [Required]
        [Display(Name = "Deemed Exports")]
        public double exp_to { get; set; }

        [Required]
        [Display(Name = "Taxable  [other than zero rated]")]
        public double txble_to { get; set; }

        [Required]
        [Display(Name = "Zero rated supply on  payment of Tax ")]
        public double zrsp_to { get; set; }

        [Required]
        [Display(Name = "Zero rated supply without payment of Tax")]
        public double zrswp_to { get; set; }

        [Required]
        [Display(Name = "Nil rated")]
        public double nil_to { get; set; }

        [Required]
        [Display(Name = "Non GST Supply")]
        public double non_to { get; set; }




        [Required]
        [Display(Name = "Net Taxable Turnover")]
        public double net_to { get; set; }

        [Required]
        [Display(Name = "Exempted")]
        public double exmptd_to { get; set; }
    }
}