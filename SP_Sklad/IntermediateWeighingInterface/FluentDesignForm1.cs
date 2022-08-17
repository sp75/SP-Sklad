using DevExpress.XtraBars;
using SP_Sklad.IntermediateWeighingInterface.Views;
using SP_Sklad.SkladData;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SP_Sklad.IntermediateWeighingInterface
{
    public partial class FluentDesignForm1 : DevExpress.XtraBars.FluentDesignSystem.FluentDesignForm
    {
        public BaseEntities _db { get; set; }

        public FluentDesignForm1(int wbill_id)
        {
            InitializeComponent();
            _db = new BaseEntities();
            bindingSource1.DataSource =  GetDetail(wbill_id);
        }


        private void accordionControl1_Click(object sender, EventArgs e)
        {

        }

        private void FluentDesignForm1_Load(object sender, EventArgs e)
        {
            tileView1.ColumnSet.GroupColumn = tileViewColumn1;
        }
        private List<make_det> GetDetail(int wbill_id)
        {
            var group_list = DB.SkladBase().UserAccessMatGroup.Where(w => w.UserId == DBHelper.CurrentUser.UserId).Select(s => s.GrpId).ToList();
            var wbm = _db.WayBillMake.FirstOrDefault(w => w.WbillId == wbill_id);
            var intermediate_weighing = _db.IntermediateWeighing.Where(w => w.WbillId == wbill_id).OrderBy(o=> o.OnDate).ToList();
            var intermediate_det_list = _db.v_IntermediateWeighingDet.Where(w => w.WbillId == wbill_id).ToList();
            
            var wb_make_det = _db.GetWayBillMakeDet(wbill_id).Where(w => group_list.Contains(w.GrpId.Value) && w.Rsv == 0).OrderBy(o => o.Num).ToList();

            var result = new List<make_det>();
            int rn = 0;
            foreach (var item in intermediate_weighing)
            {
                ++rn;
                foreach (var wb_make_item in wb_make_det)
                {
                    var intermediate_det_item = intermediate_det_list.FirstOrDefault(w => w.MatId == wb_make_item.MatId && w.IntermediateWeighingId == item.Id);
                    result.Add(new make_det
                    {
                        IntermediateWeighingId = item.Id,
                        IntermediateWeighingNum = item.Num,
                        IntermediateWeighingDetId = intermediate_det_item?.Id,
                        MsrName = wb_make_item.MsrName,
                        MatName = wb_make_item.MatName,
                        AmountIntermediateWeighing = intermediate_det_item?.Total,
                        MatId = wb_make_item.MatId,
                        WbillId = wbill_id,
                        IntermediateWeighingCount = intermediate_det_list.Count(co => co.MatId == wb_make_item.MatId),
                        RecipeCount = intermediate_weighing.Count(),//wbm.RecipeCount,
                        Rn = rn
                    });
                }
            }

            return result;

        }

        public make_det focused_row { get; set; }


    }
}
