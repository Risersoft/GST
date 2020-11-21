using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Risersoft.API.GSTN.GSTR3
{
    public class InterestLiability
    {
       

        

        [Required]
        [Display(Name = "Output liability on mismatch ")]
        public Interstdet out_liab_mis { get; set; }

        [Required]
        [Display(Name = "ITC claimed on mismatched invoice")]
        public Interstdet itc_clm_mis { get; set; }

        [Required]
        [Display(Name = "On account of other ITC reversal")]
        public Interstdet oth_itc_rvl { get; set; }




        [Required]
        [Display(Name = "Undue excess claims or excess reduction")]
        public Interstdet exc_clm_red { get; set; }

        [Required]
        [Display(Name = "Credit of interest on rectification of mismatch ")]
        public Interstdet cr_intr_mis { get; set; }


        [Required]
        [Display(Name = "Interest liability carry forward")]
        public Interstdet intr_liab_fwd { get; set; }

        [Required]
        [Display(Name = "Interest liability carry forward")]
        public Interstdet del_pymt_tx { get; set; }

        [Required]
        [Display(Name = "IGST on imports")]
        public Interstdet a { get; set; }




        [Required]
        [Display(Name = "Total interest liability")]
        public Interstdet tot_intr_liab { get; set; }


       
    }



    public class Interstdet
    {


        [Required]
        [Display(Name = "Integrated Amount")]
        public double iamt { get; set; }

        [Required]
        [Display(Name = "Central Amount")]
        public double camt { get; set; }

        [Required]
        [Display(Name = "State / Union Territory  Amount")]
        public double samt { get; set; }

        [Required]
        [Display(Name = "Cess Amount")]
        public double cess { get; set; }
    }
}