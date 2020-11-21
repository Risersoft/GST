using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Risersoft.API.GSTN.GSTR3
{

    public class IntrSupConReg
    {

        [Required]
        [Display(Name = "Taxable supplies (other than reverse charge and zero rated supply)")]
        public List<Taxsup> tax_sup { get; set; }

        [Required]
        [Display(Name = "Supplies attracting  reverse charge-Tax payable by recipient of supply ")]
        public List<Taxsup> rev_sup { get; set; }

        [Required]
        [Display(Name = "Intra-State Supplies to Registered taxpayers")]
        public List<Taxsup> zr_sup_wp { get; set; }

        [Required]
        [Display(Name = "Out of the supplies mentioned at A, the value of  supplies made though an e-commerce operator attracting TCS")]
        public List<Ecommsup> ecomm_sup { get; set; }



    }
    public class Taxsup
    {

        [Required]
        [Display(Name = " Rate of Tax")]
        public double tx_r { get; set; }

        [Required]
        [Display(Name = " taxable value/Net differential value ")]
        public double txval { get; set; }


        [Required]
        [Display(Name = " IGST Amount as per invoice")]
        public double iamt { get; set; }


        [Required]
        [Display(Name = "CESS on Value of Inter-State Supply of goods to Registered taxpayers")]
        public double cess { get; set; }
    }
    public class Ecommsup
    {
        [Required]
        [Display(Name = " Rate of Tax")]
        public double tx_r { get; set; }

        [Required]
        [Display(Name = " taxable value/Net differential value ")]
        public double txval { get; set; }
        [Required]
        [Display(Name = " Gstin of the Ecommerce taxpayer")]
        [MaxLength(15)]
        public string gstin { get; set; }

        [Required]
        [Display(Name = " IGST Amount as per invoice")]
        public double iamt { get; set; }


        [Required]
        [Display(Name = "CESS on Value of Inter-State Supply of goods to Registered taxpayers")]
        public double cess { get; set; }

    }
}