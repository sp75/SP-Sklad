using SP_Sklad.SkladData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SP_Sklad.Common
{
    public class ExecuteDocument
    {
        public static Guid ExecuteRawMaterialManagement(Guid id, BaseEntities _db)
        {
            var _rmm = _db.RawMaterialManagement.Find(id);

            if (_rmm.Checked != 0 || !_rmm.KaId.HasValue || _rmm.RawMaterialManagementDet.Any(a => (a.Price ?? 0) == 0))
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
                Nds = _db.Kagent.Find(_rmm.KaId)?.NdsPayer == 1 ? DBHelper.CommonParam.Nds : 0,
                UpdatedBy = DBHelper.CurrentUser.UserId,
                EntId = DBHelper.Enterprise.KaId,
                PTypeId = 1,
                Reason = $"Зважування напівтуш №{_rmm.Num}",
                KaId = _rmm.KaId
            });

            _db.SetDocRel(id, wb.Id);
            _db.SaveChanges();

            var list_det = _db.RawMaterialManagementDet.Where(w => w.RawMaterialManagementId == id && w.PosId == null)
                .GroupBy(g => new { g.MatId, g.Price }).Select(s => new
                {
                    s.Key.MatId,
                    s.Key.Price,
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
                    Price = item.Price,
                    Discount = 0,
                    Nds = wb.Nds,
                    CurrId = 2,
                    OnDate = wb.OnDate,
                    Num = ++num,
                    Checked = 0,
                    OnValue = 1,
                    Total = item.Amount.Value * item.Price,
                    BasePrice = item.Price,
                });
                _db.SaveChanges();

                foreach (var rmm_det in _db.RawMaterialManagementDet.Where(w => w.RawMaterialManagementId == id && w.MatId == item.MatId))
                {
                    rmm_det.PosId = wbd.PosId;
                }
            }

            var ex_wb = _db.ExecuteWayBill(wb.WbillId, null, DBHelper.CurrentUser.KaId).FirstOrDefault();
            if (ex_wb.ErrorMessage != "False")
            {
                MessageBox.Show(ex_wb.ErrorMessage);
                return Guid.Empty;
            }

            _rmm.Checked = 1;

            _db.SaveChanges();

            return wb.Id;
        }

        public static int? ExecuteRawMaterialManagementMove(Guid id, BaseEntities _db)
        {
            var _rmm = _db.RawMaterialManagement.Find(id);

            if (_rmm.Checked != 0)
            {
                return null;
            }

            var wb = _db.WaybillList.Add(new WaybillList()
            {
                Id = Guid.NewGuid(),
                WType = 4,
                OnDate = DBHelper.ServerDateTime(),
                Num = new BaseEntities().GetDocNum("wb_move").FirstOrDefault(),
                CurrId = 2,
                OnValue = 1,
                PersonId = DBHelper.CurrentUser.KaId,
                WaybillMove = new WaybillMove { SourceWid = _rmm.WId.Value },
                UpdatedBy = DBHelper.CurrentUser.UserId,
                EntId = DBHelper.Enterprise.KaId
            });

            _db.SetDocRel(id, wb.Id);
            _db.SaveChanges();

            var list_det = _db.RawMaterialManagementDet.Where(w => w.RawMaterialManagementId == id && w.PosId != null);

            var num = 0;
            foreach (var item in list_det)
            {
                var wbd = _db.WaybillDet.Add(new WaybillDet()
                {
                    WbillId = wb.WbillId,
                    Price = item.WaybillDet.Price * item.WaybillDet.OnValue,
                    BasePrice = item.WaybillDet.BasePrice * item.WaybillDet.OnValue,
                    Nds = 0,
                    CurrId = wb.CurrId,
                    OnDate = wb.OnDate,
                    WId = _rmm.WId,
                    Num = ++num,
                    Amount = item.Amount.Value,
                    MatId = item.MatId,
                    OnValue = wb.OnValue

                });

                wbd.WMatTurn1.Add(new WMatTurn
                {
                    PosId = item.PosId.Value,
                    WId = _rmm.WId.Value,
                    MatId = item.MatId,
                    OnDate = wb.OnDate,
                    TurnType = 2,
                    Amount = Convert.ToDecimal(item.Amount),
                });
            }
            _rmm.Checked = 1;

            _db.SaveChanges();

            return wb.WbillId;
        }

        public static Guid ExecuteRawMaterialManagementWBWriteOff(Guid id, BaseEntities _db)
        {
            var _rmm = _db.RawMaterialManagement.Find(id);

            var wb = _db.WaybillList.Add(new WaybillList()
            {
                Id = Guid.NewGuid(),
                WType = -5,
                DefNum = 1,
                OnDate = DBHelper.ServerDateTime(),
                Num = new BaseEntities().GetDocNum("wb_write_off").FirstOrDefault(),
                CurrId = 2,
                OnValue = 1,
                PersonId = DBHelper.CurrentUser.KaId,
                WaybillMove = new WaybillMove { SourceWid = _rmm.WId.Value },
                Nds = 0,
                UpdatedBy = DBHelper.CurrentUser.UserId,
                EntId = DBHelper.Enterprise.KaId,
                Commission = new List<Commission>() { new Commission { KaId = DBHelper.CurrentUser.KaId } }
            });

            //  _db.Commission.Add(new Commission { WbillId = wb.WbillId, KaId = DBHelper.CurrentUser.KaId });
            _db.SetDocRel(id, wb.Id);
            _db.SaveChanges();


            var pos_list = _db.RawMaterialManagementDet.Where(w => w.RawMaterialManagementId == id).Select(s => s.PosId).Distinct().ToList();
            foreach (var pos_item in pos_list)
            {
                var pos_info = _db.RawMaterialManagementDet.Where(w => w.PosId == pos_item).GroupBy(g => g.PosId)
                    .Select(s => new
                    {
                        PosId = s.Key,
                        CountIn = s.Sum(si => si.RawMaterialManagement.DocType == 1 ? 1 : 0),
                        CountOut = s.Sum(si => si.RawMaterialManagement.DocType == -1 ? 1 : 0)
                    }).ToList();

                foreach (var pos_info_item in pos_info)
                {
                    if (pos_info_item.CountIn == pos_info_item.CountOut)
                    {
                        var remain = _db.v_PosRemains.FirstOrDefault(w => w.PosId == pos_info_item.PosId);
                        if (remain != null && remain.ActualRemain > 0)
                        {
                            var wbd = _db.WaybillDet.Add(new WaybillDet
                            {
                                WbillId = wb.WbillId,
                                OnDate = wb.OnDate,
                                MatId = remain.MatId,
                                WId = _rmm.WId.Value,
                                Amount = remain.ActualRemain.Value,
                                Price = remain.AvgPrice,
                                Discount = 0,
                                Nds = wb.Nds,
                                CurrId = wb.CurrId,
                                OnValue = wb.OnValue,
                                BasePrice = remain.AvgPrice,
                                PosKind = 0,
                                PosParent = 0,
                                DiscountKind = 0
                            });

                            wbd.WMatTurn1.Add(new WMatTurn
                            {
                                PosId = remain.PosId,
                                WId = _rmm.WId.Value,
                                MatId = remain.MatId,
                                OnDate = wb.OnDate,
                                TurnType = 2,
                                Amount = remain.ActualRemain.Value,
                            });

                            _db.SaveChanges();
                        }
                    }
                }
            }

            if (!wb.WaybillDet.Any())
            {
                _db.WaybillList.Remove(wb);
                _db.SaveChanges();

                return Guid.Empty;
            }

            return wb.Id;
        }

        public static void ExecuteWaybillTemplate(Guid id, WaybillList wb, BaseEntities _db)
        {
            var p_type = _db.Kagent.Find(wb.KaId).PTypeId ?? _db.PriceTypes.First(w => w.Def == 1).PTypeId;
            var num = wb.WaybillDet.Count();

            var list = _db.WaybillTemplateDet.Where(w => w.WaybillTemplateId == id).OrderBy(o => o.Num).ToList();
            foreach (var item in list)
            {
                var mat_price = _db.GetMatPrice(item.MatId, wb.CurrId, p_type, wb.KaId).FirstOrDefault();
                var base_price = mat_price?.Price != null ? Math.Round(mat_price.Price ?? 0, 4) : 0;

                var dis = _db.GetDiscount(wb.KaId, item.MatId).FirstOrDefault();
                var discount = dis.DiscountType == 0 ? dis.Discount : (base_price > 0 ? (dis.Discount / base_price * 100) : 0);

                var wbd = new WaybillDet
                {
                    WbillId = wb.WbillId,
                    Num = ++num,
                    OnDate = wb.OnDate,
                    MatId = item.MatId,
                    WId = item.WId ?? item.Materials.WId,
                    Amount = 0,
                    Price = base_price - (base_price * (discount ?? 0) / 100),
                    PtypeId = mat_price.PType,
                    Discount = discount,
                    Nds = wb.Nds,
                    CurrId = wb.CurrId,
                    OnValue = wb.OnValue,
                    BasePrice = base_price,
                    PosKind = 0,
                    PosParent = 0,
                    DiscountKind = 0,
                    WayBillDetAddProps = null,
                    Notes = item.Notes
                };
                _db.WaybillDet.Add(wbd);
            }
            _db.SaveChanges();
        }
    }
}
