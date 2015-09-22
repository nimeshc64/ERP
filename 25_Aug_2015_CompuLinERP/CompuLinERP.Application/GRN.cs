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
    public partial class GRN : Form
    {
        InventoryWebService.InventoryWebService webService = new InventoryWebService.InventoryWebService();
        USERINFO _user;
        DataTable _table;
        Dictionary<string, string> dictionaryPrice = new Dictionary<string, string>();
        Dictionary<string, string> dictionaryQty = new Dictionary<string, string>();
        string previousLabel = "";

        public GRN(USERINFO user)
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
        private void GRN_Load(object sender, EventArgs e)
        {
            txtLoca.Focus();

        }

        private void txtLoca_TextChanged(object sender, EventArgs e)
        {

        }
        private void txtLoca_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                txtLoca.BackColor = Color.White;
                if (e.KeyCode == Keys.F2)
                {
                    SearchCriteria search = new SearchCriteria();
                    search.Location = (location.Text);
                    search.SequenceNo = 5;
                    search.CompanyCode = _user.COMPCODE;
                    search.SearchStartingCharacters =_user.USERCODE;

                    HelpScreen form = new HelpScreen(search);
                    DialogResult dialogResult = form.ShowDialog();
                    txtLoca.Text = form._fromHelpValue;
                    location.Text = form._fromHelpValue;
                }
                else if (e.KeyCode == Keys.Enter && txtLoca.Text.Trim() != "")
                {
                    txtSuppCode.Focus();
                }
                else if (e.KeyCode == Keys.Enter && txtLoca.Text.Trim() == "")
                {
                    txtLoca.BackColor = Color.Red;
                }

            }
            catch (Exception)
            {

                throw;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtSuppCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                SearchCriteria search = new SearchCriteria();
                    search.Location = (txtSuppCode.Text);
                search.SequenceNo = 4;
                search.CompanyCode = _user.COMPCODE;
                search.SearchStartingCharacters = txtSuppCode.Text;

                HelpScreen form = new HelpScreen(search);
                DialogResult dialogResult = form.ShowDialog();
                txtSuppCode.Text = form._fromHelpValue;
                


            }
            else if (e.KeyCode == Keys.Enter && txtSuppCode.Text != "")
            {
                SUPP_MAST supDetails = new SUPP_MAST();
                supDetails = webService.GetAllSupplierDetailsById(txtSuppCode.Text, _user.COMPCODE );
                if (supDetails.SUPP_CODE !="")
                {
                    txtSuppName.Text = supDetails.SUPP_NAME; 
                }
                txtSuppName.Focus();

            }
            else if (e.KeyCode == Keys.Enter && txtSuppCode.Text.Trim() == "")
            {
                txtSuppCode.BackColor = Color.Red;
            }
        }

        private void txtSuppCode_TextChanged(object sender, EventArgs e)
        {

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
        private void ClearMaindetails()
        {
            txtPrice.Text = "";
            description.Text = "";
            txtQTY.Text = "";
            discount.Text = "";
            value.Text = "";
            txtpurchase.Text = "";
            cmbUnits.Items.Clear();
            checkBox1.Checked = false;
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
                _discountAmt = ((double.Parse(txtpurchase.Text) * double.Parse(txtQTY.Text)) * double.Parse(discount.Text)) / 100;
            }
            else
            {
                _discountAmt = double.Parse(discount.Text);
            }

            _discountAmt = Math.Round(_discountAmt, 2);

            //_table.Rows.Add(itemcode.Text, description.Text, location.Text, cmbUnits.Text, txtpurchase.Text, txtQTY.Text, discount.Text, checkBox1.Checked, _discountAmt, value.Text, lblBulkUnit.Text, lblLooseUnit.Text);
            //_table.Rows.Add(location.Text, itemcode.Text,txtpurchase.Text,value.Text);
            _table.Rows.Add(itemcode.Text, description.Text, location.Text, cmbUnits.Text, txtpurchase.Text, txtQTY.Text, discount.Text, checkBox1.Checked, _discountAmt, value.Text, lblBulkUnit.Text, lblLooseUnit.Text);
           

            dataGridView3.DataSource = _table;
            //dataGridView3.Columns[10].Width = 0;
            //dataGridView3.Columns[11].Width = 0;

        }

        //itemcode_keyPress
        private void itemcode_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.F2)
                {
                    //ClearMaindetails();

                    //SearchCriteria search = new SearchCriteria();
                    //search.Location = (location.Text);
                    //search.SequenceNo = 1;
                    //search.CompanyCode = _user.COMPCODE;
                    //search.SearchStartingCharacters = itemcode.Text.Trim();

                    //HelpScreen form = new HelpScreen(search);
                    //DialogResult dialogResult = form.ShowDialog();
                    //itemcode.Text = form._fromHelpValue;
                    ////description.Text = form._fromHelpValue2;
                    ////string aa=form._fromHelpValue2;
                    //itemcode.Focus();

                    SearchCriteria search = new SearchCriteria();
                    search.Location = (location.Text);
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
                    
                   // SetReadonlyControls(groupInvHeader.Controls);


                    ITEM_MAST searchdetails = new ITEM_MAST();
                    searchdetails.ITEM = itemcode.Text;
                    searchdetails.LOCA_CODE = location.Text;
                    searchdetails.COMPCODE = _user.COMPCODE;




                   // ITEM_MAST[] ItemList = webService.GetAllItemDetailsByAllLocation(searchdetails);
                    //Create table and add data
                    // Add table to datagrid

                    //dgStocks_in_allLocations.DataSource = ItemList;

                    //dgStocks_in_allLocations.Columns["LOCA_CODE"].DisplayIndex = 0;
                    //dgStocks_in_allLocations.Columns["DESC1"].DisplayIndex = 1;
                    //dgStocks_in_allLocations.Columns["UNIT_1_QTY"].DisplayIndex = 2;
                    //dgStocks_in_allLocations.Columns["UNIT_2_QTY"].DisplayIndex = 3;

                    //dgStocks_in_allLocations.Columns["COMPCODE"].Visible = false;



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

                        if (details.SALES_PRICE_BULK == null)
                        {
                            details.SALES_PRICE_BULK = 0;
                        }
                        if (details.SALES_PRICE_LOOSE == null)
                        {
                            details.SALES_PRICE_LOOSE = 0;
                        }


                        dictionaryPrice.Add(details.UNIT.ToString(), calculateItemSalesPrice("SHA", "001", "", "", decimal.Parse(details.SALES_PRICE_BULK.ToString())).ToString());
                        if (details.UNIT.ToString() != details.ITEM_UNIT0.ToString())
                        {
                            dictionaryPrice.Add(details.ITEM_UNIT0.ToString(), calculateItemSalesPrice("SHA", "001", "", "", decimal.Parse(details.SALES_PRICE_LOOSE.ToString())).ToString());
                        }

                        dictionaryQty.Add(details.UNIT.ToString(), details.UNIT_1_QTY.ToString());
                        if (details.UNIT.ToString() != details.ITEM_UNIT0.ToString())
                        {
                            dictionaryQty.Add(details.ITEM_UNIT0.ToString(), details.UNIT_2_QTY.ToString());
                        }
                        cmbUnits.Items.Clear();
                        cmbUnits.Items.Add(details.UNIT.ToString());
                        //cmbUnits.Items.Add(details.ITEM_UNIT0.ToString());
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
                    salesPriceofCurrentItem = Math.Round((CurrentPrice / 100) * 111, 2);
                }
                else
                {
                    //If it is a CREDIT customer, normal sales price will be shown. at the end of the invoice, VAT breakdown will be added.
                    salesPriceofCurrentItem = Math.Round(CurrentPrice, 2);
                }

                #endregion
            }
            return salesPriceofCurrentItem;
        }


        private void SetReadonlyControls(Control.ControlCollection controlCollection)
        {
            if (controlCollection == null)
            {
                return;
            }

            foreach (TextBoxBase c in controlCollection.OfType<TextBoxBase>())
            {
                c.ReadOnly = true;
            }
        }


        private void location_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.F2)
                {
                    SearchCriteria search = new SearchCriteria();
                    search.Location = (location.Text);
                    search.SequenceNo = 2;
                    search.CompanyCode = _user.COMPCODE;

                    HelpScreen form = new HelpScreen(search);
                    DialogResult dialogResult = form.ShowDialog();
                    location.Text = form._fromHelpValue;

                }
                else if (e.KeyCode == Keys.Enter)
                {
                    itemcode.Focus();
                }

            }
            catch (Exception)
            {

                throw;
            }
        }

        private void itemcode_TextChanged(object sender, EventArgs e)
        {

        }

        private void cmbUnits_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtpurchase.Text = dictionaryPrice[cmbUnits.Text.Trim()];
            txtAvailableQTY.Text = dictionaryQty[cmbUnits.Text.Trim()];
            
        }

        private void discount_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                
                if (!String.IsNullOrEmpty(txtpurchase.Text) && !String.IsNullOrEmpty(txtQTY.Text))
                {

                    double totalAmount = (double.Parse(txtpurchase.Text) * double.Parse(txtQTY.Text));
                    double discountAmount = 0;
                    if (!String.IsNullOrEmpty(discount.Text.Trim()))
                    {
                        discountAmount = double.Parse(discount.Text);

                        double salesQtyAmount = double.Parse(discount.Text);

                        if (checkBox1.Checked)
                            discountAmount = ((totalAmount * salesQtyAmount) / 100);
                    }
                    else
                    {
                        discount.Text = "0";
                    }

                    totalAmount = totalAmount - discountAmount;
                    value.Text = totalAmount.ToString();
                    value.Focus();
                }
            }
        }

        private void txtFullDiscount_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {


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


                if (chkVAT.Checked == true)
                {
                    
                    //TODO - this 0.11 should be parameterized through TAX_MAST 
                    txtVAT.Text = (Math.Round(double.Parse(txtAfterDiscount.Text) * 0.11, 2)).ToString();
                    txtNBT.Text = "0";
                }
                else
                {
                    txtVAT.Text = "0";
                    txtNBT.Text = "0";
                }
//                txtDeductions.Text = "0";

                txtWithoutTax.Text = double.Parse(txtAfterDiscount.Text).ToString();
                txtGrandTot.Text = (double.Parse(txtAfterDiscount.Text) + (double.Parse(txtVAT.Text) + double.Parse(txtNBT.Text))).ToString();

 //               payableAmount.Text = (double.Parse(txtGrandTot.Text) - double.Parse(txtDeductions.Text)).ToString();

                button1.Enabled = true;


            }
            else if (e.KeyCode == Keys.Escape)
            {
                if (double.Parse(txtGrandTot.Text) > 0)
                {
                    button1.Enabled = true;
                    button1.Focus();
                    //grpLogin.Visible = true;
                    //txtINVUser.Text = _user.USERCODE;
                    //txtINVUser.Focus();

                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (txtGrandTot.Text == "")
            {
                txtGrandTot.Text = "0";
            }
            
                if (decimal.Parse(txtGrandTot.Text) > 0)
                {
                    button1.Enabled = false;
                    GRN_MAST grn = new GRN_MAST();

                    grn.COMPCODE = _user.COMPCODE;                    
                    grn.SUPP_CODE = txtSuppCode.Text;
                    grn.SUPP_NAME = txtSuppName.Text;
                    grn.GRN_DATE = DateTime.Now;
                    grn.GRN_TYPE = "GRN";
                    grn.REF = txtMemo.Text.Trim();
                    grn.TXN_DATE = DateTime.Now;
                    grn.TXN_TIME = DateTime.Now.ToShortTimeString();
                    grn.LOCA = txtLoca.Text;
                    grn.DN_NO = "";
                    grn.BRANCH_CODE = System.Configuration.ConfigurationManager.AppSettings.Get("BranchCode");
                    grn.PAY_STATUS = 0;
                    grn.PAY_IN_PROGRESS = 0;
                    grn.PRINTED = 0;
                    grn.USERCODE = _user.USERCODE;
                    grn.TOTAL = decimal.Parse(txtGrandTot.Text);
                    grn.PAYABLE_AMOUNT = decimal.Parse(txtGrandTot.Text);
                    grn.PAY_BALANCE = decimal.Parse(txtGrandTot.Text);
                    grn.DISCOUNT = decimal.Parse(txtFullDiscount.Text);
                    grn.DISCOUNT_AMT = decimal.Parse(txtFullDiscountValue.Text);
                    grn.TOTAL_AFTER_DISCOUNT = decimal.Parse(txtAfterDiscount.Text);
                    grn.VAT_AMOUNT = decimal.Parse(txtVAT.Text);
                    grn.NBT_AMOUNT = decimal.Parse(txtNBT.Text);
                    grn.COST_AMOUNT = decimal.Parse(txtWithoutTax.Text);
                    grn.DEDUCTIONS = 0;


                    List<GRN_DETAIL> grnDetails = new List<GRN_DETAIL>();

                    foreach (DataGridViewRow item in dataGridView3.Rows)
                    {

                        if (item.Cells["Value"].Value != null && !String.IsNullOrEmpty(item.Cells["Value"].Value.ToString()))
                        {
                            GRN_DETAIL detail = new GRN_DETAIL();

                            detail.COMPCODE = _user.COMPCODE;

                            //detail.CSH_CRD = cmbINVType.Text.Substring(0, (cmbINVType.Text.IndexOf("-"))).Trim();
                            detail.DISCOUNT = Decimal.Parse(item.Cells["Discount Rate"].Value.ToString());
                            if (item.Cells["Dis%"].Value.ToString() == "true")
                            {
                                detail.IS_DISCOUNT_PER = 1;
                            }
                            else
                            {
                                detail.IS_DISCOUNT_PER = 0;
                            }
                            detail.DISCOUNT_AMT = Decimal.Parse(item.Cells["Discount Amt"].Value.ToString());
                            detail.ITEM = item.Cells["Item Code"].Value.ToString();
                            detail.ITEM_DESC = item.Cells["Description"].Value.ToString();
                            detail.ITEM_LOCA = item.Cells["Loca"].Value.ToString();
                            detail.UNIT = item.Cells["Unit"].Value.ToString();
                            detail.QTY = Decimal.Parse(item.Cells["Qty"].Value.ToString());

                            if (item.Cells["Qty"].Value.ToString() == "")
                            {
                                detail.BULK_QTY = 0;
                                detail.BULK_SALES_PRICE = 0;
                                detail.BULK_UNIT = "";
                                detail.LOOSE_QTY = 0;
                                detail.LOOSE_SALES_PRICE = 0;
                                detail.LOOSE_UNIT = "";
                            }
                            else
                            {
                                if (item.Cells["Unit"].Value.ToString() == item.Cells["BUnit"].Value.ToString())
                                {
                                    detail.BULK_QTY = Decimal.Parse(item.Cells["Qty"].Value.ToString());
                                    detail.BULK_SALES_PRICE = Decimal.Parse(item.Cells["Sales Price"].Value.ToString());
                                    detail.BULK_UNIT = item.Cells["Unit"].Value.ToString();
                                    detail.LOOSE_QTY = 0;
                                    detail.LOOSE_SALES_PRICE = 0;
                                    detail.LOOSE_UNIT = "";
                                }
                                else
                                {
                                    detail.BULK_QTY = 0;
                                    detail.BULK_SALES_PRICE = 0;
                                    detail.BULK_UNIT = "";
                                    detail.LOOSE_QTY = Decimal.Parse(item.Cells["Qty"].Value.ToString());
                                    detail.LOOSE_SALES_PRICE = Decimal.Parse(item.Cells["Sales Price"].Value.ToString());
                                    detail.LOOSE_UNIT = item.Cells["Unit"].Value.ToString();
                                }
                            }

                            detail.TOTAL = Decimal.Parse(item.Cells["Value"].Value.ToString());
                            detail.LOCA = grn.LOCA; // item.Cells["Loca"].Value.ToString(); 
                            grnDetails.Add(detail);
                        }
                    }

                    
                    string documentNumber = webService.InsertGRNeMastDetails(grn, grnDetails.ToArray());
                    txtGRNNo.Text = documentNumber;
                    ////DisplayPrintScreen form = new DisplayPrintScreen(txtInvoiceNo.Text,"Invoice.rpt","INV_NO");           
                    ////DialogResult dialogResult = form.ShowDialog();

                    //string csh_crd = cmbINVType.Text.Substring(0, (cmbINVType.Text.IndexOf("-"))).Trim(); ;
                    //DisplayPrintScreen form = new DisplayPrintScreen(txtGRNNo.Text, "GRN.rpt", "GRN_NO", txtLoca.Text, "GRN", _user.COMPCODE, "", DateTime.Now, DateTime.Now);
                    //DialogResult dialogResult = form.ShowDialog();

                    txtGRNNO_Display.Text = txtGRNNo.Text;
                    txtLOCA_Display.Text = txtLoca.Text;
                    //cmbINVType.Focus();
                    ClearGRNdetails();


                }
            
        }
        private void ClearGRNdetails()
        {
            txtPrice.Text = "";
            description.Text = "";
            txtQTY.Text = "";
            discount.Text = "";
            value.Text = "";
            //lblCustomerType.Text = "";
            cmbUnits.Items.Clear();
            checkBox1.Checked = false;
            txtFullDiscount.Text = "";
            txtVAT.Text = "0";
            txtNBT.Text = "0";
            //txtDeductions.Text = "0";
            txtWithoutTax.Text = "0";
            txtGrandTot.Text = "0";
            //payableAmount.Text = "0";
            dataGridView3.DataSource = null; ;
            //lblLocaLabel.Text = "";
            //lblCustomerType.Text = "";
            //txtPO.Text = "";
            //txtRef.Text = "";
            txtSuppName.Text = "";
            txtSuppCode.Text = "";
            txtMemo.Text = "";
            location.Text = "";
            txtAvailableQTY.Text = "";
            itemcode.Text = "";
            txtSubTot.Text = "";
            txtAfterDiscount.Text = "";
            txtLoca.Text = "";

        }

        private void btnDisplay_Display_Click(object sender, EventArgs e)
        {

            //DisplayPrintScreen form = new DisplayPrintScreen(txtGRNNO_Display.Text, "GRN.rpt", "GRN_NO", txtLOCA_Display.Text, "GRN", _user.COMPCODE, "", DateTime.Now, DateTime.Now);
                //DialogResult dialogResult = form.ShowDialog();
  
            
        }

        private void txtFullDiscount_TextChanged(object sender, EventArgs e)
        {

        }

        private void cmbUnits_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtpurchase.Focus();

            }
        }

        private void txtpurchase_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtQTY.Focus();
            }

        }

        private void txtQTY_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                checkBox1.Focus();
            }
        }

        private void checkBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                discount.Focus();
            }
        }

        private void txtSuppName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtMemo.Focus();
            }
        }

        private void txtMemo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                location.Focus();
            }
        }

        private void dataGridView3_DoubleClick(object sender, EventArgs e)
        {
            DataRow dr = _table.Rows[dataGridView3.CurrentRow.Index];
            itemcode.Text = dr.ItemArray[0].ToString();
            description.Text = dr.ItemArray[1].ToString();
            location.Text = dr.ItemArray[2].ToString();
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
    }
}
