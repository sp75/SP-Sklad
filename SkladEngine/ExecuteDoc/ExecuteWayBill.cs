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
                            PosParent = 0,
                            DiscountKind = 0,
                            Amount = det_item.Amount,
                            BasePrice = det_item.Price,
                            Price = det_item.Price,
                            WId = wb_out.Kagent1.WId,
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


        public void ExecuteReturnIn(int wbill_id)
        {

        }

        public void StornoReturnIn(int wbill_id)
        {


            /*     declare id_cursor  cursor local FAST_FORWARD for 
              select wbd.PosId, wbd.matid, wbd.wid, wbd.amount, wbd.currid, wbd.num, wbd.onvalue ,  p_wbd.ondate, p_wbd.price, rtr.outposid, rtr.pposid , p_wbd.BasePrice 
              from waybilldet wbd
              inner join returnrel rtr on rtr.posid =  wbd.PosId
              inner join waybilldet p_wbd on p_wbd.posid = rtr.pposid   --Взнаємо за якою ціною було зроблена закупка (приход)
              where wbd.wbillid = @WBILLID

              open    id_cursor

              fetch next from id_cursor into @POSID , @matid, @wid, @amount, @currid, @num, @onvalue ,  @wbd_ondate, @price, @V_OUTPOSID, @V_PPOSID, @BasePrice  
              while (@@fetch_status = 0)
              begin
                       -- Добавляем в waybilldet позицію (прибуток)
                       insert into waybilldet (WBILLID,  matid, wid, amount, currid, ondate, num, onvalue,price, BasePrice )
                                       values (@WBILLID, @matid, @wid, @amount, @currid, @ondate,@num, @onvalue, @price , @BasePrice) ;

                        SET @NEW_POSID = SCOPE_IDENTITY();  
                       --Оновлюемо поле dposid яка вказує на нову позицію прихода в waybilldet
                       update returnrel set dposid =@NEW_POSID where posid =@POSID and outposid=@V_OUTPOSID and pposid=@V_PPOSID ;

                       -- Добавляем в WMATTURN обороти по матеріалу згідно нового waybilldet
                       insert into WMATTURN  (POSID, WID, MATID, ondate, TURNTYPE, AMOUNT, SOURCEID)
                                     values (@NEW_POSID, @wid, @matid, @ondate, 1,@amount, @NEW_POSID);

                fetch next from id_cursor into @POSID , @matid, @wid, @amount, @currid, @num, @onvalue ,  @wbd_ondate, @price, @V_OUTPOSID, @V_PPOSID, @BasePrice   
              end

              close      id_cursor
              deallocate id_cursor

              update waybilllist set checked = 1 where wbillid =@WBILLID ;
                  */
        }
    }
}
