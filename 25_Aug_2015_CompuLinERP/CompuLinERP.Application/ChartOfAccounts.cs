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
    public partial class ChartOfAccounts : Form
    {
        InventoryWebService.InventoryWebService webService = new InventoryWebService.InventoryWebService();
        USERINFO _user;
        DataTable _table;

        public ChartOfAccounts(USERINFO user)
        {
            InitializeComponent();
            _user = user;
            ResetPage();
        }

        private void ResetPage()
        {
            ACC_CAT_L1 searchdetails = new ACC_CAT_L1();
            searchdetails.COMPCODE = _user.COMPCODE;

            ACC_CAT_L1[] masterDetails = webService.GetAllAccountMasterInfo(searchdetails);
            foreach (ACC_CAT_L1 item in masterDetails)
            {
                L5L1Code.Items.Add(item.CAT_L1 + " - " + item.CAT_DESC);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            int Is_Control=0;
            int Is_CashBook=0;
            int Bank_Acc = 0;
            int Prot=0;

            if (L5L1Code.Items == null || L5L2Code.Items == null || L5L3Code.Items == null || L5L4Code.Items == null || L5L5Code.Items == null || String.IsNullOrEmpty(RangeFrom.Text) || String.IsNullOrEmpty(RangeTo.Text) || String.IsNullOrEmpty(AccCode.Text) || String.IsNullOrEmpty(BankAbbrivation.Text))
            {
                if (L5L1Code.Items == null || L5L1Code.Items.Count == 0 || L5L1Code.SelectedItem == null)
                    MessageBox.Show("Level-1 cannot be empty.");
                else if (L5L2Code.Items == null || L5L2Code.Items.Count == 0 || L5L2Code.SelectedItem == null)
                    MessageBox.Show("Level-2 cannot be empty.");
                else if (L5L3Code.Items == null || L5L3Code.Items.Count == 0 || L5L3Code.SelectedItem == null)
                    MessageBox.Show("Level-3 cannot be empty.");
                else if (L5L4Code.Items == null || L5L4Code.Items.Count == 0 || L5L4Code.SelectedItem == null)
                    MessageBox.Show("Level-4 cannot be empty.");
                else if (L5L5Code.Items == null || L5L5Code.Items.Count == 0 || L5L5Code.SelectedItem == null)
                    MessageBox.Show("Level-5 cannot be empty.");
                else if (String.IsNullOrEmpty(RangeFrom.Text))
                    MessageBox.Show("Range From cannot be empty.");
                else if (String.IsNullOrEmpty(RangeTo.Text))
                    MessageBox.Show("Range To cannot be empty.");
                else if (String.IsNullOrEmpty(BankAbbrivation.Text))
                    MessageBox.Show("Bank Abbrivation cannot be empty.");
                else if (AccCode.Text == null)
                    MessageBox.Show("Bank Code cannot be empty.");
                else if (OpenBal.Text == null)
                    MessageBox.Show("Open Balance cannot be empty.");
                else if (BankSt.Text == null)
                    MessageBox.Show("Bank Statement cannot be empty.");
                else if (PwdProtest.Checked == true)
                    PwdProtest.Text = "1";
            }
            else
            {
                //Multiple check box
                if (ControlAccount.Checked == true)
                    Is_Control = 1;
                else if (DetailAccount.Checked == true)
                    Is_Control = 0;

                //Cash Book details
                if (CashBook.Checked == true)
                    Is_CashBook = 1;
                else if (CashBook.Checked == false)
                    Is_CashBook = 0;
                //Bank Account details
                if (BankAccount.Checked)
                    Bank_Acc = 1;
                else if (!BankAccount.Checked)
                    Bank_Acc = 0;

                //Protected details
                if (PwdProtest.Checked == true)
                    Prot = 1;
                else if (PwdProtest.Checked == false)
                    Prot = 0;

                GL Accounts = new GL();
                Accounts.CAT_L1 = (L5L1Code.Text.Split(new string[] { " - " }, StringSplitOptions.None)[0]);
                Accounts.CAT_L2 = (L5L2Code.Text.Split(new string[] { " - " }, StringSplitOptions.None)[0]);
                Accounts.CAT_L3 = (L5L3Code.Text.Split(new string[] { " - " }, StringSplitOptions.None)[0]);
                Accounts.CAT_L4 = (L5L4Code.Text.Split(new string[] { " - " }, StringSplitOptions.None)[0]);
                Accounts.CAT_L5 = (L5L5Code.Text.Split(new string[] { " - " }, StringSplitOptions.None)[0]);
                Accounts.COMPCODE = _user.COMPCODE;
                Accounts.IS_CONTROL_ACC = Is_Control;
                Accounts.IS_CASHBOOK = Is_CashBook;
                Accounts.OPBAL = decimal.Parse(OpenBal.Text);
                Accounts.BS_OPBAL = decimal.Parse(BankSt.Text);
                Accounts.GLCODE = AccCode.Text;
                Accounts.GLDESC = AccDesc.Text;
                Accounts.IS_PROTECTED = Prot;
                Accounts.BUDGET_AMT = 2500; //Need to retrieve
                Accounts.BANK_ABR = BankAbbrivation.Text;
                Accounts.USERCODE = _user.USERCODE;
                Accounts.CHANGED_USERCODE = _user.USERCODE;
                Accounts.TYPE_DR_CR = "DR";
                Accounts.IS_BANK = 1 ;
                Accounts.REC_UPTO_DATE = DateTime.Now;

                bool addAcount= webService.InsertAccountDetails(Accounts);

                if (addAcount)
                {
                    MessageBox.Show("Data Saved");
                    RestText();
                } 
            }
        }

        private void RestText()
        {
            L5L5Code.Items.Clear();
            L5L5Code.ResetText();
            L5L4Code.Items.Clear();
            L5L4Code.ResetText();
            L5L3Code.Items.Clear();
            L5L3Code.ResetText();
            L5L2Code.Items.Clear();
            L5L2Code.ResetText();
            L5L1Code.Items.Clear();
            L5L1Code.ResetText();
            ControlAccount.Checked = false;
            DetailAccount.Checked = false;
            CashBook.Checked = false;
            BankAccount.Checked = false;
            BankAbbrivation.Text = "";
            AccCode.Text = "";
            AccDesc.Text = "";
            OpenBal.Text = "";
            BankSt.Text = "";
            RangeFrom.Text = "";
            RangeTo.Text = "";

            ResetPage();

        }

        private void L5L1Code_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (string.IsNullOrEmpty(L5L1Code.Text))
            {
                MessageBox.Show("Select Fields");
            }
            else
            {
                L5L2Code.Items.Clear();
                L5L2Code.ResetText();

                ACC_CAT_L2 searchdetails = new ACC_CAT_L2();
                searchdetails.CAT_L1 = L5L1Code.SelectedItem.ToString().Split(new string[] { " - " }, StringSplitOptions.None)[0];
                searchdetails.COMPCODE = _user.COMPCODE;

                ACC_CAT_L2[] l2Details = webService.GetAllL2AccountDetailsByMasterId(searchdetails);
                foreach (ACC_CAT_L2 item in l2Details)
                {
                    L5L2Code.Items.Add(item.CAT_L2 + " - " + item.CAT_DESC);
                }
                if (l2Details != null && l2Details.Count() > 0)
                    L5L2Code.SelectedIndex = 0;
            }
        }

        private void L5L2Code_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (string.IsNullOrEmpty(L5L1Code.Text) || string.IsNullOrEmpty(L5L2Code.Text))
            {
                MessageBox.Show("Select Fields");
            }
            else
            {
                L5L3Code.Items.Clear();
                L5L3Code.ResetText();

                ACC_CAT_L3 searchdetails = new ACC_CAT_L3();
                searchdetails.CAT_L2 = (L5L2Code.Text.Split(new string[] { " - " }, StringSplitOptions.None)[0]);
                searchdetails.CAT_L1 = (L5L1Code.Text.Split(new string[] { " - " }, StringSplitOptions.None)[0]);
                searchdetails.COMPCODE = _user.COMPCODE;

                ACC_CAT_L3[] l2Details = webService.GetAllL3AccountDetailsByMastCatId(searchdetails);
                foreach (ACC_CAT_L3 item in l2Details)
                {
                    L5L3Code.Items.Add(item.CAT_L3 + " - " + item.CAT_DESC);
                }

                if (l2Details != null && l2Details.Count() > 0)
                    L5L3Code.SelectedIndex = 0;
            }
        }

        private void L5L3Code_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (string.IsNullOrEmpty(L5L1Code.Text) || string.IsNullOrEmpty(L5L2Code.Text) || string.IsNullOrEmpty(L5L3Code.Text) )
            {
                MessageBox.Show("Select Fields");
            }
            else
            {
                L5L4Code.Items.Clear();
                L5L4Code.ResetText();

                ACC_CAT_L4 searchdetails = new ACC_CAT_L4();
                searchdetails.CAT_L3 = (L5L3Code.Text.Split(new string[] { " - " }, StringSplitOptions.None)[0]);
                searchdetails.CAT_L2 = (L5L2Code.Text.Split(new string[] { " - " }, StringSplitOptions.None)[0]);
                searchdetails.CAT_L1 = (L5L1Code.Text.Split(new string[] { " - " }, StringSplitOptions.None)[0]);
                searchdetails.COMPCODE = _user.COMPCODE;

                ACC_CAT_L4[] l2Details = webService.GetAllL4AccountDetailsByMastCatId(searchdetails);
                foreach (ACC_CAT_L4 item in l2Details)
                {
                    L5L4Code.Items.Add(item.CAT_L4 + " - " + item.CAT_DESC);
                }

                if (l2Details != null && l2Details.Count() > 0)
                    L5L4Code.SelectedIndex = 0;
            }
        }

        private void L5L4Code_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (string.IsNullOrEmpty(L5L1Code.Text) || string.IsNullOrEmpty(L5L2Code.Text) || string.IsNullOrEmpty(L5L3Code.Text) || string.IsNullOrEmpty(L5L4Code.Text))
            {
                MessageBox.Show("Select Fields");
            }
            else
            {
                L5L5Code.Items.Clear();
                L5L5Code.ResetText();

                ACC_CAT_L5 searchdetails = new ACC_CAT_L5();
                searchdetails.CAT_L4 = L5L4Code.Text.Split(new string[] { " - " }, StringSplitOptions.None)[0];
                searchdetails.CAT_L3 = L5L3Code.Text.Split(new string[] { " - " }, StringSplitOptions.None)[0];
                searchdetails.CAT_L2 = L5L2Code.Text.Split(new string[] { " - " }, StringSplitOptions.None)[0];
                searchdetails.CAT_L1 = L5L1Code.Text.Split(new string[] { " - " }, StringSplitOptions.None)[0];
                searchdetails.COMPCODE = _user.COMPCODE;

                ACC_CAT_L5[] l2Details = webService.GetAllL5AccountDetailsByMastCatId(searchdetails);
                foreach (ACC_CAT_L5 item in l2Details)
                {
                    L5L5Code.Items.Add(item.CAT_L5 + " - " + item.CAT_DESC);
                }

                if (l2Details != null && l2Details.Count() > 0)
                    L5L5Code.SelectedIndex = 0;
            }
        }

        private void L5L5Code_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (L5L1Code.Text == null || L5L2Code.Text == null || L5L3Code.Text == null || L5L4Code.Text == null || L5L5Code.Text == null)
            {
                MessageBox.Show("Select Fields");
            }
            //GL searchdetails = new GL();
            //searchdetails.COMPCODE = _user.COMPCODE;
            //searchdetails.GLCODE = AccCode.Text;

            //GL details = webService.GetAllDetailsByAccCode(searchdetails);
            //BankAbbrivation.Text = details.BANK_ABR;
            //AccDesc.Text = details.GLDESC;
            //OpenBal.Text = details.OPBAL.ToString();
            //BankSt.Text = details.BS_OPBAL.ToString();
            //BankAbbrivation.Text = details.BANK_ABR;



            //if (details != null)
            //{
            //    if (details.IS_CONTROL_ACC == 1)
            //        ControlAccount.Checked = true;
            //    else
            //        DetailAccount.Checked = true;


            //    if (details.IS_CASHBOOK == 1)
            //        CashBook.Checked = true;
            //    if (details.IS_BANK == 1)
            //        BankAccount.Checked = true;
            //    if (details.IS_PROTECTED == 1)
            //        PwdProtest.Checked = true;

            //    L5L1Code.Text = details.CAT_L1;
            //    L5L2Code.Text = details.CAT_L2;
            //    L5L3Code.Text = details.CAT_L3;
            //    L5L4Code.Text = details.CAT_L4;
            //    L5L5Code.Text = details.CAT_L5;

            //    ACC_CAT_L5 RangeDetails = new ACC_CAT_L5();
            //    RangeDetails.COMPCODE = _user.COMPCODE;
            //    RangeDetails.CAT_L1 = details.CAT_L1;
            //    RangeDetails.CAT_L2 = details.CAT_L2;
            //    RangeDetails.CAT_L3 = details.CAT_L3;
            //    RangeDetails.CAT_L4 = details.CAT_L4;
            //    RangeDetails.CAT_L5 = details.CAT_L5;

            //    ACC_CAT_L5 GetRangeDetails = webService.GetAllL5AccountDetailsByIdAll(RangeDetails);
            //    RangeFrom.Text = GetRangeDetails.FROM_RANGE.ToString();
            //    RangeTo.Text = GetRangeDetails.TO_RANGE.ToString();
            //}
            else
            {
                ACC_CAT_L5 newSearchdetails = new ACC_CAT_L5();
                newSearchdetails.CAT_L5 = L5L5Code.Text.Split(new string[] { " - " }, StringSplitOptions.None)[0];
                newSearchdetails.CAT_L4 = L5L4Code.Text.Split(new string[] { " - " }, StringSplitOptions.None)[0];
                newSearchdetails.CAT_L3 = L5L3Code.Text.Split(new string[] { " - " }, StringSplitOptions.None)[0];
                newSearchdetails.CAT_L2 = L5L2Code.Text.Split(new string[] { " - " }, StringSplitOptions.None)[0];
                newSearchdetails.CAT_L1 = L5L1Code.Text.Split(new string[] { " - " }, StringSplitOptions.None)[0];
                newSearchdetails.COMPCODE = _user.COMPCODE;
                try
                {
                    ACC_CAT_L5 GetRangeDetails = webService.GetAllL5AccountDetailsByIdAll(newSearchdetails);
                    RangeFrom.Text = GetRangeDetails.FROM_RANGE.ToString();
                    RangeTo.Text = GetRangeDetails.TO_RANGE.ToString();

                }
                catch
                {
                    MessageBox.Show("Error occured.");
                }

            }
        }

        private void AccCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                if (string.IsNullOrEmpty(AccCode.Text))
                {
                    MessageBox.Show("Enter Account Code");
                }
                else 
                {
                    GL searchdetails = new GL();
                    searchdetails.COMPCODE = _user.COMPCODE;
                    searchdetails.GLCODE = AccCode.Text;
                    try
                    {
                        GL details = webService.GetAllDetailsByAccCode(searchdetails);
                        BankAbbrivation.Text = details.BANK_ABR;
                        AccDesc.Text = details.GLDESC;
                        OpenBal.Text = details.OPBAL.ToString();
                        BankSt.Text = details.BS_OPBAL.ToString();
                        BankAbbrivation.Text = details.BANK_ABR;

                        if (details.IS_CONTROL_ACC == 1)
                            ControlAccount.Checked = true;
                        else
                            DetailAccount.Checked = true;


                        if (details.IS_CASHBOOK == 1)
                            CashBook.Checked = true;
                        if (details.IS_BANK == 1)
                            BankAccount.Checked = true;
                        if (details.IS_PROTECTED == 1)
                            PwdProtest.Checked = true;

                        L5L1Code.Text = details.CAT_L1;
                        L5L2Code.Text = details.CAT_L2;
                        L5L3Code.Text = details.CAT_L3;
                        L5L4Code.Text = details.CAT_L4;
                        L5L5Code.Text = details.CAT_L5;

                        ACC_CAT_L5 RangeDetails = new ACC_CAT_L5();
                        RangeDetails.COMPCODE = _user.COMPCODE;
                        RangeDetails.CAT_L1 = details.CAT_L1;
                        RangeDetails.CAT_L2 = details.CAT_L2;
                        RangeDetails.CAT_L3 = details.CAT_L3;
                        RangeDetails.CAT_L4 = details.CAT_L4;
                        RangeDetails.CAT_L5 = details.CAT_L5;

                        ACC_CAT_L5 GetRangeDetails = webService.GetAllL5AccountDetailsByIdAll(RangeDetails);
                        RangeFrom.Text = GetRangeDetails.FROM_RANGE.ToString();
                        RangeTo.Text = GetRangeDetails.TO_RANGE.ToString();
                    }
                    catch 
                    {
                        MessageBox.Show("Data is not available");
                    }
                    
                    

                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (AccCode.Text!=null)
            {
                GL details = new GL();
                details.COMPCODE = _user.COMPCODE;
                details.GLCODE = AccCode.Text;

                bool deleteEntry = webService.DeleteAccountDetails(details);
                if (deleteEntry)
                {
                    MessageBox.Show("Data Deleted !");
                    RestText();
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int Is_Control = 0;
            int Is_CashBook = 0;
            int Bank_Acc = 0;
            int Prot = 0;

            GL details = new GL();
            details.COMPCODE = _user.COMPCODE;
            details.GLCODE = AccCode.Text;

            GL info = new GL();
            info.COMPCODE = _user.COMPCODE;
            info.GLCODE = AccCode.Text;
            info.CAT_L1 = (L5L1Code.Text.Split(new string[] { " - " }, StringSplitOptions.None)[0]);
            info.CAT_L2 = (L5L2Code.Text.Split(new string[] { " - " }, StringSplitOptions.None)[0]);
            info.CAT_L3 = (L5L3Code.Text.Split(new string[] { " - " }, StringSplitOptions.None)[0]);
            info.CAT_L4 = (L5L4Code.Text.Split(new string[] { " - " }, StringSplitOptions.None)[0]);
            info.CAT_L5 = (L5L5Code.Text.Split(new string[] { " - " }, StringSplitOptions.None)[0]);
            info.COMPCODE = _user.COMPCODE;
            info.IS_CONTROL_ACC = Is_Control;
            info.IS_CASHBOOK = Is_CashBook;
            info.OPBAL = decimal.Parse(OpenBal.Text);
            info.BS_OPBAL = decimal.Parse(BankSt.Text);
            info.GLCODE = AccCode.Text;
            info.GLDESC = AccDesc.Text;
            info.IS_PROTECTED = Prot;
            info.BUDGET_AMT = 2500; //Need to retrieve
            info.BANK_ABR = BankAbbrivation.Text;
            info.USERCODE = _user.USERCODE;
            info.CHANGED_USERCODE = _user.USERCODE;
            info.TYPE_DR_CR = "DR";
            info.IS_BANK = 1;
            info.REC_UPTO_DATE = DateTime.Now;

            bool updateEntry = webService.UpdateAccountDetails(details,info);
            if (updateEntry)
            {
                MessageBox.Show("Data Updated !");
                RestText();
            }
            else 
            {
                MessageBox.Show("Cannot Update !");
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            RestText();
        }
    }
}
