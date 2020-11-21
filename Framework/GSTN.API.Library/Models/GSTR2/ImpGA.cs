using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Risersoft.API.GSTN.GSTR2
{



    public class ImpGItem
    {
        [Required]
        [Display(Name = "Serial no")]
        public double num { get; set; }
       
        [Display(Name = "HSN code")]
        public string hsn { get; set; }

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


        
        [Display(Name = "ITC available as CESS this month")]
        public double tc_cs { get; set; }

    }
    public class ImpGA
    {

        [Required]
        [Display(Name = "Bill of Entry Number")]
        [MaxLength(50)]
        public int boe_num { get; set; }

        [Required]
        [Display(Name = "Original Bill of Entry Number")]
        [MaxLength(50)]
        public int oboe_num { get; set; }   

        [Required]
        [Display(Name = "Bill of Entry Date")]
        [RegularExpression("^((0[1-9]|[12][0-9]|3[01])[-](0[1-9]|1[012])[-]((19|20)\\d\\d))*$")]
        public string boe_dt { get; set; }


        [Required]
        [Display(Name = "Original Bill of Entry Date")]
        [RegularExpression("^((0[1-9]|[12][0-9]|3[01])[-](0[1-9]|1[012])[-]((19|20)\\d\\d))*$")]
        public string oboe_dt { get; set; } 



        [Required]
        [Display(Name = "Port Code")]
        public string port_code { get; set; }

        [Required]
        [Display(Name = "Bill of Entry Value")]
        public double boe_val { get; set; }

        [Required]
        [Display(Name = "itms")]
        public List<ImpGItem> itms { get; set; }


        [Display(Name = "Invoice Check sum value")]
        [MaxLength(15)]
        public string chksum { get; set; }

    }
}
