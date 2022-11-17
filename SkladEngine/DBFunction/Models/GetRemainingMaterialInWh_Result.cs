using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkladEngine.DBFunction.Models
{
   public class GetRemainingMaterialInWh_Result
    {
        public decimal Remain { get; set; }
        public decimal Rsv { get; set; }
        public decimal CurRemain { get; set; }
    }
}
