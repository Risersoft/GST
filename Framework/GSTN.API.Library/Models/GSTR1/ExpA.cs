﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Risersoft.API.GSTN.GSTR1
{
    public class ExpAInv : ExpInv
    {
        [Required]
        [Display(Name = "Original invoice number")]
        [MaxLength(50)]
        [RegularExpression("^[a-zA-Z0-9]+$")]
        public string oinum { get; set; }
        [Required]
        [Display(Name = "Original invoice date")]
        [RegularExpression("^((0[1-9]|[12][0-9]|3[01])[-](0[1-9]|1[012])[-]((19|20)\\d\\d))*$")]
        public string oidt { get; set; }


        [Required]
        [Display(Name = "Counterparty GSTIN")]
        [MaxLength(15)]
        public string ctin { get; set; }
    }
    public class ExpA
    {
        [Display(Name = "Supplies exported types i.e WithPay,WithoutPay")]
        [Required]
        [MaxLength(5)]
        public string exp_typ { get; set; }

        [Required]
        [Display(Name = "Invoice Details")]
        public List<ExpAInv> inv { get; set; }
        public string error_cd { get; set; }
        public string error_msg { get; set; }
    }
}
