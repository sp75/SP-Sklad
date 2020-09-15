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

namespace SP_Sklad.ViewsForm
{
    public partial class frmManufacturing : DevExpress.XtraEditors.XtraForm
    {
        public List<CustomWBListMake> manuf_list { get; set; }

        public WBListMake_Result wb_focused_row
        {
            get
            {
                return WbGridView.GetFocusedRow() as WBListMake_Result;
            }
        }

        public frmManufacturing(BaseEntities db)
        {
            InitializeComponent();

            var satrt_date = DateTime.Now.AddYears(-100);
            var end_date = DateTime.Now.AddYears(100);


            WBListMakeBS.DataSource = db.WBListMake(satrt_date.Date, end_date.Date.AddDays(1), 2, "*", 0, -20).ToList();

        }

        private void frmManufacturing_Load(object sender, EventArgs e)
        {
            manuf_list = new List<CustomWBListMake>();
            ManufListGridControl.DataSource = manuf_list;
        }

        private void WbGridView_DoubleClick(object sender, EventArgs e)
        {
            if (xtraTabPage14.PageVisible)
            {
                AddItem.PerformClick();
            }
            else
            {
                OkButton.PerformClick();
            }
        }

        private void OkButton_Click(object sender, EventArgs e)
        {

        }

        private void AddItem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var amount = (wb_focused_row.AmountOut ?? 0) - (wb_focused_row.ShippedAmount ?? 0);

            AddWBMake(amount, $"{wb_focused_row.OnDate.Day}{wb_focused_row.OnDate.Month}{wb_focused_row.Artikul}{Convert.ToInt32( Math.Round( amount, 3) * 1000)}{0}");
        }

        private void AddWBMake(decimal Amount, string bar_code)
        {
            manuf_list.Add(new CustomWBListMake
            {
                Id = wb_focused_row.Id,
                Amount = Amount,
                MatId = wb_focused_row.MatId,
                MatName = wb_focused_row.MatName,
                MsrName = wb_focused_row.MsrName,
                Num = wb_focused_row.Num,
                OnDate = wb_focused_row.OnDate,
                Price = wb_focused_row.Price,
                WbillId = wb_focused_row.WbillId,
                BarCode = bar_code
            });

            ManufListGridView.RefreshData();
        }

        public partial class CustomWBListMake
        {
            public System.Guid Id { get; set; }
            public int WbillId { get; set; }
            public string Num { get; set; }
            public DateTime OnDate { get; set; }
            public string MatName { get; set; }
            public decimal Amount { get; set; }
            public int MatId { get; set; }
            public string MsrName { get; set; }
            public decimal? Price { get; set; }
            public string BarCode { get; set; }
        }

        private void BarCodeEdit_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13 && !String.IsNullOrEmpty(BarCodeEdit.Text) && BarCodeEdit.Text.Count() == 13)
            {
                var day = BarCodeEdit.Text.Substring(0, 2);
                var mounth = BarCodeEdit.Text.Substring(2, 2);
                var artikul = BarCodeEdit.Text.Substring(4, 3);
                decimal amount = Convert.ToInt32(BarCodeEdit.Text.Substring(7, 5)) / 1000.00m;

                var row = WBListMakeBS.List.OfType<WBListMake_Result>().ToList().FirstOrDefault(f => f.Artikul == artikul);
                var pos = WBListMakeBS.IndexOf(row);
                WBListMakeBS.Position = pos;
                //  var amount = wb_focused_row.AmountOut ?? 0;

                if (row != null && xtraTabPage14.PageVisible)
                {
                    AddWBMake(amount, BarCodeEdit.Text);
                }


                BarCodeEdit.Text = "";
            }
        }

        private void DelItem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var r = ManufListGridView.GetFocusedRow() as CustomWBListMake;
            manuf_list.Remove(r);

            ManufListGridView.RefreshData();
        }

        private void ManufListGridView_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.FieldName == "Amount")
            {
                var amount = Convert.ToDecimal(e.Value);
                var r = ManufListGridView.GetRow(e.RowHandle) as CustomWBListMake;

                r.BarCode = $"{r.WbillId.ToString()}+{Math.Truncate(amount)}+{(int)(amount * 1000m) % 1000}+{r.MatId}";
            }
        }
    }
}
