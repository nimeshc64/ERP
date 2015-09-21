using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using CompuLinINV.WIN;
using CompuLinINV.WIN.DTO;
using CompuLinERP.WIN.InventoryWebService;


namespace CompuLinINV.WIN
{
    public partial class Login : Form
    {
        string CompanyCode;
        string CompanyName;
        InventoryWebService webService = new InventoryWebService();
        
        public Login()
        {
            InitializeComponent();
        }



        private void LoginB_Click(object sender, EventArgs e)
        {           
            bool isExists = webService.IsValidUser(userName.Text, password.Text, CompanyCode);

            if (!isExists)
                invalidMessage.Visible = true;
            else
            {                             

                this.Visible = false;
                InventoryUser userDetails = new InventoryUser();
                userDetails.Username = userName.Text;
                Company companyDetails = new Company();
                companyDetails.Name = CompanyName;
                companyDetails.Code = CompanyCode;
                userDetails.Company = companyDetails;
                USERINFO user = webService.GetUserInfo(userName.Text, CompanyCode);
                MainForm form = new MainForm(user);
                form.Visible = true;   
                
            }
        }

        private void Login_Load(object sender, EventArgs e)
        {
            companyComboBox.Items.Clear();

            var versionInfo = FileVersionInfo.GetVersionInfo(Environment.CurrentDirectory+"\\CompuLinERP.WIN.exe" );
            lblVersion.Text  = "Version : "+ versionInfo.ProductVersion;

             NETWORK[] companies = webService.GetAllCompanyDetails();

            foreach (NETWORK company in companies)
            {
                companyComboBox.Items.Add(company.COMPCODE + " - " + company.CompName);
            }
            companyComboBox.SelectedIndex = 0;

            if (companyComboBox != null && !String.IsNullOrEmpty(companyComboBox.SelectedItem.ToString()) && companyComboBox.Items.Count > 0)
            {
                CompanyCode = companyComboBox.SelectedItem.ToString().Split(new string[] { " - " }, StringSplitOptions.None)[0];
                CompanyName = (companyComboBox.SelectedItem.ToString().Split(new string[] { " - " }, StringSplitOptions.None)[1]);
            }
            userName.Focus();
        }

        

        private void companyComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (companyComboBox != null && !String.IsNullOrEmpty(companyComboBox.SelectedItem.ToString()) && companyComboBox.Items.Count > 0)
            {
                CompanyCode = (companyComboBox.SelectedItem.ToString().Split(new string[] { " - " }, StringSplitOptions.None)[0]);
                CompanyName = (companyComboBox.SelectedItem.ToString().Split(new string[] { " - " }, StringSplitOptions.None)[1]);
            }
        }

        private void companyComboBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter )
            {
                userName.Focus();
            }
        }

        private void userName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                password.Focus();
            }
        }

        private void password_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                LoginB.Focus();
            }
        }

        private void password_TextChanged(object sender, EventArgs e)
        {

        }
    }


}
