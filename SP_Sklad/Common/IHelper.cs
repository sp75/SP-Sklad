using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraTreeList.Nodes;
using DocumentFormat.OpenXml.ReportBuilder;
using FormulaExcel;
using SP_Sklad.EditForm;
using SP_Sklad.Properties;
using SP_Sklad.SkladData;
using SP_Sklad.ViewsForm;
using SpreadsheetReportBuilder;

namespace SP_Sklad.Common
{
    class IHelper
    {
        public static string template_path
        {
            get
            {
#if DEBUG
                return Path.Combine( @"c:\WinVSProjects\SP-Sklad\SP_Sklad\", "TempLate" );
#else
                if (Directory.Exists(Path.Combine(Application.StartupPath, "TempLate")))
                {
                    return Path.Combine(Application.StartupPath, "TempLate");
                }

                return DBHelper.CommonParam.TemplatePatch;
#endif
            }
        }

        public static string rep_path
        {
            get
            {
#if DEBUG
                return Path.Combine( @"c:\WinVSProjects\SP-Sklad\SP_Sklad\", "Rep" );
#else
               return Path.Combine(Application.StartupPath, "Rep" );
#endif
            }
        }
        static public string reg_layout_path
        {
            get { return "SP_Sklad\\XtraGrid\\Layouts\\"; }

        }

        static public bool isRowDublClick(object grid_view)
        {
            GridView view = (GridView)grid_view;
            if (view == null)
            {
                return false;
            } 

            Point pt = view.GridControl.PointToClient(Control.MousePosition);
            GridHitInfo info = view.CalcHitInfo(pt);

            if (info.InRow || info.InRowCell)
            {
                return true;
            }
            else return false;
        }

         static public void ShowMatList(BaseEntities db, WaybillList wb)
        {
            var f = new frmCatalog(2);

            f.uc.xtraTabPage3.PageVisible = false;
            f.uc.xtraTabPage4.PageVisible = false;
            f.uc.xtraTabPage5.PageVisible = false;
            f.uc.xtraTabPage12.PageVisible = false;
            f.uc.xtraTabPage13.PageVisible = false;
            f.uc.xtraTabPage14.PageVisible = true;
            f.uc.wb = wb;
            f.uc.isMatList = true;

            if (f.ShowDialog() == DialogResult.OK)
            {
                foreach (var item in f.uc.custom_mat_list.OrderBy(o=> o.Num).ToList())
                {
                    var price = (item.Price ?? 0);

                    var wbd = db.WaybillDet.Add(new WaybillDet
                      {
                          WbillId = wb.WbillId,
                          Num = wb.WaybillDet.Count(),
                          MatId = item.MatId,
                          WId = item.WId,
                          Amount = item.Amount,
                          Price = price,
                          Discount = 0,
                          Nds = wb.Nds,
                          CurrId = wb.CurrId,
                          OnDate = wb.OnDate,
                          OnValue = wb.OnValue,
                          BasePrice = price + Math.Round(price * wb.Nds.Value / 100, 2),
                          PosKind = 0,
                          PosParent = 0,
                          DiscountKind = 0

                      });

                    if (wb.WType == -16)
                    {
                        var dis = db.GetDiscount(wb.KaId, item.MatId).FirstOrDefault() ?? 0.00m;
                        wbd.Discount = dis;
                        wbd.Price = item.Price - Math.Round((item.Price.Value * dis / 100), 2);
                        wbd.BasePrice = item.Price + Math.Round(item.Price.Value * wb.Nds.Value / 100, 2);
                    }
                    db.SaveChanges();

                    if (wb.WType == 16)
                    {
                        db.WMatTurn.Add(new WMatTurn()
                        {
                            SourceId = wbd.PosId,
                            PosId = wbd.PosId,
                            WId = wbd.WId.Value,
                            MatId = wbd.MatId,
                            OnDate = wbd.OnDate.Value,
                            TurnType = 3,
                            Amount = wbd.Amount
                        });
                    }
                }
                db.Save(wb.WbillId);
                
            }
        }

        static public void ShowMatListByWH(BaseEntities db, WaybillList wb, DiscCards disc_card = null)
        {
            var f = new frmWhCatalog(1, disc_card);

            //   f.uc.xtraTabPage3.PageVisible = false;
            f.uc.xtraTabPage4.PageVisible = false;
            f.uc.xtraTabPage5.PageVisible = false;
            f.uc.xtraTabPage9.PageVisible = false;
            f.uc.xtraTabPage11.PageVisible = false;
            f.uc.MatListTabPage.PageVisible = true;
            f.uc.xtraTabControl1.SelectedTabPageIndex = 4;
            f.uc.wb = wb;
            f.uc.isMatList = true;

            if (f.ShowDialog() == DialogResult.OK)
            {
                var num = wb.WaybillDet.Count();
                foreach (var item in f.uc.custom_mat_list)
                {
                    var wbd = new WaybillDet
                    {
                        WbillId = wb.WbillId,
                        Num = ++num,
                        OnDate = wb.OnDate,
                        MatId = item.MatId,
                        WId = item.WId,
                        Amount = item.Amount,
                        Price = item.Price - (item.Price * item.Discount / 100),
                        PtypeId = item.PTypeId,
                        Discount = item.Discount,
                        Nds = wb.Nds,
                        CurrId = wb.CurrId,
                        OnValue = wb.OnValue,
                        BasePrice = item.Price + Math.Round(item.Price.Value * wb.Nds.Value / 100, 2),
                        PosKind = 0,
                        PosParent = 0,
                        DiscountKind = disc_card != null ? 2 : 0,

                    };
                    db.WaybillDet.Add(wbd);
                    db.SaveChanges();

                    if(wb.WType == 16)
                    {
                        db.WMatTurn.Add(new WMatTurn()
                        {
                            SourceId = wbd.PosId,
                            PosId = wbd.PosId,
                            WId = wbd.WId.Value,
                            MatId = wbd.MatId,
                            OnDate = wbd.OnDate.Value,
                            TurnType = 3,
                            Amount = wbd.Amount
                        });
                    }
                }
                db.SaveChanges();
            }
        }

        static public void ShowMatListByWH2(BaseEntities db, WaybillList wb, int ka_id)
        {
            //Не доделано
            var f = new frmWhCatalog(1);

            f.uc.xtraTabPage4.PageVisible = false;
            f.uc.xtraTabPage5.PageVisible = false;
            f.uc.xtraTabPage9.PageVisible = false;
            f.uc.MatListTabPage.PageVisible = true;
            f.uc.xtraTabControl1.SelectedTabPageIndex = 4;
            f.uc.gridColumn49.Visible = false;
            f.uc.gridColumn51.Visible = false;
            f.uc.gridColumn52.Visible = false;
            f.uc.bar3.Visible = false;
            f.uc.ByWhBtn.Down = true;
            f.uc.splitContainerControl1.SplitterPosition = 0;
            
            f.uc.whKagentList.EditValue = ka_id;
            f.uc.whKagentList.Enabled = false;

            f.uc.WHTreeList.DataSource = new BaseEntities().GetWhTree(DBHelper.CurrentUser.UserId, 2).Where(w => w.GType == 1).ToList();
        
            f.uc.GrpNameGridColumn.GroupIndex = 0;

            f.uc.wb = wb;
            f.uc.isMatList = true;
            if (f.ShowDialog() == DialogResult.OK)
            {
                foreach (var item in f.uc.custom_mat_list)
                {
                    var wbd = db.WaybillDet.Add(new WaybillDet
                    {
                        WbillId = wb.WbillId,
                        OnDate = wb.OnDate,
                        MatId = item.MatId,
                        WId = item.WId,
                        Amount = item.Amount,
                        Price = item.Price,
                        Discount = 0,
                        Nds = wb.Nds,
                        CurrId = wb.CurrId,
                        OnValue = wb.OnValue,
                        BasePrice = item.Price,
                        PosKind = 0,
                        PosParent = 0,
                        DiscountKind = 0

                    });
                }
                db.SaveChanges();
            }
        }

        static public void ShowMatListByWH3(BaseEntities db, WaybillList wb, String WID)
        {
            var f = new frmWhCatalog(1);

            f.uc.xtraTabPage4.PageVisible = false;
            f.uc.xtraTabPage5.PageVisible = false;
            f.uc.xtraTabPage9.PageVisible = false;
            f.uc.MatListTabPage.PageVisible = true;
            f.uc.xtraTabControl1.SelectedTabPageIndex = 4;
            f.uc.gridColumn49.Visible = false;
            f.uc.gridColumn51.Visible = false;
            f.uc.gridColumn52.Visible = false;
            f.uc.MatListGridColumnWh.Visible = (WID == "*");
            f.uc.bar3.Visible = false;
            f.uc.ByWhBtn.Down = true;
            f.uc.splitContainerControl1.SplitterPosition = 0;

            int wid;
            if (int.TryParse(WID, out wid))
            {
                f.uc.WHTreeList.DataSource = new BaseEntities().GetWhTree(DBHelper.CurrentUser.UserId, 2).Where(w => w.GType == 1 && w.Num == wid).ToList();
            }
            else
            {
                f.uc.WHTreeList.DataSource = new BaseEntities().GetWhTree(DBHelper.CurrentUser.UserId, 2).Where(w => w.GType == 1).ToList();
            }
            f.uc.GrpNameGridColumn.GroupIndex = 0;

            f.uc.wb = wb;
            f.uc.isMatList = true;
            if (f.ShowDialog() == DialogResult.OK)
            {
                foreach (var item in f.uc.custom_mat_list)
                {
                    var wbd = db.WaybillDet.Add(new WaybillDet
                    {
                        WbillId = wb.WbillId,
                        OnDate = wb.OnDate,
                        MatId = item.MatId,
                        WId =  (WID != "*") ? Convert.ToInt32(WID) : item.WId,
                        Amount = item.Amount,
                        Price = item.Price ,
                        Discount = 0,
                        Nds = wb.Nds,
                        CurrId = wb.CurrId,
                        OnValue = wb.OnValue,
                        BasePrice = item.Price,
                        PosKind = 0,
                        PosParent = 0,
                        DiscountKind = 0

                    });
                }
                db.SaveChanges();
            }
        }


        static public object ShowDirectList(object old_id, int Typ)
        {
            switch (Typ)
            {
                case 1:
                    using (var f = new frmCatalog(1))
                    {
                        f.uc.isDirectList = true;
                        f.Text = "Контрагенти";
                        if (f.ShowDialog() == DialogResult.OK)
                        {
                            old_id = (f.uc.KaGridView.GetFocusedRow() as dynamic).KaId;
                        }
                    }

                    break;

                case 2:
                    using (var f = new frmCatalog(null, 25))
                    {
                        f.uc.isDirectList = true;
                        f.Text = "Склади";
                        if (f.ShowDialog() == DialogResult.OK)
                        {
                            old_id = (f.uc.WarehouseGridView.GetFocusedRow() as Warehouse).WId;
                        }
                    }
                    break;


                case 3:
                    using (var f = new frmCatalog(null, 5))
                    {
                        f.uc.isDirectList = true;
                        f.Text = "Службовці";
                        if (f.ShowDialog() == DialogResult.OK)
                        {
                            old_id = (f.uc.KaGridView.GetFocusedRow() as dynamic).KaId;
                        }
                    }
                    break;

                case 4:
                    using (var f = new frmCatalog(null, 64))
                    {
                        f.uc.isDirectList = true;
                        f.Text = "Каси";
                        if (f.ShowDialog() == DialogResult.OK)
                        {
                            old_id = (f.uc.CashDesksGridView.GetFocusedRow() as CashDesks).CashId;
                        }
                    }
                    break;

                case 5:  //Товари
                    using (var f = new frmCatalog(2))
                    {
                        f.uc.isDirectList = true;
                        f.Text = "Товари";
                        if (f.ShowDialog() == DialogResult.OK)
                        {
                            old_id = (f.uc.MatGridView.GetFocusedRow() as GetMatList_Result).MatId;
                        }
                    }
                    break;

                case 6:
                    using (var f = new frmCatalog(null, 102))
                    {
                        f.uc.isDirectList = true;
                        f.Text = "Статті витрат";
                        if (f.ShowDialog() == DialogResult.OK)
                        {
                            old_id = (f.uc.ChargeTypeGridView.GetFocusedRow() as ChargeType).CTypeId;
                        }
                    }
                    break;

                case 7:

                    using (var f = new frmCatalog(null, 43))
                    {
                        f.uc.isDirectList = true;
                        f.Text = "Країни";
                        if (f.ShowDialog() == DialogResult.OK)
                        {
                            old_id = (f.uc.CountriesGridView.GetFocusedRow() as Countries).CId;
                        }
                    }
                    break;

                case 8:
                    using (var f = new frmCatalog(null, 40))
                    {
                        f.uc.isDirectList = true;
                        f.Text = "Категорії цін";
                        if (f.ShowDialog() == DialogResult.OK)
                        {
                            old_id = (f.uc.PriceTypesGridView.GetFocusedRow() as dynamic).PTypeId;
                        }
                    }

                    break;

                case 9:
                    using (var f = new frmCatalog(null, 11))
                    {
                        f.uc.isDirectList = true;
                        f.Text = "Банки";
                        if (f.ShowDialog() == DialogResult.OK)
                        {
                            old_id = (f.uc.BanksGridView.GetFocusedRow() as Banks).BankId;
                        }
                    }
                    break;

                case 10:
                    using (var f = new frmCatalog(null, 12))
                    {
                        f.uc.isDirectList = true;
                        f.Text = "Типи рахунків в банку";
                        if (f.ShowDialog() == DialogResult.OK)
                        {
                            old_id = (f.uc.AccountTypeGridView.GetFocusedRow() as AccountType).TypeId;
                        }
                    }
                    break;

                case 11:
                    using (var f = new frmCatalog(3))
                    {
                        f.uc.isDirectList = true;
                        f.Text = "Послуги";
                        if (f.ShowDialog() == DialogResult.OK)
                        {
                            old_id = (f.uc.ServicesGridView.GetFocusedRow() as v_Services).SvcId;
                        }
                    }
                    break;

                case 12:
                    using (var f = new frmCatalog(null, 2))
                    {
                        f.uc.isDirectList = true;
                        f.Text = "Одиниці виміру";
                        if (f.ShowDialog() == DialogResult.OK)
                        {
                            old_id = (f.uc.MeasuresGridView.GetFocusedRow() as Measures).MId;
                        }
                    }
                    break;

                case 13:
                    using (var f = new frmCatalog(null, 53))
                    {
                        f.uc.isDirectList = true;
                        f.Text = "Рецепти";
                        if (f.ShowDialog() == DialogResult.OK)
                        {
                            old_id = (f.uc.MatRecipeGridView.GetFocusedRow() as dynamic).RecId;
                        }
                    }
                    break;

                case 14:
                    using (var f = new frmCatalog(null, 112))
                    {
                        f.uc.isDirectList = true;
                        f.Text = "Техпроцеси";
                        if (f.ShowDialog() == DialogResult.OK)
                        {
                            var tp = f.uc.TechProcessGridView.GetFocusedRow() as TechProcess;
                            if (tp != null)
                            {
                                old_id = tp.ProcId;
                            }
                        }
                    }
                    break;

                case 15:
                    using (var f = new frmCatalog(null, 42))
                    {
                        f.uc.isDirectList = true;
                        f.Text = "Обвалка";
                        if (f.ShowDialog() == DialogResult.OK)
                        {
                            old_id = (f.uc.MatRecipeGridView.GetFocusedRow() as dynamic).RecId;
                        }
                    }
                    break;

            }

            return old_id;
        }

        public class ReturnRemainByWH
        {
            public int mat_id { get; set; }
            public int? wid { get; set; }
        }

        static public ReturnRemainByWH ShowRemainByWH(object old_MATID, object old_WID, int Typ)
        {
            var result = new ReturnRemainByWH();

            var f = new frmWhCatalog(1);
            f.uc.OnDateEdit.Enabled = false;

            switch (Typ)
            {
                case 1: f.Text = "Склад";//(f.uc.WHTreeList.DataSource as List<GetWhTree_Result>).fir

                    // frmWHPanel->SP_WMAT_GET->Locate("MATID",MATID, TLocateOptions()) ;
                    if (f.ShowDialog() == DialogResult.OK)
                    {
                        result.mat_id = (f.uc.WhMatGridView.GetFocusedRow() as WhMatGet_Result).MatId;
                     //   result.wid = (f.uc.WhRemainGridView.GetFocusedRow() as WMatGetByWh_Result).WId;
                        var remain_in_wh = DB.SkladBase().MatRemainByWh(result.mat_id, old_WID != DBNull.Value ? (int?)old_WID : 0, 0, f.uc.OnDateEdit.DateTime, "*", DBHelper.CurrentUser.UserId).ToList();
                        result.wid = remain_in_wh.Any() ? remain_in_wh.First().WId : DBHelper.WhList.FirstOrDefault(w => w.Def == 1).WId;
                    }
                    else
                    {
                        result.mat_id = old_MATID != null ? (int)old_MATID : 0;
                        result.wid = old_WID != DBNull.Value ? (int?)old_WID : 0;
                    }
                    break;

                case 2:
                    if (old_MATID != null)
                    {
                        using (var frm = new frmRemainOnWh(DB.SkladBase(), (int)old_MATID))
                        {
                            if (frm.ShowDialog() == DialogResult.OK)
                            {
                                result.wid = frm.focused_wh.WId;
                                result.mat_id = (int)old_MATID;
                            }
                            else
                            {
                                result.wid = old_WID != null && old_WID != DBNull.Value ? (int?)old_WID : 0;
                            }
                        }
                    }

                    break;

            }

            return result;
        }

        static public void ShowKABalans(int ka_id)
        {
            if (DBHelper.CurrentUser.ShowBalance == 1)
            {
                var f = new frmKABalans(ka_id);
                f.ShowDialog();
            }
            else MessageBox.Show("Перегляд заборонено!");
        }

        static public void ShowTurnMaterial(int? mat_id)
        {
            if (mat_id == null)
            {
                return;
            }

            new frmMatTurn(mat_id.Value).ShowDialog();

        }
        static public void ShowMatRSV(int? mat_id, BaseEntities db)
        {
            if (mat_id == null)
            {
                return;
            }

            var f = new frmMatRSV(mat_id.Value, db).ShowDialog();
        }

        static public void ShowMatInfo(int? mat_id)
        {
            using (var db = DB.SkladBase())
            {
                if (db.Materials.Any(a => a.MatId == mat_id))
                {
                    using (var f = new frmMaterialEdit(mat_id))
                    {
                        f.OkButton.Visible = false;
                        f.ShowDialog();
                    }
                }
                else
                {
                    MessageBox.Show("Товар не знайдено!");
                }
            }
        }

        static public void ShowOrdered(int ka_id, int w_type, int mat_id)
        {
            using (var f = new frmOrderedList( ka_id,  w_type,  mat_id))
            {
                f.ShowDialog();
            }
        }

        static public bool FindMatInWH(int? mat_id)
        {
            mainForm.main_form.xtraTabControl1.SelectedTabPageIndex = 2;

            var first_node = mainForm.main_form.whUserControl.WHTreeList.GetNodeByVisibleIndex(0);
            mainForm.main_form.whUserControl.WHTreeList.SetFocusedNode(first_node);

            var rowHandle = mainForm.main_form.whUserControl.WhMatGridView.LocateByValue("MatId", mat_id);
            mainForm.main_form.whUserControl.WhMatGridView.FocusedRowHandle = rowHandle;

            return rowHandle != GridControl.InvalidRowHandle;
        }

        static public bool FindMatInDir(int? mat_id)
        {
            mainForm.main_form.xtraTabControl1.SelectedTabPageIndex = 5;

            var first_node = mainForm.main_form.DirUserControl.DirTreeList.FindNodeByFieldValue("Id", 6);
            mainForm.main_form.DirUserControl.DirTreeList.SetFocusedNode(first_node);

            var rowHandle = mainForm.main_form.DirUserControl.MatGridView.LocateByValue("MatId", mat_id);
            mainForm.main_form.DirUserControl.MatGridView.FocusedRowHandle = rowHandle;

            return rowHandle != GridControl.InvalidRowHandle;
        }

        public static void Print(Dictionary<string, IList> data_for_report, string temlate, bool show_report = true, bool print = false)
        {
            String template_file = Path.Combine(template_path, temlate);

            if (File.Exists(template_file))
            {
                var file_format = DBHelper.CurrentUser.ReportFormat;

                String result_file = Path.Combine(rep_path, Path.GetFileNameWithoutExtension(temlate) + "_" + DateTime.Now.Ticks.ToString() + "." + file_format);
                var rep = ReportBuilder.GenerateReport(data_for_report, template_file, false, file_format);


                if (print)
                {
                    using (var p_f = new frmSpreadsheed(rep))
                    {
                        p_f.Print();
                    }
                }
                else if (DBHelper.CurrentUser.InternalEditor != null && DBHelper.CurrentUser.InternalEditor.Value)
                {
                    if (file_format == "pdf" && show_report)
                    {
                        new frmPdfView(rep).Show();
                    }
                    else if (file_format == "xlsx" && show_report)
                    {
                        new frmSpreadsheed(rep).Show();
                    }
                }
                else
                {
                    File.WriteAllBytes(result_file, rep);

                    if (File.Exists(result_file) && show_report)
                    {
                        Process.Start(result_file);
                    }
                }

            }
            else
            {
                MessageBox.Show("Шлях до шаблонів " + template_file + " не знайдено!");
            }
        }

        public static void Print2(Dictionary<string, IList> data_for_report, string temlate)
        {
            String template_file = Path.Combine(template_path, temlate);

            if (File.Exists(template_file))
            {
                var file_format = DBHelper.CurrentUser.ReportFormat;

                String result_file = Path.Combine(rep_path, Path.GetFileNameWithoutExtension(temlate) + "_" + DateTime.Now.Ticks.ToString() + "." + file_format);

                var report = ReportBuilderXLS.GenerateReport(data_for_report, template_file).ToArray();
          //      var calc = CalculationlFormulaExcel.CalcSpreadsheetDocument(report, true).ToArray();

                if (DBHelper.CurrentUser.InternalEditor != null && DBHelper.CurrentUser.InternalEditor.Value)
                {
                    if (file_format == "pdf")
                    {
                        new frmPdfView(report).Show();
                    }
                    else if (file_format == "xlsx")
                    {
                        new frmSpreadsheed(report).Show();
                    }
                }
                else
                {
                    File.WriteAllBytes(result_file, report);

                    if (File.Exists(result_file))
                    {
                        Process.Start(result_file);
                    }
                }

            }
            else
            {
                MessageBox.Show("Шлях до шаблонів " + template_file + " не знайдено!");
            }
        }


        public static void ShowReport(byte[] report, string temlate)
        {
            var file_format = DBHelper.CurrentUser.ReportFormat;

            if (DBHelper.CurrentUser.InternalEditor != null && DBHelper.CurrentUser.InternalEditor.Value)
            {
                if (file_format == "pdf")
                {
                    new frmPdfView(report).Show();
                }
                else if (file_format == "xlsx")
                {
                    new frmSpreadsheed(report).Show();
                }
            }
            else
            {
                String result_file = Path.Combine(rep_path, Path.GetFileNameWithoutExtension(temlate) + "_" + DateTime.Now.Ticks.ToString() + "." + file_format);

                File.WriteAllBytes(result_file, report);

                if (File.Exists(result_file))
                {
                    Process.Start(result_file);
                }
            }
        }

        public static void ExportToXlsx(GridControl grid)
        {
            var file_format = DBHelper.CurrentUser.ReportFormat;


            if (DBHelper.CurrentUser.InternalEditor != null && DBHelper.CurrentUser.InternalEditor.Value)
            {
                if (file_format == "pdf")
                {
                    using (MemoryStream ms_pdf = new MemoryStream())
                    {
                        grid.ExportToPdf(ms_pdf);
                        new frmPdfView(ms_pdf.ToArray()).Show();
                    }
                }
                else if (file_format == "xlsx")
                {
                    using (MemoryStream ms_xlsx = new MemoryStream())
                    {
                        grid.ExportToXlsx(ms_xlsx);
                        new frmSpreadsheed(ms_xlsx.ToArray()).Show();
                    }
                }
            }
            else
            {
                String result_file = Path.Combine(rep_path, "expotr" + "_" + DateTime.Now.Ticks.ToString() + "." + file_format);
                if (file_format == "pdf")
                {
                    grid.ExportToPdf(result_file);
                }
                else if (file_format == "xlsx")
                {
                    grid.ExportToXlsx(result_file);
                }

                Process.Start(result_file);
            }
        }

        public static String ConvertLogData(String str)
        {
            var res = "";
            if (!String.IsNullOrEmpty(str) && str.Split(';').Count() == 5)
            {
                var split = str.Split(';');
                res += "Номер: " + split[0] + System.Environment.NewLine;
                res += "Дата: " + split[1] + System.Environment.NewLine;
                res += "Контрагент: " + split[4] + System.Environment.NewLine;
                res += "Сума: " + split[3] + System.Environment.NewLine;
            }

            if (!String.IsNullOrEmpty(str) && str.Split(';').Count() == 6)
            {
                var split = str.Split(';');
                res += "Номер: " + split[0] + System.Environment.NewLine;
                res += "Дата: " + split[1] + System.Environment.NewLine;
                res += "Користувач: " + split[4] + System.Environment.NewLine;
                res += "Вихід: " + split[3] + System.Environment.NewLine;
                res += "Технологічний процес: " + split[5] + System.Environment.NewLine;
            }

            return res;
        }

        /// <summary>
        /// map properties
        /// </summary>
        /// <param name="sourceObj"></param>
        /// <param name="targetObj"></param>
        public static void MapProp(object sourceObj, object targetObj)
        {
            Type T1 = sourceObj.GetType();
            Type T2 = targetObj.GetType();

            PropertyInfo[] sourceProprties = T1.GetProperties(BindingFlags.Instance | BindingFlags.Public);
            PropertyInfo[] targetProprties = T2.GetProperties(BindingFlags.Instance | BindingFlags.Public);

            foreach (var sourceProp in sourceProprties)
            {
                object osourceVal = sourceProp.GetValue(sourceObj, null);
                int entIndex = Array.IndexOf(targetProprties, sourceProp);
                if (entIndex >= 0)
                {
                    var targetProp = targetProprties[entIndex];
                    targetProp.SetValue(targetObj, osourceVal);
                }
            }
        }

        public static decimal GetAmounRecipe(BaseEntities db, int mat_id, int rec_id)
        {
            var measure_id = db.Materials.Find(mat_id).MId;

            var main_sum = db.MatRecDet.Where(w => w.RecId == rec_id && w.Materials.MId == measure_id).ToList()
                   .Sum(s => s.Amount);

            var ext_sum = db.MatRecDet.Where(w => w.RecId == rec_id && w.Materials.MId != w.MatRecipe.Materials.MId)
                .Select(s => new { MaterialMeasures = s.Materials.MaterialMeasures.Where(f => f.MId == measure_id), s.Amount }).ToList()
                .SelectMany(sm => sm.MaterialMeasures, (k, n) => new { k.Amount, MeasureAmount = n.Amount }).Sum(su => su.MeasureAmount * su.Amount);

            return main_sum + ext_sum;
        }
    }



    public class CustomMatList
    {
        public bool Check { get; set; }
        public int Num { get; set; }
        public int MatId { get; set; }
        public string Name { get; set; }
        public decimal Amount { get; set; }
        public decimal? Price { get; set; }
        public int WId { get; set; }
    }

    public class CatalogTreeList
    {
        public int Id { get; set; }
        public int ParentId { get; set; }
        public string Text { get; set; }
        public int ImgIdx { get; set; }
        public int TabIdx { get; set; }
        public object DataSetId { get; set; }
    }

    public static class DateTimeDayOfMonthExtensions
    {
        public static DateTime FirstDayOfMonth(this DateTime value)
        {
            return new DateTime(value.Year, value.Month, 1);
        } 

        public static int DaysInMonth(this DateTime value)
        {
            return DateTime.DaysInMonth(value.Year, value.Month);
        }

        public static DateTime LastDayOfMonth(this DateTime value)
        {
            return new DateTime(value.Year, value.Month, value.DaysInMonth());
        }

        public static DateTime SetEndDay(this DateTime value)
        {
            return value.Date.AddHours(23).AddMinutes(59).AddSeconds(59);
        }
    }



    
}
