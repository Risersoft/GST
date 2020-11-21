using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;

namespace Risersoft.API.GSTN.GSTR2
{


    public class Doclist
    {

        [Display(Name = "Total Tax available as ITC IGST Amount")]
        public double tx_i { get; set; }

        [Display(Name = "Total Tax available as ITC CGST Amount")]
        public double tx_c { get; set; }

        [Display(Name = "Total Tax available as ITC SGST Amount")]
        public double tx_s { get; set; }

        [Display(Name = "Total Tax available as ITC CESS Amount")]
        public double tx_cs { get; set; }

        [Display(Name = "IGST Amount as ISD")]
        public double iamt { get; set; }

        [Display(Name = "CGST Amount asISD")]
        public double camt { get; set; }

        [Display(Name = "SGST Amount as ISD")]
        public double samt { get; set; }



        [Display(Name = "CESS Amount as ISD")]
        public double csamt { get; set; }



        
        [Display(Name = "ITC eligible")]
        [MaxLength(1)]
        public string itc_elg { get; set; }

       
        [Display(Name = "Invoice Check sum value ")]
        [MaxLength(64)]
        public string chksum { get; set; }
        
        [Display(Name = "flag for accepting  invoice")]
        public string flag { get; set; }
        
        [Display(Name = "Document Number")]
        [MaxLength(16)]
        public string docnum { get; set; }

        
        [Display(Name = "Document  Date")]
        [RegularExpression("^((0[1-9]|[12][0-9]|3[01])[-](0[1-9]|1[012])[-]((19|20)\\d\\d))*$")]
        public string docdt { get; set; }

       
        [Display(Name = "Document Type")]
        public string isd_docty { get; set; }


       
        [Display(Name = "Original Period")]
     
        public string opd { get; set; }
    }

    public class IsdInward
    {

       

        
        [Display(Name = "GSTIN/UID of the ISD taxpayer")]
        [MaxLength(15)]
        [MinLength(15)]
        [RegularExpression("^[a-zA-Z0-9]+$")]
        public string ctin { get; set; }

        
        [Display(Name = "counter party Filing Status")]
        public string cfs { get; set; }

        
        [Display(Name = "Credit/Debit Notes")]
        public List<Doclist> doclist { get; set; }
    }


}