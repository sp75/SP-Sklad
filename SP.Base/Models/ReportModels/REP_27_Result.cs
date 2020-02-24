using System;
using System.ComponentModel.DataAnnotations;

namespace SP.Base.Models
{
    public partial class REP_27_Result
    {
        [Key]
        public int MatId { get; set; }
        public string MatName { get; set; }
        public Nullable<decimal> AmountOrd { get; set; }
        public Nullable<decimal> TotalOrd { get; set; }
        public Nullable<int> KaId { get; set; }
        public string KaName { get; set; }
        public string MsrName { get; set; }
        public string BarCode { get; set; }
        public Nullable<decimal> AmountOut { get; set; }
        public Nullable<decimal> TotalOut { get; set; }
        public string PersonName { get; set; }
    }
}
