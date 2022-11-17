using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkladEngine.DBFunction.Models
{
    public class GetMatRemainByWh_Result
    {
        public int WId { get; set; }
        public DateTime OnDate { get; set; }
        public decimal? Remain { get; set; }
        public decimal? Rsv { get; set; }
        public decimal? CurRemain { get; set; }
        public decimal? AvgPrice { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
        public decimal? SumRemain { get; set; }
        public decimal? ORsv { get; set; }
        public decimal? Ordered { get; set; }
    }
}
