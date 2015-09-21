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
using System.Windows.Forms;

namespace CompuLinERP.WIN
{
    public partial class ItemDetails : Form
    {
        InventoryWebService.InventoryWebService webService = new InventoryWebService.InventoryWebService();
        USERINFO _user;
        DataTable _table;

        public ItemDetails(USERINFO user)
        {
            InitializeComponent();
            _user = user;
            Reset(0);
            locationComboBox.SelectedIndex = 0;
        }

        private void Reset(int option)
        {
            LId.Text = string.Empty;
            oldLocation.Text = string.Empty;
            itemCode.Text = string.Empty;
            itemDesc1.Text = string.Empty;
            itemDesc2.Text = string.Empty;
            isServiceItem.Checked = false;
            catL1Combo.Items.Clear();
            catL1Combo.ResetText();
            catL2Combo.Items.Clear();
            catL2Combo.ResetText();
            CatL3Combo.Items.Clear();
            CatL3Combo.ResetText();
            catL4Combo.Items.Clear();
            catL4Combo.ResetText();
            unitCombo.Items.Clear();
            unitCombo.ResetText();
            packsize.Text = string.Empty;
            packUnit.Items.Clear();
            packUnit.ResetText();
            lengthDetails.Text = string.Empty;
            lengthUnit.Items.Clear();
            lengthUnit.ResetText();
            widthDetails.Text = string.Empty;
            widthUnit.Items.Clear();
            widthUnit.ResetText();
            minStock.Text = string.Empty;
            maxStock.Text = string.Empty;
            reorderLevel.Text = string.Empty;
            ReOrderQty.Text = string.Empty;
            id.Text = string.Empty;
            addItemsToAllLocations.Checked = false;
            isActiveItem.Checked = false;
            storeStocksWithLU.Checked = false;
            suppliers.Items.Clear();
            suppliers.ResetText();
            suppPurchase.Text = string.Empty;
            supplierGrid.DataSource = null;
            itemgridview.DataSource = null;
            removeSupp.Visible = false;

            _table = CreateDataTable();

            LoadComboSet1(option);

            locationComboBox.Select();
        }

        private void LoadComboSet1(int option)
        {
            if (option == 0)
            {
                locationComboBox.Items.Clear();
                locationComboBox.ResetText();
                LOCA_MAST[] catdetails = webService.GetAllLocationDetails(_user.COMPCODE);
                foreach (LOCA_MAST item in catdetails)
                {
                    locationComboBox.Items.Add(item.LOCA_CODE + " - " + item.LOCA_DESCRIPT);
                }
            }

            CAT_MAST searchdetails = new CAT_MAST();
            searchdetails.COMPCODE = _user.COMPCODE;

            catL1Combo.Items.Clear();
            catL1Combo.ResetText();
            CAT_MAST[] masterDetails = webService.GetAllCategoryMasterInfo(searchdetails);
            foreach (CAT_MAST item in masterDetails)
            {
                catL1Combo.Items.Add( item.CAT_CODE + " - " + item.CAT_DESC);
            }

            unitCombo.Items.Clear();
            unitCombo.ResetText();
            packUnit.Items.Clear();
            packUnit.ResetText();
            lengthUnit.Items.Clear();
            lengthUnit.ResetText();
            widthUnit.Items.Clear();
            widthUnit.ResetText();

            UNIT_MAST[] details = webService.GetAllUnitDetailsByCompany(_user.COMPCODE);
            foreach (UNIT_MAST item in details)
            {
                unitCombo.Items.Add(item.UNIT_CODE + " - " + item.UNIT_DESC);
                packUnit.Items.Add(item.UNIT_CODE + " - " + item.UNIT_DESC);
                lengthUnit.Items.Add(item.UNIT_CODE + " - " + item.UNIT_DESC);
                widthUnit.Items.Add(item.UNIT_CODE + " - " + item.UNIT_DESC);
            }

            suppliers.Items.Clear();
            suppliers.ResetText();
            SUPP_MAST[] supDetails = webService.GetAllSupplierDetailsByCompany("SUPP_CODE", "","", _user.COMPCODE,"SUPP_CODE");
            foreach (SUPP_MAST item in supDetails)
            {
                suppliers.Items.Add( item.SUPP_CODE + " - " + item.SUPP_NAME);
            }
        }

