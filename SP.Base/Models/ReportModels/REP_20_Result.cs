using System;
using System.ComponentModel.DataAnnotations;

namespace SP.Base.Models
{
    public partial class REP_20_Result
    {
        public int GrpId { get; set; }
        public string GrpName { get; set; }
        [Key]
        public int SvcId { get; set; }
        public string Name { get; set; }
        public string Artikul { get; set; }
        public Nullable<decimal> Amount { get; set; }
        public string MsrName { get; set; }
        public Nullable<decimal> Summ { get; set; }
    }
}
