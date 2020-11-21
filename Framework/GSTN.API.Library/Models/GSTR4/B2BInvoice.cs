using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;

namespace Risersoft.API.GSTN.GSTR4
{
    public class ItmDet
    {

        
        
        [Required]
        [Display(Name = "Taxable value of Goods or Service as per invoice")]
        public double txval { get; set; }

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
    }

    public class B2Bitem
    {
        [Required]
        [Display(Name = "Serial no")]
        public int num { get; set; }
        [Required]
        [Display(Name = "Item Details")]
        public ItmDet itm_det { get; set; }

    }

    public class B2BInv
    {



        [Display(Name = "Invoice Status")]
        public string flag { get; set; }                   


        [Required]
        [Display(Name = "Uploaded by")]
        public string updby { get; set; }

        [Required]
        [Display(Name = "Status of counter party")]
        public string cflag { get; set; }



        [Required]
        [Display(Name = "Supplier Invoice Number")]
        [MaxLength(16)]
        public string inum { get; set; }

        [Display(Name = "Invoice checksum value")]
        [MaxLength(64)]
        [RegularExpression("^[a-zA-Z0-9]+$")]
        public string chksum { get; set; }

        [Required]
        [Display(Name = "Supplier Invoice Date")]
        [RegularExpression("^((0[1-9]|[12][0-9]|3[01])[-](0[1-9]|1[012])[-]((19|20)\\d\\d))$")]
        public string idt { get; set; }

        [Required]
        [Display(Name = "Invoice Type")]
        public string inv_typ { get; set; }

        [Required]
        [Display(Name = "Supplier Invoice Value")]
        public double val { get; set; }

        [Required]
        [Display(Name = "Reverse Charge")]
        public string rchrg { get; set; }

        [Required]
        [Display(Name = "Actual Place of Supply")]
        [MaxLength(2)]
        [MinLength(2)]
        public string pos { get; set; }

        [Display(Name = "Original Period")]
        [RegularExpression("^((0[1-9]|1[012])[-]((19|20)\\d\\d))*$")]
        public string opd { get; set; }                              

        [Required]
        [Display(Name = "Items")]
        public List<B2Bitem> itms { get; set; }


    }




    public class B2BInvoice
    {

        [Required]
        [Display(Name = "GSTIN/UID of the Receiver taxpayer/UN, Govt Bodies")]
        [MaxLength(15)]
        [MinLength(15)]
        [RegularExpression("^[a-zA-Z0-9._\\s]+$")]
        public string ctin { get; set; }


        [Required]
        [Display(Name = "GSTR2 filing status of counter party")]
        public string cfs { get; set; }

        [Required]
        [Display(Name = "Invoice Details")]
        public List<B2BInv> inv { get; set; }
    }

}