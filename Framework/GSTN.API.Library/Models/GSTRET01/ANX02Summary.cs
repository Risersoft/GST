using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Risersoft.API.GSTN.ANX02
{
    public partial class Anx02Summary
    {
        public string Gstin { get; set; }
        public string Rtnprd { get; set; }
        public string Summtyp { get; set; }
        public string Chksum { get; set; }
        public List<Secsum> Secsum { get; set; }
    }

    public partial class Secsum
    {
        public string Secnm { get; set; }
        public string Chksum { get; set; }
        public long Ttldoc { get; set; }
        public long Ttlval { get; set; }
        public double Ttligst { get; set; }
        public long Ttlcgst { get; set; }
        public double Ttlsgst { get; set; }
        public long Ttlcess { get; set; }
        public long Ttltax { get; set; }
        public List<Actionsum> Actionsum { get; set; }
        public List<Cptysum> Cptysum { get; set; }
    }

    public partial class Actionsum
    {
        public string Action { get; set; }
        public string Chksum { get; set; }
        public long Ttldoc { get; set; }
        public long Ttlval { get; set; }
        public double Ttligst { get; set; }
        public long Ttlcgst { get; set; }
        public double Ttlsgst { get; set; }
        public long Ttlcess { get; set; }
        public long Ttltax { get; set; }
    }

    public partial class Cptysum
    {
        public string Ctin { get; set; }
        public List<Totactsum> Totactsum { get; set; }
    }

    public partial class Totactsum
    {
        public string Action { get; set; }
        public string Chksum { get; set; }
        public long Ttldoc { get; set; }
        public long Ttltax { get; set; }
    }

}
