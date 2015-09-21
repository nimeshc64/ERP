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
    public partial class CostPrice : Form
    {
        InventoryWebService.InventoryWebService webService = new InventoryWebService.InventoryWebService();
        USERINFO _user;
        DataTable _table;

        public CostPrice(USERINFO user)
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
                    search.Location =  (cmbLoca.SelectedItem.ToString().Split(new string[] { " - " }, StringSplitOptions.None)[0]);
                    search.SequenceNo=1;
                    search.CompanyCode = _user.COMPCODE;

                    HelpScreen form = new HelpScreen(search);
                    DialogResult dialogResult = form.ShowDialog();
                    txtToItem.Text = form._fromHelpValue;
                    
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

                    HelpScreen form = new HelpScreen(search); DialogResult dialogResult = form.ShowDialog();
                    txtFromItem.Text = form._fromHelpValue;
                    
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
            DGPhysical.Columns.Add("COST_PRICE_BULK", "COST_PRICE_BULK");
            DGPhysical.Columns.Add("COST_PRICE_LOOSE", "COST_PRICE_LOOSE");
            
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
            DGPhysical.Columns["COST_PRICE_BULK"].DisplayIndex = 5;
            DGPhysical.Columns["COST_PRICE_BULK"].Visible = true;
            DGPhysical.Columns["COST_PRICE_LOOSE"].DisplayIndex = 6;
            DGPhysical.Columns["COST_PRICE_LOOSE"].Visible = true;

            DGPhysical.Columns["ITEM"].ReadOnly = true;
            DGPhysical.Columns["DESC1"].ReadOnly = true;
            DGPhysical.Columns["PACK"].ReadOnly = true;
            DGPhysical.Columns["UNIT"].ReadOnly = true;
            DGPhysical.Columns["ITEM_UNIT0"].ReadOnly = true;
        }

        private void cmdPOST_Click(object sender, EventArgs e)
        {
            if ( DGPhysical.Rows.Count > 0)
            {
                List<ITEM_MAST> ITEM_MAST_List = new List<ITEM_MAST>();

                for (int i = 0; i < DGPhysical.Rows.Count; i++)
                {
                    if (DGPhysical.Rows[i].Cells[0].Value != null)
                    {
                        ITEM_MAST details = new ITEM_MAST();

                        details.ITEM = Convert.ToString(DGPhysical.Rows[i].Cells["ITEM"].Value);
                        details.COST_PRICE_BULK = Decimal.Parse(DGPhysical.Rows[i].Cells["COST_PRICE_BULK"].Value.ToString());
                        details.COST_PRICE_LOOSE = Decimal.Parse(DGPhysical.Rows[i].Cells["COST_PRICE_LOOSE"].Value.ToString());
                        details.COMPCODE = _user.COMPCODE;
                        details.LOCA_CODE = (cmbLoca.SelectedItem.ToString().Split(new string[] { " - " }, StringSplitOptions.None)[0]);
                        details.LAST_CHANGE_DATE = dateTimePicker1.Value;
                        
                        ITEM_MAST_List.Add(details);
                    }

                }

                bool isSuccess = webService.UpdateItemsDetailsByColumn(ITEM_MAST_List.ToArray(), 3);

                if (isSuccess)
                    MessageBox.Show("Information is successfully added.");
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
    }
}
