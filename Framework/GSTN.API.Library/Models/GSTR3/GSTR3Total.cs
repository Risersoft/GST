using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;


namespace Risersoft.API.GSTN.GSTR3
{
    public class GSTR3Total
    {


        [Required]
        [Display(Name = "Supplier GSTIN")]
        [MaxLength(15)]
        [MinLength(15)]
        [RegularExpression("^[a-zA-Z0-9]+$")]
        public string gstin { get; set; }



        [Required]
        [Display(Name = "Return Period")]
        [RegularExpression("^((0[1-9]|1[012])((19|20)\\d\\d))*$")]
        public string ret_period { get; set; }


        [Required]
        [Display(Name = "Turn Over Details")]
        public Tod tod { get; set; }

        [Required]
        [Display(Name = "Outward Supplies")]
        public Out osup { get; set; }

        [Required]
        [Display(Name = "Inward Supplies")]
        public InS isup { get; set; }

        [Required]
        [Display(Name = "Total Tax Liability")]
        public Ttxl ttxl { get; set; }

        [Required]
        [Display(Name = "TCS Credit")]
        public TcsCredit tcs_cr { get; set; }

        [Required]
        [Display(Name = "TDS Credit")]
        public TdsCredit tds_cr { get; set; }
        [Required]
        [Display(Name = "ITC Credit")]
        public ItcCredit itc_cr { get; set; }

        [Required]
        [Display(Name = "Interest Liability")]
        public InterestLiability intr_liab { get; set; }


        [Required]
        [Display(Name = "Late Fee")]
        public Latefee lt_fee { get; set; }
        [Required]
        [Display(Name = "Interest, Late Fee and any other amount (other than tax) payable and paid")]
        public InterestLatefee intr { get; set; }


        [Required]
        [Display(Name = "Tax Payable and  Paid")]
        public TPaid tpm { get; set; }
        [Required]
        [Display(Name = "Debit entries in the ledger")]
        public Debitledger ldg_debit_entries { get; set; }




        [Required]
        [Display(Name = "Mismatch")]
        public Mismatch mis { get; set; }
        [Required]
        [Display(Name = "Refund Claim")]
        public RfClm rf_clm { get; set; }
    }
}