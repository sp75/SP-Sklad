using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using SP_Sklad.EditForm;
using SP_Sklad.SkladData;

namespace SP_Sklad.Common
{
    class IHelper
    {
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
                foreach (var item in f.uc.custom_mat_list)
                {
                    var wbd = db.WaybillDet.Add(new WaybillDet
                      {
                          WbillId = wb.WbillId,
                          MatId = item.MatId,
                          WId = item.WId,
                          Amount = item.Amount,
                          Price = item.Price,
                          Discount = 0,
                          Nds = wb.Nds,
                          CurrId = wb.CurrId,
                          OnDate = wb.OnDate,
                          OnValue = wb.OnValue,
                          BasePrice = item.Price + Math.Round(item.Price.Value * wb.Nds.Value / 100, 2),
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
                }
                db.SaveChanges();
            }
        }

        static public void ShowMatListByWH(BaseEntities db, WaybillList wb)
        {
            var f = new frmWhCatalog(1);

            //   f.uc.xtraTabPage3.PageVisible = false;
            f.uc.xtraTabPage4.PageVisible = false;
            f.uc.xtraTabPage5.PageVisible = false;
            f.uc.xtraTabPage9.PageVisible = false;
            f.uc.MatListTabPage.PageVisible = true;
            f.uc.xtraTabControl1.SelectedTabPageIndex = 4;
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
                        Price = item.Price - (item.Price * item.Discount / 100),
                        PtypeId = item.PTypeId,
                        Discount = item.Discount,
                        Nds = wb.Nds,
                        CurrId = wb.CurrId,
                        OnValue = wb.OnValue,
                        BasePrice = item.Price + Math.Round(item.Price.Value * wb.Nds.Value / 100, 2),
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
                            old_id = (f.uc.KaGridView.GetFocusedRow() as KagentList).KaId;
                        }
                    }

                          break;

                  case 2:
                          using (var f = new frmCatalog(1,25))
                          {
                              f.uc.isDirectList = true;
                              f.Text = "Склади";
                              if (f.ShowDialog() == DialogResult.OK)
                              {
                                  old_id = (f.uc.KaGridView.GetFocusedRow() as KagentList).KaId;
                              }
                          }
                      
                     /* frmDirect->DirectTree->Filter = "ID=25"; //Склади;
                          frmDirect->cxSplitter1->CloseSplitter();
                          frmDirect->Caption = frmDirect->DirectTreeNAME->Value;
                          if(frmDirect->ShowModal()== mrOk) old_ID = SkladData->WarehouseWID->Value;*/
                          break;

              /*    case 3: frmDirect->DirectTree->Filter = "ID=5";  //Службовці
                          frmDirect->cxSplitter1->CloseSplitter();
                          frmDirect->Caption = frmDirect->DirectTreeNAME->Value;
                          if(frmDirect->ShowModal()== mrOk) old_ID = frmDirect->KAgentKAID->Value;
                          break;

                  case 4: frmDirect->DirectTree->Filter = "ID=64";  //Каси
                          frmDirect->cxSplitter1->CloseSplitter();
                          frmDirect->Caption = frmDirect->DirectTreeNAME->Value;
                          if(frmDirect->ShowModal()== mrOk) old_ID = SkladData->CashdesksCASHID->Value;
                          break;*/

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

