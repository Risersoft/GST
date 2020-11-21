using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Risersoft.API.GSTN.GSTR3
{


    public class OutReverse
    {
        [Required]
        [Display(Name = "Outward taxable  supplies  (other than zero rated, nil rated and exempted)")]
        public List<IntrSupConReg> osup_det { get; set; }

        [Required]
        [Display(Name = "Outward taxable  supplies  (zero rated )")]
        public List<IntraSupConReg> osup_zero { get; set; }

       

        [Required]
        [Display(Name = "Other outward supplies (Nil rated, exempted)")]
        public Outamend osup_nil_exmp { get; set; }
        [Required]
        [Display(Name = "Inward supplies (liable to reverse charge)")]
        public List<IntrSupConReg> isup_rev { get; set; }


        [Required]
        [Display(Name = "Non-GST outward supplies ")]
        public List<IntrSupConReg> osup_nongst { get; set; }

    }
}