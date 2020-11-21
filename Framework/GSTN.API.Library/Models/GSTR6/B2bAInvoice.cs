using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;

namespace Risersoft.API.GSTN.GSTR6
{
   

   



    public class InvDet:Inv
    {
        [Required]   
        [Display(Name = "filing period of the original invoice")]
        public string opd { get; set; }                    


        [Required]
        [Display(Name = "Original Invoice")]
        [MaxLength(15)]
        public string ostin { get; set; }

        [Required]
        [Display(Name = "Original Invoice Number")]
        [MaxLength(16)]
        [RegularExpression("^[a-zA-Z0-9]+$")]
        public string oinum { get; set; }


        [Required]
        [Display(Name = "Original Invoice Date")]
        [RegularExpression("^((0[1-9]|[12][0-9]|3[01])[-](0[1-9]|1[012])[-]((19|20)\\d\\d))$")]
        public string oidt { get; set; }

        
       

       

       

        
    }

    public class B2bAInvoice
    {

        [Required]
        [Display(Name = "GSTIN/UID of the Supplier taxpayer")]
        [MaxLength(15)]
        [MinLength(15)]
        [RegularExpression("^[a-zA-Z0-9]+$")]
        public string ctin { get; set; }

        [Required]
        [Display(Name = "counter party Filing Status")]
        public string cfs { get; set; }



        [Required]
        [Display(Name = "Invoice Details")]
        public List<InvDet> inv { get; set; }
    }

}