using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CompuLinERP.WIN.InventoryWebService;
using CompuLinINV.WIN;
using CompuLinINV.WIN.DTO;

namespace CompuLinERP.WIN
{
    public partial class TransferNote : Form
    {
        InventoryWebService.InventoryWebService webService = new InventoryWebService.InventoryWebService();
        USERINFO _user;
        DataTable _table;
        Dictionary<string, string> dictionaryPrice = new Dictionary<string, string>();
        Dictionary<string, string> dictionaryQty = new Dictionary<string, string>();
        string previousLabel = "";

        public TransferNote(USERINFO user)
        {
            _user = user;
            _table = CreateDataTable(); 
            InitializeComponent();
        }

        private DataTable CreateDataTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Item Code");
            dt.Columns.Add("Description");
            dt.Columns.Add("Loca");
            dt.Columns.Add("Unit");
            dt.Columns.Add("Sales Price");
            dt.Columns.Add("Qty");
            dt.Columns.Add("Discount Rate");
            dt.Columns.Add("Dis%");
            dt.Columns.Add("Discount Amt");
            dt.Columns.Add("Value");
            dt.Columns.Add("BUnit");
            dt.Columns.Add("LUnit");


            return dt;
        }

        private void TransferNote_Load(object sender, EventArgs e)
        {

        }

