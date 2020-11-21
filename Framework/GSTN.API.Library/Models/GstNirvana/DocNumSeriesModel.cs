using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSTN.API.Library.Models.GstNirvana
{
    public class DocNumSeriesModel
    {
        public int GSTRegID { get; set; }
        public int ReturnPeriodID { get; set; }
        public int InvoiceNumberSeriesId { get; set; }
        public int InvoiceNumberTemplateId { get; set; }
        public long? NumFrom { get; set; }
        public long? NumTo { get; set; }
        public long TotCount { get; set; }
        public long CancelledCount { get; set; }
        public long IssuedCount { get; set; }
        public long MissingCount { get; set; }
    }

    public class DocNumSeriesDiffModel
    {
        public int GSTRegID { get; set; }
        public int ReturnPeriodID { get; set; }
        public int InvoiceNumberSeriesId { get; set; }
        public int InvoiceNumberTemplateId { get; set; }
        public string DocumentNatureDescrip { get; set; }
        public string Prefix { get; set; }
        public string Suffix { get; set; }
        public long? LastPeriodEnd { get; set; }
        public long? CurrentPeriodStart { get; set; }
    }

}
