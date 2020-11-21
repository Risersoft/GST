using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Risersoft.API.GSTN.GSTR3
{


    public class GstrOut

    {
        [Required]
        [Display(Name = "Outward taxable  supplies  (other than zero rated, nil rated and exempted)")]
        public Osupdet osup_det { get; set; }

        [Required]
        [Display(Name = "Outward taxable  supplies  (zero rated )")]
        public Ozerodet osup_zero { get; set; }

        [Required]
        [Display(Name = "Other outward supplies (Nil rated, exempted)")]
        public Onil osup_nil_exmp { get; set; }

        [Required]
        [Display(Name = "Inward supplies (liable to reverse charge)")]
        public Osupdet isup_rev { get; set; }


        [Required]
        [Display(Name = "Non-GST outward supplies")]
        public Onil osup_nongst { get; set; }
    }


    public class Osupdet
    {

        [Required]
        [Display(Name = "Total Taxable value")]
        public double txval { get; set; }

        [Required]
        [Display(Name = "Central Tax")]
        public double camt { get; set; }


        [Required]
        [Display(Name = "State/UT Tax")]
        public double samt { get; set; }

        [Required]
        [Display(Name = "Cess")]
        public double csamt { get; set; }

        [Required]
        [Display(Name = "Integrated Tax")]
        public double iamt { get; set; }
    }

    public class Ozerodet
    {
        [Required]
        [Display(Name = "Central Tax")]
        public double camt { get; set; }


        [Required]
        [Display(Name = "State/UT Tax")]
        public double samt { get; set; }

        [Required]
        [Display(Name = "Cess")]
        public double csamt { get; set; }

        [Required]
        [Display(Name = "Integrated Tax")]
        public double iamt { get; set; }
    }




    public class Onil
    {
        [Required]
        [Display(Name = "Total Taxable value")]
        public double txval { get; set; }

    }


}