using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkladEngine.DBFunction.Models
{
  public  class MoneyTurnoverView
    {
        public int Id { get; set; }
        public Nullable<int> DocType { get; set; }
        public Nullable<System.DateTime> OnDate { get; set; }
        public string KaName { get; set; }
        public string CurrencyName { get; set; }
        public Nullable<decimal> Total { get; set; }
        public Nullable<decimal> TotalDef { get; set; }
        public Nullable<int> PayDocId { get; set; }
        public Nullable<int> Checked { get; set; }
        public string Num { get; set; }
        public Nullable<int> KaId { get; set; }
        public string PtypeName { get; set; }
        public string BankName { get; set; }
        public string ChargeName { get; set; }
        public string AccNum { get; set; }
        public string CashName { get; set; }
        public Nullable<decimal> OnValue { get; set; }
        public Nullable<int> DocId { get; set; }
    }
}
