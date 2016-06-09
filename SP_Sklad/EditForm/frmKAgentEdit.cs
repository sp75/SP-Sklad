using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SP_Sklad.Common;
using SP_Sklad.SkladData;

namespace SP_Sklad.EditForm
{
    public partial class frmKAgentEdit : Form
    {
        int? _ka_id { get; set; }
        private Kagent _ka { get; set; }
        BaseEntities _db { get; set; }
        KAgentSaldo k_saldo { get; set; }

        public frmKAgentEdit(int? KaId = null)
        {
            InitializeComponent();
            _ka_id = KaId;
            _db = DB.SkladBase();
        }

        private void frmKAgentEdit_Load(object sender, EventArgs e)
        {
            xtraTabControl1.ShowTabHeader = DevExpress.Utils.DefaultBoolean.False;

            var tree = new List<CatalogTreeList>();
            tree.Add(new CatalogTreeList { Id = 0, ParentId = 255, Text = "Основна інформація", ImgIdx = 0, TabIdx = 0 });
            tree.Add(new CatalogTreeList { Id = 1, ParentId = 255, Text = "Договір", ImgIdx = 10, TabIdx = 12 });
            tree.Add(new CatalogTreeList { Id = 2, ParentId = 255, Text = "Документ", ImgIdx = 5, TabIdx = 1 });
            tree.Add(new CatalogTreeList { Id = 3, ParentId = 255, Text = "Додаткова інформація", ImgIdx = 0, TabIdx = 2 });
            tree.Add(new CatalogTreeList { Id = 4, ParentId = 255, Text = "Знижки", ImgIdx = 1, TabIdx = 3 });
            tree.Add(new CatalogTreeList { Id = 5, ParentId = 255, Text = "Контактна інформація", ImgIdx = 2, TabIdx = 4 });
            tree.Add(new CatalogTreeList { Id = 6, ParentId = 5, Text = "Контактні особи", ImgIdx = 2, TabIdx = 5 });
            tree.Add(new CatalogTreeList { Id = 7, ParentId = 255, Text = "Рахунки", ImgIdx = 3, TabIdx = 6 });
            tree.Add(new CatalogTreeList { Id = 8, ParentId = 255, Text = "Примітка", ImgIdx = 4, TabIdx = 7 });
            DirTreeList.DataSource = tree;


            if (_ka_id == null)
            {
                _ka = _db.Kagent.Add(new Kagent()
                {
                     KaKind = 0,

                  /*  Archived = 0,
                    Serials = 0,
                    MId = DBHelper.MeasuresList.FirstOrDefault(w => w.Def == 1).MId,
                    WId = DBHelper.WhList().FirstOrDefault(w => w.Def == 1).WId,
                    CId = DBHelper.CountersList.FirstOrDefault(w => w.Def == 1).CId,
                    NDS = 0*/
                });
            }
            else
            {
                _ka = _db.Kagent.Find(_ka_id);
            }

            if (_ka != null)
            {
                k_saldo = DB.SkladBase().KAgentSaldo.Where(w => w.KAID == _ka.KaId).OrderBy(d => d.ONDATE).FirstOrDefault();
                KTypeLookUpEdit.Properties.DataSource = DB.SkladBase().KAgentTyp.ToList();
                KaKindLookUpEdit.Properties.DataSource = DB.SkladBase().KAKInd.ToList();

             //   GrpIdEdit.Properties.TreeList.DataSource = DB.SkladBase().MatGroup.Select(s => new { s.GrpId, s.PId, s.Name }).ToList();
          //      MsrComboBox.Properties.DataSource = DBHelper.MeasuresList;
           //     WIdLookUpEdit.Properties.DataSource = DBHelper.WhList();
           //     ProducerLookUpEdit.Properties.DataSource = DB.SkladBase().Materials.Select(s => new { s.Producer }).Distinct().ToList();
           //     CIdLookUpEdit.Properties.DataSource = DBHelper.CountersList;

                bindingSource1.DataSource = _ka;
            }
        }

        private void KTypeLookUpEdit_EditValueChanged(object sender, EventArgs e)
        {
            if (KTypeLookUpEdit.EditValue == null || KTypeLookUpEdit.EditValue == DBNull.Value)
            {
                return;
            }
                KaKindLookUpEdit.Enabled = true;
             //   cxCheckBox1->Enabled = KAgentSaldo->IsEmpty();
          //      cxCheckBox1->Checked = (!KAgentSTARTSALDODATE->IsNull);
           //     cxCheckBox1PropertiesChange(Sender);
                switch ((int)KTypeLookUpEdit.EditValue)
                {
                    case 0: xtraTabControl2.SelectedTabPageIndex = 0;
                        if (_ka.KaKind == 3 || _ka.KaKind == 4 || _ka.KaKind == 0) _ka.KaKind = 0;
                        break;
                    case 1: xtraTabControl2.SelectedTabPageIndex = 1;
                        if (_ka.KaKind == 3 || _ka.KaKind == 4 || _ka.KaKind == 0) _ka.KaKind = 0;
                        break;
                    case 2: xtraTabControl2.SelectedTabPageIndex = 2;
                        _ka.KaKind = 3;
                        KaKindLookUpEdit.Enabled = false;
                        break;
                    case 3: xtraTabControl2.SelectedTabPageIndex = 0;
                        _ka.KaKind = 4;
                        KaKindLookUpEdit.Enabled = false;

                    /*    if (k_saldo == null) checkEdit2.Checked = false;
                        checkEdit2.Enabled = false;*/
                        break;
                }
        }

        private void DirTreeList_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
        {
            var focused_tree_node = DirTreeList.GetDataRecordByNode(e.Node) as CatalogTreeList;

            xtraTabControl1.SelectedTabPageIndex = focused_tree_node.TabIdx;
        }
    }
}
