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

namespace SP_Sklad.EditForm
{
    public partial class frmPricetypesEdit : Form
    {
        private BaseEntities _db { get; set; }
        private int? _PTypeId { get; set; }
        private PriceTypes pt { get; set; }

        public frmPricetypesEdit(int? PTypeId=null)
        {
            InitializeComponent();

            _PTypeId = PTypeId;
            _db = DB.SkladBase();

            ExtraTypeLookUpEdit.Properties.DataSource = new List<object>() { new { Id = 0, Name = "На ціну приходу" }, new { Id = 2, Name = "На категорію" }, new { Id = 3, Name = "Прайс-лист" } };
           
            lookUpEdit1.Properties.DataSource = _db.PriceList.Select(s => new { s.PlId, s.Name }).ToList();
        }

        private void frmPricetypesEdit_Load(object sender, EventArgs e)
        {
            if (_PTypeId == null)
            {
                pt = _db.PriceTypes.Add(new PriceTypes
                {
                    Name = "",
                    OnValue = 0,
                    Num = _db.PriceTypes.Count() + 1,
                    Def = _db.PriceTypes.Any(a => a.Def == 1) ? 0 : 1,
                    Deleted = 0,
                    ExtraType = 0

                });
                Text = "Додати нову цінову категорію:";
            }
            else
            {
                pt = _db.PriceTypes.Find(_PTypeId);
                Text = "Властвості цінової категорії:";
            }

            PriceTypesBS.DataSource = pt;

            lookUpEdit2.Properties.DataSource = _db.PriceTypes.Where(w => w.PTypeId != pt.PTypeId).Select(s => new { s.PTypeId, s.Name }).ToList();
            lookUpEdit3.Properties.DataSource = lookUpEdit2.Properties.DataSource;

            DefCheckBox.Enabled = !DefCheckBox.Checked;

            ExtraTypeLookUpEdit_EditValueChanged(sender, e);
            checkEdit3_CheckedChanged(sender, e);

            if (pt.PPTypeId == null || pt.ExtraType == 2 || pt.ExtraType == 3) checkEdit3.Checked = true;
            else checkEdit4.Checked = true;
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            if ((int)ExtraTypeLookUpEdit.EditValue == 0 && checkEdit3.Checked)
            {
                pt.PPTypeId = null;
            }

            _db.SaveChanges();
        }

        private void ExtraTypeLookUpEdit_EditValueChanged(object sender, EventArgs e)
        {
            var type = ExtraTypeLookUpEdit.EditValue == DBNull.Value ? 0 : (int)ExtraTypeLookUpEdit.EditValue;

            lookUpEdit3.Visible = (type == 2);
            lookUpEdit1.Visible = (type == 3);
        }

        private void checkEdit3_CheckedChanged(object sender, EventArgs e)
        {
            calcEdit2.Enabled = true;
            labelControl12.Enabled = true;
            lookUpEdit2.Enabled = true;

            calcEdit1.Enabled = true;
            ExtraTypeLookUpEdit.Enabled = true;
            lookUpEdit3.Enabled = true;
            lookUpEdit1.Enabled = true;


            if (checkEdit3.Checked)
            {
                calcEdit2.Enabled = false;
                labelControl12.Enabled = false;
                lookUpEdit2.Enabled = false;
            }

            if (checkEdit4.Checked)
            {
                calcEdit1.Enabled = false;
                ExtraTypeLookUpEdit.Enabled = false;
                lookUpEdit3.Enabled = false;
                lookUpEdit1.Enabled = false;
            }
        }
    }
}
