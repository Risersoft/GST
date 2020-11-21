using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
namespace Risersoft.API.GSTN.GSTR2
{
    public class CdnAInward
    {
        [Required]
        [Display(Name = "Counter party GSTIN")]
        [MaxLength(15)]
        [MinLength(15)]
        [RegularExpression("^[a-zA-Z0-9]+$")]
        public string ctin { get; set; }

        [Required]
        [Display(Name = "counter party Filing Status")]
        public string cfs { get; set; }

        [Required]
        [Display(Name = "Credit/Debit Notes")]
        public List<CdnInov> nt { get; set; }
    }
    public class Itcitm
    {
        [Required]
        [Display(Name = "Total Tax available as ITC CGST Amount")]
        public double tx_c { get; set; }

        [Required]
        [Display(Name = "Total Tax available as ITC IGST Amount")]
        public double tx_i { get; set; }

        [Required]
        [Display(Name = "Total Tax available as ITC SGST Amount")]
        public double tx_s { get; set; }

        [Required]
        [Display(Name = "Total Tax available as ITC CESS Amount")]
        public double tx_cs { get; set; }

        [Required]
        [Display(Name = "Total Input Tax Credit available for claim this month based on the Invoices uploaded(IGST Amount)")]
        public double tc_i { get; set; }
        [Required]
        [Display(Name = "Total Input Tax Credit available for claim this month based on the Invoices uploaded(CGST Amount)")]
        public double tc_c { get; set; }

        [Required]
        [Display(Name = "Total Input Tax Credit available for claim this month based on the Invoices uploaded(SGST Amount)")]
        public double tc_s { get; set; }

        [Required]
        [Display(Name = "Total Input Tax Credit available for claim this month based on the Invoices uploaded(SGST Amount)")]
        public double tc_cs { get; set; }
        [Required]
        [Display(Name = "Eligiblity")]
        [MaxLength(2)]
        public string elg { get; set; }
    }
        public class CdnInov { 
        [Required]
        [Display(Name = "flag for accepting or rejecting a invoice")]
        [MaxLength(1)]
        public string flag { get; set; }



        [Required]
        [Display(Name = "Note type")]
        [MaxLength(1)]
        [MinLength(1)]
        public string ntty { get; set; }


        [Required]
        [Display(Name = "Debit/Credit Note number")]
        [MaxLength(50)]
        public string nt_num { get; set; }

        [Required]
        [Display(Name = "Credit date")]
        [RegularExpression("^((0[1-9]|[12][0-9]|3[01])[-](0[1-9]|1[012])[-]((19|20)\\d\\d))*$")]
        public string nt_dt { get; set; }

        [Required]
        [Display(Name = "Original Debit/Credit note number")]
        [MaxLength(50)]
        public string ont_num { get; set; }

        [Required]
        [Display(Name = "Original Debit/Credit date")]
        [RegularExpression("^((0[1-9]|[12][0-9]|3[01])[-](0[1-9]|1[012])[-]((19|20)\\d\\d))*$")]
        public string ont_dt { get; set; }

        [Required]
        [Display(Name = "rsn")]
        public string rsn { get; set; }

        [Required]
        [Display(Name = "Invoice Number")]
        [MaxLength(50)]
        [RegularExpression("^[a-zA-Z0-9]+$")]
        public string inum { get; set; }

        [Required]
        [Display(Name = "Invoice Date")]
        [RegularExpression("^((0[1-9]|[12][0-9]|3[01])[-](0[1-9]|1[012])[-]((19|20)\\d\\d))*$")]
        public string idt { get; set; }

        [Required]
        [Display(Name = "val")]
        public double val { get; set; }

        [Required]
        [Display(Name = "IGST Rate as per invoice")]
        public double irt { get; set; }

        [Required]
        [Display(Name = "iamt")]
        public double iamt { get; set; }

        [Required]
        [Display(Name = "CGST Rate as per invoice")]
        public int crt { get; set; }

        [Required]
        [Display(Name = "camt")]
        public int camt { get; set; }

        [Required]
        [Display(Name = "SGST Rate as per invoice")]
        public int srt { get; set; }

        [Required]
        [Display(Name = "CESS Rate as per invoice")]
        public int csrt { get; set; }


        [Required]
        [Display(Name = "CESS Amount as per invoice")]
        public int csamt { get; set; }

        [Required]
        [Display(Name = "Invoice Uploader")]
        public string updby { get; set; }

        [Required]
        [Display(Name = "Reverse Charge")]
        public string rchrg { get; set; }

        [Required]
        [Display(Name = "samt")]
        public int samt { get; set; }

       
        [Required]
        [Display(Name = "itc")]
        public Itcitm itc { get; set; }


        [Required]
        [Display(Name = "Invoice Check sum value")]
        [MaxLength(64)]
        public string chksum { get; set; }
    }


}