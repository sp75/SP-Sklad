using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SP_Sklad.SkladData;

namespace SP_Sklad
{
    public partial class frmLogHistory : Form
    {
        private int? _tab_id { get; set; }
        private int? _id { get; set; }

        public frmLogHistory(int? tab_id , int? id)
        {
            InitializeComponent();
            _tab_id = tab_id;
            _id = id;
        }

        private void frmLogHistory_Load(object sender, EventArgs e)
        {
            OprLogGridControl.DataSource = DB.SkladBase().OperLog.Where(w => w.Id == _id && w.TabId == _tab_id).Select(s => new
            {
                s.OpId,
                s.OpCode,
                s.Users.Name,
                s.OnDate,
                s.DataBefore,
                s.DataAfter
            }).OrderBy(o => o.OnDate).ToList();
        }
    }
}
