using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SP_Sklad.IntermediateWeighingInterface.Views;
using SP_Sklad.SkladData;
using SP_Sklad.WBDetForm;

namespace SP_Sklad.IntermediateWeighingInterface
{
    public partial class frmMainIntermediateWeighing : DevExpress.XtraEditors.XtraForm
    {
        public static int _user_id { get; set; }
        public static frmMainIntermediateWeighing main_form { get; set; }

        public BaseEntities _db { get; set; }

        private IntermediateWeighingView intermediate_weighing_focused_row => IntermediateWeighingGridView.GetFocusedRow() as IntermediateWeighingView;

        public frmMainIntermediateWeighing(int user_id)
        {
            InitializeComponent();

            _user_id = user_id;
            _db = new BaseEntities();
        }

        private void wbStartDate_Properties_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            GetIntermediateWeighing();
        }

        void GetIntermediateWeighing()
        {

        //    int top_row = IntermediateWeighingGridView.TopRowIndex;
        //    var satrt_date = IntermediateWeighingStartDate.DateTime < DateTime.Now.AddYears(-100) ? DateTime.Now.AddYears(-100) : IntermediateWeighingStartDate.DateTime;
        //    var end_date = IntermediateWeighingEndDate.DateTime < DateTime.Now.AddYears(-100) ? DateTime.Now.AddYears(100) : IntermediateWeighingEndDate.DateTime;

            IntermediateWeighingBS.DataSource = _db.v_IntermediateWeighing.Where(w =>/* w.OnDate > satrt_date && w.OnDate <= end_date && */w.Checked == 0)
                .GroupBy(g => new { g.WbillId, g.WbNum, g.RecipeName, g.WbOnDate, g.RecipeCount })
                .Select(s=> new IntermediateWeighingView
                {
                    WbillId = s.Key.WbillId,
                    WbNum = s.Key.WbNum,
                    WbOnDate = s.Key.WbOnDate,
                    RecipeName = s.Key.RecipeName,
                    RecipeCount = s.Key.RecipeCount
                })
                .OrderBy(o => o.WbOnDate).ToList();


     //       IntermediateWeighingGridView.TopRowIndex = top_row;
        }

       


