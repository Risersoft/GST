using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Risersoft.API.GSTN.GSTR6
{
    public class Elglst
    {


        [Required]
        [Display(Name = "Type of user registered/unregistered")]
        [MaxLength(2)]
        [RegularExpression("^[R/UR]")]
        public string typ { get; set; }



        [Display(Name = "State Code")]
        [MaxLength(2)]
        public string statecd { get; set; }







        [Required]
        [Display(Name = "GSTIN/UID of the recipient unit")]
        [MaxLength(16)]
        public string cpty { get; set; }

        [Required]
        public List<Doclst> doclst { get; set; }

    }
    public class Doclst
    {
        [Required]
        [Display(Name = "ISD Document Type")]
        [RegularExpression("^[ISD/ISDCN/ISDUR/ISDCNUR]")]
        public string isd_docty { get; set; }

        [Display(Name = "Inv Document Number")]
        [MaxLength(16)]
        public string docnum { get; set; }

        [Required]
        [Display(Name = "Inv Document Date")]
        [RegularExpression("^((0[1-9]|[12][0-9]|3[01])[-](0[1-9]|1[012])[-]((19|20)\\d\\d))*$")]
        public string docdt { get; set; }

        [Required]
        [Display(Name = "Credit/Debit Document Number")]
        [MaxLength(16)]
        public string crdnum { get; set; }
        [Required]
        [Display(Name = "Credit/Debit  Document Date")]
        [RegularExpression("^((0[1-9]|[12][0-9]|3[01])[-](0[1-9]|1[012])[-]((19|20)\\d\\d))*$")]
        public string crddt { get; set; }


        [Required]
        [Display(Name = "IGST used as IGST")]
        public double iamti { get; set; }


        [Required]
        [Display(Name = "SGST used as IGST")]
        public double iamts { get; set; }


        [Required]
        [Display(Name = "CGST used as IGST")]
        public double iamtc { get; set; }


        [Required]
        [Display(Name = "SGST used as SGST")]
        public double samts { get; set; }


        [Required]
        [Display(Name = "IGST used as SGST")]
        public double samti { get; set; }


        [Required]
        [Display(Name = "IGST used as CGST")]
        public double camti { get; set; }

        [Required]
        [Display(Name = "CGST used as CGST")]
        public double camtc { get; set; }




        [Required]
        [Display(Name = "CESS Amount")]
        public double csamt { get; set; }
        [Required]
        [Display(Name = "checksum value")]
        public double chksum { get; set; }
        [Display(Name = "TaxPayer Action")]
        public string flag { get; set; }                    // This not match in table schema


    }
    public class ISDdetails

    {



                                
        [Required]
        [Display(Name = "ISD Details for Eligible")]
        public List<Elglst> elglst { get; set; }
      
    }
}
