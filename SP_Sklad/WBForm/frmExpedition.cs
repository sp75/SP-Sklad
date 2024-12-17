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
    public partial class frmExpedition : DevExpress.XtraEditors.XtraForm
    {
        private Guid? _exp_id { get; set; }
        public BaseEntities _db { get; set; }
        private Expedition exp { get; set; }
        public bool is_new_record { get; set; }
        private v_ExpeditionDet focused_dr => ExpeditionDetGridView.GetFocusedRow() as v_ExpeditionDet;

        public frmExpedition(Guid? exp_id = null)
        {
            InitializeComponent();

            is_new_record = false;
            _exp_id = exp_id;

            _db = new BaseEntities();
        }
          
        private void frmPriceList_Load(object sender, EventArgs e)
        {
            CarsLookUpEdit.Properties.DataSource = _db.Cars.ToList();
            PersonComboBox.Properties.DataSource = DBHelper.Persons;
            DriversLookUpEdit.Properties.DataSource = _db.Kagent.Where(w => w.JobType == 3).Select(s => new Kontragent { KaId = s.KaId, Name = s.Name }).ToList();
            RouteLookUpEdit.Properties.DataSource = DB.SkladBase().Routes.AsNoTracking().ToList();

            if (_exp_id == null)
            {
                is_new_record = true;

                exp = _db.Expedition.Add(new Expedition
                {
                    Id = Guid.NewGuid(),
                    DocType = 32,
                    Checked = 1,
                    OnDate = DBHelper.ServerDateTime(),
                    PersonId = DBHelper.CurrentUser.KaId,
                    Num = new BaseEntities().GetDocNum("expedition").FirstOrDefault(),
                    CarId = _db.Cars.FirstOrDefault().Id
                });

                _db.SaveChanges();
                _exp_id = exp.Id;
            }
            else
            {
                exp = _db.Expedition.Find(_exp_id);
            }

            if (exp != null)
            {
                ExpeditionBS.DataSource = exp;

                exp.SessionId =  (Guid?)UserSession.SessionId;
            }

            GetDetail();
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            exp.UpdatedAt = DateTime.Now;
            exp.UpdatedBy = DBHelper.CurrentUser.UserId;

            if(exp.Checked == 1)
            {
                foreach(var item in _db.v_ExpeditionDet.Where(w=> w.ExpeditionId == exp.Id))
                {
                    if(item.RouteId.HasValue && item.Checked == 1)
                    {
                        var wb = _db.WaybillList.Find(item.WbillId);
                        wb.ShipmentDate = exp.UpdatedAt.Value.AddTicks(item.RouteDuration ?? 0);
                    }
                }
            }

            _db.SaveChanges();

            is_new_record = false;
            Close();
        }

        private void frmPriceList_FormClosed(object sender, FormClosedEventArgs e)
        {
            exp.SessionId = null;
            exp.UpdatedBy = UserSession.UserId;
            exp.UpdatedAt = DateTime.Now;

            if (is_new_record)
            {
                _db.DeleteWhere<Expedition>(w => w.Id == _exp_id);
            }
            else
            {
                _db.SaveChanges();
            }

            _db.Dispose();
        }

        void GetDetail()
        {
            int top_row = ExpeditionDetGridView.TopRowIndex;
            ExpeditionDetBS.DataSource = _db.v_ExpeditionDet.AsNoTracking().Where(w=> w.ExpeditionId == _exp_id).OrderBy(o=> o.CreatedAt).ToList();
            ExpeditionDetGridView.TopRowIndex = top_row;

            GetOk();
        }

        private void DelMaterialBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (focused_dr == null)
            {
                return;
            }

            _db.ExpeditionDet.Remove(_db.ExpeditionDet.FirstOrDefault(w => w.Id == focused_dr.Id));
            _db.SaveChanges();
            GetDetail();
        }

        private void PrevievBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            PrintDoc.ExpeditionReport(_exp_id.Value, _db);
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
            bool recult = ExpeditionDetBS.List.Count > 0;

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
            using (var frm = new frmBarCode())
            {
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    var wbill_id = Convert.ToInt32(frm.BarCodeEdit.Text);
                    if (_db.WaybillList.Any(w => w.WbillId == wbill_id && w.WType == -1))
                    {
                        using (var fed = new frmExpeditionDet(_db, null, exp, wbill_id))
                        {
                            fed.ShowDialog();
                        }

                        GetDetail();
                    }
                    else
                    {
                        MessageBox.Show("Документ не знайдено!");
                    }
                }
            }
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if(focused_dr == null)
            {
                return;
            }

            using (var frm = new frmExpeditionDet(_db, focused_dr.Id, exp, focused_dr.WbillId))
            {
                frm.ShowDialog();
            }

            GetDetail();
        }

        private void ExpeditionDetGridView_PopupMenuShowing(object sender, DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs e)
        {
            if (e.HitInfo.InRow)
            {
                Point p2 = Control.MousePosition;
                this.TemplateListPopupMenu.ShowPopup(p2);
            }
        }
    }
}
