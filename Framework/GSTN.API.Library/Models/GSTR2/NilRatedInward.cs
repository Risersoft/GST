using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
namespace Risersoft.API.GSTN.GSTR2
{

    public class NilItem
    {
       



        [Display(Name = "Value of supplies received from Compounding Dealer")]
        public double cpddr { get; set; }

       
        [Display(Name = "Value of exempted supplies received ")]
        public double exptdsply { get; set; }

      
        [Display(Name = "Total Non GST outward supplies")]
        public double ngsply { get; set; }

       
        [Display(Name = "Nil Rated Supply")]
        public double nilsply { get; set; }

    }

    public class NilSupplyData
    {

     
        
        [Display(Name = "Inter state supplies")]
        public NilItem Inter { get; set; }

       
        [Display(Name = "Intra state supplies")]
        public NilItem Intra { get; set; }

        [Required]
        [Display(Name = "Invoice Check sum value ")]
        [MaxLength(15)]
        [RegularExpression("^[a-zA-Z0-9]+$")]
        public string chksum { get; set; }

       
    }

    public class NilRatedInward
    {

        [Required]
        [Display(Name = "Nil Supplies")]
        public List<NilSupplyData> nil_supplies { get; set; }

    }
}