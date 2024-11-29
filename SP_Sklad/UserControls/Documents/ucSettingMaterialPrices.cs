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
using DevExpress.XtraBars;
using SP_Sklad.Common;
using SP_Sklad.WBForm;
using DevExpress.Data;
using SP_Sklad.ViewsForm;
using SP_Sklad.Properties;
using DevExpress.XtraEditors;
using SP_Sklad.Reports;
using System.Collections;

namespace SP_Sklad.UserControls
{
    public partial class ucSettingMaterialPrices : DevExpress.XtraEditors.XtraUserControl
    {
        private int fun_id = 97;
        private string reg_layout_path = "ucSettingMaterialPrices\\SettingMaterialPricesGridView";
        BaseEntities _db { get; set; }

        public v_SettingMaterialPrices row_smp => SettingMaterialPricesGridView.GetFocusedRow() is NotLoadedObject ? null : SettingMaterialPricesGridView.GetFocusedRow() as v_SettingMaterialPrices;
        public v_SettingMaterialPricesDet row_smp_det => SettingMaterialPricesDetGrid.GetFocusedRow() as v_SettingMaterialPricesDet;

        private UserAccess user_access { get; set; }

        private int prev_rowHandle = 0;
        int row = 0;
        bool restore = false;
        public ucSettingMaterialPrices()
        {
            InitializeComponent();
        }
        protected override void OnCreateControl()
        {
            base.OnCreateControl();

            this.ParentForm.FormClosing += new FormClosingEventHandler(ParentForm_FormClosing);
        }

        void ParentForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            SettingMaterialPricesGridView.SaveLayoutToRegistry(IHelper.reg_layout_path + reg_layout_path);
        }

        public void GetData()
        {
            prev_rowHandle = SettingMaterialPricesGridView.FocusedRowHandle;

            row = SettingMaterialPricesGridView.FocusedRowHandle;
            restore = true;

            SettingMaterialPricesGridControl.DataSource = null;
            SettingMaterialPricesGridControl.DataSource = SettingMaterialPricesSource;

            GetDetailData();
        }

        public void NewItem()
        {
            using (var frm = new frmSettingMaterialPrices())
            {
                frm.ShowDialog();
            }
        }

        public void EditItem()
        {
            using (var smp_frm = new frmSettingMaterialPrices(row_smp.Id))
            {
                smp_frm.ShowDialog();
            }
        }

