namespace SP_Sklad
{
    partial class frmOperLogDet
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmOperLogDet));
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject1 = new DevExpress.Utils.SerializableAppearanceObject();
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar2 = new DevExpress.XtraBars.Bar();
            this.PrevievBtn = new DevExpress.XtraBars.BarButtonItem();
            this.HistoryBtn = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.standaloneBarDockControl1 = new DevExpress.XtraBars.StandaloneBarDockControl();
            this.FormImgList = new System.Windows.Forms.ImageList(this.components);
            this.styleController1 = new DevExpress.XtraEditors.StyleController(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.dateEdit2 = new DevExpress.XtraEditors.DateEdit();
            this.OperLogDetBS = new System.Windows.Forms.BindingSource(this.components);
            this.wbStartDate = new DevExpress.XtraEditors.DateEdit();
            this.memoEdit1 = new DevExpress.XtraEditors.MemoEdit();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.textEdit1 = new DevExpress.XtraEditors.TextEdit();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl7 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.NumEdit = new DevExpress.XtraEditors.TextEdit();
            this.textEdit2 = new DevExpress.XtraEditors.MemoEdit();
            this.PTypeComboBox = new DevExpress.XtraEditors.ImageComboBoxEdit();
            this.panelControl4 = new DevExpress.XtraEditors.PanelControl();
            this.OkButton = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.styleController1)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dateEdit2.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEdit2.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.OperLogDetBS)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.wbStartDate.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.wbStartDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.memoEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NumEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit2.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PTypeComboBox.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl4)).BeginInit();
            this.panelControl4.SuspendLayout();
            this.SuspendLayout();
            // 
            // barManager1
            // 
            this.barManager1.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.bar2});
            this.barManager1.DockControls.Add(this.barDockControlTop);
            this.barManager1.DockControls.Add(this.barDockControlBottom);
            this.barManager1.DockControls.Add(this.barDockControlLeft);
            this.barManager1.DockControls.Add(this.barDockControlRight);
            this.barManager1.DockControls.Add(this.standaloneBarDockControl1);
            this.barManager1.Form = this;
            this.barManager1.Images = this.FormImgList;
            this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.PrevievBtn,
            this.HistoryBtn});
            this.barManager1.MainMenu = this.bar2;
            this.barManager1.MaxItemId = 13;
            // 
            // bar2
            // 
            this.bar2.BarName = "Main menu";
            this.bar2.CanDockStyle = DevExpress.XtraBars.BarCanDockStyle.Top;
            this.bar2.DockCol = 0;
            this.bar2.DockRow = 0;
            this.bar2.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar2.FloatLocation = new System.Drawing.Point(275, 401);
            this.bar2.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.PrevievBtn),
            new DevExpress.XtraBars.LinkPersistInfo(this.HistoryBtn)});
            this.bar2.OptionsBar.MultiLine = true;
            this.bar2.OptionsBar.UseWholeRow = true;
            this.bar2.Text = "Main menu";
            // 
            // PrevievBtn
            // 
            this.PrevievBtn.Caption = "Переглянути друковану форму документа";
            this.PrevievBtn.Id = 0;
            this.PrevievBtn.ImageIndex = 0;
            this.PrevievBtn.Name = "PrevievBtn";
            this.PrevievBtn.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.PrevievBtn_ItemClick);
            // 
            // HistoryBtn
            // 
            this.HistoryBtn.Caption = "Історія зміни запису";
            this.HistoryBtn.Glyph = ((System.Drawing.Image)(resources.GetObject("HistoryBtn.Glyph")));
            this.HistoryBtn.Id = 1;
            this.HistoryBtn.LargeGlyph = ((System.Drawing.Image)(resources.GetObject("HistoryBtn.LargeGlyph")));
            this.HistoryBtn.Name = "HistoryBtn";
            this.HistoryBtn.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.HistoryBtn_ItemClick);
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Size = new System.Drawing.Size(492, 24);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 491);
            this.barDockControlBottom.Size = new System.Drawing.Size(492, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 24);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 467);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(492, 24);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 467);
            // 
            // standaloneBarDockControl1
            // 
            this.standaloneBarDockControl1.AutoSize = true;
            this.standaloneBarDockControl1.CausesValidation = false;
            this.standaloneBarDockControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.standaloneBarDockControl1.Location = new System.Drawing.Point(0, 24);
            this.standaloneBarDockControl1.Name = "standaloneBarDockControl1";
            this.standaloneBarDockControl1.Size = new System.Drawing.Size(492, 0);
            this.standaloneBarDockControl1.Text = "standaloneBarDockControl1";
            // 
            // FormImgList
            // 
            this.FormImgList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("FormImgList.ImageStream")));
            this.FormImgList.TransparentColor = System.Drawing.Color.White;
            this.FormImgList.Images.SetKeyName(0, "Попередн_й перегляд.bmp");
            this.FormImgList.Images.SetKeyName(1, "Документ.bmp");
            this.FormImgList.Images.SetKeyName(2, "Add.bmp");
            this.FormImgList.Images.SetKeyName(3, "н_чого.bmp");
            this.FormImgList.Images.SetKeyName(4, "edit.bmp");
            this.FormImgList.Images.SetKeyName(5, "Delete.bmp");
            this.FormImgList.Images.SetKeyName(6, "storno.png");
            this.FormImgList.Images.SetKeyName(7, "execute.png");
            // 
            // styleController1
            // 
            this.styleController1.Appearance.Font = new System.Drawing.Font("Tahoma", 10F);
            this.styleController1.Appearance.Options.UseFont = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panelControl1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 24);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(5);
            this.panel1.Size = new System.Drawing.Size(492, 415);
            this.panel1.TabIndex = 7;
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.dateEdit2);
            this.panelControl1.Controls.Add(this.wbStartDate);
            this.panelControl1.Controls.Add(this.memoEdit1);
            this.panelControl1.Controls.Add(this.labelControl6);
            this.panelControl1.Controls.Add(this.textEdit1);
            this.panelControl1.Controls.Add(this.labelControl4);
            this.panelControl1.Controls.Add(this.labelControl7);
            this.panelControl1.Controls.Add(this.labelControl5);
            this.panelControl1.Controls.Add(this.labelControl3);
            this.panelControl1.Controls.Add(this.labelControl2);
            this.panelControl1.Controls.Add(this.labelControl1);
            this.panelControl1.Controls.Add(this.NumEdit);
            this.panelControl1.Controls.Add(this.textEdit2);
            this.panelControl1.Controls.Add(this.PTypeComboBox);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl1.Location = new System.Drawing.Point(5, 5);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(482, 405);
            this.panelControl1.TabIndex = 5;
            // 
            // dateEdit2
            // 
            this.dateEdit2.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.OperLogDetBS, "OnDate", true));
            this.dateEdit2.EditValue = null;
            this.dateEdit2.Location = new System.Drawing.Point(337, 59);
            this.dateEdit2.Name = "dateEdit2";
            this.dateEdit2.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightYellow;
            this.dateEdit2.Properties.AppearanceReadOnly.Options.UseBackColor = true;
            this.dateEdit2.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateEdit2.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateEdit2.Properties.DisplayFormat.FormatString = "t";
            this.dateEdit2.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dateEdit2.Properties.EditFormat.FormatString = "t";
            this.dateEdit2.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dateEdit2.Properties.Mask.EditMask = "t";
            this.dateEdit2.Properties.ReadOnly = true;
            this.dateEdit2.Size = new System.Drawing.Size(126, 20);
            this.dateEdit2.TabIndex = 34;
            // 
            // OperLogDetBS
            // 
            this.OperLogDetBS.DataSource = typeof(SP_Sklad.SkladData.GetOperLog_Result);
            // 
            // wbStartDate
            // 
            this.wbStartDate.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.OperLogDetBS, "OnDate", true));
            this.wbStartDate.EditValue = null;
            this.wbStartDate.Location = new System.Drawing.Point(115, 59);
            this.wbStartDate.Name = "wbStartDate";
            this.wbStartDate.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightYellow;
            this.wbStartDate.Properties.AppearanceReadOnly.Options.UseBackColor = true;
            this.wbStartDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.wbStartDate.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.wbStartDate.Properties.ReadOnly = true;
            this.wbStartDate.Size = new System.Drawing.Size(135, 20);
            this.wbStartDate.TabIndex = 33;
            // 
            // memoEdit1
            // 
            this.memoEdit1.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.OperLogDetBS, "DataAfter", true));
            this.memoEdit1.Location = new System.Drawing.Point(115, 284);
            this.memoEdit1.MenuManager = this.barManager1;
            this.memoEdit1.Name = "memoEdit1";
            this.memoEdit1.Properties.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.memoEdit1.Size = new System.Drawing.Size(348, 100);
            this.memoEdit1.TabIndex = 32;
            // 
            // labelControl6
            // 
            this.labelControl6.Location = new System.Drawing.Point(12, 98);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(37, 16);
            this.labelControl6.StyleController = this.styleController1;
            this.labelControl6.TabIndex = 30;
            this.labelControl6.Text = "Розділ";
            // 
            // textEdit1
            // 
            this.textEdit1.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.OperLogDetBS, "FunName", true));
            this.textEdit1.Location = new System.Drawing.Point(115, 95);
            this.textEdit1.MenuManager = this.barManager1;
            this.textEdit1.Name = "textEdit1";
            this.textEdit1.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightYellow;
            this.textEdit1.Properties.AppearanceReadOnly.Options.UseBackColor = true;
            this.textEdit1.Properties.ReadOnly = true;
            this.textEdit1.Size = new System.Drawing.Size(349, 22);
            this.textEdit1.StyleController = this.styleController1;
            this.textEdit1.TabIndex = 29;
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(274, 60);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(57, 16);
            this.labelControl4.StyleController = this.styleController1;
            this.labelControl4.TabIndex = 27;
            this.labelControl4.Text = "Час події:";
            // 
            // labelControl7
            // 
            this.labelControl7.Location = new System.Drawing.Point(12, 284);
            this.labelControl7.Name = "labelControl7";
            this.labelControl7.Size = new System.Drawing.Size(92, 16);
            this.labelControl7.StyleController = this.styleController1;
            this.labelControl7.TabIndex = 20;
            this.labelControl7.Text = "Дані після події";
            // 
            // labelControl5
            // 
            this.labelControl5.Location = new System.Drawing.Point(12, 168);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(76, 16);
            this.labelControl5.StyleController = this.styleController1;
            this.labelControl5.TabIndex = 7;
            this.labelControl5.Text = "Дані до події";
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(12, 134);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(54, 16);
            this.labelControl3.StyleController = this.styleController1;
            this.labelControl3.TabIndex = 5;
            this.labelControl3.Text = "Тип події";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(12, 60);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(65, 16);
            this.labelControl2.StyleController = this.styleController1;
            this.labelControl2.TabIndex = 4;
            this.labelControl2.Text = "Дата події:";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(12, 22);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(72, 16);
            this.labelControl1.StyleController = this.styleController1;
            this.labelControl1.TabIndex = 1;
            this.labelControl1.Text = "Користувач:";
            // 
            // NumEdit
            // 
            this.NumEdit.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.OperLogDetBS, "UserName", true));
            this.NumEdit.Location = new System.Drawing.Point(115, 19);
            this.NumEdit.MenuManager = this.barManager1;
            this.NumEdit.Name = "NumEdit";
            this.NumEdit.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightYellow;
            this.NumEdit.Properties.AppearanceReadOnly.Options.UseBackColor = true;
            this.NumEdit.Properties.ReadOnly = true;
            this.NumEdit.Size = new System.Drawing.Size(349, 22);
            this.NumEdit.StyleController = this.styleController1;
            this.NumEdit.TabIndex = 0;
            // 
            // textEdit2
            // 
            this.textEdit2.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.OperLogDetBS, "DataBefore", true));
            this.textEdit2.Location = new System.Drawing.Point(115, 168);
            this.textEdit2.MenuManager = this.barManager1;
            this.textEdit2.Name = "textEdit2";
            this.textEdit2.Properties.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textEdit2.Size = new System.Drawing.Size(348, 100);
            this.textEdit2.TabIndex = 31;
            // 
            // PTypeComboBox
            // 
            this.PTypeComboBox.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.OperLogDetBS, "OpCode", true));
            this.PTypeComboBox.Location = new System.Drawing.Point(115, 131);
            this.PTypeComboBox.Name = "PTypeComboBox";
            this.PTypeComboBox.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightYellow;
            this.PTypeComboBox.Properties.AppearanceReadOnly.Options.UseBackColor = true;
            this.PTypeComboBox.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, true, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, ((System.Drawing.Image)(resources.GetObject("PTypeComboBox.Properties.Buttons"))), new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject1, "", null, null, true)});
            this.PTypeComboBox.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.ImageComboBoxItem[] {
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("Створення", "I", 2),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("Редагування", "U", 4),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("Видалення", "D", 5),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("Виконання", "E", 7),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("Сторнування", "S", 6),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("Перегляд", "V", 3)});
            this.PTypeComboBox.Properties.NullText = "[EditValue is null]";
            this.PTypeComboBox.Properties.PopupSizeable = true;
            this.PTypeComboBox.Properties.ReadOnly = true;
            this.PTypeComboBox.Properties.SmallImages = this.FormImgList;
            this.PTypeComboBox.Size = new System.Drawing.Size(348, 22);
            this.PTypeComboBox.TabIndex = 9;
            // 
            // panelControl4
            // 
            this.panelControl4.Controls.Add(this.OkButton);
            this.panelControl4.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl4.Location = new System.Drawing.Point(0, 439);
            this.panelControl4.Name = "panelControl4";
            this.panelControl4.Size = new System.Drawing.Size(492, 52);
            this.panelControl4.TabIndex = 11;
            // 
            // OkButton
            // 
            this.OkButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.OkButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.OkButton.Location = new System.Drawing.Point(397, 10);
            this.OkButton.Name = "OkButton";
            this.OkButton.Size = new System.Drawing.Size(83, 30);
            this.OkButton.TabIndex = 1;
            this.OkButton.Text = "Вихід";
            // 
            // frmOperLogDet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(492, 491);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.standaloneBarDockControl1);
            this.Controls.Add(this.panelControl4);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "frmOperLogDet";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Властивості події";
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.styleController1)).EndInit();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dateEdit2.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEdit2.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.OperLogDetBS)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.wbStartDate.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.wbStartDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.memoEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NumEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit2.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PTypeComboBox.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl4)).EndInit();
            this.panelControl4.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar2;
        private DevExpress.XtraBars.BarButtonItem PrevievBtn;
        private DevExpress.XtraBars.BarButtonItem HistoryBtn;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.StandaloneBarDockControl standaloneBarDockControl1;
        public System.Windows.Forms.ImageList FormImgList;
        private DevExpress.XtraEditors.StyleController styleController1;
        private System.Windows.Forms.Panel panel1;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl7;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.TextEdit NumEdit;
        private DevExpress.XtraEditors.MemoEdit memoEdit1;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private DevExpress.XtraEditors.TextEdit textEdit1;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.MemoEdit textEdit2;
        private DevExpress.XtraEditors.ImageComboBoxEdit PTypeComboBox;
        private DevExpress.XtraEditors.PanelControl panelControl4;
        private DevExpress.XtraEditors.SimpleButton OkButton;
        public System.Windows.Forms.BindingSource OperLogDetBS;
        private DevExpress.XtraEditors.DateEdit wbStartDate;
        private DevExpress.XtraEditors.DateEdit dateEdit2;
    }
}