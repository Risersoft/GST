using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Risersoft.API.GSTN.GSTR6
{
    public class SecSummary
    {

       
        [Display(Name = "Return Section")]
        [MaxLength(5)]
        public string section_name { get; set; }

        [Display(Name = "Invoice checksum value")]
        [MaxLength(64)]
        public string chksum { get; set; }

        
        [Display(Name = "Record Count  ")]
       
        public double rc { get; set; }

       
        [Display(Name = "total value")]
        
        public double val { get; set; }

        
        [Display(Name = "Total Taxible Value ")]
        
        public double ttl_val { get; set; }

      

       
        public List<CounterPartySummary> counterpartysummary { get; set; }




        
        [Display(Name = "Total Tax paid IGST")]
        public double ttl_igst { get; set; }

       
        [Display(Name = "Total Tax paid SGST")]
        public double ttl_sgst { get; set; }


       
        [Display(Name = "Total Tax paid CGST")]
        public double ttl_cgst { get; set; }
        
        [Display(Name = "Total Tax paid CESS")]
        public double ttl_cess { get; set; }




    }




    public class ISDSectionSummary
    {
        
        [Display(Name = "Return Section")]
        [MaxLength(10)]
        public string section_name { get; set; }



       
        [Display(Name = "Invoice Check sum value ")]
        [MaxLength(64)]
        public string chksum { get; set; }



        
        [Display(Name = "Eligible list ")]
        public List<Elglstc> elglst { get; set; }

        
        [Display(Name = "Ineligible List ")]
        public List<Elglstc> inelglst { get; set; }
    }


    public class Elglstc
    {
       
        [Display(Name = "Total Taxible Value ")]
        public double ttl_val { get; set; }

       
        [Display(Name = "Total Tax paid IGST")]
        public double ttl_igst { get; set; }

       
        [Display(Name = "Total Tax paid SGST")]
        public double ttl_sgst { get; set; }
       
        [Display(Name = "Total Tax paid CGST")]
        public double ttl_cgst { get; set; }



       
        [Display(Name = "Total Tax paid CESS")]
        public double ttl_cess { get; set; }
        
        [Display(Name = "total value")]
        public string val { get; set; }

       
        [Display(Name = "Record Count  ")]
        public double rc { get; set; }

       
        [Display(Name = "Counter Party Summary")]
        public List<CounterPartySummary> ISDunitsummary { get; set; }


    }

    public class CounterPartySummary
    {
       
        [Display(Name = "Supplier TIN for ISD unit ")]
        [MaxLength(15)]
        public string ctin { get; set; }


       
        [Display(Name = "Invoice Check sum value ")]
        [MaxLength(64)]
        public string chksum { get; set; }

       
        [Display(Name = "Total Taxible Value ")]
        public double ttl_val { get; set; }

       
        [Display(Name = "Total Tax paid IGST")]
        public double ttl_igst { get; set; }

       
        [Display(Name = "Total Tax paid SGST")]
        public double ttl_sgst { get; set; }
        
        [Display(Name = "Total Tax paid CGST")]
        public double ttl_cgst { get; set; }



       
        [Display(Name = "Total Tax paid CESS")]
        public double ttl_cess { get; set; }
        
        [Display(Name = "total value")]
        public string val { get; set; }

       
        [Display(Name = "Record Count  ")]
        public double rc { get; set; }
    }

    public class Summary

    {

                                
        [Display(Name = "GSTIN of the taxpayer")]
        [MaxLength(15)]
        [MinLength(15)]
        [RegularExpression("^[a-zA-Z0-9]+$")]
        public string gstin { get; set; }


        
        [Display(Name = "Return Period")]
        [RegularExpression("^((0[1-9]|1[012])[-]((19|20)\\d\\d))*$")]
        public string ret_period { get; set; }
       
        [Display(Name = "Invoice Check sum value ")]
        [MaxLength(64)]
        public string chksum { get; set; }
       
        [Display(Name= "section_summary")]
        public List<SecSummary> section_summary { get; set; }
    }
}
