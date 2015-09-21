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
    public partial class Receipts : Form
    {
        InventoryWebService.InventoryWebService webService = new InventoryWebService.InventoryWebService();
        USERINFO _user;
        DataTable _table;
        DataTable _tableCustOutstanding;
        DataTable _tableSettlements;
        Dictionary<string, string> dictionaryPrice = new Dictionary<string, string>();
        Dictionary<string, string> dictionaryQty = new Dictionary<string, string>();
        string previousLabel = "";

        public Receipts(USERINFO user)
        {
            InitializeComponent();
            _user = user;
            _table = CreateDataTable();
            _tableCustOutstanding = CreatePendingInvoicesDataTable();
            _tableSettlements = CreateSettledDataTable();
            LoadOaymentTypes();
            load_cmbBank();
            
        }

        private DataTable CreateDataTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Type");
            dt.Columns.Add("ChqNo");
            dt.Columns.Add("ChqDate");
            dt.Columns.Add("Amount");

            dt.Columns["Type"].ReadOnly = true;
            dt.Columns["ChqNo"].ReadOnly = true;
            dt.Columns["ChqDate"].ReadOnly = true;
            dt.Columns["Amount"].ReadOnly = true;
            
            return dt;
        }

        private DataTable CreatePendingInvoicesDataTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Loca");
            dt.Columns.Add("TXN_TYPE");
            dt.Columns.Add("DOC_NO");
            dt.Columns.Add("CSH_CRD");
            dt.Columns.Add("Total");
            dt.Columns.Add("Balance");
            dt.Columns.Add("PaidAmt");
            dt.Columns.Add("NewBalance");


            dt.Columns["Loca"].ReadOnly = true;
            dt.Columns["TXN_TYPE"].ReadOnly = true;
            dt.Columns["DOC_NO"].ReadOnly = true;
            dt.Columns["CSH_CRD"].ReadOnly = true;
            dt.Columns["Total"].ReadOnly = true;
            dt.Columns["NewBalance"].ReadOnly = true;
            dt.Columns["Balance"].ReadOnly = true;
                        
            return dt;
        }

        private DataTable CreateSettledDataTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Loca");
            dt.Columns.Add("TXN_TYPE");
            dt.Columns.Add("DOC_NO");
            dt.Columns.Add("CSH_CRD");
            dt.Columns.Add("AMOUNT");
            dt.Columns.Add("PAID");
            dt.Columns.Add("BALANCE");
            return dt;
        }

        private void LoadOaymentTypes()
        {
            cmbType.Items.Clear();
                        cmbType.Items.Add("CASH");
                        cmbType.Items.Add("CHEQUE");
                        cmbType.SelectedIndex = 0;
            
        }
        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void dgStocks_in_allLocations_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void LoadBankAccount()
        {

        }
        private string loadCustomerdetails()
        {
            string strGLCODE = "";
            CUST_MAST details = webService.GetAllCustomerDetailsById(_user.COMPCODE, txtCustCode.Text);
            if (details.CUST_NAME == null)
            {
                MessageBox.Show("Customer Cannot find", "CUSTOMER", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                txtCustName.Text = details.CUST_NAME;
                lblCreditbalance.Text = details.CREDIT_BALANCE.ToString();
                lblCreditLimit.Text = details.CREDIT_LIMIT.ToString();
                             
                LoadCustomerLedger(_user.COMPCODE, txtCustCode.Text);
                CalculateTotalOutstanding();


                if (details.CREDIT_BALANCE <= 0 || details.CREDIT_BALANCE == null)
                {
                    MessageBox.Show("Customer`s Available Credit Balance = 0. This goes as an advance payment", "CUSTOMER CREDIT BALANCE", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                strGLCODE = details.GLCODE;
                txtMemo.Focus();

                
            }
            return strGLCODE;
        }


        private void txtCustCode_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                txtCustCode.BackColor = Color.White;

                //lblCustomerType.Text = "";
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
                    string custAcc;
                    custAcc = loadCustomerdetails();
                    //LoadBankAccount();
                    //LoadCashBookaccount();
                    //LoadAccCodeByID();

                }
                else if (e.KeyCode == Keys.Enter && txtCustCode.Text.Trim() == "")
                {
                    txtCustCode.BackColor = Color.Red;
                }

            }
            catch (Exception ex1)
            {
                throw;
            }
        }

        private void cmbBank_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                GL BankDetails = new GL();
                BankDetails.COMPCODE = _user.COMPCODE;

                GL[] ToCombo = webService.GetAllDetailsByAccCodeToComboCashBook(BankDetails);
                foreach (GL item in ToCombo)
                {
                    cmbCash.Items.Add(item.GLCODE);
                }
                if (ToCombo != null && ToCombo.Count() > 0)
                    cmbCash.SelectedIndex = 0;

            }
            catch (Exception ex1)
            {
                throw;
            }
        }

        private void load_cmbBank()
        {
            try
            {
                GL BankDetails = new GL();
                BankDetails.COMPCODE = _user.COMPCODE;

                GL[] ToCombo=webService.GetAllDetailsByAccCodeToCombo(BankDetails);
                foreach (GL item in ToCombo)
                {
                    cmbBank.Items.Add(item.GLCODE);
                }
                if (ToCombo != null && ToCombo.Count() > 0)
                    cmbBank.SelectedIndex = 0;

            }
            catch (Exception ex1)
            {
                throw;
            }
        }

        private void cmbCash_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                GL BankDetails = new GL();
                BankDetails.COMPCODE = _user.COMPCODE;
                BankDetails.GLCODE = "50";

                GL[] ToCombo = webService.GetAllDetailsByAccCodeToComboCreditBook(BankDetails);
                foreach (GL item in ToCombo)
                {
                    cmbCreditAcc.Items.Add(item.GLCODE);
                }
                if (ToCombo != null && ToCombo.Count() > 0)
                    cmbBank.SelectedIndex = 0;

            }
            catch (Exception ex1)
            {
                throw;
            }
        }

        private void cmbCreditAcc_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                GL BankDetails = new GL();
                MessageBox.Show("Ok");
            }
            catch (Exception ex1)
            {
                throw;
            }
        }
        
        private void CalculateTotalOutstanding()
        {
            double totalAmount = 0;
            foreach (DataGridViewRow item in dgToBeSettled.Rows)
            {
                if (item.Cells["Balance"].Value != null && !String.IsNullOrEmpty(item.Cells["Balance"].Value.ToString()))
                    totalAmount = totalAmount + double.Parse(item.Cells["Balance"].Value.ToString());
            }

            lblTotOutstanding.Text = totalAmount.ToString();
        }

        private void txtFullAmt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter )
            {
                lblReceiptBal.Text = txtFullAmt.Text ;
                cmbType.Focus();
            }
        }

        private void LoadTable()
        {
            //_table.Clear();
            if (double.Parse(txtAmount.Text) > 0)
            {
                if (txtChqNo.Text  == "")
                {
                    //txtChqNo.Text  = "CASH";
                }

                _table.Rows.Add(cmbType.Text, txtChqNo.Text, dtChqDate.Value.ToString(), txtAmount.Text);
                
                 dgPayment.DataSource = _table;
            }    
        }

        private void LoadCustomerLedger(string compcode, string custcode)
        {
            _tableCustOutstanding.Clear();
                CUSTOMER_LEDGER custLedger = webService.GetCustomerOutstandingById(compcode,custcode );

                _tableCustOutstanding.Rows.Add(custLedger.LOCA, custLedger.TXN_TYPE, custLedger.DOC_NO, custLedger.CSH_CRD, decimal.Round(custLedger.TXN_TOTAL,2), decimal.Round(custLedger.PAYABLE_BALANCE,2),0, decimal.Round(custLedger.PAYABLE_BALANCE,2));
                dgToBeSettled.DataSource = _tableCustOutstanding;

            
        }

        private void ClearPaydetails()
        {
            cmbType.SelectedIndex=0;
            txtChqNo.Text="";
            dtChqDate.Value= DateTime.Now;
            txtAmount.Text = "";
        }

        private void MakeSettlementCalculations()
        {
            double totalAmount = 0;
            foreach (DataGridViewRow item in dgToBeSettled.Rows)
            {
                if (item.Cells["PaidAmt"].Value != null && !String.IsNullOrEmpty(item.Cells["PaidAmt"].Value.ToString()))
                    totalAmount = totalAmount + double.Parse(item.Cells["PaidAmt"].Value.ToString());
            }

            lblSettledAmt.Text = totalAmount.ToString();
            lblSettledBalAmt.Text = (double.Parse(lblTotOutstanding.Text) - double.Parse(lblSettledAmt.Text)).ToString();


        }

        private void MakePaymentCalculations()
        {
            double totalAmount = 0;
            foreach (DataGridViewRow item in dgPayment.Rows)
            {
                if (item.Cells["Amount"].Value != null && !String.IsNullOrEmpty(item.Cells["Amount"].Value.ToString()))
                    totalAmount = totalAmount + double.Parse(item.Cells["Amount"].Value.ToString());
            }

            lblTot.Text = totalAmount.ToString();
            lblBal.Text = (double.Parse( lblReceiptBal.Text) - double.Parse( lblTot.Text)).ToString();
        }


        private void txtAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                if (txtAmount.Text == "")
                {
                    txtAmount.Text = "0";
                }
                else if (decimal.Parse(txtAmount.Text) > 0)
                {

                    LoadTable();
                    MakePaymentCalculations();
                    txtChqNo.Text = "";
                    ClearPaydetails();
                    cmbType.Focus();
                }
            }
        }

        private void dgToBeSettled_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgToBeSettled_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            MakeSettlementCalculations();
            dgToBeSettled.CurrentRow.Cells["newbalance"].ReadOnly = false;
            dgToBeSettled.CurrentRow.Cells["newbalance"].Value  =  decimal.Round(decimal.Parse( dgToBeSettled.CurrentRow.Cells["balance"].Value.ToString()),2) - decimal.Round(decimal.Parse(dgToBeSettled.CurrentRow.Cells["paidAmt"].Value.ToString()),2);
            dgToBeSettled.CurrentRow.Cells["newbalance"].ReadOnly = true;
        }

        private void txtFullAmt_TabIndexChanged(object sender, EventArgs e)
        {
            

        }

        private void txtFullAmt_Leave(object sender, EventArgs e)
        {
                lblReceiptBal.Text = txtFullAmt.Text;
        }

        private void txtCustCode_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtCustCode_Leave(object sender, EventArgs e)
        {
                //if (txtCustCode.Text.Trim() != "")
                //{
                //    loadCustomerdetails();
                //    txtMemo.Focus();
                //}
        }

        private void txtAmount_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtFullAmt_TextChanged(object sender, EventArgs e)
        {

        }

        private void cmdPost_Click(object sender, EventArgs e)
        {
            //Generate Next Receipt Number using Location
            //Insert into Receipt Mast

            //Insert into Receipt details

            //Insert into Receipt inv
        }
    }
}

