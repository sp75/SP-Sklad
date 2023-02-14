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
    public partial class frmSetCustomDiscount : DevExpress.XtraEditors.XtraForm
    {
        private BaseEntities _db { get; set; }
        private WaybillList _wb { get; set; }


        public frmSetCustomDiscount(BaseEntities db, WaybillList wb)
        {
            InitializeComponent();
            _db = db;
            _wb = wb;
        }

        private void AmountEdit_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {

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
            AmountEdit.Focus();
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            SetDiscount();
        }

        private void SetDiscount()
        {
            foreach (var item in _db.WaybillDet.Where(w => w.WbillId == _wb.WbillId))
            {
                if (item.DiscountKind != 2)
                {
                    var DiscountPrice = Math.Round(Convert.ToDecimal(item.BasePrice * AmountEdit.Value / 100), 2);
                    item.Price = (item.BasePrice - DiscountPrice) * (100 / (100 + item.Nds.Value));
                    item.Discount = AmountEdit.Value;
                    item.DiscountKind = 1;
                }
            }

            _db.Save(_wb.WbillId);

        }
    }
}