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
using SP_Sklad.ViewsForm;

namespace SP_Sklad.EditForm
{
    public partial class frmKAgentEdit : DevExpress.XtraEditors.XtraForm
    {
        int? _ka_id { get; set; }
        int? _k_type { get; set; }
        private Kagent _ka { get; set; }
        private BaseEntities _db { get; set; }
        private KAgentSaldo k_saldo { get; set; }
        private KADiscount k_discount { get; set; }
        private DbContextTransaction current_transaction { get; set; }
        private List<CatalogTreeList> tree { get; set; }

        public frmKAgentEdit(int? KType =null, int? KaId = null)
        {
            InitializeComponent();
            _ka_id = KaId;
            _k_type = KType;
            _db = DB.SkladBase();
            current_transaction = _db.Database.BeginTransaction();
            tree = new List<CatalogTreeList>();
        }

        private void frmKAgentEdit_Load(object sender, EventArgs e)
        {
            xtraTabControl1.ShowTabHeader = DevExpress.Utils.DefaultBoolean.False;

            tree.Add(new CatalogTreeList { Id = 0, ParentId = 255, Text = "Основна інформація", ImgIdx = 0, TabIdx = 0 });
            tree.Add(new CatalogTreeList { Id = 1, ParentId = 255, Text = "Договір", ImgIdx = 10, TabIdx = 12 });
            tree.Add(new CatalogTreeList { Id = 2, ParentId = 255, Text = "Документ", ImgIdx = 5, TabIdx = 1 });
            tree.Add(new CatalogTreeList { Id = 3, ParentId = 255, Text = "Додаткова інформація", ImgIdx = 0, TabIdx = 2 });
            tree.Add(new CatalogTreeList { Id = 4, ParentId = 255, Text = "Знижки", ImgIdx = 1, TabIdx = 3 });
            tree.Add(new CatalogTreeList { Id = 5, ParentId = 255, Text = "Контактна інформація", ImgIdx = 2, TabIdx = 4 });
            tree.Add(new CatalogTreeList { Id = 6, ParentId = 5, Text = "Контактні особи", ImgIdx = 2, TabIdx = 5 });
            tree.Add(new CatalogTreeList { Id = 7, ParentId = 255, Text = "Рахунки", ImgIdx = 3, TabIdx = 6 });
            tree.Add(new CatalogTreeList { Id = 8, ParentId = 255, Text = "Примітка", ImgIdx = 4, TabIdx = 7 });
            TreeListBS.DataSource = tree;


            if (_ka_id == null)
            {
                _ka = _db.Kagent.Add(new Kagent()
                {
                    Name = "",
                    KaKind = 0,
                    KType = _k_type.Value,
                    NdsPayer = 0,
                    Deleted = 0,
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
                StartSaldoPanel.Enabled = (checkEdit2.Checked && checkEdit2.Enabled);

                k_discount = _db.KADiscount.FirstOrDefault(w => w.KAId == _ka.KaId);
                DiscCheckEdit.Checked = k_discount != null;
                DiscPanel.Enabled = DiscCheckEdit.Checked;

                KTypeLookUpEdit.Properties.DataSource = DB.SkladBase().KAgentTyp.ToList();
                KaKindLookUpEdit.Properties.DataSource = DB.SkladBase().KAKInd.ToList();
                PTypeEdit.Properties.DataSource = DB.SkladBase().PriceTypes.Select(s => new { s.PTypeId, s.Name }).ToList();
                MatLookUpEdit.Properties.DataSource = DB.SkladBase().MaterialsList.ToList();
                GroupLookUpEdit.Properties.DataSource = DB.SkladBase().MatGroup.ToList();
                UsersLookUpEdit.Properties.DataSource = DB.SkladBase().Users.ToList().Where(w => !w.Kagent.Any() || w.UserId == _ka.UserId).ToList();
                JobLookUpEdit.Properties.Items.AddRange(DB.SkladBase().KAgentPersons.Where(w => w.JobType == 2).Select(s => s.Post).Distinct().ToList());
                lookUpEdit3.Properties.DataSource = DB.SkladBase().Jobs.AsNoTracking().ToList();

                lookUpEdit1.Properties.DataSource = DB.SkladBase().AccountType.Select(s => new { s.TypeId, s.Name }).ToList();
                lookUpEdit2.Properties.DataSource = DB.SkladBase().Banks.Select(s => new { s.BankId, s.Name }).ToList();

                UCityTypeEdit.Properties.DataSource = DB.SkladBase().CityType.ToList();
                FCityTypeEdit.Properties.DataSource = UCityTypeEdit.Properties.DataSource;

                RouteLookUpEdit.Properties.DataSource = DB.SkladBase().Routes.AsNoTracking().ToList();
                KaGroupLookUpEdit.Properties.DataSource = DB.SkladBase().KontragentGroup.AsNoTracking().ToList();

                //   GrpIdEdit.Properties.TreeList.DataSource = DB.SkladBase().MatGroup.Select(s => new { s.GrpId, s.PId, s.Name }).ToList();
                //     ProducerLookUpEdit.Properties.DataSource = DB.SkladBase().Materials.Select(s => new { s.Producer }).Distinct().ToList();
                //     CIdLookUpEdit.Properties.DataSource = DBHelper.CountersList;

                KagentBindingSource.DataSource = _ka;
                if (k_discount != null)
                {
                    DiscountBindingSource.DataSource = k_discount;
                    panelControl6.Enabled = DiscCustomCheckEdit.Checked;
                }

                if (_ka.KAgentDoc != null)
                {
                    KAgentDocDS.DataSource = _ka.KAgentDoc;
                }
                else
                {
                    KAgentDocDS.DataSource = _db.KAgentDoc.Add(new KAgentDoc { KAId = _ka.KaId });
                }

                if (_ka.KaAddr.Where(w => w.AddrType == 0).Any())
                {
                    AddrresFizBS.DataSource = _ka.KaAddr.FirstOrDefault(w => w.AddrType == 0);
                }
                else
                {
                    AddrresFizBS.DataSource = _db.KaAddr.Add(new KaAddr { KaId = _ka.KaId, AddrType = 0 });
                }

                if (_ka.KaAddr.Where(w => w.AddrType == 1).Any())
                {
                    AddrresUrBS.DataSource = _ka.KaAddr.FirstOrDefault(w => w.AddrType == 1);
                }
                else
                {
                    AddrresUrBS.DataSource = _db.KaAddr.Add(new KaAddr { KaId = _ka.KaId, AddrType = 1 });
                }

                GetAccounts();
            }
        }

        private void GetAccounts()
        {
            tree.RemoveAll(r => r.ParentId == 7);
            foreach (var item in GetAcc())
            {
                tree.Add(new CatalogTreeList
                {
                    Id = tree.Count + 1,
                    ParentId = 7,
                    Text = item.AccNum,
                    ImgIdx = 6,
                    TabIdx = 9,
                    DataSetId = item.AccId

                });
            }

            DirTreeList.RefreshDataSource();
            DirTreeList.ExpandAll();
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
            _db.SaveChanges();
            
            var focused_tree_node = DirTreeList.GetDataRecordByNode(e.Node) as CatalogTreeList;

            if (_ka != null)
            {
                GetDiscountList();
            }

            if (focused_tree_node.ParentId == 7)
            {
                KAgentAccountBS.DataSource = _db.KAgentAccount.Find(focused_tree_node.DataSetId);
            }

            if (focused_tree_node.Id == 7)
            {
                GetAcc();
            }

            xtraTabControl1.SelectedTabPageIndex = focused_tree_node.TabIdx;
        }

        private List<v_KAgentAccount> GetAcc()
        {
            var acc = _db.v_KAgentAccount.Where(w => w.KAId == _ka_id).ToList();
            v_KAgentAccountBS.DataSource = acc;

            return acc;
        }


        void GetDiscountList()
        {
            _db.SaveChanges();
            var list = _db.DiscountList(_ka.KaId).ToList();

            DiscountGridControl.DataSource = list;


      /*      tree.RemoveAll(r => r.ParentId == 4);
            foreach (var item in list)
            {
                tree.Add(new CatalogTreeList
                {
                    Id = tree.Max(m => m.Id) + 1,
                    ParentId = 4,
                    Text = item.Name,
                    ImgIdx = 2,
                    TabIdx = 4,
                    DataSetId = item.Id
                });
            }

    //        DirTreeList.DataSource = null;
      //      DirTreeList.DataSource = TreeListBS;

            DirTreeList.RefreshDataSource();
            DirTreeList.ExpandAll();*/
        }

        private void checkEdit2_CheckedChanged(object sender, EventArgs e)
        {
            StartSaldoPanel.Enabled = checkEdit2.Checked;
            if(checkEdit2.Checked)
            {
                if(KASaldoEdit.EditValue == DBNull.Value)
                {
                    KASaldoEdit.EditValue = 0;
                    StartSaldoDateEdit.DateTime = DateTime.Now;
                }
            }
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            if (k_discount != null && !DiscCheckEdit.Checked)
            {
                _db.DeleteWhere<KAMatDiscount>(w => w.KAId == _ka.KaId);
                _db.DeleteWhere<KAMatGroupDiscount>(w => w.KAId == _ka.KaId);
                _db.KADiscount.Remove(k_discount);
            }

            if (!checkEdit2.Checked && checkEdit2.Enabled)
            {
                KASaldoEdit.EditValue = null;
                StartSaldoDateEdit.EditValue = null;
            }

            _db.SaveChanges();
            current_transaction.Commit();
        }

        private void simpleButton6_Click(object sender, EventArgs e)
        {

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
            GetDiscountList();


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
            panelControl6.Enabled = DiscCustomCheckEdit.Checked;
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
                KAMatGroupDiscountDS.DataSource = _db.KAMatGroupDiscount.FirstOrDefault(w => w.DiscId == row.DiscId);
                xtraTabControl1.SelectedTabPageIndex = 11;
            }
        }

        private void barButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            xtraTabControl1.SelectedTabPageIndex = 11;
            var mat_grp_disc = _db.KAMatGroupDiscount.Add(new KAMatGroupDiscount { DiscId = Guid.NewGuid(), KAId = _ka.KaId, OnValue = 0, GrpId = DB.SkladBase().MatGroup.FirstOrDefault().GrpId });
            KAMatGroupDiscountDS.DataSource = mat_grp_disc;
            GetDiscountList();
/*
            DiscountList->Append();
            DiscountListIMG_IDX->Value = 1;
            MemTableEh1->Append();
            MemTableEh1ImgIdx->Value = 8;
            MemTableEh1Parent_ID->Value = 3;
            //	  MemTableEh1Text->Value = " ("+DiscountListONVALUE->AsString+"%) "+DiscountListNAME->Value ;
            MemTableEh1TabIdx->Value = 11;
            MemTableEh1DataSetID->Value = DiscountListDISCID->Value;
            MemTableEh1->Post();*/
        }


