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
using DevExpress.XtraEditors;
using SP_Sklad.Common;
using SP_Sklad.Reports;
using SP_Sklad.SkladData;
using SP_Sklad.WBDetForm;
using EntityState = System.Data.Entity.EntityState;

namespace SP_Sklad.WBForm
{
    public partial class frmWBInventory : DevExpress.XtraEditors.XtraForm
    {
        private const int _wtype = 7;

        BaseEntities _db { get; set; }
        private int? _wbill_id { get; set; }
        private WaybillList wb { get; set; }
        public bool is_new_record { get; set; }
        private InventoryDet focused_dr
        {
            get { return InventoryDetGridView.GetFocusedRow() as InventoryDet; }
        }

        private UserSettingsRepository user_settings { get; set; }

        public frmWBInventory(int? wbill_id = null)
        {
            is_new_record = false;

            _wbill_id = wbill_id;
            _db = new BaseEntities();
            user_settings = new UserSettingsRepository(DBHelper.CurrentUser.UserId, _db);

            InitializeComponent();
        }

        private void frmWBInventory_Load(object sender, EventArgs e)
        {
            WhOutComboBox.Properties.DataSource = DBHelper.WhList;

            PersonComboBox.Properties.DataSource = DBHelper.Persons;
            lookUpEdit1.Properties.DataSource = DBHelper.Persons;
            lookUpEdit2.Properties.DataSource = DBHelper.Persons;
            lookUpEdit3.Properties.DataSource = DBHelper.Persons;

            if (_wbill_id == null)
            {
                is_new_record = true;

                wb = _db.WaybillList.Add(new WaybillList()
                {
                    Id = Guid.NewGuid(),
                    WType = _wtype,
                    OnDate = DBHelper.ServerDateTime(),
                    Num = "",
                    CurrId = DBHelper.Currency.FirstOrDefault(w => w.Def == 1).CurrId,
                    OnValue = 1,
                    PersonId = DBHelper.CurrentUser.KaId,
                    WaybillMove = new WaybillMove { SourceWid = DBHelper.WhList.FirstOrDefault(w => w.Def == 1).WId },
                    UpdatedBy = DBHelper.CurrentUser.UserId,
                    EntId = DBHelper.Enterprise.KaId
                });

                _db.SaveChanges();

                _db.Commission.Add(new Commission { WbillId = wb.WbillId, KaId = DBHelper.CurrentUser.KaId });

                _wbill_id = wb.WbillId;
            }
            else
            {
                wb = _db.WaybillList.FirstOrDefault(f => f.WbillId == _wbill_id);
            }

            if (wb != null && wb.WaybillMove != null)
            {
                DBHelper.UpdateSessionWaybill(wb.WbillId);

                if (is_new_record)
                {
                    using (var db = new BaseEntities())
                    {
                        wb.Num = db.GetDocNum("wb_inventory").FirstOrDefault();
                    }
                }

                WaybillListBS.DataSource = wb;
                WaybillMoveBS.DataSource = wb.WaybillMove;

                if (wb.Commission.Any())
                {
                    CommissionBS.DataSource = wb.Commission.FirstOrDefault();
                }

            }

            RefreshDet();
        }

     /*   private void UpdLockWB()
        {
            if (wb != null)
            {
                _db.Entry<WaybillList>(wb).State = EntityState.Detached;
            }

            wb = _db.Database.SqlQuery<WaybillList>("SELECT * from WaybillList WITH (UPDLOCK, NOWAIT) where WbillId = {0}", _wbill_id).FirstOrDefault();
            if (wb != null)
            {
                wb.WaybillMove = _db.WaybillMove.Find(_wbill_id);
            }

            _db.Entry<WaybillList>(wb).State = EntityState.Modified;
        }*/

