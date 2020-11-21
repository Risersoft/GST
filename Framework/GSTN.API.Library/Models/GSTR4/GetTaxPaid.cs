using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;

namespace Risersoft.API.GSTN.GSTR4
{






    public class Hclsc
    {
       
        [Display(Name = "tax")]
        public double tx { get; set; }




        [Display(Name = "interest")]
        public double intr { get; set; }

        [Display(Name = "penalty")]
        public double pen { get; set; }

       
        [Display(Name = "fee")]
        public double fee { get; set; }
       
        [Display(Name = "Total")]
        public double tot { get; set; }
       
        [Display(Name = "other")]
        public double oth { get; set; }
        
    }
    public class TaxPaid
    {
        [Required]
        [Display(Name = "Debit Detail")]
        [MaxLength(15)]
        public List<Debit_dtl> debit_dtl { get; set; }
    }
    public class Debit_dtl
    {
        [Display(Name = "Igst tax")]
        public Hclsc igst { get; set; }


        [Display(Name = "cgst tax ")]
        public Hclsc cgst { get; set; }



        [Display(Name = "sgst tax ")]
        public Hclsc sgst { get; set; }




        [Display(Name = "cess tax ")]
        public Hclsc cess { get; set; }

        [Display(Name = "Description in transaction")]
        [MaxLength(100)]
        public string debit_id { get; set; }

        [Display(Name = "Liability id")]
        public double liab_id { get; set; }




        [Display(Name = "transaction code")]

        public double trancd { get; set; }

        
        [Display(Name = "transaction date")]
        [RegularExpression("^((0[1-9]|[12][0-9]|3[01])[-](0[1-9]|1[012])[-]((19|20)\\d\\d))$")]
        public string trandate { get; set; }
    }





    public class TaxPay
    {
        public TaxPay non_rev { get; set; }
        public TaxPay rev { get; set; }
    }




    public class TaxPayList
    {

       
        [Display(Name = "Igst tax")]
        public Hclsc igst { get; set; }

       
        [Display(Name = "cgst tax ")]
        public Hclsc cgst { get; set; }


       
        [Display(Name = "sgst tax ")]
        public Hclsc sgst { get; set; }



        
        [Display(Name = "cess tax ")]
        public Hclsc cess { get; set; }

        [Display(Name = "Description in transaction")]
        [MaxLength(100)]
        public string tran_desc { get; set; }

        [Display(Name = "Liability id")]
        public double liab_id { get; set; }

        [Display(Name = "transaction code")]
        public double tran_cd { get; set; }
    }





    public class GetTaxPaid
    {
        
        public List<TaxPay> tax_pay{ get; set;}

        
        public List<TaxPaid> tax_paid { get; set; }
    }
}