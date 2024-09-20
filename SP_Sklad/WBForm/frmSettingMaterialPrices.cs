using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.Data;
using DevExpress.Utils.DragDrop;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraTreeList;
using SP_Sklad.Common;
using SP_Sklad.EditForm;
using SP_Sklad.Properties;
using SP_Sklad.Reports;
using SP_Sklad.SkladData;

namespace SP_Sklad.WBForm
{
    public partial class frmSettingMaterialPrices : DevExpress.XtraEditors.XtraForm
    {
        private Guid? _wbt_id { get; set; }
        public BaseEntities _db { get; set; }
        private DbContextTransaction current_transaction { get; set; }
        private SettingMaterialPrices smp { get; set; }
        private int?  _PTypeId { get; set; }
        private int? _mat_id { get; set; }
        private UserSettingsRepository user_settings { get; set; }

        private v_SettingMaterialPricesDet focused_dr => SettingMaterialPricesDetGrid.GetFocusedRow() as v_SettingMaterialPricesDet;

        public frmSettingMaterialPrices(Guid? wbt_id = null, int? PTypeId = null, int? mat_id = null )
        {
            InitializeComponent();

            _wbt_id = wbt_id;
            _PTypeId = PTypeId;
            _mat_id = mat_id;
            _db = new BaseEntities();
            current_transaction = _db.Database.BeginTransaction();

            user_settings = new UserSettingsRepository(DBHelper.CurrentUser.UserId, _db);
        }

        Stream wb_det_layout_stream = new MemoryStream();
        private void frmPriceList_Load(object sender, EventArgs e)
        {
            SettingMaterialPricesDetGrid.SaveLayoutToStream(wb_det_layout_stream);
            SettingMaterialPricesDetGrid.RestoreLayoutFromRegistry(IHelper.reg_layout_path + "frmSettingMaterialPrices\\SettingMaterialPricesDetGrid");

            PTypeEdit.Properties.DataSource = _db.PriceTypes.AsNoTracking().ToList();
            PersonComboBox.Properties.DataSource = DBHelper.Persons;
            repositoryItemLookUpEdit1.DataSource = _db.v_MaterialBarCodes.AsNoTracking().Where(w => w.Archived == 0 && (w.TypeId == 1 || w.TypeId == 5 || w.TypeId == 6)).ToList();
            repositoryItemLookUpEdit2.DataSource = _db.Kagent.Where(w => w.Deleted == 0 && w.KaKind == 0).Select(s => new { s.KaId, s.Name }).ToList();

            if (_wbt_id == null)
            {
                smp = _db.SettingMaterialPrices.Add(new SettingMaterialPrices
                {
                    Id = Guid.NewGuid(),
                    DocType = 31,
                    Checked = 0,
                    OnDate = DBHelper.ServerDateTime(),
                    PersonId = DBHelper.CurrentUser.KaId,
                    PTypeId = _PTypeId ?? _db.PriceTypes.FirstOrDefault().PTypeId,
                    Num = new BaseEntities().GetDocNum("setting_material_prices").FirstOrDefault()
                });

                _db.SaveChanges();
                _wbt_id = smp.Id;
            }
            else
            {
                smp = _db.SettingMaterialPrices.Find(_wbt_id);
            }

            if (smp != null)
            {
                SettingMaterialPricesBS.DataSource = smp;
            }

            if (_mat_id.HasValue)
            {
                AddNewMaterial(_mat_id.Value);
            }

            GetDetail();
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            smp.UpdatedAt = DateTime.Now;
            smp.UpdatedBy = DBHelper.CurrentUser.UserId;

            GetDetail();

            _db.SaveChanges();
            current_transaction.Commit();
            Close();
        }

        private void frmPriceList_FormClosed(object sender, FormClosedEventArgs e)
        {
            SettingMaterialPricesDetGrid.SaveLayoutToRegistry(IHelper.reg_layout_path + "frmSettingMaterialPrices\\SettingMaterialPricesDetGrid");

            if (current_transaction.UnderlyingTransaction.Connection != null)
            {
                current_transaction.Rollback();
            }

            _db.Dispose();
            current_transaction.Dispose();
        }

