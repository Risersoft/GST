using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Risersoft.API.GSTN.GSTR3
{
    public class Ttxl
    {


        [Required]
        [Display(Name = "List of total tax liability on outward supplies")]
        public List<Ttltx> ttltx_out { get; set; }

        [Required]
        [Display(Name = "List of total tax liability on inward supplies")]
        public List<Ttltx> ttltx_in { get; set; }

        [Required]
        [Display(Name = "List of total tax liability on ITC Reversal/Reclaim")]
        public List<Ttltx> ttltx_itcrv { get; set; }

        [Required]
        [Display(Name = "List of total tax liability on mismatch/rectification/other reasons")]
        public List<Ttltx> ttltx_oth { get; set; }
    }
}