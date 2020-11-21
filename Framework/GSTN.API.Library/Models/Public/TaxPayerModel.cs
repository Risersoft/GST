using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Risersoft.API.GSTN.Public
{
    public class TaxPayerModel
    {

        [Display(Name = "Constitution of Business")]
        [Required]
        public string ctb { get; set; }

        [Display(Name = "Center Jurisdiction")]
        [Required]
        public string ctj { get; set; }

        [Display(Name = "Date Of Cancellation")]
        [Required]
        public string cxdt { get; set; }

        [Display(Name = "TaxPayer type")]
        [Required]
        public string dty { get; set; }

        [Display(Name = "GST Identification Number")]
        [Required]
        public string gstin { get; set; }

        [Display(Name = "Long Name")]
        [Required]
        public string lgnm { get; set; }


        [Required]
        [Display(Name = "Nature of Business Activity")]
        public List<string> nba { get; set; }

        [Display(Name = "Date of Registration")]
        [Required]
        public string rgdt { get; set; }


        [Display(Name = "State Jurisdiction")]
        [Required]
        public string stj { get; set; }

        [Display(Name = "GSTN status")]
        [Required]
        public string sts { get; set; }


    }




}


