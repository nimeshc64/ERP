using CompuLinERP.WIN.InventoryWebService;
using CompuLinINV.WIN;
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
    public partial class Invoice : Form
    {
        InventoryWebService.InventoryWebService webService = new InventoryWebService.InventoryWebService();
        USERINFO _user;
        DataTable _table;
        Dictionary<string, string> dictionaryPrice = new Dictionary<string, string>();
        Dictionary<string, string> dictionaryQty = new Dictionary<string, string>();
        string previousLabel = "";

        public Invoice(USERINFO user)
        {            
            InitializeComponent();
            _user = user;
            _table = CreateDataTable();
            chkNoVat.Visible = false;
           
        }

        private void Invoice_Load(object sender, EventArgs e)
        {
            groupBox2.Enabled = false;

            cmbINVType.Items.Clear();
            cmbINVType_Display.Items.Clear();
            cmbINVType.ResetText();

            cmbSalesPriceTypes.Items.Clear();
            cmbSalesPriceTypes.ResetText();


            TXN_TYPES[] txnDetails = webService.GetAllTxnTypes(_user.COMPCODE,"INV");
            foreach (TXN_TYPES item in txnDetails)
            {
                cmbINVType.Items.Add(item.TXN_CODE + " - " + item.TXN_NAME);
                cmbINVType_Display.Items.Add(item.TXN_CODE + " - " + item.TXN_NAME);
            }

            PRICE_TYPES[] priceTypes = webService.GetAllSalesPriceTypes(_user.COMPCODE, "INV");
            foreach (PRICE_TYPES item in priceTypes)
            {
                cmbSalesPriceTypes.Items.Add(item.PRICE_CODE + " - " + item.PRICE_NAME);
            }

            cmbINVType.SelectedIndex  = 0;
            cmbINVType_Display.SelectedIndex = 0;
            cmbSalesPriceTypes.SelectedIndex = 0;

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
            
            _table.Rows.Add(itemcode.Text, description.Text, location.Text, cmbUnits.Text, txtPrice.Text, txtQTY.Text, discount.Text, checkBox1.Checked, _discountAmt, value.Text, lblBulkUnit.Text,lblLooseUnit.Text );
            
                dataGridView3.DataSource = _table;   
            dataGridView3.Columns[10].Width = 0;
            dataGridView3.Columns[11].Width = 0;

        }

        private void ClearInvoice()
        {
            txtPrice.Text = "";            
            description.Text = "";
            txtQTY.Text = "";            
            discount.Text = "";
            value.Text = "";
            txtAvailableQTY.Text = "";
            location.Text = "";
            cmbUnits.Items.Clear();
            checkBox1.Checked = false;

            txtInvoiceNo.Text="PENDING";
            txtLoca.Text="";
            lblLocaLabel.Text="";
            lblCustomerType.Text = "";
            txtPO.Text="";
            txtRef.Text="";
            txtCustName.Text="";
            txtMemo.Text="";
            txtCustCode.Text="";

            dataGridView3.DataSource = null;

            txtSubTot.Text="";
            txtFullDiscountValue.Text="";
            txtFullDiscount.Text="";

            chkFullDiscount.Text="";
            txtAfterDiscount.Text="";

            txtVAT.Text="";
            txtNBT.Text="";
            chkVAT.Checked=false;
            chkNBT.Checked=false;

            txtWithoutTax.Text="";
            txtGrandTot.Text="";

            txtDeductions.Text="";
            payableAmount.Text = "";


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

        private void ClearInvoicedetails()
        {
            txtPrice.Text = "";
            description.Text = "";
            txtQTY.Text = "";
            discount.Text = "";
            value.Text = "";
            //lblCustomerType.Text = "";
            cmbUnits.Items.Clear();
            checkBox1.Checked = false;
            txtFullDiscount.Text="";
            txtVAT.Text = "0";
            txtNBT.Text = "0";
            txtDeductions.Text = "0";
            txtWithoutTax.Text = "0";
            txtGrandTot.Text = "0";
            payableAmount.Text = "0";
            dataGridView3.DataSource = null;;
            lblLocaLabel.Text = "";
            lblCustomerType.Text = "";
            txtPO.Text = "";
            txtRef.Text = "";
            txtCustName.Text = "";
            txtCustCode.Text = "";
            txtMemo.Text = "";
            location.Text = "";
            txtAvailableQTY.Text = "";
            itemcode.Text = "";
            txtSubTot.Text = "";
            txtAfterDiscount.Text = "";
            txtLoca.Text = "";
            lblPack.Text = "0";

        }  

        private void MakePaymentCalculations()
        {
            double totalAmount = 0;
            foreach (DataGridViewRow item in dataGridView3.Rows)               
            {
                if (item.Cells["Value"].Value !=null && !String.IsNullOrEmpty(item.Cells["Value"].Value.ToString()))
                totalAmount = totalAmount + double.Parse(item.Cells["Value"].Value.ToString());
            }

            txtSubTot.Text = totalAmount.ToString();
        }

        private void discount_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                if (!String.IsNullOrEmpty(txtPrice.Text) && !String.IsNullOrEmpty(txtQTY.Text) )
                {
                    
                    double totalAmount = (double.Parse( txtPrice.Text )  * double.Parse( txtQTY.Text)) ;
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

        private void value_KeyPress(object sender, KeyPressEventArgs e)        
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {


                
                if (value.Text == "")
                {
                    value.Text = "0";
                }
                else if( decimal.Parse(value.Text) > 0)
                {
                    
                    LoadTable();
                    MakePaymentCalculations();
                    itemcode.Text = "";
                    ClearMaindetails();
                    itemcode.Focus();
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (double.Parse(txtGrandTot.Text) + 1 < double.Parse(txtSubTot.Text))
            {
                MessageBox.Show("Grand total is less than the sub total. Cannot proceeds", "TOTALS", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            else
            {
                if (txtGrandTot.Text == "")
                {
                    txtGrandTot.Text = "0";
                }
                // txtGrandTot
                //payableAmount

                //Retrieve customer current balance now. Since some other branches also may completed a txn at the same time
                CUST_MAST details = webService.GetAllCustomerDetailsById(_user.COMPCODE, txtCustCode.Text);
                int _creditBalanceOK = 0;

                if (details.CREDIT_BALANCE.ToString() == "")
                {
                    lblCustCreditBalance.Text = "0";
                }
                else
                {
                    lblCustCreditBalance.Text = details.CREDIT_BALANCE.ToString();
                }

                if (double.Parse(payableAmount.Text.ToString()) > double.Parse(lblCustCreditBalance.Text.ToString()))
                {
                    if (cmbINVType.Text != "CSH - CASH")
                    {
                        _creditBalanceOK = 0;
                        MessageBox.Show("Customer credit balance is less than the invoice total. Cannot proceeds", "CUSTOMER", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        _creditBalanceOK = 1;
                    }
                }
                else
                {
                    _creditBalanceOK = 1;
                }
                if (_creditBalanceOK == 1)
                {
                    if (decimal.Parse(txtGrandTot.Text) > 0)
                    {
                        button1.Enabled = false;
                        INV_MAST invoice = new INV_MAST();


                        invoice.COMPCODE = _user.COMPCODE;
                        invoice.CSH_CRD = cmbINVType.Text.Substring(0, (cmbINVType.Text.IndexOf("-"))).Trim();
                        invoice.CUST_CODE = txtCustCode.Text.ToUpper();
                        invoice.CUST_NAME = txtCustName.Text;
                        invoice.INV_DATE = DateTime.Now;
                        invoice.INV_TYPE = cmbSalesPriceTypes.Text.Substring(0, (cmbSalesPriceTypes.Text.IndexOf("-"))).Trim(); ;
                        invoice.REF = txtRef.Text.Trim();
                        invoice.TXN_DATE = DateTime.Now;
                        invoice.TXN_TIME = DateTime.Now.ToShortTimeString();
                        invoice.LOCA = txtLoca.Text;
                        invoice.DN_NO = txtPO.Text;

                        invoice.BRANCH_CODE = System.Configuration.ConfigurationManager.AppSettings.Get("BranchCode");

                        invoice.PAY_STATUS = 0;
                        invoice.PAY_IN_PROGRESS = 0;
                        invoice.PRINTED = 0;
                        if (txtINVUser.Text == "")
                        {
                            invoice.USERCODE = _user.USERCODE;

                        }
                        else
                        {
                            invoice.USERCODE = txtINVUser.Text; // _user.USERCODE;
                        }

                        invoice.TOTAL = decimal.Parse(txtGrandTot.Text);
                        invoice.PAYABLE_AMOUNT = decimal.Parse(payableAmount.Text);
                        invoice.PAY_BALANCE = decimal.Parse(payableAmount.Text);
                        if (chkFullDiscount.Checked == false)
                        {
                            invoice.IS_DISCOUNT_PER = 0;
                        }
                        else
                        {
                            invoice.IS_DISCOUNT_PER = 1;
                        }
                        invoice.DISCOUNT = decimal.Parse(txtFullDiscount.Text);
                        invoice.DISCOUNT_AMT = decimal.Parse(txtFullDiscountValue.Text);


                        invoice.TOTAL_AFTER_DISCOUNT = decimal.Parse(txtAfterDiscount.Text);
                        invoice.VAT_AMOUNT = decimal.Parse(txtVAT.Text);
                        invoice.NBT_AMOUNT = decimal.Parse(txtNBT.Text);
                        invoice.COST_AMOUNT = decimal.Parse(txtWithoutTax.Text);
                        invoice.DEDUCTIONS = decimal.Parse(txtDeductions.Text);


                        List<INV_DETAIL> invDetails = new List<INV_DETAIL>();

                        foreach (DataGridViewRow item in dataGridView3.Rows)
                        {

                            if (item.Cells["Value"].Value != null && !String.IsNullOrEmpty(item.Cells["Value"].Value.ToString()))
                            {
                                INV_DETAIL detail = new INV_DETAIL();

                                detail.COMPCODE = _user.COMPCODE;

                                detail.CSH_CRD = cmbINVType.Text.Substring(0, (cmbINVType.Text.IndexOf("-"))).Trim();
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
                                detail.LOCA = invoice.LOCA; // item.Cells["Loca"].Value.ToString(); 
                                invDetails.Add(detail);
                            }
                        }

                        string documentNumber = webService.InsertInvoiceMastDetails(invoice, invDetails.ToArray());
                        txtInvoiceNo.Text = documentNumber;
                        //DisplayPrintScreen form = new DisplayPrintScreen(txtInvoiceNo.Text,"Invoice.rpt","INV_NO");           
                        //DialogResult dialogResult = form.ShowDialog();

                        string csh_crd = cmbINVType.Text.Substring(0, (cmbINVType.Text.IndexOf("-"))).Trim(); ;
                        //DisplayPrintScreen form = new DisplayPrintScreen(txtInvoiceNo.Text, "Invoice.rpt", "INV_NO", txtLoca.Text, csh_crd, _user.COMPCODE, "", DateTime.Now, DateTime.Now);
                        //DialogResult dialogResult = form.ShowDialog();

                        txtINVNO_Display.Text = txtInvoiceNo.Text;
                        txtLOCA_Display.Text = txtLoca.Text;
                        cmbINVType.Focus();
                        _table.Rows.Clear();
                        ClearInvoicedetails();
                        groupBox2.Enabled = false;
                        groupInvHeader.Enabled = true;
                        SetReadonlyControlsFalse(groupInvHeader.Controls);
                        txtInvoiceNo.Text = "PENDING";
                        txtINVUser.Text = "";
                        txtINVPwd.Text = "";
                    }
                }
            }
        }

        private void txtLoca_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                txtLoca.BackColor = Color.White;
                if (e.KeyCode == Keys.F2)
                {                    
                    
                    SearchCriteria search = new SearchCriteria();
                    search.Location = "";
                    search.TableName = "";
                    search.SequenceNo = 5;
                    search.CompanyCode = _user.COMPCODE;
                    search.SearchStartingCharacters =_user.USERCODE;

                    HelpScreen form = new HelpScreen(search);
                    DialogResult dialogResult = form.ShowDialog();
                    txtLoca.Text = form._fromHelpValue;
                    location.Text = form._fromHelpValue;
                }
                else if (e.KeyCode == Keys.Enter  && txtLoca.Text.Trim()!= "")
                {
                    cmbSalesPriceTypes.Focus();
                }
                else if (e.KeyCode == Keys.Enter && txtLoca.Text.Trim() == "")
                {
                    txtLoca.BackColor = Color.Red ;
                }

            }
            catch (Exception)
            {

                throw;
            }
        }

        private void txtCustCode_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                txtCustCode.BackColor = Color.White;

                lblCustomerType.Text = "";
                if (e.KeyCode == Keys.F2)
                {
                    SearchCriteria search = new SearchCriteria();

                    search.Location = "";
                    search.TableName = "CUST_MAST";
                    search.SequenceNo = 3;
                    search.CompanyCode = _user.COMPCODE;
                    search.SearchStartingCharacters = txtCustCode.Text;
                                        
                    HelpScreen form = new HelpScreen(search);
                    DialogResult dialogResult = form.ShowDialog();
                    txtCustCode.Text = form._fromHelpValue;


                }
                else if (e.KeyCode == Keys.Enter && txtCustCode.Text.Trim() != "")
                {
                    CUST_MAST details = webService.GetAllCustomerDetailsById(_user.COMPCODE, txtCustCode.Text);
                    if (details.CUST_NAME == null)
                    {
                        MessageBox.Show("Customer Cannot find", "CUSTOMER", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                    txtCustName.Text = details.CUST_NAME;
                    lblCustCreditBalance.Text = details.CREDIT_BALANCE.ToString();
                    if (details.CUST_VATNO == "" || details.CUST_VATNO == null || details.CUST_VATNO == "0")
                    {
                        lblCustomerType.Text = "CASH";
                        chkNoVat.Visible = false;
                    }
                    else
                    {
                            lblCustomerType.Text = details.CUST_VATNO;
                            chkNoVat.Visible = true;
                    }
                    
                    
                    if (cmbINVType.Text != "CSH - CASH")
                    {
                        if (details.CREDIT_BALANCE <= 0 || details.CREDIT_BALANCE == null )
                        {
                            MessageBox.Show("Customer Credit Balance = 0. Cannot proceeds", "CUSTOMER CREDIT BALANCE", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            groupBox2.Enabled = false;
                        }
                        else
                        {
                            groupBox2.Enabled = true;
                        }
                    }
                    else
                    {
                        groupBox2.Enabled = true;
                    }

                    txtMemo.Focus();
                }
                }
                else if (e.KeyCode == Keys.Enter && txtCustCode.Text.Trim() == "")
                {
                    txtCustCode.BackColor = Color.Red;
                }

            }
            catch (Exception)
            {

                throw;
            }
        }

        private void SetReadonlyControlsFalse(Control.ControlCollection controlCollection)
        {
            if (controlCollection == null)
            {
                return;
            }

            foreach (TextBoxBase c in controlCollection.OfType<TextBoxBase>())
            {
                c.ReadOnly = false;
            }
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


        private void itemcode_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.F2)
                {
                    ClearMaindetails();

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
                else if (e.KeyCode == Keys.Escape )
                {
                    txtFullDiscount.Focus();
                }
                else if (e.KeyCode == Keys.Enter && itemcode.Text.Trim() != "")
                {

                    btnCut.Enabled = true;
                    SetReadonlyControls(groupInvHeader.Controls);


                    ITEM_MAST searchdetails = new ITEM_MAST();
                    searchdetails.ITEM = itemcode.Text;
                    searchdetails.LOCA_CODE = location.Text;
                    searchdetails.COMPCODE = _user.COMPCODE;
                    

                    
                    
                    ITEM_MAST[] ItemList = webService.GetAllItemDetailsByAllLocation(searchdetails);
                    //Create table and add data
                    // Add table to datagrid
                                        
                    dgStocks_in_allLocations.DataSource = ItemList;

                    dgStocks_in_allLocations.Columns["LOCA_CODE"].DisplayIndex = 0;
                    dgStocks_in_allLocations.Columns["DESC1"].DisplayIndex = 1;
                    dgStocks_in_allLocations.Columns["UNIT_1_QTY"].DisplayIndex = 2;
                    dgStocks_in_allLocations.Columns["UNIT_2_QTY"].DisplayIndex = 3;

                    dgStocks_in_allLocations.Columns["COMPCODE"].Visible = false;



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
                        lblPack.Text = details.PACK.ToString();

                        if (details.SALES_PRICE_BULK == null )
                        {
                            details.SALES_PRICE_BULK = 0;
                        }
                        if (details.SALES_PRICE_LOOSE == null)
                        {
                            details.SALES_PRICE_LOOSE = 0;
                        }
                        

                        
                        dictionaryPrice.Add(details.UNIT.ToString(), calculateItemSalesPrice("SHA","001","",lblCustomerType.Text.Trim(), decimal.Parse(details.SALES_PRICE_BULK.ToString())).ToString());
                        if (details.UNIT.ToString() != details.ITEM_UNIT0.ToString())
                        {
                            dictionaryPrice.Add(details.ITEM_UNIT0.ToString(), calculateItemSalesPrice("SHA", "001", "", lblCustomerType.Text.Trim(), decimal.Parse(details.SALES_PRICE_LOOSE.ToString())).ToString());
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
                if(CustType == "CASH")
                {
                    //This is to add VAT component to normal sales price. then the normal sales price will be high
                    //TODO - this 111 should be parameterized via TAX_MAST table
                    //salesPriceofCurrentItem = Math.Round((CurrentPrice/100) * 111 ,2);
                    salesPriceofCurrentItem = Math.Round(CurrentPrice , 2);
                }
                else
                {
                    //If it is a CREDIT customer, normal sales price will be shown. at the end of the invoice, VAT breakdown will be added.
                    salesPriceofCurrentItem = Math.Round(CurrentPrice, 2) - Math.Round((CurrentPrice /111) * 11, 20);
                }

                #endregion
            }
            return salesPriceofCurrentItem;
        }

        private void clearDatainControls()
        {
            description.Text="";
            txtQTY.Text="";
            discount.Text="";
            checkBox1.Checked=false ;
            value.Text="";
            lblBulkUnit.Text = "N/A";
            lblLooseUnit.Text = "N/A";

        }
        private void itemcode_GotFocus(object sender, EventArgs e)
        {
            clearDatainControls();
        }

        private void cmbINVType_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode== Keys.Enter)
            {
                txtLoca.Focus();
            }

        }
        private void cmbINVType_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

 

        private void txtLoca_TextChanged(object sender, EventArgs e)
        {

        }

        private void cmbSalesPriceTypes_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtPO.Focus();
            }
        }

        private void txtPO_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtRef.Focus();
            }
        }

        private void txtRef_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtCustCode.Focus();
            }
        }

        private void txtCustCode_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtMemo_KeyDown(object sender, KeyEventArgs e)
        {
            if ( e.KeyCode==Keys.Enter)
            {
                location.Focus();
            }
        }

        private void txtMemo_TextChanged(object sender, EventArgs e)
        {

        }

        private void location_TextChanged(object sender, EventArgs e)
        {

        }

        private void qty_KeyDown(object sender, KeyEventArgs e)
        {
            txtQTY.BackColor = Color.White;
            lblMessage.Text = "";

            if (e.KeyCode == Keys.Enter )
            {
                if (txtQTY.Text == "")
                {
                    txtQTY.Text = "0";
                }

                if (decimal.Parse(txtQTY.Text) > decimal.Parse(txtAvailableQTY.Text))
                {
                    txtQTY.BackColor = Color.Red;
                    lblMessage.Text = "Issue QTY is greater than available QTY";
                }
                else
                {

                    //looseSalesPriceQty.Focus();
                }


            }
            
        }

        //private void looseSalesPriceQty_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if (e.KeyCode == Keys.Enter )
        //    {
        //        if (lblQIH_Loose.Text=="")
        //        {
        //            lblQIH_Loose.Text = "0";
        //        }
        //        if (looseSalesPriceQty.Text == "")
        //        {
        //            looseSalesPriceQty.Text = "0";
        //        }
        //        if (decimal.Parse(looseSalesPriceQty.Text) > decimal.Parse(lblQIH_Loose.Text))
        //        {
        //            looseSalesPriceQty.BackColor = Color.Red;
        //            lblMessage.Text = "Issue QTY is greater than available QTY";
        //        }
        //        else
        //        {
        //            discount.Focus();
        //        }

        //    }
        //    else if (e.KeyCode == Keys.Escape )
        //    {
        //        qty.Focus();
        //    }
            
        //       // lblMessage.Text = "Please enter QTY";
            

        //}

        private void discount_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode==Keys.Enter )
            {
                value.Focus();
            }
        }

        private void discount_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtFullDiscount_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                grpLogin.Visible = true;

                if (String.IsNullOrEmpty(txtFullDiscount.Text) )
                {
                    txtFullDiscount.Text = "0";
                }                                  
                
                
                double totalAmount = double.Parse (txtSubTot.Text);
                    double discountAmount = double.Parse(txtFullDiscount.Text);

                    if (chkFullDiscount.Checked)
                    {
                        discountAmount = ((totalAmount * discountAmount) / 100);
                    }


                    txtFullDiscountValue.Text = discountAmount.ToString();
                    txtAfterDiscount.Text = (totalAmount - discountAmount).ToString();

                    
                if (lblCustomerType.Text.Trim() != "CASH")
                    {
                        chkVAT.Checked = true;
                        //TODO - this 0.11 should be parameterized through TAX_MAST 
                        txtVAT.Text = (Math.Round(double.Parse(txtAfterDiscount.Text) * 0.11, 2)).ToString();
                        txtNBT.Text = "0";
                    }
                    else
                    {
                    txtVAT.Text = "0";
                    txtNBT.Text = "0";
                    }
                    txtDeductions.Text = "0";
                    
                    txtWithoutTax.Text = Math.Round(double.Parse(txtAfterDiscount.Text),2).ToString() ;
                    txtGrandTot.Text = (double.Parse(txtAfterDiscount.Text) + (double.Parse(txtVAT.Text) + double.Parse(txtNBT.Text))).ToString();
                    txtGrandTot.Text = Math.Round(double.Parse(txtGrandTot.Text), 2).ToString();
                    payableAmount.Text = (double.Parse(txtGrandTot.Text) - double.Parse(txtDeductions.Text)).ToString();
                
                    button1.Enabled = true;


                    grpLogin.Visible = true;
                    txtINVUser.Text = _user.USERCODE;
                    txtINVUser.Focus();

                }
            else if (e.KeyCode == Keys.Escape )
                {

                    if (txtGrandTot.Text != "")
                    {
                        if (double.Parse(txtGrandTot.Text) > 0)
                        {
                            grpLogin.Visible = true;
                            txtINVUser.Text = _user.USERCODE;
                            txtINVUser.Focus();

                        }
                    }
                
            }

        }

        

        private void txtFullDiscount_TextChanged(object sender, EventArgs e)
        {
            grpLogin.Visible = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ClearInvoice();
            cmbINVType.Focus();

            //InvoiceReportDisplay form = new InvoiceReportDisplay(txtInvoiceNo.Text);
            //form.Visible = true;

            //CrystalReports.CachedCrystalReport1 cr1 = new CrystalReports.CachedCrystalReport1();

            //var rd = new  CrystalDecisions.CrystalReports.Engine.ReportDocument();
            //rd.Load("InvoiceDetails.rpt");

            //string strUser = "sa";
            //string strPwd = "intel@123";
            //string strDB = "Inventory";
            //string strServer = "InventoryManagement";

            //rd.SetDatabaseLogon(strUser, strPwd, strDB, strServer);
            //rd.SetParameterValue("INV_NO", "I20140091");
 
            //rd.PrintOptions.PrinterName = printDialog1.PrinterSettings.PrinterName;
            //rd.PrintToPrinter(1, false, 0, 0);
        }

        private void cmbUnits_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode==Keys.Enter)
            {
                //TODO 
                //Check whether this item is not inside the table.
                string _gridLoca = location.Text ;
                string _gridItem = itemcode.Text ;
                string _gridUnit = cmbUnits.Text ;
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
                
                if (_exists==0)
                {
                    txtQTY.ReadOnly = false;
                txtQTY.Focus();
            }
            }

        }

        private void cmbUnits_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtPrice.Text = dictionaryPrice[cmbUnits.Text.Trim()];
            txtAvailableQTY.Text = dictionaryQty[cmbUnits.Text.Trim()];
            
        }

        private void looseSalesPriceNos_Click(object sender, EventArgs e)
        {

        }

        private void qty_TextChanged(object sender, EventArgs e)
        {

        }

        private void value_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtQTY_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode==Keys.Enter && txtQTY.Text != "")
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

        private void checkBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                discount.Focus();
            }
        }

        private void itemcode_TextChanged(object sender, EventArgs e)
        {

        }

        private void itemcode_LostFocus(object sender, System.EventArgs e)
        {
            btnCut.Enabled = false;
        }


        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }





        private void txtQTY_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void groupBox5_Enter(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            string csh_crd = cmbINVType_Display.Text.Substring(0, (cmbINVType_Display.Text.IndexOf("-"))).Trim();             
            if (_user.COMPCODE=="001")
            {
                //DisplayPrintScreen form = new DisplayPrintScreen(txtINVNO_Display.Text, "Invoice001.rpt", "INV_NO", txtLOCA_Display.Text, csh_crd, _user.COMPCODE, "", DateTime.Now, DateTime.Now);
                //DialogResult dialogResult = form.ShowDialog();
            }
            else if(_user.COMPCODE=="002")
            {
                //DisplayPrintScreen form = new DisplayPrintScreen(txtINVNO_Display.Text, "Invoice002.rpt", "INV_NO", txtLOCA_Display.Text, csh_crd, _user.COMPCODE, "", DateTime.Now, DateTime.Now);
                //DialogResult dialogResult = form.ShowDialog();
                
            }
            
            
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (groupBox7.Visible==true  )
                groupBox7.Visible=false;
            else
            groupBox7.Visible=true ;
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnCut_Click(object sender, EventArgs e)
        {
            //Message Box - Do you need to cut this item?
            //Check available Bulk QTY fir this item  + for this location.
            // if stocks are avilable, then
            //      deduct 1 from bulk qty
            //      add pack size qty for loose qty
            // add entry for stocks table - to deduct bulk qty
            // add entry for stocks table - to add loose qty
            // add entry for cut note mast

            DialogResult dialogResult = MessageBox.Show("Are you sure to CUT this item ? This will add " + lblPack.Text +"" + lblLooseUnit.Text +" to "+ location.Text +" Location" , "CUT ITEM", MessageBoxButtons.YesNoCancel);
            if (dialogResult == DialogResult.Yes)
            {
                btnCut.Enabled = false;
                ITEM_MAST searchdetails = new ITEM_MAST();
                searchdetails.ITEM = itemcode.Text;
                searchdetails.LOCA_CODE = location.Text;
                searchdetails.COMPCODE = _user.COMPCODE;

                ITEM_MAST details = webService.GetAllItemDetailsById(searchdetails);
                if (details.UNIT_1_QTY > 0)
                {
                    string status = webService.AddCutNoteDetails(searchdetails);
                }
                else
                {
                    MessageBox.Show("You don't have stocks to CUT !", "NOT ENOUGH STOCKS", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); 
                }
                //ClearInvoicedetails();
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            //InvoiceReportDisplay form = new InvoiceReportDisplay("BINCARD", location.Text, "", "BINCARD", itemcode.Text , _user.COMPCODE ,"");
            //form.Visible = true;

            //string csh_crd = cmbINVType_Display.Text.Substring(0, (cmbINVType_Display.Text.IndexOf("-"))).Trim(); ;
            //DisplayPrintScreen form = new DisplayPrintScreen(txtINVNO_Display.Text, "Invoice.rpt", "INV_NO", txtLOCA_Display.Text, csh_crd);
            //DialogResult dialogResult = form.ShowDialog();

            string _location = location.Text;
            NETWORK[] company_name = webService.GetCompanyDetailsById(_user.COMPCODE);
            //CompuLinINV.WIN.InvoiceReportDisplay form = new CompuLinINV.WIN.InvoiceReportDisplay("0", _location, "", "BINCARD", itemcode.Text, _user.COMPCODE, company_name[0].CompName, DateTime.Now, DateTime.Now);
            //form.Refresh();
            //DialogResult dialogResult = form.ShowDialog();


        }

        private void chkNoVat_CheckedChanged(object sender, EventArgs e)
        {
            

            if (lblCustomerType.Text != "CASH")
            {
                previousLabel = lblCustomerType.Text;
            }

            
            if (chkNoVat.Checked)
            {
                lblCustomerType.Text = "CASH";
            }
            else
            {
                lblCustomerType.Text = previousLabel;
            }
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            bool isExists = webService.IsValidUser(txtINVUser.Text, txtINVPwd.Text, _user.COMPCODE );

            if (!isExists)
                lblIncorrect.Visible = true;
            else
            {
                lblIncorrect.Visible = false;
                grpLogin.Visible = false;
                button1.Focus();
            }
        }

        private void txtINVUser_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtINVPwd.Focus();
            }
        }

        private void txtINVPwd_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnOK.Focus();
            }
            else if (e.KeyCode == Keys.Escape )
            {
                txtINVUser.Focus();
            }
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {

        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {                            

                                if (txtINVUser.Text =="")
                                {
                                    MessageBox.Show("Please enter username and password. Then press cancellation again", "",MessageBoxButtons.OK );
                                    grpLogin.Visible = true;
                                    txtINVUser.Text = _user.USERCODE;
                                    txtINVUser.Focus();

                                }
                                else
                                {

                                    DialogResult dialogResult = MessageBox.Show("Are you sure to CANCEL this invoice ? " + txtINVNO_Display.Text + "", "CANCEL INVOICE", MessageBoxButtons.YesNoCancel);
                                    if (dialogResult == DialogResult.Yes)
                                    {


                                        //Update INV_MAST as cancelled. ( date / time / user )
                                        //Reverse stocks. add reversal entries to STOCK table
                                        INV_MAST inv = new INV_MAST();
                                        inv.INV_NO = txtINVNO_Display.Text;
                                        inv.LOCA = txtLOCA_Display.Text;
                                        inv.COMPCODE = _user.COMPCODE;
                                        if (txtINVUser.Text == "")
                                        {
                                            inv.USERCODE = _user.USERCODE;
                                        }
                                        else
                                        {
                                            inv.USERCODE = txtINVUser.Text;
                                        }

                                        inv.CSH_CRD = cmbINVType_Display.Text.Substring(0, (cmbINVType_Display.Text.IndexOf("-"))).Trim();

                                        string stausMsg = webService.CancelInvMastDetails(inv);
                                        txtINVUser.Text = "";
                                        txtINVPwd.Text = "";
                                        MessageBox.Show("OK");
                                    }
                                }

                            
        }

        private void button7_Click(object sender, EventArgs e)
        {
            

            

        }

        private void dataGridView3_DoubleClick(object sender, EventArgs e)
        {

            DataRow dr = _table.Rows[dataGridView3.CurrentRow.Index];
            itemcode.Text = dr.ItemArray[0].ToString();
            description.Text = dr.ItemArray[1].ToString();
            location.Text = dr.ItemArray[2].ToString();
            txtPrice.Text ="";
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

        private void itemcode_MouseLeave(object sender, EventArgs e)
        {
           
        }

        private void itemcode_TabIndexChanged(object sender, EventArgs e)
        {
            //btnCut.Enabled = false;
        }

        private void itemcode_Leave(object sender, EventArgs e)
        {
            
        }

        private void location_Click(object sender, EventArgs e)
        {
            btnCut.Enabled = false;
        }

        private void itemcode_Click(object sender, EventArgs e)
        {
            btnCut.Enabled = false;
        }

        private void grpLogin_Enter(object sender, EventArgs e)
        {

        }

        private void txtCustName_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtCustName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter )
                {
                    txtMemo.Focus();

            }
        }

        private void cmdSalesSum_Click(object sender, EventArgs e)
        {
           //DisplayPrintScreen form = new DisplayPrintScreen("SALES", "SalesReport.rpt", "SALES_REPOR", "ALL", "ALL", _user.COMPCODE, "", DTFrom.Value, DTTo.Value); 
           //DialogResult dialogResult = form.ShowDialog();
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
           
     }
}
