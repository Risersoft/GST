using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace Risersoft.API.GSTN.GSTR3B
{

    public class Offsetliability
    {

        
 
        [Display(Name = "List of Paid Through Cash")]
        public List<Pdcash> pdcash { get; set; }

        [Display(Name = " Paid Through Credit")]
        public Pditc pditc { get; set; }


    }
}
