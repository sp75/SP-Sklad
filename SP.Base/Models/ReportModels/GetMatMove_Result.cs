using System;
using System.ComponentModel.DataAnnotations;

namespace SP.Base.Models
{
    public partial class GetMatMove_Result
    {
        [Key]
        public Nullable<System.Guid> Id { get; set; }
        public Nullable<int> WBillId { get; set; }
        public Nullable<int> WType { get; set; }
        public Nullable<System.DateTime> OnDate { get; set; }
        public string Num { get; set; }
        public Nullable<decimal> Amount { get; set; }
        public Nullable<decimal> Price { get; set; }
        public string CurrName { get; set; }
        public Nullable<decimal> OnValue { get; set; }
        public Nullable<int> PosId { get; set; }
        public string KAgentName { get; set; }
        public string FromWh { get; set; }
        public Nullable<int> FromWId { get; set; }
        public string ToWh { get; set; }
        public Nullable<int> ToWId { get; set; }
        public Nullable<int> DocId { get; set; }
        public Nullable<decimal> Remain { get; set; }
        public string MsrName { get; set; }
        public Nullable<decimal> SumIn { get; set; }
        public Nullable<decimal> SumOut { get; set; }
        public Nullable<decimal> AmountIn { get; set; }
        public Nullable<decimal> AmountOut { get; set; }
        public Nullable<decimal> AvgPrice { get; set; }
        public string TypeName { get; set; }
        public string WhName { get; set; }
        public Nullable<System.Guid> KAgentGroupId { get; set; }
    }
}
