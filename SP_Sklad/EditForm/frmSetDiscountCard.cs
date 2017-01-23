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
    public partial class frmSetDiscountCard : DevExpress.XtraEditors.XtraForm
    {
        BaseEntities _db { get; set; }
        int _waybill_id { get; set; }

        public frmSetDiscountCard(BaseEntities db, int waybill_id)
        {
            InitializeComponent();
            _db = db;
            _waybill_id = waybill_id;
        }

        private void AmountEdit_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {

        }

        private void AmountEdit_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                SetDiscount();
                Close();
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
            var cart = DB.SkladBase().DiscCards.FirstOrDefault(w => w.Num == AmountEdit.Text && w.Deleted == 0 && w.ExpireDate >= DateTime.Now);
            if (cart != null)
            {
                foreach (var item in _db.WaybillDet.Where(w => w.WbillId == _waybill_id))
                {
                    var DiscountPrice = item.BasePrice - (item.BasePrice * cart.OnValue / 100);
                    item.Price = DiscountPrice * 100 / (100 + item.Nds.Value);
                    item.Discount = cart.OnValue;
                    item.DiscountKind = 2;

                    if (item.WayBillDetAddProps != null)
                    {
                        item.WayBillDetAddProps.CardId = cart.CardId;
                    }
                    else
                    {
                        _db.WayBillDetAddProps.Add(new WayBillDetAddProps
                        {
                            CardId = cart.CardId,
                            PosId = item.PosId
                        });
                    }

                }
                _db.Save(_waybill_id);
            }
            else
            {
                MessageBox.Show("Дисконтну картку не знайдено !");
            }
        }
    }
}