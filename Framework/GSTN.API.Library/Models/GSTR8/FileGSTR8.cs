using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;

namespace Risersoft.API.GSTN.GSTR8
{

    

    public class FileGSTR8
    {

        [Display(Name = "Action key specific to the request")]
        public string action { get; set; }


        [Display(Name = "GSTR8 Summary")]
        public GSTR8Summary data { get; set; }

        [Required]
        [Display(Name = "PKCS#7 signature of SHA-256 hash of Base64 of response of getGSTR4Summary using private key of Tax Payer (Authorized signatory).In case of EVC sign = HMAC(Base64(Data), Base64(PAN | OTP))")]
        public string sign { get; set; }

        [Display(Name = "Type of signature – DSC or ESIGN or EVC")]
        public string st { get; set; }

        [Display(Name = "PAN of authorized representative if st = DSC  or AADHAR no. of authorized representative if st=ESIGN In case of EVC  st = PAN | OTP")]
        [MaxLength(20)]
        public string sid { get; set; }

        [Display(Name = "JSON object of header values")]
        public string hdr { get; set; }



    }

}