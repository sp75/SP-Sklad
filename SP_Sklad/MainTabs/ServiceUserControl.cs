﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SP_Sklad.SkladData;
using SP_Sklad.EditForm;
using SP_Sklad.Common;
using SP_Sklad.Properties;
using SP_Sklad.ViewsForm;
using System.Drawing.Printing;
using System.Data.SqlClient;
using System.Configuration;
using DevExpress.XtraRichEdit.Services;
using RichEditSyntaxSample;
using DevExpress.Office.Utils;
using System.IO;
using System.Text.RegularExpressions;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Base;

namespace SP_Sklad.MainTabs
{
    public partial class ServiceUserControl : DevExpress.XtraEditors.XtraUserControl
    {
        GetServiceTree_Result focused_tree_node { get; set; }

        public ServiceUserControl()
        {
            InitializeComponent();

            richEditControl1.Options.Search.RegExResultMaxGuaranteedLength = 500;
            richEditControl1.ReplaceService<ISyntaxHighlightService>(new CustomSyntaxHighlightService(richEditControl1.Document));
          
            richEditControl1.ActiveViewType = DevExpress.XtraRichEdit.RichEditViewType.Simple;
            richEditControl1.Document.Sections[0].Page.Width = Units.InchesToDocumentsF(1000f);
            
            richEditControl1.Document.DefaultCharacterProperties.FontName = "Courier New";
        }

        private void ServiceUserControl_Load(object sender, EventArgs e)
        {
            mainContentTab.ShowTabHeader = DevExpress.Utils.DefaultBoolean.False;
            delTurnDate.DateTime = DateTime.Now;

            if (!DesignMode)
            {
                using (var db = new BaseEntities())
                {
                    DirTreeList.DataSource = db.GetServiceTree(DBHelper.CurrentUser.UserId).ToList();
                    DirTreeList.ExpandToLevel(1);


                    wbStartDate.DateTime = DateTime.Now.Date; //DateTimeDayOfMonthExtensions.FirstDayOfMonth(DateTime.Now);
                    wbEndDate.DateTime = DateTime.Now.Date.AddDays(1);

                    UserComboBox.Properties.DataSource = new List<object>() { new { UserId = -1, Name = "Усі" } }.Concat(new BaseEntities().Users.Select(s => new { s.UserId, s.Name })).ToList();
                    UserComboBox.EditValue = -1;

                    wTypeList.Properties.DataSource = new List<object>() { new { FunId = (int?)-1, Name = "Усі" } }
                        .Concat(new BaseEntities().ViewLng.Where(w => w.LangId == 2 && (w.UserTreeView.Functions.TabId == 24 || w.UserTreeView.Functions.TabId == 27 || w.UserTreeView.Functions.TabId == 51 || w.UserTreeView.Functions.FunId == 6 || w.UserTreeView.Id == 10))
                        .Select(s => new
                        {
                            s.UserTreeView.FunId,
                            s.Name
                        })).ToList();
                    wTypeList.EditValue = -1;

                    WeighingScalesLookUpEdit.Properties.DataSource = new BaseEntities().WeighingScales.ToList();
                    WeighingScalesLookUpEdit2.Properties.DataSource = new BaseEntities().WeighingScales.ToList();
                }

            }

            // Add list of installed printers found to the combo box.
            // The pkInstalledPrinters string will be used to provide the display string.
            String pkInstalledPrinters;
            for (int i = 0; i < PrinterSettings.InstalledPrinters.Count; i++)
            {
                pkInstalledPrinters = PrinterSettings.InstalledPrinters[i];
                comboInstalledPrinters.Properties.Items.Add(pkInstalledPrinters);
            }

            comboInstalledPrinters.Text = Settings.Default.receipt_printer;

            DeviceNameRMKTextEdit.Text = Settings.Default.barcode_scanner_name;
        }

        private void DirTreeList_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
        {
            focused_tree_node = DirTreeList.GetDataRecordByNode(e.Node) as GetServiceTree_Result;
            if (focused_tree_node == null)
            {
                return;
            }

            RefrechItemBtn.PerformClick();
            mainContentTab.SelectedTabPageIndex = focused_tree_node.GType.Value;

            if (focused_tree_node.FunId != null)
            {
                History.AddEntry(new HistoryEntity { FunId = focused_tree_node.FunId.Value, MainTabs = 7 });

                if (DirTreeList.ContainsFocus)
                {
                    Settings.Default.LastFunId = focused_tree_node.FunId.Value;
                }
            }
        }

        private void EditItemBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (focused_tree_node == null)
            {
                return;
            }

