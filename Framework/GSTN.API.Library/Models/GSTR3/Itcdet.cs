using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Risersoft.API.GSTN.GSTR3
{
    public class Itcdet
    {
       

        [Required]
        [Display(Name = "Identifer if Goods Input , Capital Goods or Services")]
        [MaxLength(30)]
        public string ty { get; set; }

        [Required]
        [Display(Name = "Taxable Value")]
        public double txval { get; set; }

        [Required]
        [Display(Name = "Integrated Tax Amount")]
        public double itx { get; set; }

        [Required]
        [Display(Name = "Central Tax Amount")]
        public double ctx { get; set; }

        [Required]
        [Display(Name = "State / Union Territory Tax Amount")]
        public double stx { get; set; }

        [Required]
        [Display(Name = "Cess Amount")]
        public double cstx { get; set; }




        [Required]
        [Display(Name = "ITC on Integrated Tax ")]
        public double i_itc { get; set; }

        [Required]
        [Display(Name = "ITC on Central Tax ")]
        public double c_itc { get; set; }

        [Required]
        [Display(Name = "ITC on State/Union territory")]
        public double s_itc { get; set; }

        [Required]
        [Display(Name = "ITC on Cess ")]
        public double cs_itc { get; set; }

        
    }
}