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
    public partial class SupplierDetails : Form
    {
        InventoryWebService.InventoryWebService webService = new InventoryWebService.InventoryWebService();
        USERINFO _user;

        public SupplierDetails(USERINFO user)
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
            address1.Text = string.Empty;
            address2.Text = string.Empty;
            phone.Text = string.Empty;
            fax.Text = string.Empty;
            email.Text = string.Empty;
            maxCreditLimit.Text = string.Empty;
            creditPeriod.Text = string.Empty;
            accCode.Text = string.Empty;
            openingbalance.Text = string.Empty;
            remainingbalance.Text = string.Empty;

            id.Text = string.Empty;

            dataGridView1.DataSource = null;
            SUPP_MAST[] details = webService.GetAllSupplierDetailsByCompany( "SUPP_MAST",code.Text,"", _user.COMPCODE, "SUPP_CODE");
            dataGridView1.DataSource = details;

            SetupGridColumnOrdering();
        }

        private void SetupGridColumnOrdering()
        {           
            dataGridView1.Columns["SUPP_CODE"].DisplayIndex = 0;
            dataGridView1.Columns["SUPP_NAME"].DisplayIndex = 1;
            dataGridView1.Columns["SUPP_ADDRESS1"].DisplayIndex = 2;
            dataGridView1.Columns["SUPP_ADDRESS2"].DisplayIndex = 3;
            dataGridView1.Columns["SUPP_PHONE"].DisplayIndex = 4;
            dataGridView1.Columns["SUPP_EMAIL"].DisplayIndex = 5;            
            dataGridView1.Columns["CREDIT_LIMIT"].DisplayIndex = 6;
            dataGridView1.Columns["CREDIT_PERIOD"].DisplayIndex = 7;
            dataGridView1.Columns["GLCODE"].DisplayIndex = 8;
            dataGridView1.Columns["AP_OPBAL"].DisplayIndex = 9;
            dataGridView1.Columns["AP_OPBAL_BALANCE"].DisplayIndex = 10;
            dataGridView1.Columns["SUPP_FAX"].DisplayIndex = 11;
            dataGridView1.Columns["SUPP_ADDRESS3"].Visible = false; 
            dataGridView1.Columns["SUPP_CONTACT_PERSON"].Visible = false; 
            dataGridView1.Columns["SUPP_VATNO"].Visible = false; 
            dataGridView1.Columns["OLD_GLCODE"].Visible = false; 
            dataGridView1.Columns["USERCODE"].DisplayIndex = 13;
            dataGridView1.Columns["COMCODE"].Visible = false; 
        }

        private bool IsValidated()
        {
            bool isValid = true;

            if (String.IsNullOrEmpty(code.Text))
            {
                MessageBox.Show("Code cannot be empty.");
                isValid = false;
            }
            else if (String.IsNullOrEmpty(name.Text))
            {
                MessageBox.Show("Name cannot be empty.");
                isValid = false;
            }
            else if (String.IsNullOrEmpty(address1.Text))
            {
                MessageBox.Show("Address1 cannot be empty.");
                isValid = false;
            }
            else if (String.IsNullOrEmpty(phone.Text))
            {
                MessageBox.Show("Phone cannot be empty.");
                isValid = false;
            }
            if (!String.IsNullOrEmpty(openingbalance.Text))
            {
                try
                {
                    float.Parse(openingbalance.Text);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Opening Balance should be a number.");
                    isValid = false;
                }
            }
            if (!String.IsNullOrEmpty(remainingbalance.Text))
            {
                try
                {
                    float.Parse(remainingbalance.Text);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Remaining Balance should be a number.");
                    isValid = false;
                }
            }
            if (!String.IsNullOrEmpty(maxCreditLimit.Text))
            {
                try
                {
                    float.Parse(maxCreditLimit.Text);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Max Credit Limit should be a number.");
                    isValid = false;
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
                    MessageBox.Show("Credit Period should be a number.");
                    isValid = false;
                }
            }

            return isValid;
        }

        private void save_Click(object sender, EventArgs e)
        {
            if (IsValidated())
            {
                SUPP_MAST details = new SUPP_MAST();
                details.SUPP_CODE = code.Text;
                details.SUPP_NAME = name.Text;
                details.SUPP_ADDRESS1 = address1.Text;
                details.SUPP_ADDRESS2 = address2.Text;
                details.SUPP_PHONE = phone.Text;
                details.SUPP_FAX = fax.Text;
                details.SUPP_EMAIL = email.Text;
                if (!String.IsNullOrEmpty(maxCreditLimit.Text))
                    details.CREDIT_LIMIT = float.Parse(maxCreditLimit.Text);
                if (!String.IsNullOrEmpty(creditPeriod.Text))
                    details.CREDIT_PERIOD = float.Parse(creditPeriod.Text);
                details.GLCODE = accCode.Text;
                if (!String.IsNullOrEmpty(openingbalance.Text))
                    details.AP_OPBAL = float.Parse(openingbalance.Text);
                if (!String.IsNullOrEmpty(remainingbalance.Text))
                    details.AP_OPBAL_BALANCE = float.Parse(remainingbalance.Text);
                details.USERCODE = _user.USERCODE;
                details.COMCODE = _user.COMPCODE;

                bool status = false;

                if (String.IsNullOrEmpty(id.Text))
                {
                    status = webService.InsertSupplierDetails(details);
                }
                else
                {
                    SUPP_MAST searchdetails = new SUPP_MAST();
                    searchdetails.SUPP_CODE = id.Text;
                    searchdetails.COMCODE = _user.COMPCODE;

                    status = webService.UpdateSupplierDetails(searchdetails, details);
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
                bool status = webService.DeleteSupplierDetails(_user.COMPCODE, id.Text);

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
                id.Text = row.Cells["SUPP_CODE"].Value.ToString();

                SUPP_MAST details = webService.GetAllSupplierDetailsById(id.Text, _user.COMPCODE);
                code.Text=details.SUPP_CODE;
                name.Text = details.SUPP_NAME;
                address1.Text = details.SUPP_ADDRESS1;
                address2.Text = details.SUPP_ADDRESS2;
                phone.Text = details.SUPP_PHONE;
                fax.Text = details.SUPP_FAX;
                email.Text = details.SUPP_EMAIL;
                 maxCreditLimit.Text = details.CREDIT_LIMIT.ToString() ;
                 creditPeriod.Text = details.CREDIT_PERIOD.ToString();
                 accCode.Text = details.GLCODE;
                 openingbalance.Text = details.AP_OPBAL.ToString();
                 remainingbalance.Text = details.AP_OPBAL_BALANCE.ToString();

                 code.Select();

            }
        }

        private void SupplierDetails_Load(object sender, EventArgs e)
        {

        }

     

       
      
    }
}
