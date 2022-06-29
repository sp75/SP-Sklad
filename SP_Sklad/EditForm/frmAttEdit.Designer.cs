namespace SP_Sklad.EditForm
{
    partial class frmAttEdit
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
            this.BottomPanel = new DevExpress.XtraEditors.PanelControl();
            this.OkButton = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.styleController1 = new DevExpress.XtraEditors.StyleController(this.components);
            this.ReceivedTextEdit = new DevExpress.XtraEditors.TextEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.textEdit1 = new DevExpress.XtraEditors.TextEdit();
            this.labelControl7 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.AttDateEdit = new DevExpress.XtraEditors.DateEdit();
            this.CarsLookUpEdit = new DevExpress.XtraEditors.LookUpEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.OnDateDBEdit = new DevExpress.XtraEditors.DateEdit();
            this.WaybillListBS = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.BottomPanel)).BeginInit();
            this.BottomPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.styleController1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ReceivedTextEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.AttDateEdit.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.AttDateEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CarsLookUpEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.OnDateDBEdit.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.OnDateDBEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.WaybillListBS)).BeginInit();
            this.SuspendLayout();
            // 
            // BottomPanel
            // 
            this.BottomPanel.Controls.Add(this.OkButton);
            this.BottomPanel.Controls.Add(this.simpleButton1);
            this.BottomPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.BottomPanel.Location = new System.Drawing.Point(0, 271);
            this.BottomPanel.Name = "BottomPanel";
            this.BottomPanel.Size = new System.Drawing.Size(451, 52);
            this.BottomPanel.TabIndex = 21;
            // 
            // OkButton
            // 
            this.OkButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.OkButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.OkButton.Location = new System.Drawing.Point(233, 10);
            this.OkButton.Name = "OkButton";
            this.OkButton.Size = new System.Drawing.Size(100, 30);
            this.OkButton.TabIndex = 1;
            this.OkButton.Text = "Застосувати";
            // 
            // simpleButton1
            // 
            this.simpleButton1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.simpleButton1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.simpleButton1.Location = new System.Drawing.Point(339, 10);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(100, 30);
            this.simpleButton1.TabIndex = 0;
            this.simpleButton1.Text = "Видалити";
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // styleController1
            // 
            this.styleController1.Appearance.Font = new System.Drawing.Font("Tahoma", 10F);
            this.styleController1.Appearance.Options.UseFont = true;
            // 
            // ReceivedTextEdit
            // 
            this.ReceivedTextEdit.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ReceivedTextEdit.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.WaybillListBS, "Received", true));
            this.ReceivedTextEdit.Location = new System.Drawing.Point(24, 39);
            this.ReceivedTextEdit.Name = "ReceivedTextEdit";
            this.ReceivedTextEdit.Size = new System.Drawing.Size(406, 22);
            this.ReceivedTextEdit.StyleController = this.styleController1;
            this.ReceivedTextEdit.TabIndex = 45;
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.labelControl3.Location = new System.Drawing.Point(24, 17);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(79, 16);
            this.labelControl3.TabIndex = 44;
            this.labelControl3.Text = "Через кого:";
            // 
            // textEdit1
            // 
            this.textEdit1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textEdit1.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.WaybillListBS, "AttNum", true));
            this.textEdit1.Location = new System.Drawing.Point(24, 100);
            this.textEdit1.Name = "textEdit1";
            this.textEdit1.Size = new System.Drawing.Size(195, 22);
            this.textEdit1.StyleController = this.styleController1;
            this.textEdit1.TabIndex = 46;
            // 
            // labelControl7
            // 
            this.labelControl7.Location = new System.Drawing.Point(24, 78);
            this.labelControl7.Name = "labelControl7";
            this.labelControl7.Size = new System.Drawing.Size(42, 16);
            this.labelControl7.StyleController = this.styleController1;
            this.labelControl7.TabIndex = 47;
            this.labelControl7.Text = "Номер:";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(245, 78);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(76, 16);
            this.labelControl1.StyleController = this.styleController1;
            this.labelControl1.TabIndex = 48;
            this.labelControl1.Text = "Дата видачі:";
            // 
            // AttDateEdit
            // 
            this.AttDateEdit.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.WaybillListBS, "AttDate", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.AttDateEdit.EditValue = null;
            this.AttDateEdit.Location = new System.Drawing.Point(245, 100);
            this.AttDateEdit.Name = "AttDateEdit";
            this.AttDateEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.AttDateEdit.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.AttDateEdit.Size = new System.Drawing.Size(185, 22);
            this.AttDateEdit.StyleController = this.styleController1;
            this.AttDateEdit.TabIndex = 49;
            // 
            // CarsLookUpEdit
            // 
            this.CarsLookUpEdit.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.WaybillListBS, "RouteId", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.CarsLookUpEdit.Location = new System.Drawing.Point(24, 159);
            this.CarsLookUpEdit.Name = "CarsLookUpEdit";
            this.CarsLookUpEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Search)});
            this.CarsLookUpEdit.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("RouteName", 40, "Маршрут"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Name", 40, "Назва машини"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Number", "Номер машини")});
            this.CarsLookUpEdit.Properties.DisplayMember = "RouteName";
            this.CarsLookUpEdit.Properties.PopupWidth = 600;
            this.CarsLookUpEdit.Properties.ShowFooter = false;
            this.CarsLookUpEdit.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.CarsLookUpEdit.Properties.ValueMember = "RouteId";
            this.CarsLookUpEdit.Properties.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.CarsLookUpEdit_Properties_ButtonClick);
            this.CarsLookUpEdit.Size = new System.Drawing.Size(406, 22);
            this.CarsLookUpEdit.StyleController = this.styleController1;
            this.CarsLookUpEdit.TabIndex = 51;
            this.CarsLookUpEdit.EditValueChanged += new System.EventHandler(this.CarsLookUpEdit_EditValueChanged);
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(24, 137);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(106, 16);
            this.labelControl2.StyleController = this.styleController1;
            this.labelControl2.TabIndex = 50;
            this.labelControl2.Text = "Маршрут\\Машина";
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(24, 201);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(122, 16);
            this.labelControl4.StyleController = this.styleController1;
            this.labelControl4.TabIndex = 53;
            this.labelControl4.Text = "Дата відвантаження";
            // 
            // OnDateDBEdit
            // 
            this.OnDateDBEdit.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.WaybillListBS, "ShipmentDate", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.OnDateDBEdit.EditValue = null;
            this.OnDateDBEdit.Location = new System.Drawing.Point(24, 223);
            this.OnDateDBEdit.Name = "OnDateDBEdit";
            this.OnDateDBEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.OnDateDBEdit.Properties.CalendarTimeEditing = DevExpress.Utils.DefaultBoolean.True;
            this.OnDateDBEdit.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.OnDateDBEdit.Properties.Mask.EditMask = "g";
            this.OnDateDBEdit.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.OnDateDBEdit.Properties.MinValue = new System.DateTime(2000, 1, 1, 0, 0, 0, 0);
            this.OnDateDBEdit.Size = new System.Drawing.Size(195, 22);
            this.OnDateDBEdit.StyleController = this.styleController1;
            this.OnDateDBEdit.TabIndex = 54;
            // 
            // WaybillListBS
            // 
            this.WaybillListBS.DataSource = typeof(SP_Sklad.SkladData.WaybillList);
            // 
            // frmAttEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(451, 323);
            this.Controls.Add(this.OnDateDBEdit);
            this.Controls.Add(this.labelControl4);
            this.Controls.Add(this.CarsLookUpEdit);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.AttDateEdit);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.labelControl7);
            this.Controls.Add(this.textEdit1);
            this.Controls.Add(this.ReceivedTextEdit);
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.BottomPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmAttEdit";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Доручення";
            this.Load += new System.EventHandler(this.frmAttEdit_Load);
            ((System.ComponentModel.ISupportInitialize)(this.BottomPanel)).EndInit();
            this.BottomPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.styleController1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ReceivedTextEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.AttDateEdit.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.AttDateEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CarsLookUpEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.OnDateDBEdit.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.OnDateDBEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.WaybillListBS)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl BottomPanel;
        public DevExpress.XtraEditors.SimpleButton OkButton;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private DevExpress.XtraEditors.StyleController styleController1;
        private System.Windows.Forms.BindingSource WaybillListBS;
        private DevExpress.XtraEditors.TextEdit ReceivedTextEdit;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.TextEdit textEdit1;
        private DevExpress.XtraEditors.LabelControl labelControl7;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.DateEdit AttDateEdit;
        private DevExpress.XtraEditors.LookUpEdit CarsLookUpEdit;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.DateEdit OnDateDBEdit;
    }
}