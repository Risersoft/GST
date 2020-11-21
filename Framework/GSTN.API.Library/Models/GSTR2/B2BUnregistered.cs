using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
namespace Risersoft.API.GSTN.GSTR2
{
    
    public class B2BURInward
    {
        [Required]
        [Display(Name = "Invoice Details ")]
        public List<B2BURInv> inv { get; set; }
    }

    public class B2BURInv
    {
        [Required]
        [Display(Name = "Invoice Check sum value ")]
        [MaxLength(64)]
        public string chksum { get; set;}

        [Required]
        [Display(Name = "Supplier Invoice Number")]
        [MaxLength(16)]
        public string inum { get; set; }

        [Required]
        [Display(Name = "Supplier Invoice Date")]
        [RegularExpression("^((0[1-9]|[12][0-9]|3[01])[-](0[1-9]|1[012])[-]((19|20)\\d\\d))*$")]
        public string idt { get; set; }

        [Required]
        [Display(Name= "Supplier Invoice Value")]
        public double val { get; set; }
        [Required]
        [Display(Name = "Supply Type")]
        public string sply_ty { get; set;}






        [Required]
        [Display(Name = "Point of sale")]
       
        public string pos { get; set; }
        [Required]
        [Display(Name = "Items")]
        public List<Item> itms { get; set; }
    }
    public class Item
    {
        [Required]
        [Display(Name = "item number")]
        public int num { get; set; }

        [Required]
        [Display(Name = "details of the items of invoice")]
        public ItmDetU itm_det { get; set; }

        [Required]
        [Display(Name = "indirect tax component")]
        public ItcU itc { get; set; }
    }
    public class ItmDetU
    {


        [Required]
        [Display(Name = "Taxable value of Goods or Service as per invoice")]
        public double? txval { get; set; }

        [Required]
        [Display(Name = "Tax Rate")]
        public double? rt { get; set; }



        [Display(Name = "IGST Amount as per invoice")]
        public double? iamt { get; set; }
        [Display(Name = "CGST Amount as per invoice")]
        public double? camt { get; set; }

       
        [Display(Name = "SGST Amount as per invoice")]
        public double? samt { get; set; }

       
        [Display(Name = "CESS Amount as per invoice")]
        public double? csamt { get; set; }

    }

    public class ItcU
    {

        
        [Display(Name = "Total Tax available as ITC CGST Amount")]
        public double? tx_c { get; set; }

        
        [Display(Name = "Total Tax available as ITC IGST Amount")]
        public double? tx_i { get; set; }

       
        [Display(Name = "Total Tax available as ITC SGST Amount")]
        public double? tx_s { get; set; }

        
        [Display(Name = "Total Tax available as ITC CESS Amount")]
        public double? tx_cs { get; set; }


        [Required]
        [Display(Name = "Eligiblity")]
        [MaxLength(2)]
        public string elg { get; set; }

    }

}