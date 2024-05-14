using SP.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP.Reports.Reports
{
    public class RepMatMove
    {
        public List<RepMatMoveView> GetReport()
        {
            List<int> w_type = new List<int>() { -1, 1, 6, -6 };

            return SPDatabase.SPBase().v_WaybillDet.Where(w => ((w.PosParent ?? 0) == 0) && w_type.Contains(w.WType) && w.WbChecked == 1).OrderByDescending(o => o.WbOnDate)
                            .Select(s => new RepMatMoveView
                            {
                                WbNum = s.WbNum,
                                WbOnDate = s.WbOnDate,
                                KaName = s.KaName,
                                GrpName = s.GrpName,
                                MatName = s.MatName,
                                MsrName = s.MsrName,
                                Artikul = s.Artikul,
                                Amount = s.Amount,
                                Price = s.Price,
                                Total = s.Total,
                                WhName = s.WhName,
                                PersonName = s.PersonName
                            }).ToList();
        }
    }

   public class RepMatMoveView
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

        [Display(Name = "К-сть")]
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
