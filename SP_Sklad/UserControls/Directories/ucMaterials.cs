using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SP_Sklad.SkladData;
using SP_Sklad.EditForm;
using SP_Sklad.Common;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.Data;
using SkladEngine.DBFunction;
using DevExpress.XtraEditors;
using SP_Sklad.ViewsForm;

namespace SP_Sklad.MainTabs
{
    public partial class ucMaterials : DevExpress.XtraEditors.XtraUserControl
    {
        public int? GrpId { get; set; }
        public bool isDirectList { get; set; }
     //   public bool isMatList { get; set; }
        public List<CustomMatList> custom_mat_list { get; set; }
        public WaybillList wb { get; set; }
        private int _mat_archived { get; set; }
        private bool _show_rec_archived { get; set; }
        private bool _show_child_group { get; set; }

        private string reg_layout_path = "ucMaterials\\MatGridView";

        [Browsable(true)]
        public event EventHandler MatGridViewDoubleClick
        {
            add => this.MatGridView.DoubleClick += value;
            remove => this.MatGridView.DoubleClick -= value;
        }
        
        public v_Materials focused_mat
        {
            get { return MatGridView.GetFocusedRow() is NotLoadedObject ? null : MatGridView.GetFocusedRow() as v_Materials; }
        }
        private int[] selected_rows => MatGridView.GetSelectedRows();

        public ucMaterials()
        {
            InitializeComponent();
            _mat_archived = 0;
            _show_rec_archived = false;
        }

        private int prev_focused_id = 0;
        private int prev_top_row_index = 0;
        private int prev_rowHandle = 0;
        private int? find_id { get; set; }
        private bool _restore = false;
        private UserAccess user_access { get; set; }

        System.IO.Stream wh_layout_stream = new System.IO.MemoryStream();
        private void DirectoriesUserControl_Load(object sender, EventArgs e)
        {
            MatGridView.SaveLayoutToStream(wh_layout_stream);

            MatGridView.RestoreLayoutFromRegistry(IHelper.reg_layout_path + reg_layout_path);

            if (!DesignMode)
            {
                user_access = DB.SkladBase().UserAccess.FirstOrDefault(w => w.FunId == 6 && w.UserId == UserSession.UserId);

                NewItemBtn.Enabled = user_access.CanInsert == 1;

                custom_mat_list = new List<CustomMatList>();
                MatListGridControl.DataSource = custom_mat_list;

                repositoryItemLookUpEdit1.DataSource = DBHelper.WhList;
                repositoryItemLookUpEdit2.DataSource =  DB.SkladBase().MatGroup.OrderBy(o=> o.Num).Select(s => new { s.GrpId, s.PId, s.Name, ImageIndex = 17 }).ToList();
            }
        }

        protected override void OnCreateControl()
        {
            base.OnCreateControl();

            this.ParentForm.FormClosing += new FormClosingEventHandler(ParentForm_FormClosing);
        }

        void ParentForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            MatGridView.SaveLayoutToRegistry(IHelper.reg_layout_path + reg_layout_path);
        }

        public void GetData(bool show_child_group, bool restore = true)
        {
            _show_child_group = show_child_group;

            prev_rowHandle = MatGridView.FocusedRowHandle;

            if (focused_mat != null && !find_id.HasValue)
            {
                prev_top_row_index = MatGridView.TopRowIndex;
                prev_focused_id = focused_mat.MatId;
            }

            if (find_id.HasValue)
            {
                prev_top_row_index = -1;
                prev_focused_id = find_id.Value;
                find_id = null;
            }

            _restore = restore;

            MatGridControl.DataSource = null;
            MatGridControl.DataSource = MatListSource;

            SetWBEditorBarBtn();
        }

