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
using System.IO;
using System.Diagnostics;
using SP_Sklad.Properties;
using DevExpress.XtraTreeList.Nodes;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.Data;
using SP_Sklad.WBForm;
using DevExpress.XtraGrid;

namespace SP_Sklad.MainTabs
{
    public partial class DirectoriesUserControl : DevExpress.XtraEditors.XtraUserControl
    {
        GetDirTree_Result focused_tree_node { get; set; }
        public bool isDirectList { get; set; }
        public bool isMatList { get; set; }
        public List<CustomMatList> custom_mat_list { get; set; }
        public WaybillList wb { get; set; }
        private int _ka_archived { get; set; }
        private int _mat_archived { get; set; }
        private bool _show_rec_archived { get; set; }

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
            _show_rec_archived = false;
        }

        private void DirectoriesUserControl_Load(object sender, EventArgs e)
        {
            mainContentTab.ShowTabHeader = DevExpress.Utils.DefaultBoolean.False;
            extDirTabControl.ShowTabHeader = DevExpress.Utils.DefaultBoolean.False;
            ucMaterials.isDirectList = isDirectList;
            ucKagents.isDirectList = isDirectList;

            if (!DesignMode)
            {
                custom_mat_list = new List<CustomMatList>();

                DirTreeBS.DataSource = DB.SkladBase().GetDirTree(DBHelper.CurrentUser.UserId).ToList();
                DirTreeList.ExpandToLevel(1);
            }
        }

        private void DirTreeList_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
        {
            focused_tree_node = DirTreeList.GetDataRecordByNode(e.Node) as GetDirTree_Result;

            if (focused_tree_node == null)
            {
                return;
            }

            NewItemBtn.Enabled = focused_tree_node != null && focused_tree_node.CanInsert == 1;
            DeleteItemBtn.Enabled = (focused_tree_node != null && focused_tree_node.CanDelete == 1);
            EditItemBtn.Enabled = (focused_tree_node != null && focused_tree_node.CanModify == 1);
            CopyItemBtn.Enabled = (focused_tree_node != null && focused_tree_node.CanModify == 1);

            DelExplorerBtn.Enabled = (focused_tree_node.FunId == 6 || focused_tree_node.FunId == 38 || focused_tree_node.FunId == 82 || focused_tree_node.FunId == 66);
            RenameMatGroupBarButtonItem.Enabled = DelExplorerBtn.Enabled;
            EditExplorerBtn.Enabled = DelExplorerBtn.Enabled;
            btnMoveDown.Enabled = DelExplorerBtn.Enabled;
            btnMoveUp.Enabled = DelExplorerBtn.Enabled;


            RefrechItemBtn.PerformClick();


            if (focused_tree_node.GType.Value == 1)
            {
                bar1.Visible = false;
                ucKagents.KType =  focused_tree_node.Id != 10 ? focused_tree_node.GrpId : -1;
                ucKagents.FunId = focused_tree_node.FunId;
                ucKagents.GetData(false);
            }
            else if (focused_tree_node.GType.Value == 2)
            {
                bar1.Visible = false;
                ucMaterials.GrpId = focused_tree_node.Id == 6 ? -1 : focused_tree_node.GrpId;
                ucMaterials.GetData(showChildNodeBtn.Down, restore: false);
            }
            else if (focused_tree_node.GType.Value == 3)
            {
                bar1.Visible = false;
                ucServices.GrpId = focused_tree_node.Id == 51 ? -1 : focused_tree_node.GrpId;
                ucServices.GetData(false);
            }
            else
            {
                bar1.Visible = true;
            }


            mainContentTab.SelectedTabPageIndex = focused_tree_node.GType.Value;


            if (focused_tree_node.FunId != null)
            {
                History.AddEntry(new HistoryEntity { FunId = focused_tree_node.FunId.Value, MainTabs = 6 });

                if (DirTreeList.ContainsFocus)
                {
                    Settings.Default.LastFunId = focused_tree_node.FunId.Value;
                }
            }
        }


        private void RefrechItemBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var _db = DB.SkladBase();
            int top_row;

