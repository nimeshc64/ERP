using CompuLinERP.WIN.InventoryWebService;
using CompuLinINV.WIN.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CompuLinERP.WIN
{
    public partial class PhysicalStock : Form
    {
        InventoryWebService.InventoryWebService webService = new InventoryWebService.InventoryWebService();
        USERINFO _user;
        DataTable _table;

        public PhysicalStock(USERINFO user)
        {
            InitializeComponent();
            _user = user;
        }

        private void StockAdjustments_Load(object sender, EventArgs e)
        {
            loadLocationsCombo();
        }

        private void DGPhysical_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void loadLocationsCombo()
        {
            textBox1.Text = "PENDING";
            DGPhysical.DataSource = null;
            txtFromItem.Text = "";
            txtToItem.Text = "";
            cmbLoca.Items.Clear();
            cmbLoca.ResetText();            
            
            LOCA_MAST[] catdetails = webService.GetAllLocationDetails(_user.COMPCODE);
            foreach (LOCA_MAST item in catdetails)
            {
                cmbLoca.Items.Add(item.LOCA_CODE + " - " + item.LOCA_DESCRIPT);
                cmbLoca.SelectedIndex = 0;
            }

        }

        private void txtToItem_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.F2  )
                {

                    SearchCriteria search = new SearchCriteria();
                    search.Location = (cmbLoca.SelectedItem.ToString().Split(new string[] { " - " }, StringSplitOptions.None)[0]);
                    search.SequenceNo = 1;
                    search.CompanyCode = _user.COMPCODE;
                    search.SearchStartingCharacters = txtToItem.Text.Trim();
                    search.TableName = "ITEM_MAST";

                    HelpScreen form = new HelpScreen(search);
                    DialogResult dialogResult = form.ShowDialog();
                    txtToItem.Text = form._fromHelpValue;
                    txtToItem.Focus();

                    //SearchCriteria search = new SearchCriteria();
                    //search.Location =  (cmbLoca.SelectedItem.ToString().Split(new string[] { " - " }, StringSplitOptions.None)[0]);
                    //search.SequenceNo=1;
                    //search.CompanyCode = _user.COMPCODE;

                    //HelpScreen form = new HelpScreen(search);
                    //DialogResult dialogResult = form.ShowDialog();
                    //txtToItem.Text = form._fromHelpValue;
                    
                }
                
            }
            catch (Exception)
            {

                throw;
            }

        }

        private void txtFromItem_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.F2)
                {

                    SearchCriteria search = new SearchCriteria();
                    search.Location = (cmbLoca.SelectedItem.ToString().Split(new string[] { " - " }, StringSplitOptions.None)[0]);
                    search.SequenceNo = 1;
                    search.CompanyCode = _user.COMPCODE;
                    search.SearchStartingCharacters = txtFromItem.Text.Trim();
                    search.TableName = "ITEM_MAST";

                    HelpScreen form = new HelpScreen(search);
                    DialogResult dialogResult = form.ShowDialog();
                    txtFromItem.Text = form._fromHelpValue;
                    txtFromItem.Focus();

                    
                    //SearchCriteria search = new SearchCriteria();
                    //search.Location = (cmbLoca.SelectedItem.ToString().Split(new string[] { " - " }, StringSplitOptions.None)[0]);
                    //search.SequenceNo = 1;
                    //search.CompanyCode = _user.COMPCODE;

                    //HelpScreen form = new HelpScreen(search); DialogResult dialogResult = form.ShowDialog();
                    //txtFromItem.Text = form._fromHelpValue;
                    
                }
                else if (e.KeyCode == Keys.Enter)
                {
                    txtToItem.Focus();                  
                }
            }
            catch (Exception)
            {

                throw;
            }

        }

        private void LoadItems(ITEM_MAST[] itemList)
        {
            DGPhysical.DataSource = null;
            DGPhysical.DataSource = itemList;
            DGPhysical.Columns.Add("BulkQty", "Bulk Qty");
            DGPhysical.Columns.Add("LooseQty", "Loose Qty");
            
            foreach (DataGridViewColumn item in DGPhysical.Columns)
            {
                item.Visible = false;
            }            

            DGPhysical.Columns["ITEM"].DisplayIndex = 0;
            DGPhysical.Columns["ITEM"].Visible = true;
            DGPhysical.Columns["DESC1"].DisplayIndex = 1;
            DGPhysical.Columns["DESC1"].Visible = true;
            DGPhysical.Columns["PACK"].DisplayIndex = 2;
            DGPhysical.Columns["PACK"].Visible = true;
            DGPhysical.Columns["UNIT"].DisplayIndex = 3;
            DGPhysical.Columns["UNIT"].Visible = true;
            DGPhysical.Columns["ITEM_UNIT0"].DisplayIndex = 4;
            DGPhysical.Columns["ITEM_UNIT0"].Visible = true;
            DGPhysical.Columns["BulkQty"].DisplayIndex = 5;
            DGPhysical.Columns["BulkQty"].Visible = true;
            DGPhysical.Columns["LooseQty"].DisplayIndex =6;
            DGPhysical.Columns["LooseQty"].Visible = true;

            DGPhysical.Columns["ITEM"].ReadOnly = true;
            DGPhysical.Columns["DESC1"].ReadOnly = true;
            DGPhysical.Columns["PACK"].ReadOnly = true;
            DGPhysical.Columns["UNIT"].ReadOnly = true;
            DGPhysical.Columns["ITEM_UNIT0"].ReadOnly = true;
        }

        private void cmdPOST_Click(object sender, EventArgs e)
        {
            if (DGPhysical.Rows.Count > 0)
            {
                List<PHY_MAST> PHY_MAST_List = new List<PHY_MAST>();

                for (int i = 0; i < DGPhysical.Rows.Count; i++)
                {
                    if (DGPhysical.Rows[i].Cells[0].Value != null)
                    {
                        PHY_MAST details = new PHY_MAST();

                        details.ITEM = Convert.ToString(DGPhysical.Rows[i].Cells["ITEM"].Value);
                        if (DGPhysical.Rows[i].Cells["BulkQty"].Value == null)
                        {
                            details.BULK_QTY =0;
                        }
                        else
                        {
                            details.BULK_QTY = Decimal.Parse(DGPhysical.Rows[i].Cells["BulkQty"].Value.ToString());
                        }

                        if (DGPhysical.Rows[i].Cells["LooseQty"].Value == null)
                        {
                            details.LOOSE_QTY = 0;
                        }
                        else
                        {
                            details.LOOSE_QTY = Decimal.Parse(DGPhysical.Rows[i].Cells["LooseQty"].Value.ToString());
                        }
                        details.COMPCODE = _user.COMPCODE;
                        details.LOCA = (cmbLoca.SelectedItem.ToString().Split(new string[] { " - " }, StringSplitOptions.None)[0]);
                        details.USERCODE = _user.USERCODE;
                        details.PHY_DATE = dateTimePicker1.Value;
                        details.PHY_TIME = DateTime.Now.TimeOfDay.ToString();
                        
                        PHY_MAST_List.Add(details);
                    }

                }

                //11AUG  string docNo = webService.(PHY_MAST_List.ToArray(), textBox1.Text);
                //11AUG  textBox1.Text = docNo;                
                MessageBox.Show("OK");
            }
            else
            {
                MessageBox.Show("Please fill the mandatory information.");
            }
        }

        private void txtFromItem_KeyPress(object sender, KeyPressEventArgs e)
        {
            txtToItem.Text = txtFromItem.Text;
        }

        private void txtToItem_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                if (!String.IsNullOrEmpty(txtToItem.Text) && !String.IsNullOrEmpty(txtFromItem.Text))
                {
                    ITEM_MAST searchdetails = new ITEM_MAST();
                    searchdetails.COMPCODE = _user.COMPCODE;
                    searchdetails.LOCA_CODE = (cmbLoca.SelectedItem.ToString().Split(new string[] { " - " }, StringSplitOptions.None)[0]);

                    ITEM_MAST[] itemList = webService.GetAllDetailsByItemOrder(searchdetails, txtFromItem.Text, txtToItem.Text);
                    LoadItems(itemList);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            loadLocationsCombo();
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void txtFromItem_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtToItem_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string location = cmbLoca.Text.Substring(0, (cmbLoca.Text.IndexOf("-"))).Trim();
            NETWORK[] company_name = webService.GetCompanyDetailsById(_user.COMPCODE);
            CompuLinINV.WIN.InvoiceReportDisplay form = new CompuLinINV.WIN.InvoiceReportDisplay("0", location, "", "PHY_ALL", "", _user.COMPCODE, company_name[0].CompName, DateTime.Now, DateTime.Now);
            //DisplayPrintScreen form = new DisplayPrintScreen("0", "PhysicalEntryList.rpt", "PHY_ALL", "", location);
            form.Refresh();
            DialogResult dialogResult = form.ShowDialog();
        }
    }
}
