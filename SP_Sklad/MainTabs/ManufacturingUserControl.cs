using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SP_Sklad.SkladData;


namespace SP_Sklad.MainTabs
{
    public partial class ManufacturingUserControl : UserControl
    {
        public ManufacturingUserControl()
        {
            InitializeComponent();
        }

        private void wbStartDate_Properties_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void ManufacturingUserControl_Load(object sender, EventArgs e)
        {
         /*   whContentTab.ShowTabHeader = DevExpress.Utils.DefaultBoolean.False;
            OnDateEdit.DateTime = DateTime.Now;

            wbKagentList.Properties.DataSource = new List<object>() { new { KaId = 0, Name = "Усі" } }.Concat(new BaseEntities().Kagent.Select(s => new { s.KaId, s.Name }));
            wbKagentList.EditValue = 0;

            WhComboBox.Properties.DataSource = new List<object>() { new { WId = "*", Name = "Усі" } }.Concat(new BaseEntities().Warehouse.Select(s => new { WId = s.WId.ToString(), s.Name }).ToList());
            WhComboBox.EditValue = "*";

            wbSatusList.Properties.DataSource = new List<object>() { new { Id = -1, Name = "Усі" }, new { Id = 1, Name = "Проведені" }, new { Id = 0, Name = "Непроведені" } };
            wbSatusList.EditValue = -1;

            wbStartDate.EditValue = DateTime.Now.AddDays(-30);
            wbEndDate.EditValue = DateTime.Now;*/

            DocsTreeList.DataSource = DB.SkladBase().GetManufactureTree(DBHelper.CurrentUser.UserId).ToList();
            DocsTreeList.ExpandAll();
        }
       
    }
}
