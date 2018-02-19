using System;

namespace SkladEngine.ModelViews
{
    public class GetPosOutView
    {
        public int PosId { get; set; }
        public int WbillId { get; set; }
        public int WType { get; set; }
        public string Num { get; set; }
        public DateTime OnDate { get; set; }
        public int? DocId { get; set; }
        public int? KaId { get; set; }
        public string KaName { get; set; }
        public int WID { get; set; }
        public string WhName { get; set; }
        public int MatId { get; set; }
        public string MatName { get; set; }
        public string Artikul { get; set; }
        public decimal Amount { get; set; }
        public DateTime? ToDate { get; set; }
        public decimal? Price { get; set; }
        public decimal? OnValue { get; set; }
        public int? CurrId { get; set; }
        public string CurrName { get; set; }
        public int Checked { get; set; }
        public string Measure { get; set; }
        public decimal? Nds { get; set; }
        public string BarCode { get; set; }
        public decimal? Discount { get; set; }
        public decimal? BasePrice { get; set; }
        public decimal? ReturnAmount { get; set; }
        public decimal? Remain { get; set; }
    }
}
