using CompuLinERP.API;
using CompuLinERP.API.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace Test.WebApp
{
    /// <summary>
    /// Summary description for WebService1
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class WebService1 : System.Web.Services.WebService
    {
        NetworkController networkController = new NetworkController();
        //UserInfoController userInfoController = new UserInfoController();
        //NavigationInfoController navigationInfoController = new NavigationInfoController();

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        [WebMethod]
        public List<Network> GetAllCompanyDetails()
        {
            List<Network> companyList = networkController.GetAllNetworkDetails();
            return companyList;
        }
    }
}