        private void groupBox4_Enter(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, System.EventArgs e)
        {
            TRANS_MAST transMast = new TRANS_MAST();

            transMast.COMPCODE = _user.COMPCODE;
            transMast.LOCA_FROM = txtLocaFrom.Text ;
            transMast.LOCA_TO = txtLocaTo.Text;
            transMast.NOTE01 = txtNote01.Text;
            transMast.TRANS_DATE = DateTime.Now;            
            transMast.TXN_DATE = DateTime.Now;
            transMast.TXN_TIME = DateTime.Now.ToShortTimeString();
            transMast.BRANCH_CODE = System.Configuration.ConfigurationManager.AppSettings.Get("BranchCode");
            transMast.PAY_STATUS = 0;
            transMast.PAY_IN_PROGRESS = 0;
            transMast.PRINTED = 0;
            transMast.USERCODE = _user.USERCODE;
            transMast.TOTAL = decimal.Parse(txtGrandTot.Text);            
            if (chkFullDiscount.Checked == false)
            {
                transMast.IS_DISCOUNT_PER = 0;
            }
            else
            {
                transMast.IS_DISCOUNT_PER = 1;
            }
            transMast.DISCOUNT = decimal.Parse(txtFullDiscount.Text);
            transMast.DISCOUNT_AMT = decimal.Parse(txtFullDiscountValue.Text);
            transMast.TOTAL_AFTER_DISCOUNT = decimal.Parse(txtAfterDiscount.Text);
            transMast.VAT_AMOUNT = decimal.Parse(txtVAT.Text);
            transMast.NBT_AMOUNT = decimal.Parse(txtNBT.Text);
            transMast.COST_AMOUNT = decimal.Parse(txtWithoutTax.Text);
            
            List<TRANS_DETAIL> transDetailsList = new List<TRANS_DETAIL>();

            foreach (DataGridViewRow item in dataGridView3.Rows)
            {

                if (item.Cells["Value"].Value != null && !String.IsNullOrEmpty(item.Cells["Value"].Value.ToString()))
                {
                    TRANS_DETAIL transDetail = new TRANS_DETAIL();

                    transDetail.COMPCODE = _user.COMPCODE;
                                        
                    if (item.Cells["Dis%"].Value.ToString() == "true")
                    {
                        transDetail.IS_DISCOUNT_PER = 1;
                    }
                    else
                    {
                        transDetail.IS_DISCOUNT_PER = 0;
                    }
                    transDetail.DISCOUNT_AMT = Decimal.Parse(item.Cells["Discount Amt"].Value.ToString());
                    transDetail.ITEM = item.Cells["Item Code"].Value.ToString();
                    transDetail.ITEM_DESC = item.Cells["Description"].Value.ToString();
                    transDetail.ITEM_LOCA = item.Cells["Loca"].Value.ToString();
                    transDetail.UNIT = item.Cells["Unit"].Value.ToString();
                    transDetail.QTY = Decimal.Parse(item.Cells["Qty"].Value.ToString());

                    if (item.Cells["Qty"].Value.ToString() == "")
                    {
                        transDetail.BULK_QTY = 0;
                        transDetail.BULK_SALES_PRICE = 0;
                        transDetail.BULK_UNIT = "";
                        transDetail.LOOSE_QTY = 0;
                        transDetail.LOOSE_SALES_PRICE = 0;
                        transDetail.LOOSE_UNIT = "";
                    }
                    else
                    {
                        if (item.Cells["Unit"].Value.ToString() == item.Cells["BUnit"].Value.ToString())
                        {
                            transDetail.BULK_QTY = Decimal.Parse(item.Cells["Qty"].Value.ToString());
                            transDetail.BULK_SALES_PRICE = Decimal.Parse(item.Cells["Sales Price"].Value.ToString());
                            transDetail.BULK_UNIT = item.Cells["Unit"].Value.ToString();
                            transDetail.LOOSE_QTY = 0;
                            transDetail.LOOSE_SALES_PRICE = 0;
                            transDetail.LOOSE_UNIT = "";
                        }
                        else
                        {
                            transDetail.BULK_QTY = 0;
                            transDetail.BULK_SALES_PRICE = 0;
                            transDetail.BULK_UNIT = "";
                            transDetail.LOOSE_QTY = Decimal.Parse(item.Cells["Qty"].Value.ToString());
                            transDetail.LOOSE_SALES_PRICE = Decimal.Parse(item.Cells["Sales Price"].Value.ToString());
                            transDetail.LOOSE_UNIT = item.Cells["Unit"].Value.ToString();
                        }
                    }

                    transDetail.TOTAL = Decimal.Parse(item.Cells["Value"].Value.ToString());
                    transDetail.LOCA_FROM = transMast.LOCA_FROM; // item.Cells["Loca"].Value.ToString(); 
                    transDetailsList.Add(transDetail);
                }
            }

            string documentNumber = webService.InsertTransMastDetails(transMast, transDetailsList.ToArray());
            txtTRNNo.Text = documentNumber;
            //DisplayPrintScreen form = new DisplayPrintScreen(txtInvoiceNo.Text,"Invoice.rpt","INV_NO");           
            //DialogResult dialogResult = form.ShowDialog();

            //string csh_crd = cmbINVType.Text.Substring(0, (cmbINVType.Text.IndexOf("-"))).Trim(); ;
            ////DisplayPrintScreen form = new DisplayPrintScreen(txtTRNNo.Text, "Trans.rpt", "TRANS_NO", txtLocaFrom.Text, "TRN", _user.COMPCODE, "", DateTime.Now, DateTime.Now);
            //DialogResult dialogResult = form.ShowDialog();

            txtINVNO_Display.Text = txtTRNNo.Text;
            txtLOCA_Display.Text = txtLocaFrom.Text;
            //cmbINVType.Focus();
            _table.Rows.Clear();
            //ClearInvoicedetails();
            //groupBox2.Enabled = false;
            //groupInvHeader.Enabled = true;
            //SetReadonlyControlsFalse(groupInvHeader.Controls);
            txtTRNNo.Text = "PENDING";
        }

