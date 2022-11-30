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
using SP_Sklad.FinanseForm;
using SP_Sklad.Common;
using SP_Sklad.Properties;
using DevExpress.XtraEditors;

namespace SP_Sklad.MainTabs
{
    public partial class FinancesUserControl : DevExpress.XtraEditors.XtraUserControl
    {
        int cur_wtype = 0;
        public bool isDirectList { get; set; }

        private GetFinancesTree_Result focused_tree_node
        {
            get
            {
                return FinancesTreeList.GetDataRecordByNode(FinancesTreeList.FocusedNode) as GetFinancesTree_Result;
            }
        }

        public FinancesUserControl()
        {
            InitializeComponent();

            wbContentTab.ShowTabHeader = DevExpress.Utils.DefaultBoolean.False;
        }

        private void FinancesUserControl_Load(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                using (var _db = new BaseEntities())
                {

                    TurnKagentList.Properties.DataSource = DBHelper.KagentsList;
                    TurnKagentList.EditValue = 0;

                    CurrensyList.Properties.DataSource = new List<object>() { new { Id = 0, Name = "Усі" } }.Concat(_db.Currency.Select(s => new { Id = s.CurrId, Name = s.ShortName })).ToList();
                    CurrensyList.EditValue = 0;

                    wbStatusList.Properties.DataSource = new List<object>() { new { Id = -1, Name = "Усі" }, new { Id = 1, Name = "Проведені" }, new { Id = 0, Name = "Непроведені" } };
                    wbStatusList.EditValue = -1;

                    TurnStartDate.EditValue = DateTime.Now.Date.AddDays(-1);
                    TurnEndDate.EditValue = DateTime.Now;

                    wbStartDate.EditValue = DateTime.Now.Date.AddDays(-1);
                    wbEndDate.EditValue = DateTime.Now.Date;

                    dateEdit2.EditValue = DateTime.Now.AddDays(-30);
                    dateEdit1.EditValue = DateTime.Now.Date;

                    FinancesTreeList.DataSource = _db.GetFinancesTree(DBHelper.CurrentUser.UserId).ToList();
                    FinancesTreeList.ExpandAll();

                    PayDocTypeEdit.Properties.DataSource = new List<PayDocType>() { new PayDocType { Id = -1, Name = "Усі" } }.Concat(_db.PayDocType.Where(w => w.Id == 6 || w.Id == 3 || w.Id == 11)).ToList();
                    PayDocTypeEdit.EditValue = -1;

                    repositoryItemLookUpEdit1.DataSource = new List<object>() { new { Id = 1, Name = "Проведено" }, new { Id = 0, Name = "Новий" } };
                }
            }
            
        }

        private void FinancesTreeList_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
        {
            if (focused_tree_node == null)
            {
                return;
            }

            DeleteItemBtn.Enabled = false;
            ExecuteItemBtn.Enabled = false;
            EditItemBtn.Enabled = false;
            CopyItemBtn.Enabled = false;
            PrintItemBtn.Enabled = false;

            cur_wtype = focused_tree_node.WType != null ? focused_tree_node.WType.Value : 0;
            RefrechItemBtn.PerformClick();

            wbContentTab.SelectedTabPageIndex = focused_tree_node.GType.Value;

            if (focused_tree_node.FunId != null)
            {
                History.AddEntry(new HistoryEntity { FunId = focused_tree_node.FunId.Value, MainTabs = 4 });

                if (FinancesTreeList.ContainsFocus)
                {
                    Settings.Default.LastFunId = focused_tree_node.FunId.Value;
                }
            }
        }

        private void RefrechItemBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (focused_tree_node == null)
            {
                return;
            }