        private void locationComboBox_KeyPress(object sender, KeyPressEventArgs e)
        {           
            Reset(1);


            itemgridview.DataSource = null;
            ITEM_MAST[] details = webService.GetAllDetailsByLocation((locationComboBox.Text.Split(new string[] { " - " }, StringSplitOptions.None)[0]), _user.COMPCODE);
            itemgridview.DataSource = details;
            setRowNumber(itemgridview);
            locationComboBox.Select();
        }

        private void setRowNumber(DataGridView dgv)
        {
            foreach (DataGridViewRow row in dgv.Rows)
            {
                row.HeaderCell.Value = (row.Index + 1).ToString();
            }
        }

        private void catMastCombo_KeyPress(object sender, KeyPressEventArgs e)
        {
            catL2Combo.Items.Clear();
            catL2Combo.ResetText();

            CAT_L2 searchdetails = new CAT_L2();
            searchdetails.CATCODE = catL1Combo.SelectedItem.ToString().Split(new string[] { " - " }, StringSplitOptions.None)[0];
            searchdetails.COMPCODE = _user.COMPCODE;

            CAT_L2[] details = webService.GetAllL2CategoryDetailsByMasterId(searchdetails);

            foreach (CAT_L2 item in details)
            {
                catL2Combo.Items.Add(item.CATCODE_L2 + " - " + item.CAT_DESC);
                catL2Combo.SelectedIndex = 0;
            }

            catL1Combo.Select();
        }

        private void catL2Combo_KeyPress(object sender, KeyPressEventArgs e)
        {
            CatL3Combo.Items.Clear();
            CatL3Combo.ResetText();

            CAT_L3 searchdetails = new CAT_L3();
            searchdetails.CATCODE_L2 = (catL2Combo.Text.Split(new string[] { " - " }, StringSplitOptions.None)[0]);
            searchdetails.CATCODE = (catL1Combo.Text.Split(new string[] { " - " }, StringSplitOptions.None)[0]);
            searchdetails.COMPCODE = _user.COMPCODE;

            CAT_L3[] l2Details = webService.GetAllL3CategoryDetailsByMastCatId(searchdetails);
            foreach (CAT_L3 item in l2Details)
            {
                CatL3Combo.Items.Add( item.CATCODE + " - " + item.CAT_DESC);
                CatL3Combo.SelectedIndex = 0;
            }

            catL2Combo.Select();
        }

        private void CatL3Combo_KeyPress(object sender, KeyPressEventArgs e)
        {
            catL4Combo.Items.Clear();
            catL4Combo.ResetText();

            CAT_L4 searchdetails = new CAT_L4();
            searchdetails.CATCODE_L3 = CatL3Combo.Text.Split(new string[] { " - " }, StringSplitOptions.None)[0];
            searchdetails.CATCODE_L2 = catL2Combo.Text.Split(new string[] { " - " }, StringSplitOptions.None)[0];
            searchdetails.CATCODE = catL1Combo.Text.Split(new string[] { " - " }, StringSplitOptions.None)[0];
            searchdetails.COMPCODE = _user.COMPCODE;

            CAT_L4[] details = webService.GetAllL4CategoryDetailsByMastCatId(searchdetails);
            foreach (CAT_L4 item in details)
            {
                catL4Combo.Items.Add( item.CATCODE_L4 + " - " + item.CAT_DESC);
                catL4Combo.SelectedIndex = 0;
            }

            CatL3Combo.Select();
        }

        private DataTable CreateDataTable()
        {
            DataTable dt = new DataTable();            
            dt.Columns.Add("Supplier");
            dt.Columns.Add("Purchase Price");
            dt.Columns.Add("SupplierCode");
            dt.Columns.Add("RowId"); 

            return dt;
        }

