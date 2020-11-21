using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Risersoft.API.GSTN.GSTR3
{
    public class IntraSupCon
    {
       

        [Required]
        [Display(Name = "Taxable supplies (other than reverse charge and Zero Rated supply made with payment of Integrated Tax) ")]
        public List<Taxsupl> tax_sup { get; set; }

       
        [Required]
        [Display(Name = "Out of the Supplies mentioned at A, the value of supplies made though an e-commerce operator attracting TCS")]
        public List<Ecommsupl> ecomm_sup { get; set; }
       
    }

    
    }