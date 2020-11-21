using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace GSTN.API.Library.Models.EWB
{
    public class EWBCancelPostRequestInfo
    {
        public EWBCancelRequestInfo  eway_bill { get; set; }
    }
    public class EWBCancelRequestInfo
    {


        [Display(Name = "E-way bill Generated")]
        public long ewbNo { get; set; }

        [Display(Name = "Reason code for cancelling eway bill")]
        public int cancelRsnCode { get; set; }

        [Display(Name = "Reason for - cancelling eway bill")]
        [MaxLength(50)]
        public string cancelRmrk { get; set; }
    }
    public class EWBCancelResponseInfo: EWBResponseInfoBase
    {

        [Display(Name = "Unique E-Way Bill No")]
        public long ewayBillNo { get; set; }

        [Display(Name = "Date and Time  of E-Way Bill Cancellation")]
        [MaxLength(22)]
        [RegularExpression("^((0[1-9]|[12][0-9]|3[01])[-](0[1-9]|1[012])[-]((19|20)\\d\\d))$")]
        public string cancelDate { get; set; }

    }
}
