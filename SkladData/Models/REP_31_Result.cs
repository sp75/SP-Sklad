//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SkladData.Models
{
    using System;
    
    public partial class REP_31_Result
    {
        public int MatId { get; set; }
        public string MatName { get; set; }
        public Nullable<int> GrpId { get; set; }
        public string GrpName { get; set; }
        public Nullable<decimal> AmountOrd { get; set; }
        public Nullable<decimal> TotalOrd { get; set; }
        public string MsrName { get; set; }
        public string BarCode { get; set; }
        public Nullable<decimal> AmountOut { get; set; }
        public Nullable<decimal> TotalOut { get; set; }
        public string PersonName { get; set; }
        public Nullable<System.DateTime> OnDate { get; set; }
    }
}