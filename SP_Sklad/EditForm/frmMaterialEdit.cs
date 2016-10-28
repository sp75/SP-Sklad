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
using DevExpress.XtraEditors;
using SP_Sklad.Common;
using SP_Sklad.SkladData;

namespace SP_Sklad.EditForm
{
    public partial class frmMaterialEdit : DevExpress.XtraEditors.XtraForm
    {
        int? _mat_id { get; set; }
        int? _mat_grp { get; set; }
        private Materials _mat { get; set; }
        private BaseEntities _db { get; set; }
        private DbContextTransaction current_transaction { get; set; }
        private List<CatalogTreeList> tree { get; set; }
        private MatPrices _mat_prices { get; set; }

        public frmMaterialEdit(int? MatId =null, int? MatGrp = null)
        {
            InitializeComponent();
            _mat_id = MatId;
            _mat_grp = MatGrp;
            _db = DB.SkladBase();
            current_transaction = _db.Database.BeginTransaction();

            ExtraTypeLookUpEdit.Properties.DataSource = new List<object>() { new { Id = 0, Name = "На ціну приходу" }, new { Id = 2, Name = "На категорію" }, new { Id = 3, Name = "Прайс-лист" } };

            lookUpEdit1.Properties.DataSource = _db.PriceList.Select(s => new { s.PlId, s.Name }).ToList();
            CurrLookUpEdit.Properties.DataSource = DBHelper.Currency;
            lookUpEdit4.Properties.DataSource = _db.DemandGroup.AsNoTracking().ToList();
            ProducerLookUpEdit.Properties.Items.AddRange(_db.Materials.Where(w => w.Producer != null).Select(s => s.Producer).Distinct().ToList());
         
            tree = new List<CatalogTreeList>();
        }

        private void frmMaterialEdit_Load(object sender, EventArgs e)
        {
            xtraTabControl1.ShowTabHeader = DevExpress.Utils.DefaultBoolean.False;

            tree.Add(new CatalogTreeList { Id = 0, ParentId = 255, Text = "Основна інформація", ImgIdx = 0, TabIdx = 0 });
            tree.Add(new CatalogTreeList { Id = 1, ParentId = 255, Text = "Ціноутворення", ImgIdx = 1, TabIdx = 1 });
            tree.Add(new CatalogTreeList { Id = 2, ParentId = 255, Text = "Оподаткування", ImgIdx = 2, TabIdx = 2 });
            tree.Add(new CatalogTreeList { Id = 3, ParentId = 255, Text = "Взаємозамінність", ImgIdx = 3, TabIdx = 3 });
            tree.Add(new CatalogTreeList { Id = 4, ParentId = 255, Text = "Посвідчення", ImgIdx = 4, TabIdx = 4 });
            tree.Add(new CatalogTreeList { Id = 5, ParentId = 255, Text = "Зображення", ImgIdx = 5, TabIdx = 5 });
            tree.Add(new CatalogTreeList { Id = 6, ParentId = 255, Text = "Примітка", ImgIdx = 6, TabIdx = 6 });
            TreeListBS.DataSource = tree;


            if (_mat_id == null)
            {
                _mat = _db.Materials.Add(new Materials()
                {
                    Archived = 0,
                    Serials = 0,
                    MId = DBHelper.MeasuresList.FirstOrDefault(w => w.Def == 1).MId,
                    WId = DBHelper.WhList().FirstOrDefault(w => w.Def == 1).WId,
                    CId = DBHelper.CountersList.FirstOrDefault(w => w.Def == 1).CId,
                    NDS = 0,
                    GrpId = _mat_grp
                });
                _db.SaveChanges();
                _mat_id = _mat.MatId;
            }
            else
            {
                _mat = _db.Materials.Find(_mat_id);
                _mat.DateModified = DateTime.Now;
            }

            if (_mat != null)
            {
                GrpIdEdit.Properties.TreeList.DataSource = DB.SkladBase().MatGroup.Select(s => new { s.GrpId, s.PId, s.Name }).ToList();
                MsrComboBox.Properties.DataSource = DBHelper.MeasuresList;
                WIdLookUpEdit.Properties.DataSource = DBHelper.WhList();
                CIdLookUpEdit.Properties.DataSource = DBHelper.CountersList;

                MaterialsBS.DataSource = _mat;

                GetTreeMatPrices();
            }

            #region Init
            checkEdit3_CheckedChanged(sender, e);
            PricePanel.Enabled = false;
            NdsCheckEdit.Checked = _mat.NDS != -1;
            NdsEdit.EditValue = _mat.NDS == -1 ? null : _mat.NDS;
            GetNdsInfo();
            #endregion
        }

