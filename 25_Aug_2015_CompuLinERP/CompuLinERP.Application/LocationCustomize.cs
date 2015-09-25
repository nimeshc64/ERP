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
    public partial class LocationCustomize : Form
    {
        InventoryWebService.InventoryWebService webService = new InventoryWebService.InventoryWebService();
        USERINFO _user;

        public LocationCustomize(USERINFO user)
        {
            InitializeComponent();
            _user = user;

            Reset();
        }

        private void Reset()
        {           
            company.Text = string.Empty;
            location.Text = string.Empty;
            LId.Text = string.Empty;

            dataGridView1.DataSource = null;
            LOCA_MAST[] details = webService.GetAllLocationDetails(_user.COMPCODE);
            dataGridView1.DataSource = details;
            LOCA_DETAIL[] data = webService.GetAllLocationCustomize(_user.USERCODE);
            dataGridView2.DataSource = data;
            FillUserNames();
            SetupGridColumnOrdering();
        }

        private void FillUserNames()
        {
            if(_user.USERCODE=="Admin")
            {
                USERINFO[] names = webService.GetAllUsersDetails();
                foreach (USERINFO item in names)
                {
                    comboBox1.Items.Add(item.USERCODE);
                    comboBox1.SelectedItem = _user.USERCODE;
                }
            }
            else
            {
                comboBox1.Enabled = false;
                comboBox1.Items.Add(_user.USERCODE);
                comboBox1.SelectedItem = _user.USERCODE;
            }
           
        }

        private void SetupGridColumnOrdering()
        {
            dataGridView1.Columns["COMPCODE"].DisplayIndex = 0;
            dataGridView1.Columns["LOCA_CODE"].DisplayIndex = 1;
            dataGridView1.Columns["LOCA_DESCRIPT"].DisplayIndex = 2;
            dataGridView1.Columns["LOCA_LABEL"].DisplayIndex = 3;
        }

        private void dataGridView1_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dataGridView1.Rows[e.RowIndex];
                LId.Text = row.Cells["LOCA_CODE"].Value.ToString();

                LOCA_MAST details = webService.GetAllLocationDetailsById(_user.COMPCODE, (LId.Text));
                company.Text = details.COMPCODE;
                location.Text = details.LOCA_CODE;
            }
        }

        private void save_Click(object sender, EventArgs e)
        {
            bool checkST = false;
            checkST = webService.LocationCustomizeById(company.Text, Convert.ToString(comboBox1.SelectedItem), location.Text);
            if (String.IsNullOrEmpty(company.Text))
                MessageBox.Show("Company Code cannot be empty.");
            else if (comboBox1.SelectedIndex == -1)
                MessageBox.Show("User Name cannot be empty.");
            else if (String.IsNullOrEmpty(location.Text))
                MessageBox.Show("Location cannot be empty.");          
            else if (checkST == true)
                MessageBox.Show("The Data already existed");           
            else
            {
                LOCA_DETAIL details = new LOCA_DETAIL();
                details.COMPCODE = company.Text;
                details.USERCODE = Convert.ToString(comboBox1.SelectedItem);
                details.LOCACODE = location.Text;
                bool status = false;

                if (!String.IsNullOrEmpty(company.Text) && !String.IsNullOrEmpty(Convert.ToString(comboBox1.SelectedItem)) && !String.IsNullOrEmpty(location.Text))
                {
                    status = webService.InsertLocationCustomize(details);
                }
                else
                {

                    LOCA_DETAIL searchdetails = new LOCA_DETAIL();
                    searchdetails.LOCACODE = location.Text;
                    searchdetails.COMPCODE = company.Text;
                    searchdetails.USERCODE = Convert.ToString(comboBox1.SelectedItem);

                    status = webService.UpdateLocationCustomize(searchdetails, details);
                }

                if (status)
                {
                    Reset();
                }
            }
        }

        private void delete_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(company.Text) && !String.IsNullOrEmpty(Convert.ToString(comboBox1.SelectedItem)) && !String.IsNullOrEmpty(location.Text))
            {
                bool status = webService.DeleteLocationCustomize(company.Text,Convert.ToString(comboBox1.SelectedItem),location.Text);

                if (status)
                {
                    Reset();
                }
            }
        }

        private void resetB_Click(object sender, EventArgs e)
        {
            Reset();
        }

        private void dataGridView2_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dataGridView2.Rows[e.RowIndex];
                company.Text  = row.Cells["COMPCODE"].Value.ToString();
                location.Text = row.Cells["LOCACODE"].Value.ToString();
                comboBox1.SelectedItem = row.Cells["USERCODE"].Value.ToString();
            }
        }

        //private bool check(string company,string user, string location)
        //{
        //    bool status = false;
        //    for (int i = 0; i < dataGridView2.Rows.Count; i++)
        //    {
        //        for (int j = 0; j < dataGridView2.Columns.Count; j++)
        //        {
        //            if (company == dataGridView2.Rows[i].Cells[j].Value.ToString())
        //            {
        //                status = true;
        //            }
        //        }
        //    }
        //    return status;
        //}
    }
}
