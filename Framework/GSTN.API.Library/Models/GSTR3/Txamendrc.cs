using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Risersoft.API.GSTN.GSTR3
{
    public class Txamendrc
    {


        [Required]
        [Display(Name = "List of inter state supplies received")]
        public List<Interdet> inter_det { get; set; }



        [Required]
        [Display(Name = "List of intra state supplies received")]
        public List<Intradet> intra_det { get; set; }

    }

    
    }