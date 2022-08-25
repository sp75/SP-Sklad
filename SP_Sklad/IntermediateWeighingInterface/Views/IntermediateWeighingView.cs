using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP_Sklad.IntermediateWeighingInterface.Views
{
    public class IntermediateWeighingView
    {
        public Guid Id { get; set; }
        public string Num { get; set; }
        public DateTime OnDate { get; set; }
        public int Checked { get; set; }
        public string Notes { get; set; }
        public int PersonId { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int? UpdatedBy { get; set; }
        public Guid? SessionId { get; set; }
        public int WbillId { get; set; }
        public string Amount { get; set; }
        public string RecipeName { get; set; }
        public string WbNum { get; set; }
        public string PersonName { get; set; }
        public int WbChecked { get; set; }
        public string WbOnDate { get; set; }
        public int? RecipeCount { get; set; }
        public byte[] BMP { get; set; }
        public bool? IsDone { get; set; }

    }
}
