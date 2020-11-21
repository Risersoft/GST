using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Risersoft.API.GSTN.GSTR3
{

    public class Txpyrc
    {

        [Required]
        [Display(Name = "List of inter state supplies received")]
        public List<Interdet> inter_det { get; set; }

       

        [Required]
        [Display(Name = "List of intra state supplies received")]
        public List<Intradet> intra_det { get; set; }

       
    }
    public class Interdet
    {

        [Required]
        [Display(Name = " Rate of Tax")]
        public double txrt { get; set; }

        [Required]
        [Display(Name = " taxable value/Net differential value ")]
        public double txval { get; set; }
        [Required]
        [Display(Name = " IGST Amount as per invoice")]
        public double iamt { get; set; }


        [Required]
        [Display(Name = "CESS amount as per invoice")]
        public double cess { get; set; }
    }
    public class Intradet
    {
        [Required]
        [Display(Name = " Rate of Tax")]
        public double txrt { get; set; }

        [Required]
        [Display(Name = " taxable value/Net differential value ")]
        public double txval { get; set; }
        [Required]
        [Display(Name = "CGST Amount as per invoice")]
        public double camt { get; set; }
        [Required]
        [Display(Name = "SGST Amount as per invoice")]
        public double samt { get; set; }


        [Required]
        [Display(Name = "CESS amount as per invoice")]
        public double cess { get; set; }

    }
}