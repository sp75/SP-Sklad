using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP.Reports.Models.Views
{
    class rep_50
    {
        [Display(Name = "Код товару")]
        public int MatId { get; set; }
        [Display(Name = "Товар")]
        public string MatName { get; set; }
        [Display(Name = "Од. вим.")]
        public string MeasureName { get; set; }
        [Display(Name = "Контрагент")]
        public string KaName { get; set; }

        [Display(Name = "Замовлено, к-сть")]
        public Nullable<decimal> OrderedAmount { get; set; }
        [Display(Name = "Замовлено, грн")]
        public Nullable<decimal> OrderedTotal { get; set; }
        [Display(Name = "Відгружено, к-сть")]
        public Nullable<decimal> AmountOut { get; set; }
        [Display(Name = "Відгружено, грн")]
        public Nullable<decimal> TotalOut { get; set; }
        [Display(Name = "Повернуто, к-сть")]
        public Nullable<decimal> ReturnAmount { get; set; }
        [Display(Name = "Повернуто, грн")]
        public Nullable<decimal> ReturnTotal { get; set; }
        [Display(Name = "Група товарів")]
        public string MatGrpName { get; set; }
    }

}
