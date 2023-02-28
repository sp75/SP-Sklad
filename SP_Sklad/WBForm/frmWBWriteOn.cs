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
using SP_Sklad.Properties;
using SP_Sklad.Reports;
using SP_Sklad.SkladData;
using SP_Sklad.ViewsForm;
using SP_Sklad.WBDetForm;
using EntityState = System.Data.Entity.EntityState;

namespace SP_Sklad.WBForm
{
    public partial class frmWBWriteOn : DevExpress.XtraEditors.XtraForm
    {
        private const int _wtype = 5;

        public  BaseEntities _db { get; set; }
        public int? _wbill_id { get; set; }
        public Guid? doc_id { get; set; }
        private WaybillList wb { get; set; }
        private GetWaybillDetIn_Result wbd_row { get; set; }
        private List<GetWaybillDetIn_Result> wbd_list { get; set; }
        private List<GetRelDocList_Result> rdl  { get; set; }
        private GetWaybillDetIn_Result focused_dr
        {
            get { return WaybillDetInGridView.GetFocusedRow() as GetWaybillDetIn_Result; }
        }
        public bool is_new_record { get; set; }
        private UserSettingsRepository user_settings { get; set; }

        public frmWBWriteOn(int? wbill_id = null)
        {
            is_new_record = false;
            _wbill_id = wbill_id;
            _db = new BaseEntities();
            user_settings = new UserSettingsRepository(DBHelper.CurrentUser.UserId, _db);

            InitializeComponent();
        }

        private void frmWBWriteOn_Load(object sender, EventArgs e)
        {
            PersonComboBox.Properties.DataSource = DBHelper.Persons;
            var wh_list = DBHelper.WhList;
            WHComboBox.Properties.DataSource = wh_list;
            WHComboBox.EditValue = wh_list.Where(w => w.Def == 1).Select(s => s.WId).FirstOrDefault();

            if (_wbill_id == null && doc_id == null)
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
                    Nds = DBHelper.Enterprise.NdsPayer == 1 ? DBHelper.CommonParam.Nds : 0,
                    UpdatedBy = DBHelper.CurrentUser.UserId,
                    EntId = DBHelper.Enterprise.KaId
                });

