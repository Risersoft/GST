using GSTN.API.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Risersoft.API.GSTN.ANX02
{
    public partial class SaveAnx02:GSTRBase
    {
        public List<B2B> B2B { get; set; }
        public List<De> De { get; set; }

        /// <summary>
        /// Receiver GSTIN
        /// </summary>
        public string Gstin { get; set; }

        /// <summary>
        /// ITC period
        /// </summary>
        public string Rtnprd { get; set; }

        public List<Sezwop> Sezwop { get; set; }
        public List<Sezwp> Sezwp { get; set; }
    }

    public partial class B2B
    {
        /// <summary>
        /// Counterparty GSTIN
        /// </summary>
        public string Ctin { get; set; }

        public List<B2BDoc> Docs { get; set; }
    }

    public partial class B2BDoc
    {
        /// <summary>
        /// action on document
        /// </summary>
        public Action Action { get; set; }

        /// <summary>
        /// document chksum
        /// </summary>
        public string Chksum { get; set; }

        /// <summary>
        /// differential percentage
        /// </summary>
        public double? Diffprcnt { get; set; }

        public PurpleDoc Doc { get; set; }

        /// <summary>
        /// type of document
        /// </summary>
        public Doctyp Doctyp { get; set; }

        /// <summary>
        /// ITC entitlement
        /// </summary>
        public Itcent Itcent { get; set; }

        public List<PurpleItem> Items { get; set; }

        /// <summary>
        /// Place of Supply(Name of State/UT)
        /// </summary>
        public string Pos { get; set; }

        /// <summary>
        /// Refund eligibility
        /// </summary>
        public Itcent? Rfndelg { get; set; }

        /// <summary>
        /// Supply Covered Under Sec 7 of IGST Act
        /// </summary>
        public Itcent? Sec7Act { get; set; }
    }

    public partial class PurpleDoc
    {
        /// <summary>
        /// Document date
        /// </summary>
        public string Dt { get; set; }

        /// <summary>
        /// Document number
        /// </summary>
        public string Num { get; set; }

        /// <summary>
        /// Document value
        /// </summary>
        public double? Val { get; set; }
    }

    public partial class PurpleItem
    {
        /// <summary>
        /// Cess (₹)
        /// </summary>
        public double? Cess { get; set; }

        /// <summary>
        /// Central Tax (₹)
        /// </summary>
        public double? Cgst { get; set; }

        /// <summary>
        /// HSN code
        /// </summary>
        public string Hsn { get; set; }

        /// <summary>
        /// Integrated Tax (₹)
        /// </summary>
        public double? Igst { get; set; }

        /// <summary>
        /// Rate (%)
        /// </summary>
        public double? Rate { get; set; }

        /// <summary>
        /// State/UT Tax (₹)
        /// </summary>
        public double? Sgst { get; set; }

        /// <summary>
        /// Taxable Value (₹)
        /// </summary>
        public double? Txval { get; set; }
    }

    public partial class De
    {
        /// <summary>
        /// Counterparty GSTIN
        /// </summary>
        public string Ctin { get; set; }

        public List<DeDoc> Docs { get; set; }
    }

    public partial class DeDoc
    {
        /// <summary>
        /// action on document
        /// </summary>
        public Action Action { get; set; }

        /// <summary>
        /// document chksum
        /// </summary>
        public string Chksum { get; set; }

        /// <summary>
        /// Claim Refund
        /// </summary>
        public Itcent? Clmrfnd { get; set; }

        /// <summary>
        /// differential percentage
        /// </summary>
        public double? Diffprcnt { get; set; }

        public FluffyDoc Doc { get; set; }

        /// <summary>
        /// type of document
        /// </summary>
        public Doctyp Doctyp { get; set; }

        /// <summary>
        /// ITC entitlement
        /// </summary>
        public Itcent Itcent { get; set; }

        public List<FluffyItem> Items { get; set; }

        /// <summary>
        /// Place of Supply(Name of State/UT)
        /// </summary>
        public string Pos { get; set; }

        /// <summary>
        /// Refund eligibility
        /// </summary>
        public Itcent? Rfndelg { get; set; }

        /// <summary>
        /// Supply Covered Under Sec 7 of IGST Act
        /// </summary>
        public Itcent? Sec7Act { get; set; }
    }

    public partial class FluffyDoc
    {
        /// <summary>
        /// Document date
        /// </summary>
        public string Dt { get; set; }

        /// <summary>
        /// Document number
        /// </summary>
        public string Num { get; set; }

        /// <summary>
        /// Document value
        /// </summary>
        public double? Val { get; set; }
    }

    public partial class FluffyItem
    {
        /// <summary>
        /// Cess (₹)
        /// </summary>
        public double? Cess { get; set; }

        /// <summary>
        /// Central Tax (₹)
        /// </summary>
        public double? Cgst { get; set; }

        /// <summary>
        /// HSN code
        /// </summary>
        public string Hsn { get; set; }

        /// <summary>
        /// Integrated Tax (₹)
        /// </summary>
        public double? Igst { get; set; }

        /// <summary>
        /// Rate (%)
        /// </summary>
        public double? Rate { get; set; }

        /// <summary>
        /// State/UT Tax (₹)
        /// </summary>
        public double? Sgst { get; set; }

        /// <summary>
        /// Taxable Value (₹)
        /// </summary>
        public double? Txval { get; set; }
    }

    public partial class Sezwop
    {
        /// <summary>
        /// Counterparty GSTIN
        /// </summary>
        public string Ctin { get; set; }

        public List<SezwopDoc> Docs { get; set; }
    }

    public partial class SezwopDoc
    {
        /// <summary>
        /// action on document
        /// </summary>
        public Action Action { get; set; }

        /// <summary>
        /// document chksum
        /// </summary>
        public string Chksum { get; set; }

        public TentacledDoc Doc { get; set; }

        /// <summary>
        /// type of document
        /// </summary>
        public Doctyp Doctyp { get; set; }

        /// <summary>
        /// ITC entitlement
        /// </summary>
        public Itcent Itcent { get; set; }

        public List<TentacledItem> Items { get; set; }

        /// <summary>
        /// Place of Supply(Name of State/UT)
        /// </summary>
        public string Pos { get; set; }

        /// <summary>
        /// Refund eligibility
        /// </summary>
        public Itcent? Rfndelg { get; set; }
    }

    public partial class TentacledDoc
    {
        /// <summary>
        /// Document date
        /// </summary>
        public string Dt { get; set; }

        /// <summary>
        /// Document number
        /// </summary>
        public string Num { get; set; }

        /// <summary>
        /// Document value
        /// </summary>
        public double? Val { get; set; }
    }

    public partial class TentacledItem
    {
        /// <summary>
        /// HSN code
        /// </summary>
        public string Hsn { get; set; }

        /// <summary>
        /// Rate (%)
        /// </summary>
        public double? Rate { get; set; }

        /// <summary>
        /// Taxable Value (₹)
        /// </summary>
        public double? Txval { get; set; }
    }

    public partial class Sezwp
    {
        /// <summary>
        /// Counterparty GSTIN
        /// </summary>
        public string Ctin { get; set; }

        public List<SezwpDoc> Docs { get; set; }
    }

    public partial class SezwpDoc
    {
        /// <summary>
        /// action on document
        /// </summary>
        public Action Action { get; set; }

        /// <summary>
        /// document chksum
        /// </summary>
        public string Chksum { get; set; }

        /// <summary>
        /// Claim Refund
        /// </summary>
        public Itcent? Clmrfnd { get; set; }

        /// <summary>
        /// differential percentage
        /// </summary>
        public double? Diffprcnt { get; set; }

        public StickyDoc Doc { get; set; }

        /// <summary>
        /// type of document
        /// </summary>
        public Doctyp Doctyp { get; set; }

        /// <summary>
        /// ITC entitlement
        /// </summary>
        public Itcent Itcent { get; set; }

        public List<StickyItem> Items { get; set; }

        /// <summary>
        /// Place of Supply(Name of State/UT)
        /// </summary>
        public string Pos { get; set; }

        /// <summary>
        /// Refund eligibility
        /// </summary>
        public Itcent? Rfndelg { get; set; }
    }

    public partial class StickyDoc
    {
        /// <summary>
        /// Document date
        /// </summary>
        public string Dt { get; set; }

        /// <summary>
        /// Document number
        /// </summary>
        public string Num { get; set; }

        /// <summary>
        /// Document value
        /// </summary>
        public double? Val { get; set; }
    }

    public partial class StickyItem
    {
        /// <summary>
        /// Cess (₹)
        /// </summary>
        public double? Cess { get; set; }

        /// <summary>
        /// HSN code
        /// </summary>
        public string Hsn { get; set; }

        /// <summary>
        /// Integrated Tax (₹)
        /// </summary>
        public double? Igst { get; set; }

        /// <summary>
        /// Rate (%)
        /// </summary>
        public double? Rate { get; set; }

        /// <summary>
        /// Taxable Value (₹)
        /// </summary>
        public double? Txval { get; set; }
    }

    /// <summary>
    /// action on document
    /// </summary>
    public enum Action { A, N, P, R };

    /// <summary>
    /// type of document
    /// </summary>
    public enum Doctyp { C, D, I };

    /// <summary>
    /// ITC entitlement
    ///
    /// Refund eligibility
    ///
    /// Supply Covered Under Sec 7 of IGST Act
    ///
    /// Claim Refund
    /// </summary>
    public enum Itcent { N, Y };

}
