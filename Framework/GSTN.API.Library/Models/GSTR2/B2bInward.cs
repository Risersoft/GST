﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;

namespace Risersoft.API.GSTN.GSTR2
{
    public class ItmDet
    {

        
        [Required]
        [Display(Name = "Taxable value of Goods or Service as per invoice")]
        public double? txval { get; set; }

        [Required]
        [Display(Name = "Tax Rate")]
        public double? rt { get; set; }

        [Required]
        [Display(Name = "IGST Amount as per invoice")]
        public double? iamt { get; set; }
        
        [Required]
        [Display(Name = "CGST Amount as per invoice")]
        public double? camt { get; set; }

        [Required]
        [Display(Name = "SGST Amount as per invoice")]
        public double? samt { get; set; }

        [Required]
        [Display(Name = "CESS Amount as per invoice")]
        public double? csamt { get; set; }

    }

    public class Itc
    {

        [Required]
        [Display(Name = "Total Tax available as ITC CGST Amount")]
        public double? tx_c { get; set; }

        [Required]
        [Display(Name = "Total Tax available as ITC IGST Amount")]
        public double? tx_i { get; set; }

        [Required]
        [Display(Name = "Total Tax available as ITC SGST Amount")]
        public double? tx_s { get; set; }

        [Required]
        [Display(Name = "Total Tax available as ITC CESS Amount")]
        public double? tx_cs { get; set; }

       
        [Required]
        [Display(Name = "Eligiblity")]
        [MaxLength(2)]
        public string elg { get; set; }

    }

    public class Itm
    {

        [Required]
        [Display(Name = "item number")]
        public int num { get; set; }

        [Required]
        [Display(Name = "details of the items of invoice")]
        public ItmDet itm_det { get; set; }

        [Required]
        [Display(Name = "indirect tax component")]
        public Itc itc { get; set; }

    }

    public class B2bInv
    {

        [Required]
        [Display(Name = "flag for accepting or rejecting a invoice")]
        public string flag { get; set; }


        [Required]
        [Display(Name = "flag for supplier action")]
        [MaxLength(2)]
        public string cflag { get; set; }

        [Required]
        [Display(Name = "optional field only present for carry forwarded invoices")]
        public string opd { get; set; }

        [Required]
        [Display(Name = "Supplier Invoice Number")]
        [MaxLength(16)]
        [RegularExpression("^[a-zA-Z0-9]+$")]
        public string inum { get; set; }

        [Required]
        [Display(Name = "Supplier Invoice Date")]
        [RegularExpression("^((0[1-9]|[12][0-9]|3[01])[-](0[1-9]|1[012])[-]((19|20)\\d\\d))*$")]
        public string idt { get; set; }

        [Required]
        [Display(Name = "Supplier Invoice Value")]
        public double? val { get; set; }

        [Required]
        [Display(Name = "Maintained in GST System common database POS as provided in law / actual provision of service.")]
        [MaxLength(2)]
        public string pos { get; set; }

        [Required]
        [Display(Name = "Reverse Charge")]
        public string rchrg { get; set; }


        [Required]
        [Display(Name = "Uploaded by supplier/receiver")]
        public string updby { get; set; }




        [Required]
        [Display(Name = "flag to determine if it is sez or deemed")]
        public string inv_typ { get; set; }

        [Required]
        [Display(Name = "items")]
        public List<Itm> itms { get; set; }


        [Required]
        [Display(Name = "Invoice Check sum value")]
        [MaxLength(64)]
        [RegularExpression("^[a-zA-Z0-9]+$")]
        public string chksum { get; set; }
    }

    public class B2bInward
    {

        [Required]
        [Display(Name = "GSTIN/UID of the Receiver taxpayer/UN, Govt Bodies")]
        [MaxLength(15)]
        [MinLength(15)]
        [RegularExpression("^[a-zA-Z0-9]+$")]
        public string ctin { get; set; }
                
        [Required]
        [Display(Name = "R2_Fil_Status")]
        public string cfs { get; set; }

        [Required]
        [Display(Name = "Invoice Details")]
        public List<B2bInv> inv { get; set; }
    }


}