        private void xtraTabControl6_PaddingChanged(object sender, EventArgs e)
        {
            if (intermediate_weighing_focused_row == null)
            {

                IntermediateWeighingDetBS.DataSource = null;
                gridControl16.DataSource = null;
                return;
            }


            switch (xtraTabControl6.SelectedTabPageIndex)
            {
                case 0:
                    var group_list = DB.SkladBase().UserAccessMatGroup.Where(w => w.UserId == DBHelper.CurrentUser.UserId).Select(s => s.GrpId).ToList();
                    var wbm = _db.WayBillMake.FirstOrDefault(w => w.WbillId == intermediate_weighing_focused_row.WbillId);
                    var det_list = DB.SkladBase().v_IntermediateWeighingDet.Where(w => w.WbillId == intermediate_weighing_focused_row.WbillId).ToList();


                    var  mat_list = DB.SkladBase().GetWayBillMakeDet(intermediate_weighing_focused_row.WbillId).Where(w => group_list.Contains(w.GrpId.Value) && w.Rsv == 0).OrderBy(o => o.Num).ToList()
                        .Select(s => new make_det
                        {
                            MatName = s.MatName,
                            MsrName = s.MsrName,
                            AmountByRecipe = s.AmountByRecipe,
                            AmountIntermediateWeighing = _db.v_IntermediateWeighingDet.Where(w => w.WbillId == intermediate_weighing_focused_row.WbillId && w.MatId == s.MatId /*&& w.Id != det.Id*/).Sum(st => st.Total),
                            MatId = s.MatId,
                            WbillId = intermediate_weighing_focused_row.WbillId,
                            RecipeCount = wbm.RecipeCount,
                            IntermediateWeighingCount = _db.v_IntermediateWeighingDet.Where(w => w.WbillId == intermediate_weighing_focused_row.WbillId && w.MatId == s.MatId /*&& w.Id != det.Id*/).Count(),
                            TotalWeightByRecipe = wbm.AmountByRecipe,
                            RecId = wbm.RecId
                        }).ToList();

                   

                    var empty_list = mat_list.Where(w => !det_list.Any(a => a.MatId == w.MatId)).Select(ss => new make_det
                    {
                        MsrName = ss.MsrName,
                        MatName = ss.MatName,
                        AmountIntermediateWeighing = 0,
                        Rn = 1,
                        MatId = ss.MatId,
                        WbillId = ss.WbillId,
                        RecipeCount = ss.RecipeCount,
                        IntermediateWeighingCount = ss.IntermediateWeighingCount
                    }).ToList();

                    var list = det_list.GroupBy(g => g.MatName)  // PARTITION BY ^^^^
                   .Select(c => c.OrderBy(o => o.CreatedDate).Select((v, i) => new { i, v }).ToList()) //  ORDER BY ^^
                   .SelectMany(c => c)
                   .Select(c => new make_det
                   {
                       MsrName = c.v.MsrName,
                       MatName = c.v.MatName,
                       AmountIntermediateWeighing = c.v.Total,
                       MatId = c.v.MatId,
                       WbillId = c.v.WbillId,
                       IntermediateWeighingCount = det_list.Count(co => co.MatId == c.v.MatId),
                       RecipeCount = intermediate_weighing_focused_row.RecipeCount,
                       Rn = c.i + 1
                   }).ToList();

                    list.AddRange(empty_list);

                    bindingSource1.DataSource = list;

                   /*




                    var list = _db.v_IntermediateWeighingDet.AsNoTracking().Where(w => w.WbillId == intermediate_weighing_focused_row.WbillId).OrderBy(o => o.CreatedDate).ToList();

                    int top_row = WaybillDetInGridView.TopRowIndex;
                    IntermediateWeighingDetBS.DataSource = list;
                    WaybillDetInGridView.TopRowIndex = top_row;*/
                    break;


                case 1:
                    //    gridControl16.DataSource = _db.GetRelDocList(intermediate_weighing_focused_row.Id).OrderBy(o => o.OnDate).ToList();
                    break;
            }
        }

        private void IntermediateWeighingGridView_FocusedRowObjectChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventArgs e)
        {
            xtraTabControl6_PaddingChanged(sender, null);
        }

        private void button1_Click(object sender, EventArgs e)
        {
                var total = _db.IntermediateWeighing.Where(w => w.WbillId == intermediate_weighing_focused_row.WbillId).Sum(s => s.Amount);
                var amount_by_recipe = _db.WayBillMake.FirstOrDefault(w => w.WbillId == intermediate_weighing_focused_row.WbillId).AmountByRecipe;

                if ((total ?? 0) > (amount_by_recipe ?? 0))
                {
                    MessageBox.Show("Вага закладок по виробництву більша запланованої");

                    return;
                }

                var iw = _db.IntermediateWeighing.Find(intermediate_weighing_focused_row.Id);
                using (var f = new frmIntermediateWeighingDet(_db, Guid.NewGuid(), iw))
                {
                    f.ShowDialog();
                }

         //   RefreshDet();
        }

        private void frmMainIntermediateWeighing_FormClosed(object sender, FormClosedEventArgs e)
        {
        /*    docsUserControl1.SaveGridLayouts();
            whUserControl.SaveGridLayouts();
            manufacturingUserControl1.SaveGridLayouts();
            tradeUserControl1.SaveGridLayouts();*/

            using (var db = new BaseEntities())
            {
                var user = db.Users.Find(_user_id);
                if (user != null)
                {
                    user.IsOnline = false;
                }

                db.SaveChanges();
            }

            Application.Exit();
        }
    }
}