        private void RefreshDet()
        {
            var query = _db.WaybillDet.Where(w => w.WbillId == _wbill_id).Select(s => new InventoryDet
            {
                PosId = s.PosId,
                Checked = s.Checked,
                Num = s.Num,
                MatId = s.MatId,
                MatName = s.Materials.Name,
                MsrName = s.Materials.Measures.ShortName,
                Amount = s.Amount,
                Price = s.Price,
                Discount = (s.Discount ?? 0),
                Nds = (s.Nds ?? 0),
                AmountAll = (s.Discount ?? 0) - s.Amount,
                SumAll = ((s.Discount ?? 0) * (s.Nds ?? 0)) - (s.Amount * (s.Price ?? 0))
            }).ToList();

            int top_row = InventoryDetGridView.TopRowIndex;
            WaybillDetInBS.DataSource = query;
            InventoryDetGridView.TopRowIndex = top_row;

            GetOk();
        }

        bool GetOk()
        {
            bool recult = (!String.IsNullOrEmpty(NumEdit.Text) && OnDateDBEdit.EditValue != null && WaybillDetInBS.Count > 0);

            WhOutComboBox.Enabled = (WaybillDetInBS.Count == 0);
            WhBtn.Enabled = WhOutComboBox.Enabled;

            EditMaterialBtn.Enabled = WaybillDetInBS.Count > 0;
            DelMaterialBtn.Enabled = EditMaterialBtn.Enabled;
            RsvInfoBtn.Enabled = EditMaterialBtn.Enabled;
            MatInfoBtn.Enabled = EditMaterialBtn.Enabled;

            OkButton.Enabled = recult;
            return recult;
        }

        private void WhOutComboBox_EditValueChanged(object sender, EventArgs e)
        {
            if (wb != null && wb.WaybillMove != null && WhOutComboBox.EditValue != null && WhOutComboBox.EditValue != DBNull.Value)
            {
                wb.WaybillMove.SourceWid = (int)WhOutComboBox.EditValue;
            }
        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            _db.DeleteWhere<WaybillDet>(w => w.WbillId == _wbill_id);

            int is_empty = 0;
            if (MessageBox.Show("Загружати позиції з нульовими залишками ?", "Залишки на складі", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
            {
                is_empty = 1;
            }

            var wh_remain = _db.WhMatGet(0, wb.WaybillMove.SourceWid, 0, wb.OnDate, is_empty, "*", 0, "", DBHelper.CurrentUser.UserId, 0).OrderBy(o => new { o.GrpName, o.MatName }).ToList();

            int num = 0;
            foreach (var item in wh_remain)
            {
                _db.WaybillDet.Add(new WaybillDet
                {
                    WbillId = _wbill_id.Value,
                    MatId = item.MatId,
                    WId = wb.WaybillMove.SourceWid,
                    Amount = item.Remain.HasValue ? item.Remain.Value : 0,
                    Price = item.AvgPrice,
                    Discount = item.Remain,
                    Nds = item.AvgPrice,
                    OnDate = wb.OnDate,
                    Num = ++num,
                    BasePrice = item.AvgPrice
                });
            }
            _db.SaveChanges();

            RefreshDet();
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            wb.UpdatedAt = DateTime.Now;
            _db.SaveChanges();

            if (TurnDocCheckBox.Checked)
            {
                var ex_wb = _db.ExecuteWayBill(wb.WbillId, null, DBHelper.CurrentUser.KaId).FirstOrDefault();
                if (ex_wb.ErrorMessage != "False")
                {
                    MessageBox.Show(ex_wb.ErrorMessage);
                    return;
                }
            }

            is_new_record = false;

            Close();
        }

        private void frmWBInventory_FormClosed(object sender, FormClosedEventArgs e)
        {
            DBHelper.UpdateSessionWaybill(_wbill_id.Value, true);

            if (is_new_record)
            {
                _db.DeleteWhere<WaybillList>(w => w.WbillId == _wbill_id);
            }

            _db.Dispose();
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            using (var frm = new frmWBInventoryDet(_db, null, wb))
            {
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    RefreshDet();
                }
            }
        }

        private void EditMaterialBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            dynamic dr = InventoryDetGridView.GetFocusedRow();

            using (var frm = new frmWBInventoryDet(_db, dr.PosId, wb))
            {
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    RefreshDet();
                }
            }
        }