        private void GetListMatPrices()
        {
            var prices = _db.GetMatPriceTypes(_mat_id).ToList().Select(s => new
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
                s.Dis

            });

            MatPriceForGridBS.DataSource = prices;
        }

        private void GetTreeMatPrices()
        {
            tree.RemoveAll(r => r.ParentId == 1);
            foreach (var item in _db.GetMatPriceTypes(_mat_id).ToList())
            {
                tree.Add(new CatalogTreeList
                {
                    Id = tree.Max(m => m.Id) + 1,
                    ParentId = 1,
                    Text = item.Name,
                    ImgIdx = item.Dis == 1 ? 15 : 14,
                    TabIdx = 7,
                    DataSetId = item.PTypeId
                });
            }

            DirTreeList.RefreshDataSource();
            DirTreeList.ExpandAll();
        }

        private void DirTreeList_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
        {
            var focused_tree_node = DirTreeList.GetDataRecordByNode(e.Node) as CatalogTreeList;

            if (focused_tree_node.Id == 1)
            {
                GetListMatPrices();
            }

            xtraTabControl1.SelectedTabPageIndex = focused_tree_node.TabIdx;


            if (focused_tree_node.ParentId == 1)
            {
                _db.SaveChanges();

                var pt = _db.GetMatPriceTypes(_mat_id).FirstOrDefault(w => w.PTypeId == focused_tree_node.DataSetId);
                DisCheckBox.EditValue = pt.Dis;

                _mat_prices = _db.MatPrices.FirstOrDefault(a => a.PTypeId == focused_tree_node.DataSetId && a.MatId == _mat_id);
                if (_mat_prices == null)
                {
                    _mat_prices = new MatPrices
                   {
                       PTypeId = focused_tree_node.DataSetId,
                       MatId = _mat_id.Value,
                       OnValue = 0,
                       Dis = 0,
                       WithNds = 0,
                       ExtraType = pt.ExtraType.Value,
                       PPTypeId = pt.PPTypeId,
                       Currency = null
                   };
                }
               
                MatPriceTypesBS.DataSource = _mat_prices;
              

                lookUpEdit2.Properties.DataSource = _db.PriceTypes.Where(w => w.PTypeId != pt.PTypeId).Select(s => new { s.PTypeId, s.Name }).ToList();
                lookUpEdit3.Properties.DataSource = lookUpEdit2.Properties.DataSource;

                if (pt.ExtraType == 1) // в ручну встановити ціну
                {
                    CustomPriceCheckEdit.Checked = true;
                    CustomPriceEdit.EditValue = pt.OnValue;
                }

                if (pt.ExtraType == 0 || pt.ExtraType == 2 || pt.ExtraType == 3) // автоматично розрахувати ціну
                {
                    AutoCalcPriceCheckEdit.Checked = true;

                    if (pt.ExtraType == 0 && pt.PPTypeId != null) // скидка
                    {
                        checkEdit4.Checked = true;
                    }
                    else // націнка
                    {
                        checkEdit3.Checked = true;
                    }
                }

                if (pt.IsIndividually == 1)
                {
                    CommentLabel.Text = "* Націнтка встановлена для цього товара індивідуально";
                }
                else
                {
                    CommentLabel.Text = "* Націнка взята з довідника цінових катогорій";
                }
                if (pt.Dis == 1)
                {
                    CommentLabel.Text = "* Для даного товара заборонено автоматичне формування відпускної ціни в цій ціновій категорії";
                }
            }
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            _db.SaveChanges();
            current_transaction.Commit();
        }

        private void frmMaterialEdit_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (current_transaction.UnderlyingTransaction.Connection != null)
            {
                current_transaction.Rollback();
            }

