using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;


namespace Risersoft.API.GSTN.ITC4
{
    public class ITC4Summary
    {



        [Display(Name = "Total number of M2J Records")]
        public string m2j_no { get; set; }

       
        [Display(Name = "Total Taxable value of table 4")]
        public string m2j_ttl_tax { get; set; }

       
        [Display(Name = "Total number of JW2M Records")]
        public string j2m_no { get; set; }

       
        [Display(Name = "Total Taxable value of table 5")]
        public string j2m_ttl_tax { get; set; }

       
        [Display(Name = "j2m invoices checksum")]
        public string j2m_chksum { get; set; }

       
        [Display(Name = "m2j invoices checksum")]
        public string m2j_chksum { get; set; }
    }


   
}
