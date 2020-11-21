using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using GSTN.API.Library.Models;

namespace Risersoft.API.GSTN.ITC4
{
    public class SaveITC4:GSTRBase
    {


        [Display(Name = "ITC04 Invoices")]
        public List<M2JWInvoice> m2jw { get; set; }

        [Display(Name = "ITC04 Invoices")]
        public List<JW2MInvoice> jw2m { get; set; }

    }


    public class M2JWInvoice
    {
     
        [Display(Name = "Tax payer action")]
        [RegularExpression("^[D]$")]
        public string flag { get; set; }


        [Display(Name = "GSTIN of Job worker, if registered. This parameter won't come in JSON if Job worker is unregistered.")]
        public string ctin { get; set; }

        [Required]
        [Display(Name = "Job worker's State Code. This parameter is mandatory for both registererd and unregistered Job worker")]   
        public string jw_stcd { get; set; }



        [Required]
        [Display(Name = "Challan Number")]
        public string chnum { get; set; }

        [Required]
        [Display(Name = "Challan Date")]
        [RegularExpression("^((0[1-9]|[12][0-9]|3[01])[-](0[1-9]|1[012])[-]((19|20)\\d\\d))$")]
        public string chdt { get; set; }



        [Required]
        [Display(Name = "List of ITC04 M2Jw Item Data")]
        public List<ITC04M2JW> itms { get; set; }
    }

    public class ITC04M2JW

    {
        [Required]
        [Display(Name = "Supplier Invoice Value")]
        public decimal txval { get; set; }



        [Required]
        [Display(Name = "Goods type")]
        public string goods_ty { get; set; }



        [Required]
        [Display(Name = "UQC (Unit of Measure) of goods purchased")]
        public string uqc { get; set; }




        [Required]
        [Display(Name = "Quantity of goods purchased")]
        public decimal qty { get; set; }



        [Required]
        [Display(Name = "IGST rate")]
        public decimal tx_i { get; set; }


        [Required]
        [Display(Name = "CGST ratee")]
        public decimal tx_c { get; set; }



        [Required]
        [Display(Name = "SGST rate")]
        public decimal tx_s { get; set; }

        [Required]
        [Display(Name = "Cess rate")]
        public decimal tx_cs { get; set; }


        [Required]
        [Display(Name = "Goods Description")]
        [MaxLength(50)]
        public string desc { get; set; }
    }

    public class JW2MInvoice
    {
        [Required]
        [Display(Name = "Tax payer action")]
        [RegularExpression("^[D]$")]
        public string flag { get; set; }


       
        [Required]
        [Display(Name = "Job worker's State Code")]
        public string jw_stcd { get; set; }

        [Required]
        [Display(Name = "Original Challan Number")]
        [MaxLength(16)]
        public string o_chnum { get; set; }



        [Required]
        [Display(Name = "Original Challan Date")]
        [RegularExpression("^((0[1-9]|[12][0-9]|3[01])[-](0[1-9]|1[012])[-]((19|20)\\d\\d))$")]
        public string o_chdt { get; set; }

        [Required]
        [Display(Name = "List of ITC04 M2Jw Item Data")]
        public List<ITC04JW2MRBSASF> itms { get; set; }
    }

    public class ITC04JW2MRBSASF
    {
        [Required]
        [Display(Name = "action/ nature of transaction of challan, whether received back or sent out to another Job Worker or sold out by Job worker")]
        public string action { get; set; }


        [Required]
        [Display(Name = "Goods Description")]
        public string desc { get; set; }

        [Required]
        [Display(Name = "UQC (Unit of Measure) of goods purchased")]
        [MaxLength(16)]
        public string uqc { get; set; }

        [Required]
        [Display(Name = "Quantity")]
        public decimal qty { get; set; }



        [Required]
        [Display(Name = "Supplier Invoice Value")]
        public decimal txval { get; set; }
        [Required]
        [Display(Name = "Challan Number")]
        [MaxLength(16)]
        public string jw2_chnum { get; set; }


        [Required]
        [Display(Name = "State Code")]
        public string jw2_stcd { get; set; }
        [Required]
        [Display(Name = "Challan Date")]
        [RegularExpression("^((0[1-9]|[12][0-9]|3[01])[-](0[1-9]|1[012])[-]((19|20)\\d\\d))$")]
        public string uqcjw2_chdt { get; set; }

        [Required]
        [Display(Name = "Challan Number")]
        public string inum { get; set; }


        [Required]
        [Display(Name = "Challan Date")]
        [RegularExpression("^((0[1-9]|[12][0-9]|3[01])[-](0[1-9]|1[012])[-]((19|20)\\d\\d))$")]
        public string idt { get; set; }




    }







}

