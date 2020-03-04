using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SP.Base.Models
{

    public partial class REP_27_Result
    {
        [Key]
        [Column(Order = 1)]
        public int MatId { get; set; }
        [Key]
        [Column(Order = 2)]
        public int? KaId { get; set; }
        public string MatName { get; set; }
        public decimal? AmountOrd { get; set; }
        public Nullable<decimal> TotalOrd { get; set; }
        public string KaName { get; set; }
        public string MsrName { get; set; }
        public string BarCode { get; set; }
        public Nullable<decimal> AmountOut { get; set; }
        public Nullable<decimal> TotalOut { get; set; }
        public string PersonName { get; set; }
    }
}
