using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP_Sklad.SkladData.ViewModels
{
    public class MaterialMeasuresView
    {
        public int MId { get; set; }
        public string Name { get; set; }
        public decimal Amount { get; set; }
        public bool? UseInOrders { get; set; }
    }
}
