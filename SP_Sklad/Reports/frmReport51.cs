using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using SP_Sklad.SkladData;
using DevExpress.XtraGrid.Views.Grid;
using System.IO;
using SP.Reports.Models.Views;

namespace SP_Sklad.ViewsForm
{
    public partial class frmReport51 : DevExpress.XtraEditors.XtraForm
    {
        private BaseEntities _db { get; set; }

        public frmReport51()
        {
            InitializeComponent();
            _db = DB.SkladBase();
        }

        private void frmMaterialPriceHIstory_Load(object sender, EventArgs e)
        {
            YearEdit3.Value = DateTime.Now.Year;
            GrpComboBox.Properties.DataSource = new List<GrpComboBoxItem>() { new GrpComboBoxItem { GrpId = 0, Name = "Усі" } }.Concat(new BaseEntities().MatGroup.Where(w => w.Deleted == 0).Select(s => new GrpComboBoxItem { GrpId = s.GrpId, Name = s.Name }).ToList());
            GrpComboBox.EditValue = 0;

            foreach (var item in _db.Kagent.Where(w => w.Deleted == 0 && (w.Archived == 0 || w.Archived == null)).OrderBy(o => o.Name).Select(s => new
            {
                s.KaId,
                s.Name,
            }))
            {
                checkedComboBoxEdit1.Properties.Items.Add(item.KaId, item.Name,  CheckState.Unchecked, true);
            }

            foreach (var item in _db.KontragentGroup.OrderBy(o => o.Name))
            {
                checkedComboBoxEdit2.Properties.Items.Add(item.Id, item.Name, CheckState.Unchecked, true);
            }
            

       //     Text += " " + mat.Name;
         //   chartControl1.Titles[0].Text = Text;
      

        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            chartControl1.ShowRibbonPrintPreview();
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            int grp_id = (GrpComboBox.GetSelectedDataRow() as GrpComboBoxItem).GrpId;
            var ka_ids = string.Join(",", checkedComboBoxEdit1.Properties.Items.Where(w => w.CheckState == CheckState.Checked).Select(s => Convert.ToString(s.Value)).ToList());
            var ka_grp_ids = string.Join(",", checkedComboBoxEdit2.Properties.Items.Where(w => w.CheckState == CheckState.Checked).Select(s => Convert.ToString(s.Value)).ToList());

            REP_51BS.DataSource = _db.REP_51((int)YearEdit3.Value, ka_ids, ka_grp_ids, grp_id);
        }
    }
}