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
using System.ComponentModel.DataAnnotations;

namespace SP_Sklad.ViewsForm
{
    public partial class frmReport58 : DevExpress.XtraEditors.XtraForm
    {
        private BaseEntities _db { get; set; }


        public frmReport58()
        {
            InitializeComponent();
            _db = DB.SkladBase();
        }

        public class XLRPARAM
        {
            public string StartDate { get; set; }
            public string EndDate { get; set; }
            public string RecipeName { get; set; }
        }

        private List<XLRPARAM> XLR_PARAMS
        {
            get
            {
                var rec_row = RecipeComboBox.GetSelectedDataRow() as dynamic;

                var obj = new List<XLRPARAM>
                {
                    new XLRPARAM
                    {
                        StartDate = StartDateEdit.DateTime.ToShortDateString(),
                        EndDate = EndDateEdit.DateTime.ToShortDateString(),
                        RecipeName = rec_row.Name
                    }
                };

                return obj;
            }
        }

        private void frmKaGroup_Load(object sender, EventArgs e)
        {
            StartDateEdit.DateTime = DateTime.Now;
            EndDateEdit.DateTime = DateTime.Now.SetEndDay();

            var rec_list = DB.SkladBase().MatRecipe.Where(w => w.RType == 2 && !w.Archived).Select(s => new
            {
                s.RecId,
                s.Name,
                s.Amount,
                MatName = s.Materials.Name,
                s.MatId
            }).ToList();
         
            RecipeComboBox.Properties.DataSource = rec_list;
            RecipeComboBox.EditValue = rec_list.FirstOrDefault()?.RecId;

            RecipeComboBox.Properties.Buttons[1].Enabled = _db.UserTreeAccess.Any(w => w.Id == 42 && w.UserId == DBHelper.CurrentUser.UserId && w.CanView == 1);
        }

        private void frmKaGroup_FormClosed(object sender, FormClosedEventArgs e)
        {
    
        }


        public class rep_58
        {
            [Display(Name = "Код товару")]
            public int MatId { get; set; }
            [Display(Name = "Назва")]
            public string MatName { get; set; }
            [Display(Name = "Од. виміру")]
            public string MsrName { get; set; }
            [Display(Name = "Група")]
            public string GrpName { get; set; }
            [Display(Name = "К-сть")]
            public decimal? Amount { get; set; }
            [Display(Name = "Ціна")]
            public decimal? Price { get; set; }
            [Display(Name = "Разом")]
            public decimal? Total { get; set; }
        }

        private List<rep_58> GetData()
        {
            var rec_row = RecipeComboBox.GetSelectedDataRow() as dynamic;
            int rec = rec_row.RecId;

            var sql = @"SELECT dd.MatId, cast( sum( dd.Amount)as numeric(15,3))  Amount, cast( avg( dd.Price) as numeric(15,2)) Price, m.Name AS MatName, cast( SUM( dd.Amount * dd.Price) as numeric(15,2)) AS Total , dbo.Measures.ShortName AS MsrName, mg.Name GrpName
FROM            dbo.DeboningDet AS dd 
inner join dbo.WaybillList wbl on wbl.WbillId = dd.WBillId
inner join dbo.WayBillMake wbm on wbm.WbillId = wbl.WbillId
INNER JOIN dbo.Materials AS m ON dd.MatId = m.MatId
inner join dbo.MatGroup as mg on mg.GrpId = m.GrpId
INNER JOIN  dbo.Measures ON m.MId = dbo.Measures.MId 
where wbl.OnDate between {0} and {1} and wbm.RecId = {2}
					 
GROUP BY dd.MatId, m.Name,  dbo.Measures.ShortName, mg.Name";


            return _db.Database.SqlQuery<rep_58>(sql, StartDateEdit.DateTime.Date, EndDateEdit.DateTime.Date, rec).ToList();
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
       /*     string template_name = "ProductionPlanning(54).xlsx";
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
            }*/
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            new frmGridView(Text, GetData()).ShowDialog();
        }

        private void RecipeComboBox_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
            {
                RecipeComboBox.EditValue = IHelper.ShowDirectList(RecipeComboBox.EditValue, 15);
            }
        }
    }
}