            switch (focused_tree_node.GType)
            {
                case 1:
                    break;

                case 2:
                    break;

                case 3:
                 /*   if (focused_tree_node.Id == 51) ServicesBS.DataSource = DB.SkladBase().v_Services.AsNoTracking().ToList();
                    else ServicesBS.DataSource = DB.SkladBase().v_Services.AsNoTracking().Where(w => w.GrpId == focused_tree_node.GrpId).ToList();*/
                    break;

                case 4:
                    switch (focused_tree_node.FunId/*focused_tree_node.Id < 0 ? focused_tree_node.PId : focused_tree_node.Id*/)
                    {
                        case 20:
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

                        case 32:
                            var top = CountriesGridView.TopRowIndex;
                            CountriesBS.DataSource = DB.SkladBase().Countries.ToList();
                            CountriesGridView.TopRowIndex = top;

                            extDirTabControl.SelectedTabPageIndex = 8;
                            break;

                        case 31:
                            PriceTypesViewBS.DataSource = _db.v_PriceTypes.ToList().Select(s => new PriceTypesView
                            {
                                PTypeId = s.PTypeId,
                                Name = s.Name,
                                Def = s.Def,
                                TypeName = s.TypeName,
                                Summary = (s.PPTypeId == null || s.ExtraType == 2 || s.ExtraType == 3 || s.ExtraType == 4)
                                                ? ((s.PPTypeId == null) ? (s.ExtraType == 4 ? $"{s.OnValue.ToString("0.00")}% на встановлену ціну" : $"{s.OnValue.ToString("0.00")}% на ціну прихода") : (s.ExtraType == 2) ? s.OnValue.ToString("0.00") + "% на категорію " + s.PtName : (s.ExtraType == 3) ? s.OnValue.ToString("0.00") + "% на прайс-лист " + s.PtName : "")
                                                : s.OnValue.ToString("0.00") + "% від " + s.PtName

                            });
                            extDirTabControl.SelectedTabPageIndex = 4;
                            break;

                        case 51:
                            ChargeTypeBS.DataSource = DB.SkladBase().ChargeType.ToList();
                            extDirTabControl.SelectedTabPageIndex = 6;
                            break;

                        case 56:
                            if (isDirectList)
                            {
                                CashDesksBS.DataSource = DB.SkladBase().CashDesks.ToList().Where(w => DBHelper.CashDesks.Select(s => s.CashId).Contains(w.CashId)).ToList();
                            }
                            else
                            {
                                CashDesksBS.DataSource = DB.SkladBase().CashDesks.ToList();
                            }
                            extDirTabControl.SelectedTabPageIndex = 7;
                            break;

                        //      case 3: cxGridLevel6->GridView = CurrencyGrid; break;

                        case 40:
                            top_row = MatRecipeGridView.TopRowIndex;
                            var recipe_list = _db.v_MatRecipe.AsNoTracking().Where(w => w.RType == 1);
                            if (!_show_rec_archived)
                            {
                                recipe_list = recipe_list.Where(w => !w.Archived);
                            }
                            if (focused_tree_node.Id < 0)
                            {
                                recipe_list = recipe_list.Where(w => w.GrpId == focused_tree_node.GrpId);
                            }

                            MatRecipeDS.DataSource = recipe_list.ToList();
                            MatRecipeGridView.TopRowIndex = top_row;
                            //       MatRecipeGridView.ExpandAllGroups();

                            extDirTabControl.SelectedTabPageIndex = 0;
                            break;

                        case 28:
                            top_row = MatRecipeGridView.TopRowIndex;
                            var recipe_d_list = _db.v_MatRecipe.AsNoTracking().Where(w => w.RType == 2);

                            if (!_show_rec_archived)
                            {
                                recipe_d_list = recipe_d_list.Where(w => !w.Archived);
                            }

                            if (focused_tree_node.Id < 0)
                            {
                                recipe_d_list = recipe_d_list.Where(w => w.GrpId == focused_tree_node.GrpId);
                            }

                            MatRecipeDS.DataSource = recipe_d_list.ToList();
                            MatRecipeGridView.TopRowIndex = top_row;
                            //        MatRecipeGridView.ExpandAllGroups();

                            extDirTabControl.SelectedTabPageIndex = 0;
                            break;

                        //       case 68: cxGridLevel6->GridView = TaxesGrid; break;

                        case 69:
                            TechProcessDS.DataSource = _db.TechProcess.OrderBy(o => o.Num).ToList();
                            extDirTabControl.SelectedTabPageIndex = 3;
                            break;

                        case 73:
                            RoutesBS.DataSource = _db.Routes.AsNoTracking().ToList();
                            extDirTabControl.SelectedTabPageIndex = 10;
                            break;

                        case 66:
                            var disc_cards = _db.v_DiscCards.AsNoTracking().AsQueryable();

                            if (focused_tree_node.Id < 0)
                            {
                                disc_cards = disc_cards.Where(w => w.GrpId == focused_tree_node.GrpId);
                            }

                            DiscCardsBS.DataSource = disc_cards.ToList();
                            extDirTabControl.SelectedTabPageIndex = 11;
                            break;

                        case 3:
                            CurrencyBS.DataSource = _db.Currency.ToList();
                            extDirTabControl.SelectedTabPageIndex = 12;
                            break;

                        case 81:
                            PreparationMatRecipeGridControl.DataSource = _db.v_MatRecipe.AsNoTracking().Where(w => w.RType == 3).ToList();
                            extDirTabControl.SelectedTabPageIndex = 13;
                            break;

                        case 87:
                            CarsBS.DataSource = _db.Cars.ToList();
                            extDirTabControl.SelectedTabPageIndex = 14;
                            break;

                        case 91:
                            KAgentAccountBS.DataSource = _db.v_KAgentAccount.ToList();
                            extDirTabControl.SelectedTabPageIndex = 15;
                            break;

                        case 96:
                            waybillTemplateUserControl1.GetDataList();
                            extDirTabControl.SelectedTabPageIndex = 16;
                            break;

                    }
                    break;

                case 5:
                    top_row = TaraGridView.TopRowIndex;
                    TaraListDS.DataSource = _db.GetTaraList(focused_tree_node.Id == 125 ? -1 : focused_tree_node.GrpId, showChildNodeBtn.Down ? 1 : 0);
                    TaraGridView.TopRowIndex = top_row;
                    break;
            }
        }

        private void EditItemBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DialogResult result = DialogResult.Cancel;
            switch (focused_tree_node.GType)
            {
                case 1:
                  
                    break;

                case 2:
                    break;

                case 3:
                    break;

                case 5:
                    var tara = TaraGridView.GetFocusedRow() as GetTaraList_Result;

                    if (tara != null)
                    {
                        using (var frm_edit = new frmTaraEdit(tara.TaraId))
                        {
                            frm_edit.ShowDialog();
                        }
                    }
                    break;

                case 4:
                    switch (focused_tree_node.FunId)
                    {
                        case 3:
                            result = new frmCurrencyRate((CurrencyGridView.GetFocusedRow() as Currency).CurrId, DateTime.Now).ShowDialog();
                            break;

                        case 20:
                            result = new frmWarehouseEdit((WarehouseGridView.GetFocusedRow() as Warehouse).WId).ShowDialog();
                            break;

                        case 11:
                            result = new frmBanksEdit((BanksGridView.GetFocusedRow() as Banks).BankId).ShowDialog();
                            break;

                        case 2:
                            result = new frmMeasuresEdit((MeasuresGridView.GetFocusedRow() as Measures).MId).ShowDialog();
                            break;

                        case 31:
                            result = new frmPricetypesEdit((PriceTypesGridView.GetFocusedRow() as PriceTypesView).PTypeId).ShowDialog();
                            break;

                        case 12:
                            result = new frmAccountTypeEdit((AccountTypeGridView.GetFocusedRow() as AccountType).TypeId).ShowDialog();
                            break;

                        case 32:
                            var c_row = (CountriesGridView.GetFocusedRow() as Countries);
                            result = new frmCountriesEdit(c_row.CId).ShowDialog();
                            /* if (result == DialogResult.OK)
                             {
                                 var ds = CountriesBS.DataSource as List<Countries>;
                                 var fff = ds.FirstOrDefault(w => w.CId == c_row.CId);
                                 fff = DB.SkladBase().Countries.AsNoTracking().Where(w => w.CId == c_row.CId).First();
                                 CountriesGridView.SetFocusedValue(fff);
                                
                        //         CountriesGridView.RefreshRow(CountriesGridView.FocusedRowHandle);
                                 CountriesGridView.RefreshData();
                              //   CountriesBS.ResetBindings(false);
                             }*/

                            break;

                        case 51:
                            result = new frmChargeTypeEdit((ChargeTypeGridView.GetFocusedRow() as ChargeType).CTypeId).ShowDialog();
                            break;

                        case 56:
                            result = new frmCashdesksEdit((CashDesksGridView.GetFocusedRow() as CashDesks).CashId).ShowDialog();
                            break;

                        case 28:
                            if (MatRecipeGridView.FocusedRowHandle >= 0)
                            {
                                dynamic ob_item = MatRecipeGridView.GetFocusedRow();
                                result = new frmMatRecipe(2, ob_item.RecId).ShowDialog();
                            }
                            break;

                        case 40:
                            if (MatRecipeGridView.FocusedRowHandle >= 0)
                            {
                                dynamic r_item = MatRecipeGridView.GetFocusedRow();
                                result = new frmMatRecipe(1, r_item.RecId).ShowDialog();
                            }
                            break;

                        case 69:
                            result = new frmTechProcessEdit((TechProcessGridView.GetFocusedRow() as TechProcess).ProcId).ShowDialog();
                            break;

                        case 73:
                            result = new frmRouteEdit((RouteGridView.GetFocusedRow() as Routes).Id).ShowDialog();
                            break;

                        case 66:
                            var dc = DiscCardsGridView.GetFocusedRow() as v_DiscCards;
                            result = new frmDiscountCardEdit(dc.CardId).ShowDialog();
                            break;

                        case 81:
                            if (PreparationMatRecipeGridView.FocusedRowHandle >= 0)
                            {
                                dynamic r_item = PreparationMatRecipeGridView.GetFocusedRow();
                                result = new frmPreparationMatRecipe(r_item.RecId).ShowDialog();
                            }
                            break;

                        case 87:
                            result = new frmCarEdit((CarsGridView.GetFocusedRow() as Cars).Id).ShowDialog();
                            break;

                        case 91:
                            result = new frmKaAccountEdit((KAgentAccountGridView.GetFocusedRow() as v_KAgentAccount).AccId).ShowDialog();
                            break;

                        case 96:
                            result = waybillTemplateUserControl1.EditFocusedRow();
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
                    using (var k_frm = new frmKAgentEdit(focused_tree_node.GrpId))
                    {
                        k_frm.ShowDialog();
                    }
                    break;

                case 2:
                    if (DB.SkladBase().MatGroup.Any())
                    {
                        using (var mat_edit = new frmMaterialEdit(null, focused_tree_node.Id < 0 ? focused_tree_node.Id * -1 : DB.SkladBase().MatGroup.First().GrpId))
                        {
                            mat_edit.ShowDialog();
                        }
                    }
                    break;

                case 3:
                    if (DB.SkladBase().SvcGroup.Any())
                    {
                        var svc_edit = new frmServicesEdit(null, focused_tree_node.Id < 0 ? (focused_tree_node.Id * -1 - 1000000) : DB.SkladBase().SvcGroup.First().GrpId);
                        svc_edit.ShowDialog();
                    }
                    break;

                case 5:
                    if (DB.SkladBase().TaraGroup.Any())
                    {
                        using (var frm_edit = new frmTaraEdit(TaraGrp: focused_tree_node.Id < 0 ? (focused_tree_node.Id * -1 - 2000000) : DB.SkladBase().TaraGroup.First().GrpId))
                        {
                            frm_edit.ShowDialog();
                        }
                    }
                    break;

                case 4:
                    switch (focused_tree_node.FunId)
                    {
                        case 20:
                            new frmWarehouseEdit().ShowDialog();
                            break;

                        case 11:
                            new frmBanksEdit().ShowDialog();
                            break;

                        case 2:
                            new frmMeasuresEdit().ShowDialog();
                            break;

                        case 31:
                            new frmPricetypesEdit().ShowDialog();
                            break;

                        case 12:
                            new frmAccountTypeEdit().ShowDialog();
                            break;

                        case 32:
                            new frmCountriesEdit().ShowDialog();
                            break;

                        case 51:
                            new frmChargeTypeEdit().ShowDialog();
                            break;

                        case 56:
                            new frmCashdesksEdit().ShowDialog();
                            break;

                        case 28:
                            new frmMatRecipe(2).ShowDialog();
                            break;

                        case 40:
                            new frmMatRecipe(1).ShowDialog();
                            break;

                        case 69:
                            new frmTechProcessEdit().ShowDialog();
                            break;

                        case 73:
                            new frmRouteEdit().ShowDialog();
                            break;

                        case 66:
                            new frmDiscountCardEdit().ShowDialog();
                            break;

                        case 81:
                            new frmPreparationMatRecipe().ShowDialog();
                            break;

                        case 87:
                            new frmCarEdit().ShowDialog();
                            break;

                        case 91:
                            new frmKaAccountEdit().ShowDialog();
                            break;

                        case 96:
                            waybillTemplateUserControl1.AddNewItem();
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
                        break;

                    case 2:
                        break;

                    case 3:
                        break;

                    case 5:
                        var tara_row = TaraGridView.GetFocusedRow() as GetTaraList_Result;

                        var tara = db.Tara.Find(tara_row.TaraId);
                        if (tara != null)
                        {
                            tara.Deleted = 1;
                        }
                        break;

                    case 4:
                        switch (focused_tree_node.FunId)
                        {
                            case 20:
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

                            case 31:
                                var pt = PriceTypesGridView.GetFocusedRow() as PriceTypesView;
                                db.DeleteWhere<PriceTypes>(w => w.PTypeId == pt.PTypeId);
                                break;

                            case 12:
                                var at = AccountTypeGridView.GetFocusedRow() as AccountType;
                                db.DeleteWhere<AccountType>(w => w.TypeId == at.TypeId);
                                break;

                            case 51:
                                var ct = ChargeTypeGridView.GetFocusedRow() as ChargeType;
                                db.DeleteWhere<ChargeType>(w => w.CTypeId == ct.CTypeId);
                                break;

                            case 56:
                                var cd = CashDesksGridView.GetFocusedRow() as CashDesks;
                                db.DeleteWhere<CashDesks>(w => w.CashId == cd.CashId);
                                break;

                            case 32:
                                var c = CountriesGridView.GetFocusedRow() as Countries;
                                db.DeleteWhere<Countries>(w => w.CId == c.CId);
                                break;

                            case 28:
                            case 40:
                                int mat_rec = ((dynamic)MatRecipeGridView.GetFocusedRow()).RecId;
                                db.DeleteWhere<MatRecipe>(w => w.RecId == mat_rec);
                                break;

                            case 69:
                                var tp = TechProcessGridView.GetFocusedRow() as TechProcess;
                                db.DeleteWhere<TechProcess>(w => w.ProcId == tp.ProcId);
                                break;

                            case 73:
                                var rou = (RouteGridView.GetFocusedRow() as Routes);
                                db.DeleteWhere<Routes>(w => w.Id == rou.Id);
                                break;

                            case 66:
                                var dc = DiscCardsGridView.GetFocusedRow() as v_DiscCards;
                                db.DeleteWhere<DiscCards>(w => w.CardId == dc.CardId);
                                break;

                            case 81:
                                int prop_mat_rec = ((dynamic)PreparationMatRecipeGridView.GetFocusedRow()).RecId;
                                db.DeleteWhere<MatRecipe>(w => w.RecId == prop_mat_rec);
                                break;

                            case 87:
                                var car = (CarsGridView.GetFocusedRow() as Cars);
                                db.DeleteWhere<Cars>(w => w.Id == car.Id);
                                break;

                            case 91:
                                var acc_id = (KAgentAccountGridView.GetFocusedRow() as v_KAgentAccount).AccId;
                                db.DeleteWhere<KAgentAccount>(w => w.AccId == acc_id);
                                break;

                            case 96:
                                waybillTemplateUserControl1.DeleteFocusedRow();
                                break;

                        }
                        break;

                }

                db.SaveChanges();
            }
            RefrechItemBtn.PerformClick();
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
            if (e.HitInfo.InRow)
            {
                KAgentPopupMenu.ShowPopup(Control.MousePosition);
            }
        }

        private void DirTreeList_PopupMenuShowing(object sender, DevExpress.XtraTreeList.PopupMenuShowingEventArgs e)
        {
            if (e.HitInfo.InRow)
            {
                ExplorerPopupMenu.ShowPopup(Control.MousePosition);
            }
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
            switch (focused_tree_node.FunId)
            {
                case 6:
                    new frmMatGroupEdit(focused_tree_node.GrpId).ShowDialog();
                    break;

                case 38:
                    new frmSvcGroupEdit(focused_tree_node.GrpId).ShowDialog();

                    break;

                case 82:
                    new frmTaraGroupEdit(focused_tree_node.GrpId).ShowDialog();
                    break;

                case 66:
                    new frmDiscCardGroupEdit(focused_tree_node.GrpId).ShowDialog();
                    break;
            }

            ExplorerRefreshBtn.PerformClick();

            //       DirTreeList.FocusedNode = DirTreeList.FindNodeByFieldValue("Id", prev_grp_id);
        }

        private void ExplorerRefreshBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DirTreeBS.DataSource = DB.SkladBase().GetDirTree(DBHelper.CurrentUser.UserId).ToList();
            DirTreeList.ExpandToLevel(1);
        }

        private void AddGroupMatBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int? grp_id;

            if (focused_tree_node.ImageIndex == 2 && focused_tree_node.GType == 2)
            {
                using (var frm = new frmMatGroupEdit(PId: focused_tree_node.GrpId))
                {
                    frm.ShowDialog();
                    grp_id = frm._grp_id;
                }
            }
            else
            {
                using (var frm = new frmMatGroupEdit())
                {
                    frm.ShowDialog();
                    grp_id = frm._grp_id;
                }
            }

            //  DirTreeBS.Add(DB.SkladBase().GetDirTree(DBHelper.CurrentUser.UserId).FirstOrDefault(w => w.Id == grp_id * -1));

            ExplorerRefreshBtn.PerformClick();

            if (grp_id != null)
            {
                DirTreeList.FocusedNode = DirTreeList.FindNodeByFieldValue("Id", grp_id * -1);
            }
        }

        private void DelExplorerBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int? prev_id = focused_tree_node.PId;
            switch (focused_tree_node.GType)
            {
                case 2:
                    DB.SkladBase().DeleteWhere<MatGroup>(w => w.GrpId == focused_tree_node.GrpId);
                    break;

                case 3:
                    DB.SkladBase().DeleteWhere<SvcGroup>(w => w.GrpId == focused_tree_node.GrpId).SaveChanges();
                    break;

                case 5:
                    DB.SkladBase().DeleteWhere<TaraGroup>(w => w.GrpId == focused_tree_node.GrpId);
                    break;
            }

            ExplorerRefreshBtn.PerformClick();

            DirTreeList.FocusedNode = DirTreeList.FindNodeByFieldValue("Id", prev_id);
            DirTreeList.FocusedNode.Expanded = true;
        }

        private void MatRecipeGridView_DoubleClick(object sender, EventArgs e)
        {
            EditItemBtn.PerformClick();
        }

        private void KagentBalansBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
           
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
        }

        private void WarehouseGridView_DoubleClick(object sender, EventArgs e)
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

        private void barButtonItem11_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ucMaterials.GetData(showChildNodeBtn.Down, restore: false);
            RefrechItemBtn.PerformClick();
        }

        private void KaGridView_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            if (e.Column.FieldName == "Saldo")
            {
                if ( e.CellValue is NotLoadedObject || e.CellValue == null || e.CellValue == DBNull.Value  )
                {
                    return;
                }

                var saldo =  Convert.ToInt32(e.CellValue);

                if (saldo < 0)
                {
                    e.Appearance.ForeColor = Color.Red;
                }
                else
                {
                    e.Appearance.ForeColor = Color.Blue;
                }
            }
        }


        private void CopyItemBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            switch (focused_tree_node.GType)
            {
                case 1:
                    break;

                case 2:

                    break;

                case 3:
                    break;

                case 4:
                    switch (focused_tree_node.FunId)
                    {
                        case 96:
                            using (var _db = DB.SkladBase())
                            {
                                var source_template = _db.WaybillTemplate.Find(waybillTemplateUserControl1.wbt_row.Id);

                                var new_template = _db.WaybillTemplate.Add(new WaybillTemplate
                                {
                                    Id = Guid.NewGuid(),
                                    Name = "Copy_" + source_template.Name,
                                    Num = _db.WaybillTemplate.Count() + 1,
                                    Notes = source_template.Notes,
                                    WaybillTemplateDet = _db.WaybillTemplateDet.Where(w => w.WaybillTemplateId == waybillTemplateUserControl1.wbt_row.Id).ToList()
                                    .Select(s => new WaybillTemplateDet
                                    {
                                        Id = Guid.NewGuid(),
                                        MatId = s.MatId,
                                        Num = s.Num
                                    }).ToList()
                                });
                                _db.SaveChanges();

                                new frmWaybillTemplate(new_template.Id).ShowDialog();
                            }
                            break;
                    }
                    break;
            }

            RefrechItemBtn.PerformClick();
        }

        private void barButtonItem11_ItemClick_1(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            switch (focused_tree_node.GType)
            {
                case 1:
                    break;

                case 2:
                    break;

                case 5:
                    IHelper.ExportToXlsx(TaraGridControl);
                    break;
            }
        }

        private void barButtonItem7_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var row = MatRecipeGridView.GetFocusedRow() as v_MatRecipe;
            using (var db = DB.SkladBase())
            {
                var r = db.MatRecipe.Find(row.RecId);

                if (r.Archived)
                {
                    r.Archived = false;
                }
                else
                {
                    if (MessageBox.Show(string.Format("Ви дійсно хочете перемістити рецепт <{0}> в архів?", row.MatName), "Information", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                    {
                        r.Archived = true;
                    }
                }

                db.SaveChanges();
            }

            RefrechItemBtn.PerformClick();
        }

        private void MatRecipeGridView_PopupMenuShowing(object sender, DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs e)
        {
            if (e.HitInfo.InRow)
            {
                RecipePopupMenu.ShowPopup(Control.MousePosition);
            }
        }

        private void barCheckItem2_CheckedChanged(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            _show_rec_archived = ShowRecipeArchiveRecordsbarCheckItem.Checked;
            RecipeArchivedGridColumn.Visible = ShowRecipeArchiveRecordsbarCheckItem.Checked;

            RefrechItemBtn.PerformClick();
        }

        private void barButtonItem6_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (focused_tree_node != null)
            {
                if (DirTreeList.FocusedNode.Expanded)
                {
                    DirTreeList.FocusedNode.Expanded = false;
                }
                else
                {
                    DirTreeList.FocusedNode.Expanded = true;
                }
            }
        }

        private void RenameMatGroupBarButtonItem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DirTreeList.OptionsBehavior.Editable = true;
            DirTreeList.ShowEditor();

        }

        private void DirTreeList_HiddenEditor(object sender, EventArgs e)
        {
            DirTreeList.OptionsBehavior.Editable = false;
            using (var db = DB.SkladBase())
            {
                if (focused_tree_node.FunId == 6)
                {
                    var g = db.MatGroup.Find(focused_tree_node.GrpId);

                    if (g != null)
                    {
                        g.Name = DirTreeList.FocusedNode.GetDisplayText("Name");
                    }
                }

                if (focused_tree_node.FunId == 38)
                {

                    var g = db.SvcGroup.Find(focused_tree_node.GrpId);

                    if (g != null)
                    {
                        g.Name = DirTreeList.FocusedNode.GetDisplayText("Name");
                    }
                }

                if (focused_tree_node.FunId == 82)
                {

                    var g = db.TaraGroup.Find(focused_tree_node.GrpId);

                    if (g != null)
                    {
                        g.Name = DirTreeList.FocusedNode.GetDisplayText("Name");
                    }
                }

                if (focused_tree_node.FunId == 66)
                {

                    var g = db.DiscCardGrp.Find(focused_tree_node.GrpId);

                    if (g != null)
                    {
                        g.Name = DirTreeList.FocusedNode.GetDisplayText("Name");
                    }
                }

                db.SaveChanges();
            }

        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            MoveMatgroup(false);
        }

        private void btnMoveDown_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            MoveMatgroup(true);
        }

        private void MoveMatgroup(bool isDown)
        {
            DirTreeList.SetNodeIndex(DirTreeList.FocusedNode, DirTreeList.GetNodeIndex(DirTreeList.FocusedNode) + (isDown ? 1 : -1));

            var nodes = DirTreeList.GetNodeList().Where(w => (DirTreeList.GetDataRecordByNode(w) as GetDirTree_Result).GType == focused_tree_node.GType);

            using (var db = DB.SkladBase())
            {
                int idx = 0;
                foreach (TreeListNode item in /*DirTreeList.FocusedNode.ParentNode.Nodes*/nodes)
                {

                    var node = DirTreeList.GetDataRecordByNode(item) as GetDirTree_Result;
                    if (focused_tree_node.GType == 2)
                    {
                        var mg = db.MatGroup.Find(node.GrpId);
                        if (mg != null)
                        {
                            mg.Num = ++idx;
                        }
                    }

                    if (focused_tree_node.GType == 3)
                    {
                        var mgs = db.SvcGroup.Find(node.GrpId);
                        if (mgs != null)
                        {
                            mgs.Num = ++idx;
                        }
                    }

                    if (focused_tree_node.GType == 5)
                    {
                        var mgt = db.TaraGroup.Find(node.GrpId);
                        if (mgt != null)
                        {
                            mgt.Num = ++idx;
                        }
                    }

                }

                db.SaveChanges();
            }
        }

        private void PreparationMatRecipeGridView_DoubleClick(object sender, EventArgs e)
        {
            EditItemBtn.PerformClick();
        }

        private void barButtonItem1_ItemClick_1(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (focused_tree_node.ImageIndex == 2 && focused_tree_node.GType == 5)
            {
                new frmTaraGroupEdit(null, focused_tree_node.GrpId).ShowDialog();
            }
            else new frmTaraGroupEdit().ShowDialog();

            ExplorerRefreshBtn.PerformClick();
        }

        private void TaraGridView_DoubleClick(object sender, EventArgs e)
        {
            EditItemBtn.PerformClick();
        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (focused_tree_node.FunId == 66 && focused_tree_node.Id < 0)
            {
                new frmDiscCardGroupEdit(null, focused_tree_node.GrpId).ShowDialog();
            }
            else new frmDiscCardGroupEdit().ShowDialog();

            ExplorerRefreshBtn.PerformClick();
        }

        private void KagentListSource_GetQueryable(object sender, DevExpress.Data.Linq.GetQueryableEventArgs e)
        {
            if (focused_tree_node == null)
            {
                return;
            }

            var _db = DB.SkladBase();

            var ka = (from k in _db.KagentList
                      join ek in _db.EnterpriseWorker on k.KaId equals ek.WorkerId into gj_ek
                      from subfg in gj_ek.DefaultIfEmpty()
                      join ew in _db.EnterpriseWorker on subfg.EnterpriseId equals ew.EnterpriseId into gj_ew
                      from subfg2 in gj_ew.DefaultIfEmpty()
                      where (subfg == null || subfg2.WorkerId == DBHelper.CurrentUser.KaId) 
                      select new
                      {
                          k.KaId,
                          k.KType,
                          k.Archived,
                          k.Name,
                          k.GroupName,
                          k.PriceName,
                          k.KAgentKind,
                          k.OKPO,
                          k.INN,
                          k.JobName,
                          k.Login,
                          k.WebUserName,
                          k.Saldo,
                          k.PTypeId,
                          k.RouteName
                      }).Distinct();

            if (focused_tree_node.Id != 10)
            {
                ka = ka.Where(w => w.KType == focused_tree_node.GrpId);
            }

            if (_ka_archived == 0)
            {
                ka = ka.Where(w => w.Archived == 0 || w.Archived == null);
            }

            e.QueryableSource = ka;

            e.Tag = _db;
        }

        private void PrintRecipeBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (MatRecipeGridView.FocusedRowHandle >= 0)
            {
                dynamic r_item = MatRecipeGridView.GetFocusedRow();

                SP_Sklad.Reports.PrintDoc.RecipeReport(r_item.RecId, DB.SkladBase(), "Recipe.xlsx");
            }
        }

        private void barButtonItem7_ItemClick_1(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        { 
       
        }

        private void barButtonItem9_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var row = PriceTypesGridView.GetFocusedRow() as PriceTypesView;
            if (row != null)
            {
                using (var frm = new frmSettingMaterialPrices(PTypeId: row.PTypeId))
                {
                    frm.ShowDialog();
                }
            }
        }

        private void PriceTypesGridView_PopupMenuShowing(object sender, DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs e)
        {
            if (e.HitInfo.InRow)
            {
                PriceTypesPopupMenu.ShowPopup(Control.MousePosition);
            }
        }

        private void PriceTypesPopupMenu_BeforePopup(object sender, CancelEventArgs e)
        {
            SetPriceBtnItem.Enabled = IHelper.GetUserAccess(97)?.CanInsert == 1;
        }

        private void barButtonItem9_ItemClick_1(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }
 
        private void ServicesGridView_DoubleClick(object sender, EventArgs e)
        {
            EditItemBtn.PerformClick();
        }

        private void waybillTemplateUserControl1_GridViewDoubleClick(object sender, EventArgs e)
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

        private void ucMaterials_MatGridViewDoubleClick(object sender, EventArgs e)
        {
            if (isDirectList && !ucMaterials.MatListTabPage.PageVisible)
            {
                var frm = this.Parent as frmCatalog;
                frm.OkButton.PerformClick();
            }
        }
    }
}
