using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.Data;
using SP_Sklad.Common;
using SP_Sklad.Reports;
using SP_Sklad.SkladData;

namespace SP_Sklad
{
    public partial class frmIncompleteUserDoc : DevExpress.XtraEditors.XtraForm
    {
        public int? _ka_id { get; set; }

        public v_UserDocs focused_kagent => DocumentGridView.GetFocusedRow() is NotLoadedObject ? null : DocumentGridView.GetFocusedRow() as v_UserDocs;

        public frmIncompleteUserDoc(int? KaId)
        {
            InitializeComponent();
            _ka_id = KaId;
        }


        private void frmKABalans_Load(object sender, EventArgs e)
        {
            wTypeList.Properties.DataSource = new List<object>() { new { Id = 0, Name = "Усі" } }.Concat(new BaseEntities().DocType.Select(s => new { s.Id, s.Name })).ToList();
            wTypeList.EditValue = 0;

            wbStartDate.DateTime = DateTimeDayOfMonthExtensions.FirstDayOfMonth(DateTime.Now);
            wbEndDate.DateTime = DateTime.Now.AddDays(1);

            //    GetBalans();
        }

        private void GetData()
        {
            DocumentGridControl.DataSource = null;
            DocumentGridControl.DataSource = PersonDocListSource;
        }

        private void wbStartDate_EditValueChanged(object sender, EventArgs e)
        {
            if (wTypeList.ContainsFocus || wbStartDate.ContainsFocus || wbEndDate.ContainsFocus)
            {
                //        GetBalans();
            }
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            IHelper.ExportToXlsx(DocumentGridControl);
        }

        private void bandedGridView1_PopupMenuShowing(object sender, DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs e)
        {
            if (e.HitInfo.InRow)
            {
                popupMenu1.ShowPopup(Control.MousePosition);
            }
        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var dir = DocumentGridView.GetFocusedRow() as GetDocList_Result;
            PrintDoc.Show(focused_kagent.DocId, focused_kagent.DocType.Value, DB.SkladBase());
        }

        private void barButtonItem5_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            if (focused_kagent != null)
            {
                FindDoc.Find(focused_kagent.DocId, focused_kagent.DocType, focused_kagent.OnDate);
            }
        }

        private void PersonDocListSource_GetQueryable(object sender, DevExpress.Data.Linq.GetQueryableEventArgs e)
        {
            var db = DB.SkladBase();

         //   int w_type = Convert.ToInt32(wTypeList.EditValue);

            e.QueryableSource = db.v_UserDocs.Where(w => w.PersonId == _ka_id && w.Checked == 0 /*&& (w_type == 0 || w.DocType == w_type)*/);

            e.Tag = db;
        }

        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            GetData();
        }
    }
}
