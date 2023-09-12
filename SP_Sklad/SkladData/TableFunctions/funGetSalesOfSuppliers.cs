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
        public static List<GetSalesOfSuppliersView> GetSalesOfSuppliers(this BaseEntities db, DateTime? from_date, DateTime? to_date, int SupplierId)
        {
            var sql = @"   SELECT  m.MatId, m.Name MatName, WMatTurn.SupplierId, k.Name SupplierName, ABS( sum([CalcAmount])) Amount, m.WId,
       ( SELECT sum( pr.remain) Remain
         FROM PosRemains pr
		 WHERE pr.matid = m.MatId and pr.SupplierId = WMatTurn.SupplierId
               and pr.ondate = (select max(ondate) from posremains where posid = pr.posid  )
               and (pr.remain > 0 or Ordered > 0) ) CurRemain , ms.ShortName MeasureName
  FROM [WMatTurn]
  inner join WaybillDet wbd on wbd.PosId = [WMatTurn].SourceId
  inner join WaybillList on WaybillList.WbillId = wbd.WbillId
  inner join Materials m on m.MatId = WMatTurn.MatId
  inner join Kagent k on k.KaId = WMatTurn.SupplierId
  inner join Measures ms on ms.MId = m.MId
  where  WaybillList.WType in (-1,-25,6,25) and WaybillList.OnDate between {0} and {1} and WMatTurn.[SupplierId] = {2}
  group by WMatTurn.[SupplierId], m.MatId, m.Name , k.Name, ms.ShortName, m.WId
  having sum([CalcAmount]) < 0";

            return db.Database.SqlQuery<GetSalesOfSuppliersView>(sql, from_date, to_date, SupplierId).ToList();
        }
    }
}
