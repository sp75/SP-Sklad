using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using SP_Sklad.Common;
using SP_Sklad.SkladData;

namespace SP_Sklad.WBForm
{
    public partial class frmCashboxWBOut : DevExpress.XtraEditors.XtraForm
    {
        private List<GetWayBillDetOut_Result> wbd_list { get; set; }
        private BaseEntities _db { get; set; }
        public WaybillList wb { get; set; }
        public bool is_new_record { get; set; }
        public int? _wbill_id { get; set; }

        public frmCashboxWBOut()
        {
            InitializeComponent();

            _db = new BaseEntities();
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            if ( !DBHelper.CheckOrderedInSuppliers(wb.WbillId, _db)) return;

            if (!DBHelper.CheckInDate(wb, _db, wb.OnDate))
            {
                return;
            }

       //     payDocUserControl1.Execute(wb.WbillId);

            wb.UpdatedAt = DateTime.Now;
            _db.Save(wb.WbillId);

            if (!wbd_list.Any(w => w.Rsv == 0 && w.PosType == 0 && w.Total > 0))
            {
                var ex_wb = _db.ExecuteWayBill(wb.WbillId, null, DBHelper.CurrentUser.KaId).ToList();
            }

            is_new_record = false;

            Close();
        }

        private void frmCashboxWBOut_Load(object sender, EventArgs e)
        {
          //  KagentComboBox.Properties.DataSource = DBHelper.Kagents;
         //   PersonComboBox.Properties.DataSource = DBHelper.Persons;

            is_new_record = true;

            wb = _db.WaybillList.Add(new WaybillList()
            {
                Id = Guid.NewGuid(),
                WType = -1,
                OnDate = DBHelper.ServerDateTime(),
                Num = new BaseEntities().GetDocNum("wb_out").FirstOrDefault(),
                CurrId = DBHelper.Currency.FirstOrDefault(w => w.Def == 1).CurrId,
                OnValue = 1,
                PersonId = DBHelper.CurrentUser.KaId,
                EntId = DBHelper.Enterprise.KaId,
                UpdatedBy = DBHelper.CurrentUser.UserId,
                KaId = _db.Kagent.FirstOrDefault().KaId,
                Nds = 0
            });

            _db.SaveChanges();

            _wbill_id = wb.WbillId;

            DBHelper.UpdateSessionWaybill(wb.WbillId);

            WaybillListBS.DataSource = wb;
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            wb.Kontragent = _db.Kagent.Find(wb.KaId);

            IHelper.ShowMatListByWH(_db, wb);

            _db.SaveChanges();

            _db.ReservedAllPosition(wb.WbillId, DBHelper.CurrentUser.UserId).ToList();

            RefreshDet();

            WaybillDetOutGridView.MoveLastVisible();
        }

        private void RefreshDet()
        {
            wbd_list = _db.GetWayBillDetOut(_wbill_id).OrderBy(o => o.Num).ToList();

            int top_row = WaybillDetOutGridView.TopRowIndex;
            WaybillDetOutBS.DataSource = wbd_list;
            WaybillDetOutGridView.TopRowIndex = top_row;

         // GetOk();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void frmCashboxWBOut_FormClosed(object sender, FormClosedEventArgs e)
        {
            DBHelper.UpdateSessionWaybill(_wbill_id.Value, true);

            if (is_new_record)
            {
                _db.DeleteWhere<WaybillList>(w => w.WbillId == _wbill_id);
            }


            _db.Dispose();

        }

        private void frmCashboxWBOut_KeyPress(object sender, KeyPressEventArgs e)
        {
            ;
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            textEdit4.Text += ((SimpleButton)sender).Text;
        }

        private void simpleButton12_Click(object sender, EventArgs e)
        {
            textEdit4.Text = "";
        }

        private void simpleButton5_Click(object sender, EventArgs e)
        {

        }

        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void simpleButton5_Click_1(object sender, EventArgs e)
        {
            textEdit4.Text = "dfsdfsdf";
        }
    }
}
