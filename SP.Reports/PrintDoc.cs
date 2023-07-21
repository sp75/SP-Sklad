﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using DocumentFormat.OpenXml.ReportBuilder;
using SP.Base;
using SP.Reports.Comon;

namespace SP.Reports
{
    public class PrintDoc : BaseReport
    {
        private Dictionary<string, IList> data_for_report { get; set; }

        public PrintDoc()
        {
            data_for_report = new Dictionary<string, IList>();
        }

        public byte[] CreateCustomReport(Guid id, string template_file , int pl_id)
        {
            var wb = _db.v_WaybillList.Where(w => w.Id == id).AsNoTracking().ToList();

            var ent_id = wb.First().EntId;
            var wb_det = _db.GetWayBillDetOut(wb.First().WbillId).ToList().OrderBy(o => o.Num).ToList();

            var custom_pl = _db.PriceListDet.Where(w => w.PlId == pl_id).ToList();
            foreach(var item in wb_det)
            {
                item.BasePrice = custom_pl.FirstOrDefault(w => w.MatId == item.MatId)?.Price ?? 0;
                item.Price = item.BasePrice - (item.BasePrice * item.Discount / 100);
                item.Total = Math.Round(item.Amount * item.Price ?? 0, 2);
            }

            if (wb != null)
            {
                var m = new MoneyToStr("UAH", "UKR", "TEXT");
                wb.First().www = m.convertValue(wb_det.Sum(s=> s.Total.Value));
            }

            data_for_report.Add("EntAccount", _db.EnterpriseAccount.Where(w => w.KaId == ent_id && w.Def == 1).ToList());
            data_for_report.Add("WayBillList", wb);
            data_for_report.Add("range1", wb_det);

            var summary = wb_det.Where(w => w.PosType != 2).GroupBy(g => g.MsrName).Select(s => new
            {
                MsrName = s.Key,
                Amount = s.Sum(sum => sum.Amount)
            });

            data_for_report.Add("Summary", summary.ToList());

            return ReportBuilder.GenerateReport(data_for_report, template_file, false, "xlsx").ToArray();
        }

