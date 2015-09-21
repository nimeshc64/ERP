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
    public partial class CategoryDetails : Form
    {
        InventoryWebService.InventoryWebService webService = new InventoryWebService.InventoryWebService();
        USERINFO _user;

        public CategoryDetails(USERINFO user)
        {
            InitializeComponent();
            _user = user;
            ResetPage();
        }

        private void SetTabPosition()
        {
            if (tabControl1.SelectedIndex == 0)
            {
                L1CatCode.TabIndex = 0;
                L1CatDesc.TabIndex = 1;
                saveButton.TabIndex = 2;
                deleteButton.TabIndex = 3;
                resetB.TabIndex = 4;
                L1DataGrid.TabIndex = 7;
                
                L1CatCode.Select();
            }
            else if (tabControl1.SelectedIndex == 1)
            {
                L2Code.TabIndex = 1;
                L2Desc.TabIndex = 2;
                
                L2L1Code.TabIndex = 0;
                saveButton.TabIndex = 3;
                deleteButton.TabIndex = 4;
                resetB.TabIndex = 5;
                L2DataGrid.TabIndex = 6;

                L2L1Code.Select();

            }
            else if (tabControl1.SelectedIndex == 2)
            {
                L3L1Code.TabIndex = 0;
                L3L2Code.TabIndex = 1;
                L3Code.TabIndex = 2;
                L3Name.TabIndex = 3;
                L3OldCatCode.TabIndex = 4;

                saveButton.TabIndex = 5;
                deleteButton.TabIndex = 6;
                resetB.TabIndex = 7;

                L3Datagrid.TabIndex = 14;

                L3L1Code.Select();
            }
            else if (tabControl1.SelectedIndex == 3)
            {          
                L4L1Code.TabIndex = 0;
                L4L2Code.TabIndex = 1;
                L4L3Code.TabIndex = 2;
                L4Code.TabIndex = 3;
                L4Name.TabIndex = 4;
                L4Datagrid.TabIndex = 14;
                saveButton.TabIndex = 5;
                deleteButton.TabIndex = 6;
                resetB.TabIndex = 7;            
            }

        }

        private void ResetPage()
        {
            if (tabControl1.SelectedIndex == 0)
            {                
                L1CatCode.Text = string.Empty;
                L1CatDesc.Text = string.Empty;
                L1Id.Text = string.Empty;

                L1DataGrid.DataSource = null;

                CAT_MAST searchdetails = new CAT_MAST();
                searchdetails.COMPCODE = _user.COMPCODE;

                CAT_MAST[] details = webService.GetAllCategoryMasterInfo(searchdetails);
                L1DataGrid.DataSource = details;               
            }
            else if (tabControl1.SelectedIndex == 1)
            {
                L2Code.Text = string.Empty;
                L2Desc.Text = string.Empty;
                L2oldCatCode.Text = string.Empty;
                L2oldCatL2.Text = string.Empty;
                L2L1Code.Items.Clear();
                L2L1Code.ResetText();

                L2DataGrid.DataSource = null;

                CAT_MAST searchdetails = new CAT_MAST();
                searchdetails.COMPCODE = _user.COMPCODE;

                CAT_MAST[] masterDetails = webService.GetAllCategoryMasterInfo(searchdetails);

                foreach (CAT_MAST item in masterDetails)
                {
                    L2L1Code.Items.Add( item.CAT_CODE + " - " + item.CAT_DESC);
                }

                L2DataGrid.DataSource = null;
            }
            else if (tabControl1.SelectedIndex == 2)
            {
                L3Code.Text = string.Empty;
                L3Name.Text = string.Empty;
                L3OldCatCode.Text = string.Empty;
                L3oldCatCodeL2.Text = string.Empty;
                L3oldCatCodeL3.Text = string.Empty;
                L3L1Code.Items.Clear();
                L3L1Code.ResetText();
                L3L2Code.Items.Clear();
                L3L2Code.ResetText();

                CAT_MAST searchdetails = new CAT_MAST();
                searchdetails.COMPCODE = _user.COMPCODE;

                CAT_MAST[] masterDetails = webService.GetAllCategoryMasterInfo(searchdetails);
                foreach (CAT_MAST item in masterDetails)
                {
                    L3L1Code.Items.Add(item.CAT_CODE + " - " + item.CAT_DESC);
                }
                
                L3Datagrid.DataSource = null;
            }
            else if (tabControl1.SelectedIndex == 3)
            {
                L4Code.Text = string.Empty;
                L4Name.Text = string.Empty;
                L4OldCatCodeL1.Text = string.Empty;
                L4OldCatCodeL2.Text = string.Empty;
                L4OldCatCodeL3.Text = string.Empty;
                L4OldCatCodeL4.Text = string.Empty;
                L4L1Code.Items.Clear();
                L4L1Code.ResetText();
                L4L2Code.Items.Clear();
                L4L2Code.ResetText();
                L4L3Code.Items.Clear();
                L4L3Code.ResetText();

                CAT_MAST searchdetails = new CAT_MAST();
                searchdetails.COMPCODE = _user.COMPCODE;

                CAT_MAST[] masterDetails = webService.GetAllCategoryMasterInfo(searchdetails);
                foreach (CAT_MAST item in masterDetails)
                {
                    L4L1Code.Items.Add(item.CAT_CODE + " - " + item.CAT_DESC);
                }                
                L4Datagrid.DataSource = null;
            }

            SetTabPosition(); 
            SetupGridColumnOrdering();
        }

        private void SetupGridColumnOrdering()
        {
            if (tabControl1.SelectedIndex == 0)            
            {
                if (L1DataGrid.DataSource != null)
                {                    
                    L1DataGrid.Columns["CAT_CODE"].DisplayIndex = 0;
                    L1DataGrid.Columns["CAT_DESC"].DisplayIndex = 1;
                    L1DataGrid.Columns["USERCODE"].DisplayIndex = 2;
                    L1DataGrid.Columns["CHANGED"].DisplayIndex = 3;
                    L1DataGrid.Columns["CHANGEDDATE"].DisplayIndex = 4;

                    L1DataGrid.Columns["COMPCODE"].Visible = false;
                    L1DataGrid.Columns["REMOVE"].Visible = false;
                    L1DataGrid.Columns["GlCode"].Visible = false;
                    L1DataGrid.Columns["Purch_GlCode"].Visible = false;                    
                }
            }
            else if (tabControl1.SelectedIndex == 1)
            {
                if (L2DataGrid.DataSource != null)
                {
                    L2DataGrid.Columns["CATCODE_L2"].DisplayIndex = 0;
                    L2DataGrid.Columns["CAT_DESC"].DisplayIndex = 1;
                    L2DataGrid.Columns["CATCODE"].DisplayIndex = 2;                    
                    L2DataGrid.Columns["USERCODE"].DisplayIndex = 3;
                    L2DataGrid.Columns["CHANGED"].DisplayIndex = 4;
                    L2DataGrid.Columns["CHANGEDDATE"].DisplayIndex = 5;

                    L2DataGrid.Columns["COMPCODE"].Visible = false;
                    L2DataGrid.Columns["REMOVE"].Visible = false;
                    L2DataGrid.Columns["GlCode"].Visible = false;
                    L2DataGrid.Columns["Purch_GlCode"].Visible = false;   
                }              
            }
            else if (tabControl1.SelectedIndex == 2)
            {
                if (L3Datagrid.DataSource != null)
                {
                    L3Datagrid.Columns["CATCODE_L3"].DisplayIndex = 0;
                    L3Datagrid.Columns["CAT_DESC"].DisplayIndex = 1;
                    L3Datagrid.Columns["CATCODE"].DisplayIndex = 2;
                    L3Datagrid.Columns["CATCODE_L2"].DisplayIndex = 3;                   
                    L3Datagrid.Columns["USERCODE"].DisplayIndex = 4;
                    L3Datagrid.Columns["CHANGED"].DisplayIndex = 5;
                    L3Datagrid.Columns["CHANGEDDATE"].DisplayIndex = 6;

                    L3Datagrid.Columns["COMPCODE"].Visible = false;
                    L3Datagrid.Columns["REMOVE"].Visible = false;
                    L3Datagrid.Columns["GlCode"].Visible = false;
                    L3Datagrid.Columns["Purch_GlCode"].Visible = false; 
                }
            }
            else if (tabControl1.SelectedIndex == 3)
            {
                if (L4Datagrid.DataSource != null)
                {
                    L4Datagrid.Columns["CATCODE_L4"].DisplayIndex = 0;
                    L4Datagrid.Columns["CAT_DESC"].DisplayIndex = 1;
                    L4Datagrid.Columns["CATCODE"].DisplayIndex = 2;
                    L4Datagrid.Columns["CATCODE_L2"].DisplayIndex = 3;
                    L4Datagrid.Columns["CATCODE_L3"].DisplayIndex = 4;
                    L4Datagrid.Columns["USERCODE"].DisplayIndex = 5;
                    L4Datagrid.Columns["CHANGED"].DisplayIndex = 6;
                    L4Datagrid.Columns["CHANGEDDATE"].DisplayIndex = 7;

                    L4Datagrid.Columns["COMPCODE"].Visible = false;
                    L4Datagrid.Columns["REMOVE"].Visible = false;
                    L4Datagrid.Columns["GlCode"].Visible = false;
                    L4Datagrid.Columns["Purch_GlCode"].Visible = false; 
                }
            }
           
        }


        private void saveButton_Click(object sender, EventArgs e)
        {
            try
            {
                #region Level1
                if (tabControl1.SelectedIndex == 0)
                {
                    if (String.IsNullOrEmpty(L1CatCode.Text))
                        MessageBox.Show("Code cannot be empty.");
                    else if (String.IsNullOrEmpty(L1CatDesc.Text))
                        MessageBox.Show("Description cannot be empty.");
                    else
                    {
                        CAT_MAST details = new CAT_MAST();
                        details.CAT_CODE = L1CatCode.Text;
                        details.CAT_DESC = L1CatDesc.Text;
                        details.COMPCODE = _user.COMPCODE;
                        details.USERCODE = _user.USERCODE;
                        details.REMOVE = 0;

                        bool status = false;

                        if (String.IsNullOrEmpty(L1Id.Text))
                        {
                            status = webService.InsertCategoryMasterInfo(details);
                        }
                        else
                        {
                            CAT_MAST searchdetails = new CAT_MAST();
                            searchdetails.CAT_CODE = L1Id.Text;
                            searchdetails.COMPCODE = _user.COMPCODE;

                            status = webService.UpdateCategoryMasterInfo(searchdetails, details);
                        }

                        if (status)
                        {
                            ResetPage();
                        }
                    }

                }
                #endregion

                #region Level2
                else if (tabControl1.SelectedIndex == 1)
                {
                    if (String.IsNullOrEmpty(L2Code.Text))
                        MessageBox.Show("Code cannot be empty.");
                    else if (String.IsNullOrEmpty(L2Desc.Text))
                        MessageBox.Show("Description cannot be empty.");
                    else if (L2L1Code.Items == null || L2L1Code.Items.Count == 0 || L2L1Code.SelectedItem == null)
                        MessageBox.Show("Level-1 cannot be empty.");
                    else
                    {
                        CAT_L2 details = new CAT_L2();
                        details.CAT_DESC = L2Desc.Text;
                        details.CATCODE_L2 = L2Code.Text;
                        details.CATCODE = (L2L1Code.Text.Split(new string[] { " - " }, StringSplitOptions.None)[0]);
                        details.USERCODE = _user.USERCODE;
                        details.COMPCODE = _user.COMPCODE;
                        details.REMOVE = 0;

                        bool status = false;

                        if (String.IsNullOrEmpty(L2oldCatL2.Text))
                        {
                            status = webService.InsertCategoryL2Info(details);
                        }
                        else
                        {
                            CAT_L2 searchdetails = new CAT_L2();
                            searchdetails.CATCODE_L2 = L2oldCatL2.Text;
                            searchdetails.CATCODE = L2oldCatCode.Text;
                            searchdetails.COMPCODE = _user.COMPCODE;

                            status = webService.UpdateCategoryL2Info(searchdetails, details);
                        }

                        if (status)
                        {
                            // ResetPage();
                            L2Code.Text = "";
                            L2Desc.Text = "";
                            KeyPressEventArgs arg = new KeyPressEventArgs(Convert.ToChar(Keys.Enter));
                            L2L1Code_KeyPress(sender, arg);
                            L2Code.Focus();

                        }
                    }

                }
                #endregion

                #region Level3
                else if (tabControl1.SelectedIndex == 2)
                {
                    if (String.IsNullOrEmpty(L3Code.Text))
                        MessageBox.Show("Code cannot be empty.");
                    else if (String.IsNullOrEmpty(L3Name.Text))
                        MessageBox.Show("Description cannot be empty.");
                    else if (L3L1Code.Items == null || L3L1Code.Items.Count == 0 || L3L1Code.SelectedItem == null)
                        MessageBox.Show("Level-1 cannot be empty.");
                    else if (L3L2Code.Items == null || L3L2Code.Items.Count == 0 || L3L2Code.SelectedItem == null)
                        MessageBox.Show("Level-2 cannot be empty.");
                    else
                    {
                        CAT_L3 details = new CAT_L3();
                        details.CATCODE_L3 = L3Code.Text;
                        details.CAT_DESC = L3Name.Text;
                        details.USERCODE = _user.USERCODE;
                        details.COMPCODE = _user.COMPCODE;
                        details.CATCODE = (L3L1Code.Text.Split(new string[] { " - " }, StringSplitOptions.None)[0]);
                        details.CATCODE_L2 = (L3L2Code.Text.Split(new string[] { " - " }, StringSplitOptions.None)[0]);
                        details.REMOVE = 0;
                        bool status = false;

                        if (String.IsNullOrEmpty(L3oldCatCodeL3.Text))
                        {
                            status = webService.InsertCategoryL3Info(details);
                        }
                        else
                        {
                            CAT_L3 searchdetails = new CAT_L3();
                            searchdetails.CATCODE_L3 = L3oldCatCodeL3.Text;
                            searchdetails.CATCODE_L2 = L3oldCatCodeL2.Text;
                            searchdetails.CATCODE = L3OldCatCode.Text;
                            searchdetails.COMPCODE = _user.COMPCODE;
                            status = webService.UpdateCategoryL3Info(searchdetails, details);
                        }

                        if (status)
                        {
                            ResetPage();
                        }
                    }
                }
                #endregion

                #region Level4
                else if (tabControl1.SelectedIndex == 3)
                {
                    if (String.IsNullOrEmpty(L4Code.Text))
                        MessageBox.Show("Code cannot be empty.");
                    else if (String.IsNullOrEmpty(L4Name.Text))
                        MessageBox.Show("Description cannot be empty.");
                    else if (L4L1Code.Items == null || L4L1Code.Items.Count == 0 || L4L1Code.SelectedItem == null)
                        MessageBox.Show("Level-1 cannot be empty.");
                    else if (L4L2Code.Items == null || L4L2Code.Items.Count == 0 || L4L2Code.SelectedItem == null)
                        MessageBox.Show("Level-2 cannot be empty.");
                    else if (L4L3Code.Items == null || L4L3Code.Items.Count == 0 || L4L3Code.SelectedItem == null)
                        MessageBox.Show("Level-3 cannot be empty.");
                    else
                    {
                        CAT_L4 details = new CAT_L4();
                        details.CATCODE_L4 = L4Code.Text;
                        details.CAT_DESC = L4Name.Text;
                        details.USERCODE = _user.USERCODE;
                        details.COMPCODE = _user.COMPCODE;
                        details.CATCODE = (L4L1Code.Text.Split(new string[] { " - " }, StringSplitOptions.None)[0]);
                        details.CATCODE_L2 = (L4L2Code.Text.Split(new string[] { " - " }, StringSplitOptions.None)[0]);
                        details.CATCODE_L3 = (L4L3Code.Text.Split(new string[] { " - " }, StringSplitOptions.None)[0]);
                        details.REMOVE = 0;

                        bool status = false;

                        if (String.IsNullOrEmpty(L4OldCatCodeL4.Text))
                        {
                            status = webService.InsertCategoryL4Info(details);
                        }
                        else
                        {
                            CAT_L4 searchdetails = new CAT_L4();
                            searchdetails.CATCODE = L4OldCatCodeL1.Text;
                            searchdetails.CATCODE_L2 = L4OldCatCodeL2.Text;
                            searchdetails.CATCODE_L3 = L4OldCatCodeL3.Text;
                            searchdetails.CATCODE_L4 = L4OldCatCodeL4.Text;
                            searchdetails.COMPCODE = _user.COMPCODE;

                            status = webService.UpdateCategoryL4Info(searchdetails, details);
                        }

                        if (status)
                        {
                            ResetPage();
                        }
                    }
                }
                #endregion
            }
            catch
            {
                MessageBox.Show("Invalid Value Entered", "COMPULIN ERP",
                MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
            }
        }      


        private void L1DataGrid_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (tabControl1.SelectedIndex == 0)
            {
                if (e.RowIndex >= 0)
                {
                    DataGridViewRow row = this.L1DataGrid.Rows[e.RowIndex];
                    L1Id.Text = row.Cells["CAT_CODE"].Value.ToString();

                    CAT_MAST searchdetails = new CAT_MAST();
                    searchdetails.CAT_CODE = L1Id.Text;
                    searchdetails.COMPCODE = _user.COMPCODE;

                    CAT_MAST details = webService.GetAllCategoryDetailsById(searchdetails);
                    L1CatCode.Text = details.CAT_CODE;
                    L1CatDesc.Text = details.CAT_DESC;

                    SetTabPosition();
                    
                }
            }
           
        }

        private void L2DataGrid_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.L2DataGrid.Rows[e.RowIndex];
                L2oldCatCode.Text = row.Cells["CATCODE"].Value.ToString();
                L2oldCatL2.Text = row.Cells["CATCODE_L2"].Value.ToString();                   

                CAT_L2 searchdetails = new CAT_L2();
                searchdetails.CATCODE_L2 = L2oldCatL2.Text;
                searchdetails.CATCODE = L2oldCatCode.Text;
                searchdetails.COMPCODE = _user.COMPCODE;

                CAT_L2 details = webService.GetAllL2CategoryDetailsById(searchdetails);
                L2Code.Text = details.CATCODE_L2;
                L2Desc.Text = details.CAT_DESC;

                int count = 0;
                foreach (var item in L2L1Code.Items)
                {
                    if ((item.ToString().Split(new string[] { " - " }, StringSplitOptions.None)[0]) == L2oldCatCode.Text)
                    {
                        L2L1Code.SelectedIndex = count;
                        break;
                    }
                    count++;
                }

                SetTabPosition();
            }
        }

        private void L3Datagrid_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.L3Datagrid.Rows[e.RowIndex];
                L3OldCatCode.Text = row.Cells["CATCODE"].Value.ToString();
                L3oldCatCodeL2.Text = row.Cells["CATCODE_L2"].Value.ToString();
                L3oldCatCodeL3.Text = row.Cells["CATCODE_L3"].Value.ToString();

                CAT_L3 searchdetails = new CAT_L3();
                searchdetails.CATCODE_L3 = L3oldCatCodeL3.Text;
                searchdetails.CATCODE_L2 = L3oldCatCodeL2.Text;
                searchdetails.CATCODE = L3OldCatCode.Text;
                searchdetails.COMPCODE = _user.COMPCODE;

                CAT_L3 details = webService.GetAllL3CategoryDetailsById(searchdetails);
                L3Code.Text = details.CATCODE_L3;
                L3Name.Text = details.CAT_DESC;

                int count = 0;
                foreach (var item in L3L1Code.Items)
                {
                    if ((item.ToString().Split(new string[] { " - " }, StringSplitOptions.None)[0]) == details.CATCODE)
                    {
                        L3L1Code.SelectedIndex = count;
                        break;
                    }
                    count++;
                }
                count = 0;
                foreach (var item in L3L2Code.Items)
                {
                    if ((item.ToString().Split(new string[] { " - " }, StringSplitOptions.None)[0]) == details.CATCODE_L2)
                    {
                        L3L2Code.SelectedIndex = count;
                        break;
                    }
                    count++;
                }

                SetTabPosition();
            }
        }

        private void L4Datagrid_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.L4Datagrid.Rows[e.RowIndex];
                L4OldCatCodeL1.Text = row.Cells["CATCODE"].Value.ToString();
                L4OldCatCodeL2.Text = row.Cells["CATCODE_L2"].Value.ToString();
                L4OldCatCodeL3.Text = row.Cells["CATCODE_L3"].Value.ToString();
                L4OldCatCodeL4.Text = row.Cells["CATCODE_L4"].Value.ToString();

                CAT_L4 searchdetails = new CAT_L4();
                searchdetails.CATCODE_L4 = L4OldCatCodeL4.Text;
                searchdetails.CATCODE_L3 = L4OldCatCodeL3.Text;
                searchdetails.CATCODE_L2 = L4OldCatCodeL2.Text;
                searchdetails.CATCODE = L4OldCatCodeL1.Text;
                searchdetails.COMPCODE = _user.COMPCODE;

                CAT_L4 details = webService.GetAllL4CategoryDetailsById(searchdetails);
                L4Code.Text = details.CATCODE_L4;
                L4Name.Text = details.CAT_DESC;
              
                int count = 0;
                foreach (var item in L4L1Code.Items)
                {
                    if ((item.ToString().Split(new string[] { " - " }, StringSplitOptions.None)[0]) == details.CATCODE)
                    {
                        L4L1Code.SelectedIndex = count;
                        break;
                    }
                    count++;
                }
                count = 0;
                foreach (var item in L4L2Code.Items)
                {
                    if ((item.ToString().Split(new string[] { " - " }, StringSplitOptions.None)[0]) == details.CATCODE_L2)
                    {
                        L4L2Code.SelectedIndex = count;
                        break;
                    }
                    count++;
                }
                count = 0;
                foreach (var item in L4L3Code.Items)
                {
                    if ((item.ToString().Split(new string[] { " - " }, StringSplitOptions.None)[0]) == details.CATCODE_L3)
                    {
                        L4L3Code.SelectedIndex = count;
                        break;
                    }
                    count++;
                }

                SetTabPosition();
            }
        }

        private void resetB_Click(object sender, EventArgs e)
        {
            ResetPage();
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 0)
            {
                bool status = false;

                if (!String.IsNullOrEmpty(L1Id.Text))
                {
                    CAT_MAST searchdetails = new CAT_MAST();
                    searchdetails.CAT_CODE = L1Id.Text;
                    searchdetails.COMPCODE = _user.COMPCODE;

                    status = webService.DeleteCategoryMasterInfo(searchdetails);
                }   
    
                if (status)
                {
                    ResetPage();
                }

            }
            else if (tabControl1.SelectedIndex == 1)
            {
                bool status = false;

                if (!String.IsNullOrEmpty(L2oldCatL2.Text))
                {
                    CAT_L2 searchdetails = new CAT_L2();
                    searchdetails.CATCODE_L2 = L2oldCatL2.Text;
                    searchdetails.CATCODE = L2oldCatCode.Text;
                    searchdetails.COMPCODE = _user.COMPCODE;

                    status = webService.DeleteCategoryL2Info(searchdetails);
                }

                if (status)
                {
                    ResetPage();
                }
            }
            else if (tabControl1.SelectedIndex == 2)
            {
                bool status = false;

                if (!String.IsNullOrEmpty(L3oldCatCodeL3.Text))
                {
                    CAT_L3 searchdetails = new CAT_L3();
                    searchdetails.CATCODE_L3 = L3oldCatCodeL3.Text;
                    searchdetails.CATCODE_L2 = L3oldCatCodeL2.Text;
                    searchdetails.CATCODE = L3OldCatCode.Text;
                    searchdetails.COMPCODE = _user.COMPCODE;

                    status = webService.DeleteCategoryL3Info(searchdetails);
                }

                if (status)
                {
                    ResetPage();
                }
            }
            else if (tabControl1.SelectedIndex == 3)
            {
                bool status = false;

                if (!String.IsNullOrEmpty(L4OldCatCodeL4.Text))
                {
                    CAT_L4 searchdetails = new CAT_L4();
                    searchdetails.CATCODE_L4 = L4OldCatCodeL4.Text;
                    searchdetails.CATCODE_L3 = L4OldCatCodeL3.Text;
                    searchdetails.CATCODE_L2 = L4OldCatCodeL2.Text;
                    searchdetails.CATCODE = L4OldCatCodeL1.Text;
                    searchdetails.COMPCODE = _user.COMPCODE;

                    status = webService.DeleteCategoryL4Info(searchdetails);
                }

                if (status)
                {
                    ResetPage();
                }
            }
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ResetPage();
        }

        private void L2L1Code_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                CAT_L2 searchdetails = new CAT_L2();
                searchdetails.CATCODE = (L2L1Code.SelectedItem.ToString().Split(new string[] { " - " }, StringSplitOptions.None)[0]);
                searchdetails.COMPCODE = _user.COMPCODE;

                CAT_L2[] details = webService.GetAllL2CategoryDetailsByMasterId(searchdetails);
                L2DataGrid.DataSource = details;

                SetupGridColumnOrdering();
            }
            catch
            {
                MessageBox.Show("Invalid Value Entered", "COMPULIN ERP",
                MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
            }
        }

        private void L3L1Code_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                L3L2Code.Items.Clear();
                L3L2Code.ResetText();

                CAT_L2 searchdetails = new CAT_L2();
                searchdetails.CATCODE = L3L1Code.SelectedItem.ToString().Split(new string[] { " - " }, StringSplitOptions.None)[0];
                searchdetails.COMPCODE = _user.COMPCODE;

                CAT_L2[] l2Details = webService.GetAllL2CategoryDetailsByMasterId(searchdetails);
                foreach (CAT_L2 item in l2Details)
                {
                    L3L2Code.Items.Add(item.CATCODE_L2 + " - " + item.CAT_DESC);
                }
                if (l2Details != null && l2Details.Count() > 0)
                    L3L2Code.SelectedIndex = 0;

                L3Datagrid.DataSource = null;
            }
            catch
            {
                MessageBox.Show("Invalid Value Entered", "COMPULIN ERP",
                MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
            }
        }

        private void L3L2Code_KeyPress(object sender, KeyPressEventArgs e)
        {
            L3Datagrid.DataSource = null;

            CAT_L3 searchdetails = new CAT_L3();           
            searchdetails.CATCODE_L2 = (L3L2Code.Text.Split(new string[] { " - " }, StringSplitOptions.None)[0]);
            searchdetails.CATCODE = (L3L1Code.Text.Split(new string[] { " - " }, StringSplitOptions.None)[0]);
            searchdetails.COMPCODE = _user.COMPCODE;

            CAT_L3[] details = webService.GetAllL3CategoryDetailsByMastCatId(searchdetails);
            L3Datagrid.DataSource = details;

            SetupGridColumnOrdering();
        }

        private void L4L1Code_KeyPress(object sender, KeyPressEventArgs e)
        {
            L4L2Code.Items.Clear();
            L4L2Code.ResetText();

            CAT_L2 searchdetails = new CAT_L2();
            searchdetails.CATCODE = L4L1Code.SelectedItem.ToString().Split(new string[] { " - " }, StringSplitOptions.None)[0];
            searchdetails.COMPCODE = _user.COMPCODE;

            CAT_L2[] l2Details = webService.GetAllL2CategoryDetailsByMasterId(searchdetails);
            foreach (CAT_L2 item in l2Details)
            {
                L4L2Code.Items.Add(item.CATCODE_L2 + " - " + item.CAT_DESC);
            }
            if (l2Details != null && l2Details.Count() > 0)
                L4L2Code.SelectedIndex = 0;

            L4Datagrid.DataSource = null;           
        }

        private void L4L2Code_KeyPress(object sender, KeyPressEventArgs e)
        {
            L4L3Code.Items.Clear();
            L4L3Code.ResetText();

            CAT_L3 searchdetails = new CAT_L3();
            searchdetails.CATCODE_L2 = (L4L2Code.Text.Split(new string[] { " - " }, StringSplitOptions.None)[0]);
            searchdetails.CATCODE = (L4L1Code.Text.Split(new string[] { " - " }, StringSplitOptions.None)[0]);
            searchdetails.COMPCODE = _user.COMPCODE;

            CAT_L3[] l2Details = webService.GetAllL3CategoryDetailsByMastCatId(searchdetails);
            foreach (CAT_L3 item in l2Details)
            {
                L4L3Code.Items.Add(item.CATCODE_L3 + " - " + item.CAT_DESC);
            }

            if (l2Details != null && l2Details.Count() > 0)
                L4L3Code.SelectedIndex = 0;

            L4Datagrid.DataSource = null;            
        }

        private void L4L3Code_KeyPress(object sender, KeyPressEventArgs e)
        {
            L4Datagrid.DataSource = null;
            CAT_L4 searchdetails = new CAT_L4();            
            searchdetails.CATCODE_L3 = L4L3Code.Text.Split(new string[] { " - " }, StringSplitOptions.None)[0];
            searchdetails.CATCODE_L2 = L4L2Code.Text.Split(new string[] { " - " }, StringSplitOptions.None)[0];
            searchdetails.CATCODE = L4L1Code.Text.Split(new string[] { " - " }, StringSplitOptions.None)[0];
            searchdetails.COMPCODE = _user.COMPCODE;

            CAT_L4[] details = webService.GetAllL4CategoryDetailsByMastCatId(searchdetails);
            L4Datagrid.DataSource = details;

            SetupGridColumnOrdering();
        }


       
    }
}
