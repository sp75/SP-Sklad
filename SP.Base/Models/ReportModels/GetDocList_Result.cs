using System;
using System.ComponentModel.DataAnnotations;

namespace SP.Base.Models
{
    public partial class GetDocList_Result
    {
        [Key]
        public Nullable<System.Guid> Id { get; set; }
        public Nullable<int> WbillId { get; set; }
        public Nullable<int> WType { get; set; }
        public string Num { get; set; }
        public Nullable<System.DateTime> OnDate { get; set; }
        public Nullable<System.DateTime> ToDate { get; set; }
        public Nullable<int> KaId { get; set; }
        public string KaName { get; set; }
        public Nullable<decimal> OnValue { get; set; }
        public Nullable<int> CurrId { get; set; }
        public string CurrName { get; set; }
        public Nullable<int> Checked { get; set; }
        public Nullable<int> DocId { get; set; }
        public Nullable<decimal> NDS { get; set; }
        public Nullable<decimal> SummAll { get; set; }
        public Nullable<decimal> SummInCurr { get; set; }
        public Nullable<decimal> Saldo { get; set; }
        public string TypeName { get; set; }
        public string DocShortName { get; set; }
    }
}