                /*  case 6: frmDirect->DirectTree->Filter = "ID=102";  //Статті витрат
                          frmDirect->cxSplitter1->CloseSplitter();
                          frmDirect->Caption = frmDirect->DirectTreeNAME->Value;
                          if(frmDirect->ShowModal()== mrOk) old_ID = SkladData->ChargetypeCTYPEID->Value;
                          break;

                  case 7: frmDirect->DirectTree->Filter = "ID=43";  //Країни
                          frmDirect->cxSplitter1->CloseSplitter();
                          frmDirect->Caption = frmDirect->DirectTreeNAME->Value;
                          if(frmDirect->ShowModal()== mrOk) old_ID = SkladData->CountriesCID->Value;
                          break;

                  case 8: frmDirect->DirectTree->Filter = "ID=40";  //Категорії цін
                          frmDirect->cxSplitter1->CloseSplitter();
                          frmDirect->Caption = frmDirect->DirectTreeNAME->Value;
                          if(frmDirect->ShowModal()== mrOk) old_ID = SkladData->PricetypesPTYPEID->Value;
                          break;

                  case 9: frmDirect->DirectTree->Filter = "ID=11";  //Банки
                          frmDirect->cxSplitter1->CloseSplitter();
                          frmDirect->Caption = frmDirect->DirectTreeNAME->Value;
                          if(frmDirect->ShowModal()== mrOk) old_ID = SkladData->BanksBANKID->Value;
                          break;

                  case 10: frmDirect->DirectTree->Filter = "ID=12";  //Типи рахунків в банку
                          frmDirect->cxSplitter1->CloseSplitter();
                          frmDirect->Caption = frmDirect->DirectTreeNAME->Value;
                          if(frmDirect->ShowModal()== mrOk) old_ID = SkladData->AccountTypeTYPEID->Value;
                          break;

                  case 11: frmDirect->DirectTree->Filter = "GTYPE=3";  //Послуги
                          frmDirect->Caption = frmDirect->DirectTreeNAME->Value;
                          if(frmDirect->ShowModal()== mrOk) old_ID = frmDirect->ServicesSVCID->Value;
                          break;

                  case 12: frmDirect->DirectTree->Filter = "ID=2";  //Одиниці виміру
                          frmDirect->cxSplitter1->CloseSplitter();
                          frmDirect->Caption = frmDirect->DirectTreeNAME->Value;
                          if(frmDirect->ShowModal()== mrOk) old_ID = SkladData->MeasuresMID->Value;
                          break;

                  case 13: frmDirect->DirectTree->Filter = "ID=53";  //Рецепти
                          frmDirect->cxSplitter1->CloseSplitter();
                          frmDirect->Caption = frmDirect->DirectTreeNAME->Value;
                          if(frmDirect->ShowModal()== mrOk) old_ID = SkladData->MatRecipeRECID->Value;
                          break;

                  case 14: frmDirect->DirectTree->Filter = "ID=112";  //Техпроцеси
                           frmDirect->cxSplitter1->CloseSplitter();
                           frmDirect->Caption = frmDirect->DirectTreeNAME->Value;
                           if(frmDirect->ShowModal()== mrOk) old_ID = SkladData->TechProcessPROCID->Value;
                           break;

                  case 15: frmDirect->DirectTree->Filter = "ID=42";  //Обвалка
                          frmDirect->cxSplitter1->CloseSplitter();
                          frmDirect->Caption = frmDirect->DirectTreeNAME->Value;
                          if(frmDirect->ShowModal()== mrOk) old_ID = SkladData->MatRecipeRECID->Value;
                          break;*/

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
                        result.mat_id = (f.uc.WhMatGridView.GetFocusedRow() as WhMatGet_Result).MatId.Value;
                        result.wid = (f.uc.WhRemainGridView.GetFocusedRow() as WMatGetByWh_Result).WId;
                    }
                    else
                    {
                        result.mat_id = old_MATID != null ? (int)old_MATID : 0;
                        result.wid = old_WID != null ? (int?)old_WID : 0;
                    }
                    break;

                /*     case 2: frmWHPanel->Caption = "Наявність на складах: "+frmWHPanel->WhTreeDataNAME->Value;
                             frmWHPanel->WhTreeData->Filter = "GTYPE=1";
                             frmWHPanel->cxGrid3->Parent = frmWHPanel;
                             if(frmWHPanel->SP_WMAT_GET->Locate("MATID",MATID,TLocateOptions()))
                              {
                                 if(frmWHPanel->ShowModal()== mrOk)
                                  {
                                     result[0] = frmWHPanel->SP_WMAT_GETMATID->Value;
                                     result[1] = frmWHPanel->WMAT_GET_BY_WHWID->Value;
                                  } else {
                                     result[0] = old_MATID;
                                     result[1] = old_WID;
                                  }
                             }else {
                                     result[0] = old_MATID;
                                     result[1] = old_WID;
                                   }
                             break;*/

            }



            return result;
        }

        static public void ShowKABalans(int ka_id)
        {
            if (DBHelper.CurrentUser.ShowBalance == 1)
            {
                var f = new frmKABalans(ka_id);
                f.ShowDialog();
            } //else ShowMessage("Перегляд заборонено!");
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
            using (var f = new frmMaterialEdit(mat_id))
            {
                f.OkButton.Visible = false;
                f.ShowDialog();
            }
        }

        static public void ShowOrdered(int ka_id, int w_type, int mat_id)
        {
            using (var f = new frmOrderedList( ka_id,  w_type,  mat_id))
            {
                f.ShowDialog();
            }
        }

    }

    public class CustomMatList
    {
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
        public int DataSetId { get; set; }
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
    }
}
