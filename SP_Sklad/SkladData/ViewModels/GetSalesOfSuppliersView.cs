using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP_Sklad.SkladData.ViewModels
{
    public class GetSalesOfSuppliersView
    {
        public int MatId { get; set; }
        public string MatName { get; set; }
        public int SupplierId { get; set; }
        public string SupplierName { get; set; }
        public decimal Amount { get; set; }
        public decimal? CurRemain { get; set; }
        public string MeasureName { get; set; }
        public int WId { get; set; }

    }
}
