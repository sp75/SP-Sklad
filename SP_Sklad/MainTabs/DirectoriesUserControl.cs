using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SP_Sklad.SkladData;
using SP_Sklad.EditForm;
using SP_Sklad.Common;
using DevExpress.XtraTreeList;

namespace SP_Sklad.MainTabs
{
    public partial class DirectoriesUserControl : UserControl
    {
        GetDirTree_Result focused_tree_node { get; set; }
        public bool isDirectList { get; set; }
        public bool isMatList { get; set; }
        public List<CustomMatList> custom_mat_list { get; set; }
        public WaybillList wb { get; set; }
        public Object resut { get; set; }
        private int _archived { get; set; }

        public DirectoriesUserControl()
        {
            InitializeComponent();
            _archived = 0;
        }

        private void wbStartDate_Properties_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void DirectoriesUserControl_Load(object sender, EventArgs e)
        {
            mainContentTab.ShowTabHeader = DevExpress.Utils.DefaultBoolean.False;
            extDirTabControl.ShowTabHeader = DevExpress.Utils.DefaultBoolean.False;

            if (!DesignMode)
            {
                custom_mat_list = new List<CustomMatList>();
                MatListGridControl.DataSource = custom_mat_list;

                using (var db = new BaseEntities())
                {
                    repositoryItemLookUpEdit1.DataSource = DBHelper.WhList();

                    DirTreeList.DataSource = db.GetDirTree(DBHelper.CurrentUser.UserId).ToList();
                    DirTreeList.ExpandToLevel(1);
                }
            }
        }

        private void DirTreeList_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
        {
            focused_tree_node = DirTreeList.GetDataRecordByNode(e.Node) as GetDirTree_Result;

            RefrechItemBtn.PerformClick();
            mainContentTab.SelectedTabPageIndex = focused_tree_node.GType.Value;
        }

        private void RefrechItemBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var db = DB.SkladBase();

            switch (focused_tree_node.GType)
            {
                case 1:
                    //  KAgent->ParamByName("WDATE")->Value = frmMain->WorkDateEdit->Date;
                    var ka = DB.SkladBase().KagentList.Where(w => w.Archived == _archived || w.Archived == null);
                    if (focused_tree_node.Id != 10) ka = ka.Where(w => w.KType == focused_tree_node.GrpId);
                    KaGridControl.DataSource = ka.ToList();
                    break;

                case 2:
                    MatGridControl.DataSource = null;
                    MatGridControl.DataSource = DB.SkladBase().GetMatList(focused_tree_node.Id == 6 ? -1 : focused_tree_node.GrpId, 0, 0, 0);
                    break;

                /*     case 3: Services->Open();
                         if (DirectTreeID->Value == 51) Services->Filter = "";
                         else Services->Filter = "GRPID = " + DirectTreeGRPID->AsString;
                         break;*/

                case 4: switch (focused_tree_node.Id)
                    {
                        //      case 25: cxGridLevel6->GridView = WarehouseGrid; break;
                        //      case 11: cxGridLevel6->GridView = BanksGrid; break;
                        //      case 2: cxGridLevel6->GridView = MeasuresGrid; break;
                        //      case 43: cxGridLevel6->GridView = CountriesGrid; break;
                        //      case 12: cxGridLevel6->GridView = AccountTypeGrid; break;
                        //      case 40: cxGridLevel6->GridView = PricetypesGrid; break;
                        //      case 102: cxGridLevel6->GridView = ChargetypeGrid; break;
                        //      case 64: cxGridLevel6->GridView = CashdesksGrid; break;
                        //      case 3: cxGridLevel6->GridView = CurrencyGrid; break;
                        case 53:
                            MatRecipeGridControl.DataSource = db.MatRecipe.Where(w => w.RType == 1).Select(s => new
                            {
                                s.RecId,
                                MatName = s.Materials.Name,
                                s.OnDate,
                                s.Amount,
                                s.Materials.Measures.ShortName,
                                s.Name,
                                GrpName = s.Materials.MatGroup.Name
                            }).ToList();
                            
                            extDirTabControl.SelectedTabPageIndex = 0; break;
                        case 42:
                            MatRecipeGridControl.DataSource = db.MatRecipe.Where(w => w.RType == 2).Select(s => new
                            {
                                s.RecId,
                                MatName = s.Materials.Name,
                                s.OnDate,
                                s.Amount,
                                s.Materials.Measures.ShortName,
                                s.Name,
                                GrpName = s.Materials.MatGroup.Name
                            }).ToList();
                            extDirTabControl.SelectedTabPageIndex = 0; break;
                        //       case 68: cxGridLevel6->GridView = TaxesGrid; break;
                        //       case 112: cxGridLevel6->GridView = TechProcessGrid; break;
                    }
                    break;
            }

