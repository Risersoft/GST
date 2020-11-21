using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;
using System.ComponentModel.DataAnnotations;
namespace GSTN.API.Library.Models.GstNirvana
{
    public class ConvertModel
    {
        [Display(Name = "Return")]
        public string ReturnKey { get; set; }
        [Display(Name = "Table")]
        public string TableName { get; set; }
        [Display(Name = "Type")]
        public int ConversionType { get; set; }
        [Display(Name = "Input")]
        [DataType(DataType.MultilineText)]
        public string TextToConvert { get; set; }
        [Display(Name = "Output")]
        [DataType(DataType.MultilineText)]
        public string TextConverted { get; set; }
    }
}