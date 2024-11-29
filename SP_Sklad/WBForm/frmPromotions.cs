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
    public partial class frmPromotions : DevExpress.XtraEditors.XtraForm
    {
        public BaseEntities _db { get; set; }
        private Promotions prom { get; set; }
        public bool is_new_record { get; set; }
        private Guid? _prom_id { get; set; }
        private PromotionKagent focused_correction_det => PromotionKagentGridView.GetFocusedRow() as PromotionKagent;

        public frmPromotions(Guid? prom_id = null)
        {
            InitializeComponent();

            is_new_record = false;
            _prom_id = prom_id;

            _db = new BaseEntities();
        }
          
        private void frmPriceList_Load(object sender, EventArgs e)
        {
            PersonComboBox.Properties.DataSource = DBHelper.Persons;
            MatLookUpEdit.Properties.DataSource = _db.v_Materials.AsNoTracking().Where(w => w.Archived == 0).ToList();


            if (_prom_id == null)
            {
                is_new_record = true;

                prom = _db.Promotions.Add(new Promotions
                {
                    Id = Guid.NewGuid(),
                    DocType = 36,
                    Checked = 1,
                    StartDate = DBHelper.ServerDateTime(),
                    PersonId = DBHelper.CurrentUser.KaId,
                    Num = new BaseEntities().GetDocNum("promotion").FirstOrDefault(),
                });

                _db.SaveChanges();
                _prom_id = prom.Id;
            }
            else
            {
                prom = _db.Promotions.Find(_prom_id);
            }

            if (prom != null)
            {
                PromotionsBS.DataSource = prom;
            }

            GetDetail();
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            prom.UpdatedAt = DateTime.Now;
            prom.UpdatedBy = DBHelper.CurrentUser.UserId;
     //       prom.Checked = 1;

            _db.SaveChanges();

            is_new_record = false;

            Close();

        }

        private void frmPriceList_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (is_new_record)
            {
                _db.DeleteWhere<Promotions>(w => w.Id == _prom_id);
            }

            _db.Dispose();
        }

        void GetDetail()
        {
            
            int top_row = PromotionKagentGridView.TopRowIndex;
            PromotionKagentBS.DataSource = _db.v_PromotionKagent.Where(w=> w.PromotionId == _prom_id).ToList();
            PromotionKagentGridView.TopRowIndex = top_row;

            GetOk();
        }

        private void DelMaterialBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            foreach (var item in PromotionKagentGridView.GetSelectedRows())
            {
                var row = PromotionKagentGridView.GetRow(item) as v_PromotionKagent;

                _db.PromotionKagent.Remove(_db.PromotionKagent.Find(row.Id));
            }
            _db.SaveChanges();

            GetDetail();
        }

        private void PrevievBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            Close();
        }
             
        private void OnDateDBEdit_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {

        }

        bool GetOk()
        {
            bool recult = MatLookUpEdit.EditValue != null && MatLookUpEdit.EditValue != DBNull.Value && PriceEdit.Value > 0; // PromotionKagentBS.List.Count > 0;

            OkButton.Enabled = recult;

            return recult;
        }

        private void OnDateDBEdit_EditValueChanged(object sender, EventArgs e)
        {
            GetOk();
        }

        private void frmSettingMaterialPrices_Shown(object sender, EventArgs e)
        {
            NumEdit.Focus();
        }

        private void AddDocBtnItem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            using (var frm = new frmKagents(5, ""))
            {
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    foreach(var item in frm.SelectedRows)
                    {
                        if (!_db.PromotionKagent.Any(a => a.KaId == item.KaId && a.PromotionId == _prom_id))
                        {
                            _db.PromotionKagent.Add(new PromotionKagent { KaId = item.KaId, PromotionId = _prom_id });
                        }
                    }
                    _db.SaveChanges();
                    GetDetail();
                }
            }
        }

        private void ExpeditionDetGridView_PopupMenuShowing(object sender, DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs e)
        {
            if (e.HitInfo.InRow)
            {
                Point p2 = Control.MousePosition;
                this.TemplateListPopupMenu.ShowPopup(p2);
            }
        }

        private void MatLookUpEdit_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if(e.Button.Index == 1)
            {
                MatLookUpEdit.EditValue = IHelper.ShowDirectList(MatLookUpEdit.EditValue, 5);
            }
        }
    }
}
