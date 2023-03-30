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

namespace SP_Sklad.UserControls
{
    public partial class SettingMaterialPricesUserControl : DevExpress.XtraEditors.XtraUserControl
    {
        BaseEntities _db { get; set; }
        public BarButtonItem EditBtn { get; set; }
        public BarButtonItem DeleteBtn { get; set; }
        public BarButtonItem ExecuteBtn { get; set; }
        public BarButtonItem CopyBtn { get; set; }
        public BarButtonItem PrintBtn { get; set; }

        public v_SettingMaterialPrices row_smp => SettingMaterialPricesGridView.GetFocusedRow() as v_SettingMaterialPrices;
        private UserAccess user_access { get; set; }

        public SettingMaterialPricesUserControl()
        {
            InitializeComponent();
        }

        public void GetData()
        {
            int rIndex2 = SettingMaterialPricesGridView.GetVisibleIndex(SettingMaterialPricesGridView.FocusedRowHandle);
            SettingMaterialPricesBS.DataSource = DB.SkladBase().v_SettingMaterialPrices.ToList();
            SettingMaterialPricesGridView.TopRowIndex = rIndex2;

            GetDetData();
        }

        private void SettingMaterialPricesGridView_FocusedRowObjectChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventArgs e)
        {
            DeleteBtn.Enabled = (row_smp != null && row_smp.Checked == 0 && user_access.CanDelete == 1);
            ExecuteBtn.Enabled = (row_smp != null && user_access.CanPost == 1);
            EditBtn.Enabled = (row_smp != null && row_smp.Checked == 0 && user_access.CanModify == 1);
            CopyBtn.Enabled = (row_smp != null && user_access.CanModify == 1); 
            PrintBtn.Enabled = (row_smp != null);

            GetDetData();
        }

        private void GetDetData()
        {
            if (row_smp != null)
            {
                SettingMaterialPricesDetBS.DataSource = DB.SkladBase().v_SettingMaterialPricesDet.Where(w => w.SettingMaterialPricesId == row_smp.Id).ToList();
            }
            else
            {
                SettingMaterialPricesDetBS.DataSource = null;
            }
        }

        private void SettingMaterialPricesUserControl_Load(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                _db = new BaseEntities();
                user_access = _db.UserAccess.FirstOrDefault(w => w.FunId == 97 && w.UserId == UserSession.UserId);
            }
        }

        private void SettingMaterialPricesGridView_DoubleClick(object sender, EventArgs e)
        {
            EditBtn.PerformClick();
        }
    }
}
