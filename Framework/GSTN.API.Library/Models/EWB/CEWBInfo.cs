using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace GSTN.API.Library.Models.EWB
{
  public  class CEWBInfo
    {


        [Display(Name = "vehicle number")]
        [MaxLength(20)]
        public string vehicleNo { get; set; }

        [Display(Name = "from place of consignor")]
        [MaxLength(50)]
        public string fromPlace { get; set; }


        [Display(Name = "Mode of Transportation")]
        public int transMode { get; set; }

        [Display(Name = "Transporter Document number")]
        [MaxLength(50)]
        public string transDocNo { get; set; }

        [Display(Name = "Transporter Document Date")]
        [RegularExpression("^((0[1-9]|[12][0-9]|3[01])[-](0[1-9]|1[012])[-]((19|20)\\d\\d))$")]
        public string transDocDate { get; set; }

        [Display(Name = "State of Consignor")]
        public double fromState { get; set; }

        [Display(Name = "List of eway bills")]        
        public List<TripSheetEwbBills> tripSheetEwbBills { get; set; }

        
    }
    public class TripSheetEwbBills
    {
        [Display(Name = "E-way bill Number generated")]
        public long ewbNo { get; set; }
    }
}
