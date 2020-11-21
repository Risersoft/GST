using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Risersoft.API.GSTN.GSTR2
{
    public class ImpSAItm


    {
        [Required]
        [Display(Name = "Serial no")]
        public double num { get; set; }
        
        [Display(Name = "SAC code")]
        public string sac { get; set; }

        [Required]
        [Display(Name = "Taxable value ")]
        public double txval { get; set; }
        
        [Display(Name = "IGST rate")]
        public double irt { get; set; }

        
        [Display(Name = "IGST amount")]
        public double iamt { get; set; }

        
        [Display(Name = "CESS rate")]
        public double csrt { get; set; }
       
        [Display(Name = "CESS amount")]
        public double csamt { get; set; }
        [Required]
        [Display(Name = "Eligibility for ITC")]
        [MaxLength(2)]
        public string elg { get; set; }
        
        [Display(Name = "Total IGST available as ITC ")]
        public double tx_i { get; set; }




        
        [Display(Name = "ITC available this month")]
        public double tc_i { get; set; }

       
        [Display(Name = "Total CESS available as ITC ")]
        public double tx_cs { get; set; }


        [Required]
        [Display(Name = "ITC available as CESS this month")]
        public double tc_cs { get; set; }


    }
    public class ImpSA
    {

        [Required]
        [Display(Name = "Invoice number")]
        [MaxLength(10)]
        [RegularExpression("^[a-zA-Z0-9]+$")]
        public string inum { get; set; }

        [Required]
        [Display(Name = "Invoice date")]
        [RegularExpression("^((0[1-9]|[12][0-9]|3[01])[-](0[1-9]|1[012])[-]((19|20)\\d\\d))*$")]
        public string idt { get; set; }

        [Required]
        [Display(Name = "Invoice Value")]
        public double ival { get; set; }

        [Required]
        [Display(Name = "Original Invoice Number")]
        [MaxLength(50)]
        public string oinum { get; set; }

        [Required]
        [Display(Name = "Original Invoice Date")]
        [RegularExpression("^((0[1-9]|[12][0-9]|3[01])[-](0[1-9]|1[012])[-]((19|20)\\d\\d))*$")]
        public string oidt { get; set; }

        [Required]
        [Display(Name = "itms")]
        public List<ImpSAItm> itms { get; set; }


        [Display(Name = "Invoice Check sum value")]
        [MaxLength(15)]
        public string chksum { get; set; }
    }
}
