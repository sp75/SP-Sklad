using System;
using System.ComponentModel.DataAnnotations;

namespace SP.Base.Models
{
    public partial class REP_10_Result
    {
        [Key]
        public int MatId { get; set; }
        public string Name { get; set; }
        public Nullable<int> GrpId { get; set; }
        public string BarCode { get; set; }
        public string Artikul { get; set; }
        public string GrpName { get; set; }
        public string MsrName { get; set; }
        public Nullable<decimal> AvgPriceStart { get; set; }
        public Nullable<decimal> RemainStart { get; set; }
        public Nullable<decimal> SummStart { get; set; }
        public Nullable<decimal> AvgPriceEnd { get; set; }
        public Nullable<decimal> RemainEnd { get; set; }
        public Nullable<decimal> SummEnd { get; set; }
        public Nullable<decimal> AmountIn { get; set; }
        public Nullable<decimal> SummIn { get; set; }
        public Nullable<decimal> AmountOut { get; set; }
        public Nullable<decimal> SummOut { get; set; }
    }
}
