using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Risersoft.API.GSTN.GSTR3B
{
    public class Isupdetails
    {



        [Display(Name = "type")]
        public string ty { get; set; }
        [Display(Name = "Inter - State supplies")]
        public double inter { get; set; }
        [Display(Name = "Intra - State supplies")]
        public double intra { get; set; }


    }
    public class Inwardsup
    {



        [Display(Name = "Inward Supply Details")]
        public List<Isupdetails> isup_details { get; set; }


    }
}
