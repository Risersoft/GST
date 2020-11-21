using System.ComponentModel.DataAnnotations;

namespace Risersoft.API.GSTN.Ledger
{
    public class LedgerCashBalanceITC
    {
        [Display(Name = "GSTIN")]
        [MaxLength(15)]
        [MinLength(15)]
        [RegularExpression("^[a-zA-Z0-9]+$")]
        [Required]
        public string gstin { get; set; }

        public LedgerCashBalanceITC_CashBalance cash_bal { get; set; }

        public LedgerCashBalanceITC_ITCBalance itc_bal { get; set; }

        public class LedgerCashBalanceITC_CashBalance
        {
            [Display(Name = "IGST Total Balance")]
            [Required]
            public decimal igst_tot_bal { get; set; }

            [Display(Name = "IGST")]
            [Required]
            public GstEntry igst { get; set; }

            [Display(Name = "CGST Total Balance")]
            [Required]
            public decimal cgst_tot_bal { get; set; }

            [Display(Name = "CGST")]
            [Required]
            public GstEntry cgst { get; set; }

            [Display(Name = "SGST Total Balance")]
            [Required]
            public decimal sgst_tot_bal { get; set; }

            [Display(Name = "SGST")]
            [Required]
            public GstEntry sgst { get; set; }

            [Display(Name = "CESS Total Balance")]
            [Required]
            public decimal cess_tot_bal { get; set; }

            [Display(Name = "CESS")]
            [Required]
            public GstEntry cess { get; set; }

            public class GstEntry
            {
                [Display(Name = "Tax")]
                [Required]
                public int tx { get; set; }

                [Display(Name = "Intrest")]
                [Required]
                public int intr { get; set; }

                [Display(Name = "Penality")]
                [Required]
                public int pen { get; set; }

                [Display(Name = "Fee")]
                [Required]
                public int fee { get; set; }

                [Display(Name = "Other")]
                [Required]
                public int oth { get; set; }
            }
        }

        public class LedgerCashBalanceITC_ITCBalance
        {
            [Display(Name = "IGST Balance")]
            [Required]
            public decimal igst_bal { get; set; }

            [Display(Name = "CGST Balance")]
            [Required]
            public decimal cgst_bal { get; set; }

            [Display(Name = "SGST Balance")]
            [Required]
            public decimal sgst_bal { get; set; }

            [Display(Name = "CESS Balance")]
            [Required]
            public decimal cess_bal { get; set; }
        }
    }
}
