using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using SP_Sklad.SkladData;
using DevExpress.XtraGrid.Views.Grid;
using System.Collections;
using SP.Reports;
using System.IO;
using SP_Sklad.Common;
using SP.Reports.Models.Views;

namespace SP_Sklad.ViewsForm
{
    public partial class frmReport54 : DevExpress.XtraEditors.XtraForm
    {
        private BaseEntities _db { get; set; }


        public frmReport54()
        {
            InitializeComponent();
            _db = DB.SkladBase();
        }

        public class XLRPARAM
        {
            public string OnDate { get; set; }
            public string OnDate1 { get; set; }
            public string OnDate2 { get; set; }
            public string WhName { get; set; }
        }

        private List<XLRPARAM> XLR_PARAMS
        {
            get
            {
                var wh_row = WhComboBox.GetSelectedDataRow() as dynamic;

                var obj = new List<XLRPARAM>
                {
                    new XLRPARAM
                    {
                        OnDate = OnDateDBEdit.DateTime.ToShortDateString(),
                        OnDate1 = dateEdit1.DateTime.ToShortDateString(),
                        WhName = wh_row.Name
                    }
                };

                return obj;
            }
        }

        private void frmKaGroup_Load(object sender, EventArgs e)
        {
            OnDateDBEdit.DateTime = DateTime.Now;
            dateEdit1.DateTime = DateTime.Now;

            var wh = new BaseEntities().Warehouse.Where(w => w.UserAccessWh.Any(a => a.UserId == DBHelper.CurrentUser.UserId)).Select(s => new { WId = s.WId, s.Name, s.Def }).ToList();

            WhComboBox.Properties.DataSource = new List<object>() { new  { WId = 0, Name = "Усі" } }.Concat(wh.Select(s => new 
            {
                 s.WId,
                 s.Name
            }).ToList());

            WhComboBox.EditValue = 0;
        }

        private void frmKaGroup_FormClosed(object sender, FormClosedEventArgs e)
        {
    
        }

        private void KontragentGroupBS_AddingNew(object sender, AddingNewEventArgs e)
        {
            e.NewObject = _db.KontragentGroup.Add(new KontragentGroup() { Id = Guid.NewGuid() });
        }
        public class rep_54
        {
            public int MatId { get; set; }
            public int GrpId { get; set; }
            public string MatName { get; set; }
            public string GrpName { get; set; }
            public decimal? Remain { get; set; }
            public decimal? OrderedAmount { get; set; }
            public decimal? MakeAmount { get; set; }
            public string Artikul { get; set; }
            public string MsrName { get; set; }
        }
        private List<rep_54> GetData()
        {
            var wh_row = WhComboBox.GetSelectedDataRow() as dynamic;
            int wid = wh_row.WId;

            var sql = @"select 
   wbd.MatId, 
   m.Name MatName, mg.Name GrpName, mg.GrpId, m.Artikul, Measures.ShortName MsrName,
   sum(wbd.Amount) OrderedAmount,
   coalesce((select sum(wbm.AmountByRecipe * (mr.Out/100)) from WaybillList wbl , WayBillMake wbm, MatRecipe mr where wbm.RecId = mr.RecId and mr.MatId = wbd.MatId and wbm.WbillId = wbl.WbillId and wbl.WType = -20 and wbl.Checked in(0, 2) ),0) MakeAmount,
   (select 
				   sum( pr.remain) Remain
		   from PosRemains pr
		   join (
						   SELECT 
							    PosId,
							    max(OnDate) OnDate
						   FROM PosRemains
					     where ondate <= {0}
					     group by [PosId]
						) x on x.PosId = pr.PosId and pr.OnDate = x.OnDate
        where  (pr.remain > 0 or Ordered > 0)  and (pr.WId = {2} or {2} = 0 ) and pr.MatId = wbd.MatId
    ) Remain 
from WaybillDet wbd
        inner join  WaybillList wbl on wbd.WbillId = wbl.WbillId
		inner join Materials m on m.MatId =  wbd.MatId  and m.TypeId in (1,5,6)
		inner join MatGroup mg on mg.GrpId = m.GrpId 
        inner join Measures on Measures.MId = m.MId
where   wbl.WType = -16 and wbl.OnDate between {1} and DATEADD (day, 1 , {1} ) 
group by wbd.MatId, m.Name , mg.Name , mg.GrpId, m.Artikul, Measures.ShortName
order by m.Artikul";


            return _db.Database.SqlQuery<rep_54>(sql, OnDateDBEdit.DateTime.Date, dateEdit1.DateTime.Date, wid).ToList();
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            string template_name = "ProductionPlanning(54).xlsx";
            var mat = GetData();

            if (!mat.Any())
            {
                return;
            }

            var ka_grp = mat.GroupBy(g => new { g.GrpId, g.GrpName }).Select(s => new
            {
                s.Key.GrpId,
                s.Key.GrpName
            }).OrderBy(o => o.GrpName).ToList();

              List<object> realation = new List<object>();
            Dictionary<string, IList> data_for_report =  new Dictionary<string, IList>();
            realation.Add(new
            {
                pk = "GrpId",
                fk = "GrpId",
                master_table = "MatGroup",
                child_table = "MatInDet"
            });

            data_for_report.Add("XLRPARAMS", XLR_PARAMS);
            data_for_report.Add("MatGroup", ka_grp);
            data_for_report.Add("MatInDet", mat);
            data_for_report.Add("_realation_", realation);

   
            var template_file = Path.Combine(IHelper.template_path, template_name);
            if (File.Exists(template_file))
            {
                var report_data = ReportBuilder.GenerateReport(data_for_report, template_file, false, "xlsx");
                if (report_data != null)
                {
                    IHelper.ShowReport(report_data, template_name);
                }
                else
                {
                    MessageBox.Show("За обраний період звіт не містить даних !");
                }
            }
            else
            {
                MessageBox.Show("Шлях до шаблонів " + template_file + " не знайдено!");
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            new frmGridView(Text, GetData()).ShowDialog();
        }
    }
}