using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SP_Sklad.Common;
using SP_Sklad.Reports;
using SP_Sklad.SkladData;

namespace SP_Sklad
{
    public partial class frmReport : DevExpress.XtraEditors.XtraForm
    {
        int _rep_id { get; set; }

        public frmReport(int rep_id)
        {
            InitializeComponent();
            _rep_id = rep_id;
        }

        private void frmReport_Load(object sender, EventArgs e)
        {
            var dt = DateTime.Now;
            OnDateDBEdit.DateTime = dt;
            xtraTabControl1.AppearancePage.PageClient.BackColor = mainPanel.BackColor;
            StartDateEdit.DateTime = dt.Date;
            EndDateEdit.DateTime = dt.Date.SetEndDay();
            MonthEdit.SelectedIndex = dt.Month - 1;
            YearEdit.Value = dt.Year;
            YearEdit2.Value = dt.Year;
            YearEdit3.Value = dt.Year;

            textEdit1.Text = DB.SkladBase().RepLng.FirstOrDefault(w => w.RepId == _rep_id && w.LangId == 2).Notes;

            if (InDocGroupBox.Visible) checkEdit2.Checked = true;
            if (OutDocGroupBox.Visible) checkEdit4.Checked = true;
            if (!OnDateGroupBox.Visible) Height -= OnDateGroupBox.Height;
            if (!MatGroupBox.Visible)
            {
                Height -= MatGroupBox.Height;
            }
            else
            {
                if (_rep_id == 9 || _rep_id == 19)
                {
                    var mat = new BaseEntities().Materials.Where(w => w.Deleted == 0).Select(s => new { s.MatId, s.Name }).ToList();
                    MatComboBox.Properties.DataSource = mat;
                    var first_or_default = mat.FirstOrDefault();
                    if (first_or_default != null)
                    {
                        MatComboBox.EditValue = first_or_default.MatId;
                    }
                }
                else
                {
                    MatComboBox.Properties.DataSource = new List<object>() { new { MatId = 0, Name = "Усі" } }.Concat(new BaseEntities().Materials.Where(w => w.Deleted == 0).Select(s => new { s.MatId, s.Name }).ToList());
                    MatComboBox.EditValue = 0;
                }
            }
            if (!PeriodGroupBox.Visible) Height -= PeriodGroupBox.Height;
            if (!WHGroupBox.Visible)
            {
                Height -= WHGroupBox.Height;
            }
            else
            {
                WhComboBox.Properties.DataSource = new List<object>() { new { WId = "*", Name = "Усі" } }.Concat(new BaseEntities().Warehouse.Select(s => new { WId = s.WId.ToString(), s.Name }).ToList());
                WhComboBox.EditValue = "*";
            }

            if (!GRPGroupBox.Visible)
            {
                Height -= GRPGroupBox.Height;
            }
            else
            {
                GrpComboBox.Properties.DataSource = new List<object>() { new { GrpId = 0, Name = "Усі" } }.Concat(new BaseEntities().MatGroup.Where(w => w.Deleted == 0).Select(s => new { s.GrpId, s.Name }).ToList());
                GrpComboBox.EditValue = 0;
            }
            if (!KAGroupBox.Visible)
            {
                Height -= KAGroupBox.Height;
            }
            else
            {
                KagentComboBox.Properties.DataSource = new List<object>() { new { KaId = 0, Name = "Усі" } }.Concat(new BaseEntities().Kagent.Where(w => w.Archived == null || w.Archived == 0).Select(s => new { s.KaId, s.Name }));
                KagentComboBox.EditValue = 0;
            }

            if (!DocTypeGroupBox.Visible)
            {
                Height -= DocTypeGroupBox.Height;
            }
            else
            {
                DocTypeEdit.Properties.DataSource = new List<object>() { new { Id = 0, Name = "Усі" } }.Concat(new BaseEntities().DocType.Select(s => new { s.Id, s.Name }));
                DocTypeEdit.EditValue = 0;
            }

            //       if (!cxGroupBox1.Visible) Height -= cxGroupBox1.Height;
            if (!DocTypeGroupBox2.Visible)
            {
                Height -= DocTypeGroupBox2.Height;
            }


            if (!ChargeGroupBox.Visible)
            {
                Height -= ChargeGroupBox.Height;
            }
            else
            {
                ChTypeEdit.Properties.DataSource = new List<object>() { new { CTypeId = 0, Name = "Усі" } }.Concat(new BaseEntities().ChargeType.Where(w => w.Deleted == 0).Select(s => new { s.CTypeId, s.Name }));
                ChTypeEdit.EditValue = 0;
            }

            if (!wbStatusList.Visible)
            {
                Height -= wbStatusList.Height;
            }
            else
            {
                wbStatusList.Properties.DataSource = new List<object>() { new { Id = -1, Name = "Усі" }, new { Id = 1, Name = "Проведені" }, new { Id = 0, Name = "Непроведені" } };
                wbStatusList.EditValue = 1;
            }
        }

