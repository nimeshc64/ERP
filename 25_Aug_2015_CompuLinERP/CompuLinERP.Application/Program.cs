using CompuLinERP.WIN;
using CompuLinERP.WIN.InventoryWebService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace CompuLinINV.WIN
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            USERINFO user = new USERINFO();
            user.COMPCODE = "001";
            user.USERCODE = "Admin";
            //Application.Run(new Invoice(user));
            Application.Run(new Login());
            //Application.Run(new GRN(user));
            //Application.Run(new TransferNote(user));
        }
    }
}