            switch (focused_tree_node.GType)
            {
                case 1:
                    var u = UsersGridView.GetFocusedRow() as v_Users;
                    new frmUserEdit(u.UserId).ShowDialog();

                    RefrechItemBtn.PerformClick();
                    break;

                case 5:
                    var f = new frmOperLogDet();
                    f.OperLogDetBS.DataSource = OprLogGridView.GetFocusedRow();
                    f.ShowDialog();
                    break;
            }
        }

        private void RefrechItemBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if(focused_tree_node == null)
            {
                return;
            }

            switch (focused_tree_node.GType)
            {
                case 1:
                    UsersDS.DataSource = DB.SkladBase().v_Users.AsNoTracking().ToList();
                    break;

                case 2:
                    using (var db = DB.SkladBase())
                    {
                        UsersOnlineBS.DataSource = db.Users.AsNoTracking().Where(w => w.IsOnline == true && w.IsWorking).ToList();
                        gridControl2.DataSource = db.WaybillList.Join(db.DocType, w => w.WType, t => t.Id, (w, t) => new
                        {
                            w.Id,
                            w.Num,
                            w.OnDate,
                            w.UpdatedAt,
                            w.SessionId,
                            UserName = db.Users.Where(u => u.UserId == w.UpdatedBy).Select(u => u.Name).FirstOrDefault(),
                            t.Name,
                            doc_type = t.Id
                        }).Where(w => w.SessionId != null).ToList().Concat(db.ProductionPlans.Where(w => w.SessionId != null).Select(s => new
                        {
                            s.Id,
                            s.Num,
                            s.OnDate,
                            s.UpdatedAt,
                            s.SessionId,
                            UserName = db.Users.Where(u => u.UserId == s.UpdatedBy).Select(u => u.Name).FirstOrDefault(),
                            Name = "Планування виробницва",
                            doc_type =  20
                        }).ToList());
                    }
                    break;

                case 5:
                    if (xtraTabControl2.SelectedTabPageIndex == 0)
                    {
                        GetOperLogBS.DataSource = DB.SkladBase().GetOperLog(wbStartDate.DateTime, wbEndDate.DateTime, (int)wTypeList.EditValue, (int)UserComboBox.EditValue).OrderByDescending(o => o.OnDate).ToList()
                            .Select(s => new GetOperLog_Result
                            {
                                OpCode = s.OpCode,
                                OnDate = s.OnDate,
                                FunName = s.FunName,
                                Id = s.Id,
                                DocNum = s.DocNum,
                                UserName = s.UserName,
                                TabId = s.TabId,
                                DataBefore = IHelper.ConvertLogData(s.DataBefore),
                                DataAfter = IHelper.ConvertLogData(s.DataAfter),
                                ClassName = s.ClassName,
                                DocType = s.DocType,
                                FunId = s.FunId,
                                OpId = s.OpId,
                                UserId = s.UserId
                            }).ToList();
                    }
                    if (xtraTabControl2.SelectedTabPageIndex == 1)
                    {
                        PrintLogGridControl.DataSource = DB.SkladBase().GetPrintLog(wbStartDate.DateTime, wbEndDate.DateTime, (int)UserComboBox.EditValue).ToList();
                    }
                    if (xtraTabControl2.SelectedTabPageIndex == 2)
                    {
                        ErrorLogGridControl.DataSource = DB.SkladBase().v_ErrorLog.AsNoTracking().OrderByDescending(o => o.OnDate).Take(100).ToList();
                    }
                    break;

                case 6:
                    CommonParamsBS.DataSource = DBHelper.CommonParam;
                    break;
            }
        }

        private void NewItemBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (focused_tree_node == null)
            {
                return;
            }

            switch (focused_tree_node.GType)
            {
                case 1:
                    new frmUserEdit().ShowDialog();

                    RefrechItemBtn.PerformClick();
                    break;
            }
            RefrechItemBtn.PerformClick();
        }

        private void UsersGridView_DoubleClick(object sender, EventArgs e)
        {
            EditItemBtn.PerformClick();
        }

        private void DeleteItemBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (focused_tree_node == null)
            {
                return;
            }

            if (MessageBox.Show("Ви дійсно бажаєте видалити вибрані записи ?", "Підтвердіть видалення", MessageBoxButtons.YesNo, MessageBoxIcon.Information) != DialogResult.Yes)
            {
                return;
            }

            switch (focused_tree_node.GType)
            {
                case 1:
                    var u = UsersGridView.GetFocusedRow() as v_Users;
                    using (var db = DB.SkladBase())
                    {
                        db.DeleteWhere<Users>(w => w.UserId == u.UserId);
                    }
                    break;

                case 5:

                    if (xtraTabControl2.SelectedTabPageIndex == 2)
                    {
                        if(ErrorLogGridView.SelectedRowsCount >0)
                        {
                            Int32[] selectedRowHandles = ErrorLogGridView.GetSelectedRows();

                            for (int i = 0; i < selectedRowHandles.Length; i++)
                            {
                                int selectedRowHandle = selectedRowHandles[i];
                                if (selectedRowHandle >= 0)
                                {
                                    var log_item = ErrorLogGridView.GetRow(selectedRowHandle) as v_ErrorLog;
                                    DB.SkladBase().DeleteWhere<ErrorLog>(w => w.Id == log_item.Id);
                                }
                            }
                        }
                        else
                        {
                            var er = ErrorLogGridView.GetFocusedRow() as v_ErrorLog;
                            using (var db = DB.SkladBase())
                            {
                                db.DeleteWhere<ErrorLog>(w => w.Id == er.Id);
                            }
                        }
                    }

                    break;
            }

            RefrechItemBtn.PerformClick();
        }

        private void OprLogGridView_PopupMenuShowing(object sender, DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs e)
        {
            if (e.HitInfo.InRow)
            {
                Point p2 = Control.MousePosition;
                OperLogPopupMenu.ShowPopup(p2);
            }
        }

        private void wbStartDate_EditValueChanged(object sender, EventArgs e)
        {
            RefrechItemBtn.PerformClick();
        }

        private void OprLogGridView_DoubleClick(object sender, EventArgs e)
        {
            EditItemBtn.PerformClick();
        }

        private void barButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var dr = OprLogGridView.GetFocusedRow() as GetOperLog_Result;
            new frmLogHistory(dr.TabId, dr.Id).ShowDialog();
        }

        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (Regex.IsMatch(richEditControl1.Text, @"(?i)(\W|^)(update|delete|insert)(\W|$)", RegexOptions.IgnoreCase) || string.IsNullOrEmpty(richEditControl1.Text.Trim()))
            {
                return;
            }

            gridView1.Columns.Clear();
            gridView1.GroupSummary.Clear();

            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["SPBaseModel"].ConnectionString))
            {
                SqlCommand command = new SqlCommand(richEditControl1.Text, connection);
                command.Connection.Open();
                gridControl1.DataSource = command.ExecuteReader();
            }
        }

        private void barButtonItem5_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            dynamic s = gridView2.GetFocusedRow();
            using (var db = DB.SkladBase())
            {
                Guid id =  s.Id;
                var wb = db.WaybillList.FirstOrDefault(w => w.Id == id);
                if (wb != null)
                {
                    wb.SessionId = null;
                }

                var pp = db.ProductionPlans.Find(id);
                if (pp != null)
                {
                    pp.SessionId = null;
                }
                db.SaveChanges();

                RefrechItemBtn.PerformClick();
            }
        }

        private void gridView2_PopupMenuShowing(object sender, DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs e)
        {
            if (e.HitInfo.InRow)
            {
                Point p2 = Control.MousePosition;
                SessionPopupMenu.ShowPopup(p2);
            }
        }

        private void PatchEdit_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                PatchEdit.EditValue = folderBrowserDialog1.SelectedPath;

                using (var db = DB.SkladBase())
                {
                    var c = db.CommonParams.FirstOrDefault();
                    if (c != null)
                    {
                        c.TemplatePatch = folderBrowserDialog1.SelectedPath;
                        db.SaveChanges();
                    }
                }

                DBHelper.CommonParam = null;
            }
        }

        private void xtraTabControl2_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            RefrechItemBtn.PerformClick();
        }

        private void OnDateDBEdit_EditValueChanged(object sender, EventArgs e)
        {
            if (!EndPeriodDateEdit.ContainsFocus)
            {
                return;
            }

            PeriodChangeBtn.PerformClick();
        }

        private void PeriodChangeBtn_Click(object sender, EventArgs e)
        {
            using (var db = DB.SkladBase())
            {
                var c = db.CommonParams.FirstOrDefault();
                if (c != null)
                {
                    c.EndCalcPeriod = EndPeriodDateEdit.DateTime;
                    db.SaveChanges();
                }
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Ви дійсно бажаєте видалити історію залишків по всім товарам ?", "Історія залишків буде видалена по " + delTurnDate.DateTime.Date+" включно !", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
            {
                var dt = delTurnDate.DateTime.Date;
                using (var db = DB.SkladBase())
                {
                    foreach (var item in db.Materials.ToList())
                    {
                        var min_date_post = db.v_PosRemains.Where(w => w.MatId == item.MatId).Select(s=> s.OnDate).ToList();
                        if (min_date_post.Count > 0 && min_date_post.Min() < delTurnDate.DateTime.Date)
                        {
                            dt = min_date_post.Min().Date.AddDays(-1);
                        }

                        var pos = db.Database.ExecuteSqlCommand(@"
                         delete from [PosRemains]
                         where PosId IN (
                           SELECT PosId 
                           FROM [PosRemains]
                           where Remain = 0 and Ordered=0 and  MatId = {0} and OnDate <= {1}
                           group by PosId)", item.MatId, dt);
                    }

                    db.SaveChanges();
                }

                MessageBox.Show("Історія по залишкам очищена !");
            }
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            using (var frm = new frmAddBarCodeScanner())
            {
                if(frm.ShowDialog() == DialogResult.OK)
                {
                    DeviceNameRMKTextEdit.Text = frm.DeviceName;
                }
            }
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            using (var frm = new frmTestComPort(ComPortNameEdit.Text, Convert.ToInt32(ComPortSpeedEdit.Text)))
            {
                frm.ShowDialog();
            }
        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            using (var frm = new frmTestComPort(ComPortName2Edit.Text, Convert.ToInt32(ComPortSpeed2Edit.Text)))
            {
                frm.ShowDialog();
            }
        }

        private void linqInstantFeedbackSource1_GetQueryable(object sender, DevExpress.Data.Linq.GetQueryableEventArgs e)
        {
            e.QueryableSource = new BaseEntities().v_LoginHistory;
        }

        private void barButtonItem7_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                richEditControl1.LoadDocument(openFileDialog1.FileName); 
            }
        }

        private void barButtonItem8_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                richEditControl1.SaveDocument(saveFileDialog1.FileName, DevExpress.XtraRichEdit.DocumentFormat.PlainText);
            }
        }

        private void CopyItemBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void barButtonItem11_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            using (MemoryStream ms_xlsx = new MemoryStream())
            {
                gridView1.ExportToXlsx(ms_xlsx);
                new frmSpreadsheed(ms_xlsx.ToArray()).Show();
            }
        }

        private void barButtonItem9_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            using (MemoryStream ms_pdf = new MemoryStream())
            {
                gridView1.ExportToPdf(ms_pdf);
                new frmPdfView(ms_pdf.ToArray()).Show();
            }
        }

        private void barButtonItem12_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            gridView1.PrintDialog();
        }

        private void barCheckItem1_CheckedChanged(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gridView1.OptionsBehavior.AlignGroupSummaryInGroupRow == DevExpress.Utils.DefaultBoolean.Default)
            {
                gridView1.OptionsBehavior.AlignGroupSummaryInGroupRow = DevExpress.Utils.DefaultBoolean.True;
            }
            else
            {
                gridView1.OptionsBehavior.AlignGroupSummaryInGroupRow = DevExpress.Utils.DefaultBoolean.Default;
            }
        }

        private void gridView1_CustomSummaryExists(object sender, DevExpress.Data.CustomSummaryExistEventArgs e)
        {
            GridSummaryItem item = e.Item as GridSummaryItem;
            if (item == null) return;
            GridColumn col = ((ColumnView)sender).Columns[item.FieldName];
            if (col == null) return;
            item.DisplayFormat = "{0:" + col.DisplayFormat.FormatString + "}";
        }

        private void buttonEdit1_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                AttachedFilesPathEdit.EditValue = folderBrowserDialog1.SelectedPath;

                using (var db = DB.SkladBase())
                {
                    var c = db.CommonParams.FirstOrDefault();
                    if (c != null)
                    {
                        c.AttachedFilesPath = folderBrowserDialog1.SelectedPath;
                        db.SaveChanges();
                    }
                }

                DBHelper.CommonParam = null;
            }
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if(xtraTabControl2.SelectedTabPageIndex == 0)
            {
                IHelper.ExportToXlsx(OprLogGridControl); 
            }

            if (xtraTabControl2.SelectedTabPageIndex == 1)
            {
                IHelper.ExportToXlsx(PrintLogGridControl);
            }

            if (xtraTabControl2.SelectedTabPageIndex == 2)
            {
                IHelper.ExportToXlsx(ErrorLogGridControl);
            }

            if (xtraTabControl2.SelectedTabPageIndex == 3)
            {
                IHelper.ExportToXlsx(LoginHistoryGridControl);
            }
        }
    }
}
