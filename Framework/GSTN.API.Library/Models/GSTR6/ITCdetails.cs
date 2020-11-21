using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Risersoft.API.GSTN.GSTR6
{
    public class TaxPaid
    {


        [Required]
        [Display(Name = "Amount of eligible ITC")]
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
   
    public class ITCdetails

    {
             
       
        [Display(Name = "Total  ITC available for distribution")]
        public TaxPaid TotalItc { get; set; }



        
        [Display(Name= "Amount of ineligible ITC")]
        public TaxPaid InelgItc { get; set; }

       
        [Display(Name = "Amount of eligible ITC")]
        public TaxPaid ElgItc { get; set; }
    }
}
