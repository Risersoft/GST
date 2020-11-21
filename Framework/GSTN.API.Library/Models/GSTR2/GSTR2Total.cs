using GSTN.API.Library.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Risersoft.API.GSTN.GSTR2
{

    public class GSTR2Total : GSTRBase
    {


        [Required]
        [Display(Name = "Claiming Credit under sec 17(3)")]
        [MaxLength(1)]
        [MinLength(1)]
        public string crclm_17_3 { get; set; }

        public List<B2bInward> b2b { get; set; }


        public List<B2bAInward> b2ba { get; set; }

        public List<B2BURInward> b2bur { get; set; }

        public List<ImpG> imp_g { get; set; }


        public List<ImpGA> imp_ga { get; set; }

        public List<ImpS> imp_s { get; set; }

        public List<ImpSA> imp_sa { get; set; }

        public List<CdnInward> cdn { get; set; }

        public List<CdnAInward> cdna { get; set; }
    
        public List<CDNURInward> cdnur { get; set; }
        public List<NilRatedInward> nil_supplies { get; set; }
        public HSNSummaryInward hsn { get; set; }

        public List<IsdRcd> isd { get; set; }


        public List<TdsCredit> tds_credit { get; set; }


        public List<TcsData> tcs_data { get; set; }


        public List<ItcRcd> itc_rcd { get; set; }


        public List<Txli> txi { get; set; }

        public List<TxliA> atxi { get; set; }

        public List<Txpd> txpd { get; set; }


        public List<ItcRvsl> itc_rvsl { get; set; }

    }
}