        private bool IsValidated()
        {
            bool allSuccess = true;

            if (String.IsNullOrEmpty(itemCode.Text))
            {
                MessageBox.Show("Code cannot be empty.");
                allSuccess = false;
            }
            else if (String.IsNullOrEmpty(itemDesc1.Text))
            {
                MessageBox.Show("Description 1 cannot be empty.");
                allSuccess = false;
            }
            else if (String.IsNullOrEmpty(itemDesc2.Text))
            {
                MessageBox.Show("Description 2 cannot be empty.");
                allSuccess = false;
            }
            else if (String.IsNullOrEmpty(packsize.Text))
            {
                MessageBox.Show("Pack size cannot be empty.");
                allSuccess = false;
            }
            else if (String.IsNullOrEmpty(lengthDetails.Text))
            {
                MessageBox.Show("Length cannot be empty.");
                allSuccess = false;
            }
            else if (String.IsNullOrEmpty(widthDetails.Text))
            {
                MessageBox.Show("Width cannot be empty.");
                allSuccess = false;
            }
            else if (locationComboBox.Items == null || locationComboBox.Items.Count == 0 || locationComboBox.SelectedItem == null)
            {
                MessageBox.Show("Location cannot be empty.");
                allSuccess = false;
            }
            else if (catL1Combo.Items == null || catL1Combo.Items.Count == 0 || catL1Combo.SelectedItem == null)
            {
                MessageBox.Show("Category Level-1 cannot be empty.");
                allSuccess = false;
            }
            //else if (catL2Combo.Items == null || catL2Combo.Items.Count == 0 || catL2Combo.SelectedItem == null)
            //{
            //    MessageBox.Show("Category Level-2 cannot be empty.");
            //    allSuccess = false;
            //}
            //else if (CatL3Combo.Items == null || CatL3Combo.Items.Count == 0 || CatL3Combo.SelectedItem == null)
            //{
            //    MessageBox.Show("Category Level-3 cannot be empty.");
            //    allSuccess = false;
            //}
            //else if (catL4Combo.Items == null || catL4Combo.Items.Count == 0 || catL4Combo.SelectedItem == null)
            //{
            //    MessageBox.Show("Category Level-4 cannot be empty.");
            //    allSuccess = false;
            //}
            else if (unitCombo.Items == null || unitCombo.Items.Count == 0 || unitCombo.SelectedItem == null
                || packUnit.Items == null || packUnit.Items.Count == 0 || packUnit.SelectedItem == null
                || lengthUnit.Items == null || lengthUnit.Items.Count == 0 || lengthUnit.SelectedItem == null
                || widthUnit.Items == null || widthUnit.Items.Count == 0 || widthUnit.SelectedItem == null)
            {
                MessageBox.Show("Unit cannot be empty.");
                allSuccess = false;
            }
            else if (supplierGrid.DataSource == null || supplierGrid.RowCount == 0)
            {
                MessageBox.Show("Suppliers cannot be empty.");
                allSuccess = false;
            }
            else
            {
                if (!String.IsNullOrEmpty(packsize.Text))
                {
                    try
                    {
                        decimal.Parse(packsize.Text);
                    }
                    catch (Exception ex)
                    {
                        if (allSuccess != false)
                            MessageBox.Show("Pack Size should be a number.");
                        allSuccess = false;
                    }
                }
                if (!String.IsNullOrEmpty(lengthDetails.Text))
                {
                    try
                    {
                        decimal.Parse(lengthDetails.Text);
                    }
                    catch (Exception ex)
                    {
                        if (allSuccess != false)
                            MessageBox.Show("Length should be a number.");
                        allSuccess = false;
                    }
                }
                if (!String.IsNullOrEmpty(widthDetails.Text))
                {
                    try
                    {
                        decimal.Parse(widthDetails.Text);
                    }
                    catch (Exception ex)
                    {
                        if (allSuccess != false)
                            MessageBox.Show("Width should be a number.");
                        allSuccess = false;
                    }
                }
            }

            return allSuccess;
        }

