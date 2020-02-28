using System;
using System.ComponentModel.DataAnnotations;

namespace SP.Base.Models
{
    public partial class REP_41_Result
    {
        [Key]
        public int KaId { get; set; }
        public string Name { get; set; }
        public string KontragentGroupName { get; set; }
        public Nullable<decimal> Amount_1 { get; set; }
        public Nullable<decimal> Amount_2 { get; set; }
        public Nullable<decimal> Amount_3 { get; set; }
        public Nullable<decimal> Amount_4 { get; set; }
        public Nullable<decimal> Amount_5 { get; set; }
        public Nullable<decimal> Amount_6 { get; set; }
        public Nullable<decimal> Amount_7 { get; set; }
        public Nullable<decimal> Amount_8 { get; set; }
        public Nullable<decimal> Amount_9 { get; set; }
        public Nullable<decimal> Amount_10 { get; set; }
        public Nullable<decimal> Amount_11 { get; set; }
        public Nullable<decimal> Amount_12 { get; set; }
        public Nullable<decimal> Amount_Total { get; set; }
    }
}
