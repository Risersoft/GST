using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using Risersoft.API.GSTN.GSTR7;

namespace GSTN.API.Library.Models.ITC
{
    public class GetITCSummary
    {



        [Display(Name = "GSTIN of the taxpayer")]
        [MaxLength(15)]
        [MinLength(15)]
        [RegularExpression("^[0-9]{2}[a-zA-Z]{5}[0-9]{4}[a-zA-Z]{1}[1-9A-Za-z]{1}[Zz1-9A-Ja-j]{1}[0-9a-zA-Z]{1}$")]
        public string gstin { get; set; }

       
        [Display(Name = "Return Type")]
        [MaxLength(8)]
        public string rtn_typ { get; set; }

       
        [Display(Name = "Date of Filing, present only in case of ITC03-4A Return Type.")]
        public string dof { get; set; }

       
        [Display(Name = "Composition ARN, Present in case of ITC03-4A return Type. ARN of the application filed to opt composition")]
        [MaxLength(15)]
        public string comp_arn { get; set; }

        [Display(Name = "Date of Exemption, present only in case of ITC03-4B Return Type.")]
        public string doe { get; set; }

        [Display(Name = "Section 5A Summary")]
        public ITCSummary sec_5a { get; set; }

        [Display(Name = "Section 5B Summary")]
        public ITCSummary sec_5b { get; set; }

        [Display(Name = "Section 5C Summary")]
        public ITCSummary sec_5c { get; set; }

        [Display(Name = "Section 5D Summary")]
        public ITCSummary sec_5d { get; set; }

        [Display(Name = "Section 5E Summary")]
        public ITCSummary sec_5e { get; set; }

        [Display(Name = "Tax Payable Details")]
        public ITCTaxPay tax_pay { get; set; }

        [Display(Name = "Tax Paid Details")]
        public ITCTaxPaid tax_paid { get; set; }

        [Display(Name = "CA Certificate Details")]
        public CADetailsComp attachment { get; set; }
    }
    public class ITCSummary
    {
       
        [Display(Name = "Invoice Check sum value ")]
        [MaxLength(64)]
        public string chksum { get; set; }


       

       
        [Display(Name = "Total Record Count")]
        public decimal ttl_rec { get; set; }
       
        [Display(Name = "Total records value")]
        public decimal ttl_val { get; set; }


       
        [Display(Name = "Total IGST ")]
        public decimal ttl_igst { get; set; }

        
        [Display(Name = "Total CGST ")]
        public decimal ttl_cgst { get; set; }


        [Display(Name = "Total SGST ")]
        public decimal ttl_sgst { get; set; }


        [Display(Name = "Total Cess")]
        public decimal ttl_cess { get; set; }

       
        [Display(Name = "Total taxable value of records")]
        public decimal ttl_tax { get; set; }


    }

    public class ITCTaxPay
    {
      

        [Display(Name = "Tax Payable")]   
        public List<ITCTaxPayComp> tax_pay { get; set; }

    }

    public class ITCTaxPayComp
    {



        
        [Display(Name = "Description in transaction")]
        [MaxLength(100)]
        public string tran_desc { get; set; }

      
        [Display(Name = "Liability id")]
        public decimal liab_id { get; set; }


        [Display(Name = "transaction code")]
        public decimal tran_cd { get; set; }


        [Display(Name = "Igst tax payable")]
        public MinorHeads igst { get; set; }

        [Display(Name = "cgst tax payable")]
        public MinorHeads cgst { get; set; }

        [Display(Name = "sgst tax payable")]
        public MinorHeads sgst { get; set; }

        [Display(Name = "cess tax payable")]
        public MinorHeads cess { get; set; }



    }
    public class ITCTaxPaid
    {

        [Display(Name = "Tax Paid through Cash")]
        public List<ITCTaxPaidCash> pd_by_cash { get; set; }

        [Display(Name = "Tax Paid through ITC")]
        public List<TaxPaidByItc> pd_by_itc { get; set; }

    }






    public class ITCTaxPaidCash
    {
        [Display(Name = "Description in transaction")]
        [MaxLength(100)]
        public string debit_id { get; set; }

       
        [Display(Name = "Liability id")]
        public decimal liab_id { get; set; }


        [Display(Name = "transaction code")]
        public decimal trancd { get; set; }

        [Display(Name = "transaction date")]
        [RegularExpression("^((0[1-9]|[12][0-9]|3[01])[-](0[1-9]|1[012])[-]((19|20)\\d\\d))*$")]
        public string trandate { get; set; }

        [Display(Name = "Igst tax payable")]
        public MinorHeads igst { get; set; }

        [Display(Name = "cgst tax payable")]
        public MinorHeads cgst { get; set; }

        [Display(Name = "sgst tax payable")]
        public MinorHeads sgst { get; set; }

        [Display(Name = "cess tax payable")]
        public MinorHeads cess { get; set; }


    }



    public class TaxPaidByItc
    {
        [Display(Name = "Description in transaction")]
        [MaxLength(100)]
        public string debit_id { get; set; }

       
        [Display(Name = "Liability id")]
        public decimal liab_id { get; set; }


        [Display(Name = "transaction code")]
        public decimal trancd { get; set; }

        [Display(Name = "transaction date")]
        [RegularExpression("^((0[1-9]|[12][0-9]|3[01])[-](0[1-9]|1[012])[-]((19|20)\\d\\d))*$")]
        public string trandate { get; set; }



        [Display(Name = "IGST paid using igst")]
        public decimal igst_igst_amt { get; set; }

        [Display(Name = "IGST paid using Cgst")]
        public decimal igst_cgst_amt { get; set; }

        [Display(Name = "IGST paid using Sgst")]
        public decimal igst_sgst_amt { get; set; }

        [Display(Name = "SGST paid using sgst ")]
        public decimal sgst_sgst_amt { get; set; }

        [Display(Name = "SGST paid using igst ")]
        public decimal sgst_igst_amt { get; set; }

        [Display(Name = "CGST paid using cgst")]
        public decimal cgst_cgst_amt { get; set; }

        [Display(Name = "CGST paid using igst")]
        public decimal cgst_igst_amt { get; set; }

        [Display(Name = "Cess paid using cess")]
        public decimal cess_cess_amt { get; set; }

    }



    public class CADetailsComp
    {

        [Display(Name = "Uploaded Document Id")]
      
        public string doc_id { get; set; }

       
        [Display(Name = "Hash of Document uploaded")]
        public string hash { get; set; }


        [Display(Name = "CA Details")]
        public CADetails ca_details { get; set; }

       
       
    }

    public class CADetails
    {

        [Display(Name = "Name of the Firm issuing certificate")]

        public string firm { get; set; }

     
        [Display(Name = "Name of the certifying Chartered Accountant/Cost Accountant")]
        public string caname { get; set; }


        [Display(Name = "Membership number")]
        public string member { get; set; }

        [Display(Name = "Date of issuance of certificate")]
        [RegularExpression("^((0[1-9]|[12][0-9]|3[01])[-](0[1-9]|1[012])[-]((19|20)\\d\\d))*$")]
        public string cadt { get; set; }
    }
}