        private void save_Click(object sender, EventArgs e)
        {     
       
            if (IsValidated())
            {
                int isExistingItem = 0;
                ITEM_MAST searchItemCode = new ITEM_MAST();
                searchItemCode.COMPCODE = _user.COMPCODE;
                searchItemCode.ITEM = itemCode.Text;
                searchItemCode.LOCA_CODE = (locationComboBox.SelectedItem.ToString().Split(new string[] { " - " }, StringSplitOptions.None)[0]);
                

                ITEM_MAST details = new ITEM_MAST();

                details = webService.GetAllItemDetailsById(searchItemCode);
                if (details.ITEM != null)
                {
                    isExistingItem= 1;
                }

                details.ITEM = itemCode.Text;
                details.DESC1 = itemDesc1.Text;
                details.DESC2 = itemDesc2.Text;
                details.COMPCODE = _user.COMPCODE;

                details.PACK = decimal.Parse(packsize.Text);
                details.LENGTH = decimal.Parse(lengthDetails.Text);
                details.WIDTH = decimal.Parse(widthDetails.Text);

                details.LOCA_CODE = (locationComboBox.SelectedItem.ToString().Split(new string[] { " - " }, StringSplitOptions.None)[0]);
                details.CATCODE = (catL1Combo.SelectedItem.ToString().Split(new string[] { " - " }, StringSplitOptions.None)[0]);
                details.UNIT = (unitCombo.SelectedItem.ToString().Split(new string[] { " - " }, StringSplitOptions.None)[0]);
                details.ITEM_UNIT0 = (packUnit.SelectedItem.ToString().Split(new string[] { " - " }, StringSplitOptions.None)[0]);
                details.ITEM_UNIT1 = (lengthUnit.SelectedItem.ToString().Split(new string[] { " - " }, StringSplitOptions.None)[0]);
                details.ITEM_UNIT2 = (widthUnit.SelectedItem.ToString().Split(new string[] { " - " }, StringSplitOptions.None)[0]);

                //Not mandatory
                if (catL2Combo.SelectedItem != null)
                    details.CATCODE_L2 = (catL2Combo.SelectedItem.ToString().Split(new string[] { " - " }, StringSplitOptions.None)[0]);
                if (CatL3Combo.SelectedItem != null)
                    details.CATCODE_L3 = (CatL3Combo.SelectedItem.ToString().Split(new string[] { " - " }, StringSplitOptions.None)[0]);
                if (catL4Combo.SelectedItem != null)
                    details.CATCODE_L4 = (catL4Combo.SelectedItem.ToString().Split(new string[] { " - " }, StringSplitOptions.None)[0]);
                

                if (!String.IsNullOrEmpty(maxStock.Text))
                    details.MAX_S = decimal.Parse(maxStock.Text);
                if (!String.IsNullOrEmpty(minStock.Text))
                    details.MIN_S = decimal.Parse(minStock.Text);
                if (!String.IsNullOrEmpty(reorderLevel.Text))
                    details.ROL = decimal.Parse(reorderLevel.Text);
                if (!String.IsNullOrEmpty(ReOrderQty.Text))
                    details.ROQ = decimal.Parse(ReOrderQty.Text);
                //end not mandatory

                if (isActiveItem.Checked)
                    details.ACTIVE_STAT = 1;
                else
                    details.ACTIVE_STAT = 0;

                if (storeStocksWithLU.Checked)
                    details.Is_stocks_in_second__unit = 1;
                else
                    details.Is_stocks_in_second__unit = 0;

                if (isServiceItem.Checked)
                    details.IS_SERVICE_ITEM = 1;
                else
                    details.IS_SERVICE_ITEM = 0;

                details.CHANGED = 0;

                bool status = false;

                List<ITEM_SUPP> itemsuppliers = new List<ITEM_SUPP>();

                foreach (DataGridViewRow item in supplierGrid.Rows)
                {
                    if ((item.Cells[1].Value) != null && !String.IsNullOrEmpty(item.Cells[1].Value.ToString()))
                    {
                        ITEM_SUPP itemsupplier = new ITEM_SUPP();
                        itemsupplier.LOCA_CODE = (locationComboBox.SelectedItem.ToString().Split(new string[] { " - " }, StringSplitOptions.None)[0]);
                        itemsupplier.COMPCODE = _user.COMPCODE;
                        itemsupplier.ITEMCODE = itemCode.Text;
                        
                        itemsupplier.P_PRICE = float.Parse(item.Cells[1].Value.ToString());
                        itemsupplier.SUPP_CODE = (item.Cells[0].Value.ToString().Split(new string[] { " - " }, StringSplitOptions.None)[0]);

                        itemsuppliers.Add(itemsupplier);
                    }
                }


                if (addItemsToAllLocations.Checked == true)
                {
                    LOCA_MAST[] locationsArray = webService.GetAllLocationDetails(_user.COMPCODE);
                    foreach (LOCA_MAST location in locationsArray)
                    {
                        isExistingItem = 0;
                        searchItemCode.LOCA_CODE = location.LOCA_CODE;
                        ITEM_MAST detailsNew = new ITEM_MAST();
                        detailsNew = webService.GetAllItemDetailsById(searchItemCode);
                        if (detailsNew.ITEM != null)
                        {
                            isExistingItem = 1;
                        }

                        details.LOCA_CODE = location.LOCA_CODE;
                        foreach (ITEM_SUPP itemsupplier in itemsuppliers)
                        {
                            itemsupplier.LOCA_CODE = location.LOCA_CODE;
                        }
                        if (isExistingItem == 0)
                        {
                            details.UNIT_1_QTY = 0;
                            details.UNIT_2_QTY = 0;
                            status = webService.InsertItemDetails(details, itemsuppliers.ToArray());
                        }
                        else
                        {
                            ITEM_MAST searchdetails2 = new ITEM_MAST();
                            searchdetails2.ITEM = LId.Text;
                            searchdetails2.LOCA_CODE = oldLocation.Text;
                            searchdetails2.COMPCODE = _user.COMPCODE;

                            status = webService.UpdateItemDetails(searchdetails2, details, itemsuppliers.ToArray());
                        }

                    }

                }
                else
                {

                    if (isExistingItem == 0)
                    {
                        status = webService.InsertItemDetails(details, itemsuppliers.ToArray());
                    }
                    else
                    {
                        ITEM_MAST searchdetails = new ITEM_MAST();
                        searchdetails.ITEM = LId.Text;
                        searchdetails.LOCA_CODE = oldLocation.Text;
                        searchdetails.COMPCODE = _user.COMPCODE;

                        status = webService.UpdateItemDetails(searchdetails, details, itemsuppliers.ToArray());

                    }

                }


                if (status)
                {
                    Reset(0);
                }
            }
        }

