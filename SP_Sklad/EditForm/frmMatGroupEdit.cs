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
using DevExpress.XtraTreeList.Nodes;
using SP_Sklad.Common;
using SP_Sklad.SkladData;

namespace SP_Sklad.EditForm
{
    public partial class frmMatGroupEdit : DevExpress.XtraEditors.XtraForm
    {
        public int? _grp_id { get; set; }
        int? _pid { get; set; }
        private MatGroup _mg { get; set; }
        private BaseEntities _db { get; set; }
        private DbContextTransaction current_transaction { get; set; }
        private MatGroupPrices _mat_grp_prices { get; set; }
        private List<CatalogTreeList> tree { get; set; }

        public frmMatGroupEdit(int? GrpId = null, int? PId = null)
        {
            InitializeComponent();

            _grp_id = GrpId;
            _pid = PId;
            _db = DB.SkladBase();
            current_transaction = _db.Database.BeginTransaction();

            ExtraTypeLookUpEdit.Properties.DataSource = new List<object>() { new { Id = 0, Name = "На ціну приходу" }, new { Id = 2, Name = "На категорію" }, new { Id = 3, Name = "Прайс-лист" } };
            CurrLookUpEdit.Properties.DataSource = DBHelper.Currency;

            tree = new List<CatalogTreeList>();
            TreeListBS.DataSource = tree;
        }

        private void frmMatGroupEdit_Load(object sender, EventArgs e)
        {
            xtraTabControl1.ShowTabHeader = DevExpress.Utils.DefaultBoolean.False;

            TreeListBS.Add(new CatalogTreeList { Id = 0, ParentId = 255, Text = "Основна інформація", ImgIdx = 0, TabIdx = 0 });
            TreeListBS.Add(new CatalogTreeList { Id = 1, ParentId = 255, Text = "Ціноутворення ", ImgIdx = 1, TabIdx = 1 });
            TreeListBS.Add(new CatalogTreeList { Id = 2, ParentId = 255, Text = "Оподаткування", ImgIdx = 2, TabIdx = 2 });
            TreeListBS.Add(new CatalogTreeList { Id = 3, ParentId = 255, Text = "Класифікатор браку", ImgIdx = 8, TabIdx = 5 });
            TreeListBS.Add(new CatalogTreeList { Id = 4, ParentId = 255, Text = "Примітка", ImgIdx = 3, TabIdx = 3 });

            if (_grp_id == null)
            {
                _mg = _db.MatGroup.Add(new MatGroup
                {
                    Deleted = 0,
                    Nds = 0,
                    PId = 0,
                    Name = ""
                });
                _db.SaveChanges();

                _grp_id = _mg.GrpId;
                _mg.PId = _pid ?? _mg.GrpId;
            }
            else
            {
                _mg = _db.MatGroup.Find(_grp_id);
            }

            if (_mg != null)
            {
                checkEdit4.Checked = (_mg.GrpId == _mg.PId);

                GrpIdEdit.Properties.TreeList.DataSource = DB.SkladBase().MatGroup.Select(s => new { s.GrpId, s.PId, s.Name, ImageIndex = 7 }).ToList();

                MatGroupDS.DataSource = _mg;
                DefectsClassifierBS.DataSource = _db.DefectsClassifier.Where(w => w.GrpId == _mg.GrpId).ToList();
                DefectsClassifierTreeList.ExpandAll();



            }

            GetTreeMatPrices();


            #region Init

            checkEdit3_CheckedChanged(sender, e);
            PricePanel.Enabled = false;
            NdsEdit.EditValue = _mg.Nds == -1 ? null : _mg.Nds;

            #endregion

            DirTreeList.ExpandAll();
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            _db.SaveChanges();
            current_transaction.Commit();
        }

        private void frmMatGroupEdit_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (current_transaction.UnderlyingTransaction.Connection != null)
            {
                current_transaction.Rollback();
            }

            _db.Dispose();
            current_transaction.Dispose();
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
                int data_set_id = (int)focused_tree_node.DataSetId;

                var pt = _db.GetMatGroupPriceTypes(_grp_id).FirstOrDefault(w => w.PTypeId == data_set_id);
                DisCheckBox.EditValue = pt.Dis;

