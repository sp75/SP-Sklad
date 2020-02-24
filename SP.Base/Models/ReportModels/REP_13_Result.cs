using System;
using System.ComponentModel.DataAnnotations;

namespace SP.Base.Models
{
    public partial class REP_13_Result
    {
        [Key]
        public int MatId { get; set; }
        public string BarCode { get; set; }
        public Nullable<int> GrpId { get; set; }
        public string GrpName { get; set; }
        public string Name { get; set; }
        public string Artikul { get; set; }
        public Nullable<decimal> AmountOut { get; set; }
        public string MsrName { get; set; }
        public Nullable<decimal> SummOut { get; set; }
        public Nullable<decimal> AmountIn { get; set; }
        public Nullable<decimal> SummIn { get; set; }
        public Nullable<decimal> AvgPrice { get; set; }
        public Nullable<decimal> Income { get; set; }
    }
}
