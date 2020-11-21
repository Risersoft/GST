using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;

namespace Risersoft.API.GSTN.GSTR3
{
    
   
    public class SetOffsetLiability
    {

        [Required]
        [Display(Name = "Tax Paid")]
        public TaxPaid tx_offset { get; set; }

        [Required]
        [Display(Name = "Tax Paid")]
        public TaxPaid intr_offset { get; set; }


    }

    public class TaxPaid
    {
        [Required]
        [Display(Name = "List of Paid Through Cash")]
        public List<pdcas_det> pdcas { get; set; }


        [Required]
        [Display(Name = "List of Paid Through Credit")]
        public List<pdcr_det> pdcr { get; set; }



    }
    public class pdcas_det
    {
        [Required]
        [Display(Name = "Igst paid")]
        public double ipd { get; set; }
        [Required]
        [Display(Name = "Cgst paid")]
        public double cpd { get; set; }




        [Required]
        [Display(Name = "Sgst paid ")]
        public double spd { get; set; }
        [Required]
        [Display(Name = "CESS Paid")]
        public double cspd { get; set; }

    }

    public class pdcr_det
    {



        [Required]
        [Display(Name = "IGST paid using igst")]
        public double i_pdi { get; set; }
        [Required]
        [Display(Name = "IGST paid using Cgst")]
        public double i_pdc { get; set; }

        [Required]
        [Display(Name = "IGST paid using Sgst")]
        public double i_ds { get; set; }
        [Required]
        [Display(Name = "CGST paid using igst")]
        public double c_pdi { get; set; }
        [Required]
        [Display(Name = "CGST paid using cgst")]
        public double c_pdc { get; set; }

        [Required]
        [Display(Name = "SGST paid using igst ")]
        public double s_pdi { get; set; }
        [Required]
        [Display(Name = "SGST paid using sgst ")]
        public double s_pds { get; set; }
        [Required]
        [Display(Name = "Cess paid using cess")]
        public double cs_pdcs { get; set; }

    }
}