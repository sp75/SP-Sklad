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
    public partial class ucRelDocGrid : DevExpress.XtraEditors.XtraUserControl
    {
        private GetRelDocList_Result rel_row => RelDocGridView.GetFocusedRow() as GetRelDocList_Result;

        public ucRelDocGrid()
        {
            InitializeComponent();
        }

        private void ucRelDocGrid_Load(object sender, EventArgs e)
        {
            ;
        }

        public void GetRelDoc(Guid? id)
        {
            GetRelDocListBS.DataSource = new BaseEntities().GetRelDocList(id).OrderBy(o => o.OnDate).ToList();
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
            FindDoc.Find(rel_row.Id, rel_row.DocType, rel_row.OnDate);
        }

        private void PrintDocBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            PrintDoc.Show(rel_row.Id, rel_row.DocType.Value, DB.SkladBase());
        }
    }
}
