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
    public partial class LocationDetails : Form
    {
        InventoryWebService.InventoryWebService webService = new InventoryWebService.InventoryWebService();
        USERINFO _user;

        public LocationDetails(USERINFO user)
        {
            InitializeComponent();
            _user = user;

            Reset();
        }

        private void Reset()
        {
            number.Select();
            number.Text = string.Empty;
            name.Text = string.Empty;
            LLabel.Text = string.Empty;
            LId.Text = string.Empty;

            dataGridView1.DataSource = null;
            LOCA_MAST[] details = webService.GetAllLocationDetails(_user.COMPCODE);
            dataGridView1.DataSource = details;

            SetupGridColumnOrdering();
        }

        private void SetupGridColumnOrdering()
        {            
            dataGridView1.Columns["LOCA_CODE"].DisplayIndex = 0;
            dataGridView1.Columns["LOCA_DESCRIPT"].DisplayIndex = 1;
            dataGridView1.Columns["LOCA_LABEL"].DisplayIndex = 2;

            dataGridView1.Columns["COMPCODE"].Visible = false;
            
        }

        private void save_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(number.Text))
                MessageBox.Show("Number cannot be empty.");
            else if (String.IsNullOrEmpty(name.Text))
                MessageBox.Show("Name cannot be empty.");
            else if (String.IsNullOrEmpty(LLabel.Text))
                MessageBox.Show("Label cannot be empty.");
            else
            {
                LOCA_MAST details = new LOCA_MAST();
                details.LOCA_CODE = number.Text;
                details.LOCA_DESCRIPT = name.Text;
                details.LOCA_LABEL = LLabel.Text;
                details.COMPCODE = _user.COMPCODE;

                bool status = false;

                if (String.IsNullOrEmpty(LId.Text))
                {
                    status = webService.InsertLocationDetails(details);
                }
                else
                {
                    LOCA_MAST searchdetails = new LOCA_MAST();
                    searchdetails.LOCA_CODE = LId.Text;
                    searchdetails.COMPCODE = _user.COMPCODE;

                    status = webService.UpdateLocationDetails(searchdetails, details);
                }

                if (status)
                {
                    Reset();
                }
            }
        }

        private void delete_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(LId.Text))
            {
                bool status = webService.DeleteLocationDetails((LId.Text), _user.COMPCODE);

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
                LId.Text = row.Cells["LOCA_CODE"].Value.ToString();

                LOCA_MAST details = webService.GetAllLocationDetailsById(_user.COMPCODE, (LId.Text));
                number.Text = details.LOCA_CODE;
                name.Text = details.LOCA_DESCRIPT;
                LLabel.Text = details.LOCA_LABEL;
                number.Select();
            }
        }

        private void LocationDetails_Load(object sender, EventArgs e)
        {

        }
    }
}
