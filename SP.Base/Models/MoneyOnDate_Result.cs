using System;
using System.ComponentModel.DataAnnotations;

namespace SP.Base.Models
{
    public partial class MoneyOnDate_Result
    {
        [Key]
        public int Id { get; set; }
        public int SaldoType { get; set; }
        public System.DateTime OnDate { get; set; }
        public Nullable<int> AccId { get; set; }
        public int CurrId { get; set; }
        public decimal Saldo { get; set; }
        public Nullable<int> CashId { get; set; }
        public Nullable<decimal> SaldoDef { get; set; }
        public string Currency { get; set; }
    }
}
