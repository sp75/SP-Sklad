using System;
using System.ComponentModel.DataAnnotations;

namespace SP.Base.Models
{
    public partial class REP_3_14_Result
    {
        public string BarCode { get; set; }
        public Nullable<int> GrpId { get; set; }
        [Key]
        public int MatId { get; set; }
        public string Name { get; set; }
        public string Artikul { get; set; }
        public Nullable<decimal> Amount { get; set; }
        public Nullable<decimal> Summ { get; set; }
        public string ShortName { get; set; }
        public Nullable<decimal> ReturnAmountIn { get; set; }
        public Nullable<decimal> ReturnSummIn { get; set; }
        public string DocNum { get; set; }
        public string TypeName { get; set; }
        public System.DateTime OnDate { get; set; }
        public Nullable<int> KaId { get; set; }
        public string PersonName { get; set; }
    }
}
