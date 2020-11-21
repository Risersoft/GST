using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Risersoft.API.GSTN.GSTR6
{
    public class TaxPaidcredit
    {


        [Required]
        [Display(Name = "Input tax credit mismatch")]
        public string des { get; set; }

        [Display(Name = "tax amount for igst")]
        public double iamt { get; set; }


        [Display(Name = "tax amount for cgst")]
        public double camt { get; set; }

        [Display(Name = "tax amount for sgst")]
        public double samt { get; set; }

        [Required]
        [Display(Name = "tax amount for cess")]
        public double csamt { get; set; }

    

    }
   
    public class Mismatchdetails

    {
             
       
        [Display(Name = "Input tax credit mismatch")]
        public TaxPaidcredit inptaxcredit { get; set; }



        
        [Display(Name= "Input tax credit reclaimed ")]
        public TaxPaidcredit inptaxreclm { get; set; }

       
       
    }
}
