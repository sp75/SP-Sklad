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
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid;


namespace SP_Sklad.MainTabs
{
    public partial class ManufacturingUserControl : UserControl
    {
        public ManufacturingUserControl()
        {
            InitializeComponent();
        }

        private void wbStartDate_Properties_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void ManufacturingUserControl_Load(object sender, EventArgs e)
        {
         /*   whContentTab.ShowTabHeader = DevExpress.Utils.DefaultBoolean.False;
            OnDateEdit.DateTime = DateTime.Now;

            wbKagentList.Properties.DataSource = new List<object>() { new { KaId = 0, Name = "Усі" } }.Concat(new BaseEntities().Kagent.Select(s => new { s.KaId, s.Name }));
            wbKagentList.EditValue = 0;

            WhComboBox.Properties.DataSource = new List<object>() { new { WId = "*", Name = "Усі" } }.Concat(new BaseEntities().Warehouse.Select(s => new { WId = s.WId.ToString(), s.Name }).ToList());
            WhComboBox.EditValue = "*";

            wbSatusList.Properties.DataSource = new List<object>() { new { Id = -1, Name = "Усі" }, new { Id = 1, Name = "Проведені" }, new { Id = 0, Name = "Непроведені" } };
            wbSatusList.EditValue = -1;

            wbStartDate.EditValue = DateTime.Now.AddDays(-30);
            wbEndDate.EditValue = DateTime.Now;*/

            DocsTreeList.DataSource = DB.SkladBase().GetManufactureTree(DBHelper.CurrentUser.UserId).ToList();
            DocsTreeList.ExpandAll();
        }
        void GetWayBillList(int wtyp)
        {
            if (wbSatusList.EditValue == null || wbKagentList.EditValue == null || DocsTreeList.FocusedNode == null)
            {
                return;
            }

            var satrt_date = wbStartDate.DateTime < DateTime.Now.AddYears(-100) ? DateTime.Now.AddYears(-100) : wbStartDate.DateTime;
            var end_date = wbEndDate.DateTime < DateTime.Now.AddYears(-100) ? DateTime.Now.AddYears(100) : wbEndDate.DateTime;

            var dr = WbGridView.GetRow(WbGridView.FocusedRowHandle) as GetWayBillList_Result;

            WBGridControl.DataSource = null;
            WBGridControl.DataSource = DB.SkladBase().GetWayBillList(satrt_date.Date, end_date.Date.AddDays(1), wtyp, (int)wbSatusList.EditValue, (int)wbKagentList.EditValue, show_null_balance, "*", 0).OrderByDescending(o => o.OnDate);

            WbGridView.FocusedRowHandle = FindRowHandleByRowObject(WbGridView, dr);
        }

        private int FindPayDocRow(GridView view, GetPayDocList_Result dr)
        {
            if (dr != null)
            {
                for (int i = 0; i < view.DataRowCount; i++)
                {
                    if (dr.PayDocId == (view.GetRow(i) as GetPayDocList_Result).PayDocId)
                    {
                        return i;
                    }
                }
            }
            return GridControl.InvalidRowHandle;
        }

        private int FindRowHandleByRowObject(GridView view, GetWayBillList_Result dr)
        {
            if (dr != null)
            {
                for (int i = 0; i < view.DataRowCount; i++)
                {
                    if (dr.WbillId == (view.GetRow(i) as GetWayBillList_Result).WbillId)
                    {
                        return i;
                    }
                }
            }
            return GridControl.InvalidRowHandle;
        }
    }
}
