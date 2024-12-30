using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.ComponentModel.DataAnnotations;
using SP.Reports;
using System.IO;
using System.Collections;
using SP_Sklad.SkladData;
using OpenStore.Tranzit.Base;
using SP_Sklad.Common;

namespace SP_Sklad.Interfaces.Tablet.UI
{
    public partial class ucTabletReport27 : DevExpress.XtraEditors.XtraUserControl
    {
        public ucTabletReport27()
        {
            InitializeComponent();

            windowsUIButtonPanel.BackColor = new Color();
        }

        void windowsUIButtonPanel_ButtonClick(object sender, DevExpress.XtraBars.Docking2010.ButtonEventArgs e)
        {
            if (e.Button.Properties.VisibleIndex == 0)
            {
                RefreshGrid();
            }
            else if(e.Button.Properties.VisibleIndex == 1)
            {
                IHelper.ExportToXlsx(gridControl1);
            }

        }

        private List<REP_27> GetDataSource()
        {
            var area = KagentList.GetSelectedDataRow() as Kontragent;
            string ka_ids = "";
          

            if (area.KaId == -1)
            {
                ka_ids = string.Join(", ", DBHelper.EmployeeKagentList.Where(w => w.WId != null).Select(s => s.KaId).ToList());
            }
            else
            {
                ka_ids = area.KaId.ToString();
            }

            var list = new BaseEntities().Database.SqlQuery<REP_27>(@"
    select wbd.MatId, mat.Name MatName, sum(wbd.Amount) AmountOrd, sum(wbd.Total) TotalOrd,  wbl.KaId,
           ka.name KaName, msr.shortname MsrName
		   , sum(wbdo.Amount) AmountOut, sum(wbdo.Total) TotalOut , mg.Name GrpName, mat.Artikul
    from WAYBILLDET wbd 
	  join  WAYBILLLIST wbl on wbl.wbillid = wbd.wbillid
      join MATERIALS mat on mat.matid = wbd.MatId
      join measures msr on msr.mid=mat.mid
      join kagent ka on wbl.kaid = ka.kaid
	  join MatGroup mg on mg.GrpId = mat.GrpId
	  left outer join KontragentGroup ka_grp on ka_grp.Id = ka.GroupId
	  left outer join OrderedRels ordr on ordr.OrdPosId = wbd.PosId
	  left outer join WAYBILLDET wbdo on ordr.OutPosId = wbdo.PosId
	  left outer join DOCRELS dl on dl.RelOriginatorId = wbl.id
	  left outer join  WAYBILLLIST wblo on wblo.Id = dl.OriginatorId and wblo.WType = -1
	  left outer join kagent kaemp on wblo.personid = kaemp.kaid

    where wbl.wbillid =   wbd.wbillid  and wbl.wtype = -16
          and wbl.ondate between {0} and {1}
          and wbl.kaid in ({ka_ids}) 
    group by wbl.KaId ,wbd.MatId, mat.Name, ka.Name , msr.shortname, mg.Name, mat.Artikul".Replace("{ka_ids}", ka_ids), wbStartDate.DateTime, wbEndDate.DateTime).ToList();

            return list;
        }

        public class REP_27
        {
            [Display(Name = "Код товару")]
            public int MatId { get; set; }
            [Display(Name = "Код контрагента")]
            public int? KaId { get; set; }
            [Display(Name = "Назва товару")]
            public string MatName { get; set; }

            [Display(Name = "Група товарів")]
            public string GrpName { get; set; }

            [Display(Name = "Замовлено, к-сть")]
            public decimal? AmountOrd { get; set; }

            [Display(Name = "Замовлено, сума")]
            public Nullable<decimal> TotalOrd { get; set; }

            [Display(Name = "Контрагент")]
            public string KaName { get; set; }

            [Display(Name = "Од. вим.")]
            public string MsrName { get; set; }

            [Display(Name = "Артикул")]
            public string Artikul { get; set; }

            [Display(Name = "Відгружено, к-сть")]
            public Nullable<decimal> AmountOut { get; set; }

            [Display(Name = "Відгружено, сума")]
            public Nullable<decimal> TotalOut { get; set; }

            [Display(Name = "Різниця, к-сть")]
            public decimal TotalAmount => (AmountOut ?? 0) - (AmountOrd ?? 0);

            [Display(Name = "Різниця, сума")]
            public decimal TotalSum => (TotalOut ?? 0) - (TotalOrd ?? 0);
        }


        public void RefreshGrid()
        {
            var h = gridView1.FocusedRowHandle;
            var top_r = gridView1.TopRowIndex;

            gridControl1.DataSource = GetDataSource();

            gridView1.TopRowIndex = top_r;
            gridView1.FocusedRowHandle = h;
        }

        private void ucCurrentReturned_Load(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                KagentList.Properties.DataSource = DBHelper.EmployeeKagentList;
                KagentList.EditValue = -1;

                PeriodComboBoxEdit.SelectedIndex = 1;

                var user_settings = new UserSettingsRepository(DBHelper.CurrentUser.UserId, new BaseEntities());
                gridView1.Appearance.Row.Font = new Font(user_settings.GridFontName, (float)user_settings.GridFontSize);
            }
        }
        private void PeriodComboBoxEdit_EditValueChanged(object sender, EventArgs e)
        {
            wbEndDate.DateTime = DateTime.Now.Date.SetEndDay();
            switch (PeriodComboBoxEdit.SelectedIndex)
            {
                case 1:
                    wbStartDate.DateTime = DateTime.Now.Date;
                    break;

                case 2:
                    wbStartDate.DateTime = DateTime.Now.Date.StartOfWeek(DayOfWeek.Monday);
                    break;

                case 3:
                    wbStartDate.DateTime = DateTime.Now.Date.FirstDayOfMonth();
                    break;

                case 4:
                    wbStartDate.DateTime = new DateTime(DateTime.Now.Year, 1, 1);
                    break;
            }

            RefreshGrid();
        }

        private void wbStartDate_EditValueChanged(object sender, EventArgs e)
        {
            if (wbStartDate.ContainsFocus)
            {
                RefreshGrid();
            }
        }

        private void wbEndDate_EditValueChanged(object sender, EventArgs e)
        {
            if (wbEndDate.ContainsFocus)
            {
                RefreshGrid();
            }
        }

        private void KagentList_EditValueChanged(object sender, EventArgs e)
        {
            if (KagentList.ContainsFocus)
            {
                RefreshGrid();
            }
        }
    }
}
