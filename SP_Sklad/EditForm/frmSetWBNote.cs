using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using SP_Sklad.SkladData;

namespace SP_Sklad.ViewsForm
{
    public partial class frmSetWBNote : DevExpress.XtraEditors.XtraForm
    {
        private BaseEntities _db { get; set; }
        private WaybillList _wb { get; set; }


        public frmSetWBNote( WaybillList wb)
        {
            InitializeComponent();
            _wb = wb;
        }



        private void AmountEdit_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                OkButton.PerformClick();
            }

        }

        private void frmSetDiscountCard_Shown(object sender, EventArgs e)
        {
            NoteEdit.Focus();
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            _wb.Notes = NoteEdit.Text;
        }

    }
}