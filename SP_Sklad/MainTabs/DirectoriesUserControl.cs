using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SP_Sklad.SkladData;

namespace SP_Sklad.MainTabs
{
    public partial class DirectoriesUserControl : UserControl
    {
        GetDirTree_Result focused_tree_node { get; set; }
        public bool isCatalog { get; set; }
        public bool isMatList { get; set; }
        public List<CustomMatList> custom_mat_list { get; set; }
        public WaybillList wb { get; set; }
        public Object resut { get; set; }

        public DirectoriesUserControl()
        {
            InitializeComponent();
           custom_mat_list  = new List<CustomMatList>();
        }

        private void wbStartDate_Properties_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void DirectoriesUserControl_Load(object sender, EventArgs e)
        {
            MatListGridControl.DataSource = custom_mat_list;
          /*  if (isMatList)
            {
                xtraTabPage3.PageVisible = false;
                xtraTabPage4.PageVisible = false;
                xtraTabPage5.PageVisible = false;
                xtraTabPage12.PageVisible = false;
                xtraTabPage13.PageVisible = false;
                xtraTabPage14.PageVisible = true;
            }*/
            if (!DesignMode)
            {
                using (var db = new BaseEntities())
                {
                    repositoryItemLookUpEdit1.DataSource = DBHelper.WhList();

                    DirTreeList.DataSource = db.GetDirTree(DBHelper.CurrentUser.UserId).ToList();
                    DirTreeList.ExpandToLevel(0);// ExpandAll();
                }
            }
        }

        private void DirTreeList_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
        {
            focused_tree_node = DirTreeList.GetDataRecordByNode(e.Node) as GetDirTree_Result;

            RefrechItemBtn.PerformClick();
            wbContentTab.SelectedTabPageIndex = focused_tree_node.GType.Value;
        }

        private void RefrechItemBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            switch (focused_tree_node.GType)
            {
            /*    case 1: KAgent->Close();
                    KAgent->ParamByName("WDATE")->Value = frmMain->WorkDateEdit->Date;
                    if (DirectTreeID->Value == 10) KAgent->Filter = "";
                    else KAgent->Filter = "KTYPE = " + DirectTreeGRPID->AsString;
                    KAgent->Open();
                    break;*/


                case 2:
                    MatGridControl.DataSource = DB.SkladBase().GetMatList(focused_tree_node.Id == 6 ? -1 : focused_tree_node.GrpId, 0, 0, 0);
                    break;

           /*     case 3: Services->Open();
                    if (DirectTreeID->Value == 51) Services->Filter = "";
                    else Services->Filter = "GRPID = " + DirectTreeGRPID->AsString;
                    break;

                case 4: switch (DirectTreeID->Value)
                    {
                        case 25: cxGridLevel6->GridView = WarehouseGrid; break;
                        case 11: cxGridLevel6->GridView = BanksGrid; break;
                        case 2: cxGridLevel6->GridView = MeasuresGrid; break;
                        case 43: cxGridLevel6->GridView = CountriesGrid; break;
                        case 12: cxGridLevel6->GridView = AccountTypeGrid; break;
                        case 40: cxGridLevel6->GridView = PricetypesGrid; break;
                        case 102: cxGridLevel6->GridView = ChargetypeGrid; break;
                        case 64: cxGridLevel6->GridView = CashdesksGrid; break;
                        case 3: cxGridLevel6->GridView = CurrencyGrid; break;
                        case 53: SkladData->MatRecipe->Filter = "RTYPE = 1";
                            cxGridLevel6->GridView = MatRecipeGrid; break;
                        case 42: SkladData->MatRecipe->Filter = "RTYPE = 2";
                            cxGridLevel6->GridView = MatRecipeGrid; break;
                        case 68: cxGridLevel6->GridView = TaxesGrid; break;
                        case 112: cxGridLevel6->GridView = TechProcessGrid; break;
                    }
                    break;*/
            }
        }

        public class CustomMatList
        {
          public  int Num { get; set; }
          public int MatId { get; set; }
          public string Name { get; set; }
          public decimal Amount { get; set; }
          public decimal? Price { get; set; }
          public int WId { get; set; }
        }

        private void MatGridView_DoubleClick(object sender, EventArgs e)
        {
            var row = MatGridView.GetFocusedRow() as GetMatList_Result;

            if (isCatalog)
            {
                var t = (wb.Kagent != null ? wb.Kagent.PTypeId : null);
                var price = DB.SkladBase().GetListMatPrices(row.MatId, wb.CurrId).FirstOrDefault(w => w.PType == t);

                custom_mat_list.Add(new CustomMatList
                {
                    Num = custom_mat_list.Count() + 1,
                    MatId = row.MatId,
                    Name = row.Name,
                    Amount = 1,
                    Price = price != null ? price.Price : 0,
                    WId = row.WId != null ? row.WId.Value : DBHelper.WhList().FirstOrDefault(w => w.Def == 1).WId
                });

                MatListGridView.RefreshData();
            }
          /*  else if (isMatList)
            {
                resut = row;
            }*/
        }

        private void MatGridView_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            resut = MatGridView.GetFocusedRow();
        }
    }
}
