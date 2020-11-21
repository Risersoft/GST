using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;

namespace Risersoft.API.GSTN.GSTR4
{
    
   
    public class SetOffLiability
    {

        [Required]
        [Display(Name = "GSTIN of the taxpayer")]
        [MaxLength(15)]
        [MinLength(15)]
        [RegularExpression("^[a-zA-Z0-9._\\s]+$")]
        public string gstin { get; set; }

        [Required]
        [Display(Name = "Return Period")]
        [RegularExpression("^((0[1-9]|1[012])[-]((19|20)\\d\\d))$")]
        public string ret_period { get; set; }

        [Required]
        [Display(Name = "Debit Detail")]
        [MaxLength(15)]
        public List<Debit_detl> op_liab { get; set; }

    }

    public class Debit_detl
    {
        [Display(Name = "Igst tax")]
        public Hclsc igst { get; set; }

        [Display(Name = "cgst tax ")]
        public Hclsc cgst { get; set; }


        [Display(Name = "sgst tax ")]
        public Hclsc sgst { get; set; }


        [Display(Name = "cess tax ")]
        public Hclsc cess { get; set; }


        [Display(Name = "Description in transaction")]
        [MaxLength(100)]
        public double tran_desc { get; set; }


        [Display(Name = "transaction code")]
        public double tran_cd { get; set; }

    }

}