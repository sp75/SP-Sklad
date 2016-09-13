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
using SP_Sklad.SkladData;
using SP_Sklad.WBDetForm;
using System.Data.SqlClient;
using System.Data.Linq;
using EntityState = System.Data.Entity.EntityState;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using SP_Sklad.Reports;
using SP_Sklad.Common;

namespace SP_Sklad.WBForm
{
    public partial class frmWayBillIn : Form
    {
        private int _wtype { get; set; }
        public BaseEntities _db { get; set; }
        private int? _wbill_id { get; set; }
        public int? doc_id { get; set; }
        private DbContextTransaction current_transaction { get; set; }
        private WaybillList wb { get; set; }

        public frmWayBillIn(int wtype, int? wbill_id = null)
        {
            _wtype = wtype;
            _wbill_id = wbill_id;
            _db = new BaseEntities();
            current_transaction = _db.Database.BeginTransaction(/*IsolationLevel.RepeatableRead*/);

            InitializeComponent();
        }

        private void frmWayBillIn_Load(object sender, EventArgs e)
        {
            if (_wbill_id == null && doc_id == null)
            {
                wb = _db.WaybillList.Add(new WaybillList()
                {
                    WType = _wtype,
                    OnDate = DBHelper.ServerDateTime(),
                    Num = new BaseEntities().GetCounter("wb_in").FirstOrDefault(),
                    CurrId = 2,
                    OnValue = 1,
                    PersonId = DBHelper.CurrentUser.KaId,
                    Nds = DBHelper.Enterprise.NdsPayer == 1 ? DBHelper.CommonParam.Nds : 0
                });

                _db.SaveChanges();
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
                GetDocValue(wb);
            }

            KagentComboBox.Properties.DataSource = DBHelper.Kagents;
            PersonComboBox.Properties.DataSource = DBHelper.Persons;

            var wh_list = DBHelper.WhList();
            WHComboBox.Properties.DataSource = wh_list;
            WHComboBox.EditValue = wh_list.Where(w => w.Def == 1).Select(s => s.WId).FirstOrDefault();

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

        private void GetDocValue(WaybillList wb)
        {
            _wbill_id = wb.WbillId;
            NumEdit.Text = wb.Num;
            OnDateDBEdit.DateTime = wb.OnDate;
            KagentComboBox.EditValue = wb.KaId;
            PersonComboBox.EditValue = wb.PersonId;
            TurnDocCheckBox.Checked = Convert.ToBoolean(wb.Checked);
            ReasonEdit.Text = wb.Reason;
            NotesEdit.Text = wb.Notes;
            PersonComboBox.EditValue = wb.PersonId;

            payDocUserControl1.OnLoad(_db, wb);
        }

        private void frmWayBillIn_Shown(object sender, EventArgs e)
        {
            if (_wtype == 1) this.Text = "Властивості прибуткової накладної";
            if (_wtype == 16) this.Text = "Замовлення постачальникові";

            TurnDocCheckBox.Enabled = (_wtype != 16);
            checkEdit2.Visible = (_wtype == 16);
            dateEdit2.Visible = (_wtype == 16);

            OnDateDBEdit.Enabled = (DBHelper.CurrentUser.EnableEditDate == 1);
            NowDateBtn.Enabled = OnDateDBEdit.Enabled;

            PersonComboBox.Enabled = (DBHelper.CurrentUser.EnableEditDate == 1);
            PersonEditBtn.Enabled = PersonComboBox.Enabled;
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            wb.Num = NumEdit.Text;
            wb.OnDate = OnDateDBEdit.DateTime;
            wb.KaId = (int?)KagentComboBox.EditValue;
            wb.PersonId = (int?)PersonComboBox.EditValue;
            wb.Reason = ReasonEdit.Text;
            wb.Notes = NotesEdit.Text;
            wb.UpdatedAt = DateTime.Now;

            if (!CheckDate())
            {
                return;
            }

            _db.SaveChanges();
            payDocUserControl1.Execute(wb.WbillId);
            if (TurnDocCheckBox.Checked)
            {
                var ew = _db.ExecuteWayBill(wb.WbillId, null).ToList();
            }
            current_transaction.Commit();

            Close();
        }

        private bool CheckDate()
        {
            var q = _db.WMatTurn.Where(w => w.WaybillDet.WbillId == _wbill_id).Select(s => new
            {
                s.OnDate,
                s.WaybillDet.Materials.Name
            }).Distinct().FirstOrDefault();
            /*  select first 1 distinct wmt.ondate, m.name
   from WMATTURN wmt, waybilldet wbd , materials m
   where wbd.wbillid=:WBILLID and m.matid = wbd.matid and wbd.posid=wmt.posid
     and wmt.turntype = 2
  order by wmt.ondate */

            if (q != null && OnDateDBEdit.DateTime > q.OnDate)
            {
                String msg = "Дата документа не може бути більшою за дату видаткової партії! \nПозиція: " + q.Name + " \nДата: " + q.OnDate + " \nЗмінити дату докомента на " + q.OnDate + "?";
                if (MessageBox.Show(msg, "Інформація", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    wb.OnDate = q.OnDate;
                    return true;
                }
                else return false;
            }

            return true;
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void frmWayBillIn_FormClosed(object sender, FormClosedEventArgs e)
        {
           if (current_transaction.UnderlyingTransaction.Connection != null)
            {
                current_transaction.Rollback();
            }

           _db.Dispose();
           current_transaction.Dispose();
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            OnDateDBEdit.DateTime = DBHelper.ServerDateTime();
        }

        private void AddMaterialBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var df = new frmWayBillDetIn(_db, null,  wb);
            if (df.ShowDialog() == DialogResult.OK)
            {
                RefreshDet();
            }
        }

        private void EditMaterialBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var dr = WaybillDetInGridView.GetRow(WaybillDetInGridView.FocusedRowHandle) as GetWaybillDetIn_Result;

            if (dr != null)
            {
                if (dr.PosId > 0)
                {
                    new frmWayBillDetIn(_db, dr.PosId, wb).ShowDialog();
                }
                else
                {
                    new frmWaybillSvcDet(_db, dr.PosId * -1, wb).ShowDialog();
                }

                RefreshDet();
            }
        }

        private void DelMaterialBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var dr = WaybillDetInGridView.GetRow(WaybillDetInGridView.FocusedRowHandle) as GetWaybillDetIn_Result;

            if (dr != null)
            {
                if (dr.PosId > 0)
                {
                    _db.DeleteWhere<WaybillDet>(w => w.PosId == dr.PosId);
                }
                else
                {
                    _db.DeleteWhere<WayBillSvc>(w => w.PosId == dr.PosId * -1);
                }
                _db.SaveChanges();

                RefreshDet();
            }
        }

