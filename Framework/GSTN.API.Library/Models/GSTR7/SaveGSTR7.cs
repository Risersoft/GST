using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using GSTN.API.Library.Models;

namespace Risersoft.API.GSTN.GSTR7
{
   
   
    public class SaveGSTR7:GSTRBase
    {


        [Display(Name = "TDS details")]
        public GetTDSDet tds { get; set; }


        [Display(Name = "Amended TDS details")]
        public GetTDSADetails tdsa { get; set; }

    }

  

}