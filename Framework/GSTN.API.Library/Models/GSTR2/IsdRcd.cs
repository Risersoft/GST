using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Risersoft.API.GSTN.GSTR2
{
    public partial class IsdRcd
    {
        /// <summary>
        /// counter party filing status
        /// </summary>
        public string Cfs { get; set; }

        /// <summary>
        /// ISD gstin
        /// </summary>
        public string Ctin { get; set; }

        public List<IsdRcdDoclist> Doclist { get; set; }
    }

    public partial class IsdRcdDoclist
    {
        /// <summary>
        /// ISD received as CGST
        /// </summary>
        public double? Camt { get; set; }

        /// <summary>
        /// checksum
        /// </summary>
        public string Chksum { get; set; }

        /// <summary>
        /// ISD received as CESS
        /// </summary>
        public double? Csamt { get; set; }

        /// <summary>
        /// Document Date
        /// </summary>
        public string Docdt { get; set; }

        /// <summary>
        /// Document number
        /// </summary>
        public string Docnum { get; set; }

        /// <summary>
        /// Flag for receiver status
        /// </summary>
        public string Flag { get; set; }

        /// <summary>
        /// ISD received as IGST
        /// </summary>
        public double? Iamt { get; set; }

        /// <summary>
        /// document type
        /// </summary>
        public string Isd_Docty { get; set; }

        /// <summary>
        /// Eligible for ITC
        /// </summary>
        public string Itc_Elg { get; set; }

        /// <summary>
        /// Original period
        /// </summary>
        public string Opd { get; set; }

        /// <summary>
        /// ISD received as SGST
        /// </summary>
        public double? Samt { get; set; }

        /// <summary>
        /// ITC availed as CGST
        /// </summary>
        public double? TxC { get; set; }

        /// <summary>
        /// ITC availed as CESS
        /// </summary>
        public double? TxCs { get; set; }

        /// <summary>
        /// ITC availed as IGST
        /// </summary>
        public double? TxI { get; set; }

        /// <summary>
        /// ITC availed as SGST
        /// </summary>
        public double? TxS { get; set; }
    }

}