        private void LoadTable()
        {
            string strBUnit = "";
            string strBPrice = "";
            string strBQty = "";

            string strLUnit = "";
            string strLPrice = "";
            string strLQty = "";
            double _discountAmt = 0;

            if (discount.Text == "")
            {
                discount.Text = "0";
            }

            if (checkBox1.Checked == true)
            {
                _discountAmt = ((double.Parse(txtPrice.Text) * double.Parse(txtQTY.Text)) * double.Parse(discount.Text)) / 100;
            }
            else
            {
                _discountAmt = double.Parse(discount.Text);
            }

            _discountAmt = Math.Round(_discountAmt, 2);

            _table.Rows.Add(itemcode.Text, description.Text, txtLocation.Text, cmbUnits.Text, txtPrice.Text, txtQTY.Text, discount.Text, checkBox1.Checked, _discountAmt, value.Text, lblBulkUnit.Text, lblLooseUnit.Text);

            dataGridView3.DataSource = _table;
            dataGridView3.Columns[10].Width = 0;
            dataGridView3.Columns[11].Width = 0;

        }

        private void MakePaymentCalculations()
        {
            double totalAmount = 0;
            foreach (DataGridViewRow item in dataGridView3.Rows)
            {
                if (item.Cells["Value"].Value != null && !String.IsNullOrEmpty(item.Cells["Value"].Value.ToString()))
                    totalAmount = totalAmount + double.Parse(item.Cells["Value"].Value.ToString());
            }

            txtSubTot.Text = totalAmount.ToString();
        }

