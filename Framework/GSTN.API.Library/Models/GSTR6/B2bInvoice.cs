using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;

namespace Risersoft.API.GSTN.GSTR6
{
    public class ItmDetn
    {

        [Required]
        [Display(Name = "rate as per invoice")]
        public double? rt { get; set; }
        
        [Required]
        [Display(Name = "Taxable value of Goods or Service as per invoice")]
        public double txval { get; set; }
        [Required]
        [Display(Name = "IGST Amount as per invoice")]
        public double? iamt { get; set; }

        [Required]
        [Display(Name = "CGST Amount as per invoice")]
        public double? camt { get; set; }
        [Required]
        [Display(Name = "SGST Amount as per invoice")]
        public double? samt { get; set; }
        [Required]
        [Display(Name = "Cess Amount as per invoice")]
        public double? csamt { get; set; }
    }

    public class item
    {
        [Required]
        [Display(Name = "Serial no")]
        public int num { get; set; }
        [Required]
        [Display(Name = "Item Details")]
        public ItmDetn itm_det { get; set; }

    }
    public class Inv
    {
        [Required]   
        [Display(Name = "flag for accepting or rejecting  or modifying a invoice")]
        public string flag { get; set; }                    


        [Required]
        [Display(Name = "Invoice Uploader")]
        public string updby { get; set; }





        [Required]
        [Display(Name = "Supplier Invoice Number")]
        [MaxLength(16)]
        [RegularExpression("^[a-zA-Z0-9]+$")]
        public string inum { get; set; }





        [Required]
        [Display(Name = "Invoice checksum value")]
        [MaxLength(64)]
        [RegularExpression("^[a-zA-Z0-9]+$")]
        public string chksum { get; set; }

        [Required]
        [Display(Name = "Supplier Invoice Date")]
        [RegularExpression("^((0[1-9]|[12][0-9]|3[01])[-](0[1-9]|1[012])[-]((19|20)\\d\\d))$")]
        public string idt { get; set; }

        
        [Required]
        [Display(Name = "Supplier Invoice Value")]
        public double val { get; set; }

       

        [Required]
        [Display(Name = "Items")]
        public List<item> itms { get; set; }

        
    }

    public class B2bInvoice
    {

        [Required]
        [Display(Name = "GSTIN/UID of the Supplier taxpayer")]
        [MaxLength(15)]
        [MinLength(15)]
        [RegularExpression("^[a-zA-Z0-9]+$")]
        public string ctin { get; set; }

        [Required]
        [Display(Name = "counter party Filing Status")]
        public string cfs { get; set; }



        [Required]
        [Display(Name = "Invoice Details")]
        public List<Inv> inv { get; set; }
    }

}