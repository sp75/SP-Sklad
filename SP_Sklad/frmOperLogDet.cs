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
    public partial class frmOperLogDet : Form
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
                   var DocId =  DB.SkladBase().WaybillList.Where(w=> w.WbillId == dr.Id).Select(s=> s.DocId).First();
                   if (DocId != null)
                   {
                       PrintDoc.Show(DocId.Value, dr.DocType.Value, DB.SkladBase());
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
