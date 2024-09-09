using System;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using SP_Sklad.SkladData;
using SP_Sklad.EditForm;
using SP_Sklad.Common;
using DevExpress.Data;
using SP_Sklad.WBForm;
using System.ComponentModel;

namespace SP_Sklad.MainTabs
{
    public partial class ucKagents : DevExpress.XtraEditors.XtraUserControl
    {
        public int? KType { get; set; }
        public int? FunId { get; set; }
        public bool isDirectList { get; set; }
        private int _ka_archived { get; set; }
        private bool _show_rec_archived { get; set; }
        public KagentListView focused_kagent => KaGridView.GetFocusedRow() is NotLoadedObject ? null : KaGridView.GetFocusedRow() as KagentListView;
        private UserAccess user_access { get; set; }

        private string reg_layout_path = "ucKagents\\KaGridView";

        [Browsable(true)]
        public event EventHandler KaGridViewDoubleClick
        {
            add => this.KaGridView.DoubleClick += value;
            remove => this.KaGridView.DoubleClick -= value;
        }

        public ucKagents()
        {
            InitializeComponent();
            _ka_archived = 0;
            _show_rec_archived = false;

        }

        private bool _restore = false;

         int kagent_restore_row = 0;
        int kagent_restore_top_row = 0;

        System.IO.Stream wh_layout_stream = new System.IO.MemoryStream();
        private void DirectoriesUserControl_Load(object sender, EventArgs e)
        {
            KaGridView.SaveLayoutToStream(wh_layout_stream);

            KaGridView.RestoreLayoutFromRegistry(IHelper.reg_layout_path + reg_layout_path);

            if (!DesignMode)
            {
                KagentSaldoGridColumn.Visible = (DBHelper.CurrentUser.ShowBalance == 1);
                KagentSaldoGridColumn.OptionsColumn.ShowInCustomizationForm = KagentSaldoGridColumn.Visible;
            }
        }

        protected override void OnCreateControl()
        {
            base.OnCreateControl();

            this.ParentForm.FormClosing += new FormClosingEventHandler(ParentForm_FormClosing);
        }

        void ParentForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            KaGridView.SaveLayoutToRegistry(IHelper.reg_layout_path + reg_layout_path);
        }

        public void GetData(bool restore = true)
        {
            LoginGridColumn.Visible = KType == 2;

            kagent_restore_row = KaGridView.FocusedRowHandle;
            kagent_restore_top_row = KaGridView.TopRowIndex;
            _restore = restore;

            KaGridControl.DataSource = null;
            KaGridControl.DataSource = KagentListSource;

            GetKontragentDetail();
        }

        private void RefrechItemBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            GetData( true);

