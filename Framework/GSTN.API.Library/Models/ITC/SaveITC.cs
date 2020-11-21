using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;


namespace GSTN.API.Library.Models.ITC
{
    public class SaveITC
    {



        [Display(Name = "GSTIN of the taxpayer")]
        [MaxLength(15)]
        [MinLength(15)]
        [RegularExpression("^[0-9]{2}[a-zA-Z]{5}[0-9]{4}[a-zA-Z]{1}[1-9A-Za-z]{1}[Zz1-9A-Ja-j]{1}[0-9a-zA-Z]{1}$")]
        public string gstin { get; set; }

       
        [Display(Name = "Return Type")]
        [MaxLength(8)]
        public string rtn_typ { get; set; }

       
        [Display(Name = "Date of Filing, present only in case of ITC03-4A Return Type.")]
        public string dof { get; set; }

       
        [Display(Name = "Composition ARN, Present in case of ITC03-4A return Type. ARN of the application filed to opt composition")]
        [MaxLength(15)]
        public string comp_arn { get; set; }

       
        [Display(Name = "Date of Exemption, present only in case of ITC03-4B Return Type.")]
        public string doe { get; set; }

       
        [Display(Name = "List of Invoices")]
        public List<Invoice> invs { get; set; }

        [Display(Name = "CA Certificate Details")]
        public CADetailsComp attachment { get; set; }
    }


}