        private void RefreshDet()
        {
            _db.UpdWaybillDetPrice(_wbill_id);
            WaybillDetInBS.DataSource = _db.GetWaybillDetIn(_wbill_id);
            GetOk();
        }

        bool GetOk()
        {
            bool recult = (NumEdit.EditValue != null  && KagentComboBox.EditValue != null && OnDateDBEdit.EditValue != null && WaybillDetInBS.List.Count > 0);
            barSubItem1.Enabled = KagentComboBox.EditValue != null;

            OkButton.Enabled = recult;
            EditMaterialBtn.Enabled = WaybillDetInBS.List.Count > 0;
            DelMaterialBtn.Enabled = WaybillDetInBS.List.Count > 0;

            return recult;
        }

        private void NUMDBTextEdit_EditValueChanged(object sender, EventArgs e)
        {
            GetOk();
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

        private void WaybillDetInGridView_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            var dr = WaybillDetInGridView.GetRow(e.RowHandle) as GetWaybillDetIn_Result;
            var wbd = _db.WaybillDet.Find(dr.PosId);

            wbd.Amount = Convert.ToDecimal(e.Value);
        }

        private void PrintBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            PrintDoc.Show(wb.DocId.Value, wb.WType, _db);
        }

        private void barButtonItem6_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            IHelper.ShowMatList(_db, wb);
            RefreshDet();
        }

        private void KagentComboBox_EditValueChanged(object sender, EventArgs e)
        {
            if (!KagentComboBox.ContainsFocus) return;

            wb.KaId = (int?)KagentComboBox.EditValue;
            GetOk();
        }

        private void barButtonItem7_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var df = new frmWaybillSvcDet(_db, null, wb);
            if (df.ShowDialog() == DialogResult.OK)
            {
                RefreshDet();
            }
        }

        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void KagBalBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }



    }
}
