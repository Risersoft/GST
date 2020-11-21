using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Risersoft.API.GSTN.GSTR3B
{
    public class Itcdet
    {



        [Display(Name = "Type")]
        public string ty { get; set; } 

        [Display(Name = "Integrated Tax")]
        public double iamt { get; set; }

        [Display(Name = "Central Tax")]
        public double camt { get; set; }

        [Display(Name = "State/UT Tax")]
        public double samt { get; set; }

        [Display(Name = "Cess")]
        public double csamt { get; set; }

    }
    public class Itcdetnet
    {




        [Display(Name = "Integrated Tax")]
        public double iamt { get; set; }

        [Display(Name = "Central Tax")]
        public double camt { get; set; }

        [Display(Name = "State/UT Tax")]
        public double samt { get; set; }

        [Display(Name = "Cess")]
        public double csamt { get; set; }
    }
    public class Itcdetnelg
    {



        [Required]
        [Display(Name = "Type")]
        public string ty { get; set; }  

        [Display(Name = "Integrated Tax")]
        public double iamt { get; set; }

        [Display(Name = "Central Tax")]
        public double camt { get; set; }

        [Display(Name = "State/UT Tax")]
        public double samt { get; set; }

        [Display(Name = "Cess")]
        public double csamt { get; set; }
    }

    public class Itcelg
    {
        
        [Display(Name = "ITC Available (whether in full or part) ")]
        public List<Itcdet> itc_avl { get; set; }
        [Display(Name = "ITC Reversed")]
        public List<Itcdet> itc_rev { get; set; }
        [Display(Name = "Net ITC Available")]
        public Itcdetnet itc_net { get; set; }
        [Display(Name = "Ineligible ITC")]
        public List<Itcdetnelg> itc_inelg { get; set; }

    }
}
