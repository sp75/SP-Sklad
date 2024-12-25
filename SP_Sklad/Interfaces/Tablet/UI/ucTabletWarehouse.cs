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
using System.ComponentModel.DataAnnotations;
using SP.Reports;
using System.IO;
using System.Collections;
using SP_Sklad.SkladData;
using OpenStore.Tranzit.Base;
using SP_Sklad.Common;
using SkladEngine.DBFunction;
using SkladEngine.DBFunction.Models;

namespace SP_Sklad.Interfaces.Tablet.UI
{
    public partial class ucTabletWarehouse : DevExpress.XtraEditors.XtraUserControl
    {
        public ucTabletWarehouse()
        {
            InitializeComponent();

            windowsUIButtonPanel.BackColor = new Color();
        }

        void windowsUIButtonPanel_ButtonClick(object sender, DevExpress.XtraBars.Docking2010.ButtonEventArgs e)
        {
            if (e.Button.Properties.VisibleIndex == 0)
            {
                RefreshGrid();
            }
            else if(e.Button.Properties.VisibleIndex == 1)
            {
                IHelper.ExportToXlsx(gridControl1);
            }

        }

        private List<GetMaterialsOnWh_Result> GetDataSource()
        {
            var area = KagentList.GetSelectedDataRow() as Kontragent;
            string wids = "";

            if(area.KaId == -1)
            {
                wids = string.Join(", ", DBHelper.EmployeeKagentList.Where(w => w.WId != null).Select(s => s.WId).ToList());
            }
            else
            {
                wids = area.WId.ToString();
            }

            var list = new MaterialRemain(UserSession.UserId).GetMaterialsOnWh(string.Join(", ", wids));

            return list;
        }

        public void RefreshGrid()
        {
            var h = bandedGridView1.FocusedRowHandle;
            var top_r = bandedGridView1.TopRowIndex;

            gridControl1.DataSource = GetDataSource();
            //     gridView1.ExpandAllGroups();

            bandedGridView1.TopRowIndex = top_r;
            bandedGridView1.FocusedRowHandle = h;
        }

        private void ucCurrentReturned_Load(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                KagentList.Properties.DataSource = DBHelper.EmployeeKagentList;
                KagentList.EditValue = -1;

                var user_settings = new UserSettingsRepository(DBHelper.CurrentUser.UserId, new BaseEntities());
                bandedGridView1.Appearance.Row.Font = new Font(user_settings.GridFontName, (float)user_settings.GridFontSize);

                RefreshGrid();
            }
        }
       

        private void KagentList_EditValueChanged(object sender, EventArgs e)
        {
            if (KagentList.ContainsFocus)
            {
                RefreshGrid();
            }
        }
    }
}
