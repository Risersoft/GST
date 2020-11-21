using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;

namespace Risersoft.API.GSTN.GSTR7
{
   
   
    public class GetTDSDetails
    {


        [Display(Name = "List of TDS ")]
        public List<TDSDet> tds { get; set; }

        [Display(Name = "List of TDSA")]
        public List<TDSADet> tdsa { get; set; }




        [Required]
        [Display(Name = "Request time")]
        [RegularExpression("^((0[1-9]|[12][0-9]|3[01])[-](0[1-9]|1[012])[-]((19|20)\\d\\d))*$")]
        public string req_time { get; set; }

    }



    public class TDSDet
    {

        [Required]
        [Display(Name = "GSTIN of the taxpayer")]
        [MaxLength(15)]
        [MinLength(15)]
        [RegularExpression("^[0-9]{2}[a-zA-Z]{5}[0-9]{4}[a-zA-Z]{1}[1-9A-Za-z]{1}[Zz1-9A-Ja-j]{1}[0-9a-zA-Z]{1}$")]
        public string gstin_ded { get; set; }

       

       [Required]
        [Display(Name = "Amount paid to deductee on which tax is deducted")]
        public decimal amt_ded { get; set; }


        [Required]
        [Display(Name = "IGST TDS amount")]
        public decimal iamt { get; set; }

        [Required]
        [Display(Name = "CGST TDS amount")]
        public decimal camt { get; set; }



        [Required]
        [Display(Name = "SGST TDS amount")]
        public decimal samt { get; set; }
        [Required]
        [Display(Name = "CheckSum")]
        [MaxLength(64)]
        public decimal chksum { get; set; }


    }

    public class TDSADet: TDSDet
    {

        [Required]
        [Display(Name = "Original gstin of the deductee ")]
        [MaxLength(15)]
        [MinLength(15)]
        [RegularExpression("^[0-9]{2}[a-zA-Z]{5}[0-9]{4}[a-zA-Z]{1}[1-9A-Za-z]{1}[Zz1-9A-Ja-j]{1}[0-9a-zA-Z]{1}$")]
        public string ogstin_ded { get; set; }




        [Required]
        [Display(Name = "original month")]
        public decimal omonth { get; set; }

        [Required]
        [Display(Name = "Original amount deducted")]
        public decimal oamt_ded { get; set; }
        [Required]
        [Display(Name = "Source of amendment")]
        public string source { get; set; }

        [Required]
        [Display(Name = "Action Taken")]
        public string act_tkn { get; set; }


    }

}