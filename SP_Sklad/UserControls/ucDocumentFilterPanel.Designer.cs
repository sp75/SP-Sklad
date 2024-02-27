
namespace SP_Sklad.UserControls
{
    partial class ucDocumentFilterPanel
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucDocumentFilterPanel));
            DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions1 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject1 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject2 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject3 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject4 = new DevExpress.Utils.SerializableAppearanceObject();
            this.imageCollection1 = new DevExpress.Utils.ImageCollection(this.components);
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.PeriodComboBoxEdit = new DevExpress.XtraEditors.ComboBoxEdit();
            this.styleController1 = new DevExpress.XtraEditors.StyleController(this.components);
            this.wbStatusList = new DevExpress.XtraEditors.LookUpEdit();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.wbKagentList = new DevExpress.XtraEditors.LookUpEdit();
            this.kaListLabelControl = new DevExpress.XtraEditors.LabelControl();
            this.wbEndDate = new DevExpress.XtraEditors.DateEdit();
            this.wbStartDate = new DevExpress.XtraEditors.DateEdit();
            this.imageCollection2 = new DevExpress.Utils.ImageCollection(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PeriodComboBoxEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.styleController1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.wbStatusList.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.wbKagentList.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.wbEndDate.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.wbEndDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.wbStartDate.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.wbStartDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection2)).BeginInit();
            this.SuspendLayout();
            // 
            // imageCollection1
            // 
            this.imageCollection1.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("imageCollection1.ImageStream")));
            this.imageCollection1.TransparentColor = System.Drawing.Color.White;
            this.imageCollection1.InsertImage(global::SP_Sklad.Properties.Resources.kontragents_folder, "kontragents_folder", typeof(global::SP_Sklad.Properties.Resources), 0);
            this.imageCollection1.Images.SetKeyName(0, "kontragents_folder");
            this.imageCollection1.InsertImage(global::SP_Sklad.Properties.Resources.supplier, "supplier", typeof(global::SP_Sklad.Properties.Resources), 1);
            this.imageCollection1.Images.SetKeyName(1, "supplier");
            // 
            // panelControl2
            // 
            this.panelControl2.Controls.Add(this.PeriodComboBoxEdit);
            this.panelControl2.Controls.Add(this.wbStatusList);
            this.panelControl2.Controls.Add(this.labelControl4);
            this.panelControl2.Controls.Add(this.wbKagentList);
            this.panelControl2.Controls.Add(this.kaListLabelControl);
            this.panelControl2.Controls.Add(this.wbEndDate);
            this.panelControl2.Controls.Add(this.wbStartDate);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl2.Location = new System.Drawing.Point(0, 0);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(1286, 54);
            this.panelControl2.TabIndex = 2;
            // 
            // PeriodComboBoxEdit
            // 
            this.PeriodComboBoxEdit.EditValue = "Довільний період";
            this.PeriodComboBoxEdit.Location = new System.Drawing.Point(10, 15);
            this.PeriodComboBoxEdit.Name = "PeriodComboBoxEdit";
            this.PeriodComboBoxEdit.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.PeriodComboBoxEdit.Properties.Appearance.Options.UseFont = true;
            this.PeriodComboBoxEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.PeriodComboBoxEdit.Properties.Items.AddRange(new object[] {
            "Довільний період",
            "За поточний день",
            "З початку неділі",
            "З початку місяця",
            "З початку року"});
            this.PeriodComboBoxEdit.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.PeriodComboBoxEdit.Size = new System.Drawing.Size(156, 22);
            this.PeriodComboBoxEdit.StyleController = this.styleController1;
            this.PeriodComboBoxEdit.TabIndex = 21;
            this.PeriodComboBoxEdit.EditValueChanged += new System.EventHandler(this.PeriodComboBoxEdit_EditValueChanged);
            // 
            // styleController1
            // 
            this.styleController1.Appearance.Font = new System.Drawing.Font("Tahoma", 10F);
            this.styleController1.Appearance.Options.UseFont = true;
            // 
            // wbStatusList
            // 
            this.wbStatusList.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.wbStatusList.Location = new System.Drawing.Point(1079, 15);
            this.wbStatusList.Name = "wbStatusList";
            this.wbStatusList.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.wbStatusList.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Name", "Name")});
            this.wbStatusList.Properties.DisplayMember = "Name";
            this.wbStatusList.Properties.ShowHeader = false;
            this.wbStatusList.Properties.ValueMember = "Id";
            this.wbStatusList.Size = new System.Drawing.Size(196, 22);
            this.wbStatusList.StyleController = this.styleController1;
            this.wbStatusList.TabIndex = 8;
            this.wbStatusList.EditValueChanged += new System.EventHandler(this.wbStatusList_EditValueChanged);
            // 
            // labelControl4
            // 
            this.labelControl4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelControl4.Location = new System.Drawing.Point(1034, 18);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(39, 16);
            this.labelControl4.StyleController = this.styleController1;
            this.labelControl4.TabIndex = 7;
            this.labelControl4.Text = "Статус";
            // 
            // wbKagentList
            // 
            this.wbKagentList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.wbKagentList.Location = new System.Drawing.Point(589, 15);
            this.wbKagentList.Name = "wbKagentList";
            editorButtonImageOptions1.ImageIndex = 0;
            editorButtonImageOptions1.ImageList = this.imageCollection1;
            this.wbKagentList.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Clear),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, true, false, editorButtonImageOptions1, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject1, serializableAppearanceObject2, serializableAppearanceObject3, serializableAppearanceObject4, "", null, null, DevExpress.Utils.ToolTipAnchor.Default)});
            this.wbKagentList.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Name", "Назва")});
            this.wbKagentList.Properties.DisplayMember = "Name";
            this.wbKagentList.Properties.SearchMode = DevExpress.XtraEditors.Controls.SearchMode.AutoSuggest;
            this.wbKagentList.Properties.ShowHeader = false;
            this.wbKagentList.Properties.ValueMember = "KaId";
            this.wbKagentList.Properties.MouseUp += new System.Windows.Forms.MouseEventHandler(this.wbKagentList_Properties_MouseUp);
            this.wbKagentList.Size = new System.Drawing.Size(397, 24);
            this.wbKagentList.StyleController = this.styleController1;
            this.wbKagentList.TabIndex = 6;
            this.wbKagentList.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.wbKagentList_ButtonClick);
            this.wbKagentList.EditValueChanged += new System.EventHandler(this.wbKagentList_EditValueChanged);
            // 
            // kaListLabelControl
            // 
            this.kaListLabelControl.Location = new System.Drawing.Point(500, 18);
            this.kaListLabelControl.Name = "kaListLabelControl";
            this.kaListLabelControl.Size = new System.Drawing.Size(83, 16);
            this.kaListLabelControl.StyleController = this.styleController1;
            this.kaListLabelControl.TabIndex = 4;
            this.kaListLabelControl.Text = "Постачальник";
            // 
            // wbEndDate
            // 
            this.wbEndDate.EditValue = null;
            this.wbEndDate.Location = new System.Drawing.Point(318, 15);
            this.wbEndDate.Name = "wbEndDate";
            this.wbEndDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.wbEndDate.Properties.CalendarTimeEditing = DevExpress.Utils.DefaultBoolean.True;
            this.wbEndDate.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.wbEndDate.Properties.DisplayFormat.FormatString = "";
            this.wbEndDate.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.wbEndDate.Properties.EditFormat.FormatString = "";
            this.wbEndDate.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.wbEndDate.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.wbEndDate.Properties.MaskSettings.Set("mask", "g");
            this.wbEndDate.Size = new System.Drawing.Size(140, 22);
            this.wbEndDate.StyleController = this.styleController1;
            this.wbEndDate.TabIndex = 3;
            this.wbEndDate.EditValueChanged += new System.EventHandler(this.wbEndDate_EditValueChanged);
            // 
            // wbStartDate
            // 
            this.wbStartDate.EditValue = null;
            this.wbStartDate.Location = new System.Drawing.Point(172, 15);
            this.wbStartDate.Name = "wbStartDate";
            this.wbStartDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.wbStartDate.Properties.CalendarTimeEditing = DevExpress.Utils.DefaultBoolean.True;
            this.wbStartDate.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.wbStartDate.Properties.DisplayFormat.FormatString = "";
            this.wbStartDate.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.wbStartDate.Properties.EditFormat.FormatString = "";
            this.wbStartDate.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.wbStartDate.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.wbStartDate.Properties.MaskSettings.Set("mask", "g");
            this.wbStartDate.Size = new System.Drawing.Size(140, 22);
            this.wbStartDate.StyleController = this.styleController1;
            this.wbStartDate.TabIndex = 1;
            this.wbStartDate.EditValueChanged += new System.EventHandler(this.wbStartDate_EditValueChanged);
            // 
            // imageCollection2
            // 
            this.imageCollection2.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("imageCollection2.ImageStream")));
            this.imageCollection2.TransparentColor = System.Drawing.Color.White;
            this.imageCollection2.InsertImage(global::SP_Sklad.Properties.Resources.kontragents_folder, "kontragents_folder", typeof(global::SP_Sklad.Properties.Resources), 0);
            this.imageCollection2.Images.SetKeyName(0, "kontragents_folder");
            this.imageCollection2.InsertImage(global::SP_Sklad.Properties.Resources.supplier, "supplier", typeof(global::SP_Sklad.Properties.Resources), 1);
            this.imageCollection2.Images.SetKeyName(1, "supplier");
            // 
            // ucDocumentFilterPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelControl2);
            this.Name = "ucDocumentFilterPanel";
            this.Size = new System.Drawing.Size(1286, 54);
            this.Load += new System.EventHandler(this.ucWBFilterPanel_Load);
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            this.panelControl2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PeriodComboBoxEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.styleController1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.wbStatusList.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.wbKagentList.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.wbEndDate.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.wbEndDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.wbStartDate.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.wbStartDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraEditors.ComboBoxEdit PeriodComboBoxEdit;
        public DevExpress.XtraEditors.LookUpEdit wbStatusList;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        public DevExpress.XtraEditors.LookUpEdit wbKagentList;
        private DevExpress.XtraEditors.LabelControl kaListLabelControl;
        public DevExpress.XtraEditors.DateEdit wbEndDate;
        public DevExpress.XtraEditors.DateEdit wbStartDate;
        private DevExpress.XtraEditors.StyleController styleController1;
        private DevExpress.Utils.ImageCollection imageCollection1;
        private DevExpress.Utils.ImageCollection imageCollection2;
    }
}
