using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;

namespace Risersoft.API.GSTN.GSTR2
{


    public class B2bAInv
    {

        [Required]
        [Display(Name = "flag for accepting or rejecting a invoice")]
        public string flag { get; set; }

        [Required]
        [Display(Name = "Supplier Invoice Number")]
        [MaxLength(50)]
        public string inum { get; set; }


        [Required]
        [Display(Name = "Supplier Invoice Date")]
        [RegularExpression("^((0[1-9]|[12][0-9]|3[01])[-](0[1-9]|1[012])[-]((19|20)\\d\\d))*$")]
        public string idt { get; set; }

        [Required]
        [Display(Name = "Original invoice no")]
        [MaxLength(50)]
        [RegularExpression("^[a-zA-Z0-9]+$")]
        public string oinum { get; set; }

        [Required]
        [Display(Name = "Original date")]
        [RegularExpression("^((0[1-9]|[12][0-9]|3[01])[-](0[1-9]|1[012])[-]((19|20)\\d\\d))*$")]
        public string oidt { get; set; }


        [Required]
        [Display(Name = "Supplier Invoice Value")]
        public double val { get; set; }
        [Required]
        [Display(Name = "Place of supply")]
        [MaxLength(2)]
        public string pos { get; set; }
        [Required]
        [Display(Name = "Reverse Charge")]
        public string rchrg { get; set; }
        [Required]
        [Display(Name = "Invoice Uploader")]
        public string updby { get; set; }
        [Required]
        public List<B2bcitem> itms { get; set; }



        [Required]
        [Display(Name = "Invoice Check sum value")]
        [MaxLength(64)]
        public string chksum { get; set; }

    }

    public class B2bcitem
    {
        [Required]
        [Display(Name = "Serial no")]
        public double num { get; set; }
        [Required]
        [Display(Name = "itc Details")]
        public Bitc itc { get; set; }


        [Required]
        [Display(Name = "Item Details")]
        public Bitem itm_det { get; set; }
    }



   public class Bitc {



        
        [Display(Name = "Total Tax available as ITC IGST Amount")]
        public double tx_i { get; set; }
       
        [Display(Name = "Total Tax available as ITC CGST Amount")]
        public double tx_c { get; set; }
       
        [Display(Name = "Total Tax available as ITC SGST Amount")]
        public double tx_s { get; set; }
       
        [Display(Name = "Total Tax available as ITC CESS Amount")]
        public double tx_cs { get; set; }
        
        [Display(Name = "Total Input Tax Credit available for claim this month based on the Invoices uploaded(IGST Amount)")]
        public double tc_i { get; set; }
       
        [Display(Name = "Total Input Tax Credit available for claim this month based on the Invoices uploaded(CGST Amount)")]
        public double tc_c { get; set; }
       
        [Display(Name = "Total Input Tax Credit available for claim this month based on the Invoices uploaded(SGST Amount)")]
        public double tc_s { get; set; }
       


        [Display(Name = "Total Input Tax Credit available for claim this month based on the Invoices uploaded(SGST Amount)")]
        public double tc_cs { get; set; }
        [Required]
        [Display(Name = "Eligibility")]
        [MaxLength(2)]
        public string elg { get; set; }
    }


    public class Bitem
    {


        [Required]
        [Display(Name = "Identifier if Goods or Services")]
        public string ty { get; set; }
       
        [Display(Name = "HSN or SAC of Goods or Services as per Invoice line items")]
        public string hsn_sc { get; set; }


        [Required]
        [Display(Name = "Taxable value of Goods or Service as per invoice")]
        public double txval { get; set; }

       
        [Display(Name = "IGST Rate as per invoice")]
        public double irt { get; set; }

        
        [Display(Name = "IGST Amount as per invoice")]
        public double iamt { get; set; }

       
        [Display(Name = "CGST Rate as per invoice")]
        public double crt { get; set; }


        
        [Display(Name = "CGST Amount as per invoice")]
        public double camt { get; set; }

        
        [Display(Name = "SGST Rate as per invoice")]
        public double srt { get; set; }

        

        [Display(Name = "SGST Amount as per invoice")]
        public double samt { get; set; }
       
        [Display(Name = "CESS Rate as per invoice")]
        public double csrt { get; set; }
        
        [Display(Name = "CESS Amount as per invoice")]
        public double csamt { get; set; }

    }




    public class B2bAInward
    {

        [Required]
        [Display(Name = "GSTIN/UID of the Receiver taxpayer/UN, Govt Bodies")]
        public string ctin { get; set; }
        [Required]
        [Display(Name = "counter party Filing Status")]
        public string cfs { get; set; }

        [Required]
        [Display(Name = "Invoice Details")]
        public List<B2bAInv> inv { get; set; }
    }


}