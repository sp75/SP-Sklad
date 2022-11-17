using System;
using SP.Base;
using SP.Base.Models;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkladEngine.DBFunction
{
   public class GetLastPrice
    {
        public decimal Price { get; set; }
        public int? CurrId { get; set; }
        public decimal? OnValue { get; set; }

        public GetLastPrice(int mat_id, int? ka_id, int wtype, DateTime on_date)
        {
            using (var db = Database.SPBase())
            {
                var quary = db.WaybillDet.Where(w => w.MatId == mat_id && w.WaybillList.WType == wtype && w.OnDate <= on_date && w.WaybillList.Checked == 1);

                if (ka_id > 0)
                {
                    quary = quary.Where(w => w.WaybillList.KaId == ka_id);
                }

                var result = quary.Select(s => new
                {
                    s.Price,
                    s.CurrId,
                    s.OnValue,
                    s.OnDate
                }).OrderByDescending(o => o.OnDate).FirstOrDefault();

                Price = result?.Price ?? 0;
                CurrId = result?.CurrId;
                OnValue = result?.OnValue;
            }
        }
        
    }
}
