using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SP.Base.Models
{

    public partial class REP_27_Result
    {
        [Key]
        [Column(Order = 1)]
        [Display(AutoGenerateField = false, Name = "Код товару")]
        public int MatId { get; set; }
        [Key]
        [Column(Order = 2)]
        [Display(AutoGenerateField = false, Name = "Код контрагента")]
        public int? KaId { get; set; }

        [Display(Name = "Назва товару")]
        public string MatName { get; set; }

        [Display(Name = "Замовлено, к-сть")]
        public decimal? AmountOrd { get; set; }

        [Display(Name = "Замовлено, сума")]
        public Nullable<decimal> TotalOrd { get; set; }

        [Display(Name = "Контрагент")]
        public string KaName { get; set; }

        [Display(Name = "Од. вим.")]
        public string MsrName { get; set; }

        [Display(Name = "Штрих-код")]
        public string BarCode { get; set; }

        [Display(Name = "Відгружено, к-сть")]
        public Nullable<decimal> AmountOut { get; set; }

        [Display(Name = "Відгружено, сума")]
        public Nullable<decimal> TotalOut { get; set; }

        [Display(Name = "Відвантажив")]
        public string PersonName { get; set; }

        [Display(Name = "Різниця, к-сть")]
        public decimal TotalAmount => (AmountOut ?? 0) - (AmountOrd ?? 0);

        [Display(Name = "Різниця, сума")]
        public decimal TotalSum => (TotalOut ?? 0) - (TotalOrd ?? 0);
    }
}