            db.Dispose();
        }

        private void MatGridView_DoubleClick(object sender, EventArgs e)
        {
            var row = MatGridView.GetFocusedRow() as GetMatList_Result;

            if (isMatList)
            {
                var t = (wb.Kagent != null ? wb.Kagent.PTypeId : null);
                var price = DB.SkladBase().GetListMatPrices(row.MatId, wb.CurrId).FirstOrDefault(w => w.PType == t);

                custom_mat_list.Add(new CustomMatList
                {
                    Num = custom_mat_list.Count() + 1,
                    MatId = row.MatId,
                    Name = row.Name,
                    Amount = 1,
                    Price = price != null ? price.Price : 0,
                    WId = row.WId != null ? row.WId.Value : DBHelper.WhList().FirstOrDefault(w => w.Def == 1).WId
                });

                MatListGridView.RefreshData();
            }
            else if (isDirectList)
            {
                var frm = this.Parent as frmCatalog;
                frm.OkButton.PerformClick();
            }
            else
            {
                EditItemBtn.PerformClick();
            }
        }

        private void MatGridView_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            resut = MatGridView.GetFocusedRow();
        }

        private void EditItemBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            switch (focused_tree_node.GType)
            {
                case 1:
                    var ka = resut as KagentList;
                    if (new frmKAgentEdit(null, ka.KaId).ShowDialog() == DialogResult.OK)
                    {
                        RefrechItemBtn.PerformClick();
                    }

                    KaGridView.RefreshData();
                    break;

                case 2:
                    var r = resut as GetMatList_Result;
                    if (new frmMaterialEdit(r.MatId).ShowDialog() == DialogResult.OK)
                    {
                        RefrechItemBtn.PerformClick();
                    }
                    break;

                /*     case 3: frmServicesEdit = new TfrmServicesEdit(Application);
                         frmServicesEdit->Services->ParamByName("SVCID")->Value = ServicesSVCID->Value;
                         frmServicesEdit->Services->Open();
                         frmServicesEdit->Services->Edit();
                         frmServicesEdit->ShowModal();
                         delete frmServicesEdit;
                         DirectTree->Refresh();
                         Services->FullRefresh();
                         break;*/

                case 4: switch (focused_tree_node.Id)
                    {
                        /*     case 25: frmWarehouseEdit = new TfrmWarehouseEdit(Application);
                                 frmWarehouseEdit->Warehouse->ParamByName("WID")->Value = SkladData->WarehouseWID->Value;
                                 frmWarehouseEdit->Warehouse->Open();
                                 frmWarehouseEdit->Warehouse->Edit();
                                 frmWarehouseEdit->ShowModal();
                                 delete frmWarehouseEdit;
                                 SkladData->Warehouse->FullRefresh();
                                 break;

                             case 11: frmBanksEdit = new TfrmBanksEdit(Application);
                                 frmBanksEdit->Banks->ParamByName("BANKID")->Value = SkladData->BanksBANKID->Value;
                                 frmBanksEdit->Banks->Open();
                                 frmBanksEdit->Banks->Edit();
                                 frmBanksEdit->ShowModal();
                                 delete frmBanksEdit;
                                 SkladData->Banks->FullRefresh();
                                 break;

                             case 2: frmMeasuresEdit = new TfrmMeasuresEdit(Application);
                                 frmMeasuresEdit->Measures->ParamByName("MID")->Value = SkladData->MeasuresMID->Value;
                                 frmMeasuresEdit->Measures->Open();
                                 frmMeasuresEdit->Measures->Edit();
                                 frmMeasuresEdit->ShowModal();
                                 delete frmMeasuresEdit;
                                 SkladData->Measures->FullRefresh();
                                 break;

                             case 40: frmPricetypesEdit = new TfrmPricetypesEdit(Application);
                                 frmPricetypesEdit->Pricetypes->ParamByName("PTYPEID")->Value = SkladData->PricetypesPTYPEID->Value;
                                 frmPricetypesEdit->Pricetypes->Open();
                                 frmPricetypesEdit->Pricetypes->Edit();
                                 frmPricetypesEdit->ShowModal();
                                 delete frmPricetypesEdit;
                                 SkladData->Pricetypes->FullRefresh();
                                 break;

                             case 12: frmAccountTypeEdit = new TfrmAccountTypeEdit(Application);
                                 frmAccountTypeEdit->AccountType->ParamByName("TYPEID")->Value = SkladData->AccountTypeTYPEID->Value;
                                 frmAccountTypeEdit->AccountType->Open();
                                 frmAccountTypeEdit->AccountType->Edit();
                                 frmAccountTypeEdit->ShowModal();
                                 delete frmAccountTypeEdit;
                                 SkladData->AccountType->FullRefresh();
                                 break;

                             case 43: frmCountriesEdit = new TfrmCountriesEdit(Application);
                                 frmCountriesEdit->Countries->ParamByName("CID")->Value = SkladData->CountriesCID->Value;
                                 frmCountriesEdit->Countries->Open();
                                 frmCountriesEdit->Countries->Edit();
                                 frmCountriesEdit->ShowModal();
                                 delete frmCountriesEdit;
                                 SkladData->Countries->FullRefresh();
                                 break;

                             case 102: frmChargetypeEdit = new TfrmChargetypeEdit(Application);
                                 frmChargetypeEdit->Chargetype->ParamByName("CTYPEID")->Value = SkladData->ChargetypeCTYPEID->Value;
                                 frmChargetypeEdit->Chargetype->Open();
                                 frmChargetypeEdit->Chargetype->Edit();
                                 frmChargetypeEdit->ShowModal();
                                 delete frmChargetypeEdit;
                                 SkladData->Chargetype->FullRefresh();
                                 break;

                             case 64: frmCashdesksEdit = new TfrmCashdesksEdit(Application);
                                 frmCashdesksEdit->Cashdesks->ParamByName("CASHID")->Value = SkladData->CashdesksCASHID->Value;
                                 frmCashdesksEdit->Cashdesks->Open();
                                 frmCashdesksEdit->Cashdesks->Edit();
                                 frmCashdesksEdit->ShowModal();
                                 delete frmCashdesksEdit;
                                 SkladData->Cashdesks->FullRefresh();
                                 break;*/

                        case 42:
                        case 53:
                            dynamic r_item = MatRecipeGridView.GetFocusedRow();
                            new frmMatRecipe(null, r_item.RecId).ShowDialog();
                            //      SkladData->MatRecipe->FullRefresh();
                            break;

                        /*   case 112: frmTechProcessEdit = new TfrmTechProcessEdit(Application);
                               frmTechProcessEdit->TechProcess->ParamByName("PROCID")->Value = SkladData->TechProcessPROCID->Value;
                               frmTechProcessEdit->TechProcess->Open();
                               frmTechProcessEdit->TechProcess->Edit();
                               frmTechProcessEdit->ShowModal();
                               delete frmTechProcessEdit;
                               SkladData->TechProcess->FullRefresh();
                               break;*/
                    }
                    break;

            }
        }

        private void NewItemBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            switch (focused_tree_node.GType)
            {
                case 1:
                    new frmKAgentEdit(focused_tree_node.GrpId).ShowDialog();
                    break;
 
                case 2: var mat_edit = new frmMaterialEdit();
                  
               /*     if (frmMaterialEdit->MatGroup->IsEmpty())
                    {
                        delete frmMaterialEdit;
                        break;
                    }
                  
                    if (frmMaterialEdit->MatGroup->Locate("GRPID", DirectTreeID->Value * -1, TLocateOptions()))
                        frmMaterialEdit->MaterialsGRPID->Value = DirectTreeID->Value * -1;
                    else
                    {
                        frmMaterialEdit->MatGroup->First();
                        frmMaterialEdit->MaterialsGRPID->Value = frmMaterialEdit->MatGroupGRPID->Value;
                    }*/
                    mat_edit.ShowDialog();
                    break;
/*
                case 3: frmServicesEdit = new TfrmServicesEdit(Application);
                    frmServicesEdit->Services->Open();
                    frmServicesEdit->SvcGroup->Locate("GRPID", DirectTreeID->Value, TLocateOptions());
                    frmServicesEdit->Services->Append();
                    frmServicesEdit->ShowModal();
                    delete frmServicesEdit;
                    Services->FullRefresh();
                    break;

                case 4: switch (DirectTreeID->Value)
                    {
                        case 25: frmWarehouseEdit = new TfrmWarehouseEdit(Application);
                            frmWarehouseEdit->Warehouse->Open();
                            frmWarehouseEdit->Warehouse->Append();
                            frmWarehouseEdit->ShowModal();
                            delete frmWarehouseEdit;
                            SkladData->Warehouse->FullRefresh();
                            break;

                        case 11: frmBanksEdit = new TfrmBanksEdit(Application);
                            frmBanksEdit->Banks->Open();
                            frmBanksEdit->Banks->Append();
                            frmBanksEdit->ShowModal();
                            delete frmBanksEdit;
                            SkladData->Banks->FullRefresh();
                            break;

                        case 2: frmMeasuresEdit = new TfrmMeasuresEdit(Application);
                            frmMeasuresEdit->Measures->Open();
                            frmMeasuresEdit->Measures->Append();
                            frmMeasuresEdit->ShowModal();
                            delete frmMeasuresEdit;
                            SkladData->Measures->FullRefresh();
                            break;

                        case 40: frmPricetypesEdit = new TfrmPricetypesEdit(Application);
                            frmPricetypesEdit->Pricetypes->Open();
                            frmPricetypesEdit->Pricetypes->Append();
                            frmPricetypesEdit->ShowModal();
                            delete frmPricetypesEdit;
                            SkladData->Pricetypes->FullRefresh();
                            break;

                        case 12: frmAccountTypeEdit = new TfrmAccountTypeEdit(Application);
                            frmAccountTypeEdit->AccountType->Open();
                            frmAccountTypeEdit->AccountType->Append();
                            frmAccountTypeEdit->ShowModal();
                            delete frmAccountTypeEdit;
                            SkladData->AccountType->FullRefresh();
                            break;

                        case 43: frmCountriesEdit = new TfrmCountriesEdit(Application);
                            frmCountriesEdit->Countries->Open();
                            frmCountriesEdit->Countries->Append();
                            frmCountriesEdit->ShowModal();
                            delete frmCountriesEdit;
                            SkladData->Countries->FullRefresh();
                            break;

                        case 102: frmChargetypeEdit = new TfrmChargetypeEdit(Application);
                            frmChargetypeEdit->Chargetype->Open();
                            frmChargetypeEdit->Chargetype->Append();
                            frmChargetypeEdit->ShowModal();
                            delete frmChargetypeEdit;
                            SkladData->Chargetype->FullRefresh();
                            break;

                        case 64: frmCashdesksEdit = new TfrmCashdesksEdit(Application);
                            frmCashdesksEdit->Cashdesks->Open();
                            frmCashdesksEdit->Cashdesks->Append();
                            frmCashdesksEdit->ShowModal();
                            delete frmCashdesksEdit;
                            SkladData->Cashdesks->FullRefresh();
                            break;

                        case 42:
                        case 53: frmMatRecipe = new TfrmMatRecipe(Application);
                            frmMatRecipe->MatRecipe->Open();
                            frmMatRecipe->MatRecipe->Append();
                            if (DirectTreeID->Value == 53) frmMatRecipe->MatRecipeRTYPE->Value = 1;
                            if (DirectTreeID->Value == 42) frmMatRecipe->MatRecipeRTYPE->Value = 2;
                            frmMatRecipe->ShowModal();
                            delete frmMatRecipe;
                            SkladData->MatRecipe->FullRefresh();
                            break;

                        case 112: frmTechProcessEdit = new TfrmTechProcessEdit(Application);
                            frmTechProcessEdit->TechProcess->Open();
                            frmTechProcessEdit->TechProcess->Append();
                            frmTechProcessEdit->ShowModal();
                            delete frmTechProcessEdit;
                            SkladData->TechProcess->FullRefresh();
                            break;

                    }
                    break;*/

            }

            RefrechItemBtn.PerformClick();
        }

        private void DeleteItemBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (MessageBox.Show("Ви дійсно бажаєте відалити цей запис з довідника?", "Підтвердіть видалення", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
              {
                switch (focused_tree_node.GType)
                {
                    case 1:
                        var ka = resut as KagentList;
                        using (var db = DB.SkladBase())
                        {
                            var item = db.Kagent.Find(ka.KaId);
                            if (item != null)
                            {
                                item.Deleted = 1;
                                db.SaveChanges();
                            }
                        }
                        break;

                    case 2:
                        var r = resut as GetMatList_Result;
                        using (var db = DB.SkladBase())
                        {
                            var mat = db.Materials.Find(r.MatId);
                            if (mat != null)
                            {
                                mat.Deleted = 1;
                                db.SaveChanges();
                            }
                        }
                        break;

                 /*   case 3: Services->Delete();
                        break;

                    case 4: switch (DirectTreeID->Value)
                        {
                            case 25: SkladData->Warehouse->Delete(); break;
                            case 11: SkladData->Banks->Delete(); break;
                            case 2: SkladData->Measures->Delete(); break;
                            case 40: SkladData->Pricetypes->Delete(); break;
                            case 12: SkladData->AccountType->Delete(); break;
                            case 43: SkladData->Countries->Delete(); break;
                            case 102: SkladData->Chargetype->Delete(); break;
                            case 64: SkladData->Cashdesks->Delete(); break;
                            case 42:
                            case 53: SkladData->MatRecipe->Delete(); break;
                            case 112: SkladData->TechProcess->Delete(); break;
                        }
                        break;*/

                }
                RefrechItemBtn.PerformClick();
            }
        }

        private void KaGridView_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            resut = KaGridView.GetFocusedRow();
        }

        private void KaGridView_DoubleClick(object sender, EventArgs e)
        {
            EditItemBtn.PerformClick();
        }

        private void KaGridView_PopupMenuShowing(object sender, DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs e)
        {
            ;
        }

        private void DirTreeList_PopupMenuShowing(object sender, DevExpress.XtraTreeList.PopupMenuShowingEventArgs e)
        {
            Point p2 = Control.MousePosition;
            ExplorerPopupMenu.ShowPopup(p2);
        }

        private void DirTreeList_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                TreeList treeList = sender as TreeList;
                TreeListHitInfo info = treeList.CalcHitInfo(e.Location);
                if (info.Node != null)
                {
                    treeList.FocusedNode = info.Node;
                }
            }
        }

        private void barButtonItem5_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            switch (focused_tree_node.GType)
            {
                case 2: new frmMatGroupEdit(focused_tree_node.GrpId).ShowDialog();
                    break;

              /*  case 3: frmServGroupEdit = new TfrmServGroupEdit(Application);
                    frmServGroupEdit->SvcGroup->ParamByName("GRPID")->Value = DirectTreeGRPID->Value;
                    frmServGroupEdit->SvcGroup->Open();
                    frmServGroupEdit->SvcGroup->Edit();
                    frmServGroupEdit->ShowModal();
                    delete frmServGroupEdit;
                    break;*/
            }

            ExplorerRefreshBtn.PerformClick();
        }

        private void ExplorerRefreshBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DirTreeList.DataSource = DB.SkladBase().GetDirTree(DBHelper.CurrentUser.UserId).ToList();
        }

        private void AddGroupMatBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (focused_tree_node.ImageIndex == 2 && focused_tree_node.GType == 2)
            {
                new frmMatGroupEdit(null, focused_tree_node.GrpId).ShowDialog();
            }
            else new frmMatGroupEdit().ShowDialog();

            ExplorerRefreshBtn.PerformClick();
        }

        private void DelExplorerBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DB.SkladBase().DeleteWhere<MatGroup>(w => w.GrpId == focused_tree_node.GrpId).SaveChanges();
        }
    }
}
