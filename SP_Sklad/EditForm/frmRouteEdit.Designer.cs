namespace SP_Sklad.EditForm
{
    partial class frmRouteEdit
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
            this.NotesTextEdit = new DevExpress.XtraEditors.TextEdit();
            this.styleController1 = new DevExpress.XtraEditors.StyleController(this.components);
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.BottomPanel = new DevExpress.XtraEditors.PanelControl();
            this.OkButton = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.DriversLookUpEdit = new DevExpress.XtraEditors.LookUpEdit();
            this.labelControl54 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.CarsLookUpEdit = new DevExpress.XtraEditors.LookUpEdit();
            this.labelControl22 = new DevExpress.XtraEditors.LabelControl();
            this.DurationEdit = new DevExpress.XtraEditors.TimeSpanEdit();
            this.RoutesBS = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.NotesTextEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.styleController1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BottomPanel)).BeginInit();
            this.BottomPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DriversLookUpEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CarsLookUpEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DurationEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.RoutesBS)).BeginInit();
            this.SuspendLayout();
            // 
            // NotesTextEdit
            // 
            this.NotesTextEdit.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.NotesTextEdit.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.RoutesBS, "Name", true));
            this.NotesTextEdit.Location = new System.Drawing.Point(12, 34);
            this.NotesTextEdit.Name = "NotesTextEdit";
            this.NotesTextEdit.Size = new System.Drawing.Size(451, 22);
            this.NotesTextEdit.StyleController = this.styleController1;
            this.NotesTextEdit.TabIndex = 40;
            // 
            // styleController1
            // 
            this.styleController1.Appearance.Font = new System.Drawing.Font("Tahoma", 10F);
            this.styleController1.Appearance.Options.UseFont = true;
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.labelControl3.Appearance.Options.UseFont = true;
            this.labelControl3.Location = new System.Drawing.Point(12, 12);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(40, 16);
            this.labelControl3.TabIndex = 39;
            this.labelControl3.Text = "Назва";
            // 
            // BottomPanel
            // 
            this.BottomPanel.Controls.Add(this.OkButton);
            this.BottomPanel.Controls.Add(this.simpleButton1);
            this.BottomPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.BottomPanel.Location = new System.Drawing.Point(0, 300);
            this.BottomPanel.Name = "BottomPanel";
            this.BottomPanel.Size = new System.Drawing.Size(475, 52);
            this.BottomPanel.TabIndex = 41;
            // 
            // OkButton
            // 
            this.OkButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.OkButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.OkButton.Location = new System.Drawing.Point(253, 10);
            this.OkButton.Name = "OkButton";
            this.OkButton.Size = new System.Drawing.Size(100, 30);
            this.OkButton.TabIndex = 1;
            this.OkButton.Text = "Застосувати";
            this.OkButton.Click += new System.EventHandler(this.OkButton_Click);
            // 
            // simpleButton1
            // 
            this.simpleButton1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.simpleButton1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.simpleButton1.Location = new System.Drawing.Point(363, 10);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(100, 30);
            this.simpleButton1.TabIndex = 0;
            this.simpleButton1.Text = "Відмінити";
            // 
            // DriversLookUpEdit
            // 
            this.DriversLookUpEdit.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.RoutesBS, "DriverId", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.DriversLookUpEdit.Location = new System.Drawing.Point(12, 101);
            this.DriversLookUpEdit.Name = "DriversLookUpEdit";
            this.DriversLookUpEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Clear)});
            this.DriversLookUpEdit.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Name", "Name1")});
            this.DriversLookUpEdit.Properties.DisplayMember = "Name";
            this.DriversLookUpEdit.Properties.NullText = "";
            this.DriversLookUpEdit.Properties.PopupSizeable = false;
            this.DriversLookUpEdit.Properties.ShowFooter = false;
            this.DriversLookUpEdit.Properties.ShowHeader = false;
            this.DriversLookUpEdit.Properties.ValueMember = "KaId";
            this.DriversLookUpEdit.Properties.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.DriversLookUpEdit_Properties_ButtonClick);
            this.DriversLookUpEdit.Size = new System.Drawing.Size(451, 22);
            this.DriversLookUpEdit.StyleController = this.styleController1;
            this.DriversLookUpEdit.TabIndex = 45;
            this.DriversLookUpEdit.EditValueChanged += new System.EventHandler(this.DriversLookUpEdit_EditValueChanged);
            // 
            // labelControl54
            // 
            this.labelControl54.Location = new System.Drawing.Point(12, 79);
            this.labelControl54.Name = "labelControl54";
            this.labelControl54.Size = new System.Drawing.Size(31, 16);
            this.labelControl54.StyleController = this.styleController1;
            this.labelControl54.TabIndex = 44;
            this.labelControl54.Text = "Водій";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(12, 141);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(48, 16);
            this.labelControl1.StyleController = this.styleController1;
            this.labelControl1.TabIndex = 46;
            this.labelControl1.Text = "Машина";
            // 
            // CarsLookUpEdit
            // 
            this.CarsLookUpEdit.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.RoutesBS, "CarId", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.CarsLookUpEdit.Location = new System.Drawing.Point(12, 163);
            this.CarsLookUpEdit.Name = "CarsLookUpEdit";
            this.CarsLookUpEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Search)});
            this.CarsLookUpEdit.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Name", "Назва"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Number", "Номер"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Kagent.Name", "Водій")});
            this.CarsLookUpEdit.Properties.DisplayMember = "Name";
            this.CarsLookUpEdit.Properties.ShowFooter = false;
            this.CarsLookUpEdit.Properties.ShowHeader = false;
            this.CarsLookUpEdit.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.CarsLookUpEdit.Properties.ValueMember = "Id";
            this.CarsLookUpEdit.Properties.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.CarsLookUpEdit_Properties_ButtonClick);
            this.CarsLookUpEdit.Size = new System.Drawing.Size(451, 22);
            this.CarsLookUpEdit.StyleController = this.styleController1;
            this.CarsLookUpEdit.TabIndex = 47;
            // 
            // labelControl22
            // 
            this.labelControl22.Location = new System.Drawing.Point(12, 215);
            this.labelControl22.Name = "labelControl22";
            this.labelControl22.Size = new System.Drawing.Size(65, 16);
            this.labelControl22.StyleController = this.styleController1;
            this.labelControl22.TabIndex = 57;
            this.labelControl22.Text = "Тривалість";
            // 
            // DurationEdit
            // 
            this.DurationEdit.EditValue = null;
            this.DurationEdit.Location = new System.Drawing.Point(12, 237);
            this.DurationEdit.Name = "DurationEdit";
            this.DurationEdit.Properties.BeepOnError = false;
            this.DurationEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.DurationEdit.Properties.DisplayFormat.FormatString = "{0:dd} днів, {0:hh} годин, {0:mm} хвилин";
            this.DurationEdit.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.DurationEdit.Properties.MaskSettings.Set("mask", "dd дні hh години mm хвилини");
            this.DurationEdit.Properties.MaskSettings.Set("allowNegativeValues", false);
            this.DurationEdit.Properties.MaxValue = System.TimeSpan.Parse("30.00:00:00");
            this.DurationEdit.Properties.MinValue = System.TimeSpan.Parse("00:00:00");
            this.DurationEdit.Size = new System.Drawing.Size(228, 22);
            this.DurationEdit.StyleController = this.styleController1;
            this.DurationEdit.TabIndex = 56;
            this.DurationEdit.EditValueChanged += new System.EventHandler(this.textEdit4_EditValueChanged);
            // 
            // RoutesBS
            // 
            this.RoutesBS.DataSource = typeof(SP_Sklad.SkladData.Routes);
            // 
            // frmRouteEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(475, 352);
            this.Controls.Add(this.labelControl22);
            this.Controls.Add(this.DurationEdit);
            this.Controls.Add(this.CarsLookUpEdit);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.DriversLookUpEdit);
            this.Controls.Add(this.labelControl54);
            this.Controls.Add(this.BottomPanel);
            this.Controls.Add(this.NotesTextEdit);
            this.Controls.Add(this.labelControl3);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmRouteEdit";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Властивості маршрута";
            this.Load += new System.EventHandler(this.frmRouteEdit_Load);
            ((System.ComponentModel.ISupportInitialize)(this.NotesTextEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.styleController1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BottomPanel)).EndInit();
            this.BottomPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DriversLookUpEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CarsLookUpEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DurationEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.RoutesBS)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.TextEdit NotesTextEdit;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.PanelControl BottomPanel;
        public DevExpress.XtraEditors.SimpleButton OkButton;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private DevExpress.XtraEditors.StyleController styleController1;
        private System.Windows.Forms.BindingSource RoutesBS;
        private DevExpress.XtraEditors.LookUpEdit DriversLookUpEdit;
        private DevExpress.XtraEditors.LabelControl labelControl54;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LookUpEdit CarsLookUpEdit;
        private DevExpress.XtraEditors.LabelControl labelControl22;
        private DevExpress.XtraEditors.TimeSpanEdit DurationEdit;
    }
}