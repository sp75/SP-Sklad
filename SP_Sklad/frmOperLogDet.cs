using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SP_Sklad.Reports;
using SP_Sklad.SkladData;

namespace SP_Sklad
{
    public partial class frmOperLogDet : DevExpress.XtraEditors.XtraForm
    {
        public frmOperLogDet()
        {
            InitializeComponent();
        }

        private void HistoryBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var dr = OperLogDetBS.DataSource as GetOperLog_Result;
            new frmLogHistory(dr.TabId, dr.Id).ShowDialog();
        }

        private void PrevievBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var dr = OperLogDetBS.DataSource as GetOperLog_Result;
            if (dr.DocType != null)
            {
                 if (dr.DocType != -3 && dr.DocType != 3 && dr.DocType != -2 && dr.DocType != 18 && dr.DocType != 8 && dr.DocType != -8 && dr.DocType != -7)
                {
                    var wb = DB.SkladBase().WaybillList.Find(dr.Id);
                    if (wb != null)
                   {
                       PrintDoc.Show(wb.DocId.Value, dr.DocType.Value, DB.SkladBase());
                   }
                   else
                   {
                       MessageBox.Show("Документ не знайдено !");
                   }
                }
            }
        }
    }
}