        private void DelDiscountBtn_Click(object sender, EventArgs e)
        {
            var row = DiscountGridView.GetFocusedRow() as DiscountList_Result;
            if (row == null) return;

            if (row.ImgIdx == 0)
            {
                _db.DeleteWhere<KAMatDiscount>(w => w.DiscId == row.DiscId);
            }
            else
            {
                _db.DeleteWhere<KAMatGroupDiscount>(w => w.DiscId == row.DiscId);
            }
            GetDiscountList();
            xtraTabControl1.SelectedTabPageIndex = 3;
        }

        private void frmKAgentEdit_Shown(object sender, EventArgs e)
        {
            Text = "Властивості контрагента: " + _ka.Name;
        }

        private void DiscountGridView_DoubleClick(object sender, EventArgs e)
        {
            EditDiscountBtn.PerformClick();
        }

        private void AddAccBtn_Click(object sender, EventArgs e)
        {
            xtraTabControl1.SelectedTabPageIndex = 9;
            var new_det = _db.KAgentAccount.Add(new KAgentAccount
            {
                KAId = _ka.KaId,
                AccNum = "",
                TypeId = _db.AccountType.FirstOrDefault().TypeId,
                BankId = _db.Banks.FirstOrDefault().BankId,
                Def = _db.KAgentAccount.Any(a => a.KAId == _ka.KaId) ? 0 : 1
            });
            KAgentAccountBS.DataSource = new_det;
            _db.SaveChanges();
            GetAccounts();

            DirTreeList.FocusedNode = DirTreeList.GetNodeList().FirstOrDefault(w => Convert.ToInt32(w.GetValue("DataSetId")) == new_det.AccId);
        }