        void GetDetail()
        {
            int top_row = SettingMaterialPricesDetGrid.TopRowIndex;
            var focus_row_id = focused_dr != null ? focused_dr.Id : Guid.Empty;
            SettingMaterialPricesDetBS.DataSource = _db.v_SettingMaterialPricesDet.AsNoTracking().OrderBy(o => o.Num).Where(w => w.SettingMaterialPricesId == _wbt_id).ToList();
            SettingMaterialPricesDetGrid.ExpandAllGroups();
            SettingMaterialPricesDetGrid.TopRowIndex = top_row;
            var rowHandle = SettingMaterialPricesDetGrid.LocateByValue("Id", focus_row_id);

            if (rowHandle != DevExpress.XtraGrid.GridControl.InvalidRowHandle)
            {
                SettingMaterialPricesDetGrid.FocusedRowHandle = rowHandle;
            }
        }

        private void DelMaterialBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if(focused_dr == null)
            {
                return;
            }


            var smp = _db.SettingMaterialPricesDet.FirstOrDefault(w => w.Id == focused_dr.Id);
            if (smp != null)
            {
                _db.SettingMaterialPricesDet.Remove(smp);
                _db.SaveChanges();
            }

            GetDetail();

        }

        private void PrevievBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            PrintDoc.SettingMaterialPricesReport(smp.Id, _db);
        }

        private void MatInfoBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var dr = SettingMaterialPricesDetGrid.GetFocusedRow() as GetPriceListDet_Result;
            IHelper.ShowMatInfo(dr.MatId);
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            Close();
        }
             
        private void AddMaterialBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            using (var f = new frmCatalog(2))
            {
                f.uc.isDirectList = true;
                f.Text = "Товари";
                if (f.ShowDialog() == DialogResult.OK)
                {
                    foreach (var item in f.uc.ucMaterials.MatGridView.GetSelectedRows())
                    {
                        var row = f.uc.ucMaterials.MatGridView.GetRow(item) as v_Materials;

                        AddNewMaterial(row.MatId);
                    }

                    GetDetail();
                }
            }

