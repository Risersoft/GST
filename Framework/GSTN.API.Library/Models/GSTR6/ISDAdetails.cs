using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Risersoft.API.GSTN.GSTR6
{
    
    public class ISDAdetails

    {
   
        [Display(Name = "ISD Details for Eligible")]
        public List<Elglstdet> elglst { get; set; }
    }

    public class Elglstdet
    {
       
        [Display(Name = "Type of user registered/unregistered")]
        [MaxLength(64)]
        [RegularExpression("^[R/UR]")]
        public string typ { get; set; }



        [Display(Name = "State Code")]
        [MaxLength(2)]
        public string statecd { get; set; }


       
        [Display(Name = "GSTIN/UID of the recipient unit")]
        [MaxLength(15)]
        public string cpty { get; set; }

        
        [Display(Name = "Revised GSTIN/UID of the Supplier taxpayer/UN, Govt Bodies")]
        [MaxLength(15)]
        public string rcpty { get; set; }


       
        [Display(Name = "Revised State Code")]
        [MaxLength(2)]
        public string rstatecd { get; set; }

        public List<DoclstItem> doclst { get; set; }
    }
    public class DoclstItem
    {
       
        [Display(Name = "ISD Document Type")]
       
        public string isd_docty { get; set; }

        [Display(Name = "Revised Credit/Debit Document Number")]
        public string rcrdnum { get; set; }

        [Display(Name = "Original Document Number")]
        public string odocnum { get; set; }



        [Display(Name = "Original Credit/Debit Document  Date")]
        public string ocrddt { get; set; }

    
        [Display(Name = "Original Credit/Debit Document Number")]
        public string ocrdnum { get; set; }



        [Display(Name = "Revised Credit/Debit Document Date")]
        public string rcrddt { get; set; }

        [Display(Name = "Original Document Date")]
        public string odocdt { get; set; }

        [Display(Name = "Revised  Document Number")]
      
        public string rdocnum { get; set; }


        [Display(Name = "Revised  Document Date")]
        public string rdocdt { get; set; }
       
        [Display(Name = "IGST used as IGST")]
        public double iamti { get; set; }

        [Display(Name = "SGST used as IGST")]
        public double iamts { get; set; }

        [Display(Name = "CGST used as IGST")]
        public double iamtc { get; set; }

        [Display(Name = "SGST used as SGST")]
        public double samts { get; set; }


        [Display(Name = "IGST used as SGST")]
        public double samti { get; set; }

        [Display(Name = "IGST used as CGST")]
        public double camti { get; set; }

        [Display(Name = "CGST used as CGST")]
        public double camtc { get; set; }

      
        [Display(Name = "CESS Amount")]
        public double csamt { get; set; }

       
        [Display(Name = "checksum value")]
        public double chksum { get; set; }

        [Display(Name = "TaxPayer Action")]
        public string flag { get; set; }                    // This not match in table schema

        [Display(Name = "GSTIN/UID of the Supplier taxpayer/UN, Govt Bodies")]
        [MaxLength(15)]
        public string cpty { get; set; }


        [Display(Name = "State Code")]
        [MaxLength(2)]
        public string statecd { get; set; }
    }





}
