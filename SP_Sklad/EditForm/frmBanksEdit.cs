using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SP_Sklad.Common;
using SP_Sklad.SkladData;

namespace SP_Sklad.EditForm
{
    public partial class frmBanksEdit : DevExpress.XtraEditors.XtraForm
    {
        private BaseEntities _db { get; set; }
        private DbContextTransaction current_transaction { get; set; }
        private int? _BankId { get; set; }
        private Banks bank { get; set; }
        private List<CatalogTreeList> tree { get; set; }

        public frmBanksEdit(int? BankId=null)
        {
            InitializeComponent();

            _BankId = BankId;
            _db = DB.SkladBase();
            current_transaction = _db.Database.BeginTransaction();
            tree = new List<CatalogTreeList>();
        }

        private void frmBanksEdit_Load(object sender, EventArgs e)
        {
            xtraTabControl1.ShowTabHeader = DevExpress.Utils.DefaultBoolean.False;

            tree.Add(new CatalogTreeList { Id = -2, ParentId = -2, Text = "Основна інформація", ImgIdx = 0, TabIdx = 0 });
            tree.Add(new CatalogTreeList { Id = 0, ParentId = -1, Text = "Контактні особи", ImgIdx = 1, TabIdx = 1 });

            TreeListBindingSource.DataSource = tree;

            if (_BankId == null)
            {
                bank = _db.Banks.Add(new Banks
                {
                    Deleted = 0,
                    Def = _db.Banks.Any(a => a.Def == 1) ? 0 : 1,
                    Name = "",
                    MFO = ""
                });

                _db.SaveChanges();

                Text = "Додати новий банк";
            }
            else
            {
                bank = _db.Banks.Find(_BankId);

                Text = "Властвості банку";
            }

            BanksBS.DataSource = bank;
            GetPersons();

            DefCheckBox.Enabled = !DefCheckBox.Checked;
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            _db.SaveChanges();
            current_transaction.Commit();
        }

        private void frmBanksEdit_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (current_transaction.UnderlyingTransaction.Connection != null)
            {
                current_transaction.Rollback();
            }

            _db.Dispose();
            current_transaction.Dispose();
        }

        private void GetPersons()
        {
            var list = _db.BanksPersons.Where(w => w.BankId == bank.BankId).Select(s => new
            {
                s.PersonId,
                s.Name,
                s.Job,
            }).ToList();
            BanksPersonsGridControl.DataSource = list;

            tree.RemoveAll(r => r.ParentId == 0);
            foreach (var item in list)
            {
                tree.Add(new CatalogTreeList
                {
                    Id = tree.Count + 1, 
                    ParentId = 0,
                    Text = item.Name,
                    ImgIdx = 2,
                    TabIdx = 2,
                    DataSetId = item.PersonId
                });
            }
            DirTreeList.RefreshDataSource();
            DirTreeList.ExpandAll();
        }

        private void DirTreeList_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
        {
            _db.SaveChanges();

            var focused_tree_node = DirTreeList.GetDataRecordByNode(e.Node) as CatalogTreeList;

            if (focused_tree_node.ParentId == 0)
            {
                BanksPersonsBS.DataSource = _db.BanksPersons.Find(focused_tree_node.DataSetId);
            }

            if (focused_tree_node.Id == 0)
            {
                var list = _db.BanksPersons.Where(w => w.BankId == bank.BankId).Select(s => new
                {
                    s.PersonId,
                    s.Name,
                    s.Job,
                }).ToList();
                BanksPersonsGridControl.DataSource = list;
            }

            xtraTabControl1.SelectedTabPageIndex = focused_tree_node.TabIdx;
        }

        private void AddRecDetBtn_Click(object sender, EventArgs e)
        {
            xtraTabControl1.SelectedTabPageIndex = 2;
            var new_det = _db.BanksPersons.Add(new BanksPersons { BankId = bank.BankId, Name = "" });
            BanksPersonsBS.DataSource = new_det;
            _db.SaveChanges();
            GetPersons();

            DirTreeList.FocusedNode = DirTreeList.GetNodeList().FirstOrDefault(w => Convert.ToInt32(w.GetValue("DataSetId")) == new_det.PersonId);

        }

        private void EditRecDetBtn_Click(object sender, EventArgs e)
        {
            dynamic det_item = BanksPersonsGridView.GetFocusedRow();
            if (det_item == null) return;

            xtraTabControl1.SelectedTabPageIndex = 2;
            BanksPersonsBS.DataSource = _db.BanksPersons.Find(det_item.PersonId);

            DirTreeList.FocusedNode = DirTreeList.GetNodeList().FirstOrDefault(w => Convert.ToInt32(w.GetValue("DataSetId")) == det_item.PersonId);
        }

        private void DelRecDetBtn_Click(object sender, EventArgs e)
        {
            dynamic det_item = BanksPersonsGridView.GetFocusedRow();
            if (det_item == null) return;

            _db.BanksPersons.Remove(_db.BanksPersons.Find(det_item.PersonId));
            _db.SaveChanges();

            GetPersons();
        }

        private void simpleButton7_Click(object sender, EventArgs e)
        {
            _db.BanksPersons.Remove(BanksPersonsBS.DataSource as BanksPersons);
            _db.SaveChanges();

            GetPersons();
            simpleButton11.PerformClick();
        }

        private void simpleButton11_Click(object sender, EventArgs e)
        {
            DirTreeList.FocusedNode = DirTreeList.GetNodeList().FirstOrDefault(w => Convert.ToInt32(w.GetValue("Id")) == 0);
        }

        private void BanksPersonsGridView_DoubleClick(object sender, EventArgs e)
        {
            EditRecDetBtn.PerformClick();
        }

        private void textEdit5_EditValueChanged(object sender, EventArgs e)
        {
            if (textEdit5.ContainsFocus)
            {
                DirTreeList.FocusedNode.SetValue("Text", textEdit5.EditValue);
            }
        }
    }
}
