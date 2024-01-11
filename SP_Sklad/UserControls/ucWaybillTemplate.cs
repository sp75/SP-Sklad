using DevExpress.XtraEditors;
using SP_Sklad.Common;
using SP_Sklad.EditForm;
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
    public partial class ucWaybillTemplate : DevExpress.XtraEditors.XtraUserControl
    {
        public frmWaybillTemplateView view_frm { get; set; }

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

        public ucWaybillTemplate()
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
            WaybillTemplateBS.DataSource = DB.SkladBase().WaybillTemplate.AsNoTracking().OrderBy(o=> o.Num).ToList();
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
            if (view_frm != null)
            {
                view_frm.OkButton.PerformClick();
            }
            else if (EditFocusedRow() == DialogResult.OK)
            {
                GetDataList();
            }
        }

        private void WaybillTemplateGridView_FocusedRowObjectChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventArgs e)
        {
            if(wbt_row == null)
            {
                return;
            }

            WaybillTemplateDetBS.DataSource = DB.SkladBase().v_WaybillTemplateDet.AsNoTracking().Where(w => w.WaybillTemplateId == wbt_row.Id).OrderBy(o=> o.Num).ToList();
            WaybillTemplateDetGrid.ExpandAllGroups();

            GetKagentList();
        }

        private void  GetKagentList()
        {
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
                        Nds = DBHelper.Enterprise.NdsPayer == 1 ? DBHelper.CommonParam.Nds : 0,
                        KaId = kagent.KaId,
                    });
                    db.SaveChanges();

                    //       ExecuteDocument.ExecuteWaybillTemplate(wbt_row.Id, _wb, db);

                    db.CreateOrderByWBTemplate (_wb.WbillId, wbt_row.Id);

                    ++wb_count;
                }

                XtraMessageBox.Show(string.Format("Створено {0} замовлень !", wb_count));
            }
        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var ka_id = IHelper.ShowDirectList(null, 1);
            if(ka_id != null)
            {
                using (var db = DB.SkladBase())
                {
                    var ka = db.Kagent.Find((int)ka_id);
                    ka.WaybillTemplate.Add(db.WaybillTemplate.Find(wbt_row.Id));
                    db.SaveChanges();
                }

                GetKagentList();
            }
        }

        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (XtraMessageBox.Show("Ви дійсно бажаете видалити привязку з контрагентом ?", "Видалити запис", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {

                var row = KaTemplateListGridView.GetFocusedRow() as KaTemplateList;

                if (row != null)
                {
                    using (var db = DB.SkladBase())
                    {
                        var ka = db.Kagent.Find(row.KaId);
                        ka.WaybillTemplate.Remove(db.WaybillTemplate.Find(wbt_row.Id));
                        db.SaveChanges();
                    }

                    GetKagentList();
                }
            }
        }
    }
}