        public byte[] CreateReport(Guid id, int doc_type, string template_file)
        {
            switch (doc_type)
            {
                /*     case 1:
                         WayBillInReport(id, db, TemlateList.wb_in);
                         break;*/

                case -1:
                    WayBillOutReport(id);
                    //  IHelper.Print(data_report, TemlateList.wb_out);
                    break;

                    /*     case 5:
                             WayBillReport(id, db, TemlateList.write_on);
                             break;

                         case -5:
                             WayBillReport(id, db, TemlateList.write_off);
                             break;

                         case -6:
                             WayBillReport(id, db, TemlateList.re_supp);
                             break;

                         case 6:
                             WayBillReport(id, db, TemlateList.re_cust);
                             break;

                         case -16:
                             var ord_out =  WayBillOrderedOutReport(id, db);
                             IHelper.Print(ord_out, TemlateList.ord_out);
                             break;

                         case 16:
                             WayBillReport(id, db, TemlateList.ord_in);
                             break;

                         case 4:
                             WayBillMoveReport(id, db, TemlateList.wb_move);
                             break;

                         case 7:
                             WayBillInvwntoryReport(id, db, TemlateList.wb_inv);
                             break;

                         case -22:
                             DeboningReport(id, db);
                             break;

                         case -20:
                             MakedReport(id, db);
                             break;

                         case 10:
                             PriseListReport(id, db);
                             break;

                         case 3:
                             PayDocReport(id, db, TemlateList.pay_doc_in);
                             break;

                         case -3:
                             PayDocReport(id, db, TemlateList.pay_doc_out);
                             break;

                         case -2:
                             PayDocReport(id, db, TemlateList.pay_doc_out);
                             break;

                         case 2:
                             InvoiceReport(id, db, TemlateList.wb_invoice);
                             break;

                         case 21:
                             ProductionPlansReport(id, db, TemlateList.wb_prod_plan);
                             break;

                         case 22:
                             PlannedCalculationReport(id, db);
                             break;*/

            }
            return ReportBuilder.GenerateReport(data_for_report, template_file, false, "xlsx").ToArray();
        }
/*
        public static void WayBillInReport(Guid id, BaseEntities db, string template_name)
        {
            var dataForReport = new Dictionary<string, IList>();

            var wb = db.v_WaybillList.Where(w => w.Id == id).AsNoTracking().OrderBy(o => o.Num).ToList();
            int wbill_id = wb.First().WbillId; 

            dataForReport.Add("WayBillList", wb);
            dataForReport.Add("range1", db.GetWaybillDetIn(wbill_id).OrderBy(o => o.Num).ToList());

             IHelper.Print(dataForReport, template_name);
        }

        public static void WayBillReport(Guid id, BaseEntities db, string template_name)
        {
            var dataForReport = new Dictionary<string, IList>();

            var wb = db.v_WaybillList.Where(w => w.Id == id).AsNoTracking().ToList();
            if (wb.Any())
            {
                int wbill_id = wb.First().WbillId;

                dataForReport.Add("WayBillList", wb);
                dataForReport.Add("WayBillItems", db.GetWaybillDetIn(wbill_id).ToList().OrderBy(o => o.Num).ToList());
                dataForReport.Add("Commission", db.Commission.Where(w => w.WbillId == wbill_id).Select(s => new
                {
                    MainName = s.Kagent.Name,
                    FirstName = s.Kagent1.Name,
                    SecondName = s.Kagent2.Name,
                    ThirdName = s.Kagent3.Name
                }).ToList());

                IHelper.Print(dataForReport, template_name);
            }
            else
            {
                MessageBox.Show("Документ відсутній!");
            }
        }

        public static Dictionary<string, IList> WayBillOrderedOutReport(Guid id, BaseEntities db)
        {
            var dataForReport = new Dictionary<string, IList>();

            var wb = db.v_WaybillList.Where(w => w.Id == id).AsNoTracking().ToList();
            if (wb.Any())
            {
                int wbill_id = wb.First().WbillId;

                var wb_list = db.GetWaybillDetIn(wbill_id).ToList().OrderBy(o => o.Num).ToList();
                dataForReport.Add("WayBillList", wb);
                dataForReport.Add("WayBillItems", wb_list);
                dataForReport.Add("WayBillItems2", wb_list);
            }
            else
            {
                MessageBox.Show("Документ відсутній!");
            }

            return dataForReport;
        }

        public static void WayBillMoveReport(Guid id, BaseEntities db, string template_name)
        {
            var dataForReport = new Dictionary<string, IList>();

            var wb = db.v_WaybillList.Where(w => w.Id == id).AsNoTracking().ToList();
            int wbill_id = wb.First().WbillId;
            var wb_items = db.GetWaybillDetIn(wbill_id).OrderBy(o => o.Num).ToList();
            dataForReport.Add("WayBillList", wb);
            dataForReport.Add("WayBillItems", wb_items.GroupBy(g => new { g.MatId, g.MatName, g.MsrName }).Select((s, index) => new
            {
                Num = index + 1,
                s.Key.MatName,
                s.Key.MsrName,
                Amount = s.Sum(sum => sum.Amount)
            }).ToList());
            dataForReport.Add("SummaryField", wb_items.GroupBy(g => new {g.MsrName}).Select(s => new
            {
                s.Key.MsrName,
                Amount = s.Sum(a => a.Amount),
            }).ToList());

            IHelper.Print(dataForReport, template_name);
        }


        public static void WayBillInvwntoryReport(Guid id, BaseEntities db, string template_name)
        {
            var dataForReport = new Dictionary<string, IList>();
            var rel = new List<object>();

            var wb = db.v_WaybillList.Where(w => w.Id == id).AsNoTracking().ToList();
            int wbill_id = wb.First().WbillId;
            var items = db.GetWaybillDetIn(wbill_id).OrderBy(o => o.Num).ToList();

            var mat_grp = items.GroupBy(g => new { g.GrpName, g.GrpId }).Select(s => new
            {
                s.Key.GrpId,
                Name = s.Key.GrpName,
                TotalOrd = s.Sum(xs => xs.Total)
            }).OrderBy(o => o.Name).ToList();

            rel.Add(new
            {
                pk = "GrpId",
                fk = "GrpId",
                master_table = "MatGroup",
                child_table = "WayBillItems"
            });

            dataForReport.Add("MatGroup", mat_grp);
            dataForReport.Add("_realation_", rel);
            dataForReport.Add("WayBillList", wb);
            dataForReport.Add("WayBillItems",items );
            dataForReport.Add("Commission", db.Commission.Where(w => w.WbillId == wbill_id).Select(s => new
            {
                MainName = s.Kagent.Name,
                FirstName = s.Kagent1.Name,
                SecondName = s.Kagent2.Name,
                ThirdName = s.Kagent3.Name
            }).ToList());
            dataForReport.Add("SummaryField", items.GroupBy(g => 1).Select(s => new
            {
                SummAll = s.Sum(a => (a.Discount*a.Nds)- (a.Amount * a.Price)),
            }).ToList());

            IHelper.Print(dataForReport, template_name);
        }*/

