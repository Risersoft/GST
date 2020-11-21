using GSTN.API.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Risersoft.API.GSTN.ANX01
{
    public partial class SaveAnx01:GSTRBase
    {
        public List<B2B> B2B { get; set; }
        public List<B2C> B2C { get; set; }
        public List<De> De { get; set; }
        public List<Ecom> Ecom { get; set; }
        public List<Expwop> Expwop { get; set; }
        public List<Expwp> Expwp { get; set; }

        /// <summary>
        /// Supplier GSTIN
        /// </summary>
        public string Gstin { get; set; }

        public List<Impg> Impg { get; set; }
        public List<Impgsez> Impgsez { get; set; }
        public List<Imp> Imps { get; set; }
        public List<Mi> Mis { get; set; }
        public List<Rev> Rev { get; set; }

        /// <summary>
        /// Tax period
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
        /// differential percentage
        /// </summary>
        public double? Diffprcnt { get; set; }

        public PurpleDoc Doc { get; set; }

        /// <summary>
        /// type of document
        /// </summary>
        public Doctyp Doctyp { get; set; }

        public List<PurpleItem> Items { get; set; }

        /// <summary>
        /// Place of Supply(Name of State/UT)
        /// </summary>
        public string Pos { get; set; }

        /// <summary>
        /// Refund eligibility
        /// </summary>
        public Boolean? Rfndelg { get; set; }

        /// <summary>
        /// Supply Covered Under Sec 7 of IGST Act
        /// </summary>
        public Boolean? Sec7Act { get; set; }

        /// <summary>
        /// Upload action
        /// </summary>
        public Flag? Flag { get; set; }
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

    public partial class B2C
    {
        /// <summary>
        /// differential percentage
        /// </summary>
        public double? Diffprcnt { get; set; }

        public List<B2CItem> Items { get; set; }

        /// <summary>
        /// Place of Supply(Name of State/UT)
        /// </summary>
        public string Pos { get; set; }

        /// <summary>
        /// Refund eligibility
        /// </summary>
        public Boolean? Rfndelg { get; set; }

        /// <summary>
        /// Supply Covered Under Sec 7 of IGST Act
        /// </summary>
        public Boolean? Sec7Act { get; set; }

        /// <summary>
        /// Upload action
        /// </summary>
        public Flag? Flag { get; set; }
    }

    public partial class B2CItem
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
        /// Claim Refund
        /// </summary>
        public Boolean? Clmrfnd { get; set; }

        /// <summary>
        /// differential percentage
        /// </summary>
        public double? Diffprcnt { get; set; }

        public FluffyDoc Doc { get; set; }

        /// <summary>
        /// type of document
        /// </summary>
        public Doctyp Doctyp { get; set; }

        public List<FluffyItem> Items { get; set; }

        /// <summary>
        /// Place of Supply(Name of State/UT)
        /// </summary>
        public string Pos { get; set; }

        /// <summary>
        /// Refund eligibility
        /// </summary>
        public Boolean? Rfndelg { get; set; }

        /// <summary>
        /// Supply Covered Under Sec 7 of IGST Act
        /// </summary>
        public Boolean? Sec7Act { get; set; }

        /// <summary>
        /// Upload action
        /// </summary>
        public Flag? Flag { get; set; }
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

    public partial class Ecom
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
        /// E-commerce GSTIN
        /// </summary>
        public string Etin { get; set; }

        /// <summary>
        /// Integrated Tax (₹)
        /// </summary>
        public double? Igst { get; set; }

        /// <summary>
        /// net supplied amount
        /// </summary>
        public double? Nsup { get; set; }

        /// <summary>
        /// State/UT Tax (₹)
        /// </summary>
        public double? Sgst { get; set; }

        /// <summary>
        /// supplied amount
        /// </summary>
        public double? Sup { get; set; }

        /// <summary>
        /// supplies returned amount
        /// </summary>
        public double? Supr { get; set; }

        /// <summary>
        /// Upload action
        /// </summary>
        public Flag? Flag { get; set; }
    }

    public partial class Expwop
    {
        public ExpwopDoc Doc { get; set; }

        /// <summary>
        /// type of document
        /// </summary>
        public Doctyp Doctyp { get; set; }

        public List<ExpwopItem> Items { get; set; }

        /// <summary>
        /// Refund eligibility
        /// </summary>
        public Boolean? Rfndelg { get; set; }

        public ExpwopSb Sb { get; set; }

        /// <summary>
        /// Upload action
        /// </summary>
        public Flag? Flag { get; set; }
    }

    public partial class ExpwopDoc
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

    public partial class ExpwopItem
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

    public partial class ExpwopSb
    {
        /// <summary>
        /// Shipping bill date
        /// </summary>
        public string Dt { get; set; }

        /// <summary>
        /// Shipping bill number
        /// </summary>
        public string Num { get; set; }

        /// <summary>
        /// port code
        /// </summary>
        public string Pcode { get; set; }
    }

    public partial class Expwp
    {
        public ExpwpDoc Doc { get; set; }

        /// <summary>
        /// type of document
        /// </summary>
        public Doctyp Doctyp { get; set; }

        public List<ExpwpItem> Items { get; set; }

        /// <summary>
        /// Refund eligibility
        /// </summary>
        public Boolean? Rfndelg { get; set; }

        public ExpwpSb Sb { get; set; }

        /// <summary>
        /// Upload action
        /// </summary>
        public Flag? Flag { get; set; }
    }

    public partial class ExpwpDoc
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

    public partial class ExpwpItem
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

    public partial class ExpwpSb
    {
        /// <summary>
        /// Shipping bill date
        /// </summary>
        public string Dt { get; set; }

        /// <summary>
        /// Shipping bill number
        /// </summary>
        public string Num { get; set; }

        /// <summary>
        /// port code
        /// </summary>
        public string Pcode { get; set; }
    }

    public partial class Impg
    {
        public List<ImpgDoc> Docs { get; set; }

        /// <summary>
        /// Place of Supply(Name of State/UT)
        /// </summary>
        public string Pos { get; set; }
    }

    public partial class ImpgDoc
    {
        public PurpleBoe Boe { get; set; }

        /// <summary>
        /// type of document
        /// </summary>
        public Impgdoctyp Doctyp { get; set; }

        public List<TentacledItem> Items { get; set; }

        /// <summary>
        /// Upload action
        /// </summary>
        public Flag? Flag { get; set; }
    }

    public partial class PurpleBoe
    {
        /// <summary>
        /// Bill date
        /// </summary>
        public string Dt { get; set; }

        /// <summary>
        /// Bill of entry
        /// </summary>
        public string Num { get; set; }

        /// <summary>
        /// port code
        /// </summary>
        public string Pcode { get; set; }

        /// <summary>
        /// Bill value
        /// </summary>
        public double? Val { get; set; }
    }

    public partial class TentacledItem
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

    public partial class Impgsez
    {
        /// <summary>
        /// Counterparty GSTIN
        /// </summary>
        public string Ctin { get; set; }

        public List<ImpgsezDoc> Docs { get; set; }
    }

    public partial class ImpgsezDoc
    {
        public FluffyBoe Boe { get; set; }

        /// <summary>
        /// type of document
        /// </summary>
        public Impgdoctyp Doctyp { get; set; }

        public List<StickyItem> Items { get; set; }

        /// <summary>
        /// Place of Supply(Name of State/UT)
        /// </summary>
        public string Pos { get; set; }

        /// <summary>
        /// Refund eligibility
        /// </summary>
        public Boolean? Rfndelg { get; set; }

        /// <summary>
        /// Upload action
        /// </summary>
        public Flag? Flag { get; set; }
    }

    public partial class FluffyBoe
    {
        /// <summary>
        /// Bill date
        /// </summary>
        public string Dt { get; set; }

        /// <summary>
        /// Bill of entry
        /// </summary>
        public string Num { get; set; }

        /// <summary>
        /// port code
        /// </summary>
        public string Pcode { get; set; }

        /// <summary>
        /// Bill value
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

    public partial class Imp
    {
        public List<ImpItem> Items { get; set; }

        /// <summary>
        /// Place of Supply(Name of State/UT)
        /// </summary>
        public string Pos { get; set; }

        /// <summary>
        /// Refund eligibility
        /// </summary>
        public Boolean? Rfndelg { get; set; }

        /// <summary>
        /// Upload action
        /// </summary>
        public Flag? Flag { get; set; }
    }

    public partial class ImpItem
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

    public partial class Mi
    {
        /// <summary>
        /// Counterparty GSTIN
        /// </summary>
        public string Ctin { get; set; }

        public List<MiDoc> Docs { get; set; }
        public Tblref Tblref { get; set; }
    }

    public partial class MiDoc
    {
        /// <summary>
        /// Claim Refund
        /// </summary>
        public Boolean? Clmrfnd { get; set; }

        /// <summary>
        /// differential percentage
        /// </summary>
        public double? Diffprcnt { get; set; }

        public TentacledDoc Doc { get; set; }

        /// <summary>
        /// type of document
        /// </summary>
        public Doctyp Doctyp { get; set; }

        public List<IndigoItem> Items { get; set; }

        /// <summary>
        /// Place of Supply(Name of State/UT)
        /// </summary>
        public string Pos { get; set; }

        /// <summary>
        /// Supply Covered Under Sec 7 of IGST Act
        /// </summary>
        public Boolean? Sec7Act { get; set; }

        /// <summary>
        /// Upload action
        /// </summary>
        public Flag? Flag { get; set; }
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

    public partial class IndigoItem
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

    public partial class Rev
    {
        /// <summary>
        /// Counterparty GSTIN
        /// </summary>
        public string Ctin { get; set; }

        public List<RevDoc> Docs { get; set; }
    }

    public partial class RevDoc
    {
        /// <summary>
        /// differential percentage
        /// </summary>
        public double? Diffprcnt { get; set; }

        public List<IndecentItem> Items { get; set; }

        /// <summary>
        /// Place of Supply(Name of State/UT)
        /// </summary>
        public string Pos { get; set; }

        /// <summary>
        /// Refund eligibility
        /// </summary>
        public Boolean? Rfndelg { get; set; }

        /// <summary>
        /// Supply Covered Under Sec 7 of IGST Act
        /// </summary>
        public Boolean? Sec7Act { get; set; }

        /// <summary>
        /// Upload action
        /// </summary>
        public Flag? Flag { get; set; }
    }

    public partial class IndecentItem
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
        public StickyDoc Doc { get; set; }

        /// <summary>
        /// type of document
        /// </summary>
        public Doctyp Doctyp { get; set; }

        public List<HilariousItem> Items { get; set; }

        /// <summary>
        /// Place of Supply(Name of State/UT)
        /// </summary>
        public string Pos { get; set; }

        /// <summary>
        /// Refund eligibility
        /// </summary>
        public Boolean? Rfndelg { get; set; }

        /// <summary>
        /// Upload action
        /// </summary>
        public Flag? Flag { get; set; }
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

    public partial class HilariousItem
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
        /// Claim Refund
        /// </summary>
        public Boolean? Clmrfnd { get; set; }

        /// <summary>
        /// differential percentage
        /// </summary>
        public double? Diffprcnt { get; set; }

        public IndigoDoc Doc { get; set; }

        /// <summary>
        /// type of document
        /// </summary>
        public Doctyp Doctyp { get; set; }

        public List<AmbitiousItem> Items { get; set; }

        /// <summary>
        /// Place of Supply(Name of State/UT)
        /// </summary>
        public string Pos { get; set; }

        /// <summary>
        /// Refund eligibility
        /// </summary>
        public Boolean? Rfndelg { get; set; }

        /// <summary>
        /// Upload action
        /// </summary>
        public Flag? Flag { get; set; }
    }

    public partial class IndigoDoc
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

    public partial class AmbitiousItem
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
    /// type of document
    /// </summary>
    public enum Doctyp { C, D, I };

    /// <summary>
    /// Upload action
    /// </summary>
    public enum Flag { D };

    /// <summary>
    /// Refund eligibility
    ///
    /// Supply Covered Under Sec 7 of IGST Act
    ///
    /// Claim Refund
    /// </summary>
    public enum Boolean { N, Y };

    /// <summary>
    /// type of document
    /// </summary>
    public enum Impgdoctyp { B };

    public enum Tblref { The3B, The3E, The3F, The3G };

}
