using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;

namespace Risersoft.API.GSTN.GSTR8
{
    
   
    public class B2BUnregisteredInvoice
    {

        [Required]
        [Display(Name = "B2BUR Invoices")]
        public List<B2BUR> b2bur { get; set; }
    }
    public class B2BUR
    {
        [Required]
        [Display(Name = "Invoice Details ")]
        public List<InvItem> inv { get; set; }
    }
    public class InvItem
    {
        [Required]
        [Display(Name = "Invoice Check sum value ")]
        [MaxLength(64)]
        public string chksum { get; set; }

        [Required]
        [Display(Name = "Supplier Invoice Number")]
        [MaxLength(16)]
        public double inum { get; set; }

        [Required]
        [Display(Name = "Supplier Invoice Date")]
        [RegularExpression("^((0[1-9]|[12][0-9]|3[01])[-](0[1-9]|1[012])[-]((19|20)\\d\\d))*$")]
        public string idt { get; set; }

        [Required]
        [Display(Name = "Supplier Invoice Value")]
        public double val { get; set; }
        [Required]
        [Display(Name = "Supply Type")]
        public string sply_ty { get; set; }






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

    }
    public class ItmDetU
    {


        [Required]
        [Display(Name = "Taxable value of Goods or Service as per invoice")]
        public double txval { get; set; }

        [Required]
        [Display(Name = "Tax Rate")]
        public double rt { get; set; }




        [Display(Name = "CGST Amount as per invoice")]
        public double camt { get; set; }


        [Display(Name = "SGST Amount as per invoice")]
        public double samt { get; set; }


        [Display(Name = "CESS Amount as per invoice")]
        public double csamt { get; set; }
        [Display(Name = "IGST Amount as per invoice")]
        public double iamt { get; set; }

    }
}