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
    
    public partial class GetWayBillMakeDet_Result
    {
        public int Num { get; set; }
        public int PosId { get; set; }
        public decimal Amount { get; set; }
        public Nullable<decimal> Price { get; set; }
        public Nullable<System.DateTime> OnDate { get; set; }
        public Nullable<int> Checked { get; set; }
        public string MatName { get; set; }
        public int MatId { get; set; }
        public string MsrName { get; set; }
        public string WhName { get; set; }
        public Nullable<int> wid { get; set; }
        public Nullable<int> GrpId { get; set; }
        public string GroupName { get; set; }
        public Nullable<decimal> BasePrice { get; set; }
        public Nullable<decimal> Total { get; set; }
        public Nullable<decimal> AmountByRecipe { get; set; }
        public Nullable<int> Rsv { get; set; }
        public int IsIntermediateWeighing { get; set; }
        public Nullable<decimal> AmountIntermediateWeighing { get; set; }
        public string Notes { get; set; }
        public Nullable<System.DateTime> UpdateAt { get; set; }
        public Nullable<int> MatDefWId { get; set; }
        public Nullable<int> DefectsClassifierId { get; set; }
        public string DefectsClassifierName { get; set; }
        public Nullable<int> RawMaterialTypeId { get; set; }
        public string RawMaterialTypeName { get; set; }
    }
}