        private void delete_Click(object sender, EventArgs e)
        {
            bool status = false;
            DialogResult dialogResult = MessageBox.Show("Are you sure to DELETE this item from this LOCATION ?", "DELETE ITEM", MessageBoxButtons.YesNoCancel );
            if(dialogResult == DialogResult.Yes)
            {

                ITEM_MAST searchdetails = new ITEM_MAST();
                searchdetails.ITEM = LId.Text;
                searchdetails.LOCA_CODE = oldLocation.Text;
                searchdetails.COMPCODE = _user.COMPCODE;

                ITEM_SUPP itemsupp = new ITEM_SUPP();

                if (!String.IsNullOrEmpty(LId.Text))
                {
                    status = webService.DeleteItemDetails(searchdetails, itemsupp);
                }

                if (status)
                {
                    Reset(0);
                }
                else if (status==false )
                {
                     dialogResult = MessageBox.Show("Cannot delete this item", "DELETE ITEM", MessageBoxButtons.OK);
                }
            }
        }
       
        private bool ValidateSuppliers()
        {
            bool allSuccess = true;
            if (suppliers.Items == null || suppliers.Items.Count == 0 || suppliers.SelectedItem == null)
            {
                MessageBox.Show("Suppliers cannot be empty.");
                suppliers.Select();
                allSuccess = false;
            }
            else if (String.IsNullOrEmpty(suppPurchase.Text))
            {
                MessageBox.Show("Purchase Price cannot be empty.");
                suppPurchase.Select();
                allSuccess = false;
            }
            else if (!String.IsNullOrEmpty(suppPurchase.Text))
            {
                try
                {
                    decimal.Parse(suppPurchase.Text);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Width should be a number.");
                    allSuccess = false;
                    suppPurchase.Select();
                }
            }

            return allSuccess;
        }      

        
        private void supplierGrid_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.supplierGrid.Rows[e.RowIndex];
                suppId.Text = row.Cells["RowId"].Value.ToString();

