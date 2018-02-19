using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SkladEngine.ModelViews;

namespace SP_Sklad.SkladData
{
    public static class funGetPosOut
    {
        public static List<GetPosOutView> GetPosOut(this BaseEntities db, DateTime from_date, DateTime to_date, int mat_id, int? ka_id)
        //   where T : GetPosOut_Result
        {
            var sql = @"
            select wbd.PosId, wbl.WbillId, wbl.WType, wbl.Num, wbl.OnDate, wbl.DocId, ka.KaId, ka.Name KaName, w.WID, w.name WhName, m.MatId, m.name MatName, m.Artikul, 
                 wbd.Amount , wbl.ToDate, wbd.Price , wbd.OnValue , wbd.CurrId, c.shortname CurrName, wbl.Checked, ms.shortname Measure , wbd.Nds, 
		         m.BarCode, wbd.Discount, wbd.BasePrice,
		         sum(wbd_r.amount) ReturnAmount,
     		     (wbd.Amount - coalesce(  sum(wbd_r.amount),0 )) Remain
           from waybilldet wbd
           join waybilllist wbl on wbl.wbillid=wbd.wbillid
           join warehouse w on w.wid=wbd.wid
           join materials m on m.matid=wbd.matid
           join measures ms on ms.mid=m.mid
           left join kagent ka on ka.kaid=wbl.kaid
           left join currency c on c.currid=wbd.currid
		   left outer join  RETURNREL rr on  rr.outposid =wbd.posid
		   left outer join  waybilldet wbd_r on wbd_r.posid = rr.posid 
           where  wbl.ondate between  {0} and {1}
                  and {2} in (m.matid , 0)
                  and {3} = ka.kaid 
                  and wbl.checked = 1
		          and {4} in (wbl.wtype , 0) 
           group by  wbd.PosId, wbl.WbillId, wbl.WType, wbl.Num, wbl.OnDate, wbl.DocId, ka.KaId, ka.Name , w.WID, w.name , m.MatId, m.name , m.Artikul, 
                     wbd.Amount , wbl.ToDate, wbd.Price , wbd.OnValue , wbd.CurrId, c.shortname , wbl.Checked, ms.shortname  , wbd.Nds, 
		             m.BarCode, wbd.Discount, wbd.BasePrice";

            return db.Database.SqlQuery<GetPosOutView>(sql, from_date, to_date, mat_id, ka_id).ToList();
        }
    }

}
