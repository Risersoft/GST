using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using risersoft.shared.portable.Model;

namespace GSTN.API.Library.Models.EWB
{
    public class BulkEWBInfo 
    {
        public List<GenerateEWBInfo> billLists { get; set; }
    }
    public class EWBQRInfo
    {

        public long EwbNo { get; set; }
        [MaxLength(15)]
        public string userGstin { get; set; }
        public string GenDate { get; set; }

    }
    public class EWBInfo:BaseEWBInfo
    {

        [MaxLength(15)]
        public string userGstin { get; set; }

        public long EwbNo { get; set; }
        public string EwayBillDate { get; set; }
        public string GenMode { get; set; }
        [Display(Name = "GSTIN of API User")]
        public long ActualDist { get; set; }
        public long NoValidDays { get; set; }
        public string ValidUpto { get; set; }
        public long ExtendedTimes { get; set; }
        public string Status { get; set; }
        public string RejectStatus { get; set; }

        public List<EWBVehicleInfo> VehiclListDetails { get; set; }

    }
    public class GenerateEWBInfo : BaseEWBInfo
    {
        [Display(Name = "Distance of transportation")]
        public string transDistance { get; set; }
        [Display(Name = "Vehicle Type")]
        [MaxLength(1)]
        public string VehicleType { get; set; }

        [Display(Name = "Vehicle No.")]
        [MaxLength(10)]
        public string vehicleNo { get; set; }

    }

    public class BaseEWBInfo:BaseInfo
    {

        [Display(Name = "Supply whether it is outward/inward.")]
        [MaxLength(1)]
        public string supplyType { get; set; }


        [Display(Name = "Sub types of Supply ")]
        public string subSupplyType { get; set; }

        [Display(Name = "Document Type")]
        [MaxLength(3)]
        public string docType { get; set; }


        [Display(Name = "Document No")]
        [MaxLength(50)]
        public string docNo { get; set; }

        [Display(Name = "Document Date")]
        [RegularExpression("^((0[1-9]|[12][0-9]|3[01])[-](0[1-9]|1[012])[-]((19|20)\\d\\d))$")]
        public string docDate { get; set; }



        [Display(Name = "GSTIN of the Consignor")]
        [MaxLength(15)]
        public string fromGstin { get; set; }

        [Display(Name = "LegalName of consignor")]
        [MaxLength(100)]
        public string fromTrdName { get; set; }

        [Display(Name = "Address of consignor - Line 1")]
        [MaxLength(120)]
        public string fromAddr1 { get; set; }

        [Display(Name = "Address of consignor - Line 2")]
        [MaxLength(120)]
        public string fromAddr2 { get; set; }

        [Display(Name = "Place of consignor")]
        [MaxLength(50)]
        public string fromPlace { get; set; }

        [Display(Name = "Pincode of consignor")]
        public int fromPincode { get; set; }




        [Display(Name = "Actual State of consignor")]
        public int actFromStateCode { get; set; }

        [Display(Name = "State of consignor")]
        public int fromStateCode { get; set; }

        [Display(Name = "GSTIN of consignee")]
        [MaxLength(15)]
        public string toGstin { get; set; }




        [Display(Name = "Legalname of consignee")]
        [MaxLength(100)]
        public string toTrdName { get; set; }

        [Display(Name = "Address of consignee - Line 1")]
        [MaxLength(120)]
        public string toAddr1 { get; set; }

        [Display(Name = "Address of consignee- Line 2")]
        [MaxLength(120)]
        public string toAddr2 { get; set; }

        [Display(Name = "Place of consignee")]
        [MaxLength(60)]
        public string toPlace { get; set; }

        [Display(Name = "Pincode of the consignee")]
        public int toPincode { get; set; }

        [Display(Name = "Actual State of Supply")]
        public int actToStateCode { get; set; }

        [Display(Name = "State of Supply")]
        public int toStateCode { get; set; }

        [Display(Name = "Taxable Amount")]
        public double totalValue { get; set; }

        [Display(Name = "Total Invoice Value")]
        public double totInvValue { get; set; }

        [Display(Name = "CGST Amount ")]
        public double cgstValue { get; set; }


        [Display(Name = "SGST Amount")]
        public double sgstValue { get; set; }


        [Display(Name = "IGST Amount")]
        public double igstValue { get; set; }


        [Display(Name = "CESS Amount")]
        public double cessValue { get; set; }



        [Display(Name = "Transporter Id")]
        [MaxLength(15)]
        public string transporterId { get; set; }

        [Display(Name = "Transporter Name")]
        [MaxLength(100)]
        public string transporterName { get; set; }

        [Display(Name = "Transporter Doc No")]
        [MaxLength(15)]
        public string transDocNo { get; set; }

