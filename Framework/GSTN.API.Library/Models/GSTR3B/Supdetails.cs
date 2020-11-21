using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Risersoft.API.GSTN.GSTR3B
{
    public class Osupdet
    {


        [Display(Name = "Total Taxable value")]
        public double txval { get; set; }

        [Display(Name = "Integrated Tax")]
        public double iamt { get; set; }

        [Display(Name = "Central Tax")]
        public double camt { get; set; }

        [Display(Name = "State/UT Tax")]
        public double samt { get; set; }

        [Display(Name = "Cess")]
        public double csamt { get; set; }
    }


    public class Osupzero
    {


        [Display(Name = "Total Taxable value")]
        public double txval { get; set; }
        [Display(Name = "Integrated Tax")]
        public double iamt { get; set; }
        [Display(Name = "Cess")]
        public double csamt { get; set; }

    }
    public class Osupnilexmp
    {

        [Display(Name = "Total Taxable value")]
        public double txval { get; set; }
    }

    public class Supdetails
    {


        [Display(Name = "Outward taxable  supplies  (other than zero rated, nil rated and exempted)")]
        public Osupdet osup_det { get; set; }



        [Display(Name = "Outward taxable  supplies  (zero rated )")]
        public Osupzero osup_zero { get; set; }


        [Display(Name = "Other outward supplies (Nil rated, exempted)")]
        public Osupnilexmp osup_nil_exmp { get; set; }

        [Display(Name = "Inward supplies (liable to reverse charge)")]

        public Osupdet isup_rev { get; set; }


        [Display(Name = "Non-GST outward supplies")]
        [MaxLength(16)]
        public Osupnilexmp osup_nongst { get; set; }
    }
}
