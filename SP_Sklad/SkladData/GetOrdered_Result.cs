//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SP_Sklad.SkladData
{
    using System;
    
    public partial class GetOrdered_Result
    {
        public decimal Remain { get; set; }
        public decimal Ordered { get; set; }
        public decimal Rsv { get; set; }
        public int PosId { get; set; }
        public int WId { get; set; }
        public string WhName { get; set; }
        public System.DateTime OnDate { get; set; }
        public Nullable<System.DateTime> PosDate { get; set; }
        public Nullable<decimal> Price { get; set; }
        public Nullable<int> CurrId { get; set; }
        public int WType { get; set; }
        public string Num { get; set; }
        public int WbillId { get; set; }
        public Nullable<int> DocId { get; set; }
        public Nullable<System.DateTime> ToDate { get; set; }
        public Nullable<decimal> OnValue { get; set; }
        public string CurrName { get; set; }
        public Nullable<int> KaId { get; set; }
        public string KaName { get; set; }
        public string Notes { get; set; }
        public string CertNum { get; set; }
        public System.Guid Id { get; set; }
    }
}