        private void frmReport_Shown(object sender, EventArgs e)
        {
            
        }

        private void PeriodComboBoxEdit_EditValueChanged(object sender, EventArgs e)
        {
            xtraTabControl1.SelectedTabPageIndex = PeriodComboBoxEdit.SelectedIndex;
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            String str = "";
            if (checkEdit2.Checked) str += "1";
            if (checkEdit1.Checked) str += ",5";
            if (checkEdit3.Checked) str += ",6";

            if (checkEdit4.Checked) str += ",-1";
            if (checkEdit5.Checked) str += ",-6";
            if (checkEdit6.Checked) str += ",-5";
            if (checkEdit7.Checked) str += ",-20";
            if (checkEdit8.Checked) str += ",-22";
            SetDate();

            var pr = new PrintReport
            {
                OnDate = OnDateDBEdit.DateTime,
                StartDate = StartDateEdit.DateTime,
                EndDate = EndDateEdit.DateTime,
                MatGroup = GrpComboBox.GetSelectedDataRow(),
                Kagent = KagentComboBox.GetSelectedDataRow(),
                Warehouse = WhComboBox.GetSelectedDataRow(),
                Material = MatComboBox.GetSelectedDataRow(),
                DocStr = str,
                DocType = DocTypeEdit.EditValue,
                ChType = ChTypeEdit.GetSelectedDataRow(),
                Status = wbStatusList.EditValue
            };

            pr.CreateReport(_rep_id);
        }

        private void SetDate()
        {
            switch (xtraTabControl1.SelectedTabPageIndex)
            {
                case 1:
                    StartDateEdit.DateTime = DateTime.Parse("01." + Convert.ToString(MonthEdit.SelectedIndex + 1) + "." + Convert.ToString(YearEdit.Value));
                    EndDateEdit.DateTime = DateTimeDayOfMonthExtensions.LastDayOfMonth(StartDateEdit.DateTime);
                    break;

                case 2:
                    string year = Convert.ToString(YearEdit2.Value);
                    switch (comboBoxEdit3.SelectedIndex)
                    {
                        case 0: StartDateEdit.DateTime = DateTime.Parse("01.01." + year);
                            EndDateEdit.DateTime = DateTime.Parse("31.03." + year).SetEndDay();
                            break;
                        case 1: StartDateEdit.DateTime = DateTime.Parse("01.04." + year);
                            EndDateEdit.DateTime = DateTime.Parse("30.06." + year).SetEndDay();
                            break;
                        case 2: StartDateEdit.DateTime = DateTime.Parse("01.07." + year);
                            EndDateEdit.DateTime = DateTime.Parse("30.09." + year).SetEndDay();
                            break;
                        case 3: StartDateEdit.DateTime = DateTime.Parse("01.10." + year);
                            EndDateEdit.DateTime = DateTime.Parse("31.12." + year).SetEndDay();
                            break;
                    }
                    break;

                case 3:
                    StartDateEdit.DateTime = DateTime.Parse("01.01." + Convert.ToString(YearEdit3.Value));
                    EndDateEdit.DateTime = DateTime.Parse("31.12." + Convert.ToString(YearEdit3.Value)).SetEndDay();
                    break;
            }
        }

        private void WhBtn_Click(object sender, EventArgs e)
        {
            WhComboBox.EditValue = IHelper.ShowDirectList(WhComboBox.EditValue, 2);
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            KagentComboBox.EditValue = IHelper.ShowDirectList(KagentComboBox.EditValue, 1);
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            MatComboBox.EditValue = IHelper.ShowDirectList(MatComboBox.EditValue, 5);
        }
    }
}