        private void RefrechItemBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            GetData(_show_child_group);
        }

        private void MatGridView_DoubleClick(object sender, EventArgs e)
        {
            if (MatListTabPage.PageVisible)
            {
                AddItem.PerformClick();
            }
            else if (isDirectList)
            {
                return;
            }
            else
            {
                EditItemBtn.PerformClick();
            }
        }

        private void EditItemBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DialogResult result = DialogResult.Cancel;

            if (focused_mat != null)
            {
                using (var mat_edit_frm = new frmMaterialEdit(focused_mat.MatId))
                {
                    result = mat_edit_frm.ShowDialog();
                }
            }

            if (result == DialogResult.OK)
            {
                RefrechItemBtn.PerformClick();
            }
        }

        private void NewItemBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (DB.SkladBase().MatGroup.Any())
            {
                using (var mat_edit = new frmMaterialEdit(null, GrpId > 0 ? GrpId : DB.SkladBase().MatGroup.First()?.GrpId))
                {
                    mat_edit.ShowDialog();
                }
            }
            RefrechItemBtn.PerformClick();
        }

        private void DeleteItemBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (focused_mat == null)
            {
                return;
            }

            if (MessageBox.Show("Ви дійсно бажаєте відалити цей запис з довідника?", "Підтвердіть видалення", MessageBoxButtons.YesNo, MessageBoxIcon.Information) != DialogResult.Yes)
            {
                return;
            }

            using (var db = DB.SkladBase())
            {
                var mat = db.Materials.Find(focused_mat.MatId);
                var mat_remain = db.v_MatRemains.Where(w => w.MatId == focused_mat.MatId).OrderByDescending(o => o.OnDate).FirstOrDefault();
                var mat_recipe = db.MatRecipe.Where(w => w.MatId == focused_mat.MatId && !w.Archived).Any();

                if (mat != null && mat_remain == null && !mat_recipe)
                {
                    mat.Deleted = 1;
                    mat.UpdatedBy = UserSession.UserId;
                }
                else
                {
                    MessageBox.Show("Видаляти заборонено !");
                }

                db.SaveChanges();
            }

            RefrechItemBtn.PerformClick();
        }


        private void AddItem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            AddMatItemToList(focused_mat);
        }

        public void AddMatItemToList(v_Materials row, string  bar_code = null )
        {
            if (row == null)
            {
                return;
            }

            var ka_price = GetPrice(row.MatId, wb);
            custom_mat_list.Add(new CustomMatList
            {
                Num = custom_mat_list.Count() + 1,
                MatId = row.MatId,
                Name = row.Name,
                Amount = 1,
                Price = ka_price,
                PriceWithoutNDS = wb.Nds > 0 ? Math.Round(ka_price * 100 / (100 + (wb.Nds ?? 0)), 4) : ka_price,
                WId = row.WId != null ? row.WId.Value : DBHelper.WhList.FirstOrDefault(w => w.Def == 1).WId,
                BarCode = bar_code
            });

            MatListGridView.RefreshData();
        }

        private decimal GetPrice(int mat_id, WaybillList wb)
        {
            decimal Price = 0;
            if (wb.WType == 1)
            {
                var get_last_price_result = new GetLastPrice(mat_id, wb.KaId, 1, wb.OnDate);
                Price = get_last_price_result.Price;
            }
            else if (wb.WType == -1 || wb.WType == -16 || wb.WType == 2)
            {
                var p_type = (wb.Kontragent != null ? (wb.Kontragent.PTypeId ?? DB.SkladBase().PriceTypes.First(w => w.Def == 1).PTypeId) : DB.SkladBase().PriceTypes.First(w => w.Def == 1).PTypeId);
                var mat_price = DB.SkladBase().GetListMatPrices(mat_id, wb.CurrId, p_type).FirstOrDefault();
                Price = mat_price.Price ?? 0;
            }

            return Price;
        }

        private void DelItem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            MatListGridView.DeleteSelectedRows();
        }

        private void MatGridView_PopupMenuShowing(object sender, DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs e)
        {
            if (e.HitInfo.InRow)
            {
                MatPopupMenu.ShowPopup(Control.MousePosition);
            }
        }

        private void barButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            IHelper.ShowTurnMaterial(focused_mat.MatId);
        }

        private void barButtonItem5_ItemClick_1(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            IHelper.ShowMatRSV(focused_mat.MatId, DB.SkladBase());
        }

        private void barButtonItem12_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (!IHelper.FindMatInWH(focused_mat.MatId))
            {
                MessageBox.Show(string.Format("На даний час товар <{0}> на складі вдсутній!", focused_mat.Name));
            }
        }

        private void barButtonItem13_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            using (var db = DB.SkladBase())
            {
                var mat = db.Materials.Find(focused_mat.MatId);
                if (mat != null)
                {
                    if (mat.Archived == 1)
                    {
                        mat.Archived = 0;
                    }
                    else
                    {
                        if (MessageBox.Show(string.Format("Ви дійсно хочете перемістити матеріал <{0}> в архів?", focused_mat.Name), "Information", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                        {
                            var mat_remain = db.v_MatRemains.Where(w => w.MatId == focused_mat.MatId).OrderByDescending(o => o.OnDate).FirstOrDefault();
                            if (mat_remain == null || mat_remain.Remain == null || mat_remain.Remain == 0)
                            {
                                foreach (var item in db.MatRecipe.Where(w => w.MatId == focused_mat.MatId))
                                {
                                    item.Archived = true;
                                }

                                mat.Archived = 1;
                            }
                            else
                            {
                                MessageBox.Show(string.Format("Неможливо перемістити матеріал <{0}> в архів, \nтому що його залишок складає {1} {2}", focused_mat.Name, mat_remain.Remain.Value.ToString(CultureInfo.InvariantCulture), focused_mat.ShortName));
                            }
                        }
                    }
                }
                db.SaveChanges();
            }

            RefrechItemBtn.PerformClick();
        }

        private void barCheckItem1_CheckedChanged(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            _mat_archived = showMatArhivedBtn.Checked ? 1 : 0;
            ArchivedGridColumn.Visible = showMatArhivedBtn.Checked;

            RefrechItemBtn.PerformClick();
        }


        private void MatGridView_FocusedRowObjectChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventArgs e)
        {
            SetWBEditorBarBtn();
        }

        private void SetWBEditorBarBtn()
        {
            xtraTabControl1_SelectedPageChanged(null, null);

            DeleteItemBtn.Enabled = (focused_mat != null && user_access.CanDelete == 1);
            EditItemBtn.Enabled = (focused_mat != null && user_access.CanModify == 1);
            CopyItemBtn.Enabled = (focused_mat != null && user_access.CanModify == 1);
        }

        private void xtraTabControl1_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            if (focused_mat == null)
            {
                return;
            }

            switch (xtraTabControl1.SelectedTabPageIndex)
            {
                case 0:
                    MatListInfoBS.DataSource = focused_mat;
                    break;

                case 1:
                    MatListInfoBS.DataSource = focused_mat;
                    repositoryItemComboBox1.Items.Clear();
                    repositoryItemComboBox1.Items.AddRange(DB.SkladBase().Materials.Where(w => w.Producer != null).Select(s => s.Producer).Distinct().ToList());
                    break;

                case 2:
                    var prices = DB.SkladBase().GetListMatPrices(focused_mat.MatId, 2, -1);

                    MatPriceGridControl.DataSource = DB.SkladBase().GetMatPriceTypes(focused_mat.MatId).ToList().Select(s => new
                    {
                        s.PTypeId,
                        s.Name,
                        s.TypeName,
                        s.IsIndividually,
                        Summary = s.Dis == 1 ? "" : s.ExtraType == 1
                                                    ? s.OnValue.Value.ToString("0.00") + " (Фіксована ціна)" : (s.PPTypeId == null || s.ExtraType == 2 || s.ExtraType == 3)
                                                    ? ((s.PPTypeId == null) ? s.OnValue.Value.ToString("0.00") + "% на ціну прихода" : (s.ExtraType == 2)
                                                    ? s.OnValue.Value.ToString("0.00") + "% на категорію " + s.PtName : (s.ExtraType == 3)
                                                    ? s.OnValue.Value.ToString("0.00") + "% на прайс-лист " + s.PtName : "")
                                                    : s.OnValue.Value.ToString("0.00") + "% від " + s.PtName,
                        s.Dis,
                        Price = DB.SkladBase().GetMatPrice(focused_mat.MatId,2, s.PTypeId,0).Select(s1 => s1.Price).FirstOrDefault()  //prices.Where(w=> w.PType == s.PTypeId).Select(s1=> s1.Price).FirstOrDefault()
                    });
                    break;

                case 3:
                    MatChangeGridControl.DataSource = DB.SkladBase().GetMatChange(focused_mat.MatId).ToList();
                    break;

                case 4:
                    MatNotesEdit.Text = focused_mat.Notes;
                    break;
            }
        }

        private void CopyItemBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            using (var frm = new frmMaterialEdit(null, null, focused_mat.MatId))
            {
                frm.ShowDialog();
            }

            RefrechItemBtn.PerformClick();
        }

        public v_Materials FindByBarCode(string br_code)
        {
            if (!String.IsNullOrEmpty(br_code))
            {
                var BarCodeSplit = br_code.Split('+');
                String kod = BarCodeSplit[0];
                var bc = DB.SkladBase().v_BarCodes.FirstOrDefault(w => w.BarCode == kod);

                if (bc != null)
                {
                    FindItem(bc.MatId);
                }

                return focused_mat;
            }

            return null;
        }

        private void barButtonItem11_ItemClick_1(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            IHelper.ExportToXlsx(MatGridControl);
        }


        private void MatListSource_GetQueryable(object sender, DevExpress.Data.Linq.GetQueryableEventArgs e)
        {
            var _db = DB.SkladBase();

            var mat = _db.v_Materials.AsQueryable();
            if(_mat_archived == 0)
            {
                mat = mat.Where(w => w.Archived == 0);
            }

        //    var grp_id = focused_tree_node.Id == 6 ? -1 : focused_tree_node.GrpId;

            if (GrpId > 0)
            {
                if (_show_child_group)
                {
                    var grp_list = _db.GetMatGroupTree(GrpId).Select(s => s.GrpId).ToList();

                    if (grp_list.Any())
                    {
                        mat = mat.Where(w => grp_list.Contains(w.GrpId));
                    }
                }
                else
                {
                    mat = mat.Where(w => w.GrpId == GrpId);
                }
            }

            e.QueryableSource = mat;

            e.Tag = _db;
        }

        private void MatGridView_AsyncCompleted(object sender, EventArgs e)
        {
            if (focused_mat == null || !_restore)
            {
                return;
            }

            int rowHandle = MatGridView.LocateByValue("MatId", prev_focused_id, OnRowSearchComplete);
            if (rowHandle != DevExpress.Data.DataController.OperationInProgress)
            {
                FocusRow(MatGridView, rowHandle);
            }
            else
            {
                MatGridView.FocusedRowHandle = prev_rowHandle;
            }

            _restore = false;
        }

        void OnRowSearchComplete(object rh)
        {
            int rowHandle = (int)rh;
            if (MatGridView.IsValidRowHandle(rowHandle))
            {
                FocusRow(MatGridView, rowHandle);
            }
        }

        public void FocusRow(GridView view, int rowHandle)
        {
            view.TopRowIndex = prev_top_row_index == -1 ? rowHandle : prev_top_row_index;
            view.FocusedRowHandle = rowHandle;
            view.SelectRow(rowHandle);
        }

        public void FindItem(int mat_id)
        {
            find_id = mat_id;
            MatGridView.ClearColumnsFilter();
            MatGridView.ClearFindFilter();

            RefrechItemBtn.PerformClick();
        }


        private void MatListGridView_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            var row = MatListGridView.GetFocusedRow() as CustomMatList;

            if (e.Column.FieldName == "Price")
            {
                row.PriceWithoutNDS = wb.Nds > 0 ? Math.Round((decimal)e.Value * 100 / (100 + (wb.Nds ?? 0)), 4) : (decimal?)e.Value;
            }

            if (e.Column.FieldName == "PriceWithoutNDS")
            {
                row.Price = wb.Nds > 0 ? Math.Round((decimal)e.Value + ((decimal)e.Value * (wb.Nds ?? 0) / 100), 4) : (decimal)e.Value;
            }
        }

        private void MatGridView_ColumnFilterChanged(object sender, EventArgs e)
        {
            SetWBEditorBarBtn();
        }

        private void BarCodeBtnEdit_KeyDown(object sender, KeyEventArgs e)
        {
            var textEdit = sender as TextEdit;

            if (e.KeyCode == Keys.Enter && !String.IsNullOrEmpty(textEdit.Text))
            {
                if ( MatListTabPage.PageVisible)
                {
                    var BarCodeSplit = textEdit.Text.Split('+');
                    String kod = BarCodeSplit[0];
                    var bc = DB.SkladBase().v_BarCodes.FirstOrDefault(w => w.BarCode == kod);
                    if (bc != null)
                    {
                        AddMatItemToList(DB.SkladBase().v_Materials.AsNoTracking().FirstOrDefault(w => w.MatId == bc.MatId), textEdit.Text);
                    }
                }
                else
                {
                    FindByBarCode(textEdit.Text);
                }

                textEdit.Text = "";
                //  (barEditItem2.Links[0] as BarEditItemLink).ShowEditor();
                e.Handled = true;
            }
        }

        private void BarCodeBtnEdit_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            var textEdit = sender as TextEdit;

            if (e.Button.Index == 0)
            {
                FindByBarCode(textEdit.Text);
            }
        }

        private void ChangeMatGrpBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var mg = IHelper.ShowDirectList(null, 19);

            if (mg != null && selected_rows.Any())
            {
                using (var db = DB.SkladBase())
                {
                    foreach (var item in selected_rows)
                    {
                        var row = MatGridView.GetRow(item) as v_Materials;

                        db.Materials.FirstOrDefault(w => w.MatId == row.MatId).GrpId = (int)mg;
                    }

                    db.SaveChanges();

                    RefrechItemBtn.PerformClick();
                }
            }
        }

        private void SelectAllBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            MatGridView.SelectAll();
        }

        private void CopyCellContentsBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Clipboard.SetText(MatGridView.GetFocusedDisplayText());
        }

        private void vGridControl2_CellValueChanged(object sender, DevExpress.XtraVerticalGrid.Events.CellValueChangedEventArgs e)
        {
            if (user_access?.CanModify != 1)
            {
                return;
            }

            if(e.Row.Properties.FieldName== "Producer")
            {
                using (var db = DB.SkladBase())
                {
                    foreach (var item in selected_rows)
                    {
                        var row = MatGridView.GetRow(item) as v_Materials;

                        db.Materials.FirstOrDefault(w => w.MatId == row.MatId).Producer = Convert.ToString(e.Value);
                    }

                    db.SaveChanges();

                    RefrechItemBtn.PerformClick();
                }
            }
        }

        private void vGridControl3_CellValueChanged(object sender, DevExpress.XtraVerticalGrid.Events.CellValueChangedEventArgs e)
        {
            if (user_access?.CanModify != 1)
            {
                return;
            }

            if (e.Row.Properties.FieldName == "GrpId")
            {
                using (var db = DB.SkladBase())
                {
                    foreach (var item in selected_rows)
                    {
                        var row = MatGridView.GetRow(item) as v_Materials;

                        db.Materials.FirstOrDefault(w => w.MatId == row.MatId).GrpId = Convert.ToInt32(e.Value);
                    }

                    db.SaveChanges();

                    RefrechItemBtn.PerformClick();
                }
            }
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            new frmKagentMaterilPrices(mat_id: focused_mat.MatId).ShowDialog();
        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            wh_layout_stream.Seek(0, System.IO.SeekOrigin.Begin);

            MatGridView.RestoreLayoutFromStream(wh_layout_stream);
        }

        private void MatGridView_KeyDown(object sender, KeyEventArgs e)
        {
            IHelper.CopyCellValueToClipboard(sender, e);
        }
    }
}
