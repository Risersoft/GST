using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;

namespace Risersoft.API.GSTN.GSTR1
{

    public class B2CLInv
    {

        [Display(Name = "TaxPayer Action")]
        public string flag { get; set; }                    // This not match in table schema


        [Required]
        [Display(Name = "Invoice checksum")]
        [MaxLength(64)]
        [MinLength(1)]
        [RegularExpression("^[a-zA-Z0-9]+$")]
        public string chksum { get; set; }
        
        [Required]
        [Display(Name = "Supplier Invoice Number")]
        [MaxLength(16)]
        public string inum { get; set; }

        
        [Display(Name = "Ecommerce Gstin")]
        [MaxLength(15)]
        [MinLength(15)]
        [RegularExpression("^[a-zA-Z0-9._\\s]+$")]
        public string etin { get; set; }

        [Required]
        [Display(Name = "Supplier Invoice Date")]
        [RegularExpression("^((0[1-9]|[12][0-9]|3[01])[-](0[1-9]|1[012])[-]((19|20)\\d\\d))*$")]
        public string idt { get; set; }

        [Required]
        [Display(Name = "Supplier Invoice Value")]
        public double val { get; set; }

        [Display(Name = "Invoice type")]
        public string inv_typ { get; set; }

        [Display(Name = "Differential percentage")]
        public double? diff_percent { get; set; }

        [Required]
        public List<B2CLitem> itms { get; set; }

       
    }
    public class B2CLItmDet
    {

        [Required]
        [Display(Name = "rate as per invoice")]
        public double rt { get; set; }

        [Required]
        [Display(Name = "Taxable value of Goods or Service as per invoice")]
        public double txval { get; set; }

        [Required]
        [Display(Name = "IGST Amount as per invoice")]
        public double iamt { get; set; }

       
        [Display(Name = "Cess Amount as per invoice")]
        public double csamt { get; set; }                 //This not match table schema
    }
    public class B2CLitem
    {
        [Required]
        [Display(Name = "Serial no")]
        public int num { get; set; }
        [Required]
        [Display(Name = "Item Details")]
        public B2CLItmDet itm_det { get; set; }

       
    }
    public class B2clOutward
    {

        [Required]
        [Display(Name = "Place of Supply ")]
        [MaxLength(2)]
        public string pos { get; set; }

        [Required]
        [Display(Name = "Invoice Details")]
        public List<B2CLInv> inv { get; set; }
        public string error_cd { get; set; }
        public string error_msg { get; set; }
    }


}