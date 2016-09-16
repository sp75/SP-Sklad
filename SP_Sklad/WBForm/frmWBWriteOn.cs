using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using SP_Sklad.Common;
using SP_Sklad.Reports;
using SP_Sklad.SkladData;
using SP_Sklad.WBDetForm;
using EntityState = System.Data.Entity.EntityState;

namespace SP_Sklad.WBForm
{
    public partial class frmWBWriteOn : Form
    {
        public  BaseEntities _db { get; set; }
        public int? _wbill_id { get; set; }
        public int? doc_id { get; set; }
        private DbContextTransaction current_transaction { get; set; }
        private WaybillList wb { get; set; }
        private GetWaybillDetIn_Result wbd_row { get; set; }
        private IQueryable<GetWaybillDetIn_Result> wbd_list { get; set; }
        private List<GetRelDocList_Result> rdl  { get; set; }
        private GetWaybillDetIn_Result focused_dr
        {
            get { return WaybillDetInGridView.GetFocusedRow() as GetWaybillDetIn_Result; }
        } 

        public frmWBWriteOn(int? wbill_id = null)
        {
            _wbill_id = wbill_id;
            _db = new BaseEntities();
            current_transaction = _db.Database.BeginTransaction(/*IsolationLevel.RepeatableRead*/);

            InitializeComponent();
        }

        private void frmWBWriteOn_Load(object sender, EventArgs e)
        {
            PersonComboBox.Properties.DataSource = DBHelper.Persons;
            var wh_list = DBHelper.WhList();
            WHComboBox.Properties.DataSource = wh_list;
            WHComboBox.EditValue = wh_list.Where(w => w.Def == 1).Select(s => s.WId).FirstOrDefault();

            if (_wbill_id == null && doc_id == null)
            {
                wb = _db.WaybillList.Add(new WaybillList()
                {
                    WType = 5,
                    OnDate = DBHelper.ServerDateTime(),
                    Num = new BaseEntities().GetCounter("wb_write_on").FirstOrDefault(),
                    CurrId = DBHelper.Currency.FirstOrDefault(w => w.Def == 1).CurrId,
                    OnValue = 1,
                    PersonId = DBHelper.CurrentUser.KaId,
                    UpdatedBy = DBHelper.CurrentUser.UserId
                });

                _db.SaveChanges();

                _wbill_id = wb.WbillId;
            }
            else
            {
                try
                {
                    UpdLockWB();
                }
                catch
                {

                    Close();
                }

            }

            if (wb != null)
            {
                TurnDocCheckBox.EditValue = wb.Checked;

                PersonComboBox.DataBindings.Add(new Binding("EditValue", wb, "PersonId", true, DataSourceUpdateMode.OnValidation));
                NumEdit.DataBindings.Add(new Binding("EditValue", wb, "Num"));
                OnDateDBEdit.DataBindings.Add(new Binding("EditValue", wb, "OnDate"));
              
                NotesEdit.DataBindings.Add(new Binding("EditValue", wb, "Notes"));
                ReasonEdit.DataBindings.Add(new Binding("EditValue", wb, "Reason"));

                rdl = _db.GetRelDocList(wb.DocId).Where(w => w.DocType == 7 || w.DocType == -22).ToList();
                AddBarSubItem.Enabled = !rdl.Any();
                EditMaterialBtn.Enabled = !rdl.Any(a => a.DocType == 7);
                DelMaterialBtn.Enabled = AddBarSubItem.Enabled;
            }

            RefreshDet();
        }

        private void UpdLockWB()
        {
            if (wb != null)
            {
                _db.Entry<WaybillList>(wb).State = EntityState.Detached;
            }

            if (_wbill_id == null && doc_id != null)
            {
                wb = _db.Database.SqlQuery<WaybillList>("SELECT * from WaybillList WITH (UPDLOCK, NOWAIT) where DocId = {0} ", doc_id).FirstOrDefault();
            }
            else
            {
                wb = _db.Database.SqlQuery<WaybillList>("SELECT * from WaybillList WITH (UPDLOCK, NOWAIT) where WbillId = {0} ", _wbill_id).FirstOrDefault();
            }

            _db.Entry<WaybillList>(wb).State = EntityState.Modified;
            _db.Entry<WaybillList>(wb).Property(f => f.SummPay).IsModified = false;

            _wbill_id = wb.WbillId;
        }

        private void RefreshDet()
        {
            wbd_list = _db.GetWaybillDetIn(_wbill_id);
            var dr_tmp = WaybillDetInGridView.GetFocusedRow() as GetWaybillDetIn_Result;

            WaybillDetInGridControl.DataSource = null;
            WaybillDetInGridControl.DataSource = wbd_list;

            WaybillDetInGridView.FocusedRowHandle = FindRowHandleByRowObject(WaybillDetInGridView, dr_tmp);

            frmValidating();
        }
        private int FindRowHandleByRowObject(GridView view, GetWaybillDetIn_Result dr)
        {
            if (dr != null)
            {
                for (int i = 0; i < view.DataRowCount; i++)
                {
                    if (dr.PosId == (view.GetRow(i) as GetWaybillDetIn_Result).PosId)
                    {
                        return i;
                    }
                }
            }
            return GridControl.InvalidRowHandle;
        }

