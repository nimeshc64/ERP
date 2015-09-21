using CompuLinERP.WIN.InventoryWebService;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CompuLinINV.WIN
{
    public partial class UserManagement : Form
    {
        InventoryWebService webService = new InventoryWebService();
        const string USERLEVEL = "USER";
        const string MANAGERLEVEL = "MANAGER";        
        USERINFO _user;

        public UserManagement(USERINFO user)
        {
            InitializeComponent();
            _user = user;           
        }

       
        private void ResetInfo()
        {
            fullNametxt.Select();
            
            fullNametxt.Text = string.Empty;
            nictxt.Text = string.Empty;
            addresstxt.Text = string.Empty;
            userNametxt.Text = string.Empty;
            passwordtxt.Text = string.Empty;
            accesscodetxt.Text = string.Empty;
            userIdlbl.Text = string.Empty;

            userlevelCmb.Items.Clear();
            userlevelCmb.ResetText();

            userlevelCmb.Items.Add(USERLEVEL);
            userlevelCmb.Items.Add(MANAGERLEVEL);

            userlevelCmb.SelectedIndex = 0;            
            
        }

        private void InsertUserInfo()
        {
            if(String.IsNullOrEmpty(fullNametxt.Text))
                MessageBox.Show("Full name cannot be empty.");
            else if(String.IsNullOrEmpty(addresstxt.Text))
                MessageBox.Show("Address cannot be empty.");
            else if(String.IsNullOrEmpty(userNametxt.Text))
                MessageBox.Show("Username cannot be empty.");
            else if(String.IsNullOrEmpty(passwordtxt.Text))
                MessageBox.Show("Password cannot be empty.");
            else if(userlevelCmb.SelectedIndex == -1)
                MessageBox.Show("User level cannot be empty.");
            else {
                USERINFO user = new USERINFO();
                user.Address = addresstxt.Text;
                user.Birthday = birthdayDateTime.Value;
                //user.DepartmentCode=;
                //user.DesignationCode=;
                user.FullName = fullNametxt.Text;
                user.NIC = nictxt.Text;
                user.Password = passwordtxt.Text;
                if (accesscodetxt.Text == USERLEVEL)
                    user.RightCodes = System.Configuration.ConfigurationManager.AppSettings.Get("UserRightCodes");
                else if (accesscodetxt.Text == MANAGERLEVEL)
                    user.RightCodes = System.Configuration.ConfigurationManager.AppSettings.Get("ManagerRightCodes");
                user.USERCODE = userNametxt.Text;
                user.UserLevel = userlevelCmb.SelectedItem.ToString();
                user.COMPCODE = _user.COMPCODE;

                bool status = webService.InsertNewUser(user);

                if (status)
                {
                    ResetInfo();                    
                    LoadUserInfoToGridView();
                }
            }
            
        }

        private void UpdateUserInfo(string userId)
        {
            if(String.IsNullOrEmpty(fullNametxt.Text))
                MessageBox.Show("Full name cannot be empty.");
            else if(String.IsNullOrEmpty(addresstxt.Text))
                MessageBox.Show("Address cannot be empty.");
            else if(String.IsNullOrEmpty(userNametxt.Text))
                MessageBox.Show("Username cannot be empty.");
            else if(String.IsNullOrEmpty(passwordtxt.Text))
                MessageBox.Show("Password cannot be empty.");
            else if (userlevelCmb.SelectedIndex == -1)
                MessageBox.Show("User level cannot be empty.");
            else
            {
                USERINFO user = new USERINFO();
                user.Address = addresstxt.Text;
                user.Birthday = birthdayDateTime.Value;
                //user.DepartmentCode=;
                //user.DesignationCode=;
                user.FullName = fullNametxt.Text;
                user.NIC = nictxt.Text;
                user.Password = passwordtxt.Text;
                if (accesscodetxt.Text == USERLEVEL)
                    user.RightCodes = System.Configuration.ConfigurationManager.AppSettings.Get("UserRightCodes");
                else if (accesscodetxt.Text == MANAGERLEVEL)
                    user.RightCodes = System.Configuration.ConfigurationManager.AppSettings.Get("ManagerRightCodes");
                user.USERCODE = userNametxt.Text;
                user.UserLevel = userlevelCmb.SelectedItem.ToString();
                user.COMPCODE = _user.COMPCODE;

                USERINFO searchUser = new USERINFO();
                searchUser.COMPCODE = _user.COMPCODE;
                searchUser.USERCODE = userIdlbl.Text;

                bool status = webService.UpdateUser(searchUser, user);

                if (status)
                {
                    ResetInfo();                   
                    LoadUserInfoToGridView();
                }
            }
        }

        private void DeleteUserInfo(USERINFO searchUser)
        {
            bool status = webService.DeleteUser(searchUser);

            if (status)
            {
                ResetInfo();               
                LoadUserInfoToGridView();
            }
        }

        private void LoadUserInfoToGridView()
        {
            USERINFO[] users = webService.GetAllUserDetailsByCompany(_user.COMPCODE);
            gridview.DataSource = users;

            
            gridview.Columns["FullName"].DisplayIndex = 0;
            gridview.Columns["USERCODE"].DisplayIndex = 1;
            gridview.Columns["COMPCODE"].DisplayIndex = 2;
            gridview.Columns["UserLevel"].DisplayIndex = 3;
            gridview.Columns["RightCodes"].DisplayIndex = 4;
            gridview.Columns["NIC"].DisplayIndex = 5;
            gridview.Columns["Address"].DisplayIndex = 6;           
            gridview.Columns["Birthday"].DisplayIndex = 7;

            gridview.Columns["Password"].Visible = false;
            gridview.Columns["DepartmentCode"].Visible = false;
            gridview.Columns["DesignationCode"].Visible = false;
           
        }

        private void UserManagement_Load(object sender, EventArgs e)
        {
            ResetInfo();           
            LoadUserInfoToGridView(); 
        }

        private void save_Click(object sender, EventArgs e)
        {
            if(String.IsNullOrEmpty(userIdlbl.Text ))
            {
                InsertUserInfo();
            }
            else 
            {
                UpdateUserInfo(userIdlbl.Text);
            }
        }

        private void gridview_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex >= 0)
            {             
                DataGridViewRow row = this.gridview.Rows[e.RowIndex];
                userIdlbl.Text = row.Cells["USERCODE"].Value.ToString();


                USERINFO user = webService.GetUserInfo(userIdlbl.Text, row.Cells["COMPCODE"].Value.ToString());

               fullNametxt.Text = user.FullName;
               nictxt.Text = user.NIC;
               addresstxt.Text = user.Address;
               userNametxt.Text = user.USERCODE;
               passwordtxt.Text = user.Password;
               accesscodetxt.Text = user.UserLevel;
               
               if (user.UserLevel == USERLEVEL)
                   userlevelCmb.SelectedIndex = 0;
               else if (user.UserLevel == MANAGERLEVEL)
                   userlevelCmb.SelectedIndex = 1;
               
              fullNametxt.Select();                     
            }
        }

       

        private void delete_Click(object sender, EventArgs e)
        {
            if(!String.IsNullOrEmpty(userIdlbl.Text ))
            {
                USERINFO searchUser = new USERINFO();
                searchUser.USERCODE = userIdlbl.Text;
                searchUser.COMPCODE = _user.COMPCODE;

                DeleteUserInfo(searchUser);
            }
        }

        private void reset_Click(object sender, EventArgs e)
        {
            ResetInfo();            
            LoadUserInfoToGridView(); 
        }       

        private void userlevelCmb_KeyPress(object sender, KeyPressEventArgs e)
        {
            accesscodetxt.Text = userlevelCmb.SelectedItem.ToString();
        }
      
    }
}
