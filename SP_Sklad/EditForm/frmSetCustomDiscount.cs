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
            DiscountEdit.Focus();
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
                    var base_price = Math.Round(item.BasePrice ?? 0, 2);
                    var total = Math.Round(base_price * item.Amount, 2);
                    var discount = Math.Round((total * DiscountEdit.Value / 100), 2);
                    var total_discount = total - discount;

                    var DiscountPrice = Math.Round((item.BasePrice ?? 0) * DiscountEdit.Value / 100, 2);
                    item.Price = DiscountEdit.Value > 0 && item.Amount > 0 ? (total_discount / item.Amount) : base_price;
                    item.Discount = DiscountEdit.Value;
                    item.DiscountKind = 1;
                }
            }

            _db.Save(_wb.WbillId);

        }
    }
}