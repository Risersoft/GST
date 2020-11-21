using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;


namespace GSTN.API.Library.Models.ITC
{
    public class GetITCInvoice
    {



        [Display(Name = "GSTIN of the taxpayer")]
        [MaxLength(15)]
        [MinLength(15)]
        [RegularExpression("^[0-9]{2}[a-zA-Z]{5}[0-9]{4}[a-zA-Z]{1}[1-9A-Za-z]{1}[Zz1-9A-Ja-j]{1}[0-9a-zA-Z]{1}$")]
        public string gstin { get; set; }

       
        [Display(Name = "Return Type")]
        [MaxLength(8)]
        public string rtn_typ { get; set; }

       
        [Display(Name = "Date of Filing, present only in case of ITC03-4A Return Type.")]
        public string dof { get; set; }

       
        [Display(Name = "Composition ARN, Present in case of ITC03-4A return Type. ARN of the application filed to opt composition")]
        [MaxLength(15)]
        public string comp_arn { get; set; }

       
        [Display(Name = "Date of Exemption, present only in case of ITC03-4B Return Type.")]
        public string doe { get; set; }

       
        [Display(Name = "List of Invoices")]
        public List<Invoice> invs { get; set; }
    }



    public class Invoice
    {
        [Required]
        [Display(Name = "Gstin of Supplier")]
        [MinLength(15)]
        [MaxLength(15)]
        [RegularExpression("^[0-9]{2}[a-zA-Z]{5}[0-9]{4}[a-zA-Z]{1}[1-9A-Za-z]{1}[Zz1-9A-Ja-j]{1}[0-9a-zA-Z]{1}|[0]{15}$")]
        public string cpty_r { get; set; }

        [Required]
        [Display(Name = "Registration under CX/ VAT of supplier")]
        [MaxLength(15)]
        public string cpty_ur { get; set; }

        [Display(Name = "Invoice Number")]
        [MaxLength(16)]
        public string inum { get; set; }

        [Display(Name = "Invoice Date")]
        [RegularExpression("^((0[1-9]|[12][0-9]|3[01])[-](0[1-9]|1[012])[-]((19|20)\\d\\d))$")]
        public string idt { get; set; }

        [Display(Name = "Items")]   
        public List<ItemDeat> itms { get; set; }
    }







    public class ItemDeat
    {
        [Required]
        [Display(Name = "Goods Type")]
        public string goods_ty { get; set; }


        [Required]
        [Display(Name = "Description of inputs held in stock")]
        public string desc { get; set; }

        [Required]
        [Display(Name = "Unit Quantity Code")]
        [MaxLength(16)]
        public string uqc { get; set; }

        [Required]
        [Display(Name = "Quantity")]
        public decimal qty { get; set; }
        [Required]
        [Display(Name = "Taxable value of Goods or Service as per invoice")]
        public decimal val { get; set; }


        [Required]
        [Display(Name = "Central tax")]
        public decimal tx_c { get; set; }

        [Required]
        [Display(Name = "State tax/UT Tax")]
        public decimal tx_s { get; set; }





        [Required]
        [Display(Name = "Integrated tax")]
        public decimal tx_i { get; set; }

        [Required]
        [Display(Name = "Cess")]
        public decimal tx_cs { get; set; }

        
    }
}
