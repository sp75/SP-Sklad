using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SP.Base.Models
{
    public partial class REP_39_Result
    {
        [Key]
        [Column(Order = 1)]
        [Display(AutoGenerateField = false)]
        public int MatId { get; set; }
        [Key]
        [Column(Order = 2)]
        [Display(AutoGenerateField = false)]
        public Nullable<int> KaId { get; set; }

        [Display(Name = "Штрих-код")]
        public string BarCode { get; set; }

        [Display(AutoGenerateField = false)]
        public Nullable<int> GrpId { get; set; }

        [Display(Name = "Товар")]
        public string Name { get; set; }

        [Display(Name = "Артикул")]
        public string Artikul { get; set; }

        [Display(Name = "Видано, к-сть")]
        public Nullable<decimal> Amount { get; set; }

        [Display(Name = "Видано, сума")]
        public Nullable<decimal> Summ { get; set; }

        [Display(Name = "Од. виміру")]
        public string ShortName { get; set; }

        [Display(Name = "Повернуто, к-сть")]
        public Nullable<decimal> ReturnAmountIn { get; set; }

        [Display(Name = "Повернуто, сума")]
        public Nullable<decimal> ReturnSummIn { get; set; }

        [Display(AutoGenerateField = false)]
        public string TypeName { get; set; }

        [Display(Name = "Контрагент")]
        public string KaName { get; set; }

        [Display(Name = "Група контрагентів")]
        public string KaGrpName { get; set; }

        [Display(Name = "Група товарів")]
        public string GrpName { get; set; }

        [Display(Name = "Всього, к-сть")]
        public decimal Total => (Amount ?? 0) - (ReturnAmountIn ?? 0);
    }
}
