
namespace SP_Sklad.ViewsForm
{
    partial class frmOpenStoreCashRegisterSyncMonitor
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
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.ucOpenStoreCashRegisterSyncMonitor1 = new SP_Sklad.UserControls.Warehouse.ucOpenStoreCashRegisterSyncMonitor();
            ((System.ComponentModel.ISupportInitialize)(this.BottomPanel)).BeginInit();
            this.BottomPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // BottomPanel
            // 
            this.BottomPanel.Controls.Add(this.OkButton);
            this.BottomPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.BottomPanel.Location = new System.Drawing.Point(0, 538);
            this.BottomPanel.Name = "BottomPanel";
            this.BottomPanel.Size = new System.Drawing.Size(1372, 52);
            this.BottomPanel.TabIndex = 17;
            // 
            // OkButton
            // 
            this.OkButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.OkButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.OkButton.Location = new System.Drawing.Point(1260, 10);
            this.OkButton.Name = "OkButton";
            this.OkButton.Size = new System.Drawing.Size(100, 30);
            this.OkButton.TabIndex = 1;
            this.OkButton.Text = "Закрити";
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 60000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // ucOpenStoreCashRegisterSyncMonitor1
            // 
            this.ucOpenStoreCashRegisterSyncMonitor1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucOpenStoreCashRegisterSyncMonitor1.Location = new System.Drawing.Point(0, 0);
            this.ucOpenStoreCashRegisterSyncMonitor1.Name = "ucOpenStoreCashRegisterSyncMonitor1";
            this.ucOpenStoreCashRegisterSyncMonitor1.Size = new System.Drawing.Size(1372, 538);
            this.ucOpenStoreCashRegisterSyncMonitor1.TabIndex = 18;
            // 
            // frmOpenStoreCashRegisterSyncMonitor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1372, 590);
            this.Controls.Add(this.ucOpenStoreCashRegisterSyncMonitor1);
            this.Controls.Add(this.BottomPanel);
            this.IconOptions.Image = global::SP_Sklad.Properties.Resources.system_report;
            this.Name = "frmOpenStoreCashRegisterSyncMonitor";
            this.Text = "Монітор синхронизаціїї кас";
            ((System.ComponentModel.ISupportInitialize)(this.BottomPanel)).EndInit();
            this.BottomPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl BottomPanel;
        public DevExpress.XtraEditors.SimpleButton OkButton;
        private UserControls.Warehouse.ucOpenStoreCashRegisterSyncMonitor ucOpenStoreCashRegisterSyncMonitor1;
        private System.Windows.Forms.Timer timer1;
    }
}