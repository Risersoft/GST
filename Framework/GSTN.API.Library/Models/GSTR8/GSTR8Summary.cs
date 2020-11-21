using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;


namespace Risersoft.API.GSTN.GSTR8
{
    public class CptySum
    {



        [Required]
        [Display(Name = "Total Record Count")]
        public double no_rec { get; set; }   

        [Required]
        [Display(Name = "Total amount deducted")]
        public double ttl_amtcol { get; set;}

        [Required]
        [Display(Name = "Total IGST ")]
        public string ttl_iamt { get; set; }

        [Required]
        [Display(Name = "Total CGST ")]
        public double ttl_camt { get; set; }

        [Required]
        [Display(Name = "Total SGST ")]
        public double ttl_samt { get; set; }


        [Required]
        [Display(Name = "Checksum value")]
        [MaxLength(64)]
        public string  chksum { get; set; }

        [Required]
        [Display(Name = "Gross Supplies Made")]
        public double gSuppMade { get; set; }

        [Required]
        [Display(Name = "Gross Supplies Returned")]
        public double gSuppRtn { get; set; }


    }

    public class SecSum
    {
        [Required]
        [Display(Name = "TCS total summary")]
        [MaxLength(5)]
        public CptySum tcs { get; set; }

        [Required]
        [Display(Name = "TCSA total summary")]
        public CptySum tcsa { get; set; }

       

    }

    public class GSTR8Summary
    {

        [Required]
        [Display(Name = "GSTIN of the taxpayer")]
        [MaxLength(15)]
        [MinLength(15)]
        [RegularExpression("^[a-zA-Z0-9]+$")]
        public string gstin { get; set; }

        [Required]
        [Display(Name = "Return Period")]
        [RegularExpression("^((0[1-9]|1[012])((19|20)\\d\\d))*$")]
        public string fp { get; set; }

        [Required]
        [Display(Name = "Invoice Check sum value")]
        [MaxLength(64)]
        public string chksum { get; set; }

        [Required]
        [Display(Name = "sec_sum")]
        public List<SecSum> sec_sum { get; set; }



        [Required]
        [Display(Name = "default intrest amount")]
        public double dflt_amt { get; set; }
        [Required]
        [Display(Name = "Part B Summary of tax paid")]
        public TaxPaid tax_paid { get; set; }

        [Required]
        [Display(Name = "Part A Summary of tax payable")]
        public TaxPaid tax_pay { get; set; }

    }

}