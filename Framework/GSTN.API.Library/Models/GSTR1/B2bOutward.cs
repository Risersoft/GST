using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;

namespace Risersoft.API.GSTN.GSTR1
{
    public class ItmDet
    {

        [Required]
        [Display(Name = "rate as per invoice")]
        public double rt { get; set; }
        
        [Required]
        [Display(Name = "Taxable value of Goods or Service as per invoice")]
        public double txval { get; set; }

        [Required]
        [Display(Name = "IGST Amount as per invoice")]
        public double iamt { get; set; }
        
        [Required]
        [Display(Name = "CGST Amount as per invoice")]
        public double camt { get; set; }
        [Required]
        [Display(Name = "SGST Amount as per invoice")]
        public double samt { get; set; }


        [Required]
        [Display(Name = "Cess Amount as per invoice")]
        public double csamt { get; set; }
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
        
        [Display(Name = "TaxPayer Action")]
        public string flag { get; set; }                    // This not match in table schema


        [Required]
        [Display(Name = "Uploaded by")]
        public string updby { get; set; }

        [Required]
        [Display(Name = "Status of counter party")]
        public string cflag { get; set; }

        [Display(Name = "EcomOperator or Ecom Tin")]
        [MaxLength(15)]
        [MinLength(15)]
        [RegularExpression("^[a-zA-Z0-9._\\s]+$")]
        public string etin { get; set; }

        [Required]
        [Display(Name = "Supplier Invoice Number")]
        [MaxLength(16)]
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
        [RegularExpression("^(((19|20)\\d\\d)-(0[1-9]|1[012]))$")]
        public string opd { get; set; }                                // This not match in table schema

        [Required]
        [Display(Name = "Items")]
        public List<B2Bitem> itms { get; set; }

        [Display(Name = "Differential percentage")]
        public double? diff_percent { get; set; }


    }

    public class B2bOutward
    {

        [Required]
        [Display(Name = "GSTIN/UID of the Receiver taxpayer/UN,Govt Bodies")]
        [MaxLength(15)]
        [MinLength(15)]
        [RegularExpression("^[a-zA-Z0-9._\\s]+$")]
        public string ctin { get; set; }


        public string error_cd { get; set; }
        public string error_msg { get; set; }


        [Required]
        [Display(Name = "GSTR2 filing status of counter party")]
      
        public string cfs { get; set; }

        [Required]
        [Display(Name = "Invoice Details")]
        public List<B2BInv> inv { get; set; }
    }

}