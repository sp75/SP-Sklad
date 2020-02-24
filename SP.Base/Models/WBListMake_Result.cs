using System;
using System.ComponentModel.DataAnnotations;

namespace SP.Base.Models
{
    public partial class WBListMake_Result
    {
        [Key]
        public System.Guid Id { get; set; }
        public int WbillId { get; set; }
        public string Num { get; set; }
        public System.DateTime OnDate { get; set; }
        public Nullable<int> CurrId { get; set; }
        public string Reason { get; set; }
        public int Checked { get; set; }
        public Nullable<decimal> SummAll { get; set; }
        public int WType { get; set; }
        public Nullable<decimal> Nds { get; set; }
        public Nullable<int> PersonId { get; set; }
        public string Notes { get; set; }
        public Nullable<int> DocId { get; set; }
        public Nullable<decimal> SummInCurr { get; set; }
        public string CurrName { get; set; }
        public Nullable<decimal> CurrRate { get; set; }
        public string PersonName { get; set; }
        public string AttNum { get; set; }
        public Nullable<System.DateTime> AttDate { get; set; }
        public string Received { get; set; }
        public Nullable<System.DateTime> ToDate { get; set; }
        public int DefNum { get; set; }
        public string RecipeName { get; set; }
        public decimal MatRecipeOut { get; set; }
        public Nullable<decimal> Deviation { get; set; }
        public string MatName { get; set; }
        public decimal AmountIn { get; set; }
        public int MatId { get; set; }
        public string MsrName { get; set; }
        public string KaName { get; set; }
        public string MakedPerson { get; set; }
        public int MId { get; set; }
        public Nullable<System.DateTime> OnDateOut { get; set; }
        public Nullable<decimal> AmountOut { get; set; }
        public Nullable<decimal> AmountBrutto { get; set; }
        public Nullable<System.DateTime> WriteOnDate { get; set; }
        public string FromWh { get; set; }
        public Nullable<int> FromWId { get; set; }
        public string DocStatus { get; set; }
        public Nullable<decimal> Price { get; set; }
        public Nullable<decimal> ShippedAmount { get; set; }
        public Nullable<decimal> Tara { get; set; }
        public Nullable<decimal> SaldoShipped { get; set; }
        public Nullable<decimal> MatOut { get; set; }
    }
}
