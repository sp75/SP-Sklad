using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.Data;
using DevExpress.Utils.DragDrop;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraTreeList;
using SP_Sklad.Common;
using SP_Sklad.Properties;
using SP_Sklad.Reports;
using SP_Sklad.SkladData;
using SP_Sklad.ViewsForm;
using SP_Sklad.WBDetForm;

namespace SP_Sklad.WBForm
{
    public partial class frmWaybillCorrection : DevExpress.XtraEditors.XtraForm
    {
        private Guid? _corr_id { get; set; }
        public BaseEntities _db { get; set; }
        private WaybillCorrection wbcor { get; set; }
        public bool is_new_record { get; set; }
        private int? _pos_id { get; set; }
        private v_WaybillCorrectionDet focused_correction_det => WaybillCorrectionDetGridView.GetFocusedRow() as v_WaybillCorrectionDet;

        public frmWaybillCorrection(int? pos_id = null)
        {
            InitializeComponent();

            is_new_record = false;
            _pos_id = pos_id;

            _db = new BaseEntities();
        }
          
        private void frmPriceList_Load(object sender, EventArgs e)
        {
            PersonComboBox.Properties.DataSource = DBHelper.Persons;
          

            if (_corr_id == null)
            {
                is_new_record = true;

                wbcor = _db.WaybillCorrection.Add(new WaybillCorrection
                {
                    Id = Guid.NewGuid(),
                    DocType = 34,
                    Checked = 1,
                    OnDate = DBHelper.ServerDateTime(),
                    PersonId = DBHelper.CurrentUser.KaId,
                    Num = new BaseEntities().GetDocNum("waybill_correction").FirstOrDefault(),
                       
                });

                _db.SaveChanges();
                _corr_id = wbcor.Id;
            }
            else
            {
                wbcor = _db.WaybillCorrection.Find(_corr_id);
            }

            if (wbcor != null)
            {
                WaybillCorrectionBS.DataSource = wbcor;
            }

            if(_pos_id.HasValue)
            {
                AddItem(_pos_id.Value);
            }

            GetDetail();
        }

        private void AddItem(int pos_id)
        {
            var pos = _db.WaybillDet.Find(pos_id);
            if (pos != null)
            {
                _db.WaybillCorrectionDet.Add(new WaybillCorrectionDet
                {
                    Checked = 0,
                    Id = Guid.NewGuid(),
                    OldMatId = pos.MatId,
                    OldPrice = pos.BasePrice,
                    WaybillCorrectionId = wbcor.Id,
                    PosId = pos.PosId,
                });

                _db.SaveChanges();
            }
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            wbcor.UpdatedAt = DateTime.Now;
            wbcor.UpdatedBy = DBHelper.CurrentUser.UserId;
            wbcor.Checked = 1;
            int corr_count = 0;

            foreach (var item in _db.v_WaybillCorrectionDet.AsNoTracking().Where(w => w.WaybillCorrectionId == wbcor.Id && w.Checked == 0).ToList())
            {
                var wbcor_det = _db.WaybillCorrectionDet.Find(item.Id);
                var wbd = _db.WaybillDet.Find(item.PosId);
                var wbl = _db.WaybillList.Find(item.WbillId);

                if (!_db.ReturnRel.Any(a => a.OutPosId == item.PosId))
                {
                    if (item.OldPrice != item.NewPrice && item.NewPrice.HasValue)
                    {
                        wbd.BasePrice = item.NewPrice;
                        wbd.Price = wbd.BasePrice - (wbd.BasePrice * (wbd.Discount ?? 0) / 100);

                        foreach (var pr in _db.PosRemains.Where(w => w.PosId == item.PosId))
                        {
                            pr.AvgPrice = wbd.Price;
                        }

                        wbl.UpdatedAt = DateTime.Now;
                        wbl.UpdatedBy = DBHelper.CurrentUser.UserId;
                        _db.SaveChanges();
                        _db.RecalcKaSaldo(item.KaId);

                        wbcor_det.Checked = 1;
                    }

                    if (item.OldMatId != item.NewMatId && item.NewMatId.HasValue)
                    {
                        wbcor_det.Checked = 0; 

                        if (_db.GetPosIn(item.OnDate, item.NewMatId, item.WId, 0, DBHelper.CurrentUser.UserId).Sum(w => w.CurRemain) >= item.Amount)
                        {
                            _db.DeleteWhere<WMatTurn>(w => w.SourceId == item.PosId );
                            wbd.MatId = item.NewMatId.Value;
                            _db.SaveChanges();

                            var r = new ObjectParameter("RSV", typeof(Int32));

                            _db.ReservedPosition(item.PosId, r, DBHelper.CurrentUser.UserId);

                            if (r.Value != null && (int)r.Value == 0)
                            {
                                wbd.MatId = item.OldMatId.Value;
                                _db.SaveChanges();
                                _db.ReservedPosition(item.PosId, r, DBHelper.CurrentUser.UserId);
                                foreach (var i in _db.WMatTurn.Where(w => w.SourceId == item.PosId).ToList())
                                {
                                    i.TurnType = -1;
                                }

                                wbcor_det.Notes = "Не вдалося зарезервувати!";
                            }
                            else
                            {
                                foreach (var i in _db.WMatTurn.Where(w => w.SourceId == item.PosId ).ToList())
                                {
                                    i.TurnType = -1;
                                }

                                wbcor_det.Notes = "";
                                wbcor_det.Checked = 1;
                            }

                            _db.SaveChanges();
                        }
                        else
                        {
                            wbcor_det.Notes = "На складі відсутня необхідна кількість товару!";
                        }
                    }
                }
                else
                {
                    wbcor_det.Notes = "Є повернення по цій позиції!";
                }

                ++corr_count;
            }

            _db.SaveChanges();

            is_new_record = false;

            GetDetail();

            MessageBox.Show($"{corr_count} корегувань виконано!");
           
        }

