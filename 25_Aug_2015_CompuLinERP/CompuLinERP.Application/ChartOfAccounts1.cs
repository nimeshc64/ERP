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
        }
    }
}
