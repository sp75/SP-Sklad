using SP_Sklad.SkladData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP_Sklad.Common
{
   public class ExecuteDocument
    {
        public static Guid ExecuteRawMaterialManagement(Guid id, BaseEntities _db)
        {
            var _rmm = _db.RawMaterialManagement.Find(id);

            if(_rmm.Checked != 0)
            {
                return Guid.Empty;
            }


            var wb = _db.WaybillList.Add(new WaybillList()
            {
                Id = Guid.NewGuid(),
                WType = 1,
                OnDate = DBHelper.ServerDateTime(),
                Num = new BaseEntities().GetDocNum("wb_in").FirstOrDefault(),
                CurrId = 2,
                OnValue = 1,
                PersonId = DBHelper.CurrentUser.KaId,
                Nds = DBHelper.Enterprise.NdsPayer == 1 ? DBHelper.CommonParam.Nds : 0,
                UpdatedBy = DBHelper.CurrentUser.UserId,
                EntId = DBHelper.Enterprise.KaId,
                PTypeId = 1,
                Reason = $"Зважування напівтуш №{_rmm.Num}"
            });

            _db.SetDocRel(id, wb.Id);
            _db.SaveChanges();

            var list_det = _db.RawMaterialManagementDet.Where(w => w.RawMaterialManagementId == id && w.PosId == null)
                .GroupBy(g => new { g.MatId }).Select(s => new
                {
                    s.Key.MatId,
                    Amount = s.Sum(ss => ss.Amount)
                }).ToList();

            var num = 0;
            foreach (var item in list_det)
            {
                var wbd = _db.WaybillDet.Add(new WaybillDet
                {
                    WbillId = wb.WbillId,
                    MatId = item.MatId,
                    WId = _rmm.WId,
                    Amount = item.Amount.Value,
                    Price = 0,
                    Discount = 0,
                    Nds = wb.Nds,
                    CurrId = 2,
                    OnDate = wb.OnDate,
                    Num = ++num,
                    Checked = 0,
                    OnValue = 1,
                    Total = 0,
                    BasePrice = 0,
                });
                _db.SaveChanges();

                foreach (var rmm_det in _db.RawMaterialManagementDet.Where(w => w.RawMaterialManagementId == id && w.MatId == item.MatId))
                {
                    rmm_det.PosId = wbd.PosId;
                }
            }

            _rmm.Checked = 1;

            _db.SaveChanges();

            return wb.Id;
        }
    }
}
