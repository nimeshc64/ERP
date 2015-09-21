using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics ;
using CompuLinINV.WIN.DTO;
using CompuLinERP.WIN.InventoryWebService;
using CompuLinERP.WIN;


namespace CompuLinINV.WIN
{
    public partial class MainForm : Form
    {
        USERINFO _user;
        InventoryWebService webService = new InventoryWebService();

        public MainForm(USERINFO user)
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
            _user = user;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            //
            NETWORK[] companyname = webService.GetCompanyDetailsById(_user.COMPCODE );
            this.Text = companyname[0].COMPCODE + " - " + companyname[0].CompName ;
            lblCompanyName.Text = companyname[0].COMPCODE + " - " + companyname[0].CompName;
            var versionInfo = FileVersionInfo.GetVersionInfo(Environment.CurrentDirectory + "\\CompuLinERP.WIN.exe");
            toolStripStatusLabel1.Text = "Version : " + versionInfo.ProductVersion;
            toolStripStatusLabel2.Text = "Workstation : " + Environment.MachineName;
            toolStripStatusLabel3.Text = "Current User : " + _user.USERCODE;

           LoadTreeData();
        }


        private void LoadTreeData()
        {
            NAVIGATION[] treeData = webService.GetAllTreeViewInfo();

            Char[] charArray = _user.RightCodes.ToCharArray();            
            int i = 0;

            foreach (char userLevel in charArray)
            {
                if (userLevel == '1' && i < treeData.Length)
                {
                   if(treeView1.Nodes.ContainsKey(treeData[i].PARENT) && !String.IsNullOrEmpty(treeData[i].CHILD))
                   {
                       TreeNode child = new TreeNode(treeData[i].CHILD);
                       child.Name = treeData[i].CHILD;
                       treeView1.Nodes[treeData[i].PARENT].Nodes.Add(child);                
                   }
                   else
                   {
                       TreeNode parent = new TreeNode(treeData[i].PARENT);
                       parent.Name = treeData[i].PARENT;
                       treeView1.Nodes.Add(parent);                       

                       if(!String.IsNullOrEmpty(treeData[i].CHILD) && (treeView1.Nodes.ContainsKey(treeData[i].PARENT)))
                       {
                           TreeNode child = new TreeNode(treeData[i].CHILD);
                           child.Name = treeData[i].CHILD;
                           treeView1.Nodes[treeData[i].PARENT].Nodes.Add(child);                           
                       }
                   }                    
                }

                i++;
            }

            //treeView1.ExpandAll();
            treeView1.CollapseAll();
        }

        private void treeView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            TreeNode node = treeView1.SelectedNode;

            switch(node.Text)
            {
                
                
                case "Unit Details":
                    {
                        UnitDetails form = new UnitDetails(_user);
                        form.MdiParent = this;
                        form.Show();
                        break;
                    }
                case "Supplier Details":
                    {
                        SupplierDetails form = new SupplierDetails(_user);
                        form.MdiParent = this;
                        form.StartPosition = FormStartPosition.CenterScreen;
                        form.Show();
                        break;
                    }
                case "Location Details":
                    {
                        LocationDetails form = new LocationDetails(_user);
                        form.MdiParent = this;
                        form.Show();
                        break;
                    }
                case "Category Details":
                    {
                        CategoryDetails form = new CategoryDetails(_user);
                        form.MdiParent = this;
                        form.Show();
                        break;
                    }
                case "Customer Details":
                    {
                        CustomerDetails form = new CustomerDetails(_user);
                        form.MdiParent = this;
                        form.StartPosition = FormStartPosition.CenterScreen;
                        form.Show();
                        break;
                    }
                case "Item Details":
                    {
                        ItemDetails form = new ItemDetails(_user);
                        form.MdiParent = this;
                        form.StartPosition = FormStartPosition.CenterScreen;
                        form.Show();
                        break;
                    }
                
                case "Cost Price":
                    {
                        CostPrice form = new CostPrice(_user);
                        form.MdiParent = this;
                        form.StartPosition = FormStartPosition.CenterScreen;
                        form.Show();
                        break;
                    }
                case "Sales Price":
                    {
                        SalesPrice form = new SalesPrice(_user);
                        form.MdiParent = this;
                        form.StartPosition = FormStartPosition.CenterScreen;
                        form.Show();
                        break;
                    }
                case "Invoice Details":
                    {
                        Invoice form = new Invoice(_user);
                        form.MdiParent = this;
                        form.StartPosition = FormStartPosition.CenterScreen;
                        form.Show();
                        break;
                    }
                case "Goods Received Notes":
                    {
                        GRN form = new GRN(_user);
                        form.MdiParent = this;
                        form.StartPosition = FormStartPosition.CenterScreen;
                        form.Show();
                        break;
                    }
                case "Transfer Notes":
                    {
                        TransferNote form = new TransferNote(_user);
                        form.MdiParent = this;
                        form.StartPosition = FormStartPosition.CenterScreen;
                        form.Show();
                        break;
                    }
                
                case "Receipts":
                    {
                        Receipts form = new Receipts(_user);
                        form.MdiParent = this;
                        form.StartPosition = FormStartPosition.CenterScreen;
                        form.Show();
                        break;
                    }
                case "Chart of Accounts":
                    {
                        ChartOfAccounts form = new ChartOfAccounts(_user);
                        form.MdiParent = this;
                        form.StartPosition = FormStartPosition.CenterScreen;
                        form.Show();
                        break;
                    }
                case "Location Customize":
                    {
                        LocationCustomize form = new LocationCustomize(_user);
                        form.MdiParent = this;
                        form.StartPosition = FormStartPosition.CenterScreen;
                        form.Show();
                        break;
                    }               
                case "Journal Entries":
                    {
                        JournalEntries form = new JournalEntries(_user);
                        form.MdiParent = this;
                        form.StartPosition = FormStartPosition.CenterScreen;
                        form.Show();
                        break;
                    }
                case "Stock Adjustments":
                    {
                        StockAdjustments form = new StockAdjustments(_user);
                        form.MdiParent = this;
                        form.StartPosition = FormStartPosition.CenterScreen;
                        form.Show();
                        break;
                    }
                case "Cutter":
                    {
                        CutItem form = new CutItem(_user);
                        form.MdiParent = this;
                        form.StartPosition = FormStartPosition.CenterScreen;
                        form.Show();
                        break;
                    }
              
                case "Exit":
                    {

                        var current = Process.GetCurrentProcess();
                        Process.GetProcessesByName(current.ProcessName)
                            .Where(t => t.Id != current.Id)
                            .ToList()
                            .ForEach(t => t.Kill());

                        current.Kill();

                        Environment.Exit(0);
                        break;
                    }
                default:
                    break;
            }
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {

        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            var current = Process.GetCurrentProcess();
            Process.GetProcessesByName(current.ProcessName)
                .Where(t => t.Id != current.Id)
                .ToList()
                .ForEach(t => t.Kill());

            current.Kill();
        }
     
    }
}
