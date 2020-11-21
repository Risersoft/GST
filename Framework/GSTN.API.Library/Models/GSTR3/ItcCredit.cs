using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Risersoft.API.GSTN.GSTR3
{
    public class ItcCredit
    {


        [Required]
        [Display(Name = "List of ITC credit details")]
        public List<Itcdet> itc_det { get; set; }



        [Required]
        [Display(Name = "List of ITC credit details on Amendment")]
        public List<Itcdet> itc_det_amm { get; set; }
    }
}