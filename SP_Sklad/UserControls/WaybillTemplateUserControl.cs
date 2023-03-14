using SP_Sklad.Common;
using SP_Sklad.SkladData;
using SP_Sklad.WBForm;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SP_Sklad.UserControls
{
    public partial class WaybillTemplateUserControl : UserControl
    {
        public WaybillTemplate wbt_row
        {
            get
            {
                return WaybillTemplateGridView.GetFocusedRow() as WaybillTemplate;
            }
        }

        private class KaTemplateList
        {
            public bool Check { get; set; }
            public int KaId { get; set; }
            public string KaName { get; set; }
        }
        private List<KaTemplateList> ka_template_list { get; set; }

        public WaybillTemplateUserControl()
        {
            InitializeComponent();
            ka_template_list = new List<KaTemplateList>();
        }

        private void WaybillTemplateUserControl_Load(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                ;
            }
        }

        public void GetDataList()
        {
            WaybillTemplateBS.DataSource = DB.SkladBase().WaybillTemplate.OrderBy(o=> o.Num).ToList();
        }

        public DialogResult EditFocusedRow()
        {
            if (wbt_row == null)
            {
                return DialogResult.Cancel;
            }

            return new frmWaybillTemplate(wbt_row.Id).ShowDialog();
        }

        public void DeleteFocusedRow()
        {
            if(wbt_row == null)
            {
                return;
            }

            DB.SkladBase().DeleteWhere<WaybillTemplate>(w => w.Id == wbt_row.Id);
        }

        public void AddNewItem()
        {
            new frmWaybillTemplate().ShowDialog();
        }

        private void PriceListGridView_DoubleClick(object sender, EventArgs e)
        {
            EditFocusedRow();
        }

        private void WaybillTemplateGridView_FocusedRowObjectChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventArgs e)
        {
            if(wbt_row == null)
            {
                return;
            }

            WaybillTemplateDetBS.DataSource = DB.SkladBase().v_WaybillTemplateDet.AsNoTracking().Where(w => w.WaybillTemplateId == wbt_row.Id).OrderBy(o=> o.Num).ToList();
            WaybillTemplateDetGrid.ExpandAllGroups();

            ka_template_list = DB.SkladBase().WaybillTemplate.FirstOrDefault(w => w.Id == wbt_row.Id).Kagent.Select(s => new KaTemplateList
            {
                Check = true,
                KaId = s.KaId,
                KaName = s.Name
            }).ToList();

            KaTemplateListGridControl.DataSource = ka_template_list;
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            KaTemplateListGridView.CloseEditor();

            using (var db = DB.SkladBase())
            {
                int wb_count = 0;
                foreach (var kagent in ka_template_list.Where(w => w.Check))
                {
                    var _wb = db.WaybillList.Add(new WaybillList()
                    {
                        Id = Guid.NewGuid(),
                        WType = -16,
                        OnDate = DBHelper.ServerDateTime(),
                        Num = db.GetDocNum("wb(-16)").FirstOrDefault(),
                        CurrId = DBHelper.Currency.FirstOrDefault(w => w.Def == 1).CurrId,
                        OnValue = 1,
                        PersonId = DBHelper.CurrentUser.KaId,
                        EntId = DBHelper.Enterprise.KaId,
                        UpdatedBy = DBHelper.CurrentUser.UserId,
                        Nds = 0,
                        KaId = kagent.KaId,

                    });
                    db.SaveChanges();

                    ExecuteDocument.ExecuteWaybillTemplate(wbt_row.Id, _wb, db);

                    ++wb_count;
                }

                MessageBox.Show(string.Format("Створено {0} замовлень !", wb_count));
            }
        }
    }
}
