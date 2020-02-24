using System;
using System.ComponentModel.DataAnnotations;

namespace SP.Base.Models
{
    public partial class MatRemainByWh_Result
    {
        [Key]
        public int WId { get; set; }
        public string Name { get; set; }
        public Nullable<decimal> Remain { get; set; }
        public Nullable<decimal> Rsv { get; set; }
        public Nullable<decimal> CurRemain { get; set; }
    }
}
