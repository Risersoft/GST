using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace Risersoft.API.GSTN.GSTR3B
{




    public class Supdet
    {

        [Display(Name = "Total Taxable value")]
        public double txval { get; set; }
        [Display(Name = "Amount of Integrated Tax")]
        public double iamt { get; set; }
        [Display(Name = "Place of Supply (State/UT)  ")]
        public string pos { get; set; }

    }

    public class Supinter
    {

        [Display(Name = "Supplies made to Unregistered Persons")]
        public List<Supdet> unreg_details { get; set; }
        [Display(Name = "Supplies made to Composition Taxable Persons")]
        public List<Supdet> comp_details { get; set; }
        [Display(Name = "Supplies made to UIN holders")]
        public List<Supdet> uin_details { get; set; }

    }
}
