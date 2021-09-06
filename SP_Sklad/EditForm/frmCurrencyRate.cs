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
    public partial class frmCurrencyRate : DevExpress.XtraEditors.XtraForm
    {
        private BaseEntities _db { get; set; }
        private DbContextTransaction current_transaction { get; set; }
        private int? _CurrId { get; set; }
        private Currency currency { get; set; }
        private List<CatalogTreeList> tree { get; set; }
        private DateTime? _OnDate { get; set; }
        private CurrencyRate _currency_rate { get; set; }

        public frmCurrencyRate(int? CurrId = null, DateTime? OnDate = null)
        {
            InitializeComponent();

            _CurrId = CurrId;
            _OnDate = OnDate;
            _db = DB.SkladBase();
            current_transaction = _db.Database.BeginTransaction();
            tree = new List<CatalogTreeList>();
        }

        private void frmCurrencyRate_Load(object sender, EventArgs e)
        {
            xtraTabControl1.ShowTabHeader = DevExpress.Utils.DefaultBoolean.False;

            tree.Add(new CatalogTreeList { Id = -2, ParentId = -2, Text = "Основна інформація", ImgIdx = 0, TabIdx = 0 });
            tree.Add(new CatalogTreeList { Id = 0, ParentId = -1, Text = "Курси валют", ImgIdx = 1, TabIdx = 1 });

            TreeListBindingSource.DataSource = tree;
            currency = _db.Currency.Find(_CurrId);

            var curr_on_date = _db.CurrencyRate.FirstOrDefault(w => w.OnDate == _OnDate && w.CurrId == _CurrId);
            if(curr_on_date == null)
            {
                _currency_rate = _db.CurrencyRate.Add(new CurrencyRate
                {
                    CurrId = _CurrId.Value,
                    OnDate = _OnDate.Value,
                    OnValue = 1
                });
            }

            CurrencyRateBS.DataSource = _currency_rate;


            CurrencyBS.DataSource = currency;
            GetCurrencyRate();

            DefCheckBox.Enabled = !DefCheckBox.Checked;
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            _db.SaveChanges();
            current_transaction.Commit();
        }

        private void frmCurrencyRate_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (current_transaction.UnderlyingTransaction.Connection != null)
            {
                current_transaction.Rollback();
            }

            _db.Dispose();
            current_transaction.Dispose();
        }

        private void GetCurrencyRate()
        {
            var list = _db.CurrencyRate.Where(w => w.CurrId == _CurrId).Select(s => new
            {
                s.RateId,
                s.OnDate,
                s.OnValue,
            }).ToList();

            BanksPersonsGridControl.DataSource = list;

         /*   tree.RemoveAll(r => r.ParentId == 0);
            foreach (var item in list)
            {
                tree.Add(new CatalogTreeList
                {
                    Id = tree.Count + 1, 
                    ParentId = 0,
                    Text = item.Name,
                    ImgIdx = 2,
                    TabIdx = 2,
                    DataSetId = item.PersonId
                });
            }*/

            DirTreeList.RefreshDataSource();

            DirTreeList.ExpandAll();
        }

        private void DirTreeList_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
        {
            _db.SaveChanges();

            var focused_tree_node = DirTreeList.GetDataRecordByNode(e.Node) as CatalogTreeList;

        /*    if (focused_tree_node.ParentId == 0)
            {
                BanksPersonsBS.DataSource = _db.BanksPersons.Find(focused_tree_node.DataSetId);
            }

            if (focused_tree_node.Id == 0)
            {
                var list = _db.BanksPersons.Where(w => w.BankId == bank.BankId).Select(s => new
                {
                    s.PersonId,
                    s.Name,
                    s.Job,
                }).ToList();
                BanksPersonsGridControl.DataSource = list;
            }*/

            xtraTabControl1.SelectedTabPageIndex = focused_tree_node.TabIdx;
        }

        private void AddRecDetBtn_Click(object sender, EventArgs e)
        {
          /*  xtraTabControl1.SelectedTabPageIndex = 2;
            var new_det = _db.BanksPersons.Add(new BanksPersons { BankId = bank.BankId, Name = "" });
            BanksPersonsBS.DataSource = new_det;
            _db.SaveChanges();
            GetCurrencyRate();

            DirTreeList.FocusedNode = DirTreeList.GetNodeList().FirstOrDefault(w => Convert.ToInt32(w.GetValue("DataSetId")) == new_det.PersonId);
            */
        }

        private void EditRecDetBtn_Click(object sender, EventArgs e)
        {
            dynamic det_item = BanksPersonsGridView.GetFocusedRow();
            if (det_item == null) return;

            xtraTabControl1.SelectedTabPageIndex = 2;
            CurrencyRateBS.DataSource = _db.BanksPersons.Find(det_item.PersonId);

            DirTreeList.FocusedNode = DirTreeList.GetNodeList().FirstOrDefault(w => Convert.ToInt32(w.GetValue("DataSetId")) == det_item.PersonId);
        }

        private void DelRecDetBtn_Click(object sender, EventArgs e)
        {
            /*
            dynamic det_item = BanksPersonsGridView.GetFocusedRow();
            if (det_item == null) return;

            _db.BanksPersons.Remove(_db.BanksPersons.Find(det_item.PersonId));
            _db.SaveChanges();

            GetPersons();
            */
        }

        private void simpleButton7_Click(object sender, EventArgs e)
        {
        /*    _db.BanksPersons.Remove(BanksPersonsBS.DataSource as BanksPersons);
            _db.SaveChanges();

            GetPersons();
            simpleButton11.PerformClick();*/
        }

        private void simpleButton11_Click(object sender, EventArgs e)
        {
            DirTreeList.FocusedNode = DirTreeList.GetNodeList().FirstOrDefault(w => Convert.ToInt32(w.GetValue("Id")) == 0);
        }

        private void BanksPersonsGridView_DoubleClick(object sender, EventArgs e)
        {
            EditRecDetBtn.PerformClick();
        }

        private void textEdit5_EditValueChanged(object sender, EventArgs e)
        {
            if (textEdit5.ContainsFocus)
            {
                DirTreeList.FocusedNode.SetValue("Text", textEdit5.EditValue);
            }
        }

        private void frmCurrencyRate_Shown(object sender, EventArgs e)
        {
            labelControl3.Text = $"1 {currency.ShortName} =";
        }
    }
}
