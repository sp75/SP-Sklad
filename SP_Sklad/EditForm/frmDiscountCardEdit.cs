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

namespace SP_Sklad.EditForm
{
    public partial class frmDiscountCardEdit : DevExpress.XtraEditors.XtraForm
    {
        private BaseEntities _db { get; set; }
        private int? _id { get; set; }
        private DiscCards dc { get; set; }

        public frmDiscountCardEdit(int? CardId = null)
        {
            InitializeComponent();

            _id = CardId;
            _db = DB.SkladBase();
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            _db.SaveChanges();
        }

        private void frmDiscountCardEdit_Load(object sender, EventArgs e)
        {
            if (_id == null)
            {
                dc = _db.DiscCards.Add(new DiscCards
                {
                    GrpId = 1,
                    ExpireDate = DateTime.Now.AddHours(1),
                    OnValue = 0,
                    Num = "",
                    DiscType = 1
                });
            }
            else
            {
                dc = _db.DiscCards.Find(_id);
            }

            DiscCardsBS.DataSource = dc;

            KagentComboBox.Properties.DataSource = DBHelper.Kagents;
        }

        private void KTypeLookUpEdit_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {

        }
    }
}