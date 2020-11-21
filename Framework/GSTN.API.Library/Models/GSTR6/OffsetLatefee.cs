using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Risersoft.API.GSTN.GSTR6
{
    
    public class OffsetLatefee

    {

        [Required]
        [Display(Name = "GSTIN of the taxpayer")]
        [MaxLength(15)]
        [MinLength(15)]
        [RegularExpression("^[a-zA-Z0-9]+$")]
        public string gstin { get; set; }


        [Required]
        [Display(Name = "Return Period")]     
        public string ret_period { get; set; }




        [Required]
        [Display(Name = "Return type")]
        public string ret_type { get; set; }

        [Required]
        [Display(Name = "Liability id")]
        public double liab_id { get; set; }

        [Required]
        [Display(Name= "Transaction code")]
        public double tran_cd { get; set; }

        [Required]
        [Display(Name = "CGST Latefee Amount")]
        public double cgstfee { get; set; }

        [Required]
        [Display(Name = "SGST Latefee Amount")]
      
        public double sgstfee { get; set; }
    }




  
}
