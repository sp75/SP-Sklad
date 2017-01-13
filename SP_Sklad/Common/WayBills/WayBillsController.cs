using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SP_Sklad.SkladData;

namespace SP_Sklad.Common.WayBills
{
  public class WayBillsController
    {
      public static List<WaybillDet> GetWaybillDetIn(BaseEntities _db, int? waybill_id)
      {
          var q = from wbd in _db.WaybillDet
                  join wbl in _db.WaybillList on wbd.WbillId equals wbl.WbillId
                  join mat in _db.Materials on wbd.MatId equals mat.MatId
                  join grp in _db.MatGroup on mat.GrpId equals grp.GrpId
                  join msr in _db.Measures on mat.MId equals msr.MId
                  join wh in _db.Warehouse on wbd.WId equals wh.WId
                  where wbl.WbillId == waybill_id 
                  select new
                  {

                  };

          return _db.WaybillDet.Where(w => w.WbillId == waybill_id).ToList();
      }
    }

  /*
 select wbd.Num, wbd.PosId, wbd.WbillId, wbd.MatId, wbd.wid Wid, wbd.Amount, wbd.Price,
 wbd.Discount, wbd.Nds, wbd.CurrId, wbd.OnDate, wbd.PtypeId, wbd.Checked,
 mat.name as MatName, msr.shortname as MsrName, wh.name as WhName, mat.Artikul, curr.shortname as CurrName, 
 cast(wbd.onvalue as numeric(15,8)) as OnValue,
  (case
    when wbdap.wbmaked is null then 0
    else 2
  end) as PosType, cast(1 as numeric(15,8)) as Norm,
 wbdap.Producer, wbdap.Gtd, wbdap.CertNum, wbdap.CertDate, mat.Serials, mat.Barcode,
 mat.GrpId, c.name as Country, mat.Archived, s.SerialNo, wbd.BasePrice as FullPrice, 
 cast(null as integer) SvcToPrice, wbdap.WbMaked, wbd.Total, wbd.BasePrice,
 (cast(  cast(( cast(wbd.price as numeric(15,4)) *  wbd.amount) as numeric(15,2))  as numeric(15,2)) * (coalesce ( wbd.NDS,0) /100)  ) SumNds,
 grp.Name GrpName

from waybilldet wbd
 join waybilllist wbl on wbl.wbillid = wbd.wbillid
 join materials mat on mat.matid=wbd.matid
 join MatGroup grp on mat.GrpId = grp.GrpId
 join measures msr on msr.mid=mat.mid
 join warehouse wh on wh.wid=wbd.wid
 left outer join currency curr on curr.currid=wbd.currid
 left outer join serials s on s.posid=wbd.posid
 left outer join waybilldetaddprops wbdap on wbdap.posid=wbd.posid
 left outer join countries c on c.cid=mat.cid

where wbd.wbillid=@wbill_id and 
( wbl.wtype <> 6 or( wbl.wtype = 6 and (select count(*) count_turn from WMATTURN where WMATTURN.sourceid = wbd.posid) = 0))
  
union all
   
select wbs.num, -wbs.posid, wbl.wbillid, s.svcid, cast(0 as integer), wbs.amount, wbs.price,
  wbs.discount, wbs.nds, wbs.currid, wbl.ondate, cast(0 as integer), 0, cast(s.name as varchar(255)),
  ms.shortname, cast('' as varchar(64)), cast(s.artikul as varchar(255)),
  c.shortname, cast(wbl.onvalue as numeric(15,8)), 1, cast(wbs.norm as numeric(15,8)), cast('' as varchar(255)),
  cast('' as varchar(64)), cast('' as varchar(64)), cast(null as timestamp), 0,
  cast('' as varchar(64)), 0, cast('' as varchar(128)), 0, cast('' as varchar(64)),
  cast(null as numeric(15,8)), wbs.svctoprice, cast(null as integer) ,wbs.total , wbs.baseprice  ,
  (cast(  cast(( cast(wbs.price as numeric(15,4)) *  wbs.amount) as numeric(15,2))  as numeric(15,2)) * (coalesce ( wbs.NDS,0) /100)  ),
  grp.Name GrpName

from waybillsvc wbs
 join [services] s on s.svcid=wbs.svcid
 join SvcGroup grp on s.GrpId = grp.GrpId
 join measures ms on ms.mid=s.mid
 join waybilllist wbl on wbl.wbillid=wbs.wbillid
 join currency c on c.currid=wbs.currid
where wbs.wbillid=@wbill_id
    */
}
