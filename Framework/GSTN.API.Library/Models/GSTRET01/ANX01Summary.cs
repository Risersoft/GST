using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Risersoft.API.GSTN.ANX01
{
    public partial class Anx01Summary
    {
        /// <summary>
        /// document chksum
        /// </summary>
        public string Chksum { get; set; }

        /// <summary>
        /// Supplier GSTIN
        /// </summary>
        public string Gstin { get; set; }

        /// <summary>
        /// Return period
        /// </summary>
        public string Rtnprd { get; set; }

        public List<Secsum> Secsum { get; set; }

        /// <summary>
        /// summary level
        /// </summary>
        public Summtyp Summtyp { get; set; }
    }

    public partial class Secsum
    {
        public List<Actionsum> Actionsum { get; set; }

        /// <summary>
        /// document chksum
        /// </summary>
        public string Chksum { get; set; }

        public List<Cptysum> Cptysum { get; set; }

        /// <summary>
        /// Total taxable value
        /// </summary>
        public double Nettax { get; set; }

        /// <summary>
        /// Return Section
        /// </summary>
        public string Secnm { get; set; }

        /// <summary>
        /// total cess amount
        /// </summary>
        public double? Ttlcess { get; set; }

        /// <summary>
        /// total CGST amount
        /// </summary>
        public double? Ttlcgst { get; set; }

        /// <summary>
        /// total number of docs
        /// </summary>
        public double? Ttldoc { get; set; }

        /// <summary>
        /// total IGST amount
        /// </summary>
        public double? Ttligst { get; set; }

        /// <summary>
        /// total SGST amount
        /// </summary>
        public double? Ttlsgst { get; set; }

        /// <summary>
        /// Net taxable value
        /// </summary>
        public double? Ttlval { get; set; }
    }

    public partial class Actionsum
    {
        /// <summary>
        /// document status
        /// </summary>
        public Action Action { get; set; }

        /// <summary>
        /// document chksum
        /// </summary>
        public string Chksum { get; set; }

        /// <summary>
        /// total cess amount
        /// </summary>
        public double? Ttlcess { get; set; }

        /// <summary>
        /// total CGST amount
        /// </summary>
        public double? Ttlcgst { get; set; }

        /// <summary>
        /// total number of docs
        /// </summary>
        public double? Ttldoc { get; set; }

        /// <summary>
        /// total IGST amount
        /// </summary>
        public double? Ttligst { get; set; }

        /// <summary>
        /// total SGST amount
        /// </summary>
        public double? Ttlsgst { get; set; }

        /// <summary>
        /// Total taxable value
        /// </summary>
        public double Ttltax { get; set; }

        /// <summary>
        /// Net taxable value
        /// </summary>
        public double Ttlval { get; set; }
    }

    public partial class Cptysum
    {
        /// <summary>
        /// document chksum
        /// </summary>
        public string Chksum { get; set; }

        /// <summary>
        /// Counterparty GSTIN
        /// </summary>
        public string Ctin { get; set; }

        /// <summary>
        /// Total taxable value
        /// </summary>
        public double Nettax { get; set; }

        /// <summary>
        /// total cess amount
        /// </summary>
        public double? Ttlcess { get; set; }

        /// <summary>
        /// total CGST amount
        /// </summary>
        public double? Ttlcgst { get; set; }

        /// <summary>
        /// total number of docs
        /// </summary>
        public double? Ttldoc { get; set; }

        /// <summary>
        /// total IGST amount
        /// </summary>
        public double? Ttligst { get; set; }

        /// <summary>
        /// total SGST amount
        /// </summary>
        public double? Ttlsgst { get; set; }

        /// <summary>
        /// Net taxable value
        /// </summary>
        public double Ttlval { get; set; }
    }

    /// <summary>
    /// document status
    /// </summary>
    public enum Action { A, N, P, R };

    /// <summary>
    /// summary level
    /// </summary>
    public enum Summtyp { H, L };

}