        private void ClearMaindetails()
        {
            txtPrice.Text = "";
            description.Text = "";
            txtQTY.Text = "";
            discount.Text = "";
            value.Text = "";
            //lblCustomerType.Text = "";
            cmbUnits.Items.Clear();
            checkBox1.Checked = false;
        }
        private void value_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {



                if (value.Text == "")
                {
                    value.Text = "0";
                }
                else if (decimal.Parse(value.Text) > 0)
                {
                    LoadTable();
                    MakePaymentCalculations();
                    itemcode.Text = "";
                    ClearMaindetails();
                    itemcode.Focus();
                }
            }
        }

        private void txtLoca_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void txtLocaFrom_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                txtLocaFrom.BackColor = Color.White;
                if (e.KeyCode == Keys.F2)
                {

                    SearchCriteria search = new SearchCriteria();
                    search.Location = "";
                    search.TableName = "";
                    search.SequenceNo = 2;
                    search.CompanyCode = _user.COMPCODE;
                    search.SearchStartingCharacters = "H001";

                    HelpScreen form = new HelpScreen(search);
                    DialogResult dialogResult = form.ShowDialog();
                    txtLocaFrom.Text = form._fromHelpValue;
                    //location.Text = form._fromHelpValue;
                }
                else if (e.KeyCode == Keys.Enter && txtLocaFrom.Text.Trim() != "")
                {
                    //cmbSalesPriceTypes.Focus();
                }
                else if (e.KeyCode == Keys.Enter && txtLocaFrom.Text.Trim() == "")
                {
                    txtLocaFrom.BackColor = Color.Red;
                }

            }
            catch (Exception)
            {

                throw;
            }
        }

        private void txtLocaTo_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                txtLocaTo.BackColor = Color.White;
                if (e.KeyCode == Keys.F2)
                {

                    SearchCriteria search = new SearchCriteria();
                    search.Location = "";
                    search.TableName = "";
                    search.SequenceNo = 2;
                    search.CompanyCode = _user.COMPCODE;
                    search.SearchStartingCharacters = "H001";

                    HelpScreen form = new HelpScreen(search);
                    DialogResult dialogResult = form.ShowDialog();
                    txtLocaTo.Text = form._fromHelpValue;
                    //location.Text = form._fromHelpValue;
                }
                else if (e.KeyCode == Keys.Enter && txtLocaTo.Text.Trim() != "")
                {
                    //cmbSalesPriceTypes.Focus();
                }
                else if (e.KeyCode == Keys.Enter && txtLocaTo.Text.Trim() == "")
                {
                    txtLocaTo.BackColor = Color.Red;
                }

            }
            catch (Exception)
            {

                throw;
            }
        }

        private void itemcode_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.F2)
                {
                    //ClearMaindetails();

                    SearchCriteria search = new SearchCriteria();
                    search.Location = (txtLocation.Text);
                    search.SequenceNo = 1;
                    search.CompanyCode = _user.COMPCODE;
                    search.SearchStartingCharacters = itemcode.Text.Trim();
                    search.TableName = "ITEM_MAST";

                    HelpScreen form = new HelpScreen(search);
                    DialogResult dialogResult = form.ShowDialog();
                    itemcode.Text = form._fromHelpValue;
                    itemcode.Focus();

                }
                else if (e.KeyCode == Keys.Escape)
                {
                    txtFullDiscount.Focus();
                }
                else if (e.KeyCode == Keys.Enter && itemcode.Text.Trim() != "")
                {

                    //btnCut.Enabled = true;
                    //SetReadonlyControls(groupInvHeader.Controls);


                    ITEM_MAST searchdetails = new ITEM_MAST();
                    searchdetails.ITEM = itemcode.Text;
                    searchdetails.LOCA_CODE = txtLocation.Text;
                    searchdetails.COMPCODE = _user.COMPCODE;




                    ITEM_MAST[] ItemList = webService.GetAllItemDetailsByAllLocation(searchdetails);
                    //Create table and add data
                    // Add table to datagrid

                    ////dgStocks_in_allLocations.DataSource = ItemList;

                    ////dgStocks_in_allLocations.Columns["LOCA_CODE"].DisplayIndex = 0;
                    ////dgStocks_in_allLocations.Columns["DESC1"].DisplayIndex = 1;
                    ////dgStocks_in_allLocations.Columns["UNIT_1_QTY"].DisplayIndex = 2;
                    ////dgStocks_in_allLocations.Columns["UNIT_2_QTY"].DisplayIndex = 3;

                    ////dgStocks_in_allLocations.Columns["COMPCODE"].Visible = false;



                    ITEM_MAST details = webService.GetAllItemDetailsById(searchdetails);

                    if (details.ITEM == null)
                    {
                        MessageBox.Show("Item Cannot found in selected location !", "ITEM NOT FOUND", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        dictionaryPrice.Clear();
                        dictionaryQty.Clear();
                        //Calculate Sales Price 
                        //  Shanthi - According to the customer type
                        //  TCI - According to the sales type

                        //TODO 
                        //Handle global variables for Company Name, Comp code

                        lblBulkUnit.Text = details.UNIT.ToString();
                        lblLooseUnit.Text = details.ITEM_UNIT0.ToString();
                        //lblPack.Text = details.PACK.ToString();

                        if (details.SALES_PRICE_BULK == null)
                        {
                            details.SALES_PRICE_BULK = 0;
                        }
                        if (details.SALES_PRICE_LOOSE == null)
                        {
                            details.SALES_PRICE_LOOSE = 0;
                        }


                        dictionaryPrice.Add(details.UNIT.ToString(), calculateItemSalesPrice("SHA", "001", itemcode.Text , "CASH", decimal.Parse(details.SALES_PRICE_BULK.ToString())).ToString());
                        if (details.UNIT.ToString() != details.ITEM_UNIT0.ToString())
                        {
                            dictionaryPrice.Add(details.ITEM_UNIT0.ToString(), calculateItemSalesPrice("SHA", "001", itemcode.Text , "CASH", decimal.Parse(details.SALES_PRICE_LOOSE.ToString())).ToString());
                        }

                        dictionaryQty.Add(details.UNIT.ToString(), details.UNIT_1_QTY.ToString());
                        if (details.UNIT.ToString() != details.ITEM_UNIT0.ToString())
                        {
                            dictionaryQty.Add(details.ITEM_UNIT0.ToString(), details.UNIT_2_QTY.ToString());
                        }
                        cmbUnits.Items.Clear();
                        cmbUnits.Items.Add(details.UNIT.ToString());
                        cmbUnits.Items.Add(details.ITEM_UNIT0.ToString());
                        cmbUnits.SelectedIndex = 0;

                        description.Text = details.DESC1;

                        cmbUnits.Focus();
                    }
                }

            }
            catch (Exception)
            {

                throw;
            }

        }

        private decimal calculateItemSalesPrice(string CompName, string CompCode, string ItemCode, string CustType, decimal CurrentPrice)
        {
            decimal salesPriceofCurrentItem = 0;

            if (CompName == "SHA")
            {
                #region Shanthi
                if (CustType == "CASH")
                {
                    //This is to add VAT component to normal sales price. then the normal sales price will be high
                    //TODO - this 111 should be parameterized via TAX_MAST table
                    //salesPriceofCurrentItem = Math.Round((CurrentPrice/100) * 111 ,2);
                    salesPriceofCurrentItem = Math.Round(CurrentPrice, 2);
                }
                else
                {
                    //If it is a CREDIT customer, normal sales price will be shown. at the end of the invoice, VAT breakdown will be added.
                    salesPriceofCurrentItem = Math.Round(CurrentPrice, 2) - Math.Round((CurrentPrice / 111) * 11, 2);
                }

                #endregion
            }
            return salesPriceofCurrentItem;
        }

        private void itemcode_TextChanged(object sender, System.EventArgs e)
        {

        }

        private void cmbUnits_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            txtPrice.Text = dictionaryPrice[cmbUnits.Text.Trim()];
            txtAvailableQTY.Text = dictionaryQty[cmbUnits.Text.Trim()];
        }

        private void cmbUnits_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                //TODO 
                //Check whether this item is not inside the table.
                string _gridLoca = txtLocation.Text;
                string _gridItem = itemcode.Text;
                string _gridUnit = cmbUnits.Text;
                int _exists = 0;
                //Item Code

                foreach (DataGridViewRow item in dataGridView3.Rows)
                {
                    if (item.Cells["Loca"].Value.ToString().Trim() == _gridLoca.Trim() && item.Cells["Unit"].Value.ToString().Trim() == _gridUnit.Trim() && item.Cells["Item Code"].Value.ToString().Trim() == _gridItem.Trim())
                    {
                        MessageBox.Show("Item already selected in the grid. Try to EDIT that row.", "ITEM CODE ALREADY SELECTED", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        _exists = 1;
                        txtQTY.ReadOnly = true;
                    }
                }

                if (_exists == 0)
                {
                    txtQTY.ReadOnly = false;
                    txtQTY.Focus();
                }
            }

        }

        private void txtQTY_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && txtQTY.Text != "")
            {
                if (double.Parse(txtAvailableQTY.Text) >= double.Parse(txtQTY.Text))
                {
                    checkBox1.Focus();
                }
                else
                {
                    MessageBox.Show("Available QTY is less than requested QTY", "AVAILABLE QTY", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void discount_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                value.Focus();
            }
        }

        private void discount_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                if (!String.IsNullOrEmpty(txtPrice.Text) && !String.IsNullOrEmpty(txtQTY.Text))
                {

                    double totalAmount = (double.Parse(txtPrice.Text) * double.Parse(txtQTY.Text));
                    double discountAmount = 0;
                    if (!String.IsNullOrEmpty(discount.Text))
                    {
                        discountAmount = double.Parse(discount.Text);

                        double salesQtyAmount = double.Parse(discount.Text);

                        if (checkBox1.Checked)
                            discountAmount = ((totalAmount * salesQtyAmount) / 100);
                    }

                    totalAmount = totalAmount - discountAmount;
                    value.Text = totalAmount.ToString();

                }
            }
        }

        private void value_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void txtFullDiscount_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                //grpLogin.Visible = true;

                if (String.IsNullOrEmpty(txtFullDiscount.Text))
                {
                    txtFullDiscount.Text = "0";
                }


                double totalAmount = double.Parse(txtSubTot.Text);
                double discountAmount = double.Parse(txtFullDiscount.Text);

                if (chkFullDiscount.Checked)
                {
                    discountAmount = ((totalAmount * discountAmount) / 100);
                }


                txtFullDiscountValue.Text = discountAmount.ToString();
                txtAfterDiscount.Text = (totalAmount - discountAmount).ToString();


                //if (lblCustomerType.Text.Trim() != "CASH")
                //{
                    chkVAT.Checked = true;
                    //TODO - this 0.11 should be parameterized through TAX_MAST 
                    txtVAT.Text = (Math.Round(double.Parse(txtAfterDiscount.Text) * 0.11, 2)).ToString();
                    txtNBT.Text = "0";
                //}
                //else
                //{
                //    txtVAT.Text = "0";
                //    txtNBT.Text = "0";
                //}
                //txtDeductions.Text = "0";

                txtWithoutTax.Text = Math.Round(double.Parse(txtAfterDiscount.Text), 2).ToString();
                txtGrandTot.Text = (double.Parse(txtAfterDiscount.Text) + (double.Parse(txtVAT.Text) + double.Parse(txtNBT.Text))).ToString();
                txtGrandTot.Text = Math.Round(double.Parse(txtGrandTot.Text), 2).ToString();
                //payableAmount.Text = (double.Parse(txtGrandTot.Text) - double.Parse(txtDeductions.Text)).ToString();

                cmdPost.Enabled = true;


                //grpLogin.Visible = true;
                //txtINVUser.Text = _user.USERCODE;
                //txtINVUser.Focus();

            }
            else if (e.KeyCode == Keys.Escape)
            {

                if (txtGrandTot.Text != "")
                {
                    if (double.Parse(txtGrandTot.Text) > 0)
                    {
                        cmdPost.Focus();

                    }
                }

            }

        }

        private void button4_Click(object sender, System.EventArgs e)
        {
            ////DisplayPrintScreen form = new DisplayPrintScreen(txtINVNO_Display.Text, "TransferNote.rpt", "TRN_NO", txtLOCA_Display.Text, "TRN", _user.COMPCODE, "", DateTime.Now, DateTime.Now);
            //DialogResult dialogResult = form.ShowDialog();
        }

        private void value_TextChanged(object sender, System.EventArgs e)
        {

        }

        private void txtLocaFrom_TextChanged(object sender, System.EventArgs e)
        {

        }

        private void txtLocationNew_TextChanged(object sender, System.EventArgs e)
        {

        }

        private void txtLocation_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                txtLocation.BackColor = Color.White;
                if (e.KeyCode == Keys.F2)
                {

                    SearchCriteria search = new SearchCriteria();
                    search.Location = "";
                    search.TableName = "";
                    search.SequenceNo = 2;
                    search.CompanyCode = _user.COMPCODE;
                    search.SearchStartingCharacters = "H001";

                    HelpScreen form = new HelpScreen(search);
                    DialogResult dialogResult = form.ShowDialog();
                    txtLocation.Text = form._fromHelpValue;
                    //location.Text = form._fromHelpValue;
                }
                else if (e.KeyCode == Keys.Enter && txtLocation.Text.Trim() != "")
                {
                    //cmbSalesPriceTypes.Focus();
                }
                else if (e.KeyCode == Keys.Enter && txtLocation.Text.Trim() == "")
                {
                    txtLocation.BackColor = Color.Red;
                }

            }
            catch (Exception)
            {

                throw;
            }
        }

        private void dataGridView3_DoubleClick(object sender, System.EventArgs e)
        {
            DataRow dr = _table.Rows[dataGridView3.CurrentRow.Index];
            itemcode.Text = dr.ItemArray[0].ToString();
            description.Text = dr.ItemArray[1].ToString();
            txtLocation.Text = dr.ItemArray[2].ToString();
            txtPrice.Text = "";
            txtQTY.Text = "";
            discount.Text = "";
            value.Text = "";
            cmbUnits.Items.Clear();
            checkBox1.Checked = false;
            _table.Rows.Remove(dr);
            dataGridView3.DataSource = _table;
            MakePaymentCalculations();
            itemcode.Focus();
        }

        private void dataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

    }
}