                _mat_grp_prices = _db.MatGroupPrices.FirstOrDefault(a => a.PTypeId == data_set_id && a.GrpId == _grp_id);
                if (_mat_grp_prices == null)
                {
                    _mat_grp_prices = new MatGroupPrices
                    {
                        //   Id = _mat_prices.Id,
                        PTypeId = data_set_id,
                        GrpId = _grp_id.Value,
                        OnValue = 0,
                        Dis = 0,
                        WithNds = 0,
                        ExtraType = pt.ExtraType.Value,
                        PPTypeId = pt.PPTypeId,
                        Currency = null
                    };
                }

                MatPriceTypesBS.DataSource = _mat_grp_prices;
                DelIdividualMatPriceBtn.Enabled = _mat_grp_prices.Id > 0;



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

        private void checkEdit4_CheckedChanged(object sender, EventArgs e)
        {
            if (checkEdit4.Checked)
            {
                _mg.PId = _mg.GrpId;
            }
        }

        private void checkEdit3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkEdit3.ContainsFocus && checkEdit3.Checked && GrpIdEdit.EditValue != DBNull.Value)
            {
                _mg.PId = (int)GrpIdEdit.EditValue;
            }
        }

        private void frmMatGroupEdit_Shown(object sender, EventArgs e)
        {
            this.Text = "Група товарів: " + textEdit10.Text;
        }


        private void GetListMatPrices()
        {
            var prices = _db.GetMatGroupPriceTypes(_grp_id).ToList().Select(s => new
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

            MatGroupPriceBS.DataSource = prices;
        }

        private void AddPriceBtn_Click(object sender, EventArgs e)
        {
            var row = MatPriceGridView.GetFocusedRow() as dynamic;
            AddIndividualPrice(row.PTypeId);
            EditPriceBtn.PerformClick();
        }

        private void AddIndividualPrice(int PTypeId)
        {
            if (!_db.MatPrices.Any(a => a.PTypeId == PTypeId && a.MatId == _grp_id))
            {
                var price = _db.PriceTypes.FirstOrDefault(w => w.PTypeId == PTypeId);

                _mat_grp_prices = _db.MatGroupPrices.Add(new MatGroupPrices
                {
                    PTypeId = PTypeId,
                    GrpId = _grp_id.Value,
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

        private void EditPriceBtn_Click(object sender, EventArgs e)
        {
            var row = MatPriceGridView.GetFocusedRow() as dynamic;
            DirTreeList.FocusedNode = DirTreeList.FindNodeByFieldValue("DataSetId", row.PTypeId);
        }

        private void DelPriceBtn_Click(object sender, EventArgs e)
        {
            var row = MatPriceGridView.GetFocusedRow() as dynamic;
            int PTypeId = row.PTypeId;

            _db.DeleteWhere<MatGroupPrices>(w => w.PTypeId == PTypeId && w.GrpId == _grp_id.Value);

            GetListMatPrices();
        }

        private void GetTreeMatPrices()
        {
            tree.RemoveAll(r => r.ParentId == 1);
            foreach (var item in _db.GetMatGroupPriceTypes(_grp_id).ToList())
            {
                TreeListBS.Add(new CatalogTreeList
                {
                    Id = tree.Max(m => m.Id) + 1,
                    ParentId = 1,
                    Text = item.Name,
                    ImgIdx = item.Dis == 1 ? 5 : 4,
                    TabIdx = 4,
                    DataSetId = item.PTypeId
                });
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
                _mat_grp_prices.Dis = 0;
                if (_db.Entry<MatGroupPrices>(_mat_grp_prices).State == System.Data.Entity.EntityState.Detached)
                {
                    _mat_grp_prices = _db.MatGroupPrices.Add(_mat_grp_prices);
                }
            }
            else
            {
                _mat_grp_prices.Dis = 1;
            }

            _db.SaveChanges();
        }

        private void GoTopMatPricesBtn_Click(object sender, EventArgs e)
        {
            DirTreeList.FocusedNode = DirTreeList.FindNodeByFieldValue("Id", 1);
        }

        private void DelIdividualMatPriceBtn_Click(object sender, EventArgs e)
        {
            var ds = MatPriceTypesBS.DataSource as dynamic;

            _db.MatGroupPrices.Remove(_db.MatGroupPrices.Find(ds.Id));
            _db.SaveChanges();

            GoTopMatPricesBtn.PerformClick();
        }

        private void AutoCalcPriceCheckEdit_CheckedChanged(object sender, EventArgs e)
        {
            AutoCalcGroupControl.Enabled = AutoCalcPriceCheckEdit.Checked;

            CustomPricePanel.Enabled = CustomPriceCheckEdit.Checked;

            if (AutoCalcPriceCheckEdit.Checked && AutoCalcPriceCheckEdit.ContainsFocus)
            {
                ExtraTypeLookUpEdit.EditValue = 0;
                _mat_grp_prices.PPTypeId = null;
                _mat_grp_prices.CurrId = null;
                _db.SaveChanges();
            }
        }

        private void checkEdit2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkEdit3.ContainsFocus)
            {
                ExtraTypeLookUpEdit.EditValue = 0;
                //_mat_prices.ExtraType = 0;
                _mat_grp_prices.PPTypeId = null;
                //_db._db.SaveChanges();
            }

            calcEdit2.Enabled = true;
            labelControl16.Enabled = true;
            lookUpEdit2.Enabled = true;

            calcEdit1.Enabled = true;
            ExtraTypeLookUpEdit.Enabled = true;
            lookUpEdit3.Enabled = true;
            lookUpEdit1.Enabled = true;


            if (checkEdit3.Checked)
            {
                calcEdit2.Enabled = false;
                labelControl16.Enabled = false;
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

        private void ExtraTypeLookUpEdit_EditValueChanged(object sender, EventArgs e)
        {
            var type = ExtraTypeLookUpEdit.EditValue == DBNull.Value ? 0 : (int)ExtraTypeLookUpEdit.EditValue;

            lookUpEdit3.Visible = (type == 2);
            lookUpEdit1.Visible = (type == 3);
        }

        private void checkEdit1_CheckedChanged(object sender, EventArgs e)
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

        private void CustomPriceCheckEdit_CheckedChanged(object sender, EventArgs e)
        {
            if (CustomPriceCheckEdit.Checked && CustomPriceCheckEdit.ContainsFocus)
            {
                _mat_grp_prices.ExtraType = 1;
                _mat_grp_prices.PPTypeId = null;
                CurrLookUpEdit.EditValue = DBHelper.Currency.FirstOrDefault(w => w.Def == 1).CurrId;
                _db.SaveChanges();
            }
        }

        private void NdsCheckEdit_CheckedChanged(object sender, EventArgs e)
        {
            if (NdsCheckEdit.ContainsFocus)
            {
                _mg.Nds = NdsCheckEdit.Checked ? DBHelper.CommonParam.Nds : -1;
                NdsEdit.EditValue = NdsCheckEdit.Checked ? DBHelper.CommonParam.Nds : null;
                GetNdsInfo();
            }
        }

        private void simpleButton6_Click(object sender, EventArgs e)
        {
            _mg.Nds = null;
            NdsEdit.EditValue = null;
            GetNdsInfo();
        }

        private void GetNdsInfo()
        {
            if (_mg.Nds == -1)
            {
                NdsLabel.Text = "- Даний товар не обкладається ПДВ";
            }
            if (_mg.Nds == null)
            {
                NdsLabel.Text = "- Ставка ПДВ взята з налаштувать групи до якої він належить";
            }
            if (_mg.Nds >= 0)
            {
                NdsLabel.Text = "- Ставка ПДВ встановлена індивідуально";
            }
        }

        private void DefectsClassifierTreeList_NodeChanged(object sender, DevExpress.XtraTreeList.NodeChangedEventArgs e)
        {
            ;
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            var new_node = _db.DefectsClassifier.Add(new DefectsClassifier { Name = "Текст", GrpId = _mg.GrpId });
            _db.SaveChanges();
            new_node.PId = new_node.Id ;
 
            DefectsClassifierBS.DataSource = _db.DefectsClassifier.Where(w => w.GrpId == _mg.GrpId).ToList();
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            var node1 = DefectsClassifierTreeList.GetFocusedRow() as DefectsClassifier;

            _db.DefectsClassifier.RemoveRange(_db.DefectsClassifier.Where(w => w.PId == node1.Id || w.Id == node1.Id));
            _db.SaveChanges();
            DefectsClassifierTreeList.FocusedNode.Remove();
        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            var node1 = DefectsClassifierTreeList.GetFocusedRow() as DefectsClassifier;

            var new_node = _db.DefectsClassifier.Add(new DefectsClassifier { Name = "Текст", GrpId = _mg.GrpId });
            _db.SaveChanges();
            new_node.PId = node1 == null ? new_node.Id : node1.Id;


            DefectsClassifierBS.DataSource = _db.DefectsClassifier.Where(w => w.GrpId == _mg.GrpId).ToList();

            DefectsClassifierTreeList.FocusedNode.Expand();
        }
    }
}
