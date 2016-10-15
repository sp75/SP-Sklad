using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Globalization;
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
        private int _ka_archived { get; set; }
        private int _mat_archived { get; set; }

        private GetMatList_Result focused_mat
        {
            get { return MatGridView.GetFocusedRow() as GetMatList_Result; }
        }

        private KagentList focused_kagent
        {
            get { return KaGridView.GetFocusedRow() as KagentList; }
        }

        public class PriceTypesView
        {
            public int PTypeId { get; set; }
            public string Name { get; set; }
            public string TypeName { get; set; }
            public string Summary { get; set; }
            public int Def { get; set; }
        }

        public DirectoriesUserControl()
        {
            InitializeComponent();
            _ka_archived = 0;
            _mat_archived = 0;
        }

        private void DirectoriesUserControl_Load(object sender, EventArgs e)
        {
            mainContentTab.ShowTabHeader = DevExpress.Utils.DefaultBoolean.False;
            extDirTabControl.ShowTabHeader = DevExpress.Utils.DefaultBoolean.False;

            if (!DesignMode)
            {
                custom_mat_list = new List<CustomMatList>();
                MatListGridControl.DataSource = custom_mat_list;

                repositoryItemLookUpEdit1.DataSource = DBHelper.WhList();

                DirTreeBS.DataSource = DB.SkladBase().GetDirTree(DBHelper.CurrentUser.UserId).ToList();
                DirTreeList.ExpandToLevel(1);
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
            var _db = DB.SkladBase();

            switch (focused_tree_node.GType)
            {
                case 1:
                    //  KAgent->ParamByName("WDATE")->Value = frmMain->WorkDateEdit->Date;
                    var ka = DB.SkladBase().KagentList.AsNoTracking().AsEnumerable();
                    if (focused_tree_node.Id != 10) ka = ka.Where(w => w.KType == focused_tree_node.GrpId);
                    if (_ka_archived == 0)
                    {
                        ka = ka.Where(w => w.Archived == 0 || w.Archived == null);
                    }

                    KAgentDS.DataSource = ka.ToList();
                    break;

                case 2:
                    MatListDS.DataSource = _db.GetMatList(focused_tree_node.Id == 6 ? -1 : focused_tree_node.GrpId, 0, _mat_archived, showChildNodeBtn.Down ? 1 : 0);
                    break;

                case 3:
                    if (focused_tree_node.Id == 51) ServicesBS.DataSource = DB.SkladBase().v_Services.ToList();
                    else ServicesBS.DataSource = DB.SkladBase().v_Services.Where(w => w.GrpId == focused_tree_node.GrpId).ToList();
                    break;

                case 4: switch (focused_tree_node.Id)
                    {
                        case 25:
                            WarehouseBS.DataSource = DB.SkladBase().Warehouse.ToList();
                            extDirTabControl.SelectedTabPageIndex = 1;
                            break;

                        case 11:
                            BanksBS.DataSource = DB.SkladBase().Banks.ToList();
                            extDirTabControl.SelectedTabPageIndex = 9;
                            break;

                        case 2:
                            MeasuresDS.DataSource = DB.SkladBase().Measures.ToList();
                            extDirTabControl.SelectedTabPageIndex = 2;
                            break;

                        case 12:
                            AccountTypeBS.DataSource = DB.SkladBase().AccountType.ToList();
                            extDirTabControl.SelectedTabPageIndex = 5;
                            break;

                        case 43: CountriesBS.DataSource = DB.SkladBase().Countries.ToList();
                            extDirTabControl.SelectedTabPageIndex = 8;
                            break;

                        case 40:
                            PriceTypesViewBS.DataSource = _db.v_PriceTypes.ToList().Select(s => new PriceTypesView
                            {
                                PTypeId = s.PTypeId,
                                Name = s.Name,
                                Def = s.Def,
                                TypeName = s.TypeName,
                                Summary = (s.PPTypeId == null || s.ExtraType == 2 || s.ExtraType == 3)
                                                ? ((s.PPTypeId == null) ? s.OnValue.ToString("0.00") + "% на ціну прихода" : (s.ExtraType == 2) ? s.OnValue.ToString("0.00") + "% на категорію " + s.PtName : (s.ExtraType == 3) ? s.OnValue.ToString("0.00") + "% на прайс-лист " + s.PtName : "")
                                                : s.OnValue.ToString("0.00") + "% від " + s.PtName

                            });
                            extDirTabControl.SelectedTabPageIndex = 4;
                            break;

                        case 102:
                            ChargeTypeBS.DataSource = DB.SkladBase().ChargeType.ToList();
                            extDirTabControl.SelectedTabPageIndex = 6;
                            break;

                        case 64:
                            CashDesksBS.DataSource = DB.SkladBase().CashDesks.ToList();
                            extDirTabControl.SelectedTabPageIndex = 7;
                            break;

                        //      case 3: cxGridLevel6->GridView = CurrencyGrid; break;

                        case 53:
                            MatRecipeDS.DataSource = _db.MatRecipe.Where(w => w.RType == 1).Select(s => new
                            {
                                s.RecId,
                                MatName = s.Materials.Name,
                                s.OnDate,
                                s.Amount,
                                s.Materials.Measures.ShortName,
                                s.Name,
                                GrpName = s.Materials.MatGroup.Name
                            }).ToList();

                            extDirTabControl.SelectedTabPageIndex = 0;
                            break;

                        case 42:
                            MatRecipeDS.DataSource = _db.MatRecipe.Where(w => w.RType == 2).Select(s => new
                            {
                                s.RecId,
                                MatName = s.Materials.Name,
                                s.OnDate,
                                s.Amount,
                                s.Materials.Measures.ShortName,
                                s.Name,
                                GrpName = s.Materials.MatGroup.Name
                            }).ToList();
                            extDirTabControl.SelectedTabPageIndex = 0;
                            break;

                        //       case 68: cxGridLevel6->GridView = TaxesGrid; break;

                        case 112:
                            TechProcessDS.DataSource = _db.TechProcess.ToList();
                            extDirTabControl.SelectedTabPageIndex = 3;
                            break;
                    }
                    break;
            }
        }

        private void MatGridView_DoubleClick(object sender, EventArgs e)
        {
            if (isMatList)
            {
                AddItem.PerformClick();
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
            //resut = MatGridView.GetFocusedRow();
        }

        private void EditItemBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DialogResult result = DialogResult.Cancel;
            switch (focused_tree_node.GType)
            {
                case 1:
                    var ka = KaGridView.GetFocusedRow() as KagentList;
                    result = new frmKAgentEdit(null, ka.KaId).ShowDialog();
                    break;

                case 2:
                    var r = MatGridView.GetFocusedRow() as GetMatList_Result;
                    result = new frmMaterialEdit(r.MatId).ShowDialog();
                    break;

                case 3: var svc_row = ServicesGridView.GetFocusedRow() as v_Services;
                    result = new frmServicesEdit(svc_row.SvcId).ShowDialog();
                    break;

                case 4: switch (focused_tree_node.Id)
                    {
                        case 25:
                            result = new frmWarehouseEdit((WarehouseGridView.GetFocusedRow() as Warehouse).WId).ShowDialog();
                            break;

                        case 11:
                            result = new frmBanksEdit((BanksGridView.GetFocusedRow() as Banks).BankId).ShowDialog();
                                break;

                        case 2:
                            result = new frmMeasuresEdit((MeasuresGridView.GetFocusedRow() as Measures).MId).ShowDialog();
                            break;

                        case 40: result = new frmPricetypesEdit((PriceTypesGridView.GetFocusedRow() as PriceTypesView).PTypeId).ShowDialog();
                            break;

                        case 12: result = new frmAccountTypeEdit((AccountTypeGridView.GetFocusedRow() as AccountType).TypeId).ShowDialog();
                            break;

                        case 43:
                            result = new frmCountriesEdit((CountriesGridView.GetFocusedRow() as Countries).CId).ShowDialog();

                            break;

                        case 102: result = new frmChargeTypeEdit((ChargeTypeGridView.GetFocusedRow() as ChargeType).CTypeId).ShowDialog();
                            break;

                        case 64: result = new frmCashdesksEdit((CashDesksGridView.GetFocusedRow() as CashDesks).CashId).ShowDialog();
                            break;

                        case 42:
                        case 53:
                            dynamic r_item = MatRecipeGridView.GetFocusedRow();
                            result = new frmMatRecipe(null, r_item.RecId).ShowDialog();

                            break;

                        case 112:
                            result = new frmTechProcessEdit((TechProcessGridView.GetFocusedRow() as TechProcess).ProcId).ShowDialog();
                            break;
                    }
                    break;

            }

            if (result == DialogResult.OK)
            {
                RefrechItemBtn.PerformClick();
            }
        }

        private void NewItemBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            switch (focused_tree_node.GType)
            {
                case 1:
                    new frmKAgentEdit(focused_tree_node.GrpId).ShowDialog();
                    break;

                case 2:
                    if (DB.SkladBase().MatGroup.Any())
                    {
                        var mat_edit = new frmMaterialEdit(null, focused_tree_node.Id < 0 ? focused_tree_node.Id * -1 : DB.SkladBase().MatGroup.First().GrpId);
                        mat_edit.ShowDialog();
                    }
                    break;

                case 3: if (DB.SkladBase().SvcGroup.Any())
                    {
                        var svc_edit = new frmServicesEdit(null, focused_tree_node.Id < 0 ? focused_tree_node.Id * -1 : DB.SkladBase().SvcGroup.First().GrpId);
                        svc_edit.ShowDialog();
                    }
                    break;

                case 4: switch (focused_tree_node.Id)
                    {
                        case 25:
                            new frmWarehouseEdit().ShowDialog();
                            break;

                        case 11: new frmBanksEdit().ShowDialog();
                                     break;

                        case 2:
                            new frmMeasuresEdit().ShowDialog();
                            break;

                        case 40:
                            new frmPricetypesEdit().ShowDialog();
                            break;

                        case 12:
                            new frmAccountTypeEdit().ShowDialog();
                            break;

                        case 43: 
                            new frmCountriesEdit().ShowDialog();
                                            break;

                        case 102:
                            new frmChargeTypeEdit().ShowDialog();
                            break;

                        case 64: new frmCashdesksEdit().ShowDialog();
                            break;

                        case 42:
                            new frmMatRecipe(2).ShowDialog();
                            break;

                        case 53:
                            new frmMatRecipe(1).ShowDialog();
                            break;

                        case 112:
                            new frmTechProcessEdit().ShowDialog();
                            break;

                    }
                    break;
            }

            RefrechItemBtn.PerformClick();
        }

        private void DeleteItemBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (MessageBox.Show("Ви дійсно бажаєте відалити цей запис з довідника?", "Підтвердіть видалення", MessageBoxButtons.YesNo, MessageBoxIcon.Information) != DialogResult.Yes)
            {
                return;
            }

            using (var db = DB.SkladBase())
            {
                switch (focused_tree_node.GType)
                {
                    case 1:
                        var ka = KaGridView.GetFocusedRow() as KagentList;

                        var item = db.Kagent.Find(ka.KaId);
                        if (item != null)
                        {
                            item.Deleted = 1;
                        }

                        break;

                    case 2:
                        var r = MatGridView.GetFocusedRow() as GetMatList_Result;

                        var mat = db.Materials.Find(r.MatId);
                        if (mat != null)
                        {
                            mat.Deleted = 1;
                        }

                        break;

                    case 3:
                        var svc_row = ServicesGridView.GetFocusedRow() as v_Services;

                        var svc = db.Services.Find(svc_row.SvcId);
                        if (svc != null)
                        {
                            svc.Deleted = 1;
                        }
                        break;

                    case 4: switch (focused_tree_node.Id)
                        {
                            case 25:
                                var wh = WarehouseGridView.GetFocusedRow() as Warehouse;
                                db.DeleteWhere<Warehouse>(w => w.WId == wh.WId);
                                break;
                            case 11:

                                var b = BanksGridView.GetFocusedRow() as Banks;
                                db.DeleteWhere<Banks>(w => w.BankId == b.BankId);
                                break;

                            case 2:
                                var m = MeasuresGridView.GetFocusedRow() as Measures;
                                db.DeleteWhere<Measures>(w => w.MId == m.MId);
                                break;

                            case 40:
                                var pt = PriceTypesGridView.GetFocusedRow() as PriceTypesView;
                                db.DeleteWhere<PriceTypes>(w => w.PTypeId == pt.PTypeId);
                                break;

                            case 12:
                                var at = AccountTypeGridView.GetFocusedRow() as AccountType;
                                db.DeleteWhere<AccountType>(w => w.TypeId == at.TypeId);
                                break;

                            case 102:
                                var ct = ChargeTypeGridView.GetFocusedRow() as ChargeType;
                                db.DeleteWhere<ChargeType>(w => w.CTypeId == ct.CTypeId);
                                break;

                            case 64:
                                var cd = CashDesksGridView.GetFocusedRow() as CashDesks;
                                db.DeleteWhere<CashDesks>(w => w.CashId == cd.CashId);
                                break;

                            case 43:
                                var c = CountriesGridView.GetFocusedRow() as Countries;
                                db.DeleteWhere<Countries>(w => w.CId == c.CId);
                                break;

                            case 42:
                            case 53:
                                int mat_rec = ((dynamic)MatRecipeGridView.GetFocusedRow()).RecId;
                                db.DeleteWhere<MatRecipe>(w => w.RecId == mat_rec);
                                break;
                            case 112:

                                var tp = TechProcessGridView.GetFocusedRow() as TechProcess;
                                db.DeleteWhere<TechProcess>(w => w.ProcId == tp.ProcId);

                                break;
                        }
                        break;

                }

                db.SaveChanges();
            }
            RefrechItemBtn.PerformClick();
        }

        private void KaGridView_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            ;
        }

        private void KaGridView_DoubleClick(object sender, EventArgs e)
        {
            if (isDirectList)
            {
                var frm = this.Parent as frmCatalog;
                frm.OkButton.PerformClick();
            }
            else
            {
                EditItemBtn.PerformClick();
            }
        }

        private void KaGridView_PopupMenuShowing(object sender, DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs e)
        {
            KAgentPopupMenu.ShowPopup(Control.MousePosition); 
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
            DirTreeBS.DataSource = DB.SkladBase().GetDirTree(DBHelper.CurrentUser.UserId).ToList();
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
            switch (focused_tree_node.GType)
            {
                case 2: DB.SkladBase().DeleteWhere<MatGroup>(w => w.GrpId == focused_tree_node.GrpId).SaveChanges();
                    break;
                case 3: DB.SkladBase().DeleteWhere<SvcGroup>(w => w.GrpId == focused_tree_node.GrpId).SaveChanges();
                    break;
            }

            ExplorerRefreshBtn.PerformClick();
        }

        private void MatRecipeGridView_DoubleClick(object sender, EventArgs e)
        {
            EditItemBtn.PerformClick();
        }

        private void KagentBalansBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var ka = KaGridView.GetFocusedRow() as KagentList;

            IHelper.ShowKABalans(ka.KaId);
        }

        private void barButtonItem8_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (focused_tree_node.ImageIndex == 2 && focused_tree_node.GType == 3)
            {
                new frmSvcGroupEdit(null, focused_tree_node.GrpId).ShowDialog();
            }
            else new frmSvcGroupEdit().ShowDialog();

            ExplorerRefreshBtn.PerformClick();
        }

        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var dr = KaGridView.GetFocusedRow() as KagentList;

            IHelper.ShowOrdered(dr.KaId, 0, 0);
        }


        private void WarehouseGridView_DoubleClick(object sender, EventArgs e)
        {
            EditItemBtn.PerformClick();
        }

        private void AddItem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var row = MatGridView.GetFocusedRow() as GetMatList_Result;
            var p_type = (wb.Kagent != null ? wb.Kagent.PTypeId : null);
            var mat_price = DB.SkladBase().GetListMatPrices(row.MatId, wb.CurrId, p_type).FirstOrDefault();

            custom_mat_list.Add(new CustomMatList
            {
                Num = custom_mat_list.Count() + 1,
                MatId = row.MatId,
                Name = row.Name,
                Amount = 1,
                Price = mat_price != null ? mat_price.Price : 0,
                WId = row.WId != null ? row.WId.Value : DBHelper.WhList().FirstOrDefault(w => w.Def == 1).WId
            });

            MatListGridView.RefreshData();
        }

        private void DelItem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            MatListGridView.DeleteSelectedRows();
        }

        private void MatGridView_PopupMenuShowing(object sender, DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs e)
        {
            MatPopupMenu.ShowPopup(Control.MousePosition);    
        }

        private void barButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            IHelper.ShowMatInfo(focused_mat.MatId);
        }

        private void barButtonItem5_ItemClick_1(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            IHelper.ShowMatRSV(focused_mat.MatId, DB.SkladBase());
        }

        private void barButtonItem12_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if(!IHelper.FindMatInWH(focused_mat.MatId))
            {
                MessageBox.Show(string.Format( "На даний час товар <{0}> на складі вдсутній!", focused_mat.Name ));
            }
        }

        private void barButtonItem13_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var db = DB.SkladBase();
            var mat = db.Materials.Find(focused_mat.MatId);
            if (mat != null)
            {
                if (mat.Archived == 1)
                {
                    mat.Archived = 0;
                }
                else
                {
                    if (MessageBox.Show(string.Format("Ви дійсно хочете перемістити матеріал <{0}> в архів?", focused_mat.Name), "Information", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                    {
                        if (focused_mat.Remain == 0) mat.Archived = 1;
                        else MessageBox.Show(string.Format("Неможливо перемістити матеріал <{0}> в архів, \nтому що його залишок складає {1} {2}", focused_mat.Name, focused_mat.Remain.ToString(CultureInfo.InvariantCulture), focused_mat.ShortName));
                    }
                }
            }
            db.SaveChanges();

            RefrechItemBtn.PerformClick();
        }

        private void barCheckItem1_CheckedChanged(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            _mat_archived = showMatArhivedBtn.Checked ? 1 : 0;

            RefrechItemBtn.PerformClick();
        }

        private void barCheckItem1_CheckedChanged_1(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            _ka_archived = barCheckItem1.Checked ? 1 : 0;
            RefrechItemBtn.PerformClick();
        }

        private void barButtonItem10_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var db = DB.SkladBase();
            var ka = db.Kagent.Find(focused_kagent.KaId);

            if (ka == null)
            {
                return;
            }

            if (ka.Archived == 1)
            {
                ka.Archived = 0; 
            }
            else
            {
                if (MessageBox.Show(string.Format("Ви дійсно хочете перемістити контрагента <{0}> в архів?", ka.Name), "Information", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    if (focused_kagent.Saldo == 0 || focused_kagent.Saldo == null)
                    {
                        ka.Archived = 1;
                    }
                    else
                    {
                        MessageBox.Show(string.Format("Неможливо перемістити контрагента <{0}> в архів, \nтому що його поточний баланс рівний {1}", ka.Name, focused_kagent.Saldo.ToString()));
                    }
                }
            }
            db.SaveChanges();
            RefrechItemBtn.PerformClick();
        }

        private void barButtonItem11_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            RefrechItemBtn.PerformClick();
        }
    }
}
