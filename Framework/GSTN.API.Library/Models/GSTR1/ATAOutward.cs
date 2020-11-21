using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;

namespace Risersoft.API.GSTN.GSTR1
{

    public class item_dt

    {

        [Required]

        [Display(Name = "Rate of Invoice")]
        public double rt { get; set; }

        [Required]
        [Display(Name = "Advance received")]
        public double ad_amt { get; set; }


       
        [Display(Name = "CGST Rate as per invoice")]
        public double camt { get; set; }
       
        [Display(Name = "SGST Rate as per invoice")]
        public double samt { get; set; }


       
        [Display(Name = "Cess Rate as per invoice")]
        public double csamt { get; set; }
    }

    public class AtAOutward
    {
        [Required]
        [Display(Name = "Invoice chksum value")]
        [MaxLength(64)]
        public string chksum { get; set; }

        [Required]
        [Display(Name = "Original Month")]
       
        public string omon { get; set; }



        [Required]
        [Display(Name = "Original Place of Supply")]
        [MaxLength(2)]
        public string pos { get; set; }




        [Required]
        [Display(Name = "Original Supply Type")]
        [MaxLength(5)]
        public string sply_ty { get; set; }



        [Required]
        [Display(Name = "Item Details")]

        List<item_dt> items { get; set; }

        [Display(Name = "Differential percentage")]
        public double? diff_percent { get; set; }

        [Display(Name = "TaxPayer Action")]
        public string flag { get; set; }                    // This not match in table schema


    }


}