                _db.SaveChanges();
            }
            else
            {
                //   UpdLockWB();
                wb = _db.WaybillList.FirstOrDefault(f => f.Id == doc_id || f.WbillId == _wbill_id);
            }

            if (wb != null)
            {
                _wbill_id = wb.WbillId;

                DBHelper.UpdateSessionWaybill(wb.WbillId);

                if (is_new_record && String.IsNullOrEmpty( wb.Num)) 
                {
                    wb.Num = new BaseEntities().GetDocNum("wb_write_on").FirstOrDefault();
                }

                TurnDocCheckBox.EditValue = wb.Checked;

                PersonComboBox.DataBindings.Add(new Binding("EditValue", wb, "PersonId", true, DataSourceUpdateMode.OnValidation));
                NumEdit.DataBindings.Add(new Binding("EditValue", wb, "Num"));
                OnDateDBEdit.DataBindings.Add(new Binding("EditValue", wb, "OnDate"));
              
                NotesEdit.DataBindings.Add(new Binding("EditValue", wb, "Notes"));
                ReasonEdit.DataBindings.Add(new Binding("EditValue", wb, "Reason"));

                rdl = _db.GetRelDocList(wb.Id).Where(w => w.DocType == 7 || w.DocType == -22).ToList();
                AddBarSubItem.Enabled = !rdl.Any();
                EditMaterialBtn.Enabled = !rdl.Any(a => a.DocType == 7);
                DelMaterialBtn.Enabled = AddBarSubItem.Enabled;
            }

            RefreshDet();
        }

        private void RefreshDet()
        {
            wbd_list = _db.GetWaybillDetIn(_wbill_id).ToList();

            int top_row = WaybillDetInGridView.TopRowIndex;
            WaybillDetInBS.DataSource = wbd_list;
            WaybillDetInGridView.TopRowIndex = top_row;

            frmValidating();
        }

        bool frmValidating()
        {
            bool recult = (!String.IsNullOrEmpty(NumEdit.Text) && OnDateDBEdit.EditValue != null && WaybillDetInBS.Count > 0);


            EditMaterialBtn.Enabled = (WaybillDetInBS.Count > 0 && !rdl.Any(a => a.DocType == 7));
            DelMaterialBtn.Enabled = (WaybillDetInBS.Count > 0 && !rdl.Any());
            MatInfoBtn.Enabled = WaybillDetInBS.Count > 0;
            RsvInfoBtn.Enabled = MatInfoBtn.Enabled;
            PrevievBtn.Enabled = MatInfoBtn.Enabled;

            OkButton.Enabled = recult;
            return recult;
        }

        private void frmWBWriteOn_FormClosed(object sender, FormClosedEventArgs e)
        {
            DBHelper.UpdateSessionWaybill(_wbill_id.Value, true);

            if (is_new_record)
            {
                _db.DeleteWhere<WaybillList>(w => w.WbillId == _wbill_id);
                //   current_transaction.Commit();

            }

            /*      if (current_transaction.UnderlyingTransaction.Connection != null)
                  {
                      current_transaction.Rollback();
                  }*/

            _db.Dispose();
            //   current_transaction.Dispose();
        }

        private void frmWBWriteOn_Shown(object sender, EventArgs e)
        {
            OnDateDBEdit.Enabled = (DBHelper.CurrentUser.EnableEditDate == 1);
            NowDateBtn.Enabled = OnDateDBEdit.Enabled;

            PersonComboBox.Enabled = !String.IsNullOrEmpty(user_settings.AccessEditPersonId) && Convert.ToInt32(user_settings.AccessEditPersonId) == 1;
            PersonEditBtn.Enabled = PersonComboBox.Enabled;
            WaybillDetInGridView.Appearance.Row.Font = new Font(user_settings.GridFontName, (float)user_settings.GridFontSize);

            NumEdit.Enabled = user_settings.AccessEditDocNum;
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            wb.UpdatedAt = DateTime.Now;

            if (!CheckDate())
            {
                return;
            }

            _db.Save(wb.WbillId);

        //    current_transaction.Commit();

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
                String msg = "Дата документа не може бути меншою за дату виготовлення продукції! \nПозиція: " + q.MatName + " \nДата: " + q.Make.OnDate + " \nЗмінити дату докомента на " + q.Make.OnDate + "?";
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
                var wbd = _db.WaybillDet.FirstOrDefault(w => w.PosId == wbd_row.PosId);
                if (wbd != null)
                {
                    _db.WaybillDet.Remove(wbd);
                }
                _db.Save(wb.WbillId);

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

            _db.SaveChanges();
        }

        private void TurnDocCheckBox_EditValueChanged(object sender, EventArgs e)
        {
            frmValidating();
        }

        private void WHComboBox_EditValueChanged(object sender, EventArgs e)
        {
            if (WHComboBox.Focused)
            {
                UpdateWh();
            }
        }

        private void UpdateWh()
        {
            if (WaybillDetInBS.Count > 0)
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
                    _db.Save(wb.WbillId);
                    RefreshDet();
                }
            }
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
            _db.Save(wb.WbillId);

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

        private void barButtonItem6_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            IHelper.ShowMatList(_db, wb);
            RefreshDet();
        }

        private void PersonEditBtn_Click(object sender, EventArgs e)
        {
            WHComboBox.EditValue = IHelper.ShowDirectList(WHComboBox.EditValue, 2);
            UpdateWh();
        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            PersonComboBox.EditValue = IHelper.ShowDirectList(PersonComboBox.EditValue, 3);
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void WaybillDetInGridView_FocusedRowObjectChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventArgs e)
        {
            wbd_row = WaybillDetInGridView.GetRow(WaybillDetInGridView.FocusedRowHandle) as GetWaybillDetIn_Result;
        }

        private void frmWBWriteOn_FormClosing(object sender, FormClosingEventArgs e)
        {
            if ((is_new_record || _db.IsAnyChanges()) && OkButton.Enabled)
            {
                var m_recult = MessageBox.Show(Resources.save_wb, "Видаткова накладна №" + wb.Num, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information);

                if (m_recult == DialogResult.Yes)
                {
                    OkButton.PerformClick();
                }

                if (m_recult == DialogResult.Cancel)
                {
                    e.Cancel = true;
                }

            }
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            using (var frm = new frmManufacturing(_db, 2))
            {
                frm.xtraTabPage14.PageVisible = true;
                frm.xtraTabControl1.SelectedTabPageIndex = 1;

                if (frm.ShowDialog() == DialogResult.OK)
                {
                    foreach (var item in frm.manuf_list.ToList())
                    {
                        var price = (item.Price ?? 0);

                        var wbd = _db.WaybillDet.Add(new WaybillDet
                        {
                            WbillId = wb.WbillId,
                            Num = wb.WaybillDet.Count(),
                            MatId = item.MatId,
                            WId = Convert.ToInt32(WHComboBox.EditValue),
                            Amount = item.Amount,
                            Price = price,
                            Discount = 0,
                            Nds = wb.Nds,
                            CurrId = wb.CurrId,
                            OnDate = wb.OnDate,
                            OnValue = wb.OnValue,
                            BasePrice = price ,
                            PosKind = 0,
                            PosParent = 0,
                            DiscountKind = 0
                        });
                        _db.SaveChanges();

                        wbd.WayBillDetAddProps = new WayBillDetAddProps { PosId = wbd.PosId, WbMaked = item.WbillId };
                        _db.Serials.Add(new Serials
                        {
                            PosId = wbd.PosId,
                            SerialNo = item.Num,
                            InvNumb = item.BarCode
                        });

                    }

                    _db.SaveChanges();
                    RefreshDet();
                }
            }
        }
    }
}
