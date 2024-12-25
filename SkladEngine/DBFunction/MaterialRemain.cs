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
group by pr.WId
OPTION(RECOMPILE)", mat_id, _user_id).ToList();
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
         m.Artikul,
         sum( pr.remain) Remain,
         sum( pr.Rsv ) Rsv,
         sum( pr.ActualRemain ) CurRemain,
         cast(sum( (pr.remain + pr.Ordered) * pr.AvgPrice) / sum( pr.remain + pr.Ordered) as NUMERIC(15, 2) ) AvgPrice,
         mg.Name GrpName,
	 	 cast( sum( (pr.remain + pr.Ordered) * pr.AvgPrice) as NUMERIC(15, 2) ) SumRemain,
         m.OpenStoreId,
         m.TypeId
FROM  PosRemains AS pr 
join ( SELECT PosId, MAX(OnDate) AS OnDate
       FROM  dbo.PosRemains
       GROUP BY PosId) AS x ON x.PosId = pr.PosId AND pr.OnDate = x.OnDate
inner join Materials m on m.MatId =pr.MatId
inner join Measures ms on ms.MId = m.MId
inner join MatGroup mg ON m.GrpId = mg.GrpId 
WHERE ( (pr.Remain > 0) OR (pr.Ordered > 0) ) and pr.WId = {0} 
group by pr.MatId, m.Name, m.Artikul, ms.ShortName, mg.Name, m.OpenStoreId, m.TypeId", wh_id).ToList();
            }
        }

        public List<GetMaterialsOnWh_Result> GetMaterialsOnWh(string wh_ids)
        {
            using (var db = SPDatabase.SPBase())
            {
                return db.Database.SqlQuery<GetMaterialsOnWh_Result>($@"
SELECT   pr.MatId,
         m.Name MatName,
         ms.ShortName as MsrName,
         m.Artikul,
         sum( pr.remain) Remain,
         sum( pr.Rsv ) Rsv,
         sum( pr.ActualRemain ) CurRemain,
         cast(sum( (pr.remain + pr.Ordered) * pr.AvgPrice) / sum( pr.remain + pr.Ordered) as NUMERIC(15, 2) ) AvgPrice,
         mg.Name GrpName,
	 	 cast( sum( (pr.remain + pr.Ordered) * pr.AvgPrice) as NUMERIC(15, 2) ) SumRemain,
         m.OpenStoreId,
         m.TypeId
FROM  PosRemains AS pr 
join ( SELECT PosId, MAX(OnDate) AS OnDate
       FROM  dbo.PosRemains
       GROUP BY PosId) AS x ON x.PosId = pr.PosId AND pr.OnDate = x.OnDate
inner join Materials m on m.MatId =pr.MatId
inner join Measures ms on ms.MId = m.MId
inner join MatGroup mg ON m.GrpId = mg.GrpId 
WHERE ( (pr.Remain > 0) OR (pr.Ordered > 0) ) and pr.WId in ( {wh_ids} )
group by pr.MatId, m.Name, m.Artikul, ms.ShortName, mg.Name, m.OpenStoreId, m.TypeId").ToList();
            }
        }

        public List<GetMaterialsOnWh_Result> GetMaterialsOnWh(int wh_id, DateTime on_date)
        {
            using (var db = SPDatabase.SPBase())
            {
                return db.Database.SqlQuery<GetMaterialsOnWh_Result>(@"
SELECT   pr.MatId,
         m.Name MatName,
         ms.ShortName as MsrName,
         m.Artikul,
         sum( pr.remain) Remain,
         sum( pr.Rsv ) Rsv,
         sum( pr.ActualRemain ) CurRemain,
         cast(sum( (pr.remain + pr.Ordered) * pr.AvgPrice) / sum( pr.remain + pr.Ordered) as NUMERIC(15, 2) ) AvgPrice,
         mg.Name GrpName,
	 	 cast( sum( (pr.remain + pr.Ordered) * pr.AvgPrice) as NUMERIC(15, 2) ) SumRemain,
         m.OpenStoreId,
         m.TypeId
FROM  PosRemains AS pr 
join ( SELECT PosId, MAX(OnDate) AS OnDate
       FROM  dbo.PosRemains
       where ondate <= {1}
       GROUP BY PosId) AS x ON x.PosId = pr.PosId AND pr.OnDate = x.OnDate
inner join Materials m on m.MatId =pr.MatId
inner join Measures ms on ms.MId = m.MId
inner join MatGroup mg ON m.GrpId = mg.GrpId 
WHERE ( (pr.Remain > 0) OR (pr.Ordered > 0) ) and pr.WId = {0} 
group by pr.MatId, m.Name, m.Artikul, ms.ShortName, mg.Name, m.OpenStoreId, m.TypeId", wh_id, on_date).ToList();
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
            var sql = @" SELECT 
		   ROW_NUMBER() OVER(ORDER BY m.MatId ) AS RecNo, 
		   m.MatId, 
		   wh_item.Remain, 
		   wh_item.Rsv,   
		   m.NAME MatName, 
		   ms.SHORTNAME MsrName, 
		   m.Artikul,
		   wh_item.AvgPrice,  
		   mg.NAME GrpName, 
		   m.Num,  
		   m.BarCode,   
		   c.NAME Country, 
		   m.Producer, 
		   m.MinReserv, 
		   wh_item.Ordered, 
		   wh_item.ORsv,  
		   m.SERIALS IsSerial, 
		   m.GRPID OutGrpId, 
		   wh_item.CurRemain, 
		   wh_item.SumRemain,
		   m.MId,
		   mg.Num GrpNum,
           mt.Name MaterialTypeName
         FROM Materials m 
	   	 JOIN MEASURES ms ON ms.MId = m.MId 
		 JOIN MATGROUP mg ON m.GRPID = mg.GRPID 
         left outer join MaterialType mt on mt.Id = m.TypeId
		 left outer join COUNTRIES c ON m.CID = c.CID
         left join (
                   select 
                   pr.MatId,
				   sum( pr.remain) Remain,  
				   sum(pr.Rsv) Rsv,
				   cast(sum( (pr.remain + pr.Ordered) * pr.AvgPrice) / sum( pr.remain + pr.Ordered) as NUMERIC(15, 2) ) AvgPrice,
				   sum (pr.Ordered) Ordered, 
				   sum(OrderedRsv) ORsv,
				   sum(ActualRemain) CurRemain ,
				   cast( sum( (pr.remain + pr.Ordered) * pr.AvgPrice) as NUMERIC(15, 2) ) SumRemain
				   from PosRemains pr
				   join (
						 SELECT 
							[PosId],
							max(OnDate) OnDate
						 FROM [PosRemains]
					     where ondate <= {3}
					     group by [PosId]
						) x on x.PosId = pr.PosId and pr.OnDate = x.OnDate
                   inner join UserAccessWh awh on awh.WId = pr.wid
                   where  (pr.remain > 0 or Ordered > 0) and awh.UserId = {8}
				          and (pr.WId = {1} or {1} = 0 or EXISTS (SELECT * FROM Split(',', {5}) WHERE s = pr.WId))
				          and ({2} = 0 or pr.SupplierId = {2}) 
                   group by  pr.MatId ) wh_item on m.MatId = wh_item.MatId

         left join (
                   select 
				   MatId,
				   max(pr.OnDate) OnDate
				   from PosRemains pr
                   where  (pr.remain = 0 ) and (pr.WId = {1} or {1} = 0 or EXISTS (SELECT * FROM Split(',', {5}) WHERE s = pr.WId))
                   group by  pr.MatId ) empty_item on m.MatId = empty_item.MatId

        WHERE m.Deleted = 0 AND m.ARCHIVED = 0 AND ( {0} = 0 OR m.GRPID = {0} OR m.GRPID IN (SELECT s FROM Split(',', {7})) OR ({9} = 1 AND m.GRPID IN (SELECT GRPID FROM GetMatGroupTree({0}))) )
	           and ( (wh_item.remain > 0) OR (wh_item.ordered > 0) OR ( {4} = 1 AND empty_item.OnDate IS NOT NULL ) OR ({6} = 1 and  {1} in (0,-1)) )
        OPTION(RECOMPILE)";

            using (var db = SPDatabase.SPBase())
            {
                //return await db.Database.SqlQuery<MaterialRemainViews>(@"select * from WhMatGet({0},{1},{2},{3},{4},{5},{6},{7},{8},{9})", grp_id, wid, ka_id, on_date, get_empty, wh, show_all_mats, grp, _user_id, get_child_node).ToListAsync();
                return await db.Database.SqlQuery<MaterialRemainViews>(sql, grp_id, wid, ka_id, on_date, get_empty, wh, show_all_mats, grp, _user_id, get_child_node).ToListAsync();
            }
        }

    }
}
