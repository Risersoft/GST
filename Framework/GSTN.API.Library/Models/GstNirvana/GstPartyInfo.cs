using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSTN.API.Library.Models.GstNirvana
{
   public class GstPartyInfo
    {
         public int PartyID { get; set; }
        public Nullable<int> MainPartyID { get; set; }
        public string PartyCode { get; set; }
        public string PartyName { get; set; }
        public string Division { get; set; }
        public string SelEmail { get; set; }
        public string SelWeb { get; set; }
        public string SelAddress { get; set; }
        public string SelCity { get; set; }
        public string SelState { get; set; }
        public string SelCountry { get; set; }
        public string SelStateCode { get; set; }
        public string SelCountryCode { get; set; }
        public string SelPinCode { get; set; }
        public string SelPhCountry { get; set; }
        public string SelPhArea { get; set; }
        public string SelFaxCountry { get; set; }
        public string SelFaxArea { get; set; }
        public string SelFaxNum { get; set; }
        public string SelPhNum { get; set; }
        public string CINNum { get; set; }
        public string TINNum { get; set; }

        public string  TaxAreaCode { get; set; }
        public string GSTIN { get; set; }
        public string PANNum { get; set; }

        
    }
}