        private void DelMaterialBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            dynamic dr = InventoryDetGridView.GetFocusedRow();
            if (dr != null)
            {
                int pos_id = dr.PosId;
                _db.DeleteWhere<WaybillDet>(w => w.PosId == pos_id);
                _db.SaveChanges();

                InventoryDetGridView.DeleteSelectedRows();
            }
        }

        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
               dynamic dr = InventoryDetGridView.GetFocusedRow();
               if (dr != null)
               {
                   int pos_id = dr.PosId;
                   var wbd = _db.WaybillDet.Find(pos_id);
                   if (wbd.Checked == 1)
                   {
                       wbd.Checked = 0;
                  //     wbd_row.Checked = 0;
                   }
                   else
                   {
                       wbd.Checked = 1;
                  //     wbd_row.Checked = 1;
                   }
                   _db.SaveChanges();
                   RefreshDet();
               }
        }

        private void InventoryDetGridView_DoubleClick(object sender, EventArgs e)
        {
            if (IHelper.isRowDublClick(sender)) EditMaterialBtn.PerformClick();
        }

        private void InventoryDetGridView_PopupMenuShowing(object sender, DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs e)
        {
            if (e.HitInfo.InRow)
            {
                Point p2 = Control.MousePosition;
                this.WbDetPopupMenu.ShowPopup(p2);
            }
        }

        private void PrevievBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            _db.SaveChanges();

            PrintDoc.Show(wb.Id, wb.WType, _db);
        }

        private void RsvInfoBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            IHelper.ShowMatRSV(focused_dr.MatId, _db);
        }

        private void MatInfoBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            IHelper.ShowMatInfo(focused_dr.MatId);
        }

        private void barButtonItem7_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            _db.SaveChanges();

            IHelper.ShowMatListByWH3(_db, wb, WhOutComboBox.EditValue.ToString());
            RefreshDet();
        }

        private void InventoryDetGridView_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            var wbd = _db.WaybillDet.Find(focused_dr.PosId);

            if (e.Column.FieldName == "Discount")
            {
                wbd.Discount = Convert.ToDecimal(e.Value);
            }
            if (e.Column.FieldName == "Nds")
            {
                wbd.Nds = Convert.ToDecimal(e.Value);
            }
            _db.SaveChanges();

            RefreshDet();

        }

        public class InventoryDet
        {
            public int PosId { get; set; }
            public int? Checked { get; set; }
            public int Num { get; set; }
            public int MatId { get; set; }
            public string MatName { get; set; }
            public string MsrName { get; set; }
            public decimal Amount { get; set; }
            public decimal? Price { get; set; }
            public decimal Discount { get; set; }
            public decimal Nds { get; set; }
            public decimal AmountAll { get; set; }
            public decimal SumAll { get; set; }
        }

        private void WhBtn_Click(object sender, EventArgs e)
        {
            WhOutComboBox.EditValue = IHelper.ShowDirectList(WhOutComboBox.EditValue, 2);
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {

        }

        private void lookUpEdit1_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
            {
                ((LookUpEdit)sender).EditValue = IHelper.ShowDirectList(((LookUpEdit)sender).EditValue, 3);
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void barButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            for (int i = 0; InventoryDetGridView.RowCount > i; i++)
            {
                var row = InventoryDetGridView.GetRow(i) as InventoryDet;

                var wbd = _db.WaybillDet.Find(row.PosId);
                wbd.Num = i + 1;

            }
            _db.SaveChanges();
            RefreshDet();
        }

        private void frmWBInventory_Shown(object sender, EventArgs e)
        {
            InventoryDetGridView.Appearance.Row.Font = new Font(user_settings.GridFontName, (float)user_settings.GridFontSize);
            NumEdit.Enabled = user_settings.AccessEditDocNum;
        }
    }
}
