using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;

namespace Risersoft.API.GSTN.GSTR1
{
    public class TxpOutward
    {
        [Display(Name = "TaxPayer Action")]
        public string flag { get; set; }                    // This not match in table schema



        [Required]
        [Display(Name = "Nature of Supply")]
        [MaxLength(8)]
        [RegularExpression("^[INTER/ INTRA]")]
        public string sply_ty { get; set; }


        [Required]
        [Display(Name = "Actual Place of Supply")]
        [MaxLength(2)]
        [MinLength(2)]
        public string pos { get; set; }


        [Display(Name = "Differential percentage")]
        public double? diff_percent { get; set; }

        [Required]
        [Display(Name ="Items Details")]
        public List<ATitemdtl> itms { get; set; }

        [Required]
        [Display(Name = "Invoice checksum value")]
        [MaxLength(64)]
        [MinLength(1)]
        [RegularExpression("^[a-zA-Z0-9]+$")]
        public string chksum { get; set; }
    }

}