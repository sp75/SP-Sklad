﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraTreeList;
using SP_Sklad.Common;
using SP_Sklad.Properties;
using SP_Sklad.SkladData;
using SP_Sklad.ViewsForm;

namespace SP_Sklad.EditForm
{
    public partial class frmUserEdit : DevExpress.XtraEditors.XtraForm
    {
        int? _user_id { get; set; }
        private Users _u { get; set; }
        private BaseEntities _db { get; set; }
        private DbContextTransaction current_transaction { get; set; }
        private List<CatalogTreeList> tree { get; set; }
        private GetUserAccessTree_Result focused_node
        {
            get
            {
                return treeList1.GetDataRecordByNode(treeList1.FocusedNode) as GetUserAccessTree_Result;
            }
        }
        private UserSettingsRepository user_settings  { get; set; }
     

        public frmUserEdit(int? UserId =null)
        {
            InitializeComponent();
        

            _user_id = UserId;
            _db = DB.SkladBase();
            current_transaction = _db.Database.BeginTransaction();
            tree = new List<CatalogTreeList>();
        }
       

        private void frmUserEdit_Load(object sender, EventArgs e)
        {
            xtraTabControl1.ShowTabHeader = DevExpress.Utils.DefaultBoolean.False;
            xtraTabControl2.ShowTabHeader = DevExpress.Utils.DefaultBoolean.False;

            tree.Add(new CatalogTreeList { Id = 0, ParentId = -1, Text = "Основна інформація", ImgIdx = 0, TabIdx = 0 });
            tree.Add(new CatalogTreeList { Id = 1, ParentId = -1, Text = "Права", ImgIdx = 13, TabIdx = 1 });
            tree.Add(new CatalogTreeList { Id = 3, ParentId = 1, Text = "Основні", ImgIdx = 1, TabIdx = 1 });
            tree.Add(new CatalogTreeList { Id = 4, ParentId = 1, Text = "Доступ до сладів", ImgIdx = 1, TabIdx = 1 });
            tree.Add(new CatalogTreeList { Id = 5, ParentId = 1, Text = "Додаткові", ImgIdx = 1, TabIdx = 1 });
            tree.Add(new CatalogTreeList { Id = 6, ParentId = 1, Text = "Доступ до кас", ImgIdx = 1, TabIdx = 1 });
            tree.Add(new CatalogTreeList { Id = 8, ParentId = 1, Text = "Доступ до груп товарів", ImgIdx = 1, TabIdx = 1 });
            tree.Add(new CatalogTreeList { Id = 9, ParentId = 1, Text = "Ролі", ImgIdx = 1, TabIdx = 1 });
            tree.Add(new CatalogTreeList { Id = 7, ParentId = -1, Text = "Робоче місце касира", ImgIdx = 18, TabIdx = 2 });
            tree.Add(new CatalogTreeList { Id = 2, ParentId = -1, Text = "Додаткові налаштування", ImgIdx = 12, TabIdx = 3 });



            TreeListBindingSource.DataSource = tree;
            DirTreeList.ExpandAll();

            if (_user_id == null)
            {
                _u = _db.Users.Add(new Users
                {
                    Name = "New User" + new Random().Next(0, 100000000).ToString(),
                    SysName = "",
                    ReportFormat = "xlsx",
                    InternalEditor = true,
                    IsWorking = true
                });

                _db.SaveChanges();

                _user_id = _u.UserId;
            }
            else
            {
                _u = _db.Users.Find(_user_id);
            }

            user_settings = new UserSettingsRepository(_user_id.Value, _db);

            if (_u != null)
            {
                UserBS.DataSource = _u;
                ConfirmPassEdit.Text = _u.Pass;
                Text += string.Format(@" {0}", _u.Name);
            }

            UserGroupLookUpEdit.Properties.DataSource = DB.SkladBase().UsersGroup.AsNoTracking().ToList();

            checkEdit4.EditValue = !String.IsNullOrEmpty(user_settings.AccessEditWeight) ? Convert.ToInt32(user_settings.AccessEditWeight) : 0;
            checkEdit6.EditValue = !String.IsNullOrEmpty(user_settings.AccessEditPersonId) ? Convert.ToInt32(user_settings.AccessEditPersonId) : 0;
            checkEdit7.EditValue = user_settings.AccessEditPrice;
            checkEdit8.EditValue = user_settings.AccessEditDocNum;

            comboBoxEdit2.Text = user_settings.GridFontName;
            comboBoxEdit1.Value = user_settings.GridFontSize;

            KagentComboBox.Properties.DataSource = DBHelper.KagentsWorkerList.Where(w=> w.KaKind == 5);
            KagentComboBox.EditValue = user_settings.DefaultBuyer;

            ChargeTypesEdit.Properties.DataSource = DBHelper.ChargeTypes;
            ChargeTypesEdit.EditValue = user_settings.DefaultChargeTypeByRMK;

            CashEditComboBox.Properties.DataSource = DBHelper.AllCashDesks.Where(w=> !string.IsNullOrEmpty( w.LicenseKey)).ToList();
            CashEditComboBox.EditValue = user_settings.CashDesksDefaultRMK;

            AccountEdit.Properties.DataSource = _db.EnterpriseAccount.Select(s => new
            {
                AccId = s.AccId,
                AccNum = s.AccNum,
                Name = s.BankName,
                s.KaName
            }).ToList();

            AccountEdit.EditValue = user_settings.AccountDefaultRMK;

            GetUserQuickMaterials();


          //  WarehouseEdit.Properties.DataSource = DBHelper.GetWhList(_user_id.Value, _db);

            LoginCheckboxEdit.Text = user_settings.CashierLoginCheckbox;
            PassCheckboxEdit.Text = user_settings.CashierPasswordCheckbox;

            ValidateForm();
        }

