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

        public string ExecuteWayBillOut(int wb_id)
        {
            string msg = "false";

            using (var db = SPDatabase.SPBase())
            {
                var wb_out = db.WaybillList.Find(wb_id);
                if(wb_out.Checked == 0)
                {
                    if(!db.v_WayBillOutDet.Where(w => w.WbillId == wb_id && w.PosType == 0).Any(a=> a.Rsv == 0))
                    {
                        foreach (var wb_det in db.WaybillDet.Where(w => w.WbillId == wb_id).ToList())
                        {
                            var wmt_list = db.WMatTurn.Where(w => w.SourceId == wb_det.PosId);
                            foreach (var wmt in wmt_list)
                            {
                                wmt.TurnType = -1;
                            }

                            wb_det.AvgInPrice = wmt_list.Sum(su => su.Amount) > 0 ? wmt_list.Sum(su => su.Amount * su.WaybillDet.Price) / wmt_list.Sum(su => su.Amount) : wmt_list.Average(a => a.WaybillDet.Price);
                        }

                        wb_out.Checked = 1;

                        var v_rdocid = db.GetRelDocIds(wb_out.Id).Where(w => w.DocType == -16 || w.DocType == 2).FirstOrDefault(); //Взнаемо чи є замовлення від клієнтів або рахунок
                        if (v_rdocid != null)
                        {
                            var wb_rel = db.WaybillList.Where(w => w.Id == v_rdocid.OriginatorId).FirstOrDefault();
                            wb_rel.Checked = 1; //Товар відвантажений
                        }

                        db.SaveChanges();
                    
                    }
                    else
                    {
                        msg = "Не всі товари зарезервовано";
                    }
                }
                else
                {
                    msg = "Документ вже проведений";
                }
            }

            return msg;
        }

        public int? MoveToStoreWarehouse(int wbill_id, bool execute_doc)
        {
            using (var db = SPDatabase.SPBase())
            {
                if (!db.v_WayBillOut.Any(a => a.WbillId == wbill_id && a.IsDelivered == 0 && a.InTransit == 1))
                {
                    return null;
                }

                var wb_out = db.WaybillList.Find(wbill_id);
                var ka = db.v_Kagent.FirstOrDefault(w => w.KaId == wb_out.KaId);
                var wh = db.Warehouse.Find(ka.WId);

                if (wb_out.Checked == 1 && ka.WId.HasValue && ka.KType == 4 && !db.GetRelDocIds(wb_out.Id).Any(a => a.DocType == 1 && a.RelType == 1) && wb_out.ShipmentDate > ka.LastInventoryDate)
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
                        Notes = wh.Name,
                        Reason = $"Оприбуткування на склад {wh.Name} згідно видаткової накладної №{wb_out.Num}"
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
                            WId = ka.WId,
                            PosParent = det_item.PosId
                        });
                      
                    }

                    db.DocRels.Add(new DocRels { OriginatorId = wb_out.Id, RelOriginatorId = wb_in.Id });
                    wb_out.DeliveredWaybillId = wb_in.WbillId;
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


        public int? CorrectDocument(int wb_id,  string wb_notes, bool execute_doc)
        {
            using (var sp_base = SPDatabase.SPBase())
            {
                var wb_write_off = sp_base.WaybillList.Find(wb_id);
                if(wb_write_off.Checked == 1)
                {
                    return null;
                }

                var wb_det = sp_base.Database.SqlQuery<CorrectDetList>(@"select waybilldet.MatId, waybilldet.Amount,  remaain.TotalRemain, (waybilldet.Amount - remaain.TotalRemain) CorrectAmount, waybilldet.Price,
(SELECT        (CASE WHEN SUM(amount) = waybilldet.Amount THEN 1 ELSE 0 END) AS Expr1
                                                          FROM            dbo.WMatTurn
                                                          WHERE        (SourceId = waybilldet.PosId) AND (TurnType IN (2, - 16))) Rsv
from waybilldet 
outer apply (

                                   SELECT 
		                                 coalesce( sum( ActualRemain),0 ) TotalRemain
			                              
                                        FROM PosRemains pr
										inner join waybilldet wbd on wbd.posid=pr.posid
		                                WHERE pr.matid = waybilldet.MatId
                                              and pr.ondate = (select max(ondate) from posremains where posid = pr.posid ) 
                                              and (pr.remain > 0 or Ordered > 0) and pr.wid= waybilldet.WId  and ActualRemain > 0 
											  and  wbd.OnDate <= waybilldet.OnDate ) remaain

where waybilldet.WbillId = {0} and remaain.TotalRemain < waybilldet.Amount", wb_write_off.WbillId).ToList();

                if (wb_det.Any())
                {
                    var wb_in = sp_base.WaybillList.Add(new WaybillList()
                    {
                        Id = Guid.NewGuid(),
                        WType = 5,
                        DefNum = 0,
                        OnDate = wb_write_off.OnDate.AddMinutes(-1),
                        Num = sp_base.GetDocNum("wb_write_on").FirstOrDefault(),
                        CurrId = 2,
                        OnValue = 1,
                        //   PersonId = DBHelper.CurrentUser.KaId,
                        WaybillMove = new WaybillMove { SourceWid = wb_write_off.WaybillMove.SourceWid, DestWId = wb_write_off.WaybillMove.SourceWid },
                        Nds = 0,
                        //       UpdatedBy = DBHelper.CurrentUser.UserId,
                        EntId = wb_write_off.EntId,
                        AdditionalDocTypeId = 4, //Корегування
                        Reason = $"Документ на списання {wb_write_off.Num}",
                        Notes = wb_notes
                    });
                    sp_base.SaveChanges();

                    foreach (var item in wb_det.Where(w=> w.Rsv == 0))
                    {
                        var wbd = sp_base.WaybillDet.Add(new WaybillDet()
                        {
                            WbillId = wb_in.WbillId,
                            Num = wb_in.WaybillDet.Count() + 1,
                            Amount = item.CorrectAmount,
                            OnValue = wb_in.OnValue,
                            WId = wb_write_off.WaybillMove.SourceWid,
                            Nds = wb_in.Nds,
                            CurrId = wb_in.CurrId,
                            OnDate = wb_in.OnDate,
                            MatId = item.MatId,
                            Price = item.Price,
                            BasePrice = item.Price
                        });
                    }
                    sp_base.SaveChanges();

                    sp_base.DocRels.Add(new DocRels { OriginatorId = wb_write_off.Id, RelOriginatorId = wb_in.Id });

                    wb_in.UpdatedAt = DateTime.Now;

                    sp_base.SaveChanges();

                    if (execute_doc)
                    {
                        foreach (var det_item in sp_base.WaybillDet.Where(w => w.WbillId == wb_in.WbillId).ToList())
                        {
                            sp_base.WMatTurn.Add(new WMatTurn
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

                        sp_base.SaveChanges();
                    }

                    return wb_in.WbillId;
                }
            }
            return null;
        }

        public class CorrectDetList
        {
            public int MatId { get; set; }
            public decimal Amount { get; set; }
            public decimal CorrectAmount { get; set; }
            public decimal Price { get; set; }
            public int Rsv { get; set; }
        }
    }
}
