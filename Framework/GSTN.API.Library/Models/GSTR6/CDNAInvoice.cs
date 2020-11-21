using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Risersoft.API.GSTN.GSTR1;

namespace Risersoft.API.GSTN.GSTR6
{
    public class CDNInvDet: CDNInv


    {
        [Required]
        [Display(Name = "filing period of the original invoice")]
        public string opd { get; set; }


        [Required]
        [Display(Name = "Original Invoice")]
        [MaxLength(15)]
        public string ostin { get; set; }

        [Required]
        [Display(Name = "original Note number")]
        [MaxLength(50)]
        [RegularExpression("^[a-zA-Z0-9]+$")]
        public string ont_num { get; set; }


        [Required]
        [Display(Name = "original note date")]
        [RegularExpression("^((0[1-9]|[12][0-9]|3[01])[-](0[1-9]|1[012])[-]((19|20)\\d\\d))$")]
        public string ont_dt { get; set; }

    }
    public class CDNAInvoice

    {

        [Required]                        
        [Display(Name = "Counter party GSTIN")]
        [MaxLength(15)]
        [MinLength(15)]
        [RegularExpression("^[a-zA-Z0-9]+$")]
        public string ctin { get; set; }

        [Required]
        [Display(Name = "counter party Filing Status")]
        public string cfs { get; set; }

        [Required]
        [Display(Name= "Credit/Debit Notes  Details ")]
        public List<CDNInvDet> nt { get; set; }
    }
}
