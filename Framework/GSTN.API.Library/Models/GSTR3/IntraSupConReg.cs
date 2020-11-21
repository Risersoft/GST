using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Risersoft.API.GSTN.GSTR3
{
    public class IntraSupConReg
    {
       

        [Required]
        [Display(Name = "Taxable supplies (other than reverse charge and zero rated supply)")]
        public List<Taxsupl> tax_sup { get; set; }

        [Required]
        [Display(Name = "Supplies attracting  reverse charge-Tax payable by recipient of supply ")]
        public List<Taxsupl> rev_sup { get; set; }




        [Required]
        [Display(Name = "Out of the supplies mentioned at A, the value of  supplies made though an e-commerce operator attracting TCS")]
        public List<Ecommsupl> ecomm_sup { get; set; }
       
    }

    public class Taxsupl
    {

        [Required]
        [Display(Name = " Rate of Tax")]
        public double tx_r { get; set; }

        [Required]
        [Display(Name = " taxable value/Net differential value ")]
        public double txval { get; set; }
        [Required]
        [Display(Name = "CGST on Value of Intra-State Supply of goods to Registered taxpayers")]
        public double camt { get; set; }
        [Required]
        [Display(Name = " SGST on Value of Intra-State Supply of goods to Registered taxpayers")]
        public double samt { get; set; }


        [Required]
        [Display(Name = "CESS on Value of Inter-State Supply of goods to Registered taxpayers")]
        public double cess { get; set; }
    }
    public class Ecommsupl
    {
        [Required]
        [Display(Name = " Rate of Tax")]
        public double tx_r { get; set; }

        [Required]
        [Display(Name = " taxable value/Net differential value ")]
        public double txval { get; set; }
        [Required]
        [Display(Name = "CGST on Value of Intra-State Supply of goods to Registered taxpayers")]
        public double camt { get; set; }
        [Required]
        [Display(Name = " SGST on Value of Intra-State Supply of goods to Registered taxpayers")]
        public double samt { get; set; }


        [Required]
        [Display(Name = "CESS on Value of Inter-State Supply of goods to Registered taxpayers")]
        public double cess { get; set; }

       
        [Required]
        [Display(Name = " Gstin of the Ecommerce taxpayer")]
        [MaxLength(15)]
        public string gstin { get; set; }

     
    }
    }