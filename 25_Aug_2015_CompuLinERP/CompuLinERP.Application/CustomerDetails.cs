using CompuLinERP.WIN.InventoryWebService;
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
    public partial class CustomerDetails : Form
    {
        InventoryWebService.InventoryWebService webService = new InventoryWebService.InventoryWebService();
        USERINFO _user;

        public CustomerDetails(USERINFO user)
        {
            InitializeComponent();
            _user = user;

            Reset();
        }

        private void Reset()
        {
            code.Select();
            code.Text = string.Empty;
            name.Text = string.Empty;
            contact.Text = string.Empty;
            reference.Text = string.Empty;
            address1.Text = string.Empty;
            address2.Text = string.Empty;
            address3.Text = string.Empty;
            phone.Text = string.Empty;
            fax.Text = string.Empty;
            email.Text = string.Empty;
            vat.Text = string.Empty;
            svat.Text = string.Empty;
            maxcreditLimit.Text = string.Empty;
            accCode.Text = string.Empty;
            creditPeriod.Text = string.Empty;
            AROpBal.Text = string.Empty;
            ARremainBal.Text = string.Empty;           
            id.Text = string.Empty;

            dataGridView1.DataSource = null;
            CUST_MAST[] details = webService.GetAllCustomerDetailsByCompany(_user.COMPCODE);
            dataGridView1.DataSource = details;

            SetupGridColumnOrdering();
        }

        private void SetupGridColumnOrdering()
        {            
            dataGridView1.Columns["CUST_CODE"].DisplayIndex = 0;
            dataGridView1.Columns["CUST_NAME"].DisplayIndex = 1;
            dataGridView1.Columns["CUST_ADDRESS1"].DisplayIndex = 2;
            dataGridView1.Columns["CUST_ADDRESS2"].DisplayIndex = 3;            
            dataGridView1.Columns["CUST_ADDRESS3"].DisplayIndex = 4;
            dataGridView1.Columns["CUST_PHONE"].DisplayIndex = 5;
            dataGridView1.Columns["CUST_EMAIL"].DisplayIndex = 6;
            dataGridView1.Columns["CUST_VATNO"].DisplayIndex = 7;
            dataGridView1.Columns["CREDIT_PERIOD"].DisplayIndex = 8;
            dataGridView1.Columns["CREDIT_LIMIT"].DisplayIndex = 9;
            dataGridView1.Columns["GLCODE"].DisplayIndex = 10;
            dataGridView1.Columns["AR_OPBAL"].DisplayIndex = 11;
            dataGridView1.Columns["AR_OPBAL_BALANCE"].DisplayIndex = 12;
            dataGridView1.Columns["CUST_CONTACT_PERSON"].DisplayIndex = 13;
            dataGridView1.Columns["CUST_FAX"].DisplayIndex = 14;
            dataGridView1.Columns["ISVATEXCEMPTED"].DisplayIndex = 15;
            dataGridView1.Columns["OLD_GLCODE"].DisplayIndex = 16;
            dataGridView1.Columns["CUST_SVATNO"].DisplayIndex = 17;
            dataGridView1.Columns["REFNO"].DisplayIndex = 18;
            dataGridView1.Columns["USERCODE"].DisplayIndex = 19;

            dataGridView1.Columns["COMPCODE"].Visible = false;
            dataGridView1.Columns["IS_AUDITED"].Visible = false;
            dataGridView1.Columns["AUDIT_DATE"].Visible = false;
            dataGridView1.Columns["AUDIT_MEMO"].Visible = false;
        }

        private bool IsValid()
        {
            bool isvalid = true;

            if (String.IsNullOrEmpty(code.Text))
            {
                MessageBox.Show("Code cannot be empty.");
                code.Select();
                isvalid = false;
            }
            else if (String.IsNullOrEmpty(name.Text))
            {
                name.Select();
                MessageBox.Show("Name cannot be empty.");
                isvalid = false;
            }
            else if (String.IsNullOrEmpty(address1.Text))
            {
                address1.Select();
                MessageBox.Show("Address 1 cannot be empty.");
                isvalid = false;
            }
            else if (String.IsNullOrEmpty(address2.Text))
            {
                address2.Select();
                MessageBox.Show("Address 2 cannot be empty.");
                isvalid = false;
            }
            else if (String.IsNullOrEmpty(phone.Text))
            {
                address3.Select();
                MessageBox.Show("Phone cannot be empty.");
                isvalid = false;
            }
            else if (String.IsNullOrEmpty(email.Text))
            {
                email.Select();
                MessageBox.Show("Email cannot be empty.");
                isvalid = false;
            }
            else if (String.IsNullOrEmpty(vat.Text))
            {
                vat.Select();
                MessageBox.Show("VAT cannot be empty.");
                isvalid = false;
            }
            else if (String.IsNullOrEmpty(accCode.Text))
            {
                accCode.Select();
                MessageBox.Show("Account code cannot be empty.");
                isvalid = false;
            }
            else
            {
                if (!String.IsNullOrEmpty(maxcreditLimit.Text))
                {
                    try
                    {
                        float.Parse(maxcreditLimit.Text);                       
                    }
                    catch (Exception ex)
                    {
                        if (isvalid != false)
                            MessageBox.Show("Max Credit Limit should be a number.");
                        isvalid = false;
                        maxcreditLimit.Select();
                    }
                }
                if (!String.IsNullOrEmpty(creditPeriod.Text))
                {
                    try
                    {
                        float.Parse(creditPeriod.Text);                        
                    }
                    catch (Exception ex)
                    {
                        if (isvalid != false)
                            MessageBox.Show("Credit Period should be a number.");
                        isvalid = false;
                        creditPeriod.Select();

                    }
                }
                if (!String.IsNullOrEmpty(AROpBal.Text))
                {
                    try
                    {
                        float.Parse(AROpBal.Text);                        
                    }
                    catch (Exception ex)
                    {
                        if (isvalid != false)
                            MessageBox.Show("Opening Balance should be a number.");
                        isvalid = false;
                        AROpBal.Select();
                    }
                }
                if (!String.IsNullOrEmpty(ARremainBal.Text))
                {
                    try
                    {
                        float.Parse(ARremainBal.Text);                        
                    }
                    catch (Exception ex)
                    {
                        if (isvalid != false)
                            MessageBox.Show("Remaining Balance should be a number.");
                        isvalid = false;
                        ARremainBal.Select();
                    }
                }

            }
            return isvalid;
        }

        private void save_Click(object sender, EventArgs e)
        {            
            if(IsValid())
            {
                CUST_MAST details = new CUST_MAST();
                details.CUST_CODE = code.Text;
                if (vatEx.Checked)
                    details.ISVATEXCEMPTED = 1;
                else
                    details.ISVATEXCEMPTED = 0;
                details.CUST_NAME = name.Text;
                details.CUST_CONTACT_PERSON = contact.Text;
                details.REFNO = reference.Text;
                details.CUST_ADDRESS1 = address1.Text;
                details.CUST_ADDRESS2 = address2.Text;
                details.CUST_ADDRESS3 = address3.Text;
                details.CUST_PHONE = phone.Text;
                details.CUST_FAX = fax.Text;
                details.CUST_EMAIL = email.Text;
                details.CUST_VATNO = vat.Text;
                details.CUST_SVATNO = svat.Text;

                if (!String.IsNullOrEmpty(maxcreditLimit.Text))
                    details.CREDIT_LIMIT = float.Parse(maxcreditLimit.Text);
                if (!String.IsNullOrEmpty(creditBalance.Text))
                    details.CREDIT_BALANCE  = float.Parse(creditBalance.Text);
                if (!String.IsNullOrEmpty(creditPeriod.Text))
                    details.CREDIT_PERIOD = float.Parse(creditPeriod.Text);

                details.GLCODE = accCode.Text;
                details.USERCODE = _user.USERCODE;
                details.COMPCODE = _user.COMPCODE;

                if (!String.IsNullOrEmpty(AROpBal.Text))
                    details.AR_OPBAL = float.Parse(AROpBal.Text);
                if (!String.IsNullOrEmpty(ARremainBal.Text))
                    details.AR_OPBAL_BALANCE = float.Parse(ARremainBal.Text);

                bool status = false;

                if (String.IsNullOrEmpty(id.Text))
                {
                    status = webService.InsertCustomerDetails(details);
                }
                else
                {
                    CUST_MAST searchdetails = new CUST_MAST();
                    searchdetails.CUST_CODE = id.Text;
                    searchdetails.COMPCODE = _user.COMPCODE;

                    status = webService.UpdateCustomerDetails(searchdetails, details);
                }

                if (status)
                {
                    Reset();
                }
            }
        }

        private void delete_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(id.Text))
            {
                CUST_MAST searchdetails = new CUST_MAST();
                searchdetails.CUST_CODE = id.Text;
                searchdetails.COMPCODE = _user.COMPCODE;

                bool status = webService.DeleteCustomerDetails(searchdetails);

                if (status)
                {
                    Reset();
                }
            }
        }

        private void reset_Click(object sender, EventArgs e)
        {
            Reset();
        }

        private void dataGridView1_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dataGridView1.Rows[e.RowIndex];
                id.Text = row.Cells["CUST_CODE"].Value.ToString();

                CUST_MAST details = webService.GetAllCustomerDetailsById(_user.COMPCODE, (id.Text) );
                code.Text = details.CUST_CODE;
                if (details.ISVATEXCEMPTED == 1 )
                    vatEx.Checked = true;
                else
                    vatEx.Checked = false;
                name.Text = details.CUST_NAME;
                contact.Text = details.CUST_CONTACT_PERSON;
                reference.Text = details.REFNO;
                address1.Text = details.CUST_ADDRESS1;
                address2.Text = details.CUST_ADDRESS2;
                address3.Text = details.CUST_ADDRESS3;
                phone.Text = details.CUST_PHONE;
                fax.Text = details.CUST_FAX;
                email.Text = details.CUST_EMAIL;
                vat.Text = details.CUST_VATNO;
                svat.Text = details.CUST_SVATNO;
                if (details.CREDIT_LIMIT  !=null)
                    maxcreditLimit.Text = details.CREDIT_LIMIT.ToString();
                if (details.CREDIT_BALANCE  != null)
                    creditBalance.Text = details.CREDIT_BALANCE.ToString();
                if (details.CREDIT_PERIOD != null)
                    creditPeriod.Text = details.CREDIT_PERIOD.ToString();
                accCode.Text = details.GLCODE;
                if (details.AR_OPBAL != null)
                    AROpBal.Text = details.AR_OPBAL.ToString();
                if (details.AR_OPBAL_BALANCE != null)
                    ARremainBal.Text =details.AR_OPBAL_BALANCE.ToString() ;

                code.Select();
            }
        }

        private void CustomerDetails_Load(object sender, EventArgs e)
        {
            cmbCustType.Items.Add("CASH");
            cmbCustType.Items.Add("CREDIT");
            cmbCustType.SelectedIndex = 0;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

       
    }
}
