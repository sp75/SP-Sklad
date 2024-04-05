using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SP_Sklad.SkladData;
using SP_Sklad.EditForm;
using SP_Sklad.Common;
using DevExpress.XtraTreeList;
using System.IO;
using System.Diagnostics;
using SP_Sklad.Properties;
using DevExpress.XtraTreeList.Nodes;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.Data;
using SkladEngine.DBFunction;
using SP_Sklad.WBForm;
using DevExpress.XtraGrid;

namespace SP_Sklad.MainTabs
{
    public partial class ucServices : DevExpress.XtraEditors.XtraUserControl
    {
        public int? GrpId { get; set; }
        public v_Services svc_row => ServicesGridView.GetFocusedRow() as v_Services;

        public ucServices()
        {
            InitializeComponent();
        }

        private void DirectoriesUserControl_Load(object sender, EventArgs e)
        {

            if (!DesignMode)
            {

            }
        }

        public void GetData(bool restore = true)
        {
            if (GrpId > 0)
            {
                ServicesBS.DataSource = DB.SkladBase().v_Services.AsNoTracking().Where(w => w.GrpId == GrpId).ToList();
            }
            else
            {
                ServicesBS.DataSource = DB.SkladBase().v_Services.AsNoTracking().ToList();
            }
        }

        private void RefrechItemBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            GetData();
        }

        private void EditItemBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DialogResult result = DialogResult.Cancel;

          
            if (svc_row != null)
            {
                result = new frmServicesEdit(svc_row.SvcId).ShowDialog();
            }


            if (result == DialogResult.OK)
            {
                RefrechItemBtn.PerformClick();
            }
        }

        private void NewItemBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (DB.SkladBase().SvcGroup.Any())
            {
                using (var svc_edit = new frmServicesEdit(SvcGrp: GrpId > 0 ? GrpId : DB.SkladBase().SvcGroup.First()?.GrpId))
                {
                    svc_edit.ShowDialog();
                }
            }

            RefrechItemBtn.PerformClick();
        }

        private void DeleteItemBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (MessageBox.Show("Ви дійсно бажаєте відалити цей запис з довідника?", "Підтвердіть видалення", MessageBoxButtons.YesNo, MessageBoxIcon.Information) != DialogResult.Yes)
            {
                return;
            }

            using (var db = DB.SkladBase())
            {
                var svc_row = ServicesGridView.GetFocusedRow() as v_Services;

                var svc = db.Services.Find(svc_row.SvcId);
                if (svc != null)
                {
                    svc.Deleted = 1;
                }
                db.SaveChanges();
            }
            RefrechItemBtn.PerformClick();
        }

        private void CopyItemBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ;
        }

        private void barButtonItem11_ItemClick_1(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            IHelper.ExportToXlsx(ServicesGridControl);
        }

        private void ServicesGridView_DoubleClick(object sender, EventArgs e)
        {
            EditItemBtn.PerformClick();
        }

    }
}
