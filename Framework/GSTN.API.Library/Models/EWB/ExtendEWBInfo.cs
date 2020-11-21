using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace GSTN.API.Library.Models.EWB
{
    public class EWBExtendPostRequestInfo
    {
        public EWBExtendRequestInfo eway_bill { get; set; }
    }
    public class EWBExtendRequestInfo
    {


        /// <summary>
        /// addressLine1
        /// </summary>
        public string AddressLine1 { get; set; }

        /// <summary>
        /// addressLine2
        /// </summary>
        public string AddressLine2 { get; set; }

        /// <summary>
        /// addressLine3
        /// </summary>
        public string AddressLine3 { get; set; }

        /// <summary>
        /// consignmentStatus(T)
        /// </summary>
        public string ConsignmentStatus { get; set; }

        /// <summary>
        /// Ewaybill Number
        /// </summary>
        public long EwbNo { get; set; }

        /// <summary>
        /// Extension Remarks
        /// </summary>
        public string ExtnRemarks { get; set; }

        /// <summary>
        /// Extension Reason Code
        /// </summary>
        public double ExtnRsnCode { get; set; }

        /// <summary>
        /// From Pincode
        /// </summary>
        public double FromPincode { get; set; }

        /// <summary>
        /// From Place
        /// </summary>
        public string FromPlace { get; set; }

        /// <summary>
        /// From State
        /// </summary>
        public long FromState { get; set; }

        /// <summary>
        /// Remaining Distance
        /// </summary>
        public double RemainingDistance { get; set; }

        /// <summary>
        /// Transport Document Date
        /// </summary>
        public string TransDocDate { get; set; }

        /// <summary>
        /// Transport Document Number
        /// </summary>
        public string TransDocNo { get; set; }

        /// <summary>
        /// transit Type
        /// </summary>
        public string TransitType { get; set; }

        /// <summary>
        /// Transport Mode
        /// </summary>
        public string CoordinateTransMode { get; set; }

        /// <summary>
        /// Vehicle Number
        /// </summary>
        public string VehicleNo { get; set; }

        public int TransMode { get; set; }
    
    }
    public class EWBExtendResponseInfo: EWBResponseInfoBase
    {

        public string EwayBillNo { get; set; }
        public string UpdatedDate { get; set; }
        public string ValidUpto { get; set; }

    }

}
