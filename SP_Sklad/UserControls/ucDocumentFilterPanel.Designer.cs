
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
            DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions2 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject5 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject6 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject7 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject8 = new DevExpress.Utils.SerializableAppearanceObject();
            this.imageCollection1 = new DevExpress.Utils.ImageCollection(this.components);
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.KagentPanel = new DevExpress.XtraEditors.PanelControl();
            this.panelControl6 = new DevExpress.XtraEditors.PanelControl();
            this.wbKagentList = new DevExpress.XtraEditors.LookUpEdit();
            this.styleController1 = new DevExpress.XtraEditors.StyleController(this.components);
            this.panelControl5 = new DevExpress.XtraEditors.PanelControl();
            this.kaListLabelControl = new DevExpress.XtraEditors.LabelControl();
            this.panelControl4 = new DevExpress.XtraEditors.PanelControl();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.wbStatusList = new DevExpress.XtraEditors.LookUpEdit();
            this.panelControl3 = new DevExpress.XtraEditors.PanelControl();
            this.PeriodComboBoxEdit = new DevExpress.XtraEditors.ComboBoxEdit();
            this.wbStartDate = new DevExpress.XtraEditors.DateEdit();
            this.wbEndDate = new DevExpress.XtraEditors.DateEdit();
            this.panelLabelControl = new DevExpress.XtraEditors.PanelControl();
            this.mainLabelControl = new DevExpress.XtraEditors.LabelControl();
            this.imageCollection2 = new DevExpress.Utils.ImageCollection(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.KagentPanel)).BeginInit();
            this.KagentPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl6)).BeginInit();
            this.panelControl6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.wbKagentList.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.styleController1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl5)).BeginInit();
            this.panelControl5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl4)).BeginInit();
            this.panelControl4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.wbStatusList.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).BeginInit();
            this.panelControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PeriodComboBoxEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.wbStartDate.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.wbStartDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.wbEndDate.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.wbEndDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelLabelControl)).BeginInit();
            this.panelLabelControl.SuspendLayout();
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
            this.panelControl2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl2.Controls.Add(this.KagentPanel);
            this.panelControl2.Controls.Add(this.panelControl4);
            this.panelControl2.Controls.Add(this.panelControl3);
            this.panelControl2.Controls.Add(this.panelLabelControl);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl2.Location = new System.Drawing.Point(0, 0);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(1468, 50);
            this.panelControl2.TabIndex = 2;
            // 
            // KagentPanel
            // 
            this.KagentPanel.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.KagentPanel.Controls.Add(this.panelControl6);
            this.KagentPanel.Controls.Add(this.panelControl5);
            this.KagentPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.KagentPanel.Location = new System.Drawing.Point(569, 0);
            this.KagentPanel.Name = "KagentPanel";
            this.KagentPanel.Size = new System.Drawing.Size(638, 50);
            this.KagentPanel.TabIndex = 22;
            // 
            // panelControl6
            // 
            this.panelControl6.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl6.Controls.Add(this.wbKagentList);
            this.panelControl6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl6.Location = new System.Drawing.Point(98, 0);
            this.panelControl6.Name = "panelControl6";
            this.panelControl6.Padding = new System.Windows.Forms.Padding(5, 14, 5, 0);
            this.panelControl6.Size = new System.Drawing.Size(540, 50);
            this.panelControl6.TabIndex = 8;
            // 
            // wbKagentList
            // 
            this.wbKagentList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.wbKagentList.Location = new System.Drawing.Point(5, 14);
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
            this.wbKagentList.Size = new System.Drawing.Size(530, 24);
            this.wbKagentList.StyleController = this.styleController1;
            this.wbKagentList.TabIndex = 6;
            this.wbKagentList.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.wbKagentList_ButtonClick);
            this.wbKagentList.EditValueChanged += new System.EventHandler(this.wbKagentList_EditValueChanged);
            // 
            // styleController1
            // 
            this.styleController1.Appearance.Font = new System.Drawing.Font("Tahoma", 10F);
            this.styleController1.Appearance.Options.UseFont = true;
            // 
            // panelControl5
            // 
            this.panelControl5.AutoSize = true;
            this.panelControl5.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl5.Controls.Add(this.kaListLabelControl);
            this.panelControl5.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelControl5.Location = new System.Drawing.Point(0, 0);
            this.panelControl5.Name = "panelControl5";
            this.panelControl5.Size = new System.Drawing.Size(98, 50);
            this.panelControl5.TabIndex = 7;
            // 
            // kaListLabelControl
            // 
            this.kaListLabelControl.Location = new System.Drawing.Point(12, 17);
            this.kaListLabelControl.Name = "kaListLabelControl";
            this.kaListLabelControl.Size = new System.Drawing.Size(83, 16);
            this.kaListLabelControl.StyleController = this.styleController1;
            this.kaListLabelControl.TabIndex = 4;
            this.kaListLabelControl.Text = "Постачальник";
            // 
            // panelControl4
            // 
            this.panelControl4.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl4.Controls.Add(this.labelControl4);
            this.panelControl4.Controls.Add(this.wbStatusList);
            this.panelControl4.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelControl4.Location = new System.Drawing.Point(1207, 0);
            this.panelControl4.Name = "panelControl4";
            this.panelControl4.Size = new System.Drawing.Size(261, 50);
            this.panelControl4.TabIndex = 25;
            // 
            // labelControl4
            // 
            this.labelControl4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelControl4.Location = new System.Drawing.Point(9, 17);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(39, 16);
            this.labelControl4.StyleController = this.styleController1;
            this.labelControl4.TabIndex = 7;
            this.labelControl4.Text = "Статус";
            // 
            // wbStatusList
            // 
            this.wbStatusList.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.wbStatusList.Location = new System.Drawing.Point(54, 14);
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
            // panelControl3
            // 
            this.panelControl3.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl3.Controls.Add(this.PeriodComboBoxEdit);
            this.panelControl3.Controls.Add(this.wbStartDate);
            this.panelControl3.Controls.Add(this.wbEndDate);
            this.panelControl3.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelControl3.Location = new System.Drawing.Point(73, 0);
            this.panelControl3.Name = "panelControl3";
            this.panelControl3.Size = new System.Drawing.Size(496, 50);
            this.panelControl3.TabIndex = 24;
            // 
            // PeriodComboBoxEdit
            // 
            this.PeriodComboBoxEdit.EditValue = "Довільний період";
            this.PeriodComboBoxEdit.Location = new System.Drawing.Point(12, 14);
            this.PeriodComboBoxEdit.Name = "PeriodComboBoxEdit";
            this.PeriodComboBoxEdit.Properties.AllowMouseWheel = false;
            this.PeriodComboBoxEdit.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.PeriodComboBoxEdit.Properties.Appearance.Options.UseFont = true;
            editorButtonImageOptions2.Image = global::SP_Sklad.Properties.Resources.refresh_16x161;
            this.PeriodComboBoxEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, true, false, editorButtonImageOptions2, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject5, serializableAppearanceObject6, serializableAppearanceObject7, serializableAppearanceObject8, "", null, null, DevExpress.Utils.ToolTipAnchor.Default)});
            this.PeriodComboBoxEdit.Properties.Items.AddRange(new object[] {
            "Довільний період",
            "За поточний день",
            "З початку неділі",
            "З початку місяця",
            "З початку року",
            "З самого початку"});
            this.PeriodComboBoxEdit.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.PeriodComboBoxEdit.Size = new System.Drawing.Size(180, 24);
            this.PeriodComboBoxEdit.StyleController = this.styleController1;
            this.PeriodComboBoxEdit.TabIndex = 21;
            this.PeriodComboBoxEdit.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.PeriodComboBoxEdit_ButtonClick);
            this.PeriodComboBoxEdit.EditValueChanged += new System.EventHandler(this.PeriodComboBoxEdit_EditValueChanged);
            // 
            // wbStartDate
            // 
            this.wbStartDate.EditValue = null;
            this.wbStartDate.Location = new System.Drawing.Point(198, 14);
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
            // wbEndDate
            // 
            this.wbEndDate.EditValue = null;
            this.wbEndDate.Location = new System.Drawing.Point(344, 14);
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
            // panelLabelControl
            // 
            this.panelLabelControl.AutoSize = true;
            this.panelLabelControl.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelLabelControl.Controls.Add(this.mainLabelControl);
            this.panelLabelControl.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelLabelControl.Location = new System.Drawing.Point(0, 0);
            this.panelLabelControl.Name = "panelLabelControl";
            this.panelLabelControl.Size = new System.Drawing.Size(73, 50);
            this.panelLabelControl.TabIndex = 23;
            this.panelLabelControl.Visible = false;
            // 
            // mainLabelControl
            // 
            this.mainLabelControl.Appearance.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.mainLabelControl.Appearance.Options.UseFont = true;
            this.mainLabelControl.ImageAlignToText = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.mainLabelControl.ImageOptions.Alignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.mainLabelControl.ImageOptions.ImageIndex = 0;
            this.mainLabelControl.ImageOptions.Images = this.imageCollection2;
            this.mainLabelControl.Location = new System.Drawing.Point(4, 7);
            this.mainLabelControl.Name = "mainLabelControl";
            this.mainLabelControl.Size = new System.Drawing.Size(66, 36);
            this.mainLabelControl.TabIndex = 27;
            this.mainLabelControl.Text = "Title";
            // 
            // imageCollection2
            // 
            this.imageCollection2.ImageSize = new System.Drawing.Size(32, 32);
            this.imageCollection2.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("imageCollection2.ImageStream")));
            this.imageCollection2.TransparentColor = System.Drawing.Color.Transparent;
            this.imageCollection2.Images.SetKeyName(0, "salesperiodlifetime_32x32.png");
            // 
            // ucDocumentFilterPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelControl2);
            this.Name = "ucDocumentFilterPanel";
            this.Size = new System.Drawing.Size(1468, 50);
            this.Load += new System.EventHandler(this.ucWBFilterPanel_Load);
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            this.panelControl2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.KagentPanel)).EndInit();
            this.KagentPanel.ResumeLayout(false);
            this.KagentPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl6)).EndInit();
            this.panelControl6.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.wbKagentList.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.styleController1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl5)).EndInit();
            this.panelControl5.ResumeLayout(false);
            this.panelControl5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl4)).EndInit();
            this.panelControl4.ResumeLayout(false);
            this.panelControl4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.wbStatusList.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).EndInit();
            this.panelControl3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PeriodComboBoxEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.wbStartDate.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.wbStartDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.wbEndDate.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.wbEndDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelLabelControl)).EndInit();
            this.panelLabelControl.ResumeLayout(false);
            this.panelLabelControl.PerformLayout();
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
        private DevExpress.XtraEditors.PanelControl KagentPanel;
        private DevExpress.XtraEditors.PanelControl panelControl3;
        private DevExpress.XtraEditors.PanelControl panelLabelControl;
        private DevExpress.XtraEditors.LabelControl mainLabelControl;
        private DevExpress.XtraEditors.PanelControl panelControl4;
        private DevExpress.XtraEditors.PanelControl panelControl6;
        private DevExpress.XtraEditors.PanelControl panelControl5;
        private DevExpress.Utils.ImageCollection imageCollection2;
    }
}
