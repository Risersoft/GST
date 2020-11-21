using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;

namespace Risersoft.API.GSTN.GSTR7
{
   
   
    public class GSTR7Summary
    {

        [Required]
        [Display(Name = "GSTIN of the TDS deductor")]
        [MaxLength(15)]
        [MinLength(15)]
        [RegularExpression("^[0-9]{2}[a-zA-Z]{5}[0-9]{4}[a-zA-Z]{1}[1-9A-Za-z]{1}[Zz1-9A-Ja-j]{1}[0-9a-zA-Z]{1}$")]
        public string gstin { get; set; }




        [Required]
        [Display(Name = "Return period")]
        [RegularExpression("^(([0-9]{2}((19|20)\\d\\d)))$")]
        public string fp { get; set; }
        [Required]
        [Display(Name = "TDS summary ")]
        public Summary tds { get; set; }

        [Required]
        [Display(Name = "TDSA summary ")]
        public Summary tdsa { get; set; }

        [Required]
        [Display(Name = "offset summary ")]
        public List<Offset> offset { get; set; }
       

        [Required]
        [Display(Name = "Tax payable  Summary")]
        public List<TaxPaidDetails> tax_pay { get; set; }

      

    }

    public class Summary
    {

      
        [Required]
        [Display(Name = "Total Record Count")]
        public decimal no_rec { get; set; }

        [Required]
        [Display(Name = "Total amount deducted")]
        public decimal ttl_amtDed { get; set; }

        [Required]
        [Display(Name = "Total IGST ")]
        public decimal ttl_igst { get; set; }


        [Required]
        [Display(Name = "Total CGST ")]
        public decimal ttl_cgst { get; set; }

        [Required]
        [Display(Name = "Total SGST ")]
        public decimal ttl_sgst { get; set; }   
    }

   
   
    public class TaxPaidDetails
    {


        [Display(Name = "Liability id")]
        [MaxLength(11)]
        public decimal liab_id { get; set; }


        [Required]
        [Display(Name = "transaction code")]
        public decimal trancd { get; set; }

        [Display(Name = "transaction date")]
        [RegularExpression("^((0[1-9]|[12][0-9]|3[01])[-](0[1-9]|1[012])[-]((19|20)\\d\\d))$")]
        public string trandate { get; set; }
        [Required]
        [Display(Name = "Igst tax payable")]
        public MinorHeads igst { get; set; }

        [Required]
        [Display(Name = "cgst tax payable")]
        public MinorHeads cgst { get; set; }

        [Required]
        [Display(Name = "sgst tax payable")]
        public MinorHeads sgst { get; set; }


        [Required]
        [Display(Name = "cess tax payable")]
        public MinorHeads cess { get; set; }
    }




    public class Offset
    {
        [Required]
        [Display(Name = "transaction code")]
        public decimal tran_cd { get; set; }




        [Required]
        [Display(Name = "transaction date")]
        [RegularExpression("^((0[1-9]|[12][0-9]|3[01])[-](0[1-9]|1[012])[-]((19|20)\\d\\d))$")]
        public string trandate { get; set; }





        [Required]
        [Display(Name = "Igst tax payable")]
        public OffMinorHeads igst { get; set; }



        [Required]
        [Display(Name = "cgst tax payable")]
        public OffMinorHeads cgst { get; set; }

        [Required]
        [Display(Name = "sgst tax payable")]
        public OffMinorHeads sgst { get; set; }

       
    }
    public class MinorHeads
    {
        [Required]
        [Display(Name = "tax")]
        public decimal tx { get; set; }

        [Required]
        [Display(Name = "interest")]
        public decimal intr { get; set; }


        [Required]
        [Display(Name = "pending")]
        public decimal pen { get; set; }

        [Required]
        [Display(Name = "fee")]
        public decimal fee { get; set; }

        [Display(Name = "others")]
        public decimal oth { get; set; }
        [Required]
        [Display(Name = "total")]
        public decimal tot { get; set; }

    }






    public class OffMinorHeads
    {
        [Required]
        [Display(Name = "tax")]
        public decimal tx { get; set; }

        [Required]
        [Display(Name = "interest")]
        public decimal intr { get; set; }

      
        [Display(Name = "fee")]
        public decimal fee { get; set; }


        [Required]
        [Display(Name = "total")]
        public decimal tot { get; set; }

    }
}