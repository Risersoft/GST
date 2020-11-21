using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;

namespace Risersoft.API.GSTN.GSTR4
{





     public class ItemsDet
    {
        [Required]
        [Display(Name = "Gross Advance Paid")]
        public double adamt { get; set; }

        [Required]
        [Display(Name = "Rate")]
        public double rt { get; set; }



        [Display(Name = "IGST Amount as per invoice")]
        public double? iamt { get; set; }

        [Display(Name = "CGST Amount as per invoice")]
        public double? camt { get; set; }



        [Display(Name = "SGST Amount as per invoice")]
        public double? samt { get; set; }

        [Display(Name = "Cess Amount as per invoice")]
        public double? csamt { get; set; }



        [Required]
        [Display(Name = "Item Number")]
        public double num { get; set; }
    }
   
    public class GetAdvancePaid
    {

        [Required]
        [Display(Name = "Invoice Check sum value ")]
        [MaxLength(64)]
        public string chksum { get; set; }


        [Required]
        [Display(Name = "Place of Supply ")]
        [MaxLength(2)]
        public string pos { get; set; }
        [Required]
        [Display(Name = "Supply Type of invoice")]
        [RegularExpression("^[INTER/INTRA]")]
        public string sply_ty { get; set; }


       
        [Required]
        [Display(Name = "Item Details")]
        public List<ItemsDet> itms { get; set; }

      
    }

}