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
using SkladEngine.DBFunction;
using SkladEngine.DBFunction.Models;

namespace SP_Sklad.Interfaces.Tablet.UI
{
    public partial class ucTabletWarehouse : DevExpress.XtraEditors.XtraUserControl
    {
        public ucTabletWarehouse()
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

        private List<MaterialsOnWh> GetDataSource()
        {
            var area = KagentList.GetSelectedDataRow() as Kontragent;
            string wids = "";
            List<int?> ka_ids = new List<int?>();

            if(area.KaId == -1)
            {
                wids = string.Join(", ", DBHelper.EmployeeKagentList.Where(w => w.WId != null).Select(s => s.WId).ToList());
                ka_ids = DBHelper.EmployeeKagentList.Where(w => w.WId != null && w.OpenStoreAreaId != null).Select(s => s.OpenStoreAreaId).ToList();
            }
            else
            {
                wids = area.WId.ToString();
                ka_ids.Add(area.OpenStoreAreaId);
            }

            Tranzit_OSEntities objectContext = new Tranzit_OSEntities();
            var sales = objectContext.v_Sales.Where(w => w.SESSEND == null && ka_ids.Contains(w.SAREAID)).GroupBy(g => new { g.ARTID }).Select(s => new
            {
                s.Key.ARTID,
                Amount = s.Sum(sa => sa.AMOUNT)
            }).ToList();

            var list = new MaterialRemain(UserSession.UserId).GetMaterialsOnWh(wids).Select(s => new MaterialsOnWh
            {
                Artikul = s.Artikul,
                AvgPrice = s.AvgPrice,
                CurRemain = s.CurRemain,
                GrpName = s.GrpName,
                MatId = s.MatId,
                MatName = s.MatName,
                MsrName = s.MsrName,
                OpenStoreId = s.MatId,
                Remain = s.Remain,
                TypeId = s.TypeId,
                Rsv = s.Rsv,
                SumRemain = s.SumRemain,
                AmountSold = sales.Where(w => w.ARTID == s.OpenStoreId).Select(ss => ss.Amount).FirstOrDefault() 
            }).ToList(); ;

            return list;
        }

        public class MaterialsOnWh
        {
            public int MatId { get; set; }
            public string MatName { get; set; }
            public string MsrName { get; set; }
            public decimal Remain { get; set; }
            public decimal Rsv { get; set; }
            public decimal CurRemain { get; set; }
            public string Artikul { get; set; }
            public decimal? AvgPrice { get; set; }
            public string GrpName { get; set; }
            public decimal? SumRemain { get; set; }
            public int? OpenStoreId { get; set; }
            public int? TypeId { get; set; }
            public decimal? AmountSold { get; set; }
            public decimal? TotalRemain => CurRemain - AmountSold;
        }

        public void RefreshGrid()
        {
            var h = bandedGridView1.FocusedRowHandle;
            var top_r = bandedGridView1.TopRowIndex;

            gridControl1.DataSource = GetDataSource();
            //     gridView1.ExpandAllGroups();

            bandedGridView1.TopRowIndex = top_r;
            bandedGridView1.FocusedRowHandle = h;
        }

        private void ucCurrentReturned_Load(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                KagentList.Properties.DataSource = DBHelper.EmployeeKagentList;
                KagentList.EditValue = -1;

                var user_settings = new UserSettingsRepository(DBHelper.CurrentUser.UserId, new BaseEntities());
                bandedGridView1.Appearance.Row.Font = new Font(user_settings.GridFontName, (float)user_settings.GridFontSize);

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