        private void DirTreeList_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
        {
            var focused_tree_node = DirTreeList.GetDataRecordByNode(e.Node) as CatalogTreeList;

            switch (focused_tree_node.Id)
            {
                case 1:
                case 3:
                    UserTreeAccessBS.DataSource = _db.GetUserAccessTree(_user_id).ToList();
                    xtraTabControl2.SelectedTabPageIndex = 0;
                    break;

                case 4:
                    UserAccessWhGridControl.DataSource = _db.GetUserAccessWh(_user_id).ToList();
                    xtraTabControl2.SelectedTabPageIndex = 1;
                    break;
                case 5:
                    xtraTabControl2.SelectedTabPageIndex = 2;
                    break;
                case 6:
                    RefreshUserAccessCashDesks();
                    xtraTabControl2.SelectedTabPageIndex = 3;
                    break;

                case 7:
                
                    break;

                case 8:
                    RefreshUserAccessMatGroup();
                    xtraTabControl2.SelectedTabPageIndex = 4;
                    break;

                case 9:
                    RefreshUserRoles();
                    xtraTabControl2.SelectedTabPageIndex = 5;
                    break;
            }

            xtraTabControl1.SelectedTabPageIndex = focused_tree_node.TabIdx;
        }

        private void RefreshUserAccessCashDesks()
        {
            UserAccessCashDesksBS.DataSource = _db.GetUserAccessCashDesks(_user_id).ToList();
        }

        private void RefreshUserAccessMatGroup()
        {
            UserAccessMatGroupBS.DataSource = _db.GetUserAccessMatGroup(_user_id).ToList();
        }
        private void RefreshUserRoles()
        {
            RoleGridControl.DataSource = _db.GetUserRoles(_user_id).ToList();
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            _db.SaveChanges();
            current_transaction.Commit();
            DBHelper.CurrentUser = null;
            DBHelper.WhList = null;
            DBHelper.CashDesks = null;

            Settings.Default.Save();
        }

        private void treeList1_PopupMenuShowing(object sender, DevExpress.XtraTreeList.PopupMenuShowingEventArgs e)
        {
            if (e.HitInfo.InRow)
            {
                AccessPopupMenu.ShowPopup(Control.MousePosition);
            }
        }