            DBHelper.ReloadKagents();
        }

     
        private void EditItemBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DialogResult result = DialogResult.Cancel;

            if (focused_kagent != null)
            {
                result = new frmKAgentEdit(null, focused_kagent.KaId).ShowDialog();
            }

            if (result == DialogResult.OK)
            {
                RefrechItemBtn.PerformClick();
            }
        }

        private void NewItemBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            using (var k_frm = new frmKAgentEdit(KType : KType))
            {
                k_frm.ShowDialog();
            }

            RefrechItemBtn.PerformClick();
        }

        private void DeleteItemBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (MessageBox.Show("Ви дійсно бажаєте відалити цей запис з довідника?", "Підтвердіть видалення", MessageBoxButtons.YesNo, MessageBoxIcon.Information) != DialogResult.Yes)
            {
                return;
            }


            int KaId = focused_kagent.KaId;
            decimal? Saldo = focused_kagent.Saldo;

            if ((Saldo ?? 0) != 0)
            {
                MessageBox.Show("Не можливо видяляти, є залишок по контрагенту");
                return;
            }

            using (var _db = DB.SkladBase())
            {
                var item = _db.Kagent.Find(KaId);
                if (item != null)
                {
                    item.UserId = null;
                    item.Deleted = 1;
                }
                _db.SaveChanges();
            }

            RefrechItemBtn.PerformClick();
        }

        private void KaGridView_DoubleClick(object sender, EventArgs e)
        {
            if (isDirectList)
            {
                return;
            }
            else
            {
                EditItemBtn.PerformClick();
            }
        }

        private void KaGridView_PopupMenuShowing(object sender, DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs e)
        {
            if (e.HitInfo.InRow)
            {
                KAgentPopupMenu.ShowPopup(Control.MousePosition);
            }
        }


        private void KagentBalansBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            IHelper.ShowKABalans(focused_kagent.KaId);
        }


        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            IHelper.ShowOrdered(focused_kagent.KaId, 0, 0);
        }


        private void barCheckItem1_CheckedChanged_1(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            _ka_archived = ShowKagentArchiveRecordBarCheckItem.Checked ? 1 : 0;
            KagentGridColumnArchived.Visible = ShowKagentArchiveRecordBarCheckItem.Checked;
            RefrechItemBtn.PerformClick();
        }

        private void barButtonItem10_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var db = DB.SkladBase();
            var ka = db.Kagent.Find(focused_kagent.KaId);

            if (ka == null)
            {
                return;
            }

            if (ka.Archived == 1)
            {
                ka.Archived = 0;
            }
            else
            {
                if (MessageBox.Show(string.Format("Ви дійсно хочете перемістити контрагента <{0}> в архів?", ka.Name), "Information", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    if (focused_kagent.Saldo == 0 || focused_kagent.Saldo == null)
                    {
                        ka.Archived = 1;
                    }
                    else
                    {
                        MessageBox.Show(string.Format("Неможливо перемістити контрагента <{0}> в архів, \nтому що його поточний баланс рівний {1}", ka.Name, focused_kagent.Saldo.ToString()));
                    }
                }
            }
            db.SaveChanges();
            RefrechItemBtn.PerformClick();
        }

        private void barButtonItem11_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            RefrechItemBtn.PerformClick();
        }

        private void KaGridView_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            if (e.Column.FieldName == "Saldo")
            {
                if ( e.CellValue is NotLoadedObject || e.CellValue == null || e.CellValue == DBNull.Value  )
                {
                    return;
                }

                var saldo =  Convert.ToInt32(e.CellValue);

                if (saldo < 0)
                {
                    e.Appearance.ForeColor = Color.Red;
                }
                else
                {
                    e.Appearance.ForeColor = Color.Blue;
                }
            }
        }

        private void CopyItemBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            RefrechItemBtn.PerformClick();
        }

        private void barButtonItem11_ItemClick_1(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            IHelper.ExportToXlsx(KaGridControl);
        }

        private void GetKontragentDetail()
        {
            user_access = DB.SkladBase().UserAccess.FirstOrDefault(w => w.FunId == FunId && w.UserId == UserSession.UserId);

            NewItemBtn.Enabled = user_access.CanInsert == 1;
            DeleteItemBtn.Enabled = (focused_kagent != null && user_access.CanDelete == 1);
            EditItemBtn.Enabled = (focused_kagent != null && user_access.CanModify == 1);
            CopyItemBtn.Enabled = (focused_kagent != null && user_access.CanInsert == 1);

            if (focused_kagent == null)
            {
                gridControl4.DataSource = null;
                gridControl1.DataSource = null;
                gridControl3.DataSource = null;
                KAgentInfoBS.DataSource = null;
                memoEdit1.Text = "";

                return;
            }

            using (var db = DB.SkladBase())
            {
                int KaId = focused_kagent.KaId;

                var kagent = db.v_Kagent.AsNoTracking().FirstOrDefault(w => w.KaId == KaId);

                KAgentInfoBS.DataSource = kagent;
                memoEdit1.Text = kagent.Notes;

                gridControl3.DataSource = db.KAgentPersons.Where(w => w.KAId == KaId).Join(db.Jobs,
                                    person => person.JobType,
                                    job => job.Id,
                                    (person, job) => new { person.Name, person.Notes, person.Phone, person.Email, Post = person.JobType == 0 ? person.Post : job.Name }).ToList();

                gridControl1.DataSource = db.KAgentAccount.Where(w => w.KAId == KaId).Select(s => new { s.AccNum, s.Banks.MFO, BankName = s.Banks.Name, TypeName = s.AccountType.Name }).ToList();

                gridControl4.DataSource = db.EnterpriseWorker.Where(w => w.EnterpriseId == KaId).Join(db.Kagent, p => p.WorkerId, k => k.KaId, (p, k) => new { k.KaId, k.Name }).ToList();
            }
        }

        private void KaGridView_FocusedRowObjectChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventArgs e)
        {
            GetKontragentDetail();
        }

        private void gridView5_DoubleClick(object sender, EventArgs e)
        {
            var row = gridView5.GetFocusedRow() as dynamic;
            using (var frm = new frmKAgentEdit(null, row.KaId))
            {
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    RefrechItemBtn.PerformClick();
                }
            }
        }

        private void KagentListSource_GetQueryable(object sender, DevExpress.Data.Linq.GetQueryableEventArgs e)
        {
            if (!KType.HasValue)
            {
                return;
            }

            var _db = DB.SkladBase();

            var ka = (from k in _db.KagentList
                      join ek in _db.EnterpriseWorker on k.KaId equals ek.WorkerId into gj_ek
                      from subfg in gj_ek.DefaultIfEmpty()
                      join ew in _db.EnterpriseWorker on subfg.EnterpriseId equals ew.EnterpriseId into gj_ew
                      from subfg2 in gj_ew.DefaultIfEmpty()
                      where (subfg == null || subfg2.WorkerId == DBHelper.CurrentUser.KaId) 
                      select new
                      {
                          k.KaId,
                          k.KType,
                          k.Archived,
                          k.Name,
                          k.GroupName,
                          k.PriceName,
                          k.KAgentKind,
                          k.OKPO,
                          k.INN,
                          k.JobName,
                          k.Login,
                          k.WebUserName,
                          k.Saldo,
                          k.PTypeId,
                          k.RouteName,
                          k.WhName,
                          k.WId
                      }).Distinct();

            if (KType >= 0)
            {
                ka = ka.Where(w => w.KType == KType);
            }

            if (_ka_archived == 0)
            {
                ka = ka.Where(w => w.Archived == 0 || w.Archived == null);
            }

            e.QueryableSource = ka.Select(s => new KagentListView
            {
                Archived = s.Archived,
                GroupName = s.GroupName,
                INN = s.INN,
                JobName = s.JobName,
                KAgentKind = s.KAgentKind,
                KaId = s.KaId,
                KType = s.KType,
                Login = s.Login,
                Name = s.Name,
                OKPO = s.OKPO,
                PriceName = s.PriceName,
                PTypeId = s.PTypeId,
                RouteName = s.RouteName,
                Saldo = s.Saldo,
                WebUserName = s.WebUserName,
                WhName = s.WhName,
                WId = s.WId
            });

            e.Tag = _db;
        }

        public class KagentListView
        {
            public int KaId { get; set; }
            public int KType { get; set; }
            public int? Archived { get; set; }
            public string Name { get; set; }
            public string GroupName { get; set; }
            public string PriceName { get; set; }
            public string KAgentKind { get; set; }
            public string OKPO { get; set; }
            public string INN { get; set; }
            public string JobName { get; set; }
            public string Login { get; set; }
            public string WebUserName { get; set; }
            public decimal? Saldo { get; set; }
            public int? PTypeId { get; set; }
            public string RouteName { get; set; }
            public string WhName { get; set; }
            public int? WId { get; set; }
        }

        private void KaGridView_AsyncCompleted(object sender, EventArgs e)
        {
            if (!_restore)
            {
                return;
            }

            KaGridView.FocusedRowHandle = kagent_restore_row;
            KaGridView.TopRowIndex = kagent_restore_top_row;

            _restore = false;
        }

        private void barButtonItem7_ItemClick_1(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        { 
            using (var _db =  DB.SkladBase())
            {
                int? p_type = focused_kagent.PTypeId;
                int? ka_id = focused_kagent.KaId;

                var pl = _db.PriceList.Add(new PriceList
                {
                    Id = Guid.NewGuid(),
                    Deleted = 0,
                    UseLogo = 0,
                    CurrId = 2,
                    OnDate = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                    UpdatedBy = DBHelper.CurrentUser.UserId,
                    PTypeId = p_type,
                    Name = $"ПЛ-{focused_kagent.Name}"
                });
                _db.SaveChanges();

                foreach (var mat_item in _db.v_Materials.Where(w => (w.TypeId == 1 || w.TypeId == 5) && w.Archived == 0).Select(s => new { s.MatId, s.GrpId, s.WId }).ToList())
                {
                    var mat_price = _db.GetMatPrice(mat_item.MatId, pl.CurrId, p_type, ka_id).FirstOrDefault();
                    var dis = _db.GetDiscount(ka_id, mat_item.MatId).FirstOrDefault();
                    var discount = dis.DiscountType == 0 ? dis.Discount : (mat_price?.Price > 0 ? (dis.Discount / mat_price.Price * 100) : 0);

                    _db.PriceListDet.Add(new PriceListDet
                    {
                        PlId = pl.PlId,
                        MatId = mat_item.MatId,
                        Price = mat_price?.Price ?? 0,
                        GrpId = mat_item.GrpId,
                        PlDetType = 0,
                        Discount = discount,
                        WId = mat_item.WId
                    });
                }
                _db.SaveChanges();

                using (var frm = new frmPriceList(pl.PlId))
                {
                    frm.KagentComboBox.EditValue = ka_id;
                    if (frm.ShowDialog() != DialogResult.OK)
                    {
                        _db.PriceList.Remove(pl);
                        _db.SaveChanges();
                    }
                }
            }
        }


        private void barButtonItem9_ItemClick_1(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DB.SkladBase().RecalcKaSaldo(focused_kagent.KaId);

            RefrechItemBtn.PerformClick();
        }

        private void KaGridView_ColumnFilterChanged(object sender, EventArgs e)
        {
            GetKontragentDetail();
        }

        private void vGridControl4_CellValueChanged(object sender, DevExpress.XtraVerticalGrid.Events.CellValueChangedEventArgs e)
        {
            if (e.Row.Properties.FieldName == "Id" && DBHelper.is_admin)
            {
                using (var _db = DB.SkladBase())
                {
                    var k = _db.Kagent.FirstOrDefault(w => w.KaId == focused_kagent.KaId);
                    k.Id = new Guid((string)e.Value) ;
                    _db.SaveChanges();

                    KAgentInfoBS.DataSource = _db.v_Kagent.AsNoTracking().FirstOrDefault(w => w.KaId == focused_kagent.KaId); 
                }
            }
        }

        private void repositoryItemButtonEdit1_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 0)
            {
                IHelper.ShowRemainsInWh(focused_kagent.WId.Value, focused_kagent.WhName);
            }
        }
    }
}
