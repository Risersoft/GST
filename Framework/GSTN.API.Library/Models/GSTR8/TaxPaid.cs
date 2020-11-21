using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;

namespace Risersoft.API.GSTN.GSTR8
{






    public class Hcls
    {
       
        [Display(Name = "tax")]
        public double tx { get; set; }




        [Display(Name = "interest")]
        public double intr { get; set; }


       
        [Display(Name = "penalty")]
        public double pen { get; set; }
      
        [Display(Name = "fee")]
        public double fee { get; set; }
       
        [Display(Name = "other")]
        public double oth { get; set; }




       
        [Display(Name = "total")]
        public double tot { get; set; }

    }
   
    public class TaxPaid
    {

       
        [Display(Name = "Total IGST ")]
        public Hcls iamt { get; set; }


        [Display(Name = "Total CGST ")]
        public Hcls camt { get; set; }

       
        [Display(Name = "Total SGST ")]
        public Hcls samt { get; set; }

        
        [Display(Name = "Description in transaction")]
        public string debit_id { get; set; }



        [Display(Name = "Liability id")]
        public double liab_id { get; set; }

       
        [Display(Name = "transaction code")]
        public double trancd { get; set; }

        [Display(Name = "transaction date")]
        public string trandate { get; set; }



    }

}