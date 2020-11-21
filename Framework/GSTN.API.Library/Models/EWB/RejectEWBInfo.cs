using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace GSTN.API.Library.Models.EWB
{
    public class EWBRejectPostRequestInfo
    {
        public EWBRejectRequestInfo eway_bill { get; set; }
    }
    public class EWBRejectRequestInfo
    {


        [Display(Name = "E-way bill Generated")]
        public long ewbNo { get; set; }
    }
    public class EWBRejectResponseInfo: EWBResponseInfoBase
    {
        
        [Display(Name = "Unique E-Way Bill No")]
        public long ewayBillNo { get; set; }

        [Display(Name = "Date and Time  of E-Way Bill Rejection")]
        [MaxLength(22)]
        [RegularExpression("^((0[1-9]|[12][0-9]|3[01])[-](0[1-9]|1[012])[-]((19|20)\\d\\d))$")]
        public string ewbRejectedDate { get; set; }

    }

}
