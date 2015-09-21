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
    public partial class UnitDetails : Form
    {
        InventoryWebService.InventoryWebService webService = new InventoryWebService.InventoryWebService();
        USERINFO _user;

        public UnitDetails(USERINFO user)
        {
            InitializeComponent();
            _user = user;

            Reset();
        }

        private void Reset()
        {
            codetxt.Select();
            codetxt.Text = string.Empty;
            desctxt.Text = string.Empty;
            IdTxt.Text = string.Empty;

            dataGridView1.DataSource = null;
            UNIT_MAST[] details = webService.GetAllUnitDetailsByCompany(_user.COMPCODE);
            dataGridView1.DataSource = details;

            SetupGridColumnOrdering();
        }

        private void SetupGridColumnOrdering()
        {            
            dataGridView1.Columns["UNIT_CODE"].DisplayIndex =0;
            dataGridView1.Columns["UNIT_DESC"].DisplayIndex = 1;
           
            dataGridView1.Columns["COMPCODE"].Visible = false;           
        }

        private void savebtn_Click(object sender, EventArgs e)
        {
            if(String.IsNullOrEmpty(codetxt.Text))
                MessageBox.Show("Code cannot be empty.");
            else if (String.IsNullOrEmpty(desctxt.Text))
                MessageBox.Show("Description cannot be empty.");
            else
            {
                UNIT_MAST details = new UNIT_MAST();
                details.UNIT_CODE = codetxt.Text;
                details.UNIT_DESC = desctxt.Text;
                details.COMPCODE = _user.COMPCODE;

                bool status = false;

                if (String.IsNullOrEmpty(IdTxt.Text))
                {
                    status = webService.InsertUnitDetails(details);
                }
                else
                {                
                    UNIT_MAST searchdetails = new UNIT_MAST();
                    searchdetails.UNIT_CODE = IdTxt.Text;                 
                    searchdetails.COMPCODE = _user.COMPCODE;

                    status = webService.UpdateUnitDetails(searchdetails, details);
                }

                if (status)
                {
                    Reset();
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(IdTxt.Text))
            {
                bool status = false;

                try
                {
                    status = webService.DeleteUnitDetails(_user.COMPCODE,  IdTxt.Text);
                }
                catch(Exception ex)
                {
                    MessageBox.Show("You cannot delete this item.");
                }

               if (status)
               {
                   Reset();
               }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Reset();
        }

        private void dataGridView1_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dataGridView1.Rows[e.RowIndex];
                IdTxt.Text = row.Cells["UNIT_CODE"].Value.ToString();

                UNIT_MAST details = webService.GetAllUnitDetailsById(_user.COMPCODE, IdTxt.Text);
                codetxt.Text = details.UNIT_CODE;
                desctxt.Text = details.UNIT_DESC;
                codetxt.Select();

            }
        }
    }
}
