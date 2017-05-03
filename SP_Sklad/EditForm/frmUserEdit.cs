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
using DevExpress.XtraTreeList;
using SP_Sklad.Common;
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

            tree.Add(new CatalogTreeList { Id = 0, ParentId = -1, Text = "Основна інформація", ImgIdx = 0, TabIdx = 0 });
            tree.Add(new CatalogTreeList { Id = 1, ParentId = -1, Text = "Права", ImgIdx = 13, TabIdx = 1 });
            tree.Add(new CatalogTreeList { Id = 2, ParentId = -1, Text = "Додаткова інформація", ImgIdx = 12, TabIdx = 2 });

            TreeListBindingSource.DataSource = tree;

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
                Text += string.Format( @" {0}", _u.Name );
            }

            UserGroupLookUpEdit.Properties.DataSource = DB.SkladBase().UsersGroup.AsNoTracking().ToList();

            checkEdit4.EditValue = !String.IsNullOrEmpty(user_settings.AccessEditWeight) ? Convert.ToInt32(user_settings.AccessEditWeight) : 0;
            checkEdit6.EditValue = !String.IsNullOrEmpty(user_settings.AccessEditPersonId) ? Convert.ToInt32(user_settings.AccessEditPersonId) : 0;

            ValidateForm();
        }

        private void DirTreeList_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
        {
            var focused_tree_node = DirTreeList.GetDataRecordByNode(e.Node) as CatalogTreeList;

            if (focused_tree_node.Id == 1)
            {
                UserTreeAccessBS.DataSource = _db.GetUserAccessTree( _user_id).ToList();
                UserAccessWhGridControl.DataSource = _db.GetUserAccessWh(_user_id).ToList();
            }

            xtraTabControl1.SelectedTabPageIndex = focused_tree_node.TabIdx;
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            _db.SaveChanges();
            current_transaction.Commit();
            DBHelper.CurrentUser = null;
        }

        private void treeList1_PopupMenuShowing(object sender, DevExpress.XtraTreeList.PopupMenuShowingEventArgs e)
        {
            Point p2 = Control.MousePosition;
            AccessPopupMenu.ShowPopup(p2);
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
            var au = _db.UserAccess.FirstOrDefault(w => w.FunId == row.FunId && w.UserId == _user_id && _user_id != 0);
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
            if (e.Column.FieldName == "Allow")
            {
                var row = UserAccessWhGridView.GetFocusedRow() as GetUserAccessWh_Result;
                if ((int)e.CellValue == 0)
                {
                    _db.UserAccessWh.Add(new UserAccessWh { UserId = _user_id.Value, WId = row.WId });
                    row.Allow = 1;
                }
                else
                {
                    _db.DeleteWhere<UserAccessWh>(w => w.UserId == _user_id.Value && w.WId == row.WId);
                    row.Allow = 0;
                }
                _db.SaveChanges();
                UserAccessWhGridView.RefreshRowCell(UserAccessWhGridView.FocusedRowHandle, e.Column);
                //   UserAccessWhGridControl.DataSource = _db.GetUserAccessWh(_user_id).ToList();
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
    }
}