        private void EditAccBtn_Click(object sender, EventArgs e)
        {
            dynamic det_item = AccountsGridView.GetFocusedRow();
            if (det_item == null) return;

            xtraTabControl1.SelectedTabPageIndex = 9;
            KAgentAccountBS.DataSource = _db.KAgentAccount.Find(det_item.AccId);

            DirTreeList.FocusedNode = DirTreeList.GetNodeList().FirstOrDefault(w => Convert.ToInt32(w.GetValue("DataSetId")) == det_item.AccId);
        }

        private void DelAccBtn_Click(object sender, EventArgs e)
        {
            dynamic det_item = AccountsGridView.GetFocusedRow();
            if (det_item == null) return;

            var a =_db.KAgentAccount.Find(det_item.AccId);
            _db.KAgentAccount.Remove(a);
            _db.SaveChanges();

            GetAccounts();
        }

        private void AccountsGridView_DoubleClick(object sender, EventArgs e)
        {
            EditAccBtn.PerformClick();
        }

        private void simpleButton7_Click_1(object sender, EventArgs e)
        {
            DirTreeList.FocusedNode = DirTreeList.GetNodeList().FirstOrDefault(w => Convert.ToInt32(w.GetValue("Id")) == 7);
        }

        private void textEdit17_EditValueChanged(object sender, EventArgs e)
        {
            if (textEdit17.ContainsFocus)
            {
                DirTreeList.FocusedNode.SetValue("Text", textEdit17.EditValue);
            }
        }

