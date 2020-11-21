using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Risersoft.API.GSTN.GSTR6
{
    public class Claim
    {


        [Required]
        [Display(Name = "fee")]
        public double fee { get; set; }

        [Display(Name = "other")]
        public double others { get; set; }


    }
   
    public class RefundClaim

    {
             
       
        [Display(Name = "bank account  number")]
        [MaxLength(30)]
        public TaxPaidcredit bankaccno { get; set; }

        [Display(Name= "debit number")]
        [MaxLength(30)]
        public TaxPaidcredit debiteno { get; set; }

        [Display(Name = "cgst refund claim")]
        public List<Claim> cgstrefclm { get; set; }
        [Display(Name = "cgst refund claim")]
        public List<Claim> sgstrefclm { get; set; }



    }
}
