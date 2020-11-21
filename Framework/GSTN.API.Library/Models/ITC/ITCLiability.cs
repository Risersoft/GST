using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using Risersoft.API.GSTN.GSTR7;

namespace GSTN.API.Library.Models.ITC
{
    public class ITCLiability
    {



        [Display(Name = "GSTIN of the taxpayer")]
        [MaxLength(15)]
        [MinLength(15)]
        [RegularExpression("^[0-9]{2}[a-zA-Z]{5}[0-9]{4}[a-zA-Z]{1}[1-9A-Za-z]{1}[Zz1-9A-Ja-j]{1}[0-9a-zA-Z]{1}$")]
        public string gstin { get; set; }



        [Display(Name = "Cash Utilization")]
        public PaidCash pd_by_cash { get; set; }


        [Display(Name = "ITC Utilization")]
        public PaidUti pd_by_itc { get; set; }



    }
    public class PaidCash
    {
        [Required]
        [Display(Name = "Transaction type")]
        public decimal tran_cd { get; set; }

        [Required]
        [Display(Name = "Igst tax payable")]
        public MinorHeads igst { get; set; }

        [Required]
        [Display(Name = "cgst tax payable")]
        public MinorHeads cgst { get; set; }
        [Required]
        [Display(Name = "sgst tax payable")]
        public MinorHeads sgst { get; set; }

        [Required]
        [Display(Name = "cess tax payable")]
        public MinorHeads cess { get; set; }
    }

    public class PaidUti
    {




        [Required]
        [Display(Name = "transaction code")]
        public decimal tran_cd { get; set; }

        [Required]
        [Display(Name = "IGST paid using igst")]
        public decimal igst_igst_amt { get; set; }

        [Required]
        [Display(Name = "IGST paid using Cgst")]
        public decimal igst_cgst_amt { get; set; }

        [Required]
        [Display(Name = "IGST paid using Sgst")]
        public decimal igst_sgst_amt { get; set; }

        [Required]
        [Display(Name = "SGST paid using sgst ")]
        public decimal sgst_sgst_amt { get; set; }

        [Required]
        [Display(Name = "SGST paid using igst ")]
        public decimal sgst_igst_amt { get; set; }


        [Required]
        [Display(Name = "CGST paid using cgst")]
        public decimal cgst_cgst_amt { get; set; }

        [Required]
        [Display(Name = "CGST paid using igst")]
        public decimal cgst_igst_amt { get; set; }


        [Required]
        [Display(Name = "Cess paid using cess")]
        public decimal cess_cess_amt { get; set; }
    }
}
