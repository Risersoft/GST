using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Risersoft.API.GSTN.GSTR3
{
    public class Submitliabilitiesinterest
    {


        [Display(Name = "Interest Liability")]
        public IntLiability intr_liab { get; set; }

       
    }
    public class IntLiability
    {
       
        [Display(Name = "On account of other ITC reversal")]
        public Intdet oth_itc_rvl_usr { get; set; }

       
        [Display(Name = "Interest liability carry forward")]
        public Intdet intr_liab_fwd_usr { get; set; }

        
        [Display(Name = "Interest liability carry forward")]
        public Intdet del_pymt_tx_usr { get; set; }

        
        [Display(Name = "Other Interest")]
        public Intdet other_intr { get; set; }

    }



    public class Intdet
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