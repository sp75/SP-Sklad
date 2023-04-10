using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.SqlServer;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media.Imaging;
using DevExpress.Data;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.TableLayout;
using DevExpress.XtraGrid.Views.Tile;
using DevExpress.XtraGrid.Views.Tile.ViewInfo;
using SP.Reports.Models.Views;
using SP_Sklad.Common;
using SP_Sklad.IntermediateWeighingInterface.Views;
using SP_Sklad.SkladData;
using SP_Sklad.WBDetForm;

namespace SP_Sklad.Reports
{
    public partial class frmReport52 : DevExpress.XtraEditors.XtraForm
    {
        private int _user_id { get; set; }

        public BaseEntities _db { get; set; }

        public frmReport52()
        {
            InitializeComponent();
            _db = new BaseEntities();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            spinEdit1.EditValue = DateTime.Now.Year;


            MatLookUpEdit.Properties.DataSource = new List<object>() { new MatComboBoxItem { MatId = 0, Name = "Усі" } }.Concat(new BaseEntities().Materials.Where(w => w.Deleted == 0).Select(s => new MatComboBoxItem { MatId = s.MatId, Name = s.Name, }).ToList());
            MatLookUpEdit.EditValue = 0;
            MatGroupLookUpEdit.Properties.DataSource = new List<MatGrpComboBoxItem>() { new MatGrpComboBoxItem { GrpId = 0, Name = "Усі" } }.Concat(new BaseEntities().MatGroup.Where(w => w.Deleted == 0).Select(s => new MatGrpComboBoxItem { GrpId = s.GrpId, Name = s.Name }).ToList());
            MatGroupLookUpEdit.EditValue = 0;


            foreach (var item in _db.Kagent.Where(w => w.Deleted == 0 && (w.Archived == 0 || w.Archived == null)).OrderBy(o => o.Name).Select(s => new
            {
                s.KaId,
                s.Name,
            }))
            {
                comboBoxEdit3.Properties.Items.Add(item.KaId, item.Name, CheckState.Unchecked, true);
            }

            foreach (var item in _db.KontragentGroup.OrderBy(o => o.Name))
            {
                comboBoxEdit31.Properties.Items.Add(item.Id, item.Name, CheckState.Unchecked, true);
            }
        }


        private void frmMainIntermediateWeighing_FormClosed(object sender, FormClosedEventArgs e)
        {

        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            int mat_id = (MatLookUpEdit.GetSelectedDataRow() as MatComboBoxItem).MatId;
            int grp_id = (MatGroupLookUpEdit.GetSelectedDataRow() as MatGrpComboBoxItem).GrpId;
            var ka_ids = string.Join(",", comboBoxEdit3.Properties.Items.Where(w => w.CheckState == CheckState.Checked).Select(s => Convert.ToString(s.Value)).ToList());
            var ka_grp_ids = string.Join(",", comboBoxEdit31.Properties.Items.Where(w => w.CheckState == CheckState.Checked).Select(s => Convert.ToString(s.Value)).ToList());

             chartControl1.Titles[1].Text = comboBoxEdit31.Text;

            REP_51BS.DataSource = _db.REP_51(Convert.ToInt32(spinEdit1.EditValue), ka_ids, ka_grp_ids, grp_id, mat_id);

            RepGridView.ExpandAllGroups();
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            chartControl1.ShowRibbonPrintPreview();
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            IHelper.ExportToXlsx(RepGridControl);
        }
    }
}
