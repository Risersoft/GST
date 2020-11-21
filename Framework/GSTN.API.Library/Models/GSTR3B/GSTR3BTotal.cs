using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using GSTN.API.Library.Models;
using Newtonsoft.Json;

namespace Risersoft.API.GSTN.GSTR3B
{
   
    public class GSTR3BTotal:GSTRBase
    {
        [JsonIgnore]
        public override string fp
        {
            get
            {
                return this.ret_period;
            }

            set
            {
                this.ret_period = value;
            }
        }

        [Required]
        [Display(Name = "Return Period")]
        [RegularExpression("^((0[1-9]|1[012])[-]((19|20)\\d\\d))*$")]
        public string ret_period { get; set; }

        
        [Display(Name = "Details of Outward Supplies and inward supplies liable to reverse charge ")]
        public Supdetails sup_details { get; set; }

       
        [Display(Name = "Details of inter-State supplies made to unregistered persons, composition taxable persons and UIN holders")]
        public List<Supinter> sup_inter { get; set; }
        
        
        [Display(Name = "Eligible ITC")]
        public Itcelg itc_elg { get; set; }

        [Display(Name = "Values of exempt, nil-rated and non-GST  inward supplies")]
        public Inwardsup inward_sup { get; set; }

        [Display(Name = "Interest & late fee payable")]
        public Intrltfee intr_ltfee { get; set; }

        [Display(Name = "Payment of tax")]
        public Taxpmt tx_pmt { get; set; }


    }

}