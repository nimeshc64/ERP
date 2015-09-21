using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CompuLinERP.WEB
{
    public partial class Main : System.Web.UI.Page
    {
        string CompanyCode;
        string CompanyName;
        //WebInventoryWebService webService = new WebInventoryWebService();
        //WebInventoryWebService.IsValidUserRequest  aa =  new WebInventoryWebService;

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
          //  bool isExists = webService.IsValidUser(userName.Text, password.Text, CompanyCode);
        }
    }
}