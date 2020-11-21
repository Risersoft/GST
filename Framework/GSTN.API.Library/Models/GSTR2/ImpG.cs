using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;

namespace Risersoft.API.GSTN.GSTR2
{
    public class ImpGItm
    {

        [Required]
        [Display(Name = "Item Number")]
        public int num { get; set; }

        
        [Required]
        [Display(Name = "Taxable value of Goods/Service as per invoice")]
        public double txval { get; set; }

        [Required]
        [Display(Name = "Tax Rate")]
        public double rt { get; set; }

        [Required]
        [Display(Name = "IGST amount")]
        public double iamt { get; set; }

        [Required]
        [Display(Name = "CESS amount")]
        public double csamt { get; set; }

        [Required]
        [Display(Name = "Eligibility for ITC")]
        public string elg { get; set; }

        [Required]
        [Display(Name = "Total IGST available as ITC")]
        public double tx_i { get; set; }

        [Required]
        [Display(Name = "Total CESS available as ITC ")]
        public double tx_cs { get; set; }

    }

    public class ImpG
    {

        [Required]
        [Display(Name = "Bill of Entry Number")]
        [MaxLength(7)]
        public int boe_num { get; set; }

        [Required]
        [Display(Name = "Bill of Entry Date")]
        [RegularExpression("^((0[1-9]|[12][0-9]|3[01])[-](0[1-9]|1[012])[-]((19|20)\\d\\d))*$")]
        public string boe_dt { get; set; }

        [Required]
        [Display(Name = "Port Code")]
        public string port_code { get; set; }

        

        [Required]
        [Display(Name = "flag to determine if it is sez or not")]
        [MaxLength(1)]
        public string is_sez { get; set; }


        [Required]
        [Display(Name = "GSTIN/UID of the Supplier taxpayer(if is_sez = Y then only present)")]
        [MaxLength(15)]
        [MinLength(15)]
        public string stin { get; set; }

        [Required]
        [Display(Name = "Bill of Entry Value")]
        public double boe_val { get; set; }

        [Required]
        [Display(Name = "Bill Item Details")]
        public List<ImpGItm> itms { get; set; }


        [Display(Name = "Invoice Check sum value")]
        [MaxLength(15)]
        public string chksum { get; set; }
    }


}