            switch (focused_tree_node.GType)
            {
                case 1:
                    FinTreeList.DataSource = new BaseEntities().GetSaldoDetTree(DBHelper.CurrentUser.UserId, focused_tree_node.Id, DateTime.Now).ToList();
                    FinTreeList.FocusedNode = FinTreeList.Nodes.FirstNode;
                    gridControl1.DataSource = new BaseEntities().MoneyOnDateByUser(DateTime.Now, UserSession.UserId).GroupBy(g => new { g.Currency }).Select(s => new { s.Key.Currency, Saldo = s.Sum(m => m.Saldo) }).ToList();
                    break;

                case 2:
                 //   MoneyMoveListBS.DataSource = null;
                    MoneyMoveListBS.DataSource = new BaseEntities().MoneyMoveList((int)PayDocTypeEdit.EditValue, wbStartDate.DateTime, wbEndDate.DateTime.Date.AddDays(1), (int)wbStatusList.EditValue, DBHelper.CurrentUser.KaId).ToList();
                    RefreshBtnBar();
                    break;
                case 3:
                    CurActivesBS.DataSource = new BaseEntities().GetActives(DateTime.Now.Date,DateTime.Now.Date).OrderByDescending(o=> o.OnDate).FirstOrDefault();  //v_Actives.Where(w => w.OnDate == d).ToList();
                    break;

                case 4:
                    docsUserControl1.set_tree_node = focused_tree_node.Id;
                    docsUserControl1.DocsTreeList.FocusedNode = docsUserControl1.DocsTreeList.FindNodeByFieldValue("Id", focused_tree_node.Id);
                    docsUserControl1.splitContainerControl1.PanelVisibility = SplitPanelVisibility.Panel2;
                    break;
            }
        }

        void GetMoneyTurnover()
        {
            if (FinTreeList.FocusedNode == null)
            {
                return;
            }

            if (focused_tree_node.GType == 1)
            {
                var node = FinTreeList.GetDataRecordByNode(FinTreeList.FocusedNode) as GetSaldoDetTree_Result;
                int fun_id = 0, turn_type = 0;

                switch (node.Id)
                {
                    case 62: turn_type = 0;
                        break;
                    case 61: turn_type = 1;
                        break;
                    case 63: turn_type = 2;
                        break;
                    default: fun_id = node.Num;
                        switch (node.ImageIndex)
                        {
                            case 38: turn_type = 1;
                                break;
                            case 47: turn_type = 2;
                                break;
                        }
                        break;
                }
                MoneyTurnoverBS.DataSource = new BaseEntities()
                    .MoneyTurnover(fun_id, TurnStartDate.DateTime.Date, TurnEndDate.DateTime.Date, turn_type, (int?)CurrensyList.EditValue, (int?)TurnKagentList.EditValue, DBHelper.CurrentUser.KaId, UserSession.UserId)
                    .ToList();
            }
        }

        private void FinTreeList_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
        {
            GetMoneyTurnover();
        }

        private void TurnStartDate_EditValueChanged(object sender, EventArgs e)
        {
            if (TurnStartDate.ContainsFocus || TurnEndDate.ContainsFocus || TurnKagentList.ContainsFocus || CurrensyList.ContainsFocus)
            {
                GetMoneyTurnover();
            }
        }

        private void wbStartDate_EditValueChanged(object sender, EventArgs e)
        {
            if (wbStartDate.ContainsFocus || wbEndDate.ContainsFocus || wbStatusList.ContainsFocus || PayDocTypeEdit.CanFocus)
            {
                RefrechItemBtn.PerformClick();
            }
        }

        private void NewItemBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            using (var frm = new frmMoneyCorrecting())
            {
                frm.ShowDialog();
            }

            RefrechItemBtn.PerformClick();
        }

        private void EditItemBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (focused_tree_node == null)
            {
                return;
            }

            switch (focused_tree_node.GType)
            {
                case 2:
                    var dr = MoneyMoveGridView.GetFocusedRow() as MoneyMoveList_Result;
                    DocEdit.FinDocEdit(dr);
                    break;
            }

            RefrechItemBtn.PerformClick();
        }

        private void DeleteItemBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var pd_row = MoneyMoveGridView.GetFocusedRow() as MoneyMoveList_Result;
       
            using (var db = new BaseEntities())
            {
                try
                {
                    switch (focused_tree_node.GType)
                    {
                        case 2: db.Database.SqlQuery<PayDoc>("SELECT * from PayDoc WITH (UPDLOCK) where PayDocId = {0}", pd_row.PayDocId).FirstOrDefault(); break;
                    }
                    if (MessageBox.Show(Resources.delete_wb, "Відалення документа", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                    {
                        switch (focused_tree_node.GType)
                        {
                            case 2:
                                var pd = db.PayDoc.Find(pd_row.PayDocId);
                                db.PayDoc.Remove(pd);
                                break;
                        }
                        db.SaveChanges();
                    }
                }
                catch
                {
                    MessageBox.Show(Resources.deadlock);
                }
            }

            RefrechItemBtn.PerformClick();
        }

        private void bandedGridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            var dr = MoneyMoveGridView.GetRow(e.FocusedRowHandle) as MoneyMoveList_Result;

            if (dr != null)
            {
                MoneyMoveListInfoBS.DataSource = dr;
            }
            else
            {
                MoneyMoveListInfoBS.DataSource = null;
            }

            RefreshBtnBar();
        }

        private void RefreshBtnBar()
        {
            var dr = MoneyMoveGridView.GetFocusedRow() as MoneyMoveList_Result;

            DeleteItemBtn.Enabled = (dr != null && dr.Checked == 0 && focused_tree_node.CanDelete == 1);
            ExecuteItemBtn.Enabled = (dr != null && focused_tree_node.CanPost == 1);
            EditItemBtn.Enabled = (dr != null && focused_tree_node.CanModify == 1);
            CopyItemBtn.Enabled = EditItemBtn.Enabled;
            PrintItemBtn.Enabled = (dr != null);
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            ActivesBS.DataSource = new BaseEntities().v_Actives.Where(w => w.OnDate >= dateEdit2.DateTime.Date && w.OnDate <= dateEdit1.DateTime.Date).ToList();
        }

        private void ExecuteItemBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (focused_tree_node == null)
            {
                return;
            }

            switch (focused_tree_node.GType)
            {
                case 2:
                    var pd_row = MoneyMoveGridView.GetFocusedRow() as MoneyMoveList_Result;
                    using (var db = new BaseEntities())
                    {
                        var pd = db.PayDoc.Find(pd_row.PayDocId);
                        pd.Checked = pd_row.Checked == 0 ? 1 : 0;

                        var pd_to = db.PayDoc.FirstOrDefault(w => w.OperId == pd.OperId && pd.PayDocId != w.PayDocId);
                        if (pd_to != null)
                        {
                            pd_to.Checked = pd.Checked;
                        }

                        db.SaveChanges();
                    }
                    break;
            }

            RefrechItemBtn.PerformClick();
        }

        private void MoneyMoveGridView_DoubleClick(object sender, EventArgs e)
        {
            if (IHelper.isRowDublClick(sender)) EditItemBtn.PerformClick();
        }

        private void vGridControl3_CellValueChanged(object sender, DevExpress.XtraVerticalGrid.Events.CellValueChangedEventArgs e)
        {
            var items = CurActivesBS.DataSource as List<v_Actives>;
            if (items != null)
            {
                var d = items.FirstOrDefault().OnDate;
                CurActivesBS.DataSource = new BaseEntities().v_Actives.Where(o => o.OnDate == d).ToList();
            }
           
        }

        private void CopyItemBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            switch (focused_tree_node.GType)
            {
                case 2:
                    var dr = MoneyMoveGridView.GetFocusedRow() as MoneyMoveList_Result;
                    var doc = DB.SkladBase().DocCopy(dr.Id, DBHelper.CurrentUser.KaId).FirstOrDefault();
                    if (dr.DocType == 6)
                    {
                        using (var money_corr = new frmMoneyCorrecting(doc.out_wbill_id))
                        {
                            money_corr.ShowDialog();
                        }
                    }

                    if (dr.DocType == 3)
                    {
                        using (var money_move = new frmMoneyMove(doc.out_wbill_id))
                        {
                            money_move.ShowDialog();
                        }
                    }

                    break;
            }
            RefrechItemBtn.PerformClick();
        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            using (var frm = new frmMoneyMove())
            {
                frm.ShowDialog();
            }

            RefrechItemBtn.PerformClick();
        }

        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            using (var frm = new frmMoneyIn())
            {
                frm.ShowDialog();
            }

            RefrechItemBtn.PerformClick();
        }
    }
}
