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

        private v_Materials focused_mat
        {
            get { return MatGridView.GetFocusedRow() as v_Materials; }
        }

        private dynamic focused_kagent
        {
            get { return KaGridView.GetFocusedRow() as dynamic; }
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
            _show_rec_archived = false;

    //        KaGridControl.DataSource = null;
        }

        private void DirectoriesUserControl_Load(object sender, EventArgs e)
        {
            mainContentTab.ShowTabHeader = DevExpress.Utils.DefaultBoolean.False;
            extDirTabControl.ShowTabHeader = DevExpress.Utils.DefaultBoolean.False;

            if (!DesignMode)
            {
                custom_mat_list = new List<CustomMatList>();
                MatListGridControl.DataSource = custom_mat_list;

                repositoryItemLookUpEdit1.DataSource = DBHelper.WhList;

                DirTreeBS.DataSource = DB.SkladBase().GetDirTree(DBHelper.CurrentUser.UserId).ToList();
                DirTreeList.ExpandToLevel(1);

                KagentSaldoGridColumn.Visible = (DBHelper.CurrentUser.ShowBalance == 1);
                KagentSaldoGridColumn.OptionsColumn.ShowInCustomizationForm = KagentSaldoGridColumn.Visible;
            }
        }

        private void DirTreeList_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
        {
            focused_tree_node = DirTreeList.GetDataRecordByNode(e.Node) as GetDirTree_Result;

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

        int row = 0;
        bool restore = false;

        private void RefrechItemBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var _db = DB.SkladBase();
            int top_row;

            switch (focused_tree_node.GType)
            {
                case 1:

                    LoginGridColumn.Visible = focused_tree_node.GrpId == 2;

                    /*   var ent = DBHelper.EnterpriseList.ToList().Select(s => (int?)s.KaId);

                       var ka = (from k in _db.KagentList
                                 join ew in _db.EnterpriseWorker on k.KaId equals ew.WorkerId into gj
                                 from subfg in gj.DefaultIfEmpty()
                                 where (subfg.EnterpriseId == null || ent.Contains(subfg.EnterpriseId)) && k.Deleted == 0
                                 select k
                                 );

                       if (focused_tree_node.Id != 10)
                       {
                           ka = ka.Where(w => w.KType == focused_tree_node.GrpId);
                       }

                       if (_ka_archived == 0)
                       {
                           ka = ka.Where(w => w.Archived == 0 || w.Archived == null);
                       }

                       KAgentDS.DataSource = ka.Distinct().ToList();*/

                    row = KaGridView.FocusedRowHandle;
                    restore = true;
               //     KagentListSource.Refresh();

                    KaGridControl.DataSource = null;
                    KaGridControl.DataSource = KagentListSource;
              

                    DBHelper.ReloadKagents();
                    break;

                case 2:
                    /*  top_row = MatGridView.TopRowIndex;

                      MatListDS.DataSource = _db.GetMatList(focused_tree_node.Id == 6 ? -1 : focused_tree_node.GrpId, 0, _mat_archived, showChildNodeBtn.Down ? 1 : 0);
                      MatGridView.TopRowIndex = top_row;*/

                    row = MatGridView.FocusedRowHandle;
                    restore = true;

                    MatGridControl.DataSource = null;
                    MatGridControl.DataSource = MatListSource;

                    break;

                case 3:
                    if (focused_tree_node.Id == 51) ServicesBS.DataSource = DB.SkladBase().v_Services.ToList();
                    else ServicesBS.DataSource = DB.SkladBase().v_Services.Where(w => w.GrpId == focused_tree_node.GrpId).ToList();
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
                                Summary = (s.PPTypeId == null || s.ExtraType == 2 || s.ExtraType == 3)
                                                ? ((s.PPTypeId == null) ? s.OnValue.ToString("0.00") + "% на ціну прихода" : (s.ExtraType == 2) ? s.OnValue.ToString("0.00") + "% на категорію " + s.PtName : (s.ExtraType == 3) ? s.OnValue.ToString("0.00") + "% на прайс-лист " + s.PtName : "")
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
                            var recipe_list = _db.v_MatRecipe.Where(w => w.RType == 1);
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
                            var recipe_d_list = _db.v_MatRecipe.Where(w => w.RType == 2);

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
                            RoutesBS.DataSource = _db.Routes.ToList();
                            extDirTabControl.SelectedTabPageIndex = 10;
                            break;

                        case 66:
                            var disc_cards = _db.v_DiscCards.AsQueryable();

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
                            PreparationMatRecipeGridControl.DataSource = _db.v_MatRecipe.Where(w => w.RType == 3).ToList();
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

                    }
                    break;

                case 5:
                    top_row = TaraGridView.TopRowIndex;

                    TaraListDS.DataSource = _db.GetTaraList(focused_tree_node.Id == 125 ? -1 : focused_tree_node.GrpId, showChildNodeBtn.Down ? 1 : 0);
                    MatGridView.TopRowIndex = top_row;
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

        private void EditItemBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DialogResult result = DialogResult.Cancel;
            switch (focused_tree_node.GType)
            {
                case 1:
                    if (focused_kagent != null)
                    {
                        result = new frmKAgentEdit(null, focused_kagent.KaId).ShowDialog();
                    }
                    break;

                case 2:
                    if (focused_mat != null)
                    {
                        result = new frmMaterialEdit(focused_mat.MatId).ShowDialog();
                    }
                    break;

                case 3:
                    var svc_row = ServicesGridView.GetFocusedRow() as v_Services;
                    if (svc_row != null)
                    {
                        result = new frmServicesEdit(svc_row.SvcId).ShowDialog();
                    }
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
                        var mat_edit = new frmMaterialEdit(null, focused_tree_node.Id < 0 ? focused_tree_node.Id * -1 : DB.SkladBase().MatGroup.First().GrpId);
                        mat_edit.ShowDialog();
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
                        int KaId = focused_kagent.KaId;
                        decimal Saldo = focused_kagent.Saldo;

                        if (Saldo != 0)
                        {
                            MessageBox.Show("Не можливо видяляти, є залишок по контрагенту");
                            break;
                        }

                        var item = db.Kagent.Find(KaId);
                        if (item != null)
                        {
                            item.Deleted = 1;
                        }

                        break;

                    case 2:
                        if (focused_mat != null)
                        {
                            var mat = db.Materials.Find(focused_mat.MatId);
                            var mat_remain = db.v_MatRemains.Where(w => w.MatId == focused_mat.MatId).OrderByDescending(o => o.OnDate).FirstOrDefault();
                            var mat_recipe = db.MatRecipe.Where(w => w.MatId == focused_mat.MatId).Any();

                            if (mat != null && mat_remain == null && !mat_recipe)
                            {
                                mat.Deleted = 1;
                            }
                            else
                            {
                                MessageBox.Show("Видаляти заборонено !");
                            }
                        }
                        else
                        {
                            MessageBox.Show("Список товарів пустий!");
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
            IHelper.ShowKABalans(focused_kagent.KaId);
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
            IHelper.ShowOrdered(focused_kagent.KaId, 0, 0);
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

        private void AddItem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            /*  custom_mat_list.Add(new CustomMatList
              {
                  Num = custom_mat_list.Count() + 1,
                  MatId = focused_mat.MatId,
                  Name = focused_mat.Name,
                  Amount = 1,
                  Price = GetPrice(focused_mat.MatId, wb),
                  WId = focused_mat.WId != null ? focused_mat.WId.Value : DBHelper.WhList.FirstOrDefault(w => w.Def == 1).WId,
                  BarCode = BarCodeEdit.Text
              });

              MatListGridView.RefreshData();*/

            AddMatItemToList(focused_mat);
        }

        private void AddMatItemToList(v_Materials row )
        {
            if(row == null)
            {
                MessageBox.Show("Товар не знайдено!");

                return;
            }

            custom_mat_list.Add(new CustomMatList
            {
                Num = custom_mat_list.Count() + 1,
                MatId = row.MatId,
                Name = row.Name,
                Amount = 1,
                Price = GetPrice(row.MatId, wb),
                WId = row.WId != null ? row.WId.Value : DBHelper.WhList.FirstOrDefault(w => w.Def == 1).WId,
                BarCode = BarCodeEdit.Text
            });

            MatListGridView.RefreshData();
        }

        private decimal GetPrice(int mat_id, WaybillList wb)
        {
            decimal Price = 0;
            if (wb.WType == 1)
            {
                var get_last_price_result = DB.SkladBase().GetLastPrice(mat_id, wb.KaId, 1, wb.OnDate).FirstOrDefault();
                if (get_last_price_result != null)
                {
                    Price = get_last_price_result.Price ?? 0;
                }
            }
            else if (wb.WType == -1 || wb.WType == -16 || wb.WType == 2)
            {
                var p_type = (wb.Kontragent != null ? (wb.Kontragent.PTypeId ?? DB.SkladBase().PriceTypes.First(w => w.Def == 1).PTypeId) : DB.SkladBase().PriceTypes.First(w => w.Def == 1).PTypeId);
                var mat_price = DB.SkladBase().GetListMatPrices(mat_id, wb.CurrId, p_type).FirstOrDefault();
                Price = mat_price.Price ?? 0;
            }

            return Price;
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
            IHelper.ShowTurnMaterial(focused_mat.MatId);
        }

        private void barButtonItem5_ItemClick_1(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            IHelper.ShowMatRSV(focused_mat.MatId, DB.SkladBase());
        }

        private void barButtonItem12_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (!IHelper.FindMatInWH(focused_mat.MatId))
            {
                MessageBox.Show(string.Format("На даний час товар <{0}> на складі вдсутній!", focused_mat.Name));
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
                        var mat_remain = db.v_MatRemains.Where(w => w.MatId == focused_mat.MatId).OrderByDescending(o => o.OnDate).FirstOrDefault();
                        if (mat_remain == null || mat_remain.Remain == null || mat_remain.Remain == 0)
                        {
                            foreach(var item in db.MatRecipe.Where(w => w.MatId == focused_mat.MatId))
                            {
                                item.Archived = true;
                            }

                            mat.Archived = 1;
                        }
                        else
                        {
                            MessageBox.Show(string.Format("Неможливо перемістити матеріал <{0}> в архів, \nтому що його залишок складає {1} {2}", focused_mat.Name, mat_remain.Remain.Value.ToString(CultureInfo.InvariantCulture), focused_mat.ShortName));
                        }
                    }
                }
            }
            db.SaveChanges();

            RefrechItemBtn.PerformClick();
        }

        private void barCheckItem1_CheckedChanged(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            _mat_archived = showMatArhivedBtn.Checked ? 1 : 0;
            ArchivedGridColumn.Visible = showMatArhivedBtn.Checked;

            RefrechItemBtn.PerformClick();
        }

        private void barCheckItem1_CheckedChanged_1(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            _ka_archived = ShowKagentArchiveRecordBarCheckItem.Checked ? 1 : 0;
            KagentGridColumnArchived.Visible = ShowKagentArchiveRecordBarCheckItem.Checked;
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

        private void MatGridView_FocusedRowObjectChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventArgs e)
        {
            xtraTabControl1_SelectedPageChanged(sender, null);
        }

        private void xtraTabControl1_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            if (focused_mat == null)
            {
                return;
            }

            switch (xtraTabControl1.SelectedTabPageIndex)
            {
                case 0:
                case 1:
                    MatListInfoBS.DataSource = focused_mat;
                    break;

                case 2:
                    MatPriceGridControl.DataSource = DB.SkladBase().GetMatPriceTypes(focused_mat.MatId).ToList().Select(s => new
                    {
                        s.PTypeId,
                        s.Name,
                        s.TypeName,
                        s.IsIndividually,
                        Summary = s.Dis == 1 ? "" : s.ExtraType == 1
                                                    ? s.OnValue.Value.ToString("0.00") + " (Фіксована ціна)" : (s.PPTypeId == null || s.ExtraType == 2 || s.ExtraType == 3)
                                                    ? ((s.PPTypeId == null) ? s.OnValue.Value.ToString("0.00") + "% на ціну прихода" : (s.ExtraType == 2)
                                                    ? s.OnValue.Value.ToString("0.00") + "% на категорію " + s.PtName : (s.ExtraType == 3)
                                                    ? s.OnValue.Value.ToString("0.00") + "% на прайс-лист " + s.PtName : "")
                                                    : s.OnValue.Value.ToString("0.00") + "% від " + s.PtName,
                        s.Dis
                    });
                    break;

                case 3:
                    MatChangeGridControl.DataSource = DB.SkladBase().GetMatChange(focused_mat.MatId).ToList();
                    break;

                case 4:
                    MatNotesEdit.Text = focused_mat.Notes;
                    break;
            }
        }

        private void CopyItemBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            switch (focused_tree_node.GType)
            {
                case 1:
                    break;

                case 2:
                    using (var frm = new frmMaterialEdit(null, null, focused_mat.MatId))
                    {
                        frm.ShowDialog();
                    }

                    break;

                case 3:
                    break;

                    /*  case 4: switch (focused_tree_node.Id)
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

                              case 115:
                                  new frmRouteEdit().ShowDialog();
                                  break;

                          }
                          break;*/
            }

            RefrechItemBtn.PerformClick();
        }

        private void BarCodeEdit_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13 && !String.IsNullOrEmpty(BarCodeEdit.Text))
            {
                /*   var BarCodeSplit = BarCodeEdit.Text.Split('+');
                   String kod = BarCodeSplit[0];

                   var row = MatListDS.List.OfType<GetMatList_Result>().ToList().Find(f => f.BarCode == kod);
                   var pos = MatListDS.IndexOf(row);
                   MatListDS.Position = pos;*/

                if (/*FindByBarCode() != null &&*/ xtraTabPage14.PageVisible)
                {
                    //   AddItem.PerformClick();

                    var BarCodeSplit = BarCodeEdit.Text.Split('+');
                    String kod = BarCodeSplit[0];

                    AddMatItemToList(DB.SkladBase().v_Materials.FirstOrDefault(w => w.BarCode == kod));
                }
                else
                {
                    FindByBarCode();
                }

                BarCodeEdit.Text = "";
            }
        }

        private v_Materials FindByBarCode()
        {
            if (!String.IsNullOrEmpty(BarCodeEdit.Text))
            {
                var BarCodeSplit = BarCodeEdit.Text.Split('+');
                String kod = BarCodeSplit[0];

                MatGridView.ClearFindFilter();

                gridColumn111.FilterInfo = new DevExpress.XtraGrid.Columns.ColumnFilterInfo($"BarCode='{kod}'");


       /*         int rowHandle = MatGridView.LocateByValue("BarCode", kod);
                if (rowHandle != DevExpress.XtraGrid.GridControl.InvalidRowHandle)
                {
                    MatGridView.FocusedRowHandle = rowHandle;
                }


                var row = MatListDS.List.OfType<GetMatList_Result>().ToList().Find(f => f.BarCode == kod);
                var pos = MatListDS.IndexOf(row);
                MatListDS.Position = pos;*/

                return focused_mat;
            }

            return null;
        }

        private void barButtonItem11_ItemClick_1(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            switch (focused_tree_node.GType)
            {
                case 1:
                    IHelper.ExportToXlsx(KaGridControl);
                    break;

                case 2:
                    IHelper.ExportToXlsx(MatGridControl);
                    break;

                case 5:
                    IHelper.ExportToXlsx(TaraGridControl);
                    break;
            }
        }

        private void GetKontragentDetail()
        {
            var f_row = KaGridView.GetFocusedRow() as KagentList;

            if(f_row == null)
            {
                return;
            }

            KAgentInfoBS.DataSource = f_row;
            memoEdit1.Text = f_row.Notes;

            using (var db = DB.SkladBase())
            {
                gridControl3.DataSource = db.KAgentPersons.Where(w => w.KAId == f_row.KaId).Join(db.Jobs,
                                    person => person.JobType,
                                    job => job.Id,
                                    (person, job) => new { person.Name, person.Notes, person.Phone, person.Email, Post = person.JobType == 0 ? person.Post : job.Name }).ToList();

                gridControl1.DataSource = db.KAgentAccount.Where(w => w.KAId == f_row.KaId).Select(s => new { s.AccNum, s.Banks.MFO, BankName = s.Banks.Name, TypeName = s.AccountType.Name }).ToList();

                gridControl4.DataSource = db.EnterpriseWorker.Where(w => w.EnterpriseId == f_row.KaId).Join(db.Kagent, p => p.WorkerId, k => k.KaId, (p, k) => new { k.KaId, k.Name }).ToList();
            }
        }

        private void KaGridView_FocusedRowObjectChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventArgs e)
        {
            GetKontragentDetail();
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
            RecipePopupMenu.ShowPopup(Control.MousePosition);
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
            using (var db = DB.SkladBase())
            {
                DirTreeList.SetNodeIndex(DirTreeList.FocusedNode, DirTreeList.GetNodeIndex(DirTreeList.FocusedNode) + (isDown ? 1 : -1));

                int idx = 0;
                foreach (TreeListNode item in DirTreeList.FocusedNode.ParentNode.Nodes)
                {
                    var node = DirTreeList.GetDataRecordByNode(item) as GetDirTree_Result;
                    if (focused_tree_node.GType == 2)
                    {
                        var mg = db.MatGroup.Find(node.GrpId);
                        mg.Num = ++idx;
                    }

                    if (focused_tree_node.GType == 3)
                    {
                        var mgs = db.SvcGroup.Find(node.GrpId);
                        mgs.Num = ++idx;
                    }

                    if (focused_tree_node.GType == 5)
                    {
                        var mgs = db.TaraGroup.Find(node.GrpId);
                        mgs.Num = ++idx;
                    }

                }

                db.SaveChanges();

            }
        }

        private void gridView5_DoubleClick(object sender, EventArgs e)
        {
            var row = gridView5.GetFocusedRow() as dynamic;
            using (var frm = new frmKAgentEdit(null, row.KaId))
            {
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    GetKontragentDetail();
                }
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
            if (focused_tree_node.FunId == 66)
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

            var ent = DBHelper.EnterpriseList.ToList().Select(s => (int?)s.KaId);

            var ka = (from k in _db.KagentList
                      join ew in _db.EnterpriseWorker on k.KaId equals ew.WorkerId into gj
                      from subfg in gj.DefaultIfEmpty()
                      where (subfg == null || ent.Contains(subfg.EnterpriseId)) && k.Deleted == 0
                      select k).Distinct();

            //   var ka = _db.KagentList.Where(w => w.Deleted == 0);

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

        private void BarCodeEdit_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if(e.Button.Index == 0)
            {
                FindByBarCode();
            }
        }

        private void KaGridView_AsyncCompleted(object sender, EventArgs e)
        {
            if (!restore)
            {
                return;
            }

            KaGridView.TopRowIndex = row;
            KaGridView.FocusedRowHandle = row;
            restore = false;
        }

        private void MatListSource_GetQueryable(object sender, DevExpress.Data.Linq.GetQueryableEventArgs e)
        {
            if (focused_tree_node == null)
            {
                return;
            }

            var _db = DB.SkladBase();

            var mat = _db.v_Materials.AsQueryable();
            if(_mat_archived == 0)
            {
                mat = mat.Where(w => w.Archived == 0);
            }

            var grp_id = focused_tree_node.Id == 6 ? -1 : focused_tree_node.GrpId;

            if ( grp_id > 0)
            {
                if (showChildNodeBtn.Down)
                {
                    var grp_list = _db.GetMatGroupTree(grp_id).Select(s => s.GrpId).ToList();

                    if (grp_list.Any())
                    {
                        mat = mat.Where(w => grp_list.Contains(w.GrpId));
                    }
                }
                else
                {
                    mat = mat.Where(w => w.GrpId == grp_id);
                }
            }

            e.QueryableSource = mat;

            e.Tag = _db;
        }

        private void MatGridView_AsyncCompleted(object sender, EventArgs e)
        {
            if (!restore)
            {
                return;
            }

            MatGridView.TopRowIndex = row;
            MatGridView.FocusedRowHandle = row;
            restore = false;
        }
    }
}
