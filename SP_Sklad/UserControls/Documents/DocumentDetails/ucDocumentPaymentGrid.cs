using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using SP_Sklad.SkladData;
using SP_Sklad.Common;
using SP_Sklad.Reports;

namespace SP_Sklad.UserControls
{
    public partial class ucDocumentPaymentGrid : DevExpress.XtraEditors.XtraUserControl
    {
        private v_PayDoc focused_row => DocumentPaymentGridView.GetFocusedRow() as v_PayDoc;

        public ucDocumentPaymentGrid()
        {
            InitializeComponent();
        }

        private void ucRelDocGrid_Load(object sender, EventArgs e)
        {
            ;
        }

        public void GetPaymentDoc(Guid? id)
        {
            var _db = new BaseEntities();
            DocumentPaymentGridControl.DataSource = _db.DocRels.Where(w => w.OriginatorId == id)
                        .Join(_db.v_PayDoc, drel => drel.RelOriginatorId, pd => pd.Id, (drel, pd) => pd).OrderBy(o => o.OnDate).ToList();
        }

        private void RelDocGridView_PopupMenuShowing(object sender, DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs e)
        {
            if (e.HitInfo.InRow)
            {
                Point p2 = Control.MousePosition;
                BottomPopupMenu.ShowPopup(p2);
            }
        }

        private void MoveToDocBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FindDoc.Find(focused_row.Id, focused_row.DocType, focused_row.OnDate);
        }

        private void PrintDocBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            PrintDoc.Show(focused_row.Id, focused_row.DocType, DB.SkladBase());
        }
    }
}
