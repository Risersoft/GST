using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;


namespace Risersoft.API.GSTN.ITC4
{
    public class ITC4Invoice
    {


        [Required]
        [Display(Name = "GSTIN of the taxpayer")]
        [MaxLength(15)]
        [MinLength(15)]
        [RegularExpression("^[0-9]{2}[a-zA-Z]{5}[0-9]{4}[a-zA-Z]{1}[1-9A-Za-z]{1}[Zz1-9A-Ja-j]{1}[0-9a-zA-Z]{1}$")]
        public string gstin { get; set; }

        [Required]

        [Display(Name = "Return period")]
        [RegularExpression("^(([0-9]{2}((19|20)\\d\\d)))$")]
        public string fp { get; set; }


        [Display(Name = "ITC04 Invoices")]
        public List<M2JWInvoiceData> m2jw { get; set; }

        [Display(Name = "ITC04 Invoices")]
        public List<JW2MInvoice> jw2m { get; set; }

    }


    public class M2JWInvoiceData
    {

      

        [Required]
        [Display(Name = "Job worker's State Code. This parameter is mandatory for both registererd and unregistered Job worker")]
        public string jw_stcd { get; set; }



        [Required]
        [Display(Name = "Challan Number")]
        public string chnum { get; set; }

        [Required]
        [Display(Name = "Challan Date")]
        [RegularExpression("^((0[1-9]|[12][0-9]|3[01])[-](0[1-9]|1[012])[-]((19|20)\\d\\d))$")]
        public string chdt { get; set; }



        [Required]
        [Display(Name = "List of ITC04 M2Jw Item Data")]
        public ITC04M2JW itms { get; set; }
    }

}

