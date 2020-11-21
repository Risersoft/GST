using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using GSTN.API.Library.Models;

namespace Risersoft.API.GSTN.GSTR1
{
    public class CptySum
    {

        [Required]
        [Display(Name = "CTIN of the taxpayer")]
        [MaxLength(15)]
        [MinLength(15)]
        [RegularExpression("^[a-zA-Z0-9]+$")]
        public string ctin { get; set; }

        [Required]
        [Display(Name = "Invoice Check sum value")]
        [MaxLength(64)]
        public string chksum { get; set; }

        [Required]
        [Display(Name = "Total Record Count")]
        public int ttl_rec { get; set; }

        [Required]
        [Display(Name = "Total records value")]
        public double ttl_val { get; set; }

        [Required]
        [Display(Name = "Total Cess")]
        public double ttl_cess { get; set; }

        [Required]
        [Display(Name = "Total taxable value of records")]
        public double ttl_tax { get; set; }


        [Required]
        [Display(Name = "total IGST amount")]
        public double ttl_igst { get; set; }

        [Required]
        [Display(Name = "total SGST amount")]
        public double ttl_sgst { get; set; }

        [Required]
        [Display(Name = "total CGST amount")]
        public double ttl_cgst { get; set; }
    }

    public class SecSum
    {

        [Required]
        [Display(Name = "Return Section")]
        [MaxLength(5)]
        public string sec_nm { get; set; }

        [Required]
        [Display(Name = "Invoice Check sum value")]
        [MaxLength(64)]
        public string chksum { get; set; }

        [Required]
        [Display(Name = "Total Record Count")]
        public int ttl_rec { get; set; }

        [Required]
        [Display(Name = "Total records value")]
        public double ttl_val { get; set; }

        [Required]
        [Display(Name = "Total Cess")]
        public double ttl_cess { get; set; }

        [Required]
        [Display(Name = "Total taxable value of records")]
        public double ttl_tax { get; set; }

        [Required]
        [Display(Name = "total IGST amount")]
        public double ttl_igst { get; set; }

        [Required]
        [Display(Name = "total SGST amount")]
        public double ttl_sgst { get; set; }

        [Required]
        [Display(Name = "total CGST amount")]
        public double ttl_cgst { get; set; }

        [Display(Name = "cpty_sum")]
        public List<CptySum> cpty_sum { get; set; }
    }
    public class SummaryOutward:SummaryBase
    {
       
        [Display(Name = "section_summary")]
        public List<SecSum> sec_sum { get; set; }
    }
}