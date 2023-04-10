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
using DevExpress.XtraCharts.Designer;
using DevExpress.XtraBars;

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
            barEditItem1.EditValue = DateTime.Now.Year;

            MatGroupLookUpEdit.DataSource = new List<MatGrpComboBoxItem>() { new MatGrpComboBoxItem { GrpId = 0, Name = "Усі" } }.Concat(new BaseEntities().MatGroup.Where(w => w.Deleted == 0).Select(s => new MatGrpComboBoxItem { GrpId = s.GrpId, Name = s.Name }).ToList());
            MatGrpEditItem.EditValue = 0;

            repositoryItemLookUpEdit2.DataSource = new List<object>() { new MatComboBoxItem { MatId = 0, Name = "Усі" } }.Concat(new BaseEntities().Materials.Where(w => w.Deleted == 0).Select(s => new MatComboBoxItem { MatId = s.MatId, Name = s.Name, }).ToList());
            MatEditItem.EditValue = 0;

            foreach (var item in _db.Kagent.Where(w => w.Deleted == 0 && (w.Archived == 0 || w.Archived == null)).OrderBy(o => o.Name).Select(s => new
            {
                s.KaId,
                s.Name,
            }))
            {
                repositoryItemCheckedComboBoxEdit1.Items.Add(item.KaId, item.Name, CheckState.Unchecked, true);
            }

            foreach (var item in _db.KontragentGroup.OrderBy(o => o.Name))
            {
                repositoryItemCheckedComboBoxEdit2.Items.Add(item.Id, item.Name, CheckState.Unchecked, true);
            }

        }


        private void barButtonItem1_ItemClick(object sender, ItemClickEventArgs e)
        {
            int mat_id = (repositoryItemLookUpEdit2.GetDataSourceRowByKeyValue(MatEditItem.EditValue) as MatComboBoxItem).MatId;
            int grp_id = (MatGroupLookUpEdit.GetDataSourceRowByKeyValue(MatGrpEditItem.EditValue) as MatGrpComboBoxItem).GrpId;
            var ka_ids = string.Join(",", repositoryItemCheckedComboBoxEdit1.Items.Where(w => w.CheckState == CheckState.Checked).Select(s => Convert.ToString(s.Value)).ToList());
            var ka_grp_ids = string.Join(",", repositoryItemCheckedComboBoxEdit2.Items.Where(w => w.CheckState == CheckState.Checked).Select(s => Convert.ToString(s.Value)).ToList());

           
            //chartControl1.Titles[1].Text = repositoryItemCheckedComboBoxEdit2  as string;

            REP_51BS.DataSource = _db.REP_51(Convert.ToInt32(barEditItem1.EditValue), ka_ids, ka_grp_ids, grp_id, mat_id);
        }
    }
}