        private void frmPriceList_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (is_new_record)
            {
                _db.DeleteWhere<WaybillCorrection>(w => w.Id == _corr_id);
            }

            _db.Dispose();
        }

        void GetDetail()
        {
            
            int top_row = WaybillCorrectionDetGridView.TopRowIndex;
            WaybillCorrectionDetBS.DataSource = _db.v_WaybillCorrectionDet.AsNoTracking().Where(w=> w.WaybillCorrectionId == _corr_id).ToList();
            WaybillCorrectionDetGridView.TopRowIndex = top_row;

            GetOk();
        }

        private void DelMaterialBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var item = _db.WaybillCorrectionDet.FirstOrDefault(w => w.Id == focused_correction_det.Id);
            if (item.Checked == 0)
            {
                _db.WaybillCorrectionDet.Remove(item);
                _db.SaveChanges();
                GetDetail();
            }
        }

        private void PrevievBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
         //   PrintDoc.ExpeditionReport(_corr_id.Value, _db);
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            Close();
        }
             
        private void OnDateDBEdit_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if(e.Button.Index ==1)
            {
                OnDateDBEdit.DateTime = DBHelper.ServerDateTime();
            }
        }

        bool GetOk()
        {
            bool recult = WaybillCorrectionDetBS.List.Count > 0;

            OkButton.Enabled = recult;

            return recult;
        }

        private void OnDateDBEdit_EditValueChanged(object sender, EventArgs e)
        {
            GetOk();
        }

        private void frmSettingMaterialPrices_Shown(object sender, EventArgs e)
        {
            OnDateDBEdit.Focus();
            NumEdit.Focus();
        }

        private void AddDocBtnItem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            using (var frm = new frmWaybillDetView(new List<int?> { -1 }))
            {
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    AddItem(frm.focused_row.PosId);
                    GetDetail();
                }
            }
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
          /*  using (var frm = new frmExpeditionDet(_db, focused_dr.Id, wbcor, focused_dr.WbillId))
            {
                frm.ShowDialog();
            }

            GetDetail();*/
        }

        private void ExpeditionDetGridView_PopupMenuShowing(object sender, DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs e)
        {
            if (e.HitInfo.InRow)
            {
                Point p2 = Control.MousePosition;
                this.TemplateListPopupMenu.ShowPopup(p2);
            }
        }

        private void WaybillCorrectionDetGridView_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.FieldName == "NewPrice")
            {
                var wbcor_det = _db.WaybillCorrectionDet.Find(focused_correction_det.Id);
                wbcor_det.NewPrice = Convert.ToDecimal(e.Value);
                _db.SaveChanges();

                GetDetail();
            }

            if (e.Column.FieldName == "Notes")
            {
                var wbcor_det = _db.WaybillCorrectionDet.Find(focused_correction_det.Id);
                wbcor_det.Notes = Convert.ToString(e.Value);
                _db.SaveChanges();

                GetDetail();
            }
        }

        private void repositoryItemButtonEdit2_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 0)
            {
                var wbcor_det = _db.WaybillCorrectionDet.Find(focused_correction_det.Id);
                var result = IHelper.ShowRemainByWH(focused_correction_det.OldMatId, focused_correction_det.WId, 1);
                //       _wbd.WId = result.wid;
                if (focused_correction_det.OldMatId != result.mat_id)
                {
                    wbcor_det.NewMatId = result.mat_id;
                    _db.SaveChanges();
                    GetDetail();
                }
            }
        }
    }
}