                string supp = row.Cells["Supplier"].Value.ToString();
                
                int count = 0;
                foreach (var item in suppliers.Items)
                {
                    if (item.ToString() == supp)
                    {
                        suppliers.SelectedIndex = count;
                        break;
                    }
                    count++;
                }

                suppPurchase.Text = row.Cells["Purchase Price"].Value.ToString();
            }
        }


        private void itemgridview_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.itemgridview.Rows[e.RowIndex];
                LId.Text = row.Cells["ITEM"].Value.ToString();
                oldLocation.Text = row.Cells["LOCA_CODE"].Value.ToString();

                FillData();
            }
        }

        private void FillData()
        {
            ITEM_MAST searchdetails = new ITEM_MAST();
            searchdetails.ITEM = LId.Text;
            searchdetails.LOCA_CODE = oldLocation.Text;
            searchdetails.COMPCODE = _user.COMPCODE;

            ITEM_MAST details = webService.GetAllItemDetailsById(searchdetails);

            itemCode.Text = details.ITEM;
            itemDesc1.Text = details.DESC1;
            itemDesc2.Text = details.DESC2;
            packsize.Text = details.PACK.ToString();
            lengthDetails.Text = details.LENGTH.ToString();
            widthDetails.Text = details.WIDTH.ToString();

            maxStock.Text = details.MAX_S.ToString();
            minStock.Text = details.MIN_S.ToString();
            reorderLevel.Text = details.ROL.ToString();
            ReOrderQty.Text = details.ROQ.ToString();

            if (details.ACTIVE_STAT == 1)
                isActiveItem.Checked = true;
            else
                isActiveItem.Checked = false;

            if (details.Is_stocks_in_second__unit == 1)
                storeStocksWithLU.Checked = true;
            else
                storeStocksWithLU.Checked = false;

            if (details.IS_SERVICE_ITEM == 1)
                isServiceItem.Checked = true;
            else
                isServiceItem.Checked = false;

            LoadComboSet1(0);

            int count = 0;
            foreach (var item in locationComboBox.Items)
            {
                if ((item.ToString().Split(new string[] { " - " }, StringSplitOptions.None)[0]) == details.LOCA_CODE)
                {
                    locationComboBox.SelectedIndex = count;
                    break;
                }
                count++;
            }

            count = 0;
            foreach (var item in catL1Combo.Items)
            {
                if ((item.ToString().Split(new string[] { " - " }, StringSplitOptions.None)[0]) == details.CATCODE)
                {
                    catL1Combo.SelectedIndex = count;
                    break;
                }
                count++;
            }

            count = 0;
            foreach (var item in unitCombo.Items)
            {
                if ((item.ToString().Split(new string[] { " - " }, StringSplitOptions.None)[0]) == details.UNIT)
                {
                    unitCombo.SelectedIndex = count;
                    break;
                }
                count++;
            }

            count = 0;
            foreach (var item in packUnit.Items)
            {
                if ((item.ToString().Split(new string[] { " - " }, StringSplitOptions.None)[0]) == details.ITEM_UNIT0)
                {
                    packUnit.SelectedIndex = count;
                    break;
                }
                count++;
            }

            count = 0;
            foreach (var item in lengthUnit.Items)
            {
                if ((item.ToString().Split(new string[] { " - " }, StringSplitOptions.None)[0]) == details.ITEM_UNIT1)
                {
                    lengthUnit.SelectedIndex = count;
                    break;
                }
                count++;
            }

            count = 0;
            foreach (var item in widthUnit.Items)
            {
                if ((item.ToString().Split(new string[] { " - " }, StringSplitOptions.None)[0]) == details.ITEM_UNIT2)
                {
                    widthUnit.SelectedIndex = count;
                    break;
                }
                count++;
            }

            catL2Combo.Items.Clear();
            catL2Combo.ResetText();

            CAT_L2 cat2detailsSearch = new CAT_L2();
            cat2detailsSearch.CATCODE = catL1Combo.SelectedItem.ToString().Split(new string[] { " - " }, StringSplitOptions.None)[0];
            cat2detailsSearch.COMPCODE = _user.COMPCODE;

            CAT_L2[] cat2details = webService.GetAllL2CategoryDetailsByMasterId(cat2detailsSearch);
            count = 0;
            foreach (CAT_L2 item in cat2details)
            {
                catL2Combo.Items.Add(item.CATCODE_L2 + " - " + item.CAT_DESC);

                if (item.CATCODE_L2 == details.CATCODE_L2)
                {
                    catL2Combo.SelectedIndex = count;
                }
                count++;
            }

            CatL3Combo.Items.Clear();
            CatL3Combo.ResetText();
            count = 0;

            CAT_L3 l2Detailssearchdetails = new CAT_L3();
            l2Detailssearchdetails.CATCODE_L2 = (catL2Combo.Text.Split(new string[] { " - " }, StringSplitOptions.None)[0]);
            l2Detailssearchdetails.CATCODE = (catL1Combo.Text.Split(new string[] { " - " }, StringSplitOptions.None)[0]);
            l2Detailssearchdetails.COMPCODE = _user.COMPCODE;

            CAT_L3[] l2Details = webService.GetAllL3CategoryDetailsByMastCatId(l2Detailssearchdetails);
            foreach (CAT_L3 item in l2Details)
            {
                CatL3Combo.Items.Add(item.CATCODE_L3 + " - " + item.CAT_DESC);

                if (item.CATCODE_L3 == details.CATCODE_L3)
                {
                    CatL3Combo.SelectedIndex = count;
                }

                count++;
            }


            catL4Combo.Items.Clear();
            catL4Combo.ResetText();
            count = 0;

            CAT_L4 catL4Detailssearchdetails = new CAT_L4();
            catL4Detailssearchdetails.CATCODE_L3 = CatL3Combo.Text.Split(new string[] { " - " }, StringSplitOptions.None)[0];
            catL4Detailssearchdetails.CATCODE_L2 = catL2Combo.Text.Split(new string[] { " - " }, StringSplitOptions.None)[0];
            catL4Detailssearchdetails.CATCODE = catL1Combo.Text.Split(new string[] { " - " }, StringSplitOptions.None)[0];
            catL4Detailssearchdetails.COMPCODE = _user.COMPCODE;

            CAT_L4[] catL4Details = webService.GetAllL4CategoryDetailsByMastCatId(catL4Detailssearchdetails);
            foreach (CAT_L4 item in catL4Details)
            {
                catL4Combo.Items.Add(item.CATCODE_L4 + " - " + item.CAT_DESC);

                if (item.CATCODE_L4 == details.CATCODE_L4)
                {
                    catL4Combo.SelectedIndex = count;
                }
                count++;
            }
            ITEM_SUPP searchItemDetails = new ITEM_SUPP();
            searchItemDetails.COMPCODE = _user.COMPCODE;
            searchItemDetails.ITEMCODE = LId.Text;
            searchItemDetails.LOCA_CODE = oldLocation.Text;

            ITEM_SUPP[] supplierDetails = webService.GetAllSupplierDetailsByItemId(searchItemDetails);

            _table = CreateDataTable();
            supplierGrid.DataSource = null;

            int rowIndex = 0;
            foreach (ITEM_SUPP supplier in supplierDetails)
            {
                SUPP_MAST suppdetails = webService.GetAllSupplierDetailsById(supplier.SUPP_CODE, _user.COMPCODE);

                _table.Rows.Add(supplier.SUPP_CODE + " - " + suppdetails.SUPP_NAME + "-", supplier.P_PRICE, supplier.SUPP_CODE, rowIndex);
                rowIndex++;
            }

            supplierGrid.DataSource = _table;

            supplierGrid.Columns["SupplierCode"].Visible = false;
        }

        private void reset_Click(object sender, EventArgs e)
        {
            Reset(0);
        }

        private void suppPurchase_KeyPress(object sender, KeyPressEventArgs e)     
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                if (ValidateSuppliers())
                {
                    if (String.IsNullOrEmpty(suppId.Text))
                    {
                        supplierGrid.DataSource = null;

                        _table.Rows.Add(suppliers.SelectedItem.ToString(), suppPurchase.Text, (suppliers.SelectedItem.ToString().Split(new string[] { " - " }, StringSplitOptions.None)[0]).ToString(), _table.Rows.Count);
                        supplierGrid.DataSource = _table;

                        supplierGrid.Columns["SupplierCode"].Visible = false;
                        supplierGrid.Columns["RowId"].Visible = false;
                    }
                    else
                    {
                        supplierGrid.DataSource = null;

                        DataRow row = _table.Rows[int.Parse(suppId.Text)];
                        row["Supplier"] = suppliers.SelectedItem.ToString();
                        row["Purchase Price"] = suppPurchase.Text;
                        row["SupplierCode"] = (suppliers.SelectedItem.ToString().Split(new string[] { " - " }, StringSplitOptions.None)[0]).ToString();

                        _table.AcceptChanges();

                        supplierGrid.DataSource = _table;

                        supplierGrid.Columns["SupplierCode"].Visible = false;
                        supplierGrid.Columns["RowId"].Visible = false;
                    }
                }
            }
        }

        private void itemCode_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {        
                if (e.KeyCode == Keys.F2 )
                {
                    itemCode.Text = "";
                                        

                    SearchCriteria search = new SearchCriteria();
                    search.Location = (locationComboBox.SelectedItem.ToString().Split(new string[] { " - " }, StringSplitOptions.None)[0]); ; ;
                    search.SequenceNo = 1;
                    search.CompanyCode = _user.COMPCODE;
                    search.SearchStartingCharacters = itemCode.Text.Trim();
                    search.TableName = "ITEM_MAST";

                    HelpScreen form = new HelpScreen(search);
                    DialogResult dialogResult = form.ShowDialog();
                    itemCode.Text = form._fromHelpValue;
                    itemCode.Focus();

                } 
                else if( e.KeyCode == Keys.Enter )
                {
                    LId.Text = itemCode.Text.Trim();
                    oldLocation.Text = (locationComboBox.SelectedItem.ToString().Split(new string[] { " - " }, StringSplitOptions.None)[0]); ;

                    FillData();
                }
            }
            catch (Exception )
            {
                
               // throw;
                MessageBox.Show("Not Found");
            }

        }

        private void itemCode_TextChanged(object sender, EventArgs e)
        {

        }

        private void ItemDetails_Load(object sender, EventArgs e)
        {

        }

        private void locationComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string location = locationComboBox.Text.Substring(0, (locationComboBox.Text.IndexOf("-"))).Trim();
            NETWORK[] company_name = webService.GetCompanyDetailsById(_user.COMPCODE);
            //CompuLinINV.WIN.InvoiceReportDisplay form = new CompuLinINV.WIN.InvoiceReportDisplay("0", location, "", "ITEM_LIST", "", _user.COMPCODE, company_name[0].CompName, DateTime.Now, DateTime.Now);
            //DisplayPrintScreen form = new DisplayPrintScreen("0", "PhysicalEntryList.rpt", "PHY_ALL", "", location);
            //form.Refresh();
            //DialogResult dialogResult = form.ShowDialog();
        }


       
       
        

        
    }
}