            _db.Dispose();
            current_transaction.Dispose();
        }

        private void frmMaterialEdit_Shown(object sender, EventArgs e)
        {
            
        }

        private void NameTextEdit_EditValueChanged(object sender, EventArgs e)
        {
            Text = "Товар: " + NameTextEdit.Text;
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            ArtikulEdit.Text = NameTextEdit.Text;
            ArtikulEdit.Focus();
        }

        private void WhBtn_Click(object sender, EventArgs e)
        {
            NameTextEdit.Text = ArtikulEdit.Text;
            NameTextEdit.Focus();
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            IHelper.ShowMatRSV(_mat_id.Value, _db);
        }

        private void NowDateBtn_Click(object sender, EventArgs e)
        {

            MsrComboBox.EditValue = IHelper.ShowDirectList(MsrComboBox.EditValue, 12);
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            BarCodeEdit.EditValue = NameTextEdit.Text.ToArray().Sum(s=> Convert.ToInt32(s));
        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            CIdLookUpEdit.EditValue = IHelper.ShowDirectList(CIdLookUpEdit.EditValue, 7);
        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            IHelper.ShowTurnMaterial(_mat_id.Value);
        }

        private void ExtraTypeLookUpEdit_EditValueChanged(object sender, EventArgs e)
        {
            var type = ExtraTypeLookUpEdit.EditValue == DBNull.Value ? 0 : (int)ExtraTypeLookUpEdit.EditValue;

            lookUpEdit3.Visible = (type == 2);
            lookUpEdit1.Visible = (type == 3);
        }

        private void AutoCalcPriceCheckEdit_CheckedChanged(object sender, EventArgs e)
        {
            AutoCalcGroupControl.Enabled = AutoCalcPriceCheckEdit.Checked;

            CustomPricePanel.Enabled = CustomPriceCheckEdit.Checked;

            if (AutoCalcPriceCheckEdit.Checked && AutoCalcPriceCheckEdit.ContainsFocus)
            {
                ExtraTypeLookUpEdit.EditValue = 0;
                _mat_prices.PPTypeId = null;
                _db.SaveChanges();
            }
        }

        private void DisCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            PricePanel.Enabled = DisCheckBox.Checked;

            if (!DisCheckBox.ContainsFocus)
            {
                return;
            }

            if (DisCheckBox.Checked)
            {
                _mat_prices.Dis = 0;
                if (_db.Entry<MatPrices>(_mat_prices).State == System.Data.Entity.EntityState.Detached)
                {
                    _mat_prices = _db.MatPrices.Add(_mat_prices);
                }
            }
            else
            {
                _mat_prices.Dis = 1;
            }

            _db.SaveChanges();
        }

        private void MatPriceGridView_FocusedRowObjectChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventArgs e)
        {
            var row = MatPriceGridView.GetFocusedRow() as dynamic;

            DelPriceBtn.Enabled = (row.IsIndividually == 1);
            AddPriceBtn.Enabled = (row.IsIndividually == 0);
        }

        private void DelPriceBtn_Click(object sender, EventArgs e)
        {
            var row = MatPriceGridView.GetFocusedRow() as dynamic;
            int PTypeId = row.PTypeId;

            _db.DeleteWhere<MatPrices>(w => w.PTypeId ==PTypeId && w.MatId == _mat_id.Value);

            GetListMatPrices();
        }

        private void EditPriceBtn_Click(object sender, EventArgs e)
        {
            var row = MatPriceGridView.GetFocusedRow() as dynamic;
            DirTreeList.FocusedNode = DirTreeList.FindNodeByFieldValue("DataSetId", row.PTypeId);
        }

        private void simpleButton7_Click(object sender, EventArgs e)
        {
            DirTreeList.FocusedNode = DirTreeList.FindNodeByFieldValue("Id", 1);
        }

        private void AddPriceBtn_Click(object sender, EventArgs e)
        {
            var row = MatPriceGridView.GetFocusedRow() as dynamic;
            AddIndividualPric(row.PTypeId);
            EditPriceBtn.PerformClick();
        }

        private void AddIndividualPric(int PTypeId)
        {
            if (!_db.MatPrices.Any(a => a.PTypeId == PTypeId && a.MatId == _mat_id))
            {
                var price = _db.PriceTypes.FirstOrDefault(w => w.PTypeId == PTypeId);

                _mat_prices = _db.MatPrices.Add(new MatPrices
                  {
                      PTypeId = PTypeId,
                      MatId = _mat_id.Value,
                      OnValue = 0,
                      Dis = 0,
                      WithNds = 0,
                      ExtraType = price.ExtraType.Value,
                      PPTypeId = price.PPTypeId,
                      Currency = null
                  });

                _db.SaveChanges();
            }
        }

        private void UpdateIndividualPric(int PTypeId)
        {
            if (_mat_prices != null)
            {
                if(AutoCalcPriceCheckEdit.Checked )
                {
                    if (checkEdit3.Checked)
                    {
                        _mat_prices.OnValue = calcEdit1.Value;
                    }

                    if (checkEdit4.Checked)
                    {
                        _mat_prices.OnValue = calcEdit2.Value;
                    }
                }
            }
            else
            {
                AddIndividualPric(PTypeId);
            }
        }

        private void checkEdit3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkEdit3.ContainsFocus)
            {
                ExtraTypeLookUpEdit.EditValue = 0;
                //_mat_prices.ExtraType = 0;
                _mat_prices.PPTypeId = null;
                //_db._db.SaveChanges();
            }
            
            calcEdit2.Enabled = true;
            labelControl12.Enabled = true;
            lookUpEdit2.Enabled = true;

            calcEdit1.Enabled = true;
            ExtraTypeLookUpEdit.Enabled = true;
            lookUpEdit3.Enabled = true;
            lookUpEdit1.Enabled = true;


            if (checkEdit3.Checked)
            {
                calcEdit2.Enabled = false;
                labelControl12.Enabled = false;
                lookUpEdit2.Enabled = false;
            }

            if (checkEdit4.Checked)
            {
                calcEdit1.Enabled = false;
                ExtraTypeLookUpEdit.Enabled = false;
                lookUpEdit3.Enabled = false;
                lookUpEdit1.Enabled = false;
            }
        }

        private void CustomPriceCheckEdit_CheckedChanged(object sender, EventArgs e)
        {
            if (CustomPriceCheckEdit.Checked && CustomPriceCheckEdit.ContainsFocus)
            {
                _mat_prices.ExtraType = 1;
                _mat_prices.PPTypeId = null;
                //_mat_prices.OnValue = 0 
                //CustomPriceEdit.Value = 0;
                _db.SaveChanges();
            }
        }

        private void MatPriceTypesBS_CurrentItemChanged(object sender, EventArgs e)
        {
  
        }

        private void checkEdit4_CheckedChanged(object sender, EventArgs e)
        {
            if (checkEdit4.ContainsFocus)
            {
                ExtraTypeLookUpEdit.EditValue = 0;
                //_mat_prices.ExtraType = 0;
                lookUpEdit2.EditValue = _db.PriceTypes.FirstOrDefault().PTypeId;
                //_mat_prices.PPTypeId = _db.PriceTypes.FirstOrDefault().PTypeId;
                _db.SaveChanges();
            }
        }

        private void NdsCheckEdit_CheckedChanged(object sender, EventArgs e)
        {
            if (NdsCheckEdit.ContainsFocus)
            {
                _mat.NDS = NdsCheckEdit.Checked ? DBHelper.CommonParam.Nds : -1;
                NdsEdit.EditValue = NdsCheckEdit.Checked ? DBHelper.CommonParam.Nds : null;
                GetNdsInfo();
            }
        }

        private void simpleButton6_Click(object sender, EventArgs e)
        {
            _mat.NDS = null;
            NdsEdit.EditValue = null;
            GetNdsInfo();
        }

        private void GetNdsInfo()
        {
            if (_mat.NDS == -1)
            {
                NdsLabel.Text = "- Даний товар не обкладається ПДВ";
            }
            if (_mat.NDS == null)
            {
                NdsLabel.Text = "- Ставка ПДВ взята з налаштувать групи до якої він належить";
            }
            if (_mat.NDS >= 0)
            {
                NdsLabel.Text = "- Ставка ПДВ встановлена індивідуально";
            }
        }

        private void simpleButton5_Click(object sender, EventArgs e)
        {
            WIdLookUpEdit.EditValue = IHelper.ShowDirectList(WIdLookUpEdit.EditValue, 2);
        }
    }


}
