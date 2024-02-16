using SkladEngine.DBFunction.Models;
using SP.Base;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkladEngine.DBFunction
{
    public class MaterialRemain
    {
        private int _user_id { get; set; }

        public MaterialRemain(int user_id)
        {
            _user_id = user_id;
        }


        public List<GetMatRemainByWh_Result> GetMatRemainByWh(int mat_id)
        {
            using (var db = SPDatabase.SPBase())
            {
                return db.Database.SqlQuery<GetMatRemainByWh_Result>(@"
SELECT  pr.WId,
 	    max(pr.OnDate) OnDate,
		sum( pr.remain) Remain,  
		sum(pr.Rsv) Rsv,
		cast(sum( (pr.remain + pr.Ordered) * pr.AvgPrice) / sum( pr.remain + pr.Ordered) as NUMERIC(15, 2) ) AvgPrice,
		MIN(pr.AvgPrice) MinPrice,
		MAX(pr.AvgPrice) MaxPrice,
		sum (pr.Ordered) Ordered, 
		sum(pr.OrderedRsv) ORsv,
		sum(pr.ActualRemain) CurRemain ,
		cast( sum( (pr.remain + pr.Ordered) * pr.AvgPrice) as NUMERIC(15, 2) ) SumRemain
FROM  PosRemains AS pr 
join (  SELECT  PosId, MAX(OnDate) AS OnDate
                FROM  dbo.PosRemains
				-- where  ondate <= GETDATE()-1 
				where MatId = {0}
                 GROUP BY PosId) AS x ON x.PosId = pr.PosId AND pr.OnDate = x.OnDate
--INNER JOIN    dbo.WaybillDet AS wbd ON pr.PosId = wbd.PosId 
--INNER JOIN   dbo.WaybillList AS wbl ON wbl.WbillId = wbd.WbillId
JOIN UserAccessWh a_wh on a_wh.WId = pr.wid
WHERE ( (pr.Remain > 0) OR (pr.Ordered > 0) ) and pr.MatId = {0} and a_wh.UserId = {1}
group by pr.WId", mat_id, _user_id).ToList();
            }
        }

        public List<GetRemainsOnMaterials_Result> GetRemainsOnMaterials()
        {
            using (var db = SPDatabase.SPBase())
            {
                return db.Database.SqlQuery<GetRemainsOnMaterials_Result>(@"
SELECT  pr.MatId,
 	    max(pr.OnDate) OnDate,
		sum( pr.remain) Remain,  
		sum(pr.Rsv) Rsv,
		cast(sum( (pr.remain + pr.Ordered) * pr.AvgPrice) / sum( pr.remain + pr.Ordered) as NUMERIC(15, 2) ) AvgPrice,
		MIN(pr.AvgPrice) MinPrice,
		MAX(pr.AvgPrice) MaxPrice,
		sum (pr.Ordered) Ordered, 
		sum(pr.OrderedRsv) ORsv,
		sum(pr.ActualRemain) CurRemain ,
		cast( sum( (pr.remain + pr.Ordered) * pr.AvgPrice) as NUMERIC(15, 2) ) SumRemain
FROM  PosRemains AS pr 
join (  SELECT  PosId, MAX(OnDate) AS OnDate
                FROM  dbo.PosRemains
        GROUP BY PosId) AS x ON x.PosId = pr.PosId AND pr.OnDate = x.OnDate
JOIN UserAccessWh a_wh on a_wh.WId = pr.wid
WHERE ( (pr.Remain > 0) OR (pr.Ordered > 0) ) and a_wh.UserId = {0}
group by pr.MatId", _user_id).ToList();

            }
        }

        public List<GetMaterialsOnWh_Result> GetMaterialsOnWh(int wh_id)
        {
            using (var db = SPDatabase.SPBase())
            {
                return db.Database.SqlQuery<GetMaterialsOnWh_Result>(@"
SELECT   pr.MatId,
         m.Name MatName,
         ms.ShortName as MsrName,
         sum( pr.remain) Remain,
         sum( pr.Rsv ) Rsv,
         sum( pr.ActualRemain ) CurRemain 
FROM  PosRemains AS pr 
join ( SELECT PosId, MAX(OnDate) AS OnDate
       FROM  dbo.PosRemains
       GROUP BY PosId) AS x ON x.PosId = pr.PosId AND pr.OnDate = x.OnDate
inner join Materials m on m.MatId =pr.MatId
inner join Measures ms on ms.MId = m.MId
WHERE ( (pr.Remain > 0) OR (pr.Ordered > 0) ) and pr.WId = {0} 
group by pr.MatId, m.Name, ms.ShortName", wh_id).ToList();
            }
        }

        public GetRemainingMaterialInWh_Result GetRemainingMaterialInWh(int wh_id, int mat_id)
        {
            using (var db = SPDatabase.SPBase())
            {
                return db.Database.SqlQuery<GetRemainingMaterialInWh_Result>(@"
SELECT   sum( pr.remain) Remain,
         sum( pr.Rsv ) Rsv,
         sum( pr.ActualRemain ) CurRemain 
FROM  PosRemains AS pr 
join ( SELECT PosId, MAX(OnDate) AS OnDate
       FROM  dbo.PosRemains
       GROUP BY PosId) AS x ON x.PosId = pr.PosId AND pr.OnDate = x.OnDate
inner join Materials m on m.MatId =pr.MatId
inner join Measures ms on ms.MId = m.MId
WHERE ( (pr.Remain > 0) OR (pr.Ordered > 0) ) and pr.WId = {0} and pr.MatId = {1}", wh_id, mat_id).FirstOrDefault();
            }
        }

        public async Task<List<MaterialRemainViews>> GetRemainingMaterials(int? grp_id, int? wid, int? ka_id, DateTime? on_date, int? get_empty, string wh, int? show_all_mats, string grp, int? get_child_node)
        {
            using (var db = SPDatabase.SPBase())
            {
                //         return await db.Database.SqlQuery<MaterialRemainViews>(@"select * from WhMatGet({0},{1},{2},{3},{4},{5},{6},{7},{8},{9})", grp_id, wid, ka_id, on_date, get_empty, wh, show_all_mats, grp, _user_id, get_child_node).ToListAsync();
                return await db.WhMatGet(grp_id, wid, ka_id, on_date, get_empty, wh, show_all_mats, grp, _user_id, get_child_node).Select(s => new MaterialRemainViews
                {
                    RecNo = s.RecNo,
                    MatId = s.MatId,
                    MatName = s.MatName,
                    Remain = s.Remain,
                    Rsv = s.Rsv,
                    AvgPrice = s.AvgPrice,
                    Ordered = s.Ordered,
                    ORsv = s.ORsv,
                    CurRemain = s.CurRemain,
                    SumRemain = s.SumRemain,
                    Artikul = s.Artikul,
                    BarCode = s.BarCode,
                    GrpName = s.GrpName,
                    Num = s.Num,
                    IsSerial = s.IsSerial,
                    MId = s.MId,
                    OutGrpId = s.OutGrpId,
                    MinReserv = s.MinReserv,
                    MsrName = s.MsrName,
                    Country = s.Country,
                    Producer = s.Producer
                }).ToListAsync();
            }
        }

    }
}
