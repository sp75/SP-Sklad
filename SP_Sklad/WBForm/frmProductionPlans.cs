using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SP_Sklad.Common;
using SP_Sklad.SkladData;

namespace SP_Sklad.WBForm
{
    public partial class frmProductionPlans : DevExpress.XtraEditors.XtraForm
    {

        public BaseEntities _db { get; set; }
        
        public Guid? _doc_id { get; set; }
        private ProductionPlans pp { get; set; }
     
        /*private GetWaybillDetIn_Result wbd_row { get; set; }
        private List<GetWaybillDetIn_Result> wbd_list { get; set; }
        private List<GetRelDocList_Result> rdl { get; set; }
        private GetWaybillDetIn_Result focused_dr
        {
            get { return WaybillDetInGridView.GetFocusedRow() as GetWaybillDetIn_Result; }
        }*/

        public bool is_new_record { get; set; }
        private UserSettingsRepository user_settings { get; set; }

        public frmProductionPlans(Guid? doc_id)
        {
            is_new_record = false;
            _doc_id = doc_id;
            _db = new BaseEntities();
            user_settings = new UserSettingsRepository(DBHelper.CurrentUser.UserId, _db);

            InitializeComponent();
        }

        private void frmProductionPlans_Load(object sender, EventArgs e)
        {
            PersonComboBox.Properties.DataSource = DBHelper.Persons;
            var wh_list = DBHelper.WhList();
            WHComboBox.Properties.DataSource = wh_list;
            WHComboBox.EditValue = wh_list.Where(w => w.Def == 1).Select(s => s.WId).FirstOrDefault();

            if (_doc_id == null)
            {
                is_new_record = true;

                pp = _db.ProductionPlans.Add(new ProductionPlans
                {
                    Id = Guid.NewGuid(),
                    OnDate = DBHelper.ServerDateTime(),
                    Num = "",
                    PersonId = DBHelper.CurrentUser.KaId,
                    UpdatedBy = DBHelper.CurrentUser.UserId,
                    EntId = DBHelper.Enterprise.KaId
                });

                _db.SaveChanges();
            }
            else
            {
                pp = _db.ProductionPlans.FirstOrDefault(f => f.Id == _doc_id );
            }

            if (pp != null)
            {
                _doc_id = pp.Id;

                pp.SessionId =  (Guid?)UserSession.SessionId;
                pp.UpdatedBy = UserSession.UserId;
                pp.UpdatedAt = DateTime.Now;
                _db.SaveChanges();

                if (is_new_record)
                {
                    pp.Num = new BaseEntities().GetDocNum("prod_plan").FirstOrDefault();
                }

              //  TurnDocCheckBox.EditValue =


              //  rdl = _db.GetRelDocList(wb.Id).Where(w => w.DocType == 7 || w.DocType == -22).ToList();
            
             //   AddBarSubItem.Enabled = !rdl.Any();
            //    EditMaterialBtn.Enabled = !rdl.Any(a => a.DocType == 7);
            //    DelMaterialBtn.Enabled = AddBarSubItem.Enabled;
            }

        //    RefreshDet();
        }
    }
}
