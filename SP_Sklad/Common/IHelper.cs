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
            f.uc.isCatalog = true;

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
                        wbd.BasePrice = item.Price + Math.Round(item.Price.Value * wb.Nds.Value / 100, -2);
                    }
                }
                db.SaveChanges();
            }
        }

        static public int ShowDirectList(int old_ID, int Typ)
        {
            switch (Typ)
            {
                /*  case 1: frmDirect->Caption = "Контрагенти";
                          frmDirect->DirectTree->Filter = "GTYPE=1";
                          if(frmDirect->ShowModal()== mrOk) old_ID = frmDirect->KAgentKAID->Value;
                          break;

                  case 2: frmDirect->DirectTree->Filter = "ID=25"; //Склади;
                          frmDirect->cxSplitter1->CloseSplitter();
                          frmDirect->Caption = frmDirect->DirectTreeNAME->Value;
                          if(frmDirect->ShowModal()== mrOk) old_ID = SkladData->WarehouseWID->Value;
                          break;

                  case 3: frmDirect->DirectTree->Filter = "ID=5";  //Службовці
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
                    var f = new frmCatalog(2);
                    f.Text = "Товари";
                    if (f.ShowDialog() == DialogResult.OK) old_ID = (f.uc.resut as GetMatList_Result).MatId;
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

            return old_ID;

        }
    }
}
