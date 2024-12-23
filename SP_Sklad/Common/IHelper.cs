﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CheckboxIntegration.Client;
using CheckboxIntegration.Models;
using DevExpress.DataAccess.Excel;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraTreeList.Nodes;
using DocumentFormat.OpenXml.ReportBuilder;
using FormulaExcel;
using Newtonsoft.Json;
using SP_Sklad.EditForm;
using SP_Sklad.Properties;
using SP_Sklad.Reports;
using SP_Sklad.SkladData;
using SP_Sklad.ViewsForm;
using SP_Sklad.WBDetForm;
using SpreadsheetReportBuilder;

namespace SP_Sklad.Common
{
    class IHelper
    {
        public static int _user_id { get; set; }

        public static string template_path
        {
            get
            {
#if DEBUG
                return Path.Combine(@"c:\WinVSProjects\SP-Sklad\SP_Sklad\", "TempLate");
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
                return Path.Combine(@"c:\WinVSProjects\SP-Sklad\SP_Sklad\", "Rep");
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

        static public void ShowMatList(BaseEntities db, WaybillList wb, int? wid = null)
        {
            var f = new frmCatalog(2);

            f.uc.ucMaterials.xtraTabPage3.PageVisible = false;
            f.uc.ucMaterials.xtraTabPage4.PageVisible = false;
            f.uc.ucMaterials.xtraTabPage5.PageVisible = false;
            f.uc.ucMaterials.xtraTabPage12.PageVisible = false;
            f.uc.ucMaterials.xtraTabPage13.PageVisible = false;
            f.uc.ucMaterials.MatListTabPage.PageVisible = true;
            f.uc.ucMaterials.wb = wb;
      //      f.uc.ucMaterials.isMatList = true;
            f.uc.isDirectList = true;

            if (f.ShowDialog() == DialogResult.OK)
            {
                foreach (var item in f.uc.ucMaterials.custom_mat_list.OrderBy(o => o.Num).ToList())
                {
                    var price = (item.Price ?? 0);

                    var wbd = db.WaybillDet.Add(new WaybillDet
                    {
                        WbillId = wb.WbillId,
                        Num = wb.WaybillDet.Count(),
                        MatId = item.MatId,
                        WId = wid.HasValue ? wid : item.WId,
                        Amount = item.Amount,
                        Price = price,
                        Discount = 0,
                        Nds = wb.Nds,
                        CurrId = wb.CurrId,
                        OnDate = wb.OnDate,
                        OnValue = wb.OnValue,
                        BasePrice = price,
                        PosKind = 0,
                        PosParent = 0,
                        DiscountKind = 0

                    });

                    if (wb.WType == -16)
                    {
                        var dis = db.GetDiscount(wb.KaId, item.MatId).FirstOrDefault();
                        var discount = dis.DiscountType == 0 ? dis.Discount : (dis.Discount / item.Price * 100);
                        wbd.Discount = dis.Discount;
                        wbd.Price =  item.Price - (item.Price * discount / 100) ;
                        wbd.BasePrice = item.Price;
                    }
                    db.SaveChanges();


                    var wbdp = new WayBillDetAddProps
                    {
                        PosId = wbd.PosId,
                        BarCode = item.BarCode
                    };
                    db.WayBillDetAddProps.Add(wbdp);

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

        static public void ShowMatListByWH(BaseEntities db, WaybillList wb, DiscCards disc_card = null, int? WId = -1)
        {
            using (var f = new frmWhCatalog(disc_card))
            {
                //   f.uc.xtraTabPage3.PageVisible = false;
                f.uc.ucWhMat.xtraTabPage4.PageVisible = false;
                f.uc.ucWhMat.xtraTabPage5.PageVisible = false;
                f.uc.ucWhMat.xtraTabPage9.PageVisible = false;
                f.uc.ucWhMat.xtraTabPage11.PageVisible = false;
                f.uc.ucWhMat.xtraTabPage12.PageVisible = false;
                f.uc.ucWhMat.xtraTabPage13.PageVisible = false;
                f.uc.ucWhMat.MatListTabPage.PageVisible = true;
                f.uc.ucWhMat.xtraTabControl1.SelectedTabPageIndex = 4;
                f.uc.ucWhMat.wb = wb;
                f.uc.ucWhMat.isMatList = true;
          //      f.uc.ucWhMat._wid = WId.Value;

                if (WId != -1)
                {
                    f.Text = $"Залишки на складі: [{db.Warehouse.FirstOrDefault(w => w.WId == WId)?.Name}]";

                    f.uc.ucWhMat.MatListGridColumnWh.Visible = false;
                    f.uc.ucWhMat.WhCheckedComboBox.Enabled = false;
                    f.uc.ucWhMat.wh_list = WId.Value.ToString();
                    f.uc.ByWhBtn.Enabled = false;
                }
                else
                {
                    f.Text = "Залишки на складах";
                }

                if (f.ShowDialog() == DialogResult.OK)
                {
                    var num = wb.WaybillDet.Count();
                    foreach (var item in f.uc.ucWhMat.custom_mat_list)
                    {
                        var base_price = Math.Round(item.Price ?? 0, 2);
                        var total = Math.Round(base_price * item.Amount, 2);
                        var discount = Math.Round((total * (item.Discount ?? 0) / 100), 2);
                        var total_discount = total - discount;
                      
                        var wbd = new WaybillDet
                        {
                            WbillId = wb.WbillId,
                            Num = ++num,
                            OnDate = wb.OnDate,
                            MatId = item.MatId,
                            WId = WId == -1 ? item.WId : WId,
                            Amount = item.Amount,
                            Price = base_price - (base_price * (item.Discount ?? 0) / 100), //item.Discount > 0 && item.Amount > 0 ? (total_discount / item.Amount) : base_price,
                            PtypeId = item.PTypeId,
                            Discount = item.Discount,
                            Nds = wb.Nds,
                            CurrId = wb.CurrId,
                            OnValue = wb.OnValue,
                            BasePrice = base_price ,
                            PosKind = 0,
                            PosParent = 0,
                            DiscountKind = disc_card != null ? 2 : (item.Discount > 0 ? 1 : 0),
                            WayBillDetAddProps = disc_card != null ? new WayBillDetAddProps { CardId = disc_card.CardId } : null
                        };
                        db.WaybillDet.Add(wbd);
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
                    db.SaveChanges();
                }
            }
        }

        static public void ShowMatListByWH3(BaseEntities db, WaybillList wb, String WID)
        {
            using (var f = new frmWhCatalog())
            {
                f.Text = "Залишки на складі";

                f.uc.ucWhMat.xtraTabPage4.PageVisible = false;
                f.uc.ucWhMat.xtraTabPage5.PageVisible = false;
                f.uc.ucWhMat.xtraTabPage9.PageVisible = false;
                f.uc.ucWhMat.xtraTabPage12.PageVisible = false;
                f.uc.ucWhMat.xtraTabPage11.PageVisible = false;
                f.uc.ucWhMat.MatListTabPage.PageVisible = true;
                f.uc.ucWhMat.xtraTabControl1.SelectedTabPageIndex = 4;
                f.uc.ucWhMat.gridColumn49.Visible = false;
                f.uc.ucWhMat.gridColumn51.Visible = false;
                f.uc.ucWhMat.gridColumn52.Visible = false;
                f.uc.ucWhMat.MatListGridColumnWh.Visible = (WID == "*");
                f.uc.bar3.Visible = false;
                f.uc.ByWhBtn.Down = true;
                f.uc.splitContainerControl1.SplitterPosition = 0;


                using (var _db = new BaseEntities())
                {
                //    int wid;
                    if (int.TryParse(WID, out int wid))
                    {
                        f.uc.WHTreeList.DataSource = _db.GetWhTree(DBHelper.CurrentUser.UserId, 2).Where(w => w.GType == 1 && w.Num == wid).ToList();
                    }
                    else
                    {
                        f.uc.WHTreeList.DataSource = _db.GetWhTree(DBHelper.CurrentUser.UserId, 2).Where(w => w.GType == 1).ToList();
                    }
                    f.uc.ucWhMat.GrpNameGridColumn.GroupIndex = 0;
                }

                f.uc.ucWhMat.wb = wb;
                f.uc.ucWhMat.isMatList = true;
                if (f.ShowDialog() == DialogResult.OK)
                {
                    foreach (var item in f.uc.ucWhMat.custom_mat_list)
                    {
                        var wbd = db.WaybillDet.Add(new WaybillDet
                        {
                            WbillId = wb.WbillId,
                            OnDate = wb.OnDate,
                            MatId = item.MatId,
                            WId = (WID != "*") ? Convert.ToInt32(WID) : item.WId,
                            Amount = wb.WType == 7 ? item.AccountingAmount : item.Amount,
                            Price = item.AvgPrice,// item.Price ,
                            Discount = (wb.WType == -20 || wb.WType == 7) ? item.Amount : 0,
                            Nds = wb.WType == 7 ? item.AvgPrice : wb.Nds,
                            CurrId = wb.CurrId,
                            OnValue = wb.OnValue,
                            BasePrice = item.AvgPrice,// item.Price,
                            PosKind = 0,
                            PosParent = 0,
                            DiscountKind = 0
                        });
                    }
                    db.SaveChanges();
                }
            }
        }


        static public object ShowDirectList(object old_id, int Typ)
        {
            switch (Typ)
            {
                case 1:
                    /* using (var f = new frmCatalog(1))
                     {
                         f.uc.isDirectList = true;
                         f.Text = "Контрагенти";
                         if (f.ShowDialog() == DialogResult.OK)
                         {
                             old_id = f.uc.ucKagents.focused_kagent?.KaId;
                         }
                     }*/
                    using (var frm = new frmKagents(-1, ""))
                    {
                        if (frm.ShowDialog() == DialogResult.OK)
                        {
                            old_id = frm.focused_row?.KaId;
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
                            old_id = f.uc.ucKagents.focused_kagent?.KaId;
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
                            old_id = f.uc.ucMaterials.focused_mat?.MatId;
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
                            old_id = f.uc.ucServices.svc_row?.SvcId;
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

                case 16:
                    using (var f = new frmCatalog(null, 123))
                    {
                        f.uc.isDirectList = true;
                        f.Text = "Підготовка сировини";
                        if (f.ShowDialog() == DialogResult.OK)
                        {
                            old_id = (f.uc.MatRecipeGridView.GetFocusedRow() as dynamic).RecId;
                        }
                    }
                    break;

                case 17:
                    using (var f = new frmCatalog(null, 137))
                    {
                        f.uc.isDirectList = true;
                        f.Text = "Рахунки";
                        if (f.ShowDialog() == DialogResult.OK)
                        {
                            old_id = (f.uc.KAgentAccountGridView.GetFocusedRow() as v_KAgentAccount).AccId;
                        }
                    }
                    break;

                case 18:
                    using (var f = new frmCatalog(null, 143))
                    {
                        f.uc.TreeListBar.Visible = false;
                        f.uc.isDirectList = true;
                        f.uc.splitContainerControl1.Collapsed = true;
                        f.Text = "Шаблони";
                        if (f.ShowDialog() == DialogResult.OK)
                        {
                            old_id = f.uc.waybillTemplateUserControl1.wbt_row.Id;
                        }
                    }
                    break;

                case 19:
                    using (var f = new frmCatalog(null, 38))
                    {
                        f.uc.isDirectList = true;
                        f.Text = "Групи товарів";
                        if (f.ShowDialog() == DialogResult.OK)
                        {
                            old_id = (f.uc.MatGroupTreeList.GetFocusedRow() as MatGroup).GrpId;
                        }
                    }
                    break;

                case 20:
                    using (var f = new frmCatalog(null, 115))
                    {
                        f.uc.isDirectList = true;
                        f.Text = "Маршрути";
                        if (f.ShowDialog() == DialogResult.OK)
                        {
                            old_id = (f.uc.RouteGridView.GetFocusedRow() as Routes).Id;
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

            var f = new frmWhCatalog();
            f.uc.ucWhMat.OnDateEdit.Enabled = false;

            switch (Typ)
            {
                case 1: f.Text = "Склад";//(f.uc.WHTreeList.DataSource as List<GetWhTree_Result>).fir

                    // frmWHPanel->SP_WMAT_GET->Locate("MATID",MATID, TLocateOptions()) ;
                    if (f.ShowDialog() == DialogResult.OK && f.uc.ucWhMat.focused_wh_mat != null)
                    {
                        result.mat_id = f.uc.ucWhMat.focused_wh_mat.MatId;
                        //   result.wid = (f.uc.WhRemainGridView.GetFocusedRow() as WMatGetByWh_Result).WId;
                        var remain_in_wh = DB.SkladBase().MatRemainByWh(result.mat_id, old_WID != DBNull.Value ? (int?)old_WID : 0, 0, f.uc.ucWhMat.OnDateEdit.DateTime, "*", DBHelper.CurrentUser.UserId).ToList();
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
                case 3:
                    if (old_WID != null)
                    {
                        int wid = Convert.ToInt32(old_WID);

                        f.uc.ucWhMat.gridColumn49.Visible = false;
                        f.uc.ucWhMat.gridColumn51.Visible = false;
                        f.uc.ucWhMat.gridColumn52.Visible = false;
                        f.uc.ucWhMat.MatListGridColumnWh.Visible = (wid == -1);
                        f.uc.bar3.Visible = false;
                        f.uc.ByWhBtn.Down = true;
                        f.uc.splitContainerControl1.SplitterPosition = 0;

                        f.uc.WHTreeList.DataSource = DB.SkladBase().GetWhTree(DBHelper.CurrentUser.UserId, 2).Where(w => w.GType == 1 && w.Num == wid).ToList();

                        f.uc.ucWhMat.GrpNameGridColumn.GroupIndex = 0;

                        if (f.ShowDialog() == DialogResult.OK)
                        {
                            result.mat_id = f.uc.ucWhMat.focused_wh_mat.MatId; 
                        }
                        else
                        {
                            result.mat_id = old_MATID != null ? (int)old_MATID : 0;
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
                var f = new frmKaBalans(ka_id);
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
        static public void ShowManufacturingMaterial(int? mat_id)
        {
            if (mat_id == null)
            {
                return;
            }

            new frmManufacturing(DB.SkladBase(), mat_id.Value).ShowDialog();
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
                        f.OkButton.Visible = GetUserAccess(6)?.CanModify == 1;
                        f.ShowDialog();
                    }
                }
                else
                {
                    MessageBox.Show("Товар не знайдено!");
                }
            }
        }

        static public UserAccess GetUserAccess(int FunId)
        {
            using (var db = DB.SkladBase())
            {
                return db.UserAccess.AsNoTracking().FirstOrDefault(w => w.UserId == UserSession.UserId && w.FunId == FunId);
            }
        }

        static public UserTreeView GetUserTreeView(int id)
        {
            using (var db = DB.SkladBase())
            {
                return db.UserTreeView.AsNoTracking().FirstOrDefault(w => w.Id == id);
            }
        }

        static public void ShowWayBillDetInfo(int PosId, bool can_modify)
        {
            using (var frm = new frmWayBillDetEdit(PosId))
            {
                frm.OkButton.Visible = can_modify;
                frm.ShowDialog();
            }
        }

        static public void ShowOrdered(int ka_id, int w_type, int mat_id)
        {
            using (var f = new frmOrderedList(ka_id, w_type, mat_id))
            {
                f.ShowDialog();
            }
        }

        static public void ShowRemainsInWh(int wid , string wh_name)
        {
            using (var f = new frmRemainsWhView() { WhName = wh_name })
            {
                f.BottomPanel.Visible = false;

                f.ucWhMat.WhCheckedComboBox.Enabled = false;
                f.ucWhMat.by_grp = false;
                f.ucWhMat.focused_tree_node_num = wid;
                f.ucWhMat.GrpNameGridColumn.GroupIndex = 0;
                f.ucWhMat.isDirectList = true;

                f.ShowDialog();

            }
        }


        static public bool FindMatInWH(int? mat_id)
        {
            mainForm.main_form.mainTabControl.SelectedTabPageIndex = 2;

            var first_node = mainForm.main_form.whUserControl.WHTreeList.GetNodeByVisibleIndex(0);
            mainForm.main_form.whUserControl.whContentTab.SelectedTabPageIndex = 1;

            mainForm.main_form.whUserControl.WHTreeList.SetFocusedNode(first_node);

            //   var rowHandle = mainForm.main_form.whUserControl.ucWhMat.WhMatGridView.LocateByValue("MatId", mat_id);
            //   mainForm.main_form.whUserControl.ucWhMat.WhMatGridView.FocusedRowHandle = rowHandle;
            //  mainForm.main_form.whUserControl.ucWhMat.WhMatGridView.TopRowIndex = rowHandle;

            return mainForm.main_form.whUserControl.ucWhMat.FindItem(mat_id) != GridControl.InvalidRowHandle;
        }

        static public bool FindMatInDir(int? mat_id)
        {
       //     mainForm.main_form.DirUserControl.find_mat_id = mat_id;

            mainForm.main_form.mainTabControl.SelectedTabPageIndex = 7;
            var first_node = mainForm.main_form.DirUserControl.DirTreeList.FindNodeByFieldValue("Id", 6);
            mainForm.main_form.DirUserControl.DirTreeList.SetFocusedNode(first_node);
            mainForm.main_form.DirUserControl.ucMaterials.FindItem(mat_id.Value) ;

            //    mainForm.main_form.DirUserControl.MatGridView.ClearFindFilter();

            return true;
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

        public static void ExportToXlsx(GridControl grid, DevExpress.Export.ExportType exp_type = DevExpress.Export.ExportType.DataAware  )
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
                    DevExpress.Export.ExportSettings.DefaultExportType = exp_type;
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
                    DevExpress.Export.ExportSettings.DefaultExportType = exp_type;
                    grid.ExportToXlsx(result_file);
                }

                if (File.Exists(result_file))
                {
                    Process.Start(result_file);
                }
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

            return string.IsNullOrEmpty(res) ? str : res;
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
                int entIndex = Array.IndexOf(targetProprties.Select(s=> s.Name).ToArray(), sourceProp.Name);
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

        public static decimal GetAmounPreparationMatRecipe(BaseEntities db, int measure_id, int rec_id)
        {
            var main_sum = db.MatRecDet.Where(w => w.RecId == rec_id && w.Materials.MId == measure_id && w.TurnType == -1).ToList()
                   .Sum(s => s.Amount);

            var ext_sum = db.MatRecDet.Where(w => w.RecId == rec_id && w.Materials.MId != w.MatRecipe.Materials.MId && w.TurnType == -1)
                .Select(s => new { MaterialMeasures = s.Materials.MaterialMeasures.Where(f => f.MId == measure_id), s.Amount }).ToList()
                .SelectMany(sm => sm.MaterialMeasures, (k, n) => new { k.Amount, MeasureAmount = n.Amount }).Sum(su => su.MeasureAmount * su.Amount);

            return main_sum + ext_sum;
        }

        public static void KssBook(DateTime start_date, int cash_id)
        {
            var data_for_report = new Dictionary<string, IList>();
            var rel = new List<object>();
            var end_date = start_date.Date.AddDays(1);

            using (var db = DB.SkladBase())
            {
                var list = db.v_PayDoc.Where(w => w.OnDate >= start_date && w.OnDate < end_date /*&& (w.DocType == -1 || w.DocType == 1)*/ && w.Checked == 1/* && w.MPersonId == DBHelper.CurrentUser.KaId && w.CashId != null*/ && w.CashId == cash_id).ToList().
                    Select(s => new
                    {
                        s.DocNum,
                        s.OnDate,
                        Total = s.ActualSummInCurr,
                        KaName = !string.IsNullOrEmpty(s.KaName) ? s.KaName : s.DocTypeName,
                        PersonName = s.PersonName,
                        s.CashId,
                        CashDeskName = s.CashdName
                    }).ToList();

                data_for_report.Add("range1", list);
                data_for_report.Add("range2", list.GroupBy(g => new { g.CashId, g.CashDeskName }).Select(s => new { s.Key.CashId, s.Key.CashDeskName }).ToList());
                rel.Add(new
                {
                    pk = "CashId",
                    fk = "CashId",
                    master_table = "range2",
                    child_table = "range1"
                });
                data_for_report.Add("_realation_", rel);

                //   var Cashlist = DBHelper.CashDesks.Select(s => s.CashId).ToList();
                var start_saldo = db.MoneyOnDate(start_date.AddDays(-1)).Where(w => w.CashId == cash_id).Sum(s => s.Saldo);
                var end_saldo = db.MoneyOnDate(start_date).Where(w => w.CashId == cash_id).Sum(s => s.Saldo);

                var obj = new List<object>();
                obj.Add(new
                {
                    start_date = start_date.ToShortDateString(),
                    person = DBHelper.CurrentUser.Name,
                    start_saldo = start_saldo,
                    end_saldo = end_saldo,
                    total = list.Sum(s => s.Total)
                });

                data_for_report.Add("XLRPARAMS", obj);
                IHelper.Print(data_for_report, "kss_book.xlsx");
            }
        }

        public static void PrintReceiptPng(string access_token, Guid receipt_id)
        {
            var png_data = new CheckboxClient(access_token).GetReceiptPng(receipt_id);

            if (png_data == null)
            {
                return;
            }

            try
            {
                var ms = new MemoryStream(png_data) { Position = 0 };
                Image i = Image.FromStream(ms);

                PrintDocument p = new PrintDocument();
                p.PrinterSettings.PrinterName = Settings.Default.receipt_printer;
                p.DefaultPageSettings.Landscape = false;
                p.DefaultPageSettings.PaperSize = new PaperSize("Custom", p.DefaultPageSettings.PaperSize.Width, Convert.ToInt32(((p.DefaultPageSettings.PaperSize.Width) * i.Height) / i.Width));

                p.PrintPage += delegate (object sender1, PrintPageEventArgs e1)
                {
                    e1.Graphics.DrawImage(i, e1.PageBounds);
                };

                p.Print();

            }
            catch (Exception ex)
            {
                throw new Exception("Exception Occured While Printing", ex);
            }
        }

        public static void PrintTxtReceipt(string access_token, Guid receipt_id)
        {
            var txt_receipt = new CheckboxClient(access_token).GetReceiptTxt(receipt_id);

            //      var result_file = Path.Combine(Application.StartupPath, "Rep", receipt.id.ToString() + ".txt");
            //    File.WriteAllBytes(result_file, txt_receipt);
            /*
                               if (File.Exists(result_file))
                               {
                                   Process.Start(result_file);
                               }*/

            var z = Encoding.UTF8.GetString(txt_receipt);


            PrintDocument p = new PrintDocument();
            p.PrinterSettings.PrinterName = Settings.Default.receipt_printer;
            p.PrintPage += delegate (object sender1, PrintPageEventArgs e1)
            {
                e1.Graphics.DrawString(z, new Font("Bahnschrift SemiLight Condensed", 9), new SolidBrush(Color.Black), new RectangleF(0, 0, p.DefaultPageSettings.PrintableArea.Width, p.DefaultPageSettings.PrintableArea.Height));
            };
            try
            {
                p.Print();
            }
            catch (Exception ex)
            {
                throw new Exception("Exception Occured While Printing", ex);
            }
        }

        public static byte[] PrintReportText(string access_token, Guid receipt_id)
        {
            var txt_receipt = new CheckboxClient(access_token).GetReportText(receipt_id);
            var z = Encoding.UTF8.GetString(txt_receipt);

            PrintDocument p = new PrintDocument();
            p.PrinterSettings.PrinterName = Settings.Default.receipt_printer;
            p.PrintPage += delegate (object sender1, PrintPageEventArgs e1)
            {
                e1.Graphics.DrawString(z, new Font("Bahnschrift SemiLight Condensed", 9), new SolidBrush(Color.Black), new RectangleF(0, 0, p.DefaultPageSettings.PrintableArea.Width, p.DefaultPageSettings.PrintableArea.Height));
            };
            try
            {
                p.Print();

                return txt_receipt;
            }
            catch (Exception ex)
            {
                throw new Exception("Exception Occured While Printing", ex);
            }
        }

        public static byte[] PrintReportText(byte[] txt_receipt)
        {
            var z = Encoding.UTF8.GetString(txt_receipt);

            PrintDocument p = new PrintDocument();
            p.PrinterSettings.PrinterName = Settings.Default.receipt_printer;
            p.PrintPage += delegate (object sender1, PrintPageEventArgs e1)
            {
                e1.Graphics.DrawString(z, new Font("Bahnschrift SemiLight Condensed", 9), new SolidBrush(Color.Black), new RectangleF(0, 0, p.DefaultPageSettings.PrintableArea.Width, p.DefaultPageSettings.PrintableArea.Height));
            };
            try
            {
                p.Print();

                return txt_receipt;
            }
            catch (Exception ex)
            {
                throw new Exception("Exception Occured While Printing", ex);
            }
        }

        public static void PrintSelectedWayBill(int w_type, List<Guid> doc_list)
        {
            if (MessageBox.Show("Ви бажаєте роздрукувати " + doc_list.Count.ToString() + " документів!", "Друк документів", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
            {
                using (var db = DB.SkladBase())
                {
                    foreach (var id in doc_list)
                    {
                        if (w_type == -1)
                        {
                            var data_report = PrintDoc.WayBillOutReport(id, db);
                            IHelper.Print(data_report, TemlateList.wb_out_print, false, true);
                        }

                        if (w_type == -16)
                        {
                            var ord_out = PrintDoc.WayBillOrderedOutReport(id, db);
                            IHelper.Print(ord_out, TemlateList.wb_vidgruzka, false, true);
                        }
                    }
                }
            }
        }
       
        public static void CopyCellValueToClipboard(object sender, KeyEventArgs e)
        {
            GridView view = sender as GridView;

            if (e.Control && e.KeyCode == Keys.C)
            {
                if (view.GetRowCellValue(view.FocusedRowHandle, view.FocusedColumn) != null && view.GetRowCellValue(view.FocusedRowHandle, view.FocusedColumn).ToString() != String.Empty)
                    Clipboard.SetText(view.GetRowCellValue(view.FocusedRowHandle, view.FocusedColumn).ToString());
                else
                    MessageBox.Show("The value in the selected cell is null or empty!");
                e.Handled = true;
            }
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
        public string BarCode { get; set; }
        public decimal? PriceWithoutNDS { get; set; }
        public decimal Nds { get; set; }
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

        public static DateTime StartOfWeek(this DateTime dt, DayOfWeek startOfWeek)
        {
            int diff = (7 + (dt.DayOfWeek - startOfWeek)) % 7;
            return dt.AddDays(-1 * diff).Date;
        }
    }



    
}
