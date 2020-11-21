using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Risersoft.API.GSTN.GSTR1
{





   
    
    public class CDNURAOutward: CDNUROutward
    {



       
        [Required]
        [Display(Name = "Original Credit note/debit note /  Refund Voucher Number")]
        [RegularExpression("^((0[1-9]|[12][0-9]|3[01])[-](0[1-9]|1[012])[-]((19|20)\\d\\d))*$")]
        public string ont_num { get; set; }


        [Required]
        [Display(Name = "Original Credit Note/Debit Note/  Refund Voucher date")]
        [RegularExpression("^((0[1-9]|[12][0-9]|3[01])[-](0[1-9]|1[012])[-]((19|20)\\d\\d))*$")]
        public string ont_dt { get; set; }
        
    }
}