        private void simpleButton12_Click(object sender, EventArgs e)
        {
            _db.KAgentAccount.Remove(KAgentAccountBS.DataSource as KAgentAccount);
            _db.SaveChanges();
            GetAccounts();
            DirTreeList.FocusedNode = DirTreeList.GetNodeList().FirstOrDefault(w => Convert.ToInt32(w.GetValue("Id")) == 7);
        }

        private void checkEdit5_CheckedChanged_1(object sender, EventArgs e)
        {
            if(checkEdit5.ContainsFocus)
            {
                var acc =_db.KAgentAccount.FirstOrDefault(w => w.Def == 1);
                if(acc != null)
                {
                    acc.Def = 0;   
                }
            }
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            IHelper.ShowKABalans( _ka_id.Value );
        }

        private void simpleButton14_Click(object sender, EventArgs e)
        {
            lookUpEdit2.EditValue = IHelper.ShowDirectList(lookUpEdit2.EditValue, 9);
            
        }

        private void simpleButton13_Click(object sender, EventArgs e)
        {
            lookUpEdit1.EditValue = IHelper.ShowDirectList(lookUpEdit1.EditValue, 10);
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            _ka.UserId = null;
        }

        private void simpleButton5_Click(object sender, EventArgs e)
        {
            new frmUserEdit().ShowDialog();
            UsersLookUpEdit.Properties.DataSource = DB.SkladBase().Users.ToList().Where(w => !w.Kagent.Any()).ToList();
        }

        private void simpleButton15_Click(object sender, EventArgs e)
        {
            textEdit33.EditValue = textEdit21.EditValue;
            textEdit32.EditValue = textEdit24.EditValue;
            FCityTypeEdit.EditValue = UCityTypeEdit.EditValue;
            textEdit30.EditValue = textEdit23.EditValue;
            textEdit31.EditValue = textEdit22.EditValue;
            textEdit28.EditValue = textEdit26.EditValue;
            textEdit29.EditValue = textEdit25.EditValue;
        }

        private void lookUpEdit3_EditValueChanged(object sender, EventArgs e)
        {
            if (lookUpEdit3.EditValue == null || lookUpEdit3.EditValue == DBNull.Value)
            {
                return;
            }

            JobLookUpEdit.Visible = (int)lookUpEdit3.EditValue == 0;

        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            textEdit11.EditValue = textEdit12.EditValue;
        }

        private void RouteLookUpEdit_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
            {
                RouteLookUpEdit.EditValue = null;
            }
        }

        private void PTypeEdit_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
            {
                PTypeEdit.EditValue = null;
           //     _ka.PTypeId = null;
            }
        }

        private void KaGroupLookUpEdit_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
            {
                var frm = new frmKaGroup();
                if (frm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    KaGroupLookUpEdit.EditValue = frm.focused_row != null ? (Guid?)frm.focused_row.Id : null;
                }

                KaGroupLookUpEdit.Properties.DataSource = DB.SkladBase().KontragentGroup.AsNoTracking().ToList();
            }
        }

    }
}
