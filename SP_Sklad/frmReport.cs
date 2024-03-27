using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.Utils.Win;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Popup;
using SP.Reports;
using SP.Reports.Models.Views;
using SP_Sklad.Common;
using SP_Sklad.Reports;
using SP_Sklad.SkladData;
using SP_Sklad.ViewsForm;

namespace SP_Sklad
{
    public partial class frmReport : DevExpress.XtraEditors.XtraForm
    {
        int _rep_id { get; set; }

        public frmReport(int rep_id)
        {
            InitializeComponent();
            _rep_id = rep_id;

            switch (_rep_id)
            {
                case 1:
                    this.OutDocGroupBox.Visible = false;
                    this.MatGroupBox.Visible = false;
                    this.DocTypeGroupBox.Visible = false;
                    this.ChargeGroupBox.Visible = false;
                    this.DocTypeGroupBox2.Visible = true;
                    break;

                case 2:
                    this.InDocGroupBox.Visible = false;
                    this.MatGroupBox.Visible = false;
                    this.DocTypeGroupBox.Visible = false;
                    this.ChargeGroupBox.Visible = false;
                    this.StatusPanel.Visible = true;
                    this.DocTypeGroupBox2.Visible = true;
                    break;

                case 3:
                    this.InDocGroupBox.Visible = false;
                    this.MatGroupBox.Visible = false;
                    this.DocTypeGroupBox.Visible = false;
                    this.ChargeGroupBox.Visible = false;
                    this.GroupKontragentPanel.Visible = true;
                    break;

                case 14:
                    this.DocTypeGroupBox2.Visible = true;
                    this.InDocGroupBox.Visible = false;
                    this.MatGroupBox.Visible = false;
                    this.DocTypeGroupBox.Visible = false;
                    this.ChargeGroupBox.Visible = false;
                    this.GroupKontragentPanel.Visible = true;
                    break;

                case 4:
                    this.OutDocGroupBox.Visible = false;
                    this.MatGroupBox.Visible = false;
                    this.DocTypeGroupBox.Visible = false;
                    this.ChargeGroupBox.Visible = false;
                    break;

                case 25:
                    this.DocTypeGroupBox2.Visible = true;
                    this.OutDocGroupBox.Visible = false;
                    this.MatGroupBox.Visible = false;
                    this.DocTypeGroupBox.Visible = false;
                    this.ChargeGroupBox.Visible = false;
                    break;

                case 26:
                    this.KontragentPanel.Visible = false;
                    this.MatGroupBox.Visible = false;
                    this.DocTypeGroupBox.Visible = false;
                    this.ChargeGroupBox.Visible = false;
                    break;

                case 30:
                case 8:
                    this.WHGroupBox.Visible = false;
                    this.GRPGroupBox.Visible = false;
                    this.MatGroupBox.Visible = false;
                    this.DocTypeGroupBox.Visible = false;
                    this.ChargeGroupBox.Visible = false;
                    break;

                case 5:
                    this.OnDateGroupBox.Visible = true;
                    this.KontragentPanel.Visible = false;
                    this.PeriodGroupBox.Visible = false;
                    this.WHGroupBox.Visible = false;
                    this.GRPGroupBox.Visible = false;
                    this.KontragentPanel.Visible = false;
                    this.MatGroupBox.Visible = false;
                    this.DocTypeGroupBox.Visible = false;
                    this.ChargeGroupBox.Visible = false;
                    this.GroupKontragentPanel.Visible = true;
                    KaKindPanel.Visible = true;
                    break;

                case 6:
                case 45:
                    this.OnDateGroupBox.Visible = true;
                    this.KontragentPanel.Visible = false;
                    this.PeriodGroupBox.Visible = false;
                    this.WHGroupBox.Visible = false;
                    this.GRPGroupBox.Visible = false;
                    this.KontragentPanel.Visible = false;
                    this.MatGroupBox.Visible = false;
                    this.DocTypeGroupBox.Visible = false;
                    this.ChargeGroupBox.Visible = false;
                    this.GroupKontragentPanel.Visible = true;
                    break;

                case 18:
                case 7:
                    this.PeriodGroupBox.Visible = false;
                    this.KontragentPanel.Visible = false;
                    this.MatGroupBox.Visible = false;
                    this.DocTypeGroupBox.Visible = false;
                    this.ChargeGroupBox.Visible = false;
                    this.OnDateGroupBox.Visible = true;
                    break;

                case 9:
                case 19:
                    this.GRPGroupBox.Visible = false;
                    this.ChargeGroupBox.Visible = false;
                    this.GroupKontragentPanel.Visible = true;
                    break;

                case 10:
                    this.MatGroupBox.Visible = false;
                    this.KontragentPanel.Visible = false;
                    this.DocTypeGroupBox.Visible = false;
                    this.ChargeGroupBox.Visible = false;
                    break;

                case 11:
                    this.KontragentPanel.Visible = false;
                    this.WHGroupBox.Visible = false;
                    this.GRPGroupBox.Visible = false;
                    this.KontragentPanel.Visible = false;
                    this.MatGroupBox.Visible = false;
                    this.ChargeGroupBox.Visible = false;
                    break;

                case 13:
                    this.MatGroupBox.Visible = false;
                    this.DocTypeGroupBox.Visible = false;
                    this.ChargeGroupBox.Visible = false;
                    this.GroupKontragentPanel.Visible = true;
                    break;

                case 15:
                    this.GRPGroupBox.Visible = false;
                    this.DocTypeGroupBox.Visible = false;
                    this.ChargeGroupBox.Visible = false;
                    this.WHGroupBox.Visible = false;
                    break;

                case 16:
                    this.MatGroupBox.Visible = false;
                    this.DocTypeGroupBox.Visible = false;
                    this.WHGroupBox.Visible = false;
                    this.GRPGroupBox.Visible = false;
                    break;

                case 17:
                    this.WHGroupBox.Visible = false;
                    this.GRPGroupBox.Visible = false;
                    this.MatGroupBox.Visible = false;
                    this.GroupKontragentPanel.Visible = true;
                    this.DocTypeGroupBox.Visible = false;
                    break;

                case 23:
                    this.WHGroupBox.Visible = false;
                    this.GRPGroupBox.Visible = false;
                    this.MatGroupBox.Visible = false;
                    this.GroupKontragentPanel.Visible = true;
                    this.DocTypeGroupBox.Visible = false;
                    this.CashDesksPanel.Visible = true;
                    break;

                case 20:
                    this.MatGroupBox.Visible = false;
                    this.DocTypeGroupBox.Visible = false;
                    this.WHGroupBox.Visible = false;
                    this.GrpComboBox.Properties.DataSource = new List<object>() { new { GrpId = 0, Name = "Усі" } }.Concat(new BaseEntities().SvcGroup.Where(w => w.Deleted == 0).Select(s => new { s.GrpId, s.Name }).ToList());
                    this.ChargeGroupBox.Visible = false;
                    break;

                case 28:
                    this.WHGroupBox.Visible = false;
                    this.DocTypeGroupBox.Visible = false;
                    this.ChargeGroupBox.Visible = false;
                    this.GroupKontragentPanel.Visible = true;
                    break;

                case 27:
                    this.WHGroupBox.Visible = false;
                    this.DocTypeGroupBox.Visible = false;
                    this.ChargeGroupBox.Visible = false;
                    this.GroupKontragentPanel.Visible = true;
                    this.PersonPanel.Visible = true;
                    btnShowGrid.Visible = true;
                    break;

                case 29:
                    this.OutDocGroupBox.Visible = false;
                    this.MatGroupBox.Visible = false;
                    this.DocTypeGroupBox.Visible = false;
                    this.ChargeGroupBox.Visible = false;
                    break;

                case 31:
                    this.KontragentPanel.Visible = false;
                    this.WHGroupBox.Visible = false;
                    this.DocTypeGroupBox.Visible = false;
                    this.ChargeGroupBox.Visible = false;
                    break;

                case 32:
                    this.GRPGroupBox.Visible = false;
                    this.MatGroupBox.Visible = false;
                    this.KontragentPanel.Visible = false;
                    this.WHGroupBox.Visible = false;
                    this.DocTypeGroupBox.Visible = false;
                    this.ChargeGroupBox.Visible = false;
                    break;

                case 33:
                    this.KontragentPanel.Visible = false;
                    this.DocTypeGroupBox.Visible = false;
                    this.ChargeGroupBox.Visible = false;
                    break;

                case 34:
                    this.OnDateGroupBox.Visible = true;
                    this.PeriodGroupBox.Visible = false;
                    this.WHGroupBox.Visible = false;
                    this.GRPGroupBox.Visible = false;
                    this.MatGroupBox.Visible = false;
                    this.KontragentPanel.Visible = false;
                    this.DocTypeGroupBox.Visible = false;
                    this.ChargeGroupBox.Visible = false;
                    break;

                case 35:
                    this.KontragentPanel.Visible = false;
                    this.DocTypeGroupBox.Visible = false;
                    this.ChargeGroupBox.Visible = false;
                    break;

                case 36:
                    this.OnDateGroupBox.Visible = true;
                    this.KontragentPanel.Visible = false;
                    this.PeriodGroupBox.Visible = false;
                    this.WHGroupBox.Visible = false;
                    this.GRPGroupBox.Visible = false;
                    this.KontragentPanel.Visible = false;
                    this.MatGroupBox.Visible = false;
                    this.DocTypeGroupBox.Visible = false;
                    this.ChargeGroupBox.Visible = false;
                    break;

                case 37:
                    this.KontragentPanel.Visible = false;
                    this.GRPGroupBox.Visible = false;
                    this.KontragentPanel.Visible = false;
                    this.MatGroupBox.Visible = false;
                    this.DocTypeGroupBox.Visible = false;
                    this.ChargeGroupBox.Visible = false;
                    break;

                case 38:
                    this.KontragentPanel.Visible = false;
                    this.GRPGroupBox.Visible = false;
                    this.KontragentPanel.Visible = false;
                    this.MatGroupBox.Visible = false;
                    this.DocTypeGroupBox.Visible = false;
                    this.ChargeGroupBox.Visible = false;
                    this.WHGroupBox.Visible = false;
                    break;

                case 39:
                    this.InDocGroupBox.Visible = false;
                    this.MatGroupBox.Visible = false;
                    this.DocTypeGroupBox.Visible = false;
                    this.ChargeGroupBox.Visible = false;
                    this.GroupKontragentPanel.Visible = true;
                    this.WHGroupBox.Visible = false;
                    btnShowGrid.Visible = true;
                    break;

                case 22:
                    this.PersonPanel.Visible = true;
                    this.KontragentPanel.Visible = false;
                    this.GRPGroupBox.Visible = false;
                    this.KontragentPanel.Visible = false;
                    this.MatGroupBox.Visible = false;
                    this.DocTypeGroupBox.Visible = false;
                    this.ChargeGroupBox.Visible = false;
                    this.WHGroupBox.Visible = false;
                    break;

                case 40:
                    this.OnDateGroupBox.Visible = true;
                    this.PersonPanel.Visible = false;
                    this.GRPGroupBox.Visible = false;
                    //   this.MatGroupBox.Visible = false;
                    this.DocTypeGroupBox.Visible = false;
                    this.ChargeGroupBox.Visible = false;
                    this.WHGroupBox.Visible = false;
                    this.PeriodGroupBox.Visible = false;
                    break;

                case 41:
                    this.GroupKontragentPanel.Visible = true;
                    this.PersonPanel.Visible = false;
                    this.GRPGroupBox.Visible = false;
                    this.MatGroupBox.Visible = false;
                    this.DocTypeGroupBox.Visible = false;
                    this.ChargeGroupBox.Visible = false;
                    this.WHGroupBox.Visible = false;
                    this.KontragentPanel.Visible = false;
                    break;

                case 42:
                    this.InDocGroupBox.Visible = false;
                    this.DocTypeGroupBox.Visible = false;
                    this.ChargeGroupBox.Visible = false;
                    this.WHGroupBox.Visible = false;
                    this.KontragentPanel.Visible = false;
                    this.StatusPanel.Visible = true;
                    this.wmatturnStatusPanel.Visible = true;
                    break;


                case 43:
                    this.PersonPanel.Visible = false;
                    this.GRPGroupBox.Visible = false;
                    //   this.MatGroupBox.Visible = false;
                    this.DocTypeGroupBox.Visible = false;
                    this.ChargeGroupBox.Visible = false;
                    this.WHGroupBox.Visible = false;
                    this.GroupKontragentPanel.Visible = true;
               //     this.KontragentPanel.Visible = true;
                    break;

                case 44:
                    this.DocTypeGroupBox2.Visible = true;
                    this.MatGroupBox.Visible = false;
                    this.DocTypeGroupBox.Visible = false;
                    this.ChargeGroupBox.Visible = false;
                    this.StatusPanel.Visible = true;
                    this.GRPGroupBox.Visible = false;
                    break;

                case 46:
                    this.WHGroupBox.Visible = false;
                    this.MatGroupBox.Visible = false;
                    this.DocTypeGroupBox.Visible = false;
                    this.ChargeGroupBox.Visible = false;
                    this.GRPGroupBox.Visible = false;
                    this.KontragentPanel.Visible = false;
                    this.CarPanel.Visible = true;
                    this.StatusPanel.Visible = true;
                    this.DriverPanel.Visible = true;
                    break;

                case 47:
                    this.OutDocGroupBox.Visible = false;
                    this.MatGroupBox.Visible = false;
                    this.DocTypeGroupBox.Visible = false;
                    this.ChargeGroupBox.Visible = false;
                    break;

                case 48:
                    this.OnDateGroupBox.Visible = true;
                    this.KontragentPanel.Visible = false;
                    this.PeriodGroupBox.Visible = false;
                    this.WHGroupBox.Visible = false;
                    this.GRPGroupBox.Visible = false;
                    this.KontragentPanel.Visible = false;
                    this.MatGroupBox.Visible = false;
                    this.DocTypeGroupBox.Visible = false;
                    this.ChargeGroupBox.Visible = false;
                    break;

                case 49:
                    this.InDocGroupBox.Visible = false;
                    this.MatGroupBox.Visible = false;
                    this.DocTypeGroupBox.Visible = false;
                    this.ChargeGroupBox.Visible = false;
                    this.DocTypeGroupBox2.Visible = false;
                    KontragentPanel.Visible = true;
                    WHGroupBox.Visible = false;
                    GroupKontragentPanel.Visible = true;
                    RoutePanel.Visible = true;
                    break;

                case 50:
                    WHGroupBox.Visible = false;
                    this.InDocGroupBox.Visible = false;
                    this.ChargeGroupBox.Visible = false;
                    this.DocTypeGroupBox.Visible = false;
                    this.DocTypeGroupBox2.Visible = false;
                    KontragentPanel.Visible = true;
                    GroupKontragentPanel.Visible = true;
                    MatGroupBox.Visible = true;
                    break;

                case 52:
                    WHGroupBox.Visible = false;
                    this.InDocGroupBox.Visible = false;
                    this.ChargeGroupBox.Visible = false;
                    this.DocTypeGroupBox.Visible = false;
                    this.DocTypeGroupBox2.Visible = false;
                    KontragentPanel.Visible = true;
                    GroupKontragentPanel.Visible = true;
                    MatGroupBox.Visible = false;
                    break;

                case 56:
                    this.KontragentPanel.Visible = false;
                    this.GRPGroupBox.Visible = false;
                    this.KontragentPanel.Visible = false;
                    this.MatGroupBox.Visible = false;
                    this.DocTypeGroupBox.Visible = false;
                    this.ChargeGroupBox.Visible = false;
                    this.WHGroupBox.Visible = false;
                    this.OkButton.Visible = false;
                    btnShowGrid.Visible = true;
                    break;
            }

            btnSortColumn.Visible =  DB.SkladBase().ReportSortedFields.Any(w => w.RepId == rep_id);
        }

