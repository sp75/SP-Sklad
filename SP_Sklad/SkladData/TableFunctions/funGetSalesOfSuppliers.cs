using SP_Sklad.SkladData.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP_Sklad.SkladData
{
    public static class funGetSalesOfSuppliers
    {
        public static List<GetSalesOfSuppliersView> GetSalesOfSuppliers(this BaseEntities db, DateTime? from_date, DateTime? to_date, int SupplierId, int wh_id)
        {
            var sql = @"  SELECT  m.MatId, m.Name MatName,  k.KaId  SupplierId, k.Name SupplierName, ABS( sum([CalcAmount])) Amount, WMatTurn.WId,
       ( SELECT sum( pr.remain) Remain
         FROM PosRemains pr
		 WHERE pr.matid = m.MatId and pr.SupplierId = k.KaId
               and pr.ondate = (select max(ondate) from posremains where posid = pr.posid  )
               and (pr.remain > 0 or Ordered > 0) ) CurRemain , ms.ShortName MeasureName, w.Name WhName
  FROM [WMatTurn]
  inner join WaybillDet wbd on wbd.PosId = [WMatTurn].SourceId
  inner join WaybillList on WaybillList.WbillId = wbd.WbillId
  inner join WaybillDet wbd_p on wbd_p.PosId = [WMatTurn].PosId
  inner join WaybillList wbl_p on wbl_p.WbillId = wbd_p.WbillId
  inner join Materials m on m.MatId = WMatTurn.MatId
  inner join Kagent k on k.KaId = (case when wbl_p.wtype in (4, 6, 25) then ( select top 1 wbl.kaid from WaybillList wbl, WaybillDet wbd, ExtRel er
                                   where wbl.wbillid=wbd.wbillid and wbd.posid=er.extposid and er.intposid = wbd.PosId ) else wbl_p.kaid end)
  inner join Measures ms on ms.MId = m.MId
  inner join Warehouse w on w.WId = WMatTurn.WId
  where  WaybillList.WType in (-1,-25,6,25) and WaybillList.OnDate between {0} and {1} and  k.KaId = {2} and  ({3} = -1 or WMatTurn.WId = {3})
  group by  k.KaId, m.MatId, m.Name , k.Name, ms.ShortName, WMatTurn.WId,  w.Name
  having sum([CalcAmount]) < 0";

            return db.Database.SqlQuery<GetSalesOfSuppliersView>(sql, from_date, to_date, SupplierId, wh_id).ToList();
        }
    }
}
