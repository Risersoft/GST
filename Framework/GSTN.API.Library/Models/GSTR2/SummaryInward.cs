using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using GSTN.API.Library.Models;

//class properties does not match to schema like 'ttl_inv','ttl_tax','ttl_igst','ttl_sgst','ttl_cgst'
// in excel file not mention ctin while use return section
namespace Risersoft.API.GSTN.GSTR2
{
    public class CptySum
    {

       
        [Display(Name = "Supplier TIN")]
        [MaxLength(15)]
        [MinLength(15)]
        [RegularExpression("^[a-zA-Z0-9]+$")]
        public string ctin { get; set; }     //This is not match excel sheet Parameter Name

        [Required]
        [Display(Name = "Total Number of invoces")]
        public double rc { get; set;}

        [Required]
        [Display(Name = "Invoice Check sum value")]
        [MaxLength(64)]
        public string chksum { get; set; }


        
        [Display(Name = "ttl_value")]
        public double ttl_val { get; set; }

        
        [Display(Name = "Total Tax paid IGST")]
        public double ttl_txpd_igst { get; set; }
        [Display(Name = "Total Tax paid SGST")]
        public double ttl_txpd_sgst { get; set; }

        
        [Display(Name = "Total Tax paid CGST")]
        public double ttl_txpd_cgst { get; set; }

       
        [Display(Name = "Total Tax paid CESS")]
        public double ttl_txpd_cess { get; set; }

        
        [Display(Name = "ITC Availed IGST")]
        public double ttl_itcavld_igst { get; set; }

        
        [Display(Name = "ITC Availed SGST")]
        public double ttl_itcavld_sgst { get; set; }

       
        [Display(Name = "ITC Availed CGST")]
        public double ttl_itcavld_cgst { get; set; }

        
        [Display(Name = "ITC Availed CESS")]
        public double ttl_itcavld_cess { get; set; }
    }

    public class SecSum
    {

        [Required]
        [Display(Name = "Return Section")]
        [MaxLength(5)]
        public string section_name { get; set; }

        [Required]
        [Display(Name = "Total Number of invoces")]
        public double rc { get; set; }

        [Required]
        [Display(Name = "Invoice Check sum value")]
        [MaxLength(64)]
        public string chksum { get; set; }

        
        [Display(Name = "ttl_value")]
        public double ttl_val { get; set; }

       
        [Display(Name = "Total Tax paid IGST")]
        public double ttl_txpd_igst { get; set; }

        
        [Display(Name = "Total Tax paid SGST")]
        public double ttl_txpd_sgst { get; set; }

        
        [Display(Name = "Total Tax paid CGST")]
        public double ttl_txpd_cgst { get; set; }

        
        [Display(Name = "Total Tax paid CESS")]
        public double ttl_txpd_cess { get; set; }

        
        [Display(Name = "ITC Availed IGST")]
        public double ttl_itcavld_igst { get; set; }

       
        [Display(Name = "ITC Availed SGST")]
        public double ttl_itcavld_sgst { get; set; }

        [Display(Name = "ITC Availed CGST")]
        public double ttl_itcavld_cgst { get; set; }

        
        [Display(Name = "ITC Availed CESS")]
        public double ttl_itcavld_cess { get; set; }

        
        [Display(Name = "Counter Party Summary")]
        public List<CptySum> cpty_sum { get; set; }
    }

    public class SummaryInward: SummaryBase
    {

        [Required]
        [Display(Name = "sec_sum")]
        public List<SecSum> section_summary { get; set; }
    }

}