        private void treeList1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                TreeList treeList = sender as TreeList;
                TreeListHitInfo info = treeList.CalcHitInfo(e.Location);
                if (info.Node != null)
                {
                    treeList.FocusedNode = info.Node;
                }
            }
        }

         private void treeList1_Click(object sender, EventArgs e)
        {
            var col_name = treeList1.FocusedColumn.FieldName;
            var row = focused_node;

            if (col_name == "CanView" && isEdited(row.CanView))
            {
                if (row.CanView == 1)
                {
                    SetValue(row, 0);
                }
                else
                {
                    row.CanView = 1;
                }
            }
            
            if (col_name == "CanPost" && isEdited(row.CanPost))
            {
                row.CanPost = row.CanPost == 1 ? 0 : 1;
            }

            if (col_name == "CanModify" && isEdited(row.CanModify))
            {
                row.CanModify = row.CanModify == 1 ? 0 : 1;
            }

            if (col_name == "CanInsert" && isEdited(row.CanInsert))
            {
                row.CanInsert = row.CanInsert == 1 ? 0 : 1;
            }

            if (col_name == "CanDelete" && isEdited(row.CanDelete))
            {
                row.CanDelete = row.CanDelete == 1 ? 0 : 1;
            }

            UpdateRow(focused_node);

            treeList1.RefreshNode(treeList1.FocusedNode); 
        }

        private void UpdateRow(GetUserAccessTree_Result row)
        {
            var au = _db.UserAccess.FirstOrDefault(w => w.FunId == row.FunId && w.UserId == _user_id && !(_user_id == 0 && row.FunId == 14));
            if (au != null)
            {
                if (row.CanDelete != null) au.CanDelete = row.CanDelete.Value;
                if (row.CanInsert != null) au.CanInsert = row.CanInsert.Value;
                if (row.CanModify != null) au.CanModify = row.CanModify.Value;
                if (row.CanPost != null) au.CanPost = row.CanPost.Value;
                if (row.CanView != null) au.CanView = row.CanView.Value;

                _db.SaveChanges();
            }
        }

        private bool isEdited(int ? val)
        {
            return val == null || val == 2 ? false : true;
        }

        private void SetValue(GetUserAccessTree_Result row, int val)
        {
            if (isEdited(row.CanView))
            {
                row.CanView = row.CanView = val;
                row.CanPost = isEdited(row.CanPost) ? val : row.CanPost;
                row.CanModify = isEdited(row.CanModify) ? val : row.CanModify;
                row.CanInsert = isEdited(row.CanInsert) ? val : row.CanInsert;
                row.CanDelete = isEdited(row.CanDelete) ? val : row.CanDelete;

                UpdateRow(row);
            }
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (focused_node.FunId != null && focused_node.CanView == null)
            {
                var can_post =  _db.Functions.FirstOrDefault(w => w.FunId == focused_node.FunId).Flags == 0 ? 2 : 0;
            
                _db.UserAccess.Add(new UserAccess
                {
                    UserId = _user_id.Value,
                    FunId = focused_node.FunId.Value,
                    CanView = 0,
                    CanDelete = 0,
                    CanInsert = 0,
                    CanModify = 0,
                    CanPost = can_post
                });
                _db.SaveChanges();

                focused_node.CanView = 0;
                focused_node.CanPost = can_post;
                focused_node.CanModify = 0;
                focused_node.CanInsert = 0;
                focused_node.CanDelete = 0;
            }

            SetValue(focused_node, 1);
            treeList1.RefreshDataSource();
        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SetValue(focused_node, 0);
            treeList1.RefreshDataSource();
        }

        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            foreach (var row in UserTreeAccessBS.List)
            {
                SetValue(row as GetUserAccessTree_Result, 1);
            }

            treeList1.RefreshDataSource();
        }

        private void barButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            foreach (var row in UserTreeAccessBS.List)
            {
                SetValue(row as GetUserAccessTree_Result, 0);
            }

            treeList1.RefreshDataSource();
        }

        private void UserAccessWhGridView_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            var row = UserAccessWhGridView.GetFocusedRow() as GetUserAccessWh_Result;

            if (e.Column.FieldName == "Allow")
            {
                if ((int)e.CellValue == 0)
                {
                    var uaw = _db.UserAccessWh.Add(new UserAccessWh { UserId = _user_id.Value, WId = row.WId, UseReceived = true });
                    row.Allow = 1;
                    row.UseReceived = true;
                    _db.SaveChanges();

                    row.Id = uaw.Id;
                }
                else
                {
                    _db.UserAccessWh.RemoveRange(_db.UserAccessWh.Where(w => w.UserId == _user_id.Value && w.WId == row.WId));
                    row.Allow = 0;
                    row.UseReceived = false;
                    _db.SaveChanges();
                }

                UserAccessWhGridView.RefreshRow(UserAccessWhGridView.FocusedRowHandle);
            }

            if (e.Column.FieldName == "UseReceived" && row.Allow ==1)
            {
                var uaw = _db.UserAccessWh.Find(row.Id);
                if (row.UseReceived.Value)
                {
                    uaw.UseReceived = false;
                    row.UseReceived = false;
                }
                else
                {
                    uaw.UseReceived = true;
                    row.UseReceived = true;
                }

                _db.SaveChanges();
                UserAccessWhGridView.RefreshRowCell(UserAccessWhGridView.FocusedRowHandle, e.Column);
            }
        }

        void ValidateForm()
        {
            OkButton.Enabled = (ConfirmPassEdit.Text == textEdit2.Text);
        }

        private void textEdit2_EditValueChanged(object sender, EventArgs e)
        {
            ValidateForm();
        }

        private void UserGroupLookUpEdit_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
            {
                var frm = new frmUserGroup();
                if (frm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    UserGroupLookUpEdit.EditValue = frm.focused_row != null ? (Guid?)frm.focused_row.Id : null;
                }

                UserGroupLookUpEdit.Properties.DataSource = DB.SkladBase().UsersGroup.AsNoTracking().ToList();
            }
        }

        private void checkEdit4_CheckedChanged(object sender, EventArgs e)
        {
            user_settings.AccessEditWeight = Convert.ToString(checkEdit4.EditValue);
        }

        private void checkEdit6_CheckedChanged(object sender, EventArgs e)
        {
            user_settings.AccessEditPersonId = Convert.ToString(checkEdit6.EditValue);
        }

        private void UserAccessCashDesksGridView_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            if (e.CellValue == null)
            {
                return;
            }

            var row = UserAccessCashDesksGridView.GetFocusedRow() as GetUserAccessCashDesks_Result;
            
            if (e.Column.FieldName == "Allow")
            {
               
                if ((int)e.CellValue == 0)
                {
                    _db.UserAccessCashDesks.Add(new UserAccessCashDesks { Id = Guid.NewGuid(), UserId = _user_id.Value, CashId = row.CashId, Def = 0 });
                    row.Allow = 1;
                }
                else
                {
                    _db.DeleteWhere<UserAccessCashDesks>(w => w.UserId == _user_id.Value && w.CashId == row.CashId);
                    row.Allow = 0;
                }

                _db.SaveChanges();

                if (_db.UserAccessCashDesks.Any(w => w.UserId == _user_id.Value) && !_db.UserAccessCashDesks.Any(w => w.UserId == _user_id.Value && w.Def == 1))
                {
                    var c = _db.UserAccessCashDesks.FirstOrDefault(w => w.UserId == _user_id.Value);
                    c.Def = 1;
                }
                _db.SaveChanges();

                //     UserAccessCashDesksGridView.RefreshRowCell(UserAccessCashDesksGridView.FocusedRowHandle, e.Column);
                RefreshUserAccessCashDesks();
            }

            if (e.Column.FieldName == "Def")
            {
                if ((int)e.CellValue == 0)
                {
                    var list = UserAccessCashDesksBS.DataSource as List<GetUserAccessCashDesks_Result>;// _db.GetUserAccessCashDesks(_user_id).ToList();
                    foreach (var item in list.Where(w=> w.Allow == 1))
                    {
                        var uacd = _db.UserAccessCashDesks.FirstOrDefault(w => w.CashId == item.CashId && w.UserId == _user_id);
                        uacd.Def = 0;
                    }

                    var cur_uacd = _db.UserAccessCashDesks.FirstOrDefault(w => w.CashId == row.CashId && w.UserId == _user_id);
                    cur_uacd.Def = 1;

                }


                _db.SaveChanges();
                RefreshUserAccessCashDesks();
            }
        }

        private void checkEdit7_CheckedChanged(object sender, EventArgs e)
        {
            user_settings.AccessEditPrice = checkEdit7.Checked;
        }

        private void comboBoxEdit2_EditValueChanged(object sender, EventArgs e)
        {
            user_settings.GridFontName = comboBoxEdit2.Text;
        }

        private void comboBoxEdit1_EditValueChanged(object sender, EventArgs e)
        {
            user_settings.GridFontSize = comboBoxEdit1.Value;
        }

        private void KagentComboBox_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
            {
                KagentComboBox.EditValue = null;
            }
        }

        private void KagentComboBox_EditValueChanged(object sender, EventArgs e)
        {
            user_settings.DefaultBuyer = (int?)KagentComboBox.EditValue;
        }

        private void ChargeTypesEdit_EditValueChanged(object sender, EventArgs e)
        {
            user_settings.DefaultChargeTypeByRMK = (int)ChargeTypesEdit.EditValue;
        }

        private void ChargeTypesEdit_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
            {
                ChargeTypesEdit.EditValue = IHelper.ShowDirectList(ChargeTypesEdit.EditValue, 6);
            }
        }

        private void frmUserEdit_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (current_transaction.UnderlyingTransaction.Connection != null)
            {
                current_transaction.Rollback();
            }

            _db.Dispose();
            current_transaction.Dispose();
        }

        private void CashEditComboBox_EditValueChanged(object sender, EventArgs e)
        {
            user_settings.CashDesksDefaultRMK = (int)CashEditComboBox.EditValue;
        }

        private void AccountEdit_EditValueChanged(object sender, EventArgs e)
        {
            user_settings.AccountDefaultRMK = (int)AccountEdit.EditValue;
        }

        private void CashEditComboBox_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
            {
                CashEditComboBox.EditValue = IHelper.ShowDirectList(CashEditComboBox.EditValue, 4);
            }
        }

        private void AccountEdit_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
          
        }

        private void checkEdit8_CheckedChanged(object sender, EventArgs e)
        {
            user_settings.AccessEditDocNum = checkEdit8.Checked;
        }

        private void UserAccessMatGroupGridView_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            if (e.CellValue == null)
            {
                return;
            }

            var row = UserAccessMatGroupGridView.GetFocusedRow() as GetUserAccessMatGroup_Result;

            if (e.Column.FieldName == "Allow")
            {

                if ((int)e.CellValue == 0)
                {
                    _db.UserAccessMatGroup.Add(new UserAccessMatGroup { UserId = _user_id.Value,  GrpId  = row.GrpId });
                    row.Allow = 1;
                }
                else
                {
                    _db.DeleteWhere<UserAccessMatGroup>(w => w.UserId == _user_id.Value && w.GrpId == row.GrpId);
                    row.Allow = 0;
                }

                _db.SaveChanges();

                UserAccessMatGroupGridView.RefreshRow(UserAccessMatGroupGridView.FocusedRowHandle);

               // RefreshUserAccessMatGroup();
            }

        }

        private void LoginCheckboxEdit_EditValueChanged(object sender, EventArgs e)
        {
            user_settings.CashierLoginCheckbox = LoginCheckboxEdit.Text;
        }

        private void PassCheckboxEdit_EditValueChanged(object sender, EventArgs e)
        {
            user_settings.CashierPasswordCheckbox = PassCheckboxEdit.Text;
        }

        private void RoleGridView_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            if (e.CellValue == null)
            {
                return;
            }

            var row = RoleGridView.GetFocusedRow() as GetUserRoles_Result;

            if (e.Column.FieldName == "Allow")
            {

                if ((int)e.CellValue == 0)
                {
                    _db.UserRoles.Add(new UserRoles { UserId = _user_id.Value, RoleId = row.Id });
                    row.Allow = 1;
                }
                else
                {
                    _db.DeleteWhere<UserRoles>(w => w.UserId == _user_id.Value && w.RoleId == row.Id);
                    row.Allow = 0;
                }

                _db.SaveChanges();

                RoleGridView.RefreshRow(RoleGridView.FocusedRowHandle);
            }

        }

        private void GetUserQuickMaterials()
        {
            UserQuickMaterialsBS.DataSource = _db.UserQuickMaterials.Where(w => w.UserId == _user_id).ToList();
        }

        private void barButtonItem7_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var id = IHelper.ShowDirectList(null, 5);

            if (id != null)
            {
                int mat_id = (int)id;

                _db.UserQuickMaterials.Add(new UserQuickMaterials
                {
                    MatId = mat_id,
                    Name = "Btn1",
                    Num = UserQuickMaterialsBS.Count + 1,
                    UserId = _user_id.Value,
                    Materials = _db.Materials.Find(mat_id)
                });
                _db.SaveChanges();

                GetUserQuickMaterials();
            }
        }

        private void barButtonItem8_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var row = UserQuickMaterialsGridView.GetFocusedRow() as UserQuickMaterials;

            if(row != null)
            {
                _db.UserQuickMaterials.Remove(row);

                _db.SaveChanges();

                GetUserQuickMaterials();
            }
        }

        private void UserQuickMaterialsGridView_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
          /*  if(e.Column.FieldName == "Name")
            {

            }*/
        }

        private void barButtonItem9_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            foreach (var item in _db.GetUserAccessWh(_user_id).ToList())
            {
                if (!_db.UserAccessWh.Any(a => a.UserId == _user_id.Value && a.WId == item.WId))
                {
                    _db.UserAccessWh.Add(new UserAccessWh { UserId = _user_id.Value, WId = item.WId, UseReceived = true });
                }
            }
            _db.SaveChanges();

            UserAccessWhGridControl.DataSource = _db.GetUserAccessWh(_user_id).ToList();
        }

        private void barButtonItem10_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            _db.UserAccessWh.RemoveRange(_db.UserAccessWh.Where(w => w.UserId == _user_id.Value));
            _db.SaveChanges();

            UserAccessWhGridControl.DataSource = _db.GetUserAccessWh(_user_id).ToList();
        }
    }
}
