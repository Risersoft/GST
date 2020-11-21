using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Risersoft.API.GSTN.Ledger
{
    public class RetLibBalance
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

        [Display(Name = "GSTR Type")]
        [Required]
        public string ret_type { get; set; }

        [Display(Name = "Open Liabilities details")]
        [Required]
        public List<OpenLiabilityDetails> op_liab { get; set; }

        public class OpenLiabilityDetails
        {
            [Display(Name = "Liability ID")]
            [Required]
            public int liab_id { get; set; }

            [Display(Name = "Description")]
            [Required]
            public string tran_desc { get; set; }

            [Display(Name = "Transaction code")]
            [Required]
            public int tran_cd { get; set; }

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
}
