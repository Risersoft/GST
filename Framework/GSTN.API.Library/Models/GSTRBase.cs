using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSTN.API.Library.Models
{
  public  class GSTRBase
    {
        [Display(Name = "GSTIN of the Tax Payer")]
        [Required]
        [MaxLength(15)]
        [MinLength(15)]
        [RegularExpression("^[a-zA-Z0-9]+$")]
        public string gstin { get; set; }

        [Required]
        [Display(Name = "Financial Period")]
        [MaxLength(10)]
        [MinLength(3)]
        [RegularExpression("^[0-9]+$")]
        public virtual string fp { get; set; }
        public string token { get; set; }
        public string est { get; set; }
        public string error_cd { get; set; }
        public string error_msg { get; set; }


    }
    public class SummaryBase
    {
        [Required]
        [Display(Name = "GSTIN of the taxpayer")]
        [MaxLength(15)]
        [MinLength(15)]
        [RegularExpression("^[a-zA-Z0-9]+$")]
        public string gstin { get; set; }
        [Required]
        [Display(Name = "Return Period")]
        [RegularExpression("^((0[1-9]|1[012])((19|20)\\d\\d))*$")]
        public string ret_period { get; set; }

        [Required]
        [Display(Name = "Invoice Check sum value")]
        [MaxLength(64)]
        public string chksum { get; set; }

        [Required]
        [Display(Name = "Short / long Summary")]
        [MaxLength(1)]
        [RegularExpression("^[S/L]")]
        public string summ_typ { get; set; }



    }
}
