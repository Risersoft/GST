using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;

namespace Risersoft.API.GSTN.GSTR8
{


    public class TCSADetails : TCSDetl
    {




        [Required]
        [Display(Name = "Original GSTIN of Supplier")]
        [MaxLength(15)]
        [MinLength(15)]
        [RegularExpression("^[0-9]{2}[a-zA-Z]{5}[0-9]{4}[a-zA-Z]{1}[1-9A-Za-z]{1}[Zz1-9A-Ja-j]{1}[0-9a-zA-Z]{1}$")]
        public string ostin { get; set; }




        [Required]
        [Display(Name = "Original return Period of TCS")]
        [RegularExpression("^(([0-9]{2}((19|20)\\d\\d)))$")]
        public string ofp { get; set; }

        [Required]
        [Display(Name = "Action Taken")]

        public string actn { get; set; }

    }

}