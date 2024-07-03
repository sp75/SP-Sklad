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
using DevExpress.Data;
using DevExpress.Utils.DragDrop;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraTreeList;
using SP_Sklad.Common;
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
        private SettingMaterialPrices wbt { get; set; }
        private int?  _PTypeId { get; set; }
        private int? _mat_id { get; set; }

        private v_SettingMaterialPricesDet focused_dr => WaybillTemplateDetGrid.GetFocusedRow() as v_SettingMaterialPricesDet;

        public frmSettingMaterialPrices(Guid? wbt_id = null, int? PTypeId = null, int? mat_id = null )
        {
            InitializeComponent();

            _wbt_id = wbt_id;
            _PTypeId = PTypeId;
            _mat_id = mat_id;
            _db = new BaseEntities();
            current_transaction = _db.Database.BeginTransaction();
        }
          
        private void frmPriceList_Load(object sender, EventArgs e)
        {
            PTypeEdit.Properties.DataSource = _db.PriceTypes.Select(s => new { s.PTypeId, s.Name }).ToList();
            PersonComboBox.Properties.DataSource = DBHelper.Persons;

            if (_wbt_id == null)
            {
                wbt = _db.SettingMaterialPrices.Add(new SettingMaterialPrices
                {
                    Id = Guid.NewGuid(),
                    DocType = 31,
                    Checked = 1,
                    OnDate = DBHelper.ServerDateTime(),
                    PersonId = DBHelper.CurrentUser.KaId,
                    PTypeId = _PTypeId ?? _db.PriceTypes.FirstOrDefault().PTypeId,
                    Num = new BaseEntities().GetDocNum("setting_material_prices").FirstOrDefault()
                });

                _db.SaveChanges();
                _wbt_id = wbt.Id;
            }
            else
            {
                wbt = _db.SettingMaterialPrices.Find(_wbt_id);
            }

            if (wbt != null)
            {
                SettingMaterialPricesBS.DataSource = wbt;
            }

            if (_mat_id.HasValue)
            {
                AddNewMaterial(_mat_id.Value);
            }

            GetDetail();
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            wbt.UpdatedAt = DateTime.Now;
            wbt.UpdatedBy = DBHelper.CurrentUser.UserId;

            _db.SaveChanges();
            current_transaction.Commit();
            Close();
        }

        private void frmPriceList_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (current_transaction.UnderlyingTransaction.Connection != null)
            {
                current_transaction.Rollback();
            }

            _db.Dispose();
            current_transaction.Dispose();
        }

        void GetDetail()
        {
            int top_row = WaybillTemplateDetGrid.TopRowIndex;
            SettingMaterialPricesDetBS.DataSource = _db.v_SettingMaterialPricesDet.AsNoTracking().Where(w=> w.SettingMaterialPricesId == _wbt_id).ToList();
            WaybillTemplateDetGrid.ExpandAllGroups();
            WaybillTemplateDetGrid.TopRowIndex = top_row;
        }

        private void DelMaterialBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            _db.SettingMaterialPricesDet.Remove(_db.SettingMaterialPricesDet.FirstOrDefault(w => w.Id == focused_dr.Id));
            _db.SaveChanges();
            GetDetail();

        }

        private void PrevievBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            PrintDoc.SettingMaterialPricesReport(wbt.PTypeId, _db);
        }

        private void MatInfoBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var dr = WaybillTemplateDetGrid.GetFocusedRow() as GetPriceListDet_Result;
            IHelper.ShowMatInfo(dr.MatId);
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            Close();
        }
             
        private void AddMaterialBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var mat_id = IHelper.ShowDirectList(null, 5);
            if (mat_id != null)
            {
                var id = Convert.ToInt32(mat_id);
                AddNewMaterial(id);
                GetDetail();
                /* var mat = _db.Materials.Find(id);
                 if (!_db.SettingMaterialPricesDet.Where(w => w.MatId == id && w.SettingMaterialPricesId == _wbt_id.Value).Any())
                 {
                     _db.SettingMaterialPricesDet.Add(new SettingMaterialPricesDet
                     {
                         Id = Guid.NewGuid(),
                         MatId = mat.MatId,
                         SettingMaterialPricesId = _wbt_id.Value,
                          CreatedAt = DBHelper.ServerDateTime(),
                           Price =0
                     });

                     _db.SaveChanges();
                     GetDetail();
                 }*/
            }
        }

        private void AddNewMaterial( int mat_id)
        {
            if (!_db.SettingMaterialPricesDet.Where(w => w.MatId == mat_id && w.SettingMaterialPricesId == _wbt_id.Value).Any())
            {
                _db.SettingMaterialPricesDet.Add(new SettingMaterialPricesDet
                {
                    Id = Guid.NewGuid(),
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
                // var id = Convert.ToInt32(mat_id);
                // var mat = _db.Materials.Find(id);
                var grp_id = (int)GrpId;
                foreach (var item in _db.Materials.Where(w => w.GrpId == grp_id && w.Deleted == 0 && (w.Archived ?? 0) == 0).ToList())
                {

                    if (!_db.SettingMaterialPricesDet.Where(w => w.MatId == item.MatId && w.SettingMaterialPricesId == _wbt_id.Value).Any())
                    {
                        _db.SettingMaterialPricesDet.Add(new SettingMaterialPricesDet
                        {
                            Id = Guid.NewGuid(),
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


        private void PriceListGrid_PopupMenuShowing(object sender, DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs e)
        {
            if (e.HitInfo.InRow)
            {
                Point p2 = Control.MousePosition;
                this.TemplateListPopupMenu.ShowPopup(p2);
            }
        }


        private void WaybillTemplateDetGrid_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            var wbtd = _db.SettingMaterialPricesDet.Find(focused_dr.Id);

            if (e.Column.FieldName == "Price")
            {
                wbtd.Price = Convert.ToDecimal(e.Value);
            }

            _db.SaveChanges();
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
            OnDateDBEdit.Focus();
            NumEdit.Focus();
        }

        private void PTypeEdit_EditValueChanged(object sender, EventArgs e)
        {
            if (PTypeEdit.ContainsFocus)
            {
                wbt.PTypeId = (int)PTypeEdit.EditValue;
                _db.SaveChanges();

                GetDetail();
            }
        }
    }
}
