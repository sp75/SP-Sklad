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
    public partial class frmKAgentEdit : Form
    {
        int? _ka_id { get; set; }
        private Kagent _ka { get; set; }
        private BaseEntities _db { get; set; }
        private KAgentSaldo k_saldo { get; set; }
        private KADiscount k_discount { get; set; }
        private DbContextTransaction current_transaction { get; set; }

        public frmKAgentEdit(int? KaId = null)
        {
            InitializeComponent();
            _ka_id = KaId;
            _db = DB.SkladBase();
            current_transaction = _db.Database.BeginTransaction();
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
                    KType = 0,
                });

                _db.SaveChanges();
            }
            else
            {
                _ka = _db.Kagent.Find(_ka_id);
            }

            if (_ka != null)
            {
                k_saldo = _db.KAgentSaldo.Where(w => w.KAId == _ka.KaId).OrderBy(d => d.OnDate).FirstOrDefault();
                checkEdit2.Enabled = k_saldo == null;
                checkEdit2.Checked = _ka.StartSaldoDate != null;
                panel7.Enabled = checkEdit2.Checked;

                k_discount = _db.KADiscount.FirstOrDefault(w => w.KAId == _ka.KaId);
                DiscCheckEdit.Checked = k_discount != null;
                DiscPanel.Enabled = DiscCheckEdit.Checked;
             //   DiscountGridControl.DataSource = _db.DiscountList(_ka.KaId);

                KTypeLookUpEdit.Properties.DataSource = DB.SkladBase().KAgentTyp.ToList();
                KaKindLookUpEdit.Properties.DataSource = DB.SkladBase().KAKInd.ToList();
                PTypeEdit.Properties.DataSource = DB.SkladBase().PriceTypes.Select(s => new { s.PTypeId, s.Name }).ToList();
                MatLookUpEdit.Properties.DataSource = DB.SkladBase().MaterialsList.ToList();

                //   GrpIdEdit.Properties.TreeList.DataSource = DB.SkladBase().MatGroup.Select(s => new { s.GrpId, s.PId, s.Name }).ToList();
                //     ProducerLookUpEdit.Properties.DataSource = DB.SkladBase().Materials.Select(s => new { s.Producer }).Distinct().ToList();
                //     CIdLookUpEdit.Properties.DataSource = DBHelper.CountersList;

                KagentBindingSource.DataSource = _ka;
                if (k_discount != null)
                {
                    DiscountBindingSource.DataSource = k_discount;
                }
            }
        }

        private void KTypeLookUpEdit_EditValueChanged(object sender, EventArgs e)
        {
            if (KTypeLookUpEdit.EditValue == null || KTypeLookUpEdit.EditValue == DBNull.Value)
            {
                return;
            }
            KaKindLookUpEdit.Enabled = true;
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
                    break;
            }
            KaKindLookUpEdit.EditValue = _ka.KaKind;
        }

        private void DirTreeList_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
        {
            var focused_tree_node = DirTreeList.GetDataRecordByNode(e.Node) as CatalogTreeList;

            if (_ka != null)
            {
                _db.SaveChanges();
                DiscountGridControl.DataSource = _db.DiscountList(_ka.KaId);
            }

            xtraTabControl1.SelectedTabPageIndex = focused_tree_node.TabIdx;
        }

        private void checkEdit2_CheckedChanged(object sender, EventArgs e)
        {
            panel7.Enabled = checkEdit2.Checked;
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            if (k_discount != null && !DiscCheckEdit.Checked)
            {
                _db.KADiscount.Remove(k_discount);
            }
            _db.SaveChanges();
            current_transaction.Commit();
        }

        private void simpleButton6_Click(object sender, EventArgs e)
        {
            PTypeEdit.EditValue = null;
            _ka.PTypeId = null;
        }

        private void DiscCheckEdit_CheckedChanged(object sender, EventArgs e)
        {
            DiscPanel.Enabled = DiscCheckEdit.Checked;
            if (DiscCheckEdit.Checked && k_discount == null)
            {
                k_discount = _db.KADiscount.Add(new KADiscount { KAId = _ka.KaId, DiscCustom = 0, DiscForAll = 0, OnValue = 0 });
                DiscountBindingSource.DataSource = k_discount;
                _db.SaveChanges();
            }
        }

        private void frmKAgentEdit_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (current_transaction.UnderlyingTransaction.Connection != null)
            {
                current_transaction.Rollback();
            }

            _db.Dispose();
            current_transaction.Dispose();
        }

        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            xtraTabControl1.SelectedTabPageIndex = 10;
            var mat_disc = _db.KAMatDiscount.Add(new KAMatDiscount { DiscId = Guid.NewGuid(), KAId = _ka.KaId, OnValue = 0, MatId = DB.SkladBase().MaterialsList.FirstOrDefault().MatId });
            MatDiscountDS.DataSource = mat_disc;

            /*   DiscountList->Append();
               DiscountListIMG_IDX->Value = 0;
               MemTableEh1->Append();
               MemTableEh1ImgIdx->Value = 7;
               MemTableEh1Parent_ID->Value = 3;
               //	  MemTableEh1Text->Value = "("+DiscountListONVALUE->AsString+"%) "+DiscountListNAME->Value ;
               MemTableEh1TabIdx->Value = 10;
               MemTableEh1DataSetID->Value = DiscountListDISCID->Value;
               MemTableEh1->Post();*/
        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            xtraTabControl1.SelectedTabPageIndex = 3;
        }

        private void checkEdit5_CheckedChanged(object sender, EventArgs e)
        {
            panelControl6.Enabled = checkEdit5.Checked;
        }

        private void simpleButton7_Click(object sender, EventArgs e)
        {
            var row = DiscountGridView.GetFocusedRow() as DiscountList_Result;
            if (row == null) return;

            if (row.ImgIdx == 0)
            {
                MatDiscountDS.DataSource = _db.KAMatDiscount.FirstOrDefault(w => w.DiscId == row.DiscId);
                xtraTabControl1.SelectedTabPageIndex = 10;
            }
            else
            {
                xtraTabControl1.SelectedTabPageIndex = 11;
            }
        }
    }
}
