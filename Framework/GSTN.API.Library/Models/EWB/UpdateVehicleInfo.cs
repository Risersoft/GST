using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace GSTN.API.Library.Models.EWB
{
    public class EWBUpdVehPostRequestInfo
    {
        public EWBUpdVehRequestInfo eway_bill { get; set; }
    }

    public class EWBUpdVehRequestInfo
    {


        [Display(Name = "E-way bill Generated")]
        public long ewbNo { get; set; }

        [Display(Name = "Vehicle number")]
        [MaxLength(20)]
        public string vehicleNo { get; set; }


        [Display(Name = "Place of Consignor")]
        [MaxLength(50)]
        public string fromPlace { get; set; }

        [Display(Name = "State of Consignor")]
        public int fromState { get; set; }

        [Display(Name = "Reason code for vehicle updation")]
        public string reasonCode { get; set; }

        [Display(Name = "Reason for Vehicle Updation")]
        [MaxLength(50)]
        public string reasonRem { get; set; }

        [Display(Name = "Transporter Document number")]
        [MaxLength(50)]
        public string transDocNo { get; set; }

        [Display(Name = "Transporter Document Date")]
        [RegularExpression("^((0[1-9]|[12][0-9]|3[01])[-](0[1-9]|1[012])[-]((19|20)\\d\\d))$")]
        public string transDocDate { get; set; }

       [Display(Name = "Mode of Transport")]
        public int transMode { get; set; }
        [MaxLength(1)]
        public string vehicleType { get; set; }
    }
    public class EWBUpdVehResponseInfo: EWBResponseInfoBase
    {
       

        [Display(Name = "Date and Time  of Vehicle Updation")]
        [MaxLength(22)]
        [RegularExpression("^((0[1-9]|[12][0-9]|3[01])[-](0[1-9]|1[012])[-]((19|20)\\d\\d))$")]
        public string vehUpdDate { get; set; }

        [Display(Name = "Date and Time Eway Bill is Valid Upto")]
        [MaxLength(22)]
        [RegularExpression("^((0[1-9]|[12][0-9]|3[01])[-](0[1-9]|1[012])[-]((19|20)\\d\\d))$")]
        public string validUpto { get; set; }


    }
}