        public void WayBillOutReport(Guid id)
        {
            var wb = _db.v_WaybillList.Where(w => w.Id == id).AsNoTracking().ToList();

            if (wb != null)
            {
                var m = new MoneyToStr("UAH", "UKR", "TEXT");
                wb.First().www = m.convertValue(wb.First().SummInCurr.Value);
            }

            var ent_id = wb.First().EntId;
            var wb_det = _db.GetWayBillDetOut(wb.First().WbillId).ToList().OrderBy(o => o.Num).ToList();
            data_for_report.Add("EntAccount", _db.EnterpriseAccount.Where(w => w.KaId == ent_id && w.Def == 1).ToList());
            data_for_report.Add("WayBillList", wb);
            data_for_report.Add("range1", wb_det);

            var dt = DateTime.Now.Date;
            var w_id = wb.First().WbillId;
            var p = _db.WaybillDet.Where(w => w.WbillId == w_id && w.Materials.MatRecipe.Any()).Select(s => new
            {
                s.Num,
                s.Amount,
                s.Price,
                s.Materials.Name,
                s.Materials.Measures.ShortName,
                s.Materials.Artikul,
                s.Materials.CF1,
                s.Materials.CF2,
                s.Materials.CF3,
                s.Materials.CF4,
                s.Materials.CF5,
                OnDate = dt < s.OnDate ? DbFunctions.AddDays(s.OnDate, -1) : s.OnDate
            }).OrderBy(o => o.Num).ToList();

            data_for_report.Add("Posvitcheny", p);

            var oz = _db.GetUsedMaterials(-1, dt.AddDays(1), wb.First().KaId).ToList()
                .OrderBy(o => o.MatName)
                .Select((s, index) => new
            {
                Num = index + 1,
                s.MatName,
                s.InvNumber,
                s.Price,
                s.Remain,
                s.MsrName
              }).ToList();

            data_for_report.Add("range_oz", oz);


            var summary = wb_det.Where(w => w.PosType != 2).GroupBy(g => g.MsrName).Select(s => new
            {
                MsrName = s.Key,
                Amount = s.Sum(sum => sum.Amount)
            });

            data_for_report.Add("Summary", summary.ToList());
        }

