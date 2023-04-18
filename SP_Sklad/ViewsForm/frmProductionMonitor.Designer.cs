namespace SP_Sklad.ViewsForm
{
    partial class frmProductionMonitor
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmProductionMonitor));
            this.ProductionMonitorGrid = new DevExpress.XtraGrid.GridControl();
            this.ProductionMonitorBS = new System.Windows.Forms.BindingSource(this.components);
            this.ProductionMonitorGridView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colNum = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colOnDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTechProcessName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTechProcessStartDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colManufacturingTime = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colMatName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colWhName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemProgressBar1 = new DevExpress.XtraEditors.Repository.RepositoryItemProgressBar();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.ProductionMonitorGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ProductionMonitorBS)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ProductionMonitorGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemProgressBar1)).BeginInit();
            this.SuspendLayout();
            // 
            // ProductionMonitorGrid
            // 
            this.ProductionMonitorGrid.DataSource = this.ProductionMonitorBS;
            this.ProductionMonitorGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ProductionMonitorGrid.Location = new System.Drawing.Point(0, 0);
            this.ProductionMonitorGrid.MainView = this.ProductionMonitorGridView;
            this.ProductionMonitorGrid.Name = "ProductionMonitorGrid";
            this.ProductionMonitorGrid.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemProgressBar1});
            this.ProductionMonitorGrid.Size = new System.Drawing.Size(1458, 580);
            this.ProductionMonitorGrid.TabIndex = 19;
            this.ProductionMonitorGrid.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.ProductionMonitorGridView});
            // 
            // ProductionMonitorBS
            // 
            this.ProductionMonitorBS.DataSource = typeof(SP_Sklad.SkladData.v_ProductionMonitor);
            this.ProductionMonitorBS.AddingNew += new System.ComponentModel.AddingNewEventHandler(this.KontragentGroupBS_AddingNew);
            // 
            // ProductionMonitorGridView
            // 
            this.ProductionMonitorGridView.Appearance.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 10F);
            this.ProductionMonitorGridView.Appearance.HeaderPanel.Options.UseFont = true;
            this.ProductionMonitorGridView.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.ProductionMonitorGridView.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.ProductionMonitorGridView.Appearance.Row.Font = new System.Drawing.Font("Tahoma", 10F);
            this.ProductionMonitorGridView.Appearance.Row.Options.UseFont = true;
            this.ProductionMonitorGridView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colNum,
            this.colOnDate,
            this.colTechProcessName,
            this.colTechProcessStartDate,
            this.colManufacturingTime,
            this.colMatName,
            this.colWhName,
            this.gridColumn1,
            this.gridColumn2});
            this.ProductionMonitorGridView.GridControl = this.ProductionMonitorGrid;
            this.ProductionMonitorGridView.Name = "ProductionMonitorGridView";
            this.ProductionMonitorGridView.OptionsBehavior.AllowIncrementalSearch = true;
            this.ProductionMonitorGridView.OptionsBehavior.EditorShowMode = DevExpress.Utils.EditorShowMode.MouseDownFocused;
            this.ProductionMonitorGridView.OptionsBehavior.ReadOnly = true;
            this.ProductionMonitorGridView.OptionsFind.AlwaysVisible = true;
            this.ProductionMonitorGridView.OptionsSelection.EnableAppearanceFocusedRow = false;
            this.ProductionMonitorGridView.OptionsView.ColumnHeaderAutoHeight = DevExpress.Utils.DefaultBoolean.True;
            this.ProductionMonitorGridView.OptionsView.ShowFooter = true;
            this.ProductionMonitorGridView.OptionsView.ShowGroupPanel = false;
            this.ProductionMonitorGridView.RowHeight = 25;
            this.ProductionMonitorGridView.CustomDrawCell += new DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventHandler(this.ProductionMonitorGridView_CustomDrawCell);
            this.ProductionMonitorGridView.RowDeleted += new DevExpress.Data.RowDeletedEventHandler(this.WhRemainGridView_RowDeleted);
            // 
            // colNum
            // 
            this.colNum.Caption = "№ виробництва";
            this.colNum.FieldName = "Num";
            this.colNum.Name = "colNum";
            this.colNum.Visible = true;
            this.colNum.VisibleIndex = 0;
            this.colNum.Width = 97;
            // 
            // colOnDate
            // 
            this.colOnDate.Caption = "Дата виробництва";
            this.colOnDate.DisplayFormat.FormatString = "g";
            this.colOnDate.FieldName = "OnDate";
            this.colOnDate.Name = "colOnDate";
            this.colOnDate.Visible = true;
            this.colOnDate.VisibleIndex = 1;
            this.colOnDate.Width = 129;
            // 
            // colTechProcessName
            // 
            this.colTechProcessName.Caption = "Технологічний процес";
            this.colTechProcessName.FieldName = "TechProcessName";
            this.colTechProcessName.Name = "colTechProcessName";
            this.colTechProcessName.Visible = true;
            this.colTechProcessName.VisibleIndex = 7;
            this.colTechProcessName.Width = 167;
            // 
            // colTechProcessStartDate
            // 
            this.colTechProcessStartDate.Caption = "Початок техпроцесу";
            this.colTechProcessStartDate.DisplayFormat.FormatString = "g";
            this.colTechProcessStartDate.FieldName = "TechProcessStartDate";
            this.colTechProcessStartDate.Name = "colTechProcessStartDate";
            this.colTechProcessStartDate.Visible = true;
            this.colTechProcessStartDate.VisibleIndex = 4;
            this.colTechProcessStartDate.Width = 132;
            // 
            // colManufacturingTime
            // 
            this.colManufacturingTime.Caption = "Закінчення техпроцесу";
            this.colManufacturingTime.DisplayFormat.FormatString = "g";
            this.colManufacturingTime.FieldName = "TechProcessEndDate";
            this.colManufacturingTime.Name = "colManufacturingTime";
            this.colManufacturingTime.Visible = true;
            this.colManufacturingTime.VisibleIndex = 5;
            this.colManufacturingTime.Width = 130;
            // 
            // colMatName
            // 
            this.colMatName.Caption = "Рецепт";
            this.colMatName.FieldName = "MatName";
            this.colMatName.Name = "colMatName";
            this.colMatName.Visible = true;
            this.colMatName.VisibleIndex = 2;
            this.colMatName.Width = 194;
            // 
            // colWhName
            // 
            this.colWhName.Caption = "Цех";
            this.colWhName.FieldName = "WhName";
            this.colWhName.Name = "colWhName";
            this.colWhName.Visible = true;
            this.colWhName.VisibleIndex = 3;
            this.colWhName.Width = 232;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "Стан";
            this.gridColumn1.ColumnEdit = this.repositoryItemProgressBar1;
            this.gridColumn1.FieldName = "Pct";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 8;
            this.gridColumn1.Width = 254;
            // 
            // repositoryItemProgressBar1
            // 
            this.repositoryItemProgressBar1.EndColor = System.Drawing.Color.Empty;
            this.repositoryItemProgressBar1.LookAndFeel.SkinName = "The Bezier";
            this.repositoryItemProgressBar1.LookAndFeel.UseDefaultLookAndFeel = false;
            this.repositoryItemProgressBar1.Name = "repositoryItemProgressBar1";
            this.repositoryItemProgressBar1.ShowTitle = true;
            this.repositoryItemProgressBar1.StartColor = System.Drawing.Color.Empty;
            this.repositoryItemProgressBar1.Step = 1;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "Тривалість";
            this.gridColumn2.FieldName = "Duration";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.OptionsColumn.AllowEdit = false;
            this.gridColumn2.UnboundDataType = typeof(System.TimeSpan);
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 6;
            this.gridColumn2.Width = 98;
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 60000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // frmProductionMonitor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1458, 580);
            this.Controls.Add(this.ProductionMonitorGrid);
            this.IconOptions.Image = ((System.Drawing.Image)(resources.GetObject("frmProductionMonitor.IconOptions.Image")));
            this.Name = "frmProductionMonitor";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Монітор виробництва";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmKaGroup_FormClosed);
            this.Load += new System.EventHandler(this.frmKaGroup_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ProductionMonitorGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ProductionMonitorBS)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ProductionMonitorGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemProgressBar1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private DevExpress.XtraGrid.GridControl ProductionMonitorGrid;
        public DevExpress.XtraGrid.Views.Grid.GridView ProductionMonitorGridView;
        private System.Windows.Forms.BindingSource ProductionMonitorBS;
        private DevExpress.XtraGrid.Columns.GridColumn colNum;
        private DevExpress.XtraGrid.Columns.GridColumn colOnDate;
        private DevExpress.XtraGrid.Columns.GridColumn colTechProcessName;
        private DevExpress.XtraGrid.Columns.GridColumn colTechProcessStartDate;
        private DevExpress.XtraGrid.Columns.GridColumn colManufacturingTime;
        private DevExpress.XtraGrid.Columns.GridColumn colMatName;
        private DevExpress.XtraGrid.Columns.GridColumn colWhName;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraEditors.Repository.RepositoryItemProgressBar repositoryItemProgressBar1;
        private System.Windows.Forms.Timer timer1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
    }
}