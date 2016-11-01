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

namespace SP_Sklad.MainTabs
{
    public partial class FinancesUserControl : DevExpress.XtraEditors.XtraUserControl
    {
        int cur_wtype = 0;

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

                    TurnKagentList.Properties.DataSource = new List<object>() { new { KaId = 0, Name = "Усі" } }.Concat(_db.Kagent.Select(s => new { s.KaId, s.Name })).ToList();
                    TurnKagentList.EditValue = 0;

                    CurrensyList.Properties.DataSource = new List<object>() { new { Id = 0, Name = "Усі" } }.Concat(_db.Currency.Select(s => new { Id = s.CurrId, Name = s.ShortName })).ToList();
                    CurrensyList.EditValue = 0;

                    wbStatusList.Properties.DataSource = new List<object>() { new { Id = -1, Name = "Усі" }, new { Id = 1, Name = "Проведені" }, new { Id = 0, Name = "Непроведені" } };
                    wbStatusList.EditValue = -1;

                    TurnStartDate.EditValue = DateTime.Now.AddDays(-30);
                    TurnEndDate.EditValue = DateTime.Now;

                    wbStartDate.EditValue = DateTime.Now.AddDays(-30);
                    wbEndDate.EditValue = DateTime.Now.Date;

                    dateEdit2.EditValue = DateTime.Now.AddDays(-30);
                    dateEdit1.EditValue = DateTime.Now.Date;

                    FinancesTreeList.DataSource = _db.GetFinancesTree(DBHelper.CurrentUser.UserId).ToList();
                    FinancesTreeList.ExpandAll();
                }
            }
            
        }

        private void FinancesTreeList_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
        {
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
                History.AddEntry(new entity { FunId = focused_tree_node.FunId.Value, MainTabs = 3 });
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
                    gridControl1.DataSource = new BaseEntities().MoneyOnDate(DateTime.Now).GroupBy(g => new { g.Currency }).Select(s => new { s.Key.Currency, Saldo = s.Sum(m => m.Saldo) }).ToList();
                    break;

                case 2:
                 //   MoneyMoveListBS.DataSource = null;
                    MoneyMoveListBS.DataSource = new BaseEntities().MoneyMoveList(-1, wbStartDate.DateTime, wbEndDate.DateTime.Date.AddDays(1), (int)wbStatusList.EditValue).ToList();
                    RefreshBtnBar();
                    break;
                case 3:
                    new BaseEntities().RecalcActives(DateTime.Now.Date).ToList();
                    CurActivesBS.DataSource = new BaseEntities().Actives.OrderByDescending(o => o.OnDate).Take(1).ToList();
                    break;
            }
        }

        void GetMoneyTurnover()
        {
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
                MoneyTurnoverBS.DataSource = new BaseEntities().MoneyTurnover(fun_id, TurnStartDate.DateTime, TurnEndDate.DateTime.Date.AddDays(1), turn_type, (int?)CurrensyList.EditValue, (int?)TurnKagentList.EditValue).ToList();
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
            if (wbStartDate.ContainsFocus || wbEndDate.ContainsFocus || wbStatusList.ContainsFocus)
            {
                MoneyMoveListBS.DataSource = null;
                MoneyMoveListBS.DataSource = new BaseEntities().MoneyMoveList(-1, wbStartDate.DateTime, wbEndDate.DateTime, (int)wbStatusList.EditValue).ToList();
            }
        }

        private void NewItemBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (focused_tree_node == null)
            {
                return;
            }

            switch (focused_tree_node.GType)
            {
                case 2:
                    new frmMoneyMove(6).ShowDialog();
                    break;
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
                                db.PayDoc.Remove(db.PayDoc.Find(pd_row.PayDocId));
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
            //    gridControl2.DataSource = _db.GetWaybillDetIn(dr.WbillId);
         //       gridControl3.DataSource = _db.GetRelDocList(dr.DocId);
            }
            else
            {
            //    gridControl2.DataSource = null;
           //     gridControl3.DataSource = null;
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
            var start = dateEdit2.DateTime.Date;
            var end = dateEdit1.DateTime.AddDays(1).Date;
            ActivesBS.DataSource = new BaseEntities().Actives.Where(w => w.OnDate >= start && w.OnDate <= end).ToList();
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            var end = dateEdit1.DateTime.Date;
            using (var db = new BaseEntities())
            {
                for (var date = dateEdit2.DateTime.Date; date <= end; date = date.AddDays(1))
                {
                    db.RecalcActives(date).ToList();
                }
            }

            simpleButton1.PerformClick();
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
            var items = CurActivesBS.DataSource as List<Actives>;
            if (items != null)
            {
                var d = items.FirstOrDefault().OnDate;
                CurActivesBS.DataSource = new BaseEntities().Actives.Where(o => o.OnDate == d).ToList();
            }
           
        }

        private void CopyItemBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            switch (focused_tree_node.GType)
            {
                case 2:
                    var dr = MoneyMoveGridView.GetFocusedRow() as MoneyMoveList_Result;
                    var doc = DB.SkladBase().DocCopy(dr.DocId).FirstOrDefault();
                    using (var wb_in = new frmMoneyMove(6, doc.out_wbill_id))
                    {
                        wb_in.ShowDialog();
                    }
                    break;
            }
            RefrechItemBtn.PerformClick();
        }
    }
}
