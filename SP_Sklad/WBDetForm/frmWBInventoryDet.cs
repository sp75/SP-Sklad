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

namespace SP_Sklad.WBDetForm
{
    public partial class frmWBInventoryDet : Form
    {
        BaseEntities _db { get; set; }
        private int? _PosId { get; set; }
        private WaybillList _wb { get; set; }
        private WaybillDet _wbd { get; set; }
        public List<GetPosOut_Result> pos_out_list { get; set; }
        private ReturnRel _temp_return_rel { get; set; }
        private List<GetShippedPosIn_Result> ordered_in_list { get; set; }
        public int? outPosId { get; set; }

        public frmWBInventoryDet(BaseEntities db, int? PosId, WaybillList wb)
        {
            InitializeComponent();
            _db = db;
            _PosId = PosId;
            _wb = wb;

            WHComboBox.Properties.DataSource = DBHelper.WhList();
            MatComboBox.Properties.DataSource = db.MaterialsList.ToList();
        }

        private void AmountEdit_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {

        }

        private void frmWBInventoryDet_Load(object sender, EventArgs e)
        {
            _wbd = _db.WaybillDet.Find(_PosId);

            if (_wbd == null)
            {
                _wbd = new WaybillDet()
                {
                    WbillId = _wb.WbillId,
                    Amount = 0,
                    Num = _wb.WaybillDet.Count() + 1,
                    OnDate = _wb.OnDate,
                    CurrId = _wb.CurrId,
                    OnValue = 1,
                    WId = _wb.WaybillMove.SourceWid
                };
            }
            
            WaybillDetBS.DataSource = _wbd;

            GetOk();
        }

        bool GetOk()
        {
            bool recult = (MatComboBox.EditValue != DBNull.Value && WHComboBox.EditValue != DBNull.Value && DiscountEdit.EditValue != DBNull.Value && NdsEdit.EditValue != DBNull.Value);

            OkButton.Enabled = recult;
         
            SummOblicEdit.EditValue = (AmountEdit.EditValue != DBNull.Value ? Convert.ToDecimal(AmountEdit.EditValue) : 0) *  (PriceEdit.EditValue != DBNull.Value ? Convert.ToDecimal(PriceEdit.EditValue) : 0) ;
            SummFactEdit.EditValue = DiscountEdit.Value * NdsEdit.Value;
            SummAllEdit.EditValue = (decimal)SummFactEdit.EditValue - (decimal)SummOblicEdit.EditValue;

            return recult;
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            _db.SaveChanges();
        }

        private void MatComboBox_Properties_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void AmountEdit_EditValueChanged(object sender, EventArgs e)
        {
            AmountAllEdit.EditValue = DiscountEdit.Value - (AmountEdit.EditValue != DBNull.Value ? Convert.ToDecimal(AmountEdit.EditValue) : 0);
        }

        private void calcEdit2_EditValueChanged(object sender, EventArgs e)
        {
            SummEdit.EditValue = NdsEdit.Value - (PriceEdit.EditValue != DBNull.Value ? Convert.ToDecimal(PriceEdit.EditValue) : 0);
        }
    }
}
