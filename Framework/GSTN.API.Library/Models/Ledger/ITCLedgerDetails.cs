using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Risersoft.API.GSTN.Ledger
{


    public class ITCLedgerDetails
    {

        [Display(Name = "GSTIN of the taxpayer")]
        [MinLength(15)]
        [RegularExpression("^[a-zA-Z0-9]+$")]
        [Required]
        public string gstin { get; set; }

        [Display(Name = "from date")]
        [RegularExpression("^((0[1-9]|[12][0-9]|3[01])[-](0[1-9]|1[012])[-]((19|20)\\d\\d))*$")]
        [Required]
        public string fr_dt { get; set; }

        [Display(Name = "to date")]
        [RegularExpression("^((0[1-9]|[12][0-9]|3[01])[-](0[1-9]|1[012])[-]((19|20)\\d\\d))*$")]
        [Required]
        public string to_dt { get; set; }

        public ItcBal cl_bal { get; set; }
        public ItcBal op_bal { get; set; }
        public List<ItcTr> tr { get; set; }
    }
    public class ItcBal
    {
        public long? CessTaxBal { get; set; }
        public long? CgstTaxBal { get; set; }
        public string Desc { get; set; }
        public long? IgstTaxBal { get; set; }
        public long? SgstTaxBal { get; set; }
        public long? Tot_Rng_Bal { get; set; }
    }


    public class ItcTr
    {
        public long? CessTaxAmt { get; set; }
        public long? CessTaxBal { get; set; }
        public long? CgstTaxAmt { get; set; }
        public long? CgstTaxBal { get; set; }
        public string Desc { get; set; }
        public string Dt { get; set; }
        public long? IgstTaxAmt { get; set; }
        public long? IgstTaxBal { get; set; }
        public string Ref_No { get; set; }
        public string Ret_Period { get; set; }
        public long? SgstTaxAmt { get; set; }
        public long? SgstTaxBal { get; set; }
        public long? Tot_Rng_Bal { get; set; }
        public long? Tot_Tr_Amt { get; set; }
        public string Tr_Typ { get; set; }
    }

}
