using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;

namespace Risersoft.API.GSTN.GSTR4
{


  
    public class GetImports

    {
        [Required]
        [Display(Name = "IMPS Invoice Details ")]
        public List<Itemdet> imp_s { get; set; }

    }
    public class Itemdet
    {
        [Required]
        [Display(Name = "Invoice Check sum value ")]
        [MaxLength(64)]
        public string chksum { get; set; }

        [Required]
        [Display(Name = "Invoice No.")]
        [MaxLength(16)]
        public double inum { get; set; }

        [Required]
        [Display(Name = "Invoice Date")]
        [RegularExpression("^((0[1-9]|[12][0-9]|3[01])[-](0[1-9]|1[012])[-]((19|20)\\d\\d))*$")]
        public string idt { get; set; }

        [Required]
        [Display(Name = "Invoice Value")]
        public double ival { get; set; }
       

        [Required]
        [Display(Name = "Point of sale")]

        public string pos { get; set; }
        [Required]
        [Display(Name = "Bill Item Details")]
        public List<ItmDetc> itms { get; set; }
    }
   
    public class ItmDetc
    {


        [Required]
        [Display(Name = "Taxable value")]
        public double txval { get; set; }

        [Required]
        [Display(Name = "Tax Rate")]
        public double rt { get; set; }


        [Display(Name = "CESS Amount")]
        public double csamt { get; set; }
        [Display(Name = "IGST Amount")]
        public double iamt { get; set; }

    }
}