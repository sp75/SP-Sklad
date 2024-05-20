using SP.Base;
using SP.Base.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkladEngine.ExecuteDoc
{
    public class ExecuteWayBill
    {
        private int _doc_type { get; set; }

        public ExecuteWayBill()
        {
        }

        public int? MoveToStoreWarehouse(int wbill_id, bool execute_doc)
        {
            using (var db = SPDatabase.SPBase())
            {
                var wb_out = db.WaybillList.Find(wbill_id);

                if (wb_out.Checked == 1 && wb_out.Kagent1.WId.HasValue && wb_out.Kagent1.KType == 4 && !db.GetRelDocIds(wb_out.Id).Any(a => a.DocType == 1 && a.RelType == 1))
                {
                    var wb_in = db.WaybillList.Add(new WaybillList()
                    {
                        Id = Guid.NewGuid(),
                        WType = 1,
                        KaId = wb_out.EntId,
                        DefNum = 0,
                        OnDate = DateTime.Now,
                        Num = db.GetDocNum("wb_in").FirstOrDefault(),
                        CurrId = wb_out.CurrId,
                        OnValue = 1,
                        PersonId = wb_out.PersonId,
                        Nds = 0,
                        UpdatedBy = wb_out.UpdatedBy,
                        UpdatedAt = DateTime.Now,
                        EntId = wb_out.EntId,
                        PTypeId = wb_out.PTypeId,
                    });

                    db.SaveChanges();
                    int num = 0;
                    foreach (var det_item in db.WaybillDet.Where(w => w.WbillId == wbill_id).ToList())
                    {
                        var _wbd = db.WaybillDet.Add(new WaybillDet()
                        {
                            WbillId = wb_in.WbillId,
                            OnDate = wb_in.OnDate,
                            MatId = det_item.MatId,
                            Discount = 0,
                            Nds = wb_in.Nds,
                            CurrId = wb_in.CurrId,
                            OnValue = wb_in.OnValue,
                            Num = ++num,
                            PosKind = 0,
                            DiscountKind = 0,
                            Amount = det_item.Amount,
                            BasePrice = det_item.Price,
                            Price = det_item.Price,
                            WId = wb_out.Kagent1.WId,
                            PosParent = det_item.PosId
                        });
                      
                    }

                    db.DocRels.Add(new DocRels { OriginatorId = wb_out.Id, RelOriginatorId = wb_in.Id });
                    db.SaveChanges();

                    if (execute_doc)
                    {
                        foreach (var det_item in db.WaybillDet.Where(w => w.WbillId == wb_in.WbillId).ToList())
                        {
                            db.WMatTurn.Add(new WMatTurn
                            {
                                PosId = det_item.PosId,
                                WId = det_item.WId.Value,
                                MatId = det_item.MatId,
                                OnDate = det_item.OnDate.Value,
                                TurnType = 1,
                                Amount = det_item.Amount,
                                SourceId = det_item.PosId
                            });
                        }

                        wb_in.Checked = 1;

                        db.SaveChanges();
                    }

                    return wb_in.WbillId;
                }
                else
                {
                    return null;
                }
            }
        }

    }
}
