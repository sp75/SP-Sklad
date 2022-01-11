namespace SP_Sklad.EditForm
{
    partial class frmTaraEdit
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmTaraEdit));
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject1 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject2 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject3 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject4 = new DevExpress.Utils.SerializableAppearanceObject();
            this.ImageList = new System.Windows.Forms.ImageList(this.components);
            this.styleController1 = new DevExpress.XtraEditors.StyleController(this.components);
            this.TaraBS = new System.Windows.Forms.BindingSource(this.components);
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.OkButton = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.DirTreeList = new DevExpress.XtraTreeList.TreeList();
            this.treeListColumn1 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.splitterControl1 = new DevExpress.XtraEditors.SplitterControl();
            this.xtraTabControl1 = new DevExpress.XtraTab.XtraTabControl();
            this.xtraTabPage1 = new DevExpress.XtraTab.XtraTabPage();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.WeightEdit = new DevExpress.XtraEditors.CalcEdit();
            this.labelControl18 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl12 = new DevExpress.XtraEditors.LabelControl();
            this.MatTypeEdit = new DevExpress.XtraEditors.LookUpEdit();
            this.WIdLookUpEdit = new DevExpress.XtraEditors.LookUpEdit();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.textEdit1 = new DevExpress.XtraEditors.TextEdit();
            this.ArtikulEdit = new DevExpress.XtraEditors.TextEdit();
            this.NameTextEdit = new DevExpress.XtraEditors.TextEdit();
            this.labelControl22 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.UpTextBtn = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.simpleButton2 = new DevExpress.XtraEditors.SimpleButton();
            this.GrpIdEdit = new DevExpress.XtraEditors.TreeListLookUpEdit();
            this.treeListLookUpEdit1TreeList = new DevExpress.XtraTreeList.TreeList();
            this.treeListColumn2 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.xtraTabPage7 = new DevExpress.XtraTab.XtraTabPage();
            this.textEdit8 = new DevExpress.XtraEditors.MemoEdit();
            ((System.ComponentModel.ISupportInitialize)(this.styleController1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TaraBS)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DirTreeList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).BeginInit();
            this.xtraTabControl1.SuspendLayout();
            this.xtraTabPage1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.WeightEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MatTypeEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.WIdLookUpEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ArtikulEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NameTextEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GrpIdEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.treeListLookUpEdit1TreeList)).BeginInit();
            this.xtraTabPage7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit8.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // ImageList
            // 
            this.ImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ImageList.ImageStream")));
            this.ImageList.TransparentColor = System.Drawing.Color.White;
            this.ImageList.Images.SetKeyName(0, "CloseFolder.bmp");
            this.ImageList.Images.SetKeyName(1, "Up.bmp");
            this.ImageList.Images.SetKeyName(2, "down.bmp");
            // 
            // styleController1
            // 
            this.styleController1.Appearance.Font = new System.Drawing.Font("Tahoma", 10F);
            this.styleController1.Appearance.Options.UseFont = true;
            // 
            // TaraBS
            // 
            this.TaraBS.DataSource = typeof(SP_Sklad.SkladData.Tara);
            // 
            // panelControl2
            // 
            this.panelControl2.Controls.Add(this.OkButton);
            this.panelControl2.Controls.Add(this.simpleButton1);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl2.Location = new System.Drawing.Point(0, 253);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(808, 54);
            this.panelControl2.TabIndex = 34;
            // 
            // OkButton
            // 
            this.OkButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.OkButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.OkButton.Location = new System.Drawing.Point(590, 13);
            this.OkButton.Name = "OkButton";
            this.OkButton.Size = new System.Drawing.Size(98, 30);
            this.OkButton.TabIndex = 3;
            this.OkButton.Text = "Застосувати";
            this.OkButton.Click += new System.EventHandler(this.OkButton_Click);
            // 
            // simpleButton1
            // 
            this.simpleButton1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.simpleButton1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.simpleButton1.Location = new System.Drawing.Point(697, 13);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(93, 30);
            this.simpleButton1.TabIndex = 2;
            this.simpleButton1.Text = "Відмінити";
            // 
            // DirTreeList
            // 
            this.DirTreeList.Appearance.FocusedCell.BackColor = System.Drawing.Color.LightSkyBlue;
            this.DirTreeList.Appearance.FocusedCell.Options.UseBackColor = true;
            this.DirTreeList.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.treeListColumn1});
            this.DirTreeList.Dock = System.Windows.Forms.DockStyle.Left;
            this.DirTreeList.ImageIndexFieldName = "ImgIdx";
            this.DirTreeList.KeyFieldName = "Id";
            this.DirTreeList.Location = new System.Drawing.Point(0, 0);
            this.DirTreeList.Name = "DirTreeList";
            this.DirTreeList.OptionsBehavior.Editable = false;
            this.DirTreeList.OptionsView.ShowColumns = false;
            this.DirTreeList.OptionsView.ShowHorzLines = false;
            this.DirTreeList.OptionsView.ShowIndicator = false;
            this.DirTreeList.OptionsView.ShowVertLines = false;
            this.DirTreeList.ParentFieldName = "ParentId";
            this.DirTreeList.SelectImageList = this.ImageList;
            this.DirTreeList.Size = new System.Drawing.Size(187, 253);
            this.DirTreeList.TabIndex = 35;
            this.DirTreeList.FocusedNodeChanged += new DevExpress.XtraTreeList.FocusedNodeChangedEventHandler(this.DirTreeList_FocusedNodeChanged);
            // 
            // treeListColumn1
            // 
            this.treeListColumn1.Caption = "Назва";
            this.treeListColumn1.FieldName = "Text";
            this.treeListColumn1.MinWidth = 34;
            this.treeListColumn1.Name = "treeListColumn1";
            this.treeListColumn1.Visible = true;
            this.treeListColumn1.VisibleIndex = 0;
            // 
            // splitterControl1
            // 
            this.splitterControl1.Location = new System.Drawing.Point(187, 0);
            this.splitterControl1.Name = "splitterControl1";
            this.splitterControl1.Size = new System.Drawing.Size(5, 253);
            this.splitterControl1.TabIndex = 36;
            this.splitterControl1.TabStop = false;
            // 
            // xtraTabControl1
            // 
            this.xtraTabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xtraTabControl1.Location = new System.Drawing.Point(192, 0);
            this.xtraTabControl1.Name = "xtraTabControl1";
            this.xtraTabControl1.SelectedTabPage = this.xtraTabPage1;
            this.xtraTabControl1.ShowTabHeader = DevExpress.Utils.DefaultBoolean.True;
            this.xtraTabControl1.Size = new System.Drawing.Size(616, 253);
            this.xtraTabControl1.TabIndex = 37;
            this.xtraTabControl1.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.xtraTabPage1,
            this.xtraTabPage7});
            // 
            // xtraTabPage1
            // 
            this.xtraTabPage1.Controls.Add(this.panel1);
            this.xtraTabPage1.Name = "xtraTabPage1";
            this.xtraTabPage1.Size = new System.Drawing.Size(610, 225);
            this.xtraTabPage1.Text = "Основна інформація";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panelControl1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(5);
            this.panel1.Size = new System.Drawing.Size(610, 225);
            this.panel1.TabIndex = 29;
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.WeightEdit);
            this.panelControl1.Controls.Add(this.labelControl18);
            this.panelControl1.Controls.Add(this.labelControl12);
            this.panelControl1.Controls.Add(this.MatTypeEdit);
            this.panelControl1.Controls.Add(this.WIdLookUpEdit);
            this.panelControl1.Controls.Add(this.labelControl4);
            this.panelControl1.Controls.Add(this.textEdit1);
            this.panelControl1.Controls.Add(this.ArtikulEdit);
            this.panelControl1.Controls.Add(this.NameTextEdit);
            this.panelControl1.Controls.Add(this.labelControl22);
            this.panelControl1.Controls.Add(this.labelControl2);
            this.panelControl1.Controls.Add(this.labelControl1);
            this.panelControl1.Controls.Add(this.UpTextBtn);
            this.panelControl1.Controls.Add(this.labelControl6);
            this.panelControl1.Controls.Add(this.simpleButton2);
            this.panelControl1.Controls.Add(this.GrpIdEdit);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl1.Location = new System.Drawing.Point(5, 5);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(600, 215);
            this.panelControl1.TabIndex = 1;
            // 
            // WeightEdit
            // 
            this.WeightEdit.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.TaraBS, "Weight", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.WeightEdit.Location = new System.Drawing.Point(375, 168);
            this.WeightEdit.Name = "WeightEdit";
            this.WeightEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, true, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, ((System.Drawing.Image)(resources.GetObject("WeightEdit.Properties.Buttons"))), new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.F12), serializableAppearanceObject1, serializableAppearanceObject2, serializableAppearanceObject3, serializableAppearanceObject4, "", null, null, true)});
            this.WeightEdit.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.WeightEdit.Properties.ShowCloseButton = true;
            this.WeightEdit.Size = new System.Drawing.Size(184, 22);
            this.WeightEdit.StyleController = this.styleController1;
            this.WeightEdit.TabIndex = 55;
            this.WeightEdit.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.WeightEdit_ButtonClick);
            // 
            // labelControl18
            // 
            this.labelControl18.Location = new System.Drawing.Point(343, 171);
            this.labelControl18.Name = "labelControl18";
            this.labelControl18.Size = new System.Drawing.Size(26, 16);
            this.labelControl18.StyleController = this.styleController1;
            this.labelControl18.TabIndex = 54;
            this.labelControl18.Text = "Вага";
            // 
            // labelControl12
            // 
            this.labelControl12.Location = new System.Drawing.Point(347, 132);
            this.labelControl12.Name = "labelControl12";
            this.labelControl12.Size = new System.Drawing.Size(22, 16);
            this.labelControl12.StyleController = this.styleController1;
            this.labelControl12.TabIndex = 53;
            this.labelControl12.Text = "Тип";
            // 
            // MatTypeEdit
            // 
            this.MatTypeEdit.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.TaraBS, "TypeId", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.MatTypeEdit.Location = new System.Drawing.Point(375, 129);
            this.MatTypeEdit.Name = "MatTypeEdit";
            this.MatTypeEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Clear)});
            this.MatTypeEdit.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Name", "Назва")});
            this.MatTypeEdit.Properties.DisplayMember = "Name";
            this.MatTypeEdit.Properties.ShowFooter = false;
            this.MatTypeEdit.Properties.ShowHeader = false;
            this.MatTypeEdit.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.MatTypeEdit.Properties.ValueMember = "Id";
            this.MatTypeEdit.Size = new System.Drawing.Size(184, 22);
            this.MatTypeEdit.StyleController = this.styleController1;
            this.MatTypeEdit.TabIndex = 52;
            // 
            // WIdLookUpEdit
            // 
            this.WIdLookUpEdit.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.TaraBS, "WId", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.WIdLookUpEdit.Location = new System.Drawing.Point(115, 168);
            this.WIdLookUpEdit.Name = "WIdLookUpEdit";
            this.WIdLookUpEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.WIdLookUpEdit.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Name", "Назва")});
            this.WIdLookUpEdit.Properties.DisplayMember = "Name";
            this.WIdLookUpEdit.Properties.ShowFooter = false;
            this.WIdLookUpEdit.Properties.ShowHeader = false;
            this.WIdLookUpEdit.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.WIdLookUpEdit.Properties.ValueMember = "WId";
            this.WIdLookUpEdit.Size = new System.Drawing.Size(198, 22);
            this.WIdLookUpEdit.StyleController = this.styleController1;
            this.WIdLookUpEdit.TabIndex = 41;
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(12, 171);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(40, 16);
            this.labelControl4.StyleController = this.styleController1;
            this.labelControl4.TabIndex = 40;
            this.labelControl4.Text = "Склад:";
            // 
            // textEdit1
            // 
            this.textEdit1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textEdit1.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.TaraBS, "InvNumber", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.textEdit1.Location = new System.Drawing.Point(115, 129);
            this.textEdit1.Name = "textEdit1";
            this.textEdit1.Size = new System.Drawing.Size(198, 22);
            this.textEdit1.StyleController = this.styleController1;
            this.textEdit1.TabIndex = 34;
            // 
            // ArtikulEdit
            // 
            this.ArtikulEdit.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ArtikulEdit.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.TaraBS, "Artikul", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.ArtikulEdit.Location = new System.Drawing.Point(115, 46);
            this.ArtikulEdit.Name = "ArtikulEdit";
            this.ArtikulEdit.Size = new System.Drawing.Size(444, 22);
            this.ArtikulEdit.StyleController = this.styleController1;
            this.ArtikulEdit.TabIndex = 29;
            // 
            // NameTextEdit
            // 
            this.NameTextEdit.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.NameTextEdit.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.TaraBS, "Name", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.NameTextEdit.Location = new System.Drawing.Point(115, 12);
            this.NameTextEdit.Name = "NameTextEdit";
            this.NameTextEdit.Size = new System.Drawing.Size(444, 22);
            this.NameTextEdit.StyleController = this.styleController1;
            this.NameTextEdit.TabIndex = 27;
            this.NameTextEdit.EditValueChanged += new System.EventHandler(this.NameTextEdit_EditValueChanged);
            // 
            // labelControl22
            // 
            this.labelControl22.Location = new System.Drawing.Point(12, 132);
            this.labelControl22.Name = "labelControl22";
            this.labelControl22.Size = new System.Drawing.Size(97, 16);
            this.labelControl22.StyleController = this.styleController1;
            this.labelControl22.TabIndex = 25;
            this.labelControl22.Text = "Інвентарний №:";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(12, 85);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(91, 16);
            this.labelControl2.StyleController = this.styleController1;
            this.labelControl2.TabIndex = 22;
            this.labelControl2.Text = "Належить групі";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(12, 49);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(52, 16);
            this.labelControl1.StyleController = this.styleController1;
            this.labelControl1.TabIndex = 21;
            this.labelControl1.Text = "Артикул:";
            // 
            // UpTextBtn
            // 
            this.UpTextBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.UpTextBtn.ImageIndex = 1;
            this.UpTextBtn.ImageList = this.ImageList;
            this.UpTextBtn.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.UpTextBtn.Location = new System.Drawing.Point(565, 46);
            this.UpTextBtn.Name = "UpTextBtn";
            this.UpTextBtn.Size = new System.Drawing.Size(22, 22);
            this.UpTextBtn.TabIndex = 18;
            this.UpTextBtn.Click += new System.EventHandler(this.UpTextBtn_Click);
            // 
            // labelControl6
            // 
            this.labelControl6.Location = new System.Drawing.Point(12, 15);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(90, 16);
            this.labelControl6.StyleController = this.styleController1;
            this.labelControl6.TabIndex = 16;
            this.labelControl6.Text = "Найменування:";
            // 
            // simpleButton2
            // 
            this.simpleButton2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.simpleButton2.ImageIndex = 2;
            this.simpleButton2.ImageList = this.ImageList;
            this.simpleButton2.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.simpleButton2.Location = new System.Drawing.Point(565, 11);
            this.simpleButton2.Name = "simpleButton2";
            this.simpleButton2.Size = new System.Drawing.Size(22, 22);
            this.simpleButton2.TabIndex = 13;
            this.simpleButton2.Click += new System.EventHandler(this.simpleButton2_Click);
            // 
            // GrpIdEdit
            // 
            this.GrpIdEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.GrpIdEdit.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.TaraBS, "GrpId", true));
            this.GrpIdEdit.Location = new System.Drawing.Point(115, 82);
            this.GrpIdEdit.Name = "GrpIdEdit";
            this.GrpIdEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.GrpIdEdit.Properties.DataSource = this.TaraBS;
            this.GrpIdEdit.Properties.DisplayMember = "Name";
            this.GrpIdEdit.Properties.NullText = "";
            this.GrpIdEdit.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.GrpIdEdit.Properties.TreeList = this.treeListLookUpEdit1TreeList;
            this.GrpIdEdit.Properties.ValueMember = "GrpId";
            this.GrpIdEdit.Size = new System.Drawing.Size(444, 22);
            this.GrpIdEdit.StyleController = this.styleController1;
            this.GrpIdEdit.TabIndex = 32;
            // 
            // treeListLookUpEdit1TreeList
            // 
            this.treeListLookUpEdit1TreeList.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.treeListColumn2});
            this.treeListLookUpEdit1TreeList.DataSource = this.TaraBS;
            this.treeListLookUpEdit1TreeList.KeyFieldName = "GrpId";
            this.treeListLookUpEdit1TreeList.Location = new System.Drawing.Point(0, 0);
            this.treeListLookUpEdit1TreeList.Name = "treeListLookUpEdit1TreeList";
            this.treeListLookUpEdit1TreeList.OptionsBehavior.EnableFiltering = true;
            this.treeListLookUpEdit1TreeList.OptionsView.ShowColumns = false;
            this.treeListLookUpEdit1TreeList.OptionsView.ShowHorzLines = false;
            this.treeListLookUpEdit1TreeList.OptionsView.ShowIndicator = false;
            this.treeListLookUpEdit1TreeList.OptionsView.ShowVertLines = false;
            this.treeListLookUpEdit1TreeList.ParentFieldName = "PId";
            this.treeListLookUpEdit1TreeList.SelectImageList = this.ImageList;
            this.treeListLookUpEdit1TreeList.Size = new System.Drawing.Size(400, 200);
            this.treeListLookUpEdit1TreeList.TabIndex = 0;
            // 
            // treeListColumn2
            // 
            this.treeListColumn2.Caption = "treeListColumn2";
            this.treeListColumn2.FieldName = "Name";
            this.treeListColumn2.MinWidth = 34;
            this.treeListColumn2.Name = "treeListColumn2";
            this.treeListColumn2.Visible = true;
            this.treeListColumn2.VisibleIndex = 0;
            this.treeListColumn2.Width = 93;
            // 
            // xtraTabPage7
            // 
            this.xtraTabPage7.Controls.Add(this.textEdit8);
            this.xtraTabPage7.Name = "xtraTabPage7";
            this.xtraTabPage7.Padding = new System.Windows.Forms.Padding(5);
            this.xtraTabPage7.Size = new System.Drawing.Size(610, 225);
            this.xtraTabPage7.Text = "Примітка";
            // 
            // textEdit8
            // 
            this.textEdit8.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.TaraBS, "Notes", true));
            this.textEdit8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textEdit8.Location = new System.Drawing.Point(5, 5);
            this.textEdit8.Name = "textEdit8";
            this.textEdit8.Size = new System.Drawing.Size(600, 215);
            this.textEdit8.TabIndex = 35;
            // 
            // frmTaraEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(808, 307);
            this.Controls.Add(this.xtraTabControl1);
            this.Controls.Add(this.splitterControl1);
            this.Controls.Add(this.DirTreeList);
            this.Controls.Add(this.panelControl2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmTaraEdit";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmTaraEdit";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmServicesEdit_FormClosed);
            this.Load += new System.EventHandler(this.frmServicesEdit_Load);
            ((System.ComponentModel.ISupportInitialize)(this.styleController1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TaraBS)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DirTreeList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).EndInit();
            this.xtraTabControl1.ResumeLayout(false);
            this.xtraTabPage1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.WeightEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MatTypeEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.WIdLookUpEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ArtikulEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NameTextEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GrpIdEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.treeListLookUpEdit1TreeList)).EndInit();
            this.xtraTabPage7.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.textEdit8.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.ImageList ImageList;
        private DevExpress.XtraEditors.StyleController styleController1;
        private System.Windows.Forms.BindingSource TaraBS;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        public DevExpress.XtraEditors.SimpleButton OkButton;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private DevExpress.XtraTreeList.TreeList DirTreeList;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn1;
        private DevExpress.XtraEditors.SplitterControl splitterControl1;
        private DevExpress.XtraTab.XtraTabControl xtraTabControl1;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage1;
        private System.Windows.Forms.Panel panel1;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.TextEdit ArtikulEdit;
        private DevExpress.XtraEditors.TextEdit NameTextEdit;
        private DevExpress.XtraEditors.LabelControl labelControl22;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.SimpleButton UpTextBtn;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private DevExpress.XtraEditors.SimpleButton simpleButton2;
        private DevExpress.XtraEditors.TreeListLookUpEdit GrpIdEdit;
        private DevExpress.XtraTreeList.TreeList treeListLookUpEdit1TreeList;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn2;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage7;
        private DevExpress.XtraEditors.MemoEdit textEdit8;
        private DevExpress.XtraEditors.TextEdit textEdit1;
        private DevExpress.XtraEditors.LookUpEdit WIdLookUpEdit;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.LabelControl labelControl12;
        private DevExpress.XtraEditors.LookUpEdit MatTypeEdit;
        private DevExpress.XtraEditors.CalcEdit WeightEdit;
        private DevExpress.XtraEditors.LabelControl labelControl18;
    }
}