using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Risersoft.API.GSTN.Ledger
{

    public class SummaryTrDtl : TrDtl
    {

        [Display(Name = "transaction total")]
        [Required]
        public int tr_tot { get; set; }

    }

    public class SummaryTr
    {

        [Display(Name = "Transaction name")]
        [RegularExpression("^[a-zA-Z0-9\\s]+$")]
        [Required]
        public string tr_name { get; set; }

        [Display(Name = "Transaction details")]
        [Required]
        public List<SummaryTrDtl> tr_dtl { get; set; }
    }


    public class Cr
    {

        [Display(Name = "List of Transactions")]
        public List<Tr> tr { get; set; }

        [Display(Name = "total credit amount details  of igst,sgst,cgst,total")]
        public TotDbCr tot_cr { get; set; }
    }



    public class TotDbCr : TotPay
    {

        [Display(Name = "total")]
        [Required]
        public int tot { get; set; }
    }

    public class Db
    {

        [Display(Name = "List of Transactions")]
        [Required]
        public List<SummaryTr> tr { get; set; }

        [Display(Name = "total debit amount details  of igst,sgst,cgst,total")]
        [Required]
        public TotDbCr tot_db { get; set; }
    }



    public class BalanceEntry
    {

        [Display(Name = "description about opening balance")]
        [RegularExpression("^[a-zA-Z0-9\\s]+$")]
        [Required]
        public string desc { get; set; }

        [Display(Name = "Running balance")]
        [Required]
        public decimal tot_rng_bal { get; set; }

        [Display(Name = "IGST Balance")]
        [Required]
        public TaxEntry igstbal { get; set; }

        [Display(Name = "CGST Balance")]
        [Required]
        public TaxEntry cgstbal { get; set; }

        [Display(Name = "SGST Balance")]
        [Required]
        public TaxEntry sgstbal { get; set; }

        [Display(Name = "Cess Balance")]
        [Required]
        public TaxEntry cessbal { get; set; }



    }
    
    public class LedgerSummary
    {

        [Display(Name = "GSTIN of the Taxpayer")]
        [MaxLength(15)]
        [MinLength(15)]
        [RegularExpression("^[a-zA-Z0-9]+$")]
        [Required]
        public string gstin { get; set; }

        [Display(Name = "From Date")]
        [RegularExpression("^((0[1-9]|[12][0-9]|3[01])[-](0[1-9]|1[012])[-]((19|20|30)\\d\\d))*$")]
        [Required]
        public string fr_dt { get; set; }

        [Display(Name = "To date")]
        [RegularExpression("^((0[1-9]|[12][0-9]|3[01])[-](0[1-9]|1[012])[-]((19|20|30)\\d\\d))*$")]
        [Required]
        public string to_dt { get; set; }

        [Display(Name = "List of all Credits")]
        [Required]
        public Cr cr { get; set; }

        [Display(Name = "List of all Debits")]
        [Required]
        public Db db { get; set; }

        [Display(Name = "opening balance")]
        [Required]
        public BalanceEntry op_bal { get; set; }

        [Display(Name = "closing balance")]
        [Required]
        public BalanceEntry cl_bal { get; set; }
    }

}
