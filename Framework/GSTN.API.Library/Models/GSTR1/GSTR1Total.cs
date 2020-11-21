using GSTN.API.Library.Models;
using Risersoft.API.GSTN.GSTR1;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Risersoft.API.GSTN.GSTR1
{
    public class GSTR1Total:GSTRBase
    {


        [Required]
        [Display(Name = "Gross Turnover of the taxpayer in the previous FY")]
        public double gt { get; set; }



        [Display(Name ="Gross Turnover")]
        public double cur_gt { get; set; }
        public List<B2bOutward> b2b { get; set; }
        public List<B2bAOutward> b2ba { get; set; }
        public List<B2clOutward> b2cl { get; set; }
        public List<B2clAOutward> b2cla { get; set; }
        public List<B2csOutward> b2cs { get; set; }
        public List<B2CSAOutward> b2csa { get; set; }
        public NilRatedOutward nil { get; set; }
        public List<Exp> exp { get; set; }
        public List<ExpA> expa { get; set; }
        public List<AtOutward> at { get; set; }
        public List<AtAOutward> ata { get; set; }
        public List<CDNROutward> cdnr { get; set; }
        public List<CDNRAOutward> cdnra { get; set; }
        public List<CDNUROutward> cdnur { get; set; }
        public List<CDNURAOutward> cdnura { get; set; }
        public List<TxpOutward> txpd { get; set; }
        public List<TxpAOutward> txpda { get; set; }
        public HSNSummaryOutward hsn { get; set; }
        public DocumentIssue doc_issue { get; set; }
    }
}
