using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Risersoft.API.GSTN.GSTR3
{
    public class Debitledger
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
        [Display(Name = "Cess paid")]
        public double cspd { get; set; }

        [Required]
        [Display(Name = "Interest Paid in IGST head")]
        public double i_intr_pd { get; set; }




        [Required]
        [Display(Name = "Interest Paid in CGST head")]
        public double c_intr_pd { get; set; }





        [Required]
        [Display(Name = "Interest Paid in SGST head")]
        public double s_intr_pd { get; set; }

        [Required]
        [Display(Name = "Interest Paid in CESS head")]
        public double cs_intr_pd { get; set; }
        [Required]
        [Display(Name = "Late Fee payable in SGST head")]
        public double s_lfee_pd { get; set; }
        [Required]
        [Display(Name = "Late Fee payable in CGST head")]
        public double c_lfee_pd { get; set; }

        [Required]
        [Display(Name = "Igst paid through IGST")]
        public double i_pdi { get; set; }

        [Required]
        [Display(Name = "Igst paid through CGST")]
        public double i_pdc { get; set; }

        [Required]
        [Display(Name = "Igst paid through SGST")]
        public double i_pds { get; set; }

        [Required]
        [Display(Name = "Cgst paid through IGST")]
        public double c_pdi { get; set; }

        [Required]
        [Display(Name = "Cgst paid through CGST")]
        public double c_pdc { get; set; }

        [Required]
        [Display(Name = "Sgst paid through IGST")]
        public double s_pdi { get; set; }

        [Required]
        [Display(Name = "Sgst paid through SGST")]
        public double s_pds { get; set; }

        [Required]
        [Display(Name = "Cess paid through Cess")]
        public double cs_pdcs { get; set; }
    }
}