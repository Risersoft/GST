using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Risersoft.API.GSTN.GSTR6
{
    
    public class Latefee

    {

                         
        [Display(Name = "tax amount for cgst")]
        public double cLamt { get; set; }
        
        [Display(Name = "tax amount for sgst")]
        public double sLamt { get; set; }

        
        [Display(Name= "debit number")]
        public double debitId { get; set; }
        [Display(Name = "Offet params for offset")]
        public Foroff foroffset { get; set; }
       
        [Display(Name = "Trasaction date.")]
      
        public string date { get; set; }
    }




    public class Foroff
    {
        [Display(Name = "Liability Id - Required for offsetting late fee. After offset this parameter will not come")]
        public double liab_id { get; set; }
        [Display(Name = "Transaction Code- Required for offsetting late fee.  After offset this parameter will not come")]
        public double tran_cd { get; set; }
    }
}
