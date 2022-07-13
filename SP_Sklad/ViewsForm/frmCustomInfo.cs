using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SP_Sklad.ViewsForm
{
    public partial class frmCustomInfo : DevExpress.XtraEditors.XtraForm
    {
        public List<CustomGridItem> _list { get; set; }

        public frmCustomInfo()
        {
            InitializeComponent();

            _list = new List<CustomGridItem>();
        }

        public void AddItem(string title , object val)
        {
            _list.Add(new CustomGridItem { Title = title, Value = val });
        }

        public class CustomGridItem
        {
            public string Title { get; set; }
            public object Value { get; set; }
        }

        private void frmCustomInfo_Shown(object sender, EventArgs e)
        {
            KontragentGrid.DataSource = _list;
        }
    }
}
