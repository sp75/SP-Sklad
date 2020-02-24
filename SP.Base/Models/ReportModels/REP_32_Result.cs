using System;
using System.ComponentModel.DataAnnotations;

namespace SP.Base.Models
{
    public partial class REP_32_Result
    {
        [Key]
        public int PosId { get; set; }
        public decimal Amount { get; set; }
        public Nullable<decimal> Total { get; set; }
        public string MatName { get; set; }
        public string RouteName { get; set; }
        public string DriverName { get; set; }
        public int DriverId { get; set; }
        public int RouteId { get; set; }
        public int KaId { get; set; }
        public string KaName { get; set; }
    }
}
