using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace Risersoft.API.GSTN.GSTR3B
{

    public class Pditc
    {


        [Display(Name = "IGST paid using igst")]
        public double i_pdi { get; set; }

        [Display(Name = "IGST paid using Cgst")]
        public double i_pdc { get; set; }

        [Display(Name = "IGST paid using Sgst")]
        public double i_pds { get; set; }

        [Display(Name = "CGST paid using igst")]
        public double c_pdi { get; set; }

        [Display(Name = "CGST paid using cgst")]
        public double c_pdc { get; set; }

        [Display(Name = "SGST paid using igst ")]
        public double s_pdi { get; set; }

        [Display(Name = "SGST paid using sgst ")]
        public double s_pds { get; set; }

        [Display(Name = "Cess paid using cess")]
        public double cs_pdcs { get; set; }
    }
    public class Pdcash

    {

        [Display(Name = "Liability ledger Id")]
        public double liab_ldg_id { get; set; }

        [Display(Name = "Transaction type")]
        public double trans_typ { get; set; }

        [Display(Name = "Igst paid")]
        public double ipd { get; set; }

        [Display(Name = "Cgst paid")]
        public double cpd { get; set; }

        [Display(Name = "Sgst paid ")]
        public double spd { get; set; }

        [Display(Name = "Cess paid")]
        public double cspd { get; set; }

        [Display(Name = "Interest Paid  against IGST")]
        public double i_intrpd { get; set; }

        [Display(Name = "Interest Paid  against CGST")]
        public double c_intrpd { get; set; }


        [Display(Name = "Interest Paid  against SGST")]
        public double s_intrpd { get; set; }

        [Display(Name = "Interest Paid  against CESS")]
        public double cs_intrpd { get; set; }

        [Display(Name = "Late Fee Paid against CGST")]
        public double c_lfeepd { get; set; }

        [Display(Name = "Late Fee Paid against SGST")]
        public double s_lfeepd { get; set; }

    }
    public class Details
    {
        [Display(Name = "Interest payable")]
        public double intr { get; set; }

        [Display(Name = "Tax payable")]
        public double tx { get; set; }

        [Display(Name = "Late fee payable")]
        public double fee { get; set; }


    }


    public class Detailspy
    {

        [Display(Name = "Interest payable")]
        public double intr { get; set; }

        [Display(Name = "Tax payable")]
        public double tx { get; set; }

    }

    public class Txpy
    {

        [Display(Name = "Liability ledgerid ")]
        public double liab_ldg_id { get; set; }

        [Display(Name = "transaction type")]
        public double trans_typ { get; set; }

        [Display(Name = "transaction description")]
        public string trans_desc { get; set; }

        [Display(Name = "SGST details")]
        public Details sgst { get; set; }


        [Display(Name = "IGST details")]
        public Detailspy igst { get; set; }

        [Display(Name = "CGST details")]
        public Details cgst { get; set; }

        [Display(Name = "CESS details")]
        public Detailspy cess { get; set; }


    }

    public class Taxpmt
    {

        [Display(Name = "Tax Payable")]
        public List<Txpy> tx_py { get; set; }

        [Display(Name = "List of Paid Through Cash")]
        public List<Pdcash> pdcash { get; set; }

        [Display(Name = " Paid Through Credit")]
        public Pditc pditc { get; set; }


    }
}
