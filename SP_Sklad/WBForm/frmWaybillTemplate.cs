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
using DevExpress.XtraTreeList;
using SP_Sklad.Common;
using SP_Sklad.Properties;
using SP_Sklad.Reports;
using SP_Sklad.SkladData;

namespace SP_Sklad.WBForm
{
    public partial class frmWaybillTemplate : DevExpress.XtraEditors.XtraForm
    {
        private Guid? _wbt_id { get; set; }
        public BaseEntities _db { get; set; }
        public int? doc_id { get; set; }
        private DbContextTransaction current_transaction { get; set; }
        private WaybillTemplate wbt { get; set; }

        public frmWaybillTemplate(Guid? wbt_id = null)
        {
            InitializeComponent();

            _wbt_id = wbt_id;

            _db = new BaseEntities();
            current_transaction = _db.Database.BeginTransaction();
        }

        private void frmPriceList_Load(object sender, EventArgs e)
        {
            PTypeEdit.Properties.DataSource = DB.SkladBase().PriceTypes.Select(s => new { s.PTypeId, s.Name }).ToList();

            repositoryItemLookUpEdit1.DataSource = DBHelper.WhList;

            if (_wbt_id == null)
            {
                wbt = _db.WaybillTemplate.Add(new WaybillTemplate
                {
                    Id = Guid.NewGuid(),
                });

                _db.SaveChanges();

                _wbt_id = wbt.Id;
            }
            else
            {
                wbt = _db.WaybillTemplate.Find(_wbt_id);
            }

            if (wbt != null)
            {
                WaybillTemplateBS.DataSource = wbt;
            }

            GetDetail();
            GetMatTree();
        }

        private void OkButton_Click(object sender, EventArgs e)
        {

            _db.SaveChanges();
            current_transaction.Commit();
            Close();
        }

        private void frmPriceList_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (current_transaction.UnderlyingTransaction.Connection != null)
            {
                current_transaction.Rollback();
            }

            _db.Dispose();
            current_transaction.Dispose();
        }

        private void GetMatTree()
        {

        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            GetMatTree();
        }

        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void treeList1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                TreeList treeList = sender as TreeList;
                TreeListHitInfo info = treeList.CalcHitInfo(e.Location);
                if (info.Node != null)
                {
                    treeList.FocusedNode = info.Node;
                }
            }
        }

        private void treeList1_PopupMenuShowing(object sender, PopupMenuShowingEventArgs e)
        {
            Point p2 = Control.MousePosition;
            TreePopupMenu.ShowPopup(p2);
        }

        void AddMat(GetMatTree_Result row)
        {
            
        }


        private void barButtonItem5_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        void GetDetail()
        {
            int top_row = PriceListGrid.TopRowIndex;
            WaybillTemplateDetBS.DataSource = _db.WaybillTemplateDet.Where(w=> w.WaybillTemplateId == _wbt_id).ToList();
            PriceListGrid.ExpandAllGroups();
            PriceListGrid.TopRowIndex = top_row;
        }

        private void gridView1_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
           
        }

        private void DelMaterialBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void barButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
           
        }

        private void barButtonItem9_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void PrevievBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void MatInfoBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var dr = PriceListGrid.GetFocusedRow() as GetPriceListDet_Result;
            IHelper.ShowMatInfo(dr.MatId);
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            Close();
        }

             
        private void AddMaterialBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
          
        }

        private void barButtonItem6_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
          
        }

        private void barButtonItem7_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
           
        }

        private void barButtonItem8_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
           
        }

        private void barButtonItem10_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void PriceListGrid_PopupMenuShowing(object sender, DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs e)
        {
            if (e.HitInfo.InRow)
            {
                Point p2 = Control.MousePosition;
                this.PriceListPopupMenu.ShowPopup(p2);
            }
        }

        private void barButtonItem11_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void PriceListPopupMenu_BeforePopup(object sender, CancelEventArgs e)
        {

        }


        private void PriceListGrid_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            if (e.RowHandle < 0)
            {
                return;
            }

            var wh_row = PriceListGrid.GetRow(e.RowHandle) as GetPriceListDet_Result;

            if (wh_row != null && wh_row.Price < Math.Max(wh_row.LastInPrice ?? 0, wh_row.LastOutPrice ?? 0))
            {
                e.Appearance.ForeColor = Color.Red;
            }
        }

        private void PTypeEdit_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if(e.Button.Index == 1)
            {
                PTypeEdit.EditValue = null;
            }
        }

        private void repositoryItemButtonEdit1_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
           
        }

        private void MatListSource_GetQueryable(object sender, DevExpress.Data.Linq.GetQueryableEventArgs e)
        {
            var mat = _db.v_Materials.Where(w=> w.Archived == 0).AsQueryable();

            e.QueryableSource = mat;
        }
    }
}
