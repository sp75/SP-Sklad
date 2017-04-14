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
                    Id = Guid.NewGuid(),
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
                var k_saldo = _db.v_KAgentSaldo.Where(w => w.KaId == _ka.KaId).OrderBy(d => d.OnDate).Take(2).ToList();// FirstOrDefault();
                KASaldoEdit.EditValue = _ka.StartSaldo != null ? Math.Abs(_ka.StartSaldo.Value) : KASaldoEdit.EditValue;
                if (_ka.StartSaldo != null)
                {
                    checkEdit3.Checked = _ka.StartSaldo >= 0;
                    checkEdit4.Checked = _ka.StartSaldo < 0;
                }
                checkEdit2.Enabled = k_saldo == null || k_saldo.Count() < 2;
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
                var pos_list = DB.SkladBase().KAgentPersons.Where(w => w.JobType == 2 && w.Post != null).Select(s => s.Post).Distinct().ToList();
                JobLookUpEdit.Properties.Items.AddRange(pos_list);
                comboBoxEdit1.Properties.Items.AddRange(pos_list);
                lookUpEdit3.Properties.DataSource = DB.SkladBase().Jobs.AsNoTracking().ToList();
                PersonJobTypeLookUpEdit.Properties.DataSource = lookUpEdit3.Properties.DataSource;

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

                var _enterprise_list = new BaseEntities().Kagent.Where(w => w.KType == 3 && w.Deleted == 0 && (w.Archived == null || w.Archived == 0)).Select(s => new
                 {
                     KaId = s.KaId,
                     Name = s.Name,
                     IsWork = s.EnterpriseWorker.Any(a => a.WorkerId == _ka_id)

                 }).ToList();

                foreach (var item in _enterprise_list)
                {
                    checkedComboBoxEdit1.Properties.Items.Add(item.KaId, item.Name, item.IsWork ? CheckState.Checked : CheckState.Unchecked, true);
                }

            /*    _db.Kagent.Where(w => w.KType == 3 && w.Deleted == 0 && (w.Archived == null || w.Archived == 0)).Select(s => new
                {
                    KaId = s.KaId,
                    Name = s.Name,
                    IsOwned = s.EnterpriseKagent.Any(a => a.KaId == _ka_id)

                }).ToList().ForEach(f => checkedComboBoxEdit2.Properties.Items.Add(f.KaId, f.Name, f.IsOwned ? CheckState.Checked : CheckState.Unchecked, true));*/


                GetAccounts();
                GetPersons();
                GetDiscountList();

            }
        }

        private void GetPersons()
        {
            var list = _db.KAgentPersons.AsNoTracking().Where(w => w.KAId == _ka.KaId).ToList();
            KAgentPersonsBS.DataSource = list;

            tree.RemoveAll(r => r.ParentId == 6);
            foreach (var item in list)
            {
                tree.Add(new CatalogTreeList
                {
                    Id = tree.Max(m=> m.Id) + 1,
                    ParentId = 6,
                    Text = item.Name,
                    ImgIdx = 6,
                    TabIdx = 8,
                    DataSetId = item.PersonId

                });
            }

            DirTreeList.RefreshDataSource();
            DirTreeList.ExpandAll();
        }


        private void GetAccounts()
        {
            tree.RemoveAll(r => r.ParentId == 7);
            foreach (var item in GetAcc())
            {
                tree.Add(new CatalogTreeList
                {
                    Id = tree.Max(m => m.Id) + 1,
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

            if (_ka != null && focused_tree_node.Id == 4)
            {
                DiscountGridControl.DataSource  = _db.DiscountList(_ka.KaId).ToList();
            }
            if (focused_tree_node.ParentId == 4)
            {

                if (focused_tree_node.ImgIdx == 7) //Товар
                {
                    MatDiscountDS.DataSource = _db.KAMatDiscount.Find(focused_tree_node.DataSetId);
                }
                else
                {
                    KAMatGroupDiscountDS.DataSource = _db.KAMatGroupDiscount.Find(focused_tree_node.DataSetId);
                }
            }

            if (focused_tree_node.ParentId == 7)
            {
                KAgentAccountBS.DataSource = _db.KAgentAccount.Find(focused_tree_node.DataSetId);
            }

            if (focused_tree_node.Id == 7)
            {
                GetAcc();
            }

            if (focused_tree_node.Id == 6)
            {
               KAgentPersonsBS.DataSource = _db.KAgentPersons.AsNoTracking().Where(w => w.KAId == _ka.KaId).ToList();
            }

            if (focused_tree_node.ParentId == 6)
            {
                PersonBS.DataSource = _db.KAgentPersons.Find(focused_tree_node.DataSetId);
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


            tree.RemoveAll(r => r.ParentId == 4);
            foreach (var item in list)
            {
                tree.Add(new CatalogTreeList
                {
                    Id = tree.Max(m => m.Id) + 1,
                    ParentId = 4,
                    Text = item.Name,
                    ImgIdx = item.ImgIdx == 0 ? 7 : 8,
                    TabIdx = item.ImgIdx == 0 ? 10 : 11,
                    DataSetId = item.DiscId
                });
            }

            DirTreeList.DataSource = null;
            DirTreeList.DataSource = TreeListBS;

            DirTreeList.RefreshDataSource();
            DirTreeList.ExpandAll();
        }

        private void checkEdit2_CheckedChanged(object sender, EventArgs e)
        {
            StartSaldoPanel.Enabled = checkEdit2.Checked;
            if(checkEdit2.Checked)
            {
                if(_ka.StartSaldo == null)
                {
                    KASaldoEdit.EditValue = 0.00m;
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
                _ka.StartSaldo = null;
                _ka.StartSaldoDate = null;
            }
            if (checkEdit2.Enabled && checkEdit2.Checked)
            {
                if (checkEdit4.Checked)
                {
                    _ka.StartSaldo = KASaldoEdit.Value * -1.00m;
                }
                else
                {
                    _ka.StartSaldo = KASaldoEdit.Value;
                }
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
            var mat_disc = _db.KAMatDiscount.Add(new KAMatDiscount
            {
                DiscId = Guid.NewGuid(),
                KAId = _ka.KaId,
                OnValue = 0,
                MatId = DB.SkladBase().MaterialsList.FirstOrDefault().MatId
            });
            MatDiscountDS.DataSource = mat_disc;
            _db.SaveChanges();
            GetDiscountList();

            DirTreeList.FocusedNode = DirTreeList.FindNodeByFieldValue("DataSetId", mat_disc.DiscId);

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

            DirTreeList.FocusedNode = DirTreeList.FindNodeByFieldValue("DataSetId", row.DiscId);
        }

        private void barButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            xtraTabControl1.SelectedTabPageIndex = 11;
            var mat_grp_disc = _db.KAMatGroupDiscount.Add(new KAMatGroupDiscount
            {
                DiscId = Guid.NewGuid(),
                KAId = _ka.KaId,
                OnValue = 0,
                GrpId = DB.SkladBase().MatGroup.FirstOrDefault().GrpId
            });
            KAMatGroupDiscountDS.DataSource = mat_grp_disc;
            _db.SaveChanges();
            GetDiscountList();

            DirTreeList.FocusedNode = DirTreeList.FindNodeByFieldValue("DataSetId", mat_grp_disc.DiscId);
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

            DirTreeList.FocusedNode = DirTreeList.FindNodeByFieldValue("DataSetId", new_det.AccId);
        }

        private void EditAccBtn_Click(object sender, EventArgs e)
        {
            dynamic det_item = AccountsGridView.GetFocusedRow();
            if (det_item == null) return;

            xtraTabControl1.SelectedTabPageIndex = 9;
            KAgentAccountBS.DataSource = _db.KAgentAccount.Find(det_item.AccId);

            DirTreeList.FocusedNode = DirTreeList.FindNodeByFieldValue("DataSetId", det_item.AccId);
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
            UsersLookUpEdit.EditValue = null;
        //    _ka.UserId = null;
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

        private void simpleButton6_Click_1(object sender, EventArgs e)
        {
            DirTreeList.FocusedNode = DirTreeList.GetNodeList().FirstOrDefault(w => Convert.ToInt32(w.GetValue("Id")) == 4);
        }

        private void MatLookUpEdit_EditValueChanged(object sender, EventArgs e)
        {
            if (MatLookUpEdit.ContainsFocus)
            {
                DirTreeList.FocusedNode.SetValue("Text", MatLookUpEdit.Text);
            }
        }

        private void GroupLookUpEdit_EditValueChanged(object sender, EventArgs e)
        {
            if (GroupLookUpEdit.ContainsFocus)
            {
                DirTreeList.FocusedNode.SetValue("Text", GroupLookUpEdit.Text);
            }
        }

        private void simpleButton4_Click_1(object sender, EventArgs e)
        {
           
        }

        private void simpleButton17_Click(object sender, EventArgs e)
        {
           
        }

        private void simpleButton11_Click(object sender, EventArgs e)
        {
            _db.KAgentPersons.Remove(PersonBS.DataSource as KAgentPersons);
            _db.SaveChanges();
            GetPersons();

            DirTreeList.FocusedNode = DirTreeList.FindNodeByFieldValue("Id", 6);
        }

        private void AddPersonBtn_Click(object sender, EventArgs e)
        {
            xtraTabControl1.SelectedTabPageIndex = 8;
            var new_det = _db.KAgentPersons.Add(new KAgentPersons
            {
                KAId = _ka.KaId,
                Name = ""

            });
            PersonBS.DataSource = new_det;
            _db.SaveChanges();
            GetPersons();

            DirTreeList.FocusedNode = DirTreeList.FindNodeByFieldValue("DataSetId", new_det.PersonId);
        }

        private void DelPersonBtn_Click(object sender, EventArgs e)
        {
            dynamic det_item = KAgentPersonsGridView.GetFocusedRow();
            if (det_item == null) return;

            var a = _db.KAgentPersons.Find(det_item.PersonId);
            _db.KAgentPersons.Remove(a);
            _db.SaveChanges();

            xtraTabControl1.SelectedTabPageIndex = 6;

            GetPersons();
        }

        private void EditPersonBtn_Click(object sender, EventArgs e)
        {
            dynamic det_item = KAgentPersonsGridView.GetFocusedRow();
            if (det_item == null) return;

            xtraTabControl1.SelectedTabPageIndex = 8;
            PersonBS.DataSource = _db.KAgentPersons.Find(det_item.PersonId);

            DirTreeList.FocusedNode = DirTreeList.FindNodeByFieldValue("DataSetId", det_item.PersonId);
        }

        private void textEdit4_EditValueChanged(object sender, EventArgs e)
        {
            if (textEdit4.ContainsFocus)
            {
                DirTreeList.FocusedNode.SetValue("Text", textEdit4.EditValue);
            }
        }

        private void simpleButton18_Click(object sender, EventArgs e)
        {
            DirTreeList.FocusedNode = DirTreeList.FindNodeByFieldValue("Id", 6);
        }

        private void KAgentPersonsGridView_DoubleClick(object sender, EventArgs e)
        {
            EditPersonBtn.PerformClick();
        }

        private void checkedComboBoxEdit1_EditValueChanged(object sender, EventArgs e)
        {
            if (checkedComboBoxEdit1.ContainsFocus)
            {
                _db.EnterpriseWorker.RemoveRange(_db.EnterpriseWorker.Where(w => w.WorkerId == _ka_id));

                foreach (var item in checkedComboBoxEdit1.Properties.Items.Where(w=> w.CheckState == CheckState.Checked))
                {
                    _db.EnterpriseWorker.Add(new EnterpriseWorker { EnterpriseId = (int)item.Value, WorkerId = _ka_id.Value });
                }

                _db.SaveChanges();
            }
           
        }
/*
        private void checkedComboBoxEdit2_EditValueChanged(object sender, EventArgs e)
        {
            if (checkedComboBoxEdit2.ContainsFocus)
            {
                _db.EnterpriseKagent.RemoveRange(_db.EnterpriseKagent.Where(w => w.KaId == _ka_id));

                foreach (var item in checkedComboBoxEdit2.Properties.Items.Where(w => w.CheckState == CheckState.Checked))
                {
                    _db.EnterpriseKagent.Add(new EnterpriseKagent { EnterpriseId = (int)item.Value, KaId = _ka_id.Value });
                }

                _db.SaveChanges();
            }
        }*/

    }
}
