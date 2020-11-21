using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using GSTN.API.Library.Models;

namespace Risersoft.API.GSTN.GSTR4
{
    public class CptySum
    {

       
        [Display(Name = "GSTIN of supplier")]
        [MaxLength(15)]
        [MinLength(15)]
        [RegularExpression("^[a-zA-Z0-9]+$")]
        public string ctin { get; set; }   

       
        [Display(Name = "Total Record Count")]
        public double ttl_rec { get; set;}

       
        [Display(Name = "Invoice Check sum value")]
        [MaxLength(64)]
        public string chksum { get; set; }

        
        [Display(Name = "Total records value")]
        public double ttl_val { get; set; }

       
        [Display(Name = "Total IGST ")]
        public double ttl_igst { get; set; }

       
        [Display(Name = "Total SGST ")]
        public double ttl_sgst { get; set; }

       
        [Display(Name = "Total CGST ")]
        public double ttl_cgst { get; set; }

       
        [Display(Name = "Total Cess")]
        public double ttl_cess { get; set; }

       
        [Display(Name = "Total taxable value of records")]
        public double ttl_tax { get; set; }

    }

    public class SecSum
    {

       
        [Display(Name = "Return Section name")]
        [MaxLength(5)]
        public string sec_nm { get; set; }

        
        [Display(Name = "Total Record Count")]
        public double ttl_rec { get; set; }

        
        [Display(Name = "section Check sum value ")]
        [MaxLength(64)]
        public string chksum { get; set; }

       
        [Display(Name = "Total records value")]
        public double ttl_val { get; set; }

       
        [Display(Name = "Total IGST ")]
        public double ttl_igst { get; set; }

       
        [Display(Name = "Total SGST")]
        public double ttl_sgst { get; set; }

       
        [Display(Name = "Total CGST ")]
        public double ttl_cgst { get; set; }

       
        [Display(Name = "Total Cess")]
        public double ttl_cess { get; set; }

       
        [Display(Name = "Total taxable value of records")]
        public double ttl_tax { get; set; }
        
       
        [Display(Name = "Counter Party Summary")]
        public List<CptySum> cpty_sum { get; set; }
    }

    public class Summary: SummaryBase
    {

        [Required]
        [Display(Name = "sec_sum")]
        public List<SecSum> sec_sum { get; set; }



        [Required]
        [Display(Name = "Total Liability")]
        public GetTaxPaid tax_py_pd { get; set; }


    }

}