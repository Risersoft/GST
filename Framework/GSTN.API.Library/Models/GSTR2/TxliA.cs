using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Risersoft.API.GSTN.GSTR2
{




    public class Txlitm
    {
        [Required]
        [Display(Name = "Identifier if Goods or Services")]
        public string ty { get; set; }



        [Display(Name = "HSN or SAC of Goods or Services as per Invoice line items")]
        [MaxLength(10)]
        public string hsn_sc { get; set; }
        [Required]
        [Display(Name = "Taxable value of Goods or Service as per invoice")]
        public double txval { get; set; }

        [Display(Name = "IGST Rate as per invoice")]
        public double Irt { get; set; }

        [Display(Name = "IGST Amount as per invoice")]
        public double iamt { get; set; }

        [Display(Name = "CGST Rate as per invoice")]
        public double crt { get; set; }
        [Display(Name = "CGST Amount as per invoice")]
        public double camt { get; set; }


        [Display(Name = "SGST Rate as per invoice")]
        public double srt { get; set; }

        [Display(Name = "SGST Amount as per invoice")]
        public double samt { get; set; }



        [Display(Name = "Cess Rate as per invoice")]
        public double csrt { get; set; }
        [Display(Name = "Cess Amount as per invoice")]
        public double csamt { get; set; }




     
    }
    public class TxliA
    {
        [Required]
        [Display(Name = "Counter party")]
        [MaxLength(30)]
        [RegularExpression("^[a-zA-Z0-9]+$")]
        public string cpty { get; set; }

        [Required]
        [Display(Name = "Registration Type")]
        public string reg_type { get; set; }

       [Required]
        [Display(Name = "Original  Supplier Document Number")]
        [MaxLength(10)]
        [RegularExpression("^[a-zA-Z0-9]+$")]
        public string odnum { get; set; }

        [Required]
        [Display(Name = "Original  Supplier Document Date")]
        [RegularExpression("^((0[1-9]|[12][0-9]|3[01])[-](0[1-9]|1[012])[-]((19|20)\\d\\d))*$")]
        public string otdt { get; set; }

        [Required]
        [Display(Name = "Placeof supply State Code")]
        [MaxLength(2)]
        public int state_cd { get; set; }

        [Required]
        [Display(Name = "Supplier Document Number")]
        [MaxLength(10)]
        [RegularExpression("^[a-zA-Z0-9]+$")]
        public string dnum { get; set; }

        [Required]
        [Display(Name = "Supplier Document Date")]
        [RegularExpression("^((0[1-9]|[12][0-9]|3[01])[-](0[1-9]|1[012])[-]((19|20)\\d\\d))*$")]
        public string dt { get; set; }

        [Required]
        [Display(Name = "Original Reg Type")]
        public string oreg_type { get; set; }

        [Required]
        [Display(Name = "Original Name of Recepient")]
        [MaxLength(30)]
        [RegularExpression("^[a-zA-Z0-9]+$")]
        public string ocpty { get; set; }

        [Required]
        [Display(Name = "Taxable value of Goods or Service as per invoice")]
        public double txval { get; set; }
        [Required]
        [Display(Name = "Total IGST")]
        public double iamt { get; set; }

        [Required]
        [Display(Name = "Total CGST")]
        public double camt { get; set; }

        [Required]
        [Display(Name = "Total SGST")]
        public double samt { get; set; }


        [Required]
        [Display(Name = "Total Cess")]
        public double csamt { get; set; }

        [Required]
        [Display(Name = "itms")]
        public List<Txlitm> itms { get; set; }

        [Required]
        [Display(Name = "Invoice Check sum value")]
        [MaxLength(15)]
        [RegularExpression("^[a-zA-Z0-9]+$")]
        public string chksum { get; set; }
    }

}
