using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
namespace Risersoft.API.GSTN.GSTR2
{
    public class TxliItm
    {

        
        
        
        [Display(Name = "Item NumberItem Number")]
        public double num { get; set; }

        [Required]
        [Display(Name = "Rate of Invoice")]
        public double rt { get; set; }

       
        [Display(Name = "IGST Amount as per invoice")]
        public double iamt { get; set; }

        [Required]
        [Display(Name = "Gross Advance Paid")]
        public double adamt { get; set; }

        
        [Display(Name = "CGST Amount as per invoice")]
        public double camt { get; set; }

        [Display(Name = "SGST Amount as per invoice")]
        public double samt { get; set; }

        [Display(Name = "Cess Amount as per invoice")]
        public double csamt { get; set; }
    }

    public class Txli
    {


       
        [Display(Name = "Place of Supply")]
        [MaxLength(2)]
        public string pos { get; set; }
        [Required]
        [Display(Name = "Supply Type")]
        public string sply_ty { get; set; }


        [Required]
        [Display(Name = "Item Details")]
        public List<TxliItm> itms { get; set; }
        [Required]
        [Display(Name = "Checksum Value")]
        [RegularExpression("^[a-zA-Z0-9]+$")]
        [MaxLength(64)]
        public string chksum { get; set; }
    }


}