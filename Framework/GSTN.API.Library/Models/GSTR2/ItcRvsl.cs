using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;

namespace Risersoft.API.GSTN.GSTR2
{
    public class ItcRvsl
    {

        [Required]
        [Display(Name = "Amount in terms of Rule 2(2) of ITC Rules")]
        public ItemRev rule2_2 { get; set; }

        [Required]
        [Display(Name = "Amount in terms of rule 7 (1) (m)of ITC Rules")]
        public ItemRev rule7_1_m { get; set; }

        [Required]
        [Display(Name = "Amount in terms of rule 8(1) (h) of the ITC Rules")]
        public ItemRev rule8_1_h { get; set; }

        [Required]
        [Display(Name = "Amount in terms of rule 7 (2)(a) of ITC Rules")]
        public ItemRev rule7_2_a { get; set; }

        [Required]
        [Display(Name = "Amount in terms of rule 7(2)(b) of ITC Rules")]
        public ItemRev rule7_2_b { get; set; }

        [Required]
        [Display(Name = "On account of amount paid subsequent to reversal of ITC")]
        public ItemRev revitc { get; set; }
        
        [Required]
        [Display(Name = "Any other liability (Pl specify)")]
        public ItemRev other { get; set; }

        [Required]
        [Display(Name = "Checksum Value")]
        [MaxLength(64)]
        [RegularExpression("^[a-zA-Z0-9]+$")]
        public string chksum { get; set; }
    }
    public class ItemRev
    {
        [Display(Name = "tax amount for igst")]
        public double iamt { get; set; }

        [Display(Name = "tax amount for sgst")]
        public double samt { get; set; }

        [Display(Name = "tax amount for cgst")]
        public double camt { get; set; }

        [Display(Name = "tax amount for cess")]
        public double csamt { get; set; }
    }


}