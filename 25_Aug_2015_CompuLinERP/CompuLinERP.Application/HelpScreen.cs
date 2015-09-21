using CompuLinERP.WIN.InventoryWebService;
using CompuLinINV.WIN.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows.Forms;

namespace CompuLinERP.WIN
{
    public partial class HelpScreen : Form
    {
        InventoryWebService.InventoryWebService webService = new InventoryWebService.InventoryWebService();       
        public string _fromHelpValue; 
        public string _toHelpValue;
        public List<string> AllCodes;
        SearchCriteria _search;
        string _firstColumnName;

        public HelpScreen(SearchCriteria search)
        {
            InitializeComponent();
            _search = search;
            
            LoadGridInfo();
        }

        private void LoadGridInfo()
        {
            F2HELP_MAST hELP_MAST = webService.GetAllF2HELP_MASTDetailsBySeqNo(_search.SequenceNo);
            SetupGridColumnOrdering(hELP_MAST);
        }

        private void SetupGridColumnOrdering(F2HELP_MAST hELP_MAST)
        {
            var detailedObject = webService.GetAllDetailedObjectInfo(hELP_MAST.TABLE_NAME, _search.CompanyCode, _search.Location, _search.SearchStartingCharacters ,"");
            dataGridView1.DataSource = detailedObject;
            foreach (DataGridViewColumn item in dataGridView1.Columns)
            {
                item.Visible = false;
            }

            if (!String.IsNullOrEmpty(hELP_MAST.F1))
            {
                _firstColumnName = hELP_MAST.F1;
                dataGridView1.Columns[hELP_MAST.F1].DisplayIndex = 0;
                dataGridView1.Columns[hELP_MAST.F1].Visible = true;
            }
            if (!String.IsNullOrEmpty(hELP_MAST.F2))
            {
                dataGridView1.Columns[hELP_MAST.F2].DisplayIndex = 1;
                dataGridView1.Columns[hELP_MAST.F2].Visible = true;
            }
            if (!String.IsNullOrEmpty(hELP_MAST.F3))
            {
                dataGridView1.Columns[hELP_MAST.F3].DisplayIndex = 2;
                dataGridView1.Columns[hELP_MAST.F3].Visible = true;
            }
            if (!String.IsNullOrEmpty(hELP_MAST.F4))
            {
                dataGridView1.Columns[hELP_MAST.F4].DisplayIndex = 3;
                dataGridView1.Columns[hELP_MAST.F4].Visible = true;
            }
            if (!String.IsNullOrEmpty(hELP_MAST.F5))
            {
                dataGridView1.Columns[hELP_MAST.F5].DisplayIndex = 4;
                dataGridView1.Columns[hELP_MAST.F5].Visible = true;
            }
            if (!String.IsNullOrEmpty(hELP_MAST.F6))
            {
                dataGridView1.Columns[hELP_MAST.F6].DisplayIndex = 5;
                dataGridView1.Columns[hELP_MAST.F6].Visible = true;
            }
            if (!String.IsNullOrEmpty(hELP_MAST.F7))
            {
                dataGridView1.Columns[hELP_MAST.F7].DisplayIndex = 6;
                dataGridView1.Columns[hELP_MAST.F7].Visible = true;
            }
            if (!String.IsNullOrEmpty(hELP_MAST.F8))
            {
                dataGridView1.Columns[hELP_MAST.F8].DisplayIndex = 7;
                dataGridView1.Columns[hELP_MAST.F8].Visible = true;
            }

            columnName.Text = hELP_MAST.F1;
        }

      

        private void LoadSelectedRows()
        {
            Int32 selectedRowCount =
       dataGridView1.Rows.GetRowCount(DataGridViewElementStates.Selected);
            if (selectedRowCount > 0)
            {
                List<string> itemsList = new List<string>();

                for (int i = 0; i < selectedRowCount; i++)
                {
                    if(i==0)
                        _fromHelpValue = dataGridView1.SelectedRows[i].Cells[_firstColumnName].Value.ToString();

                    if (i + 1 == selectedRowCount)
                        _toHelpValue = dataGridView1.SelectedRows[i].Cells[_firstColumnName].Value.ToString();

                    itemsList.Add(dataGridView1.SelectedRows[i].Cells[_firstColumnName].Value.ToString());
                   
                    textBox1.Text = _fromHelpValue;
                    textBox2.Text = _toHelpValue;
                }

                _fromHelpValue = textBox1.Text;
                _toHelpValue = textBox2.Text;
                AllCodes = itemsList;
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            LoadSelectedRows();
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                Int32 selectedRowCount =
      dataGridView1.Rows.GetRowCount(DataGridViewElementStates.Selected);
                if (selectedRowCount > 0)
                {
                    for (int i = 0; i < selectedRowCount; i++)
                    {
                        if (i == 0)
                            textBox1.Text = dataGridView1.SelectedRows[i].Cells[_firstColumnName].Value.ToString();

                        if (i + 1 == selectedRowCount)
                            textBox2.Text = dataGridView1.SelectedRows[i].Cells[_firstColumnName].Value.ToString();                        
                    }
                }
                else
                {

                    DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                    columnName.Text = dataGridView1.Columns[e.ColumnIndex].HeaderText;
                    textBox1.Text = row.Cells[_firstColumnName].Value.ToString();
                    textBox2.Text = row.Cells[_firstColumnName].Value.ToString();
                }

               _fromHelpValue= textBox1.Text;
               _toHelpValue= textBox2.Text;

               this.Hide();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
           // string loca = (locationComboBox.SelectedItem.ToString().Split(new string[] { " - " }, StringSplitOptions.None)[0]);
                        

            if (_search.TableName == "ITEM_MAST")
            {
                var detailedObject = webService.GetAllFilteredInfo("ITEM_MAST", textBox1.Text.Trim(), _search.Location , _search.CompanyCode, columnName.Text.ToUpper());
                dataGridView1.DataSource = detailedObject;
            }
            else if (_search.TableName == "CUST_MAST")
            {
                var detailedObject = webService.GetAllFilteredInfo("CUST_MAST", textBox1.Text.Trim(), "", _search.CompanyCode , columnName.Text.ToUpper());
                dataGridView1.DataSource = detailedObject;
            }
            else if (_search.TableName == "SUPP_MAST")
            {
                var detailedObject = webService.GetAllFilteredInfo("SUPP_MAST", textBox1.Text.Trim(), "", _search.CompanyCode, columnName.Text.ToUpper());
                dataGridView1.DataSource = detailedObject;
            }

            
            
        }

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return )
            {               
                if (dataGridView1.SelectedCells.Count > 0)
                {
                    int selectedrowindex = dataGridView1.SelectedCells[0].RowIndex;

                    DataGridViewRow selectedRow = dataGridView1.Rows[selectedrowindex];

                    _fromHelpValue = selectedRow.Cells[_firstColumnName].Value.ToString();
                    _toHelpValue =selectedRow.Cells[_firstColumnName].Value.ToString();

                    this.Hide();

                }
            }
        }

        private void HelpScreen_Load(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.SelectedCells.Count > 0)
            {
                
                columnName.Text = dataGridView1.Columns[e.ColumnIndex].Name ;
                

            }
        }




       
    }
}
