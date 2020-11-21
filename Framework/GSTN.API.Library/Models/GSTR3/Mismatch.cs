using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Risersoft.API.GSTN.GSTR3
{
    public class Mismatch
    {
        

        [Required]
        [Display(Name = "ITC claimed on mismatched/duplication of invoices/debit notes")]
        public Misdet itc_clm { get; set; }

        [Required]
        [Display(Name = "ITC claimed on mismatched/duplication of invoices/debit notes")]
        public Misdet tax_liab { get; set; }


        [Required]
        [Display(Name = "Reclaim on rectification of mismatched invoices/Debit Notes ")]
        public Misdet rclm_idn { get; set; }

        [Required]
        [Display(Name = "Reclaim on rectification of mismatch credit note")]
        public Misdet rclm_cn { get; set; }

        [Required]
        [Display(Name = "Negative tax liability from previous tax periods ")]
        [MaxLength(5)]
        public Misdet neg_tax_liab { get; set; }
        [Required]
        [Display(Name = "Tax paid on advance")]
        public Misdet adv_tx { get; set; }
        [Required]
        [Display(Name = "ITC Reversal / Reclaim")]
        [MaxLength(5)]
        public Misdet itc_rvl { get; set; }
    }
    public class Misdet
    {
        [Required]
        [Display(Name = "Central Tax Amount")]
        public double camt { get; set; }


        [Required]
        [Display(Name = "State /  Union Territory Tax Amount")]
        public double samt { get; set; }

        [Required]
        [Display(Name = "Cess Amount")]
        public double cess { get; set; }

        [Required]
        [Display(Name = "Integrated Tax Amount")]
        public double iamt { get; set; }
    }
}