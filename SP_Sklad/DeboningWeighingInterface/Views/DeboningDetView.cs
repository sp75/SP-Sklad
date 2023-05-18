using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP_Sklad.DeboningWeighingInterface.Views
{
    public class DeboningDetView
    {
        public int DebId { get; set; }
        public int WBillId { get; set; }
        public int MatId { get; set; }
        public decimal Amount { get; set; }
        public string MatName { get; set; }
        public string TotalWeighing { get; set; }
        public string MsrName { get; set; }
        public byte[] BMP { get; set; }
    }
}
