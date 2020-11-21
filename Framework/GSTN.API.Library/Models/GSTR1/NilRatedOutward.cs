using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;

namespace Risersoft.API.GSTN.GSTR1
{
   

    public class NilSupplyData
    {

        [Required]
        [Display(Name = "Nature of Supply Type")]
        [MaxLength(8)]
        [RegularExpression("^[INTRB2B / INTRB2C / INTRAB2B / INTRAB2C]")]
        public string sply_ty { get; set; }

        [Required]
        [Display(Name = "Total Nil rated outward supplies")]
        public double? nil_amt { get; set; }

        [Required]
        [Display(Name = "Total Exempted outward supplies")]
        public double? expt_amt { get; set; }

        [Required]
        [Display(Name = "Total Non GST outward supplies")]
        public double? ngsup_amt { get; set; }
    }

    public class NilRatedOutward
    {
        [Display(Name = "TaxPayer Action")]
        public string flag { get; set; }                    // This not match in table schema



        [Required]
        [Display(Name = "Nil Invoices")]
        public List<NilSupplyData> inv { get; set; }

        [Required]
        [Display(Name = "Invoice Check sum value ")]
        [MaxLength(64)]
        public string chksum { get; set; }
    }
}