        public void DeleteItem()
        {
            var smp = _db.SettingMaterialPrices.Find(row_smp.Id);
            if (smp != null)
            {
                if (XtraMessageBox.Show($"Ви дійсно бажаєте видалити документ #{smp.Num}", "Видалення документа", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    _db.SettingMaterialPrices.Remove(smp);
                    _db.SaveChanges();
                }
            }
        }

        public void ExecuteItem()
        {
            if (row_smp.Id == null)
            {
                return;
            }

            var smp = _db.SettingMaterialPrices.Find(row_smp.Id);

            if (smp == null)
            {
                MessageBox.Show(Resources.not_find_wb);
                return;
            }
            if (smp.SessionId != null)
            {
                MessageBox.Show(Resources.deadlock);
                return;
            }


            if (smp.OnDate.Date < DBHelper.ServerDateTime().Date)
            {
                XtraMessageBox.Show("Проводити та сторнувати цей документ заборонено", "Провести документ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (smp.Checked == 1)
            {
                smp.Checked = 0;
            }
            else
            {
                smp.Checked = 1;
            }

            _db.SaveChanges();
        }

        public void CopyItem()
        {
            using (var frm = new frmMessageBox("Інформація", Resources.wb_copy))
            {
                if (!frm.user_settings.NotShowMessageCopyDocuments && frm.ShowDialog() != DialogResult.Yes)
                {
                    return;
                }
            }

            var pl_id = DB.SkladBase().CopySettingMaterialPrice(row_smp.Id).FirstOrDefault();

            using (var smp_frm = new frmSettingMaterialPrices(pl_id))
            {
                smp_frm.ShowDialog();
            }
        }

        public void PrintItem()
        {
            PrintDoc.SettingMaterialPricesReport(row_smp.Id, _db);
        }

        public void ExportToExcel()
        {
            IHelper.ExportToXlsx(SettingMaterialPricesGridControl);
        }

        private void SettingMaterialPricesGridView_FocusedRowObjectChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventArgs e)
        {
            DeleteItemBtn.Enabled = (row_smp != null && row_smp.Checked == 0 && user_access.CanDelete == 1);
            ExecuteItemBtn.Enabled = (row_smp != null && user_access.CanPost == 1);
            EditItemBtn.Enabled = (row_smp != null && row_smp.Checked == 0 && user_access.CanModify == 1);
            CopyItemBtn.Enabled = (row_smp != null && user_access.CanModify == 1);
            PrintItemBtn.Enabled = (row_smp != null);
            PrintItemBtn2.Enabled = (row_smp != null);

            if (row_smp?.Checked == 0) ExecuteItemBtn.ImageIndex = 17;
            else ExecuteItemBtn.ImageIndex = 6;

            GetDetailData();
        }

        private void GetDetailData()
        {
            if (row_smp != null)
            {
                SettingMaterialPricesDetBS.DataSource = DB.SkladBase().v_SettingMaterialPricesDet.AsNoTracking().OrderBy(o=> o.Num).Where(w => w.SettingMaterialPricesId == row_smp.Id).ToList();
            }
            else
            {
                SettingMaterialPricesDetBS.DataSource = null;
            }
        }

        System.IO.Stream wh_layout_stream = new System.IO.MemoryStream();
        private void SettingMaterialPricesUserControl_Load(object sender, EventArgs e)
        {
            SettingMaterialPricesGridView.SaveLayoutToStream(wh_layout_stream);
            SettingMaterialPricesGridView.RestoreLayoutFromRegistry(IHelper.reg_layout_path + reg_layout_path);

            if (!DesignMode)
            {
                _db = new BaseEntities();
                user_access = _db.UserAccess.FirstOrDefault(w => w.FunId == fun_id && w.UserId == UserSession.UserId);
            }
        }

        private void SettingMaterialPricesGridView_DoubleClick(object sender, EventArgs e)
        {
            EditItemBtn.PerformClick();
        }

        private void SettingMaterialPricesSource_GetQueryable(object sender, DevExpress.Data.Linq.GetQueryableEventArgs e)
        {
            var list = DB.SkladBase().v_SettingMaterialPrices.AsQueryable();
            e.QueryableSource = list;
        }

        private void SettingMaterialPricesGridView_AsyncCompleted(object sender, EventArgs e)
        {
            if (!restore)
            {
                return;
            }

            SettingMaterialPricesGridView.TopRowIndex = row;
            SettingMaterialPricesGridView.FocusedRowHandle = row;
            restore = false;
        }

        private void NewItemBtn_ItemClick(object sender, ItemClickEventArgs e)
        {
            NewItem();
            GetData();
        }

        private void SettingMaterialPricesGridView_PopupMenuShowing(object sender, DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs e)
        {
            if (e.HitInfo.InRow)
            {
                Point p2 = Control.MousePosition;
                SettingMaterialPricesPopupMenu.ShowPopup(p2);
            }
        }

        private void DeleteItemBtn_ItemClick(object sender, ItemClickEventArgs e)
        {
            DeleteItem();
            GetData();
        }

        private void RefrechItemBtn_ItemClick(object sender, ItemClickEventArgs e)
        {
            GetData();
        }

        private void ExecuteItemBtn_ItemClick(object sender, ItemClickEventArgs e)
        {
            ExecuteItem();
            GetData();
        }

        private void PrintItemBtn_ItemClick(object sender, ItemClickEventArgs e)
        {
            PrintItem();
        }

        private void EditItemBtn_ItemClick(object sender, ItemClickEventArgs e)
        {
            EditItem();
            GetData();
        }

        private void SettingMaterialPricesDetGrid_PopupMenuShowing(object sender, DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs e)
        {
            if (e.HitInfo.InRow)
            {
                Point p2 = Control.MousePosition;
                SettingMaterialPricesDetPopupMenu.ShowPopup(p2);
            }
        }

        private void HistoryBtnItem_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (row_smp_det != null)
            {
                new frmMaterialPriceHIstory(row_smp_det.MatId).ShowDialog();
            }
        }

        private void barButtonItem5_ItemClick(object sender, ItemClickEventArgs e)
        {
            IHelper.ShowTurnMaterial(row_smp_det.MatId);
        }

        private void MatIfoBtnItem_ItemClick(object sender, ItemClickEventArgs e)
        {
            IHelper.ShowMatInfo(row_smp_det.MatId);
        }

        private void barButtonItem6_ItemClick(object sender, ItemClickEventArgs e)
        {
            IHelper.ShowMatRSV(row_smp_det.MatId, DB.SkladBase());
        }

        private void barButtonItem1_ItemClick(object sender, ItemClickEventArgs e)
        {
            new frmKagentMaterilPrices().ShowDialog();
        }

        private void barButtonItem3_ItemClick(object sender, ItemClickEventArgs e)
        {
            var dataForReport = new Dictionary<string, IList>();

            var pl = _db.SettingMaterialPrices.Where(w => w.PTypeId == row_smp.PTypeId).OrderByDescending(o => o.OnDate).Take(1).Select(s => new { PriceTypesName = s.PriceTypes.Name, s.PTypeId, s.OnDate, s.Num }).ToList();
            var pl_d = _db.v_SummarySettingMaterialPrices.Where(w => w.PTypeId == row_smp.PTypeId).ToList();

            var mat_grp = pl_d.GroupBy(g => new { g.GrpNum, g.GrpName }).Select(s => new
            {
                s.Key.GrpName,
                s.Key.GrpNum
            }).OrderBy(o => o.GrpNum).ToList();

            List<object> realation = new List<object>();
            realation.Add(new
            {
                pk = "GrpName",
                fk = "GrpName",
                master_table = "MatGroup",
                child_table = "PriceListDet"
            });

            dataForReport.Add("PriceList", pl);
            dataForReport.Add("PriceListDet", pl_d);
            dataForReport.Add("MatGroup", mat_grp);
            dataForReport.Add("_realation_", realation);

            IHelper.Print(dataForReport, "SummarySettingMaterialPrices.xlsx");
        }

        private void CopyItemBtn_ItemClick(object sender, ItemClickEventArgs e)
        {
            CopyItem();
            GetData();
        }

        private void repositoryItemButtonEdit1_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            new frmMaterilPrices(row_smp.PTypeId).ShowDialog();
        }

        private void barButtonItem14_ItemClick(object sender, ItemClickEventArgs e)
        {
            ExportToExcel();
        }
    }
}
