using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Risersoft.API.GSTN.GSTR3
{
    public class InterestLatefee
    {
       

        

        [Required]
        [Display(Name = "Interest payable ")]
        public Interstpay intr_offset { get; set; }
        [Required]
        [Display(Name = "Late Fee payable")]
        public Latefeedet lfee_offset { get; set; }

       
    }



    public class Interstpay
    {


        [Required]
        [Display(Name = "Interest payable in IGST head")]
        public double i_int { get; set; }

        [Required]
        [Display(Name = "Interest payable in CGST head")]
        public double c_int { get; set; }

        [Required]
        [Display(Name = "Interest payable in SGST head")]
        public double s_int { get; set; }

        [Required]
        [Display(Name = "Interest payable in CESS head")]
        public double cs_int { get; set; }
    }
    public class Latefeedet
    {

        [Required]
        [Display(Name = "Late Fee payable in CGST head")]
        public double c_lfee { get; set; }

        [Required]
        [Display(Name = "Late Fee payable in SGST head")]
        public double s_lfee { get; set; }

        [Required]
        [Display(Name = "List of Paid through Cash details post utilization of cash in interest")]
        public List<Pdint> pdint { get; set; }

        [Required]
        [Display(Name = "List of Paid through Cash details post utilization of cash in late fee")]
        public List<Pdlfee> pdlfee { get; set; }
    }





    public class Pdint
    {

        [Required]
        [Display(Name = "Igst paid Interest")]
        public double i_pdint { get; set; }

        [Required]
        [Display(Name = "Cgst paid Interest")]
        public double c_pdint { get; set; }

        [Required]
        [Display(Name = "Sgst paid Interest")]
        public double s_pdint { get; set; }

        [Required]
        [Display(Name = "Cess paid Interest")]
        public double cs_pdint { get; set; }
    }
    public class Pdlfee
    {

        [Required]
        [Display(Name = "Cgst paid Late fee")]
        public double c_pdlfee { get; set; }

        [Required]
        [Display(Name = "Sgst paid Late fee")]
        public double s_pdlfee { get; set; }

       
    }
}