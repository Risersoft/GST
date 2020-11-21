using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Risersoft.API.GSTN.Ledger
{
    public class LibDetails
    {
        [Display(Name = "GSTIN")]
        [MaxLength(15)]
        [MinLength(15)]
        [RegularExpression("^[a-zA-Z0-9]+$")]
        [Required]
        public string gstin { get; set; }

        [Display(Name = "Return Period")]
        [RegularExpression("^((0[1-9]|1[012])((19|20|30)\\d\\d))*$")]
        [Required]
        public string ret_period { get; set; }

        [Display(Name = "Transactions")]
        [Required]
        public List<Transaction> tr { get; set; }

        [Display(Name = "Closing Balance")]
        [Required]
        public ClosingBalance cl_bal { get; set; }

        public class Transaction
        {
            [Display(Name = "Date")]
            [Required]
            public string dt { get; set; }

            [Display(Name = "Description")]
            [Required]
            public string desc { get; set; }

            [Display(Name = "Reference Number")]
            [Required]
            public string ref_no { get; set; }

            [Display(Name = "Total Transaction Amount")]
            [Required]
            public decimal tot_tr_amt { get; set; }

            [Display(Name = "Total Running  Balance")]
            [Required]
            public decimal tot_rng_bal { get; set; }

            [Display(Name = "Discharge type ITC or Cash")]
            [Required]
            public string dschrg_typ { get; set; }

            [Display(Name = "Transaction Type")]
            [Required]
            public string tr_typ { get; set; }

            [Display(Name = "IGST")]
            [Required]
            public GstEntry igst { get; set; }

            [Display(Name = "CGST")]
            [Required]
            public GstEntry cgst { get; set; }

            [Display(Name = "SGST")]
            [Required]
            public GstEntry sgst { get; set; }

            [Display(Name = "CESS")]
            [Required]
            public GstEntry cess { get; set; }

            [Display(Name = "IGST Balance")]
            [Required]
            public GstEntry igstbal { get; set; }

            [Display(Name = "CGST Balance")]
            [Required]
            public GstEntry cgstbal { get; set; }

            [Display(Name = "SGST Balance")]
            [Required]
            public GstEntry sgstbal { get; set; }

            [Display(Name = "CESS Balance")]
            [Required]
            public GstEntry cessbal { get; set; }
        }

        public class ClosingBalance
        {
            [Display(Name = "Description")]
            [Required]
            public string desc { get; set; }

            [Display(Name = "IGST Balance")]
            [Required]
            public GstEntry igstbal { get; set; }

            [Display(Name = "CGST Balance")]
            [Required]
            public GstEntry cgstbal { get; set; }

            [Display(Name = "SGST Balance")]
            [Required]
            public GstEntry sgstbal { get; set; }

            [Display(Name = "CESS Balance")]
            [Required]
            public GstEntry cessbal { get; set; }

            [Display(Name = "Total Running  Balance")]
            [Required]
            public decimal tot_rng_bal { get; set; }
        }

        public class GstEntry
        {
            [Display(Name = "Tax")]
            [Required]
            public decimal tx { get; set; }

            [Display(Name = "Intrest")]
            [Required]
            public decimal intr { get; set; }

            [Display(Name = "Penality")]
            [Required]
            public decimal pen { get; set; }

            [Display(Name = "Fee")]
            [Required]
            public decimal fee { get; set; }

            [Display(Name = "Other")]
            [Required]
            public decimal oth { get; set; }

            [Display(Name = "Total")]
            [Required]
            public decimal tot { get; set; }
        }
    }
}
