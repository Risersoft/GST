using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Risersoft.API.GSTN.GSTR3B
{
    public class Ltfeedet
    {


        [Display(Name = "Central Tax")]
        public double camt { get; set; }

        [Display(Name = "State/UT Tax")]
        public double samt { get; set; }


    }
    public class Intrdet
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
    public class Intrltfee
    {



        [Display(Name = "Interest")]
        public Intrdet intr_details { get; set; }

        [Display(Name = "Interest")]
        public Ltfeedet ltfee_details { get; set; }

    }
}
