using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;

namespace Risersoft.API.GSTN.GSTR1
{


    public class B2CLAInv : B2CLInv
    {
        [Required]
        [Display(Name = "Original invoice number")]
        [MaxLength(50)]
       
        public string oinum { get; set; }

        [Required]
        [Display(Name = "Original invoice date")]
        [RegularExpression("^((0[1-9]|[12][0-9]|3[01])[-](0[1-9]|1[012])[-]((19|20)\\d\\d))*$")]
        public string oidt { get; set; }

    }

    public class B2clAOutward
    {

        [Required]
        [Display(Name = "Place of Supply ")]
        [MaxLength(2)]
        public string pos { get; set; }

        [Required]
        [Display(Name = "Invoice Details")]
        public List<B2CLAInv> inv { get; set; }
        public string error_cd { get; set; }
        public string error_msg { get; set; }
    }



}