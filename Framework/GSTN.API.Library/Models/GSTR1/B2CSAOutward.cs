using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Risersoft.API.GSTN.GSTR1
{
    public class B2CSAOutward : B2csOutward

    {
        
      
        [Display(Name = "Original Month")]
      
        public string omon { get; set; }





        
        [Display(Name = "Original Place of Supply")]
        public string opos { get; set; }

      
    }
}
