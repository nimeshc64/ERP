using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    public partial class CutItem : Form
    {
        InventoryWebService.InventoryWebService webService = new InventoryWebService.InventoryWebService();
        USERINFO _user;
        DataTable _table;

        public CutItem(USERINFO user)
        {
            InitializeComponent();
            _user = user;
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void CutItem_Load(object sender, System.EventArgs e)
        {
            LoadLocations();
        }

        private DataTable CreateDataTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Location");
            dt.Columns.Add("Item");
            dt.Columns.Add("ItemDesc");
            dt.Columns.Add("BulkPrice");
            dt.Columns.Add("loosePrice");
            dt.Columns.Add("Length");
            dt.Columns.Add("LengthUnit");
            dt.Columns.Add("Width");
            dt.Columns.Add("WidthUnit");
            dt.Columns.Add("CutQty");

           

            return dt;
        }

        private void LoadLocations()
        {
                locationComboBox.Items.Clear();
                locationComboBox.ResetText();
                LOCA_MAST[] catdetails = webService.GetAllLocationDetails(_user.COMPCODE);
                foreach (LOCA_MAST item in catdetails)
                {
                    locationComboBox.Items.Add(item.LOCA_CODE + " - " + item.LOCA_DESCRIPT);
                }
                locationComboBox.SelectedIndex = 0;
        }

        private void txtItemCode_TextChanged(object sender, System.EventArgs e)
        {

        }

        private void txtItemCode_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.F2)
                {
                    txtItemCode.Text = "";


                    SearchCriteria search = new SearchCriteria();
                    search.Location = (locationComboBox.SelectedItem.ToString().Split(new string[] { " - " }, StringSplitOptions.None)[0]); ; ;
                    search.SequenceNo = 1;
                    search.CompanyCode = _user.COMPCODE;
                    search.SearchStartingCharacters = txtItemCode.Text.Trim();
                    search.TableName = "ITEM_MAST";

                    HelpScreen form = new HelpScreen(search);
                    DialogResult dialogResult = form.ShowDialog();

                    txtItemCode.Text = form._fromHelpValue;
                    txtItemCode.Focus();

                }
                else if (e.KeyCode == Keys.Enter)
                {
                    //LId.Text = txtItemCode.Text.Trim();
                    //txtItemCode.Text = (locationComboBox.SelectedItem.ToString().Split(new string[] { " - " }, StringSplitOptions.None)[0]); ;

                    FillData();
                }
            }
            catch (Exception)
            {

                // throw;
                MessageBox.Show("Not Found");
            }

        }

        private void FillData()
        {
            ITEM_MAST searchdetails = new ITEM_MAST();
            searchdetails.ITEM = txtItemCode.Text;
            searchdetails.LOCA_CODE =  locationComboBox.Text.Substring(0, (locationComboBox.Text.IndexOf("-"))).Trim();;
            searchdetails.COMPCODE = _user.COMPCODE;

            ITEM_MAST details = webService.GetAllItemDetailsById(searchdetails);
            
            txtItemDesc.Text = details.DESC1;
            
            //packsize.Text = details.PACK.ToString();
            lblwidth.Text = details.WIDTH.ToString();
            lblLength.Text = details.LENGTH.ToString();
            lblwidthUnit.Text = details.ITEM_UNIT2.ToString();
            lblLengthUnit.Text = details.ITEM_UNIT1.ToString();

            ITEM_CUTLIST searchList = new ITEM_CUTLIST();
            searchList.COMPCODE = _user.COMPCODE;
            searchList.LOCA_CODE = locationComboBox.Text.Substring(0, (locationComboBox.Text.IndexOf("-"))).Trim(); ;
            searchList.ITEM = txtItemCode.Text;

            List<ITEM_CUTLIST> custList = webService.GetAllItemCustList(searchList).ToList() ;
            _table = CreateDataTable();


            //dgCutItems.Rows.Clear();
            dgCutItems.DataSource = null;

            int rowIndex = 0;
            foreach (ITEM_CUTLIST cutitem in custList)
            {
                ITEM_MAST searCutItem = new ITEM_MAST();
                searCutItem.COMPCODE = cutitem.COMPCODE;
                searCutItem.LOCA_CODE = cutitem.LOCA_CODE;
                searCutItem.ITEM = cutitem.CUT_ITEM;

                ITEM_MAST itemMast = webService.GetAllItemDetailsById(searCutItem);

                _table.Rows.Add(itemMast.LOCA_CODE, itemMast.ITEM, itemMast.DESC1,  itemMast.SALES_PRICE_BULK, itemMast.SALES_PRICE_LOOSE , itemMast.LENGTH, itemMast.ITEM_UNIT1, itemMast.WIDTH, itemMast.ITEM_UNIT2, "0");
                rowIndex++;
            }

            lblTotSQA.Text = (double.Parse(lblLength.Text) * double.Parse(lblwidth.Text)).ToString();
            lblTotBalSQA.Text = lblTotSQA.Text;

            dgCutItems.DataSource = _table;
            dgCutItems.Columns["Location"].Width = 0;
            dgCutItems.Columns["Item"].Width = 180;
            dgCutItems.Columns["ItemDesc"].Width = 230;
            dgCutItems.Columns["BulkPrice"].Width = 50;
            dgCutItems.Columns["loosePrice"].Width = 50;
            dgCutItems.Columns["Length"].Width = 50;
            dgCutItems.Columns["LengthUnit"].Width = 50;
            dgCutItems.Columns["Width"].Width = 50;
            dgCutItems.Columns["WidthUnit"].Width = 50;
            dgCutItems.Columns["CutQty"].Width = 50;

            dgCutItems.Columns["Location"].ReadOnly=true ;
            dgCutItems.Columns["Item"].ReadOnly=true ;
            dgCutItems.Columns["ItemDesc"].ReadOnly = true;
            dgCutItems.Columns["BulkPrice"].ReadOnly = true;
            dgCutItems.Columns["loosePrice"].ReadOnly = true;
            dgCutItems.Columns["Length"].ReadOnly = true;
            dgCutItems.Columns["LengthUnit"].ReadOnly = true;
            dgCutItems.Columns["Width"].ReadOnly = true;
            dgCutItems.Columns["WidthUnit"].ReadOnly = true;



        }
        private void ClearFields()
        {
            txtItemDesc.Text = "";
            lblwidth.Text = "";
            lblwidthUnit.Text = "";
            lblLength.Text = "";
            lblLengthUnit.Text = "";
            dgCutItems.DataSource = null;
            btnNext.Visible = false;
            btnCut.Visible = false;
        }

        private void dgCutItems_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            //Check Units of all items
            double SQF_ItemWiseTotal = 0;
            double SQF_MainItem = 0;
            double SQF_ThisItem = 0;
            double itemLength = 0;
            double itemWidth = 0;
            double itemCutQty = 0;
            int isValid = 1;

            foreach (DataGridViewRow item in dgCutItems.Rows)
            {
                if  (!String.IsNullOrEmpty(item.Cells["LengthUnit"].Value.ToString()))
                {
                if (item.Cells["LengthUnit"].Value.ToString() != lblLengthUnit.Text.Trim() || item.Cells["WidthUnit"].Value.ToString() != lblwidthUnit.Text.Trim())
                {
                    isValid = 0;
                }
                }
            }

            if (isValid == 0)
            {
                MessageBox.Show("Item UNITS should be same for all items", "UNIT NOT EQUAL", MessageBoxButtons.OK, MessageBoxIcon.Error);
                FillData();
            }
            else
            {
                SQF_MainItem = double.Parse(lblLength.Text) * double.Parse(lblwidth.Text);

                foreach (DataGridViewRow item in dgCutItems.Rows)
                {
                    if (item.Cells["CutQty"].Value != null && !String.IsNullOrEmpty(item.Cells["CutQty"].Value.ToString()))
                    {
                        itemLength = double.Parse(item.Cells["Length"].Value.ToString());
                        itemWidth = double.Parse(item.Cells["Width"].Value.ToString());
                        itemCutQty = double.Parse(item.Cells["CutQty"].Value.ToString());

                        SQF_ThisItem = (itemLength * itemWidth) * itemCutQty;
                        SQF_ItemWiseTotal = SQF_ItemWiseTotal + SQF_ThisItem;
                    }
                }

                // Calculate Balance SqureArea
                if (SQF_MainItem != SQF_ItemWiseTotal)
                {
                    lblTotBalSQA.Text = (double.Parse(lblTotSQA.Text.ToString()) - SQF_ItemWiseTotal).ToString();
                }
                else
                {
                    lblTotBalSQA.Text = (double.Parse(lblTotSQA.Text.ToString()) - SQF_ItemWiseTotal).ToString();
                }

                if (double.Parse(lblTotBalSQA.Text.ToString()) < 0)
                {
                    lblTotBalSQA.ForeColor  = Color.Red;
                    btnNext.Visible = false;
                }
                else if (double.Parse(lblTotBalSQA.Text.ToString()) == 0)
                {
                    lblTotBalSQA.ForeColor = Color.Black;
                    btnNext.Visible = true;
                }
                else
                {
                    lblTotBalSQA.ForeColor = Color.Black;
                    btnNext.Visible = false;
                }

            }
        }

 
        private void txtItemCode_Click(object sender, System.EventArgs e)
        {
            ClearFields();
        }

        private void btnNext_Click(object sender, System.EventArgs e)
        {
            txtItemCode.ReadOnly = true;
            btnCut.Visible = true;

        }

        private void txtAdditem_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.F2)
                {
                    txtAdditem.Text = "";

                    SearchCriteria search = new SearchCriteria();
                    search.Location = (locationComboBox.SelectedItem.ToString().Split(new string[] { " - " }, StringSplitOptions.None)[0]); ; ;
                    search.SequenceNo = 1;
                    search.CompanyCode = _user.COMPCODE;
                    search.SearchStartingCharacters = txtAdditem.Text.Trim();
                    search.TableName = "ITEM_MAST";

                    HelpScreen form = new HelpScreen(search);
                    DialogResult dialogResult = form.ShowDialog();

                    txtAdditem.Text = form._fromHelpValue;
                    txtAdditem.Focus();

                }
                else if (e.KeyCode == Keys.Enter)
                {
                    //LId.Text = txtItemCode.Text.Trim();
                    //txtItemCode.Text = (locationComboBox.SelectedItem.ToString().Split(new string[] { " - " }, StringSplitOptions.None)[0]); ;

                    //Add to DB
                    FillData();
                    
                }
            }
            catch (Exception)
            {

                // throw;
                MessageBox.Show("Not Found");
            }
        }

        private void cmdAdd_Click(object sender, System.EventArgs e)
        {
            ITEM_CUTLIST addItem = new ITEM_CUTLIST();
            addItem.COMPCODE = _user.COMPCODE;
            addItem.LOCA_CODE = locationComboBox.Text.Substring(0, (locationComboBox.Text.IndexOf("-"))).Trim(); 
            addItem.ITEM = txtItemCode.Text;
            addItem.CUT_ITEM = txtAdditem.Text;
            bool itemAddStatus = webService.InsertCutItems(addItem);

            FillData();

        }

        private void dgCutItems_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void button1_Click(object sender, System.EventArgs e)
        {
            ITEM_CUTLIST addItem = new ITEM_CUTLIST();
            addItem.COMPCODE = _user.COMPCODE;
            addItem.LOCA_CODE = locationComboBox.Text.Substring(0, (locationComboBox.Text.IndexOf("-"))).Trim();
            addItem.ITEM = txtItemCode.Text;
            addItem.CUT_ITEM = txtAdditem.Text;
            bool itemAddStatus = webService.DeleteCutItems(addItem);
            FillData();
        }


        
    }
}
