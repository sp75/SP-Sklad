using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SkladEngine.Common;
using SP_Base;
using SP_Base.Models;

namespace SkladEngine.WayBills
{
    public class CopyWayBill
    {
        private int? _PersonId { get; set; }
        private int _EntId { get; set; }
        private int _UpdatedBy { get; set; }

        public CopyWayBill(int? PersonId, int EntId, int UpdatedBy)
        {
            _PersonId = PersonId;
            _EntId = EntId;
            _UpdatedBy = UpdatedBy;
        }

        public int CopyWithPriceList(int price_id)
        {
            using (var db = Database.SP_BaseModel())
            {
                var pld = db.PriceListDet.Where(w => w.PlId == price_id).ToList();

                var _wb = db.WaybillList.Add(new WaybillList()
                {
                    Id = Guid.NewGuid(),
                    WType = -16,
                    OnDate = DBHelper.ServerDateTime(),
                    Num = "",
                    CurrId = DBHelper.Currency.FirstOrDefault(w => w.Def == 1).CurrId,
                    OnValue = 1,
                    PersonId = _PersonId,
                    EntId = _EntId,
                    UpdatedBy = _UpdatedBy
                });
                db.SaveChanges();

                foreach (var item in pld.Where(w=> w.PlDetType == 0))
                {
                    db.WaybillDet.Add(new WaybillDet
                    {
                        WbillId = _wb.WbillId,
                        Amount = 0,
                        Discount = 0,
                        Nds = _wb.Nds,
                        CurrId = _wb.CurrId,
                        OnDate = _wb.OnDate,
                        Num = _wb.WaybillDet.Count() + 1,
                        OnValue = _wb.OnValue,
                        PosKind = 0,
                        PosParent = 0,
                        DiscountKind = 0,
                        PtypeId = db.Kagent.Find(_wb.KaId).PTypeId,
                        WayBillDetAddProps = new WayBillDetAddProps(),
                        BasePrice = item.Price,
                        Price = item.Price
                    });
                }
                db.SaveChanges();

                return _wb.WbillId;
            }
        }
    }
}
