using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SP.Base.Models
{
    public partial class REP_50_Result
    {
        public Nullable<int> MatGrpId { get; set; }
        [Key]
        [Column(Order = 1)]
        public int MatId { get; set; }
        public string MatName { get; set; }
        public string MeasureName { get; set; }
        [Key]
        [Column(Order = 2)]
        public Nullable<int> KaId { get; set; }
        public string KaName { get; set; }
        public Nullable<System.Guid> KaGrpId { get; set; }
        public Nullable<decimal> OrderedAmount { get; set; }
        public Nullable<decimal> OrderedTotal { get; set; }
        public Nullable<decimal> AmountOut { get; set; }
        public Nullable<decimal> TotalOut { get; set; }
        public Nullable<decimal> ReturnAmount { get; set; }
        public Nullable<decimal> ReturnTotal { get; set; }
        public string MatGrpName { get; set; }
    }
}
