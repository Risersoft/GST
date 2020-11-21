using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using GSTN.API.Library.Models;

namespace Risersoft.API.GSTN.GSTR8
{




    public class SaveGSTR8:GSTRBase {

        
        [Display(Name = "TCS details")]
        public TCSdet tcs { get; set; }



        
        [Display(Name = "Amended TCS details")]
        public TCSAdet tcsa { get; set; }


    }




    public class TCSdet
     {
        [Required]
        [Display(Name = "GSTIN of Supplier")]
        [MaxLength(15)]
        [MinLength(15)]
        [RegularExpression("^[0-9]{2}[a-zA-Z]{5}[0-9]{4}[a-zA-Z]{1}[1-9A-Za-z]{1}[Zz1-9A-Ja-j]{1}[0-9a-zA-Z]{1}$")]
        public string stin { get; set; }



        [Required]
        [Display(Name = "Gross value of supplies made")]
        public double supR { get; set; }

        [Required]
        [Display(Name = "Gross value of supplies returned")]
        public double retsupR { get; set; }

        [Required]
        [Display(Name = "Gross value of supplies made to Unregistered receiver")]
    
        public double supU { get; set; }

        [Required]
        [Display(Name = "Gross value of supplies returned by Unregistered receiver")]
        public double retsupU { get; set; }

        [Required]
        [Display(Name = "Net amount liable for TCS")]
        public double amt { get; set; }


        [Required]
        [Display(Name = "Central tax ")]
        public double camt { get; set; }

        [Required]
        [Display(Name = "State/UT Tax")]
        public double samt { get; set; }


        [Required]
        [Display(Name = "Integrated tax ")]
        public double iamt { get; set; }

        [Required]
        [Display(Name = "Flag to indicate to delete a record")]
        public string flag { get; set; }


    }





    public class TCSAdet: TCSdet
    {




        [Required]
        [Display(Name = "Original GSTIN of Supplier")]
        [MaxLength(15)]
        [MinLength(15)]
        [RegularExpression("^[0-9]{2}[a-zA-Z]{5}[0-9]{4}[a-zA-Z]{1}[1-9A-Za-z]{1}[Zz1-9A-Ja-j]{1}[0-9a-zA-Z]{1}$")]
        public string ostin { get; set; }
       



        [Required]
        [Display(Name = "Original return Period of TCS")]
        [RegularExpression("^(([0-9]{2}((19|20)\\d\\d)))$")]
        public string ofp { get; set; }

    }

}