using System;
using System.ComponentModel.DataAnnotations;

namespace SP.Base.Models
{
    public partial class REP_1_Result
    {
        [Key]
        public int MatId { get; set; }
        public string Name { get; set; }
        public string BarCode { get; set; }
        public Nullable<int> GrpId { get; set; }
        public string Artikul { get; set; }
        public Nullable<decimal> Amount { get; set; }
        public string MsrName { get; set; }
        public Nullable<decimal> Summ { get; set; }
        public Nullable<decimal> SumPrice { get; set; }
        public string GrpName { get; set; }
    }
}
