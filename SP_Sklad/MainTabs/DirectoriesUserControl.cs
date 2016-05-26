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
    public partial class DirectoriesUserControl : UserControl
    {
        public DirectoriesUserControl()
        {
            InitializeComponent();
        }

        private void wbStartDate_Properties_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void DirectoriesUserControl_Load(object sender, EventArgs e)
        {
           DirTreeList.DataSource = DB.SkladBase().GetDirTree(DBHelper.CurrentUser.UserId).ToList();
            DirTreeList.ExpandToLevel(0);// ExpandAll();
        }
    }
}
