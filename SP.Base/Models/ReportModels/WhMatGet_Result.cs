using System;
using System.ComponentModel.DataAnnotations;

namespace SP.Base.Models
{
    public partial class WhMatGet_Result
    {
        [Key]
        public long? RecNo { get; set; }
        public int MatId { get; set; }
        public decimal? Remain { get; set; }
        public decimal? Rsv { get; set; }
        public string MatName { get; set; }
        public string MsrName { get; set; }
        public string Artikul { get; set; }
        public decimal? AvgPrice { get; set; }
        public string GrpName { get; set; }
        public int? Num { get; set; }
        public string BarCode { get; set; }
        public string Country { get; set; }
        public string Producer { get; set; }
        public decimal? MinReserv { get; set; }
        public decimal? Ordered { get; set; }
        public decimal? ORsv { get; set; }
        public int? IsSerial { get; set; }
        public int? OutGrpId { get; set; }
        public decimal? CurRemain { get; set; }
        public decimal? SumRemain { get; set; }
        public int MId { get; set; }
        public decimal? GrpNum { get; set; }
    }
}
