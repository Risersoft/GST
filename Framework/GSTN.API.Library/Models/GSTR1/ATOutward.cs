using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;

namespace Risersoft.API.GSTN.GSTR1
{


    public class ATitemdtl
    {


        [Required]
        [Display(Name = "Rate of Invoice")]
        public double rt { get; set; }

        [Required]
        [Display(Name = "Amount of Advance received")]
        public double ad_amt { get; set; }

      
        [Display(Name = "IGST Amount as per invoice")]
        public double iamt { get; set; }

       
        [Display(Name = "SGST Amount as per invoice")]
        public double samt { get; set; }

       
        [Display(Name = "CGST Amount as per invoice")]
        public double camt { get; set; }

        
        [Display(Name = "Cess Amount as per invoice")]
        public double csamt { get; set; }

    }
    public class AtOutward
    {

        [Required]
        [Display(Name = "Place of Supply")]
        [MaxLength(2)]
        public string pos { get; set; }

        [Required]
        [Display(Name = "Supply Type of invoice")]
        [RegularExpression("^[INTER / INTRA]")]
        public string sply_ty { get; set; }                 // This not match table schema ex:enum": [ "C","R","D"]

       
        [Required]
        [Display(Name = "Item Details")]
        public List<ATitemdtl> itms { get; set; }


        [Required]
        [Display(Name = "Checksum Value")]
        [MaxLength(64)]
        public string chksum { get; set; }

        [Display(Name = "Differential percentage")]
        public double? diff_percent { get; set; }

        [Display(Name = "TaxPayer Action")]
        public string flag { get; set; }                    // This not match in table schema


    }


}