        [Display(Name = "Mode of transportation")]
        public string transMode { get; set; }

        
        [Display(Name = "Transporter Doc Date")]
        [RegularExpression("^((0[1-9]|[12][0-9]|3[01])[-](0[1-9]|1[012])[-]((19|20)\\d\\d))$")]
        public string transDocDate { get; set; }

        [Display(Name = "Transaction Type")]
        public string TransactionType { get; set; }

        [Display(Name = "Dispatch from GSTIN")]
        [MaxLength(15)]
        public string dispatchFromGSTIN { get; set; }

        [Display(Name = "Dispatch from Trade Name")]
        [MaxLength(100)]
        public string dispatchFromTradeName { get; set; }



        [Display(Name = "Ship To GSTIN")]
        [MaxLength(15)]
        public string shipToGSTIN { get; set; }

        [Display(Name = "Ship To Trade Name")]
        [MaxLength(100)]
        public string shipToTradeName { get; set; }

        [Display(Name = "Other Value")]
        public double otherValue { get; set; }

        [Display(Name = "Cess Non Advol Value")]
        public double cessNonAdvolValue { get; set; }


        public List<EWayBillItemInfo> ItemList { get; set; }

        
    }


    public class EWayBillItemInfo
    {
        [Display(Name = "Name of the Product")]
        [MaxLength(100)]
        public string productName { get; set; }


        [Display(Name = "Description of the Product")]
        public string productDesc { get; set; }

        [Display(Name = "HSN Code of the Product")]
        public int hsnCode { get; set; }



        [Display(Name = "Quantity of Product in Numbers")]
        public double quantity { get; set; }

        [Display(Name = "Unit of the Product, like Liter,Kg etc")]
        [MaxLength(3)]
        public string qtyUnit { get; set; }



        [Display(Name = "Total Amount/ Taxable Amount")]
        public double taxableAmount { get; set; }

        [Display(Name = "CGST Rate")]
        public double cgstRate { get; set; }

        [Display(Name = "SGST Rate")]
        public double sgstRate { get; set; }

        [Display(Name = "IGST Rate")]
        public double igstRate { get; set; }

        [Display(Name = "CESS Rate")]
        public double cessRate { get; set; }



        [Display(Name = "cessAdvol")]
        public double cessAdvol { get; set; }//json



    }
    public class EWBPostRequestInfo
    { 
        public GenerateEWBInfo eway_bill { get; set; }
    }
        public class EWBPostResponseInfo:EWBResponseInfoBase
    {

        [Display(Name = "Unique E-Way Bill No")]
        public Int64 ewayBillNo { get; set; }

        [Display(Name = "Date and Time  of E-Way Bill Generation")]
        [MaxLength(22)]
        [RegularExpression("^((0[1-9]|[12][0-9]|3[01])[-](0[1-9]|1[012])[-]((19|20)\\d\\d))$")]
        public string ewayBillDate { get; set; }

        [Display(Name = "Date and Time Eway Bill is Valid Upto")]
        [MaxLength(22)]
        [RegularExpression("^((0[1-9]|[12][0-9]|3[01])[-](0[1-9]|1[012])[-]((19|20)\\d\\d))$")]
        public string validUpto { get; set; }

        public ErrorInfo Info { get; set; }

    }
    public partial class ErrorInfo
    {
        public long FromPinCode { get; set; }
        public long ToPinCode { get; set; }
        public long Distance { get; set; }
    }

    public class EWBResponseInfoBase
    {


        [Display(Name = "Status code")]
        public double Status { get; set; }

        [Display(Name = "Refer Error Codes")]
        [MaxLength(200)]
        public string errorCodes { get; set; }


    }

    public class EWBVehicleInfo
    {
        [Display(Name = "E-way bill Generated")]
        public Int64 EwbNo { get; set; }
        public string UpdMode { get; set; }

        [Display(Name = "Vehicle number")]
        [MaxLength(20)]
        public string VehicleNo { get; set; }


        [Display(Name = "Place of Consignor")]
        [MaxLength(50)]
        public string FromPlace { get; set; }

        [Display(Name = "State of Consignor")]
        public double FromState { get; set; }
        public long TripshtNo { get; set; }
        public string UserGstinTransin { get; set; }
        public string EnteredDate { get; set; }
        [Display(Name = "Transporter Document number")]
        [MaxLength(50)]
        public string TransDocNo { get; set; }

        [Display(Name = "Transporter Document Date")]
        [RegularExpression("^((0[1-9]|[12][0-9]|3[01])[-](0[1-9]|1[012])[-]((19|20)\\d\\d))$")]
        public string TransDocDate { get; set; }

        [Display(Name = "Mode of Transport")]
        public int TransMode { get; set; }
        public string vehicleType { get; set; }
    }
    public class EwayBillApiRequest
    {
        public string action { get; set; }
        public string data { get; set; }
    }
    public class EwayBillApiResponse
    {
        public string status { get; set; }
        public string message { get; set; }
        public string error { get; set; }
        public string data { get; set; }
    }

}
