using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;

namespace Risersoft.API.GSTN.GSTR2
{
   
    public class ImpS
    {

        [Required]
        [Display(Name = "Invoice number")]
        [MaxLength(10)]
        [RegularExpression("^[a-zA-Z0-9]+$")]
        public string inum { get; set; }

        [Required]
        [Display(Name = "Invoice date")]
        [RegularExpression("^((0[1-9]|[12][0-9]|3[01])[-](0[1-9]|1[012])[-]((19|20)\\d\\d))*$")]
        public string idt { get; set; }

        [Required]
        [Display(Name = "Invoice Value")]
        public double ival { get; set; }

        [Required]
        [Display(Name = "Point of sale")]
        [MaxLength(2)]
        public string pos { get; set; }


        [Required]
        [Display(Name = "Bill Item Details")]
        public List<ImpGItm> itms { get; set; }


        [Display(Name = "Invoice Check sum value")]
        [MaxLength(15)]
        public string chksum { get; set; }
    }


}