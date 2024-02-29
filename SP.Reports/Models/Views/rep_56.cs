using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP.Reports.Models.Views
{
    class rep_56
    {
        [Display(Name = "Номер документу")]
        public string WbNum { get; set; }

        [Display(Name = "Дата документу")]
        public DateTime WbOnDate { get; set; }

        [Display(Name = "Контрагент")]
        public string KaName { get; set; }

        [Display(Name = "Група товарів")]
        public string GrpName { get; set; }

        [Display(Name = "Товар")]
        public string MatName { get; set; }

        [Display(Name = "Од. вим.")]
        public string MsrName { get; set; }

        [Display(Name = "Артикул")]
        public string Artikul { get; set; }

        [Display(Name = "Повернуто, к-сть")]
        public decimal Amount { get; set; }

        [Display(Name = "Ціна")]
        public decimal? Price { get; set; }

        [Display(Name = "Всього")]
        public decimal? Total { get; set; }

        [Display(Name = "Склад")]
        public string WhName { get; set; }

        [Display(Name = "Виконавець")]
        public string PersonName { get; set; }
    }

}
