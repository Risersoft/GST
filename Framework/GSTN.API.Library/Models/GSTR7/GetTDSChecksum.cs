using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;

namespace Risersoft.API.GSTN.GSTR7
{
   
   
    public class GetTDSChecksum
    {


        [Display(Name = "List of TDS ")]
        public List<TDS> tds { get; set; }

       
        [Display(Name = "List of TDSA")]
        public List<TDS> tdsa { get; set; }


        [Required]
        [Display(Name = "Request Time")]
        [RegularExpression("^((0[1-9]|[12][0-9]|3[01])[-](0[1-9]|1[012])[-]((19|20)\\d\\d))$")]
        public string req_time { get; set; }

    }

    public class TDS
    {

        [Required]
        [Display(Name = "GSTIN of the taxpayer")]
        [MaxLength(15)]
        [MinLength(15)]
        [RegularExpression("^[0-9]{2}[a-zA-Z]{5}[0-9]{4}[a-zA-Z]{1}[1-9A-Za-z]{1}[Zz1-9A-Ja-j]{1}[0-9a-zA-Z]{1}$")]
        public string gstin_ded { get; set; }

       

        [Required]
        [Display(Name = "CheckSum")]
        [MaxLength(64)]
        public string chksum { get; set; }
    }

   

}