/*
            var mat_id = IHelper.ShowDirectList(null, 5);
            if (mat_id != null)
            {
                var id = Convert.ToInt32(mat_id);
                AddNewMaterial(id);
                GetDetail();
            }*/
        }

        private void AddNewMaterial( int mat_id)
        {
            if (!_db.SettingMaterialPricesDet.Where(w => w.MatId == mat_id && w.SettingMaterialPricesId == _wbt_id.Value).Any())
            {
                _db.SettingMaterialPricesDet.Add(new SettingMaterialPricesDet
                {
                    Id = Guid.NewGuid(),
                    Num = (_db.SettingMaterialPricesDet.Where(w => w.SettingMaterialPricesId == _wbt_id.Value).Max(m => m.Num) ?? 0) + 1,
                    MatId = mat_id,
                    SettingMaterialPricesId = _wbt_id.Value,
                    CreatedAt = DBHelper.ServerDateTime(),
                    Price = 0
                });

                _db.SaveChanges();
            }
        }

        private void barButtonItem6_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var GrpId = IHelper.ShowDirectList(null, 19);
            if (GrpId != null)
            {
                var grp_id = (int)GrpId;
                foreach (var item in _db.Materials.Where(w => w.GrpId == grp_id && w.Deleted == 0 && (w.Archived ?? 0) == 0).ToList())
                {
                    var num = _db.SettingMaterialPricesDet.Where(w => w.SettingMaterialPricesId == _wbt_id.Value).Max(m => m.Num) ?? 0;
                    if (!_db.SettingMaterialPricesDet.Where(w => w.MatId == item.MatId && w.SettingMaterialPricesId == _wbt_id.Value).Any())
                    {
                        _db.SettingMaterialPricesDet.Add(new SettingMaterialPricesDet
                        {
                            Id = Guid.NewGuid(),
                            Num = ++num,
                            MatId = item.MatId,
                            SettingMaterialPricesId = _wbt_id.Value,
                            CreatedAt = DBHelper.ServerDateTime(),
                            Price = 0
                        });
                    }
                }
                _db.SaveChanges();
                GetDetail();
            }
        }

        private void WaybillTemplateDetGrid_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            var wbtd = _db.SettingMaterialPricesDet.Find(focused_dr.Id);

            if (wbtd != null)
            {
                if (e.Column.FieldName == "Price")
                {
                    wbtd.Price = Convert.ToDecimal(e.Value);
                    if (wbtd.ProcurementPrice.HasValue)
                    {
                        wbtd.Markup =  ( wbtd.Price - wbtd.ProcurementPrice) / wbtd.ProcurementPrice * 100;
                        focused_dr.Markup = wbtd.Markup;
                    }
                }

                if (e.Column.FieldName == "MatId")
                {
                    wbtd.MatId = Convert.ToInt32(e.Value);
                    _db.SaveChanges();
                    GetDetail();
                }

                if (e.Column.FieldName == "ProcurementPrice")
                {
                    wbtd.ProcurementPrice = Convert.ToDecimal(e.Value);
                    if (wbtd.Markup.HasValue)
                    {
                        wbtd.Price = Math.Round(wbtd.ProcurementPrice.Value + (wbtd.ProcurementPrice.Value * wbtd.Markup.Value / 100), Convert.ToInt32(barEditItem2.EditValue));
                        focused_dr.Price = wbtd.Price;
                    }
                }

                if (e.Column.FieldName == "Markup")
                {
                    wbtd.Markup = Convert.ToDecimal(e.Value);
                    if (wbtd.ProcurementPrice.HasValue)
                    {
                        wbtd.Price = Math.Round(wbtd.ProcurementPrice.Value + (wbtd.ProcurementPrice.Value * wbtd.Markup.Value / 100), Convert.ToInt32(barEditItem2.EditValue));
                        focused_dr.Price = wbtd.Price;
                    }
                }

                if (e.Column.FieldName == "SupplierId")
                {
                    wbtd.SupplierId = Convert.ToInt32(e.Value);
                    _db.SaveChanges();
                }

            }
            else
            {
                if (!_db.SettingMaterialPricesDet.Where(w => w.MatId == focused_dr.MatId && w.SettingMaterialPricesId == _wbt_id.Value).Any())
                {
                    if (wbtd.ProcurementPrice.HasValue)
                    {
                        wbtd.Markup = (wbtd.Price - wbtd.ProcurementPrice) / wbtd.ProcurementPrice * 100;
                        focused_dr.Markup = wbtd.Markup;
                    }

                    wbtd = _db.SettingMaterialPricesDet.Add(new SettingMaterialPricesDet
                    {
                        Id = focused_dr.Id,
                        Num = (_db.SettingMaterialPricesDet.Where(w => w.SettingMaterialPricesId == _wbt_id.Value).Max(m => m.Num) ?? 0) + 1,
                        MatId = focused_dr.MatId,
                        SettingMaterialPricesId = _wbt_id.Value,
                        CreatedAt = DBHelper.ServerDateTime(),
                        Price = focused_dr.Price,
                        ProcurementPrice = focused_dr.ProcurementPrice,
                        Markup = focused_dr.ProcurementPrice.HasValue ? (focused_dr.Price - focused_dr.ProcurementPrice) / focused_dr.ProcurementPrice * 100 : null
                    });

                    _db.SaveChanges();

                    var v_smpd = _db.v_SettingMaterialPricesDet.FirstOrDefault(w => w.Id == focused_dr.Id);
                    focused_dr.Num = v_smpd.Num;
                    focused_dr.Artikul = v_smpd.Artikul;
                    focused_dr.GrpName = v_smpd.GrpName;
                }
            }

            _db.SaveChanges();
            SettingMaterialPricesDetGrid.RefreshRow(SettingMaterialPricesDetGrid.FocusedRowHandle);
        }

        private void OnDateDBEdit_Validating(object sender, CancelEventArgs e)
        {
            if (OnDateDBEdit.DateTime.Date < DBHelper.ServerDateTime().Date)
            {
                OnDateDBEdit.ErrorText = "Дата документа повина бути в межах поточного дня або більшою!";
                e.Cancel = true;
            }
        }

        private void OnDateDBEdit_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if(e.Button.Index ==1)
            {
                OnDateDBEdit.DateTime = DBHelper.ServerDateTime();
            }
        }
        bool GetOk()
        {
            bool recult = OnDateDBEdit.DateTime.Date >= DBHelper.ServerDateTime().Date;

            OkButton.Enabled = recult;

            return recult;
        }

        private void OnDateDBEdit_EditValueChanged(object sender, EventArgs e)
        {
            GetOk();
        }

        private void frmSettingMaterialPrices_Shown(object sender, EventArgs e)
        {
            SettingMaterialPricesDetGrid.Appearance.Row.Font = new Font(user_settings.GridFontName, (float)user_settings.GridFontSize);
            repositoryItemLookUpEdit1.AppearanceDropDown.Font = new Font(user_settings.GridFontName, (float)user_settings.GridFontSize);

            OnDateDBEdit.Focus();
            NumEdit.Focus();
        }

        private void PTypeEdit_EditValueChanged(object sender, EventArgs e)
        {
            if (PTypeEdit.ContainsFocus)
            {
                smp.PTypeId = (int)PTypeEdit.EditValue;
                _db.SaveChanges();

                GetDetail();
            }
        }

        private void SettingMaterialPricesDetBS_AddingNew(object sender, AddingNewEventArgs e)
        {
            e.NewObject = new v_SettingMaterialPricesDet
            {
                Id = Guid.NewGuid(),
                SettingMaterialPricesId = _wbt_id.Value,
                CreatedAt = DBHelper.ServerDateTime(),
                Price = 0,
                GrpId = 0,
                MatId = 0
            };
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            GetDetail();
        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            for (int i = 0; SettingMaterialPricesDetGrid.RowCount > i; i++)
            {
                var row = SettingMaterialPricesDetGrid.GetRow(i) as v_SettingMaterialPricesDet;
                if (row != null)
                {
                    var wbd = _db.SettingMaterialPricesDet.Find(row.Id);

                    wbd.Num = i + 1;
                }
            }

            _db.SaveChanges();

            GetDetail();
        }

        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            wb_det_layout_stream.Seek(0, System.IO.SeekOrigin.Begin);

            SettingMaterialPricesDetGrid.RestoreLayoutFromStream(wb_det_layout_stream);
        }

        private void SettingMaterialPricesDetGrid_PopupMenuShowing(object sender, DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs e)
        {
            if (e.HitInfo.InRow)
            {
                Point p2 = Control.MousePosition;
                this.TemplateListPopupMenu.ShowPopup(p2);
            }
        }

        private void SettingMaterialPricesDetGrid_MouseWheel(object sender, MouseEventArgs e)
        {
            SettingMaterialPricesDetGrid.CloseEditor();
        }

        private void repositoryItemCalcEdit2_EditValueChanged(object sender, EventArgs e)
        {
            var btn_edit = sender as CalcEdit;
            
            foreach (var item in _db.SettingMaterialPricesDet.Where(w => w.SettingMaterialPricesId == _wbt_id))
            {
                item.Markup = Convert.ToDecimal(btn_edit.EditValue);
                if (item.ProcurementPrice.HasValue)
                {
                    item.Price = Math.Round((item.ProcurementPrice.Value + (item.ProcurementPrice.Value * item.Markup.Value / 100)), Convert.ToInt32(barEditItem2.EditValue)); 
                }
            }
            _db.SaveChanges();
            GetDetail();
        }

        private void repositoryItemCalcEdit1_MouseUp(object sender, MouseEventArgs e)
        {
            var edit = ((CalcEdit)sender);
            BeginInvoke(new MethodInvoker(() =>
            {
                edit.SelectAll();
            }));
        }

        private void repositoryItemCalcEdit3_EditValueChanged(object sender, EventArgs e)
        {
            var btn_edit = sender as CalcEdit;
            foreach (var item in _db.SettingMaterialPricesDet.Where(w => w.SettingMaterialPricesId == _wbt_id))
            {
                if (item.ProcurementPrice.HasValue)
                {
                    item.Price = Math.Round((item.ProcurementPrice.Value + (item.ProcurementPrice.Value * item.Markup.Value / 100)), Convert.ToInt32(btn_edit.EditValue));
                }
            }
            _db.SaveChanges();
            GetDetail();
        }

        private void barButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            IHelper.ExportToXlsx(SettingMaterialPricesDetGridControl);

            /*        excelDataSource1.Fill();

                    DevExpress.DataAccess.Native.Excel.DataView dv = ((IListSource)excelDataSource1).GetList() as DevExpress.DataAccess.Native.Excel.DataView;
                    for (int i = 0; i < dv.Count; i++)
                    {
                        DevExpress.DataAccess.Native.Excel.ViewRow row = dv[i] as DevExpress.DataAccess.Native.Excel.ViewRow;
                        foreach (DevExpress.DataAccess.Native.Excel.ViewColumn col in dv.Columns)
                        {
                            object val = col.GetValue(row);
                        }
                    }*/

        }

        private void SettingMaterialPricesDetGrid_ClipboardRowPasting(object sender, ClipboardRowPastingEventArgs e)
        {
            if (e.IsRowValid())
            {
                var MatId = Convert.ToInt32( e.Values[colMaterial]);

                var num = _db.SettingMaterialPricesDet.Where(w => w.SettingMaterialPricesId == _wbt_id.Value).Max(m => m.Num) ?? 0;
                var det = _db.SettingMaterialPricesDet.FirstOrDefault(w => w.MatId == MatId && w.SettingMaterialPricesId == _wbt_id.Value);

                if (det == null)
                {
                    _db.SettingMaterialPricesDet.Add(new SettingMaterialPricesDet
                    {
                        Id = Guid.NewGuid(),
                        Num = ++num,
                        MatId = MatId,
                        SettingMaterialPricesId = _wbt_id.Value,
                        CreatedAt = DBHelper.ServerDateTime(),
                        Price = Convert.ToDecimal(e.Values[colPrice]),
                        Markup = e.Values[colMarkup] != null ? (decimal?)Convert.ToDecimal(e.Values[colMarkup]) : null,
                        ProcurementPrice = e.Values[colProcurementPrice] != null ? (decimal?)Convert.ToDecimal(e.Values[colProcurementPrice]) : null,
                    });
                }
                else
                {
                    det.Markup = e.Values[colMarkup] != null ? (decimal?)Convert.ToDecimal(e.Values[colMarkup]) : null;
                    det.ProcurementPrice = e.Values[colProcurementPrice] != null ? (decimal?)Convert.ToDecimal(e.Values[colProcurementPrice]) : null;
                    det.Price = Convert.ToDecimal(e.Values[colPrice]);

                    e.Cancel = true;
                }

                _db.SaveChanges();
            }
        }

        private void barButtonItem7_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SettingMaterialPricesDetGrid.PasteFromClipboard();
            GetDetail();
        }

        private void barButtonItem8_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SettingMaterialPricesDetGrid.CopyToClipboard();
        }

        private void barButtonItem9_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            IHelper.ShowMatInfo(focused_dr.MatId);
            repositoryItemLookUpEdit1.DataSource = _db.v_MaterialBarCodes.AsNoTracking().Where(w => w.Archived == 0 && (w.TypeId == 1 || w.TypeId == 5 || w.TypeId == 6)).ToList();
        }
    }
}
