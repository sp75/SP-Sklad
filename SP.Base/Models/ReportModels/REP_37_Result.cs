using System;
using System.ComponentModel.DataAnnotations;

namespace SP.Base.Models
{
    public partial class REP_37_Result
    {
        [Key]
        public int PosId { get; set; }
        public Nullable<int> Num { get; set; }
        public int MatId { get; set; }
        public string MatName { get; set; }
        public int GrpId { get; set; }
        public string GrpName { get; set; }
        public Nullable<decimal> AmountAll { get; set; }
        public Nullable<decimal> SumAll { get; set; }
        public Nullable<decimal> AmountOut { get; set; }
        public Nullable<decimal> Remain { get; set; }
        public string MsrName { get; set; }
        public Nullable<decimal> AvgPrice { get; set; }
        public Nullable<decimal> Losses { get; set; }
    }
}
