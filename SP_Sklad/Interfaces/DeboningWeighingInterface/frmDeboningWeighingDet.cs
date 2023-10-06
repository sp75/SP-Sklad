using DevExpress.XtraBars;
using SP_Sklad.Common;
using SP_Sklad.DeboningWeighingInterface.Views;
using SP_Sklad.EditForm;
using SP_Sklad.SkladData;
using SP_Sklad.ViewsForm;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.SqlServer;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using EntityState = System.Data.Entity.EntityState;

namespace SP_Sklad.DeboningWeighingInterface
{
    public partial class frmDeboningWeighingDet : DevExpress.XtraBars.FluentDesignSystem.FluentDesignForm
    {
        private int _user_id { get; set; }
        private DeboningDetView focused_row => tileView1.GetFocusedRow() as DeboningDetView;
        private int _wbill_id { get; set; }

        public frmDeboningWeighingDet(int wbill_id, int user_id)
        {
            InitializeComponent();
            _wbill_id = wbill_id;
            _user_id = user_id;

            GetDetail(wbill_id);
        }


        private void FluentDesignForm1_Load(object sender, EventArgs e)
        {
            sidePanel1.Hide();

            using (var s = new UserSettingsRepository())
            {
           //     AmountEdit.ReadOnly = !(s.AccessEditWeight == "1");
            }
        }

        private List<DeboningDetView> GetDetail(int wbill_id)
        {
            using (var _db = new BaseEntities())
            {
                var result = _db.v_DeboningDet.Where(w => w.WBillId == wbill_id)
                    .Select(s => new DeboningDetView
                    {
                        DebId = s.DebId,
                        Amount = s.Amount,
                        MsrName = s.MsrName,
                        MatName = s.MatName,
                        MatId = s.MatId,
                        BMP = _db.Materials.FirstOrDefault(w => w.MatId == s.MatId).BMP,
                        TotalWeighing = s.TotalWeighing != null ? (SqlFunctions.StringConvert(s.TotalWeighing, 10, 3) + s.MsrName) : "",
                         WBillId = s.WBillId
                    }).ToList();

                bindingSource1.DataSource = result;

                return result;
            }

        }
        private void tileView1_ItemClick(object sender, DevExpress.XtraGrid.Views.Tile.TileViewItemClickEventArgs e)
        {
            sidePanel1.Show();

            tabNavigationPage1.Caption = focused_row.MatName;

            GetDeboningWeighing();

            GetOk();
        }

        private void GetDeboningWeighing()
        {
            using (var _db = new BaseEntities())
            {
                gridControl2.DataSource = _db.DeboningWeighing.Where(w => w.WbillId == focused_row.WBillId && w.MatId == focused_row.MatId).OrderBy(o=> o.CreatedAt).ToList();
            }
        }

     

        private void GetOk()
        {
            var row = focused_row;

            if (row != null)
            {
                using (var _db = new BaseEntities())
                {
                    ;
                }

            }
            else
            {
                ;
            }
        }

        private void TaraCalcEdit_EditValueChanged(object sender, EventArgs e)
        {
            GetOk();
        }



        Color colorPanelReady = Color.MistyRose;//Color.FromArgb(58, 166, 101);
        Color colorPanelSold = Color.FromArgb(158, 158, 158);
        Color colorCaptionReady = Color.Black;//Color.FromArgb(193, 222, 204);
        Color colorCaptionSold =  Color.FromArgb(219, 219, 219);

        private void tileView1_ItemCustomize(object sender, DevExpress.XtraGrid.Views.Tile.TileViewItemCustomizeEventArgs e)
        {
            if (e.Item == null || e.Item.Elements.Count == 0)
                return;
            var is_done = !string.IsNullOrEmpty((string)tileView1.GetRowCellValue(e.RowHandle, tileView1.Columns["IntermediateWeighingDetTotal"]));

            var RecipeCaption = e.Item.GetElementByName("RecipeCaption");
            var WBDateCaption = e.Item.GetElementByName("WBDateCaption");
            //     var price = e.Item.GetElementByName("Price");

            e.Item.AppearanceItem.Normal.BackColor = is_done ? colorPanelSold : colorPanelReady;

            RecipeCaption.Appearance.Normal.ForeColor = is_done ? colorCaptionSold : colorCaptionReady;
            WBDateCaption.Appearance.Normal.ForeColor = is_done ? colorCaptionSold : colorCaptionReady;
            //   if (sold) price.Text = "Sold";
        }

        private void simpleButton2_Click_1(object sender, EventArgs e)
        {
            Close();
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            using (var frm = new frmWeightEdit(focused_row.MatName, 1))
            {
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    using (var _db = new BaseEntities())
                    {
                        _db.DeboningWeighing.Add(new DeboningWeighing
                        {
                            Id = Guid.NewGuid(),
                            WbillId = focused_row.WBillId,
                            MatId = focused_row.MatId,
                            CreatedAt = DateTime.Now,
                            Amount = frm.AmountEdit.Value
                        });

                        _db.SaveChanges();
                    }

                    GetDeboningWeighing();

                    GetOk();
                }
            }
        }
    }
}
