using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Risersoft.API.GSTN.GSTR1;

namespace Risersoft.API.GSTN.GSTR6
{
    public class CDNInv


    {
        [Required]
        [Display(Name = "flag for accepting or rejecting a invoice")]
        [RegularExpression("^[M/D/A/R/P]")]
        public string flag { get; set; }




        [Required]
        [Display(Name = "Invoice checksum value")]
        [MaxLength(15)]
        [MinLength(1)]
        [RegularExpression("^[a-zA-Z0-9]+$")]
        public string chksum { get; set; }

        [Required]
        [Display(Name = "Invoice Number")]
        [MaxLength(16)]
        public string inum { get; set; }

        [Required]
        [Display(Name = "note type")]
        [RegularExpression("^[C/D]")]
        public string ntty { get; set; }


        
        [Display(Name = "Place Of Supply")]
        public string pos { get; set; }

        [Required]
        [Display(Name = "Invoice Date")]
        [RegularExpression("^((0[1-9]|[12][0-9]|3[01])[-](0[1-9]|1[012])[-]((19|20)\\d\\d))*$")]
        public string idt { get; set; }

      

       
        public List<B2Bitem> itms { get; set; }




        [Required]
        [Display(Name = "Credit note/debit note Number")]
        [MaxLength(16)]
        [RegularExpression("^[a-zA-Z0-9]+$")]
        public string nt_num { get; set; }


        [Required]
        [Display(Name = "Credit date")]
        [RegularExpression("^((0[1-9]|[12][0-9]|3[01])[-](0[1-9]|1[012])[-]((19|20)\\d\\d))*$")]
        public string nt_dt { get; set; }




        [Required]
        [Display(Name = "credit/debit note Uploader")]
        [RegularExpression("^[S/R]")]
        public string updby { get; set; }
    }
    public class CDNInvoice

    {

        [Required]                        
        [Display(Name = "Counter party GSTIN")]
        [MaxLength(15)]
        [MinLength(15)]
        [RegularExpression("^[a-zA-Z0-9]+$")]
        public string ctin { get; set; }

      
        [Required]
        [Display(Name= "Credit/Debit Notes  Details ")]
        public List<CDNInv> nt { get; set; }
    }
}