        private void frmReport_Load(object sender, EventArgs e)
        {
            var dt = DateTime.Now;
            OnDateDBEdit.DateTime = dt;
            //xtraTabControl1.AppearancePage.PageClient.BackColor = mainPanel.BackColor;
            StartDateEdit.DateTime = dt.Date;
            EndDateEdit.DateTime = dt.Date.SetEndDay();
            MonthEdit.SelectedIndex = dt.Month - 1;
            YearEdit.Value = dt.Year;
            YearEdit2.Value = dt.Year;
            YearEdit3.Value = dt.Year;

            textEdit1.Text = DB.SkladBase().RepLng.FirstOrDefault(w => w.RepId == _rep_id && w.LangId == 2).Notes;

            if (InDocGroupBox.Visible) checkEdit2.Checked = true;
            if (OutDocGroupBox.Visible) checkEdit4.Checked = true;

            Height = 0;
            Height = Height + panelControl2.Size.Height + panel1.Size.Height + 10;

            if (OnDateGroupBox.Visible)
            {
                Height += OnDateGroupBox.Height;
            }

            if (MatGroupBox.Visible)
            {
                Height += MatGroupBox.Height;


                if (_rep_id == 9 || _rep_id == 19 || _rep_id == 15)
                {
                    var mat = new BaseEntities().Materials.Where(w => w.Deleted == 0).Select(s => new MatComboBoxItem
                    {
                        MatId = s.MatId,
                        Name = s.Name,
                        MsrName = s.Measures.ShortName
                    }).ToList();
                    MatComboBox.Properties.DataSource = mat;
                    var first_or_default = mat.FirstOrDefault();
                    if (first_or_default != null)
                    {
                        MatComboBox.EditValue = first_or_default.MatId;
                    }
                }
                if (_rep_id == 40 || _rep_id == 43)
                {
                    var tmc = new BaseEntities().Materials.Where(w => w.Deleted == 0 && w.TypeId == 4/*"ТМЦ"*/).Select(s => new MatComboBoxItem
                    {
                        MatId = s.MatId,
                        Name = s.Name,
                        MsrName = s.Measures.ShortName
                    }).ToList();
                    MatComboBox.Properties.DataSource = tmc;
                    var first_or_default = tmc.FirstOrDefault();
                    if (first_or_default != null)
                    {
                        MatComboBox.EditValue = first_or_default.MatId;
                    }
                }
                else
                {
                    MatComboBox.Properties.DataSource = new List<object>() { new MatComboBoxItem { MatId = 0, Name = "Усі" } }.Concat(new BaseEntities().Materials.Where(w => w.Deleted == 0).Select(s => new MatComboBoxItem
                    {
                        MatId = s.MatId,
                        Name = s.Name,
                        MsrName = s.Measures.ShortName
                    }).ToList());
                    MatComboBox.EditValue = 0;
                }
            }

            if (PeriodGroupBox.Visible)
            {
                Height += PeriodGroupBox.Height;

                if (_rep_id == 41)
                {
                    PeriodComboBoxEdit.EditValue = "За рік";
                }
            }

            if (WHGroupBox.Visible)
            {
                Height += WHGroupBox.Height;

                var wh = new BaseEntities().Warehouse.Where(w => w.UserAccessWh.Any(a => a.UserId == DBHelper.CurrentUser.UserId)).Select(s => new { WId = s.WId.ToString(), s.Name, s.Def }).ToList();
                if (_rep_id == 37)
                {
                    WhComboBox.Properties.DataSource = wh.Select(s => new WhComboBoxItem
                    {
                        WId = s.WId,
                        Name = s.Name
                    }).ToList();

                    WhComboBox.EditValue = wh.FirstOrDefault(w => w.Def == 1).WId;
                }
                else
                {
                    WhComboBox.Properties.DataSource = new List<WhComboBoxItem>() { new WhComboBoxItem { WId = "*", Name = "Усі" } }.Concat(wh.Select(s => new WhComboBoxItem
                    {
                        WId = s.WId,
                        Name = s.Name
                    }).ToList());

                    WhComboBox.EditValue = "*";
                }
            }

            if (GRPGroupBox.Visible)
            {
                Height += GRPGroupBox.Height;

                GrpComboBox.Properties.DataSource = new List<MatGrpComboBoxItem>() { new MatGrpComboBoxItem { GrpId = 0, Name = "Усі" } }.Concat(new BaseEntities().MatGroup.Where(w => w.Deleted == 0).Select(s => new MatGrpComboBoxItem { GrpId = s.GrpId, Name = s.Name }).ToList());
                GrpComboBox.EditValue = 0;
            }

            if (KontragentPanel.Visible)
            {
                Height += KontragentPanel.Height;

                if (_rep_id == 3)
                {
                    KagentComboBox.Properties.DataSource = new List<object>() { new KagentComboBoxItem { KaId = 0, Name = "Усі" } }.Concat( DBHelper.KagentsWorkerList.Select(s => new KagentComboBoxItem { KaId = s.KaId, Name = s.Name }));
                    // KagentComboBox.EditValue = DBHelper.Kagents.FirstOrDefault().KaId;
                    KagentComboBox.EditValue = 0;
                }
                else
                {
                    KagentComboBox.Properties.DataSource = DBHelper.KagentsList.Select(s=> new KagentComboBoxItem { KaId = s.KaId, Name = s.Name });// new List<object>() { new { KaId = 0, Name = "Усі" } }.Concat(new BaseEntities().Kagent.Where(w => w.Archived == null || w.Archived == 0).Select(s => new { s.KaId, s.Name }));
                    KagentComboBox.EditValue = 0;
                }
            }

            if (DocTypeGroupBox.Visible)
            {
                Height += DocTypeGroupBox.Height;

                DocTypeEdit.Properties.DataSource = new List<object>() { new { Id = 0, Name = "Усі" } }.Concat(new BaseEntities().DocType.Select(s => new { s.Id, s.Name }));
                DocTypeEdit.EditValue = 0;
            }

            //       if (!cxGroupBox1.Visible) Height -= cxGroupBox1.Height;
            if (DocTypeGroupBox2.Visible)
            {
                Height += DocTypeGroupBox2.Height;
            }


            if (ChargeGroupBox.Visible)
            {
                Height += ChargeGroupBox.Height;

                ChTypeEdit.Properties.DataSource = new List<object>() { new ChTypeComboBoxItem { CTypeId = 0, Name = "Усі" } }.Concat(new BaseEntities().ChargeType.Where(w => w.Deleted == 0).Select(s => new ChTypeComboBoxItem { CTypeId = s.CTypeId, Name = s.Name }));
                ChTypeEdit.EditValue = 0;
            }

            if (StatusPanel.Visible)
            {
                Height += StatusPanel.Height;

                wbStatusList.Properties.DataSource = new List<object>() { new { Id = -1, Name = "Усі" }, new { Id = 1, Name = "Проведені / Закінчене виробництво" }, new { Id = 0, Name = "Непроведені / Актуалні" }, new { Id = 2, Name = "Розпочато виробництво" } };
                wbStatusList.EditValue = 1;
            }

            if (GroupKontragentPanel.Visible)
            {
                Height += GroupKontragentPanel.Height;

                GrpKagentLookUpEdit.Properties.DataSource = new List<object>() { new GrpKagentComboBoxItem { Id = Guid.Empty, Name = "Усі" } }.Concat(new BaseEntities().KontragentGroup.Select(s => new GrpKagentComboBoxItem { Id = s.Id, Name = s.Name })).ToList();
                GrpKagentLookUpEdit.EditValue = Guid.Empty;
            }

            if (PersonPanel.Visible)
            {
                Height += PersonPanel.Height;

                PersonLookUpEdit.Properties.DataSource = new List<object>() { new PersonList { KaId = 0, Name = "Усі" } }.Concat(DBHelper.Persons);
                PersonLookUpEdit.EditValue = 0;
            }

            if (wmatturnStatusPanel.Visible)
            {
                Height += wmatturnStatusPanel.Height;

                wmatturnStatus.Properties.DataSource = new List<object>() { new { Id = -1, Name = "Усі" }, new { Id = 1, Name = "Так" }, new { Id = 0, Name = "Ні" } };
                wmatturnStatus.EditValue = 0;
            }

            if(CashDesksPanel.Visible)
            {
                Height += CashDesksPanel.Height;

                CashEditComboBox.Properties.DataSource = new List<object>() { new CashDesksList { CashId = -1, Name = "Усі" } }.Concat(DBHelper.CashDesks.Select(s => new CashDesksList { CashId = s.CashId, Name = s.Name }));
                CashEditComboBox.EditValue = -1;
            }

            if (CarPanel.Visible)
            {
                Height += CarPanel.Height;

                CarsLookUpEdit.Properties.DataSource = new BaseEntities().Cars.Select(s=> new CarList {  Id = s.Id, Number = s.Number, Name = s.Name, DriverName = s.Kagent.Name}).ToList();
               // CarsLookUpEdit.EditValue = -1;
            }

            if (DriverPanel.Visible)
            {
                Height += DriverPanel.Height;
                DriversLookUpEdit.Properties.DataSource = new BaseEntities().Kagent.Where(w => w.JobType == 3).Select(s => new KagentComboBoxItem { KaId = s.KaId, Name = s.Name }).ToList();
            }

            if (KaKindPanel.Visible)
            {
                Height += KaKindPanel.Height;
                KaKindLookUpEdit.Properties.DataSource = new List<object>() { new KAKIndComboBoxItem { Id = -1, Name = "Усі" } }.Concat(new BaseEntities().KAKInd.Select(s=> new KAKIndComboBoxItem {  Id = s.Id, Name = s.Name}).ToList());
                KaKindLookUpEdit.EditValue = -1;
            }

            if(RoutePanel.Visible)
            {
                Height += RoutePanel.Height;
                RouteLookUpEdit.Properties.DataSource = new List<object>() { new RouteComboBoxItem { Id = -1, Name = "Усі" } }.Concat(new BaseEntities().Routes.Select(s => new RouteComboBoxItem { Id = s.Id, Name = s.Name }).ToList());
                RouteLookUpEdit.EditValue = -1;
            }
        }

