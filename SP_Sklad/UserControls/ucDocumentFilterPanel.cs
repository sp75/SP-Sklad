using DevExpress.XtraEditors;
using SP_Sklad.Common;
using SP_Sklad.SkladData;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace SP_Sklad.UserControls
{
    public partial class ucDocumentFilterPanel : DevExpress.XtraEditors.XtraUserControl
    {
        public delegate void OnFilterChanged(object sender, EventArgs e);
        public event OnFilterChanged OnFilterChangedEvent;

        public ucDocumentFilterPanel()
        {
            InitializeComponent();
        }

        [Browsable(false)]
        public DateTime StartDate => wbStartDate.DateTime;
        [Browsable(false)]
        public DateTime EndDate  => wbEndDate.DateTime;
        [Browsable(false)]
        public int StatusId => (int)wbStatusList.EditValue;
        [Browsable(false)]
        public int KagentId => (int)wbKagentList.EditValue;
        [Browsable(true)]
        public string Title
        {
            get => kaListLabelControl.Text;
            set => kaListLabelControl.Text = value;
        }
        [Browsable(true)]
        public int KagentImageIndex
        {
            get => wbKagentList.Properties.Buttons[2].ImageOptions.ImageIndex;
            set => wbKagentList.Properties.Buttons[2].ImageOptions.ImageIndex = value;
        }


        private void ucWBFilterPanel_Load(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                wbKagentList.Properties.DataSource =  DBHelper.KagentsList;
                wbKagentList.EditValue = 0;

                wbStatusList.Properties.DataSource = new List<object>() { new { Id = -1, Name = "Усі" }, new { Id = 1, Name = "Проведені" }, new { Id = 0, Name = "Непроведені" } };
                wbStatusList.EditValue = -1;

                wbStartDate.EditValue = DateTime.Now.Date.AddDays(-1);
                wbEndDate.EditValue = DateTime.Now.Date.SetEndDay();
            }
        }

        [Browsable(true)]
        public event OnFilterChanged FilterChanged
        {
            add => this.OnFilterChangedEvent += value;
            remove => this.OnFilterChangedEvent -= value;
        }

        public void ClearFindFilter(DateTime on_date)
        {
            PeriodComboBoxEdit.SelectedIndex = 0;
            wbStartDate.DateTime = on_date.Date;
            wbEndDate.DateTime = on_date.Date.SetEndDay();
            wbKagentList.EditValue = 0;
            wbStatusList.EditValue = -1;
        }

        private void PeriodComboBoxEdit_EditValueChanged(object sender, EventArgs e)
        {
            if (!PeriodComboBoxEdit.ContainsFocus)
            {
                return;
            }

            wbEndDate.DateTime = DateTime.Now.Date.SetEndDay();
            switch (PeriodComboBoxEdit.SelectedIndex)
            {
                case 1:
                    wbStartDate.DateTime = DateTime.Now.Date;
                    break;

                case 2:
                    wbStartDate.DateTime = DateTime.Now.Date.StartOfWeek(DayOfWeek.Monday);
                    break;

                case 3:
                    wbStartDate.DateTime = DateTime.Now.Date.FirstDayOfMonth();
                    break;

                case 4:
                    wbStartDate.DateTime = new DateTime(DateTime.Now.Year, 1, 1);
                    break;
            }

            OnFilterChangedEvent?.Invoke( sender,  e);
        }

        private void wbKagentList_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
            {
                wbKagentList.EditValue = 0;
            }
            else if (e.Button.Index == 2)
            {
                wbKagentList.EditValue = IHelper.ShowDirectList(wbKagentList.EditValue, 1);
            }
        }

        private void wbKagentList_Properties_MouseUp(object sender, MouseEventArgs e)
        {
            wbKagentList.SelectAll();
        }

        private void wbStartDate_EditValueChanged(object sender, EventArgs e)
        {
            if (wbStartDate.ContainsFocus)
            {
                OnFilterChangedEvent?.Invoke(sender, e);
            }
        }

        private void wbKagentList_EditValueChanged(object sender, EventArgs e)
        {
            if (wbKagentList.ContainsFocus)
            {
                OnFilterChangedEvent?.Invoke(sender, e);
            }
        }

        private void wbStatusList_EditValueChanged(object sender, EventArgs e)
        {
            if (wbStatusList.ContainsFocus)
            {
                OnFilterChangedEvent?.Invoke(sender, e);
            }
        }

        private void wbEndDate_EditValueChanged(object sender, EventArgs e)
        {
            if (wbEndDate.ContainsFocus)
            {
                OnFilterChangedEvent?.Invoke(sender, e);
            }
        }

    }
}
