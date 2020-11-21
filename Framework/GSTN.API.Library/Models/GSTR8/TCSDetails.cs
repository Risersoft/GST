using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;

namespace Risersoft.API.GSTN.GSTR8
{
    
   
    public class TCSDetl
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
        [Display(Name = "Checksum value")]
        [MaxLength(64)]
        public string chksum { get; set; }



    }




   

    public class TCSDetails
    {
    [Display(Name = "GSTIN of the taxpayer")]
    [MaxLength(15)]
    [MinLength(15)]
    [RegularExpression("^[0-9]{2}[a-zA-Z]{4}[a-zA-Z0-9]{1}[0-9]{4}[a-zA-Z]{1}[1-9A-Za-z]{1}[C]{1}[0-9a-zA-Z]{1}$")]
    public string gstin { get; set; }



    [Display(Name = "Return Period")]

    [RegularExpression("^(([0-9]{2}((19|20)\\d\\d)))$")]
    public string fp { get; set; }


    [Display(Name = "Timestamp value")]

    [RegularExpression("^((0[1-9]|[12][0-9]|3[01])[-](0[1-9]|1[012])[-]((19|20)\\d\\d))*$")]
    public string from_time { get; set; }

    [Display(Name = "TCS details")]
    public List<TCSDetl> tcs { get; set; }




    [Display(Name = "Amended TCS details")]
    public List<TCSADetails> tcsa { get; set; }

}

}