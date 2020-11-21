using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Risersoft.API.GSTN.GSTR2
{

    public partial class ItcRcd
    {
        /// <summary>
        /// Checksum Value
        /// </summary>
        public string Chksum { get; set; }

        /// <summary>
        /// Invoice date
        /// </summary>
        public string InvDocDt { get; set; }

        /// <summary>
        /// Invoice number
        /// </summary>
        public string InvDocNum { get; set; }

        /// <summary>
        /// This month CGST
        /// </summary>
        public double? NCg { get; set; }

        /// <summary>
        /// This month CESS
        /// </summary>
        public double? NCs { get; set; }

        /// <summary>
        /// This month IGST
        /// </summary>
        public double? NIg { get; set; }

        /// <summary>
        /// This month SGST
        /// </summary>
        public double? NSg { get; set; }

        /// <summary>
        /// Earlier CGST
        /// </summary>
        public double? OCg { get; set; }

        /// <summary>
        /// Earlier CESS
        /// </summary>
        public double? OCs { get; set; }

        /// <summary>
        /// Earlier IGST
        /// </summary>
        public double? OIg { get; set; }

        /// <summary>
        /// Earlier SGST
        /// </summary>
        public double? OSg { get; set; }

        /// <summary>
        /// Supplier tin
        /// </summary>
        public string Stin { get; set; }

        /// <summary>
        /// document type
        /// </summary>
        public string Typ { get; set; }
    }
}