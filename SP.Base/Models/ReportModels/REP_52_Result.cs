using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SP.Base.Models
{
    public partial class REP_52_Result
    {
        public Nullable<int> GrpId { get; set; }
        [Key]
        [Column(Order = 1)]
        public int MatId { get; set; }
        public string MatName { get; set; }
        public string Artikul { get; set; }
        public Nullable<decimal> Amount { get; set; }
        public string MsrName { get; set; }
        public Nullable<decimal> SummTotal { get; set; }
        public string GrpName { get; set; }
        public string KaName { get; set; }
        [Key]
        [Column(Order = 2)]
        public Nullable<int> KaId { get; set; }
    }
}