        private void frmReport_Shown(object sender, EventArgs e)
        {

        }

        private void PeriodComboBoxEdit_EditValueChanged(object sender, EventArgs e)
        {
            //xtraTabControl1.SelectedTabPageIndex = PeriodComboBoxEdit.SelectedIndex;
            CustomPeriodPanel.Visible = false;
            PerMonthPanel.Visible = false;
            PerQuarterPanel.Visible = false;
            PerYearPanel.Visible = false;

            switch (PeriodComboBoxEdit.SelectedIndex)
            {
                case 0:
                    CustomPeriodPanel.Visible = true;
                    break;

                case 1:
                    PerMonthPanel.Visible = true;
                    break;

                case 2:
                    PerQuarterPanel.Visible = true;
                    break;

                case 3:
                    PerYearPanel.Visible = true;
                    break;
            }

        }
        private PrintReportv2 GetReport()
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
            if (checkEdit9.Checked) str += ",-24";
            if (checkEdit10.Checked) str += ",4";
            if (checkEdit11.Checked) str += ",-25";
            SetDate();

            int grp = ChildGroupCheckEdit.Checked ? Convert.ToInt32((GrpComboBox.GetSelectedDataRow() as dynamic).GrpId) : 0;

            var pr2 = new PrintReportv2(_rep_id, DBHelper.CurrentUser.KaId, DBHelper.CurrentUser.UserId)
            {
                OnDate = OnDateDBEdit.DateTime,
                StartDate = StartDateEdit.DateTime,
                EndDate = EndDateEdit.DateTime,
                MatGroup = GrpComboBox.GetSelectedDataRow() as MatGrpComboBoxItem,
                Kagent = KagentComboBox.GetSelectedDataRow() as KagentComboBoxItem,
                Warehouse = WhComboBox.GetSelectedDataRow() as WhComboBoxItem,
                Material = MatComboBox.GetSelectedDataRow() as MatComboBoxItem,
                DocStr = str,
                DocType = DocTypeEdit.EditValue,
                ChType = ChTypeEdit.GetSelectedDataRow() as ChTypeComboBoxItem,
                Status = wbStatusList.EditValue,
                KontragentGroup = GrpKagentLookUpEdit.GetSelectedDataRow() as GrpKagentComboBoxItem,
                GrpStr = ChildGroupCheckEdit.Checked ? String.Join(",", new BaseEntities().GetMatGroupTree(grp).ToList().Select(s => Convert.ToString(s.GrpId))) : "",
                Person = PersonLookUpEdit.GetSelectedDataRow(),
                RsvStatus = wmatturnStatus.EditValue,
                CashDesk = CashEditComboBox.GetSelectedDataRow() as CashDesksList,
                Car = CarsLookUpEdit.GetSelectedDataRow() as CarList,
                Driver = DriversLookUpEdit.GetSelectedDataRow() as KagentComboBoxItem,
                KontragentTyp = KaKindLookUpEdit.GetSelectedDataRow() as KAKIndComboBoxItem,
                RouteId = RouteLookUpEdit.GetSelectedDataRow() as RouteComboBoxItem
            };