        bool frmValidating()
        {
            bool recult = (!String.IsNullOrEmpty(NumEdit.Text) && OnDateDBEdit.EditValue != null && WaybillDetInGridView.DataRowCount > 0);


            EditMaterialBtn.Enabled = (WaybillDetInGridView.DataRowCount > 0 && !rdl.Any(a => a.DocType == 7));
            DelMaterialBtn.Enabled = (WaybillDetInGridView.DataRowCount > 0 && !rdl.Any());
            MatInfoBtn.Enabled = WaybillDetInGridView.DataRowCount > 0;
            RsvInfoBtn.Enabled = MatInfoBtn.Enabled;
            PrevievBtn.Enabled = MatInfoBtn.Enabled;

            OkButton.Enabled = recult;
            return recult;
        }

        private void frmWBWriteOn_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (current_transaction.UnderlyingTransaction.Connection != null)
            {
                current_transaction.Rollback();
            }

            _db.Dispose();
            current_transaction.Dispose();
        }

        private void frmWBWriteOn_Shown(object sender, EventArgs e)
        {
            OnDateDBEdit.Enabled = (DBHelper.CurrentUser.EnableEditDate == 1);
            NowDateBtn.Enabled = OnDateDBEdit.Enabled;
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            wb.UpdatedAt = DateTime.Now;

            if (!CheckDate())
            {
                return;
            }

            _db.SaveChanges();

            current_transaction.Commit();

            if (TurnDocCheckBox.Checked)
            {
                _db.ExecuteWayBill(wb.WbillId, null);
            }

            Close();
        }

        private bool CheckDate()
        {
            /*  select first 1 ma.ondate, wbd.matname
from SP_WBD_GET_IN (:WBILLID) wbd , SP_GET_MAKE_AMOUNT(wbd.wbmaked) ma
order by  ma.ondate desc */
            var q = _db.GetWaybillDetIn(_wbill_id).Where(w => w.WbMaked != null).ToList().Select(s => new
            {
                Make = _db.GetMakeAmount(s.WbMaked).FirstOrDefault(),
                s.MatName
            }).OrderByDescending(o => o.Make.OnDate).FirstOrDefault();

            if (q != null && q.Make != null && OnDateDBEdit.DateTime < q.Make.OnDate)
            {
                String msg = "Дата документа не може бути меншою за дату кінця виготовлення продукції! \nПозиція: " + q.MatName + " \nДата: " + q.Make.OnDate + " \nЗмінити дату докомента на " + q.Make.OnDate + "?";
                if (MessageBox.Show(msg, "Інформація", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    wb.OnDate = q.Make.OnDate.Value;
                    return true;
                }
                else return false;
            }

            return true;
        }

        private void AddMaterialBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var df = new frmWayBillDetIn(_db, null, wb);
            if (df.ShowDialog() == DialogResult.OK)
            {
                RefreshDet();
            }
        }

        private void EditMaterialBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (wbd_row != null)
            {
                var df = new frmWayBillDetIn(_db, wbd_row.PosId, wb);
                if (df.ShowDialog() == DialogResult.OK)
                {
                    RefreshDet();
                }
            }
        }

        private void DelMaterialBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (wbd_row != null)
            {
                _db.DeleteWhere<WaybillDet>(w => w.PosId == wbd_row.PosId);
                _db.SaveChanges();

                RefreshDet();
            }
        }

        private void WaybillDetInGridView_DoubleClick(object sender, EventArgs e)
        {
            GridView view = (GridView)sender;
            Point pt = view.GridControl.PointToClient(Control.MousePosition);
            GridHitInfo info = view.CalcHitInfo(pt);

            if (info.InRow || info.InRowCell)
            {
                EditMaterialBtn.PerformClick();
            }
        }

        private void NowDateBtn_Click(object sender, EventArgs e)
        {
            wb.OnDate = DBHelper.ServerDateTime();
            OnDateDBEdit.DateTime = wb.OnDate;
        }

        private void TurnDocCheckBox_EditValueChanged(object sender, EventArgs e)
        {
            frmValidating();
        }

        private void WHComboBox_EditValueChanged(object sender, EventArgs e)
        {
            if (WaybillDetInGridView.DataRowCount > 0 && WHComboBox.Focused)
            {
                if (MessageBox.Show("Оприходувати весь товар на склад <" + WHComboBox.Text + ">?", "Інформація", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    foreach (var item in _db.WaybillDet.Where(w => w.WbillId == _wbill_id))
                    {
                        item.WId = Convert.ToInt32(WHComboBox.EditValue);

                        foreach (var turn in _db.WMatTurn.Where(w => w.SourceId == item.PosId))
                        {
                            turn.WId = Convert.ToInt32(WHComboBox.EditValue);
                        }
                    }
                    _db.SaveChanges();
                    RefreshDet();
                }
            }
        }

        private void WaybillDetInGridView_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            wbd_row = WaybillDetInGridView.GetRow(WaybillDetInGridView.FocusedRowHandle) as GetWaybillDetIn_Result;
        }

        private void WaybillDetInGridView_PopupMenuShowing(object sender, PopupMenuShowingEventArgs e)
        {
            if (e.HitInfo.InRow)
            {
                this.WbDetPopupMenu.ShowPopup(Control.MousePosition);
            }
        }

        private void PrevievBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            _db.SaveChanges();

            PrintDoc.Show(wb.DocId.Value, wb.WType, _db);
        }

        private void RsvInfoBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            IHelper.ShowMatRSV(focused_dr.MatId, _db);
        }

        private void MatInfoBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            IHelper.ShowMatInfo(focused_dr.MatId);
        }

    }
}