    /*    public static void InvoiceReport(Guid id, BaseEntities db, string template_name)
        {
            var data_report = new Dictionary<string, IList>();

            var wb = db.v_WaybillList.Where(w => w.Id == id).AsNoTracking().ToList();

            if (wb != null)
            {
                var m = new MoneyToStr("UAH", "UKR", "TEXT");
                wb.First().www = m.convertValue(wb.First().SummInCurr.Value);
            }

            var ent_id = wb.First().EntId;
            data_report.Add("EntAccount", db.EnterpriseAccount.Where(w => w.KaId == ent_id).ToList());
            data_report.Add("WayBillList", wb);
            data_report.Add("WayBillItems", db.GetWayBillDetOut(wb.First().WbillId).OrderBy(o => o.Num).ToList());

            var w_id = wb.First().WbillId;

            IHelper.Print(data_report, template_name);
        }

        public static void DeboningReport(Guid id, BaseEntities db)
        {
            var dataForReport = new Dictionary<string, IList>();
            var date = db.WaybillList.Where(w=> w.Id == id).Select(s=> s.OnDate).First();

            var wb = db.WBListMake(date,date,-1,"*",0,-22).Where(w => w.Id == id).ToList();
            int wbill_id = wb.First().WbillId;

            var item = db.DeboningDet.Where(w => w.WBillId == wbill_id).AsNoTracking().ToList().Select((s, index) => new
            {
                Num = index + 1,
                s.Amount,
                s.Price,
                s.Materials.Name,
                WhName = s.Warehouse.Name,
                MsrName = s.Materials.Measures.ShortName,
                s.Materials.GrpId,
                GrpName = s.Materials.MatGroup.Name
            }).ToList();

            var grp = item.GroupBy(g => new { g.GrpName, g.GrpId }).Select(s => new
            {
                s.Key.GrpId,
                s.Key.GrpName,
                Amount = s.Sum(sa => sa.Amount),
                Summ = s.Sum(sum => sum.Amount * sum.Price)
            }).ToList();

            var rel = new List<object>();
            rel.Add(new
            {
                pk = "GrpId",
                fk = "GrpId",
                master_table = "MGRPD",
                child_table = "DeboningItems"
            });

            dataForReport.Add("WayBillList", wb);
            dataForReport.Add("MGRPD", grp);
            dataForReport.Add("DeboningItems", item);
            dataForReport.Add("_realation_", rel);

            IHelper.Print(dataForReport, TemlateList.wb_deb);
        }

        public static void MakedReport(Guid id, BaseEntities db)
        {
            var dataForReport = new Dictionary<string, IList>();
            var wbl = db.WaybillList.Where(w => w.Id == id).Select(s => new { s.OnDate, s.WbillId }).First();
            var wb = db.WBListMake(wbl.OnDate, wbl.OnDate, -1, "*", 0, -20).Where(w=> w.Id == id).ToList();
            var item = db.GetWayBillDetOut(wbl.WbillId).ToList().OrderBy(o => o.Num).Select((s, index) => new
            {
                Num = index + 1,
                s.Amount,
                s.Price,
                s.MatName,
                s.WhName,
                s.MsrName,
                s.GrpId,
                GrpName = s.GroupName
            }).ToList();

            var grp = item.GroupBy(g => new { g.GrpName, g.GrpId }).Select(s => new
            {
                s.Key.GrpId,
                s.Key.GrpName,
                Amount = s.Sum(sa => sa.Amount),
                Summ = s.Sum(sum => sum.Amount * sum.Price)
            }).ToList();

            var tp = db.TechProcDet.Where(w => w.WbillId == wbl.WbillId).ToList().Select((s, index) => new
            {
                Num = index + 1,
                s.Out,
                s.Notes,
                PersonName = s.Kagent.Name,
                s.OnDate,
                s.TechProcess.Name
            }).ToList();

            var rel = new List<object>();
            rel.Add(new
            {
                pk = "GrpId",
                fk = "GrpId",
                master_table = "MGRP",
                child_table = "WayBillItems"
            });

            dataForReport.Add("WayBillList", wb);
            dataForReport.Add("MGRP", grp);
            dataForReport.Add("WayBillItems", item);
            dataForReport.Add("TechProc",  tp);
            dataForReport.Add("_realation_", rel);

             IHelper.Print(dataForReport, TemlateList.wb_maked);
        }

        public static void PayDocReport(Guid id, BaseEntities db, string template_name)
        {
            var dataForReport = new Dictionary<string, IList>();
            var pd = db.v_PayDoc.Where(w => w.Id == id).AsNoTracking().ToList();
            if (pd != null)
            {
                var m = new MoneyToStr("UAH", "UKR", "TEXT");
                 pd.First().CurrName = m.convertValue(pd.First().Total);
            }
            
            dataForReport.Add("PayDoc", pd);
            dataForReport.Add("Enterprise", db.KagentList.Where(w =>  w.KType == 3).Take( 1 ).ToList());

            IHelper.Print(dataForReport, template_name);

        }

        public static void PriseListReport(Guid id, BaseEntities db)
        {
            var dataForReport = new Dictionary<string, IList>();

            var pl = db.PriceList.Where(w => w.Id == id).AsNoTracking().ToList();
            var pl_d = db.GetPriceListDet(pl.FirstOrDefault().PlId).ToList().Select(s => new
            {
                s.BarCode,
                s.GrpName,
                s.Name,
                s.Price,
                BarCode_Price = Getlable(s.Price, s.BarCode)
            }).ToList();

            dataForReport.Add("PriceList", pl);
            dataForReport.Add("PriceListDet", pl_d);

            IHelper.Print(dataForReport, TemlateList.p_list);
        }

        public static void PlannedCalculationReport(Guid id, BaseEntities db)
        {
            var dataForReport = new Dictionary<string, IList>();

            var pc_list = db.v_PlannedCalculationDetDet.Where(w => w.PlannedCalculationId == id).AsNoTracking().ToList();
            var grp = pc_list.GroupBy(g => new
            {
                g.GrpId,
                g.MatGroupName
            }).Select(s => new
            {
                s.Key.GrpId,
                s.Key.MatGroupName
            }).ToList();

            var rel = new List<object>();
            rel.Add(new
            {
                pk = "GrpId",
                fk = "GrpId",
                master_table = "MatGroup",
                child_table = "MatOutDet"
            });

            string sql = @"select 
                             m.Name, 
                             ms.ShortName Measures ,
                             sum( mrd.Amount * pc.[RecipeCount]) Amount,
                             x.AvgPrice as Price
                        from [dbo].[v_PlannedCalculationDetDet] pc
                        inner join [dbo].[MatRecDet] mrd on mrd.RecId = pc.RecId
                        inner join [dbo].[Materials] m on m.MatId = mrd.MatId
                        inner join  [dbo].[Measures] ms on ms.MId = m.MId
                        cross apply (SELECT TOP (1) AvgPrice FROM  dbo.GetWMatTurnRemain(m.MatId, GETDATE(), 0)) x
                        where pc.[PlannedCalculationId] = {0}
                        group by m.Name, ms.ShortName,x.AvgPrice
                        order by m.Name";

            var raw_list = db.Database.SqlQuery<PlannedCalculation>(sql, id).Select((s, index) => new
            {
                Num = index + 1,
                s.Name,
                s.Measures,
                s.Amount,
                s.Price
            }).ToList();

            dataForReport.Add("MatGroup", grp);
            dataForReport.Add("MatOutDet", pc_list);
            dataForReport.Add("RawItems", raw_list);
            dataForReport.Add("_realation_", rel);

            IHelper.Print(dataForReport, TemlateList.planned_calculation);
        }

        public static void ProductionPlansReport(Guid id, BaseEntities db, string template_name)
        {
            var dataForReport = new Dictionary<string, IList>();

            var wb = db.ProductionPlans.Where(w => w.Id == id).AsNoTracking().Select(s => new
            {
                s.Num,
                s.OnDate,
                s.Notes,
                WhName = s.Warehouse.Name,
                ManufactoryName = s.Warehouse1.Name,
                PersonName = s.Kagent.Name,
                EntName = s.Kagent1.Name
            }).ToList();

            var wb_items = db.v_ProductionPlanDet.AsNoTracking().Where(w => w.ProductionPlanId == id).OrderBy(o => o.Num).ToList();

            var item = db.DocRels.Where(w => w.OriginatorId == id)
                .Join(db.WaybillList, p => p.RelOriginatorId, t => t.Id, (p, t) => t).SelectMany(sm => sm.WaybillDet).GroupBy(g => new
                {
                    g.MatId,
                    g.Materials.Name,
                    g.Materials.Measures.ShortName
                }).Select(s => new
                {
                    s.Key.MatId,
                    MatName = s.Key.Name,
                    MsrName = s.Key.ShortName,
                    TotalAmount = s.Sum(su => su.Amount)
                }).ToList().Select((s, index) => new
                {
                    Num = index + 1,
                    s.MatId,
                    s.MatName,
                    s.MsrName,
                    s.TotalAmount
                }).ToList();

            dataForReport.Add("WayBillList", wb);
            dataForReport.Add("WayBillItems", wb_items);
            dataForReport.Add("WayBillItems2", item);
            dataForReport.Add("SummaryField", wb_items.GroupBy(g => new { g.MsrName }).Select(s => new
            {
                s.Key.MsrName,
                Total = s.Sum(a => a.Total),
            }).ToList());

            IHelper.Print2(dataForReport, template_name);
        }

        private class ProductionPlansReportRep
        {
            public string MatName { get; set; }
            public string MsrName { get; set; }
            public decimal TotalAmount { get; set; }
        }


        private static string Getlable(Decimal? price , String code)
        {
            var split_price = Math.Round(price.Value, 2).ToString(CultureInfo.CreateSpecificCulture("en-GB")).Split('.');

            return "*" + code + "+" + split_price[0] + "+" + split_price[1] + "*";
        }

        public class PlannedCalculation
        {
            public string Name { get; set; }
            public string Measures { get; set; }
            public decimal? Amount { get; set; }
            public decimal? Price { get; set; }
        }*/
    }
}