            return pr2;
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            var pr2 = GetReport();

            var template_name = pr2.GetTemlate(_rep_id);
            var template_file = Path.Combine(IHelper.template_path, template_name);
            if (File.Exists(template_file))
            {
                var report_data = pr2.CreateReport(template_file, DBHelper.CurrentUser.ReportFormat);
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

        private void SetDate()
        {
            switch (PeriodComboBoxEdit.SelectedIndex)
            {
                case 1:
                    StartDateEdit.DateTime = DateTime.Parse("01." + Convert.ToString(MonthEdit.SelectedIndex + 1) + "." + Convert.ToString(YearEdit.Value));
                    EndDateEdit.DateTime = DateTimeDayOfMonthExtensions.LastDayOfMonth(StartDateEdit.DateTime).SetEndDay();
                    break;

                case 2:
                    string year = Convert.ToString(YearEdit2.Value);
                    switch (comboBoxEdit3.SelectedIndex)
                    {
                        case 0:
                            StartDateEdit.DateTime = DateTime.Parse("01.01." + year);
                            EndDateEdit.DateTime = DateTime.Parse("31.03." + year).SetEndDay();
                            break;
                        case 1:
                            StartDateEdit.DateTime = DateTime.Parse("01.04." + year);
                            EndDateEdit.DateTime = DateTime.Parse("30.06." + year).SetEndDay();
                            break;
                        case 2:
                            StartDateEdit.DateTime = DateTime.Parse("01.07." + year);
                            EndDateEdit.DateTime = DateTime.Parse("30.09." + year).SetEndDay();
                            break;
                        case 3:
                            StartDateEdit.DateTime = DateTime.Parse("01.10." + year);
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
         
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
          
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
           
        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            using (var frm = new frmEditSortingReport(_rep_id))
            {
                frm.ShowDialog();
            }
        }

        private void ChTypeEdit_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void DriversLookUpEdit_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {

        }

        private void simpleButton5_Click(object sender, EventArgs e)
        {
            var pr2 = GetReport();

            new frmGridView(pr2.report_name, pr2.GetReportDate()).ShowDialog();
        }

        private void OnDateDBEdit_Properties_Popup(object sender, EventArgs e)
        {
            PopupDateEditForm form = ((IPopupControl)sender).PopupWindow as PopupDateEditForm;

            if(form != null)
            {
                form.Calendar.InactiveDaysVisibility = CalendarInactiveDaysVisibility.Hidden;

               
            }
        }

        private void KagentComboBox_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            if(e.Button.Index == 1)
            {
                KagentComboBox.EditValue = IHelper.ShowDirectList(KagentComboBox.EditValue, 1);
            }
        }

        private void MatComboBox_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
            {
                MatComboBox.EditValue = IHelper.ShowDirectList(MatComboBox.EditValue, 5);
            }
        }

        private void WhComboBox_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
            {
                WhComboBox.EditValue = IHelper.ShowDirectList(WhComboBox.EditValue, 2);
            }
        }
    }
}


