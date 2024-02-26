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
   // [DefaultEvent(nameof(ValueChanged))]
    public partial class ucWBFilterPanel : DevExpress.XtraEditors.XtraUserControl
    {
        public ucWBFilterPanel()
        {
            InitializeComponent();
        }

        [Browsable(true)]
        public DateTime StartDate => wbStartDate.DateTime;
        [Browsable(true)]
        public DateTime EndDate  => wbEndDate.DateTime;
        [Browsable(true)]
        public int Status => (int)wbStatusList.EditValue;
        [Browsable(true)]
        public int KagentId => (int)wbKagentList.EditValue;
        [Browsable(true)]
        public string Title
        {
            get => kaListLabelControl.Text;
            set => kaListLabelControl.Text = value;
        }


        private void ucWBFilterPanel_Load(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                wbKagentList.Properties.DataSource = DBHelper.KagentsList;
                wbKagentList.EditValue = 0;

                wbStatusList.Properties.DataSource = new List<object>() { new { Id = -1, Name = "Усі" }, new { Id = 1, Name = "Проведені" }, new { Id = 0, Name = "Непроведені" } };
                wbStatusList.EditValue = -1;

                wbStartDate.EditValue = DateTime.Now.Date.AddDays(-1);
                wbEndDate.EditValue = DateTime.Now.Date.SetEndDay();
            }
        }

        [Browsable(true)]
        public new event EventHandler TextChanged
        {
            add => textEdit1.EditValueChanged += value;
            remove => textEdit1.EditValueChanged -= value;
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

            textEdit1.EditValue = Guid.NewGuid();
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
                textEdit1.EditValue = Guid.NewGuid();
            }
        }

        private void wbKagentList_EditValueChanged(object sender, EventArgs e)
        {
            if (wbKagentList.ContainsFocus)
            {
                textEdit1.EditValue = Guid.NewGuid();
            }
        }

        private void wbStatusList_EditValueChanged(object sender, EventArgs e)
        {
            if (wbStatusList.ContainsFocus)
            {
                textEdit1.EditValue = Guid.NewGuid();
            }
        }

        private void wbEndDate_EditValueChanged(object sender, EventArgs e)
        {
            if (wbEndDate.ContainsFocus)
            {
                textEdit1.EditValue = Guid.NewGuid();
            }
        }

    }
}
