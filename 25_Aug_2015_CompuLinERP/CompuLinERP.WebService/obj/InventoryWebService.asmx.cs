using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using CompuLinERP.API;
using CompuLinERP.API.Controllers;
using System.Collections;

namespace CompuLinINV.WebService
{
    /// <summary>
    /// Summary description for InventoryWebService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class InventoryWebService : System.Web.Services.WebService
    {
        NetworkController networkController = new NetworkController();
        UserInfoController userInfoController = new UserInfoController();
        NavigationInfoController navigationInfoController = new NavigationInfoController();
        DocumentInfoController docInfoController = new DocumentInfoController();
        CategoryMastController categoryMasterController = new CategoryMastController();
        CategoryL2Controller categoryL2Controller = new CategoryL2Controller();
        CategoryL3Controller categoryL3Controller = new CategoryL3Controller();
        CategoryL4Controller categoryL4Controller = new CategoryL4Controller();
        UnitDetailsController unitDetailsController = new UnitDetailsController();
        LocationDetailsController locationDetailsController = new LocationDetailsController();
        SupplierMasterDetailsController supplierMasterDetailsController = new SupplierMasterDetailsController();
        CustomerMastDetailsController customerMastDetailsController = new CustomerMastDetailsController();
        ItemDetailsController itemDetailsController = new ItemDetailsController();
        ItemSupplierController itemSupplierController = new ItemSupplierController();
        HelpMasterController helpController = new HelpMasterController();
        StockAdjustmentsDetailsController StockAdjustmentsDetailsController = new StockAdjustmentsDetailsController();
        StockDetailsController stockDetailsController = new StockDetailsController();
        InvoiceMastController invoiceMastController = new InvoiceMastController();
        InvoiceDetailsController invoiceDetailsController = new InvoiceDetailsController();
        TransactionTypesController transactionTypesController = new TransactionTypesController();
        SalesPriceTypesController salesPriceTypesController = new SalesPriceTypesController();
        ItemDetailsTransactions itemDetailsTransactions = new ItemDetailsTransactions();
        PhysicalStockDetailsTransactions physicalStockDetailsTransactions = new PhysicalStockDetailsTransactions();
        InvoiceMastTransactions invoiceMastTransactions = new InvoiceMastTransactions();
        TransMastTransactions transMastTransactions = new TransMastTransactions();
        GRNMasterControllers grnMasterControllers = new GRNMasterControllers();
        GRNDetailControllers grnDetailControllers = new GRNDetailControllers();
        GRNMastTransactions grnMastTransactions = new GRNMastTransactions();
        ItemCutListController itemCutListController = new ItemCutListController();
        AccountL1Controller accountMasterController = new AccountL1Controller();
        AccountL2Controller accountMasterController2 = new AccountL2Controller();
        AccountL3Controller accountMasterController3 = new AccountL3Controller();
        AccountL4Controller accountMasterController4 = new AccountL4Controller();
        AccountL5Controller accountMasterController5 = new AccountL5Controller();
        AccountDetailsController accDetails = new AccountDetailsController();
        ReceiptMasterController receiptMaster = new ReceiptMasterController();
        ReceiptDetailsController receiptDetail = new ReceiptDetailsController();
        ReceiptInvController receiptInv = new ReceiptInvController();
        LocationCustomizeController locationCustomizeController = new LocationCustomizeController();
    


        #region UserAndCompanyInfo

        [WebMethod]
        public List<NETWORK> GetAllCompanyDetails()
        {
            List<NETWORK> companyList = networkController.GetAllNetworkDetails();
            return companyList;
        }

        [WebMethod]
        public List<NETWORK> GetCompanyDetailsById(string compcode)
        {
            List<NETWORK> companyList = networkController.GetNetworkDetailsById(compcode);
            return companyList;
        }
        [WebMethod]
        public bool IsValidUser(string username, string password, string companyId)
        {
            bool isExists = userInfoController.IsValidUser(username, password, companyId);
            return isExists;
        }

        [WebMethod]
        public USERINFO GetUserInfo(string username, string companyId)
        {
            USERINFO user = new USERINFO();
            user.USERCODE = username;
            user.COMPCODE = companyId;

            USERINFO userDetails = userInfoController.GetUserDetails(user);
            return userDetails;

        }

        [WebMethod]
        public List<NAVIGATION> GetAllTreeViewInfo()
        {
            List<NAVIGATION> treeData = navigationInfoController.GetAllNavigationInfo();
            return treeData;
        }

        [WebMethod]
        public List<USERINFO> GetAllUserDetailsByCompany(string networkId)
        {
            List<USERINFO> userDetails = userInfoController.GetAllUserDetailsByCompany(networkId);
            return userDetails;
        }

        [WebMethod]
        public bool InsertNewUser(USERINFO user)
        {
            bool status = userInfoController.InsertNewUser(user);
            return status;
        }

        [WebMethod]
        public bool UpdateUser(USERINFO searchUser, USERINFO updatedDetails)
        {
            bool status = userInfoController.UpdateUser(searchUser, updatedDetails);
            return status;
        }

        [WebMethod]
        public bool UpdateUserPassword(USERINFO searchUser, string newpwd)
        {
            bool status = userInfoController.UpdateUserPassword(searchUser, newpwd);
            return status;
        }

        [WebMethod]
        public bool DeleteUser(USERINFO searchUser)
        {
            bool status = userInfoController.DeleteUser(searchUser);
            return status;
        }

        #endregion

        #region DocumentInfo

        [WebMethod]
        public string InsertDocumentInfo(DocumentInfo docInfo)
        {
            string docNumber = docInfoController.InsertNewDoc(docInfo);
            return docNumber;
        }

        [WebMethod]
        public DocumentInfo GetAllDocDetails()
        {
            DocumentInfo docInfo = docInfoController.GetAllDocInfo();
            return docInfo;
        }

        #endregion

        #region AccountDetails

        [WebMethod]
        public bool InsertAccountDetails(GL info)
        {
            bool status = accDetails.InsertAccDetails(info);
            return status;
        }

        [WebMethod]
        public GL GetAllDetailsByAccCode(GL searchDetails)
        {
            List<GL> details = accDetails.GetAllAccountDetailsbySearch(searchDetails, 1);
            return details[0];
        }

        [WebMethod]
        public List<GL> GetAllDetailsByAccCodeToCombo(GL searchDetails)
        {
            List<GL> details = accDetails.GetAllAccountDetailsbySearch(searchDetails, 2);
            return details;
        }

        [WebMethod]
        public List<GL> GetAllDetailsByAccCodeToComboCashBook(GL searchDetails)
        {
            List<GL> details = accDetails.GetAllAccountDetailsbySearch(searchDetails, 3);
            return details;
        }

        [WebMethod]
        public List<GL> GetAllDetailsByAccCodeToComboCreditBook(GL searchDetails)
        {
            List<GL> details = accDetails.GetAllAccountDetailsbySearch(searchDetails, 4);
            return details;
        }

        [WebMethod]
        public bool DeleteAccountDetails(GL info)
        {
            bool status = accDetails.DeleteAccDetails(info);
            return status;
        }

        [WebMethod]
        public bool UpdateAccountDetails(GL search, GL info)
        {
            bool status = accDetails.UpdateAccDetails(search, info);
            return status;
        }

        #endregion

        #region L1AccountInfo

        [WebMethod]
        public bool InsertAccountMasterInfo(ACC_CAT_L1 info)
        {
            bool status = accountMasterController.InsertNewCategory(info);
            return status;
        }

        [WebMethod]
        public List<ACC_CAT_L1> GetAllAccountMasterInfo(ACC_CAT_L1 searchDetails)
        {
            List<ACC_CAT_L1> details = accountMasterController.GetAllAccountDetails(searchDetails, 1);
            return details;
        }

        [WebMethod]
        public bool DeleteAccountMasterInfo(ACC_CAT_L1 searchDetails)
        {
            bool status = accountMasterController.DeleteAccountDetails(searchDetails);
            return status;
        }


        [WebMethod]
        public ACC_CAT_L1 GetAllAccountDetailsById(ACC_CAT_L1 searchDetails)
        {
            List<ACC_CAT_L1> details = accountMasterController.GetAllAccountDetails(searchDetails, 2);
            return details[0];
        }

        [WebMethod]
        public bool UpdateCategoryMasterInfoAcc(ACC_CAT_L1 searchDetails, ACC_CAT_L1 info)
        {
            bool status = accountMasterController.UpdateDetailsAcc(searchDetails, info);
            return status;
        }
        #endregion

        #region L2AccountInfo

        [WebMethod]
        public bool InsertAccountMasterInfoACCL2(ACC_CAT_L2 info)
        {
            bool status = accountMasterController2.InsertNewCategoryL2(info);
            return status;
        }


        [WebMethod]
        public List<ACC_CAT_L2> GetAllL2AccountDetailsByMasterId(ACC_CAT_L2 searchDetails)
        {
            List<ACC_CAT_L2> details = accountMasterController2.GetAllAccountDetailsL2(searchDetails, 1);
            return details;
        }

        [WebMethod]
        public ACC_CAT_L2 GetAllL2AccountDetailsById(ACC_CAT_L2 searchDetails)
        {
            List<ACC_CAT_L2> details = accountMasterController2.GetAllAccountDetailsL2(searchDetails, 2);
            return details[0];
        }

        [WebMethod]
        public bool UpdateAccountL2Info(ACC_CAT_L2 searchInfo, ACC_CAT_L2 info)
        {
            bool status = accountMasterController2.UpdateDetails(searchInfo, info);
            return status;
        }

        [WebMethod]
        public bool DeleteAccountL2Info(ACC_CAT_L2 info)
        {
            bool status = accountMasterController2.DeleteCategory(info);
            return status;
        }

        #endregion

        #region L3AccountInfo

        [WebMethod]
        public bool InsertAccountL3Info(ACC_CAT_L3 info)
        {
            bool status = accountMasterController3.InsertNewCategory(info);
            return status;
        }

        [WebMethod]
        public bool UpdateAccountL3Info(ACC_CAT_L3 searchDetails, ACC_CAT_L3 info)
        {
            bool status = accountMasterController3.UpdateDetails(searchDetails, info);
            return status;
        }

        [WebMethod]
        public List<ACC_CAT_L3> GetAllL3AccountDetails(ACC_CAT_L3 searchDetails)
        {
            List<ACC_CAT_L3> details = accountMasterController3.GetAllAccountDetails(searchDetails, 1);
            return details;
        }

        [WebMethod]
        public ACC_CAT_L3 GetAllL3AccountDetailsById(ACC_CAT_L3 searchDetails)
        {
            List<ACC_CAT_L3> details = accountMasterController3.GetAllAccountDetails(searchDetails, 2);
            return details[0];
        }

        [WebMethod]
        public List<ACC_CAT_L3> GetAllL3AccountDetailsByMastCatId(ACC_CAT_L3 searchDetails)
        {
            List<ACC_CAT_L3> details = accountMasterController3.GetAllAccountDetails(searchDetails, 3);
            return details;
        }

        [WebMethod]
        public bool DeleteAccountL3Info(ACC_CAT_L3 searchDetails)
        {
            bool status = accountMasterController3.DeleteCategory(searchDetails);
            return status;
        }

        #endregion

        #region L4AccountInfo

        [WebMethod]
        public bool InsertAccountL4Info(ACC_CAT_L4 info)
        {
            bool status = accountMasterController4.InsertNewCategory(info);
            return status;
        }

        [WebMethod]
        public bool UpdateAccountL4Info(ACC_CAT_L4 searchDetails, ACC_CAT_L4 info)
        {
            bool status = accountMasterController4.UpdateDetails(searchDetails, info);
            return status;
        }

        [WebMethod]
        public List<ACC_CAT_L4> GetAllL4AccountDetails(ACC_CAT_L4 searchDetails)
        {
            List<ACC_CAT_L4> details = accountMasterController4.GetAllAccountDetails(searchDetails, 1);
            return details;
        }

        [WebMethod]
        public ACC_CAT_L4 GetAllL4AccountDetailsById(ACC_CAT_L4 searchDetails)
        {
            List<ACC_CAT_L4> details = accountMasterController4.GetAllAccountDetails(searchDetails, 2);
            return details[0];
        }

        [WebMethod]
        public List<ACC_CAT_L4> GetAllL4AccountDetailsByMastCatId(ACC_CAT_L4 searchDetails)
        {
            List<ACC_CAT_L4> details = accountMasterController4.GetAllAccountDetails(searchDetails, 3);
            return details;
        }

        [WebMethod]
        public bool DeleteAccountL4Info(ACC_CAT_L4 searchDetails)
        {
            bool status = accountMasterController4.DeleteCategory(searchDetails);
            return status;
        }

        #endregion

        #region L5AccountInfo
        [WebMethod]
        public bool InsertAccountL5Info(ACC_CAT_L5 info)
        {
            bool status = accountMasterController5.InsertNewCategory(info);
            return status;
        }

        [WebMethod]
        public bool UpdateAccountL5Info(ACC_CAT_L5 searchDetails, ACC_CAT_L5 info)
        {
            bool status = accountMasterController5.UpdateDetails(searchDetails, info);
            return status;
        }

        [WebMethod]
        public List<ACC_CAT_L5> GetAllL5AccountDetails(ACC_CAT_L5 searchDetails)
        {
            List<ACC_CAT_L5> details = accountMasterController5.GetAllAccountDetails(searchDetails, 1);
            return details;
        }

        [WebMethod]
        public ACC_CAT_L5 GetAllL5AccountDetailsById(ACC_CAT_L5 searchDetails)
        {
            List<ACC_CAT_L5> details = accountMasterController5.GetAllAccountDetails(searchDetails, 2);
            return details[0];
        }

        [WebMethod]
        public List<ACC_CAT_L5> GetAllL5AccountDetailsByMastCatId(ACC_CAT_L5 searchDetails)
        {
            List<ACC_CAT_L5> details = accountMasterController5.GetAllAccountDetails(searchDetails, 3);
            return details;
        }

        [WebMethod]
        public ACC_CAT_L5 GetAllL5AccountDetailsByIdAll(ACC_CAT_L5 searchDetails)
        {
            List<ACC_CAT_L5> details = accountMasterController5.GetAllAccountDetails(searchDetails, 7);
            return details[0];
        }

        [WebMethod]
        public bool DeleteAccountL5Info(ACC_CAT_L5 searchDetails)
        {
            bool status = accountMasterController5.DeleteCategory(searchDetails);
            return status;
        }
        #endregion

        #region CategoryMasterInfo

        [WebMethod]
        public bool InsertCategoryMasterInfo(CAT_MAST info)
        {
            bool status = categoryMasterController.InsertNewCategory(info);
            return status;
        }

        [WebMethod]
        public bool UpdateCategoryMasterInfo(CAT_MAST searchDetails, CAT_MAST info)
        {
            bool status = categoryMasterController.UpdateDetails(searchDetails, info);
            return status;
        }

        [WebMethod]
        public bool DeleteCategoryMasterInfo(CAT_MAST searchDetails)
        {
            bool status = categoryMasterController.DeleteCategory(searchDetails);
            return status;
        }

        [WebMethod]
        public List<CAT_MAST> GetAllCategoryMasterInfo(CAT_MAST searchDetails)
        {
            List<CAT_MAST> details = categoryMasterController.GetAllCategoryDetails(searchDetails, 1);
            return details;
        }

        [WebMethod]
        public CAT_MAST GetAllCategoryDetailsById(CAT_MAST searchDetails)
        {
            List<CAT_MAST> details = categoryMasterController.GetAllCategoryDetails(searchDetails, 2);
            return details[0];
        }

        #endregion

        #region L2CategoryInfo

        [WebMethod]
        public List<CAT_L2> GetAllL2CategoryDetails(CAT_L2 searchDetails)
        {
            List<CAT_L2> details = categoryL2Controller.GetAllCategoryDetails(searchDetails, 1);
            return details;
        }

        [WebMethod]
        public bool InsertCategoryL2Info(CAT_L2 info)
        {
            bool status = categoryL2Controller.InsertNewCategory(info);
            return status;
        }

        [WebMethod]
        public bool UpdateCategoryL2Info(CAT_L2 searchInfo, CAT_L2 info)
        {
            bool status = categoryL2Controller.UpdateDetails(searchInfo, info);
            return status;
        }

        [WebMethod]
        public bool DeleteCategoryL2Info(CAT_L2 info)
        {
            bool status = categoryL2Controller.DeleteCategory(info);
            return status;
        }

        [WebMethod]
        public CAT_L2 GetAllL2CategoryDetailsById(CAT_L2 searchDetails)
        {
            List<CAT_L2> details = categoryL2Controller.GetAllCategoryDetails(searchDetails, 2);
            return details[0];
        }

        [WebMethod]
        public List<CAT_L2> GetAllL2CategoryDetailsByMasterId(CAT_L2 searchDetails)
        {
            List<CAT_L2> details = categoryL2Controller.GetAllCategoryDetails(searchDetails, 3);
            return details;
        }

        #endregion

        #region L3CategoryInfo

        [WebMethod]
        public bool InsertCategoryL3Info(CAT_L3 info)
        {
            bool status = categoryL3Controller.InsertNewCategory(info);
            return status;
        }

        [WebMethod]
        public bool UpdateCategoryL3Info(CAT_L3 searchDetails, CAT_L3 info)
        {
            bool status = categoryL3Controller.UpdateDetails(searchDetails, info);
            return status;
        }

        [WebMethod]
        public List<CAT_L3> GetAllL3CategoryDetails(CAT_L3 searchDetails)
        {
            List<CAT_L3> details = categoryL3Controller.GetAllCategoryDetails(searchDetails, 1);
            return details;
        }

        [WebMethod]
        public CAT_L3 GetAllL3CategoryDetailsById(CAT_L3 searchDetails)
        {
            List<CAT_L3> details = categoryL3Controller.GetAllCategoryDetails(searchDetails, 2);
            return details[0];
        }

        [WebMethod]
        public List<CAT_L3> GetAllL3CategoryDetailsByMastCatId(CAT_L3 searchDetails)
        {
            List<CAT_L3> details = categoryL3Controller.GetAllCategoryDetails(searchDetails, 3);
            return details;
        }

        [WebMethod]
        public bool DeleteCategoryL3Info(CAT_L3 searchDetails)
        {
            bool status = categoryL3Controller.DeleteCategory(searchDetails);
            return status;
        }

        #endregion

        #region L4CategoryInfo

        [WebMethod]
        public bool InsertCategoryL4Info(CAT_L4 info)
        {
            bool status = categoryL4Controller.InsertNewCategory(info);
            return status;
        }

        [WebMethod]
        public bool UpdateCategoryL4Info(CAT_L4 searchDetails, CAT_L4 info)
        {
            bool status = categoryL4Controller.UpdateDetails(searchDetails, info);
            return status;
        }

        [WebMethod]
        public List<CAT_L4> GetAllL4CategoryDetails(CAT_L4 searchDetails)
        {
            List<CAT_L4> details = categoryL4Controller.GetAllCategoryDetails(searchDetails, 1);
            return details;
        }

        [WebMethod]
        public CAT_L4 GetAllL4CategoryDetailsById(CAT_L4 searchDetails)
        {
            List<CAT_L4> details = categoryL4Controller.GetAllCategoryDetails(searchDetails, 2);
            return details[0];
        }

        [WebMethod]
        public List<CAT_L4> GetAllL4CategoryDetailsByMastCatId(CAT_L4 searchDetails)
        {
            List<CAT_L4> details = categoryL4Controller.GetAllCategoryDetails(searchDetails, 3);
            return details;
        }

        [WebMethod]
        public bool DeleteCategoryL4Info(CAT_L4 searchDetails)
        {
            bool status = categoryL4Controller.DeleteCategory(searchDetails);
            return status;
        }

        #endregion

        #region UnitDetails

        [WebMethod]
        public bool InsertUnitDetails(UNIT_MAST info)
        {
            bool status = unitDetailsController.InsertNewDetails(info);
            return status;
        }

        [WebMethod]
        public bool UpdateUnitDetails(UNIT_MAST searchdetails, UNIT_MAST details)
        {
            bool status = unitDetailsController.UpdateDetails(searchdetails, details);
            return status;
        }

        [WebMethod]
        public bool DeleteUnitDetails(string compcode, string usercode)
        {
            bool status = unitDetailsController.DeleteDetails(compcode, usercode);
            return status;
        }

        [WebMethod]
        public List<UNIT_MAST> GetAllUnitDetailsByCompany(string networkId)
        {
            List<UNIT_MAST> details = unitDetailsController.GetAllUnitDetailsByCompany(networkId);
            return details;
        }

        [WebMethod]
        public UNIT_MAST GetAllUnitDetailsById(string compcode, string unitCode)
        {
            UNIT_MAST details = unitDetailsController.GetAllDetailsById(compcode, unitCode);
            return details;
        }

        #endregion

        #region LocationDetails

        [WebMethod]
        public bool InsertLocationDetails(LOCA_MAST info)
        {
            bool status = locationDetailsController.InsertNewDetails(info);
            return status;
        }

        [WebMethod]
        public bool UpdateLocationDetails(LOCA_MAST searchdetails, LOCA_MAST details)
        {
            bool status = locationDetailsController.UpdateDetails(searchdetails, details);
            return status;
        }

        [WebMethod]
        public bool DeleteLocationDetails(string code, string compCode)
        {
            bool status = locationDetailsController.DeleteDetails(code, compCode);
            return status;
        }

        [WebMethod]
        public List<LOCA_MAST> GetAllLocationDetails(string networkId)
        {
            List<LOCA_MAST> details = locationDetailsController.GetAllDetails(networkId);
            return details;
        }

        [WebMethod]
        public LOCA_MAST GetAllLocationDetailsById(string netCode, string locaCode)
        {
            LOCA_MAST details = locationDetailsController.GetAllDetailsById(netCode, locaCode);
            return details;
        }


        #endregion

        #region LocationCustomize
        [WebMethod]
        public bool InsertLocationCustomize(LOCA_DETAIL info)
        {
            bool status = locationCustomizeController.InsertNewDetails(info);
            return status;
        }

        [WebMethod]
        public bool UpdateLocationCustomize(LOCA_DETAIL searchdetails, LOCA_DETAIL details)
        {
            bool status = locationCustomizeController.UpdateDetails(searchdetails, details);
            return status;
        }

        [WebMethod]
        public bool DeleteLocationCustomize(string code, string compCode)
        {
            bool status = locationCustomizeController.DeleteDetails(code, compCode);
            return status;
        }

        [WebMethod]
        public List<LOCA_DETAIL> GetAllLocationCustomize(string networkId)
        {
            List<LOCA_DETAIL> details = locationCustomizeController.GetAllDetails(networkId);
            return details;
        }

        [WebMethod]

        public List<USERINFO> GetAllUsersDetails()
        {
            List<USERINFO> details = locationCustomizeController.GetAllUsers();
            return details;
        }

        #endregion

        #region SupplierDetails

        [WebMethod]
        public bool InsertSupplierDetails(SUPP_MAST info)
        {
            bool status = supplierMasterDetailsController.InsertNewDetails(info);
            return status;
        }


        [WebMethod]
        public bool UpdateSupplierDetails(SUPP_MAST searchdetails, SUPP_MAST details)
        {
            bool status = supplierMasterDetailsController.UpdateDetails(searchdetails, details);
            return status;
        }

        [WebMethod]
        public bool DeleteSupplierDetails(string compCode, string suppCode)
        {
            bool status = supplierMasterDetailsController.DeleteDetails(compCode, suppCode);
            return status;
        }

        [WebMethod]
        public List<SUPP_MAST> GetAllSupplierDetailsByCompany(string tableName, string searchText, string locationId, string companyId, string searchColumn)
        {
            List<SUPP_MAST> details = supplierMasterDetailsController.GetAllDetailsByCompany(tableName, searchText, locationId, companyId, searchColumn);
            return details;
        }

        [WebMethod]
        public SUPP_MAST GetAllSupplierDetailsById(string code, string companyid)
        {
            SUPP_MAST details = supplierMasterDetailsController.GetAllDetailsById(code, companyid);
            return details;
        }

        #endregion

        #region CustomerDetails

        [WebMethod]
        public bool InsertCustomerDetails(CUST_MAST info)
        {
            bool status = customerMastDetailsController.InsertNewDetails(info);
            return status;
        }

        [WebMethod]
        public bool UpdateCustomerDetails(CUST_MAST searchdetails, CUST_MAST details)
        {
            bool status = customerMastDetailsController.UpdateDetails(searchdetails, details);
            return status;
        }

        [WebMethod]
        public bool DeleteCustomerDetails(CUST_MAST searchdetails)
        {
            bool status = customerMastDetailsController.DeleteDetails(searchdetails);
            return status;
        }

        [WebMethod]
        public List<CUST_MAST> GetAllCustomerDetailsByCompany(string companyId)
        {
            List<CUST_MAST> details = customerMastDetailsController.GetAllDetailsByCompany(companyId);
            return details;
        }

        [WebMethod]
        public CUST_MAST GetAllCustomerDetailsById(string companyId, string code)
        {
            CUST_MAST details = customerMastDetailsController.GetAllDetailsById(companyId, code);
            return details;
        }

        [WebMethod]
        public CUSTOMER_LEDGER GetCustomerOutstandingById(string companyId, string custCode)
        {
            CUSTOMER_LEDGER details = customerMastDetailsController.GetCustomerOutstandingById(companyId, custCode);
            return details;
        }

        #endregion

        #region ItemCutList
        [WebMethod]
        public List<ITEM_CUTLIST> GetAllItemCustList(ITEM_CUTLIST searchDetails)
        {
            List<ITEM_CUTLIST> details = itemCutListController.GetItemCustList(searchDetails);
            return details;
        }
        [WebMethod]
        public bool InsertCutItems(ITEM_CUTLIST searchDetails)
        {
            bool status = itemCutListController.InsertNewDetails(searchDetails);
            return status;
        }

        [WebMethod]
        public bool DeleteCutItems(ITEM_CUTLIST searchDetails)
        {
            bool status = itemCutListController.DeleteDetails(searchDetails);
            return status;
        }
        #endregion

        #region ItemDetails

        [WebMethod]
        public bool InsertItemDetails(ITEM_MAST info, List<ITEM_SUPP> itemsuppliers)
        {
            bool status = itemDetailsController.InsertNewDetails(info);
            status = itemSupplierController.InsertNewSupplier(itemsuppliers);

            return status;
        }

        [WebMethod]
        public bool UpdateItemDetails(ITEM_MAST searchDetails, ITEM_MAST details, List<ITEM_SUPP> itemsuppliers)
        {
            bool status = itemDetailsTransactions.UpdateItemDetails(searchDetails, details, itemsuppliers);
            return status;
        }

        [WebMethod]
        public bool DeleteItemDetails(ITEM_MAST searchDetails, ITEM_SUPP suppDetails)
        {
            bool status = itemDetailsTransactions.DeleteItemDetails(searchDetails, suppDetails);
            return status;
        }

        [WebMethod]
        public List<ITEM_MAST> GetAllItemDetailsByAllLocation(ITEM_MAST searchDetails)
        {
            List<ITEM_MAST> details = itemDetailsController.GetAllItemDetailsForAllLocations(searchDetails);
            return details;
        }

        [WebMethod]
        public List<ITEM_MAST> GetAllDetailsByLocation(string locationId, string companyid)
        {
            List<ITEM_MAST> details = itemDetailsController.GetAllDetailsByLocation(locationId, companyid);
            return details;
        }

        [WebMethod]
        public ITEM_MAST GetAllItemDetailsById(ITEM_MAST searchDetails)
        {
            ITEM_MAST details = itemDetailsController.GetAllDetailsById(searchDetails);
            return details;
        }


        [WebMethod]
        public List<ITEM_MAST> GetAllDetailsByItemOrder(ITEM_MAST searchdetails, string fromvalue, string toValue)
        {
            List<ITEM_MAST> details = itemDetailsController.GetAllDetailsByItemOrder(searchdetails, fromvalue, toValue);
            return details;
        }

        [WebMethod]
        public bool UpdateItemsDetailsByColumn(List<ITEM_MAST> details, int option)
        {
            foreach (ITEM_MAST item in details)
            {
                ITEM_MAST status = itemDetailsController.UpdateDetailsByColumn(item, option);
            }

            return true;
        }

        #endregion

        #region ItemSupplierDetails

        [WebMethod]
        public List<ITEM_SUPP> GetAllSupplierDetailsByItemId(ITEM_SUPP suppDetails)
        {
            List<ITEM_SUPP> details = itemSupplierController.GetAllSupplierDetailsByItemId(suppDetails);
            return details;
        }

        #endregion

        #region F2Help
        [WebMethod]
        public F2HELP_MAST GetAllF2HELP_MASTDetailsBySeqNo(int seqNo)
        {
            F2HELP_MAST helpMast = helpController.GetAllDetailsBySeqNo(seqNo);

            return helpMast;
        }

        [WebMethod]
        public Object GetAllDetailedObjectInfo(string tableName, string companyId, string locationId, string searchText, string searchColumn)
        {
            switch (tableName)
            {
                case "LOCA_MAST":
                    return locationDetailsController.GetAllDetailsForLoginUser(companyId, searchText);
                case "CUST_MAST":
                    return customerMastDetailsController.GetAllDetailsByFilterCriteria(searchText, "CUST_CODE");
                //return customerMastDetailsController.GetAllDetailsByCompany(companyId);
                case "ITEM_MAST":
                    return itemDetailsController.GetAllDetailsByFilterCriteria(tableName, searchText, locationId, companyId, searchColumn);
                case "SUPP_MAST":
                    return supplierMasterDetailsController.GetAllDetailsByCompany(tableName, searchText, locationId, companyId, searchColumn);
                case "LOCA_DETAIL":
                    return locationCustomizeController.GetAllLocationDetails(searchText);
                default:
                    break;
            }
            return null;
        }

        [WebMethod]
        public Object GetAllFilteredInfo(string tableName, string searchValue, string locationId, string companyid, string searchColumn)
        {
            switch (tableName)
            {
                case "LOCA_MAST": break;
                //return locationDetailsController.GetAllDetails(companyId);
                case "ITEM_MAST":
                    return itemDetailsController.GetAllDetailsByFilterCriteria(tableName, searchValue, locationId, companyid, searchColumn);
                case "CUST_MAST":
                    return customerMastDetailsController.GetAllDetailsByFilterCriteria(searchValue, searchColumn);
                case "SUPP_MAST":
                    return customerMastDetailsController.GetAllDetailsByFilterCriteria(searchValue, searchColumn);

                default:
                    break;
            }
            return null;
        }

        #endregion

        #region PhysicalStockDetailsController

        [WebMethod]
        public string StockAdjustmentsInsertNewDetails(List<PHY_MAST> PHY_MAST_List, string docNumber)
        {

            return physicalStockDetailsTransactions.StockAdjustmentsInsertNewDetails(PHY_MAST_List, docNumber);

        }
        #endregion

        #region StockAdjustmentsDetailsController

        [WebMethod]
        public string PhysicalStockInsertNewDetails(List<PHY_MAST> PHY_MAST_List, string docNumber)
        {

            return physicalStockDetailsTransactions.PhysicalStockInsertNewDetails(PHY_MAST_List, docNumber);

        }
        #endregion

        #region CutNotes

        [WebMethod]
        public string AddCutNoteDetails(ITEM_MAST details)
        {
            return invoiceMastTransactions.AddCutNote(details);
        }


        #endregion

        #region InvoiceMast

        [WebMethod]
        public string InsertInvoiceMastDetails(INV_MAST details, List<INV_DETAIL> itemDetails)
        {
            return invoiceMastTransactions.InsertInvoiceMastDetails(details, itemDetails);
        }

        [WebMethod]
        public string CancelInvMastDetails(INV_MAST details)
        {
            return invoiceMastTransactions.CancelInvoice(details);
        }


        #endregion

        #region ReceiptMaster

        #endregion

        #region ReceiptDetail

        #endregion

        #region ReceiptInv

        #endregion

        //#region L1AccountInfo

        ////[WebMethod]
        ////public bool InsertAccountMasterInfo(ACC_CAT_L1 info)
        ////{
        ////    bool status = accountMasterController.InsertNewCategory(info);
        ////    return status;
        ////}

        ////[WebMethod]
        ////public List<ACC_CAT_L1> GetAllAccountMasterInfo(ACC_CAT_L1 searchDetails)
        ////{
        ////    List<ACC_CAT_L1> details = accountMasterController.GetAllAccountDetails(searchDetails, 1);
        ////    return details;
        ////}

        ////[WebMethod]
        ////public bool DeleteAccountMasterInfo(ACC_CAT_L1 searchDetails)
        ////{
        ////    bool status = accountMasterController.DeleteAccountDetails(searchDetails);
        ////    return status;
        ////}


        ////[WebMethod]
        ////public ACC_CAT_L1 GetAllAccountDetailsById(ACC_CAT_L1 searchDetails)
        ////{
        ////    List<ACC_CAT_L1> details = accountMasterController.GetAllAccountDetails(searchDetails, 2);
        ////    return details[0];
        ////}

        ////[WebMethod]
        ////public bool UpdateCategoryMasterInfoAcc(ACC_CAT_L1 searchDetails, ACC_CAT_L1 info)
        ////{
        ////    bool status = accountMasterController.UpdateDetailsAcc(searchDetails, info);
        ////    return status;
        ////}
        //#endregion

        //#region L2AccountInfo

        //[WebMethod]
        //public bool InsertAccountMasterInfoACCL2(ACC_CAT_L2 info)
        //{
        //    bool status = accountMasterController2.InsertNewCategoryL2(info);
        //    return status;
        //}


        //[WebMethod]
        //public List<ACC_CAT_L2> GetAllL2AccountDetailsByMasterId(ACC_CAT_L2 searchDetails)
        //{
        //    List<ACC_CAT_L2> details = accountMasterController2.GetAllAccountDetailsL2(searchDetails, 1);
        //    return details;
        //}

        //[WebMethod]
        //public ACC_CAT_L2 GetAllL2AccountDetailsById(ACC_CAT_L2 searchDetails)
        //{
        //    List<ACC_CAT_L2> details = accountMasterController2.GetAllAccountDetailsL2(searchDetails, 2);
        //    return details[0];
        //}

        //[WebMethod]
        //public bool UpdateAccountL2Info(ACC_CAT_L2 searchInfo, ACC_CAT_L2 info)
        //{
        //    bool status = accountMasterController2.UpdateDetails(searchInfo, info);
        //    return status;
        //}

        //[WebMethod]
        //public bool DeleteAccountL2Info(ACC_CAT_L2 info)
        //{
        //    bool status = accountMasterController2.DeleteCategory(info);
        //    return status;
        //}

        //#endregion

        //#region L3AccountInfo

        //[WebMethod]
        //public bool InsertAccountL3Info(ACC_CAT_L3 info)
        //{
        //    bool status = accountMasterController3.InsertNewCategory(info);
        //    return status;
        //}

        //[WebMethod]
        //public bool UpdateAccountL3Info(ACC_CAT_L3 searchDetails, ACC_CAT_L3 info)
        //{
        //    bool status = accountMasterController3.UpdateDetails(searchDetails, info);
        //    return status;
        //}

        //[WebMethod]
        //public List<ACC_CAT_L3> GetAllL3AccountDetails(ACC_CAT_L3 searchDetails)
        //{
        //    List<ACC_CAT_L3> details = accountMasterController3.GetAllAccountDetails(searchDetails, 1);
        //    return details;
        //}

        //[WebMethod]
        //public ACC_CAT_L3 GetAllL3AccountDetailsById(ACC_CAT_L3 searchDetails)
        //{
        //    List<ACC_CAT_L3> details = accountMasterController3.GetAllAccountDetails(searchDetails, 2);
        //    return details[0];
        //}

        //[WebMethod]
        //public List<ACC_CAT_L3> GetAllL3AccountDetailsByMastCatId(ACC_CAT_L3 searchDetails)
        //{
        //    List<ACC_CAT_L3> details = accountMasterController3.GetAllAccountDetails(searchDetails, 3);
        //    return details;
        //}

        //[WebMethod]
        //public bool DeleteAccountL3Info(ACC_CAT_L3 searchDetails)
        //{
        //    bool status = accountMasterController3.DeleteCategory(searchDetails);
        //    return status;
        //}

        //#endregion

        //#region L4AccountInfo

        //[WebMethod]
        //public bool InsertAccountL4Info(ACC_CAT_L4 info)
        //{
        //    bool status = accountMasterController4.InsertNewCategory(info);
        //    return status;
        //}

        //[WebMethod]
        //public bool UpdateAccountL4Info(ACC_CAT_L4 searchDetails, ACC_CAT_L4 info)
        //{
        //    bool status = accountMasterController4.UpdateDetails(searchDetails, info);
        //    return status;
        //}

        //[WebMethod]
        //public List<ACC_CAT_L4> GetAllL4AccountDetails(ACC_CAT_L4 searchDetails)
        //{
        //    List<ACC_CAT_L4> details = accountMasterController4.GetAllAccountDetails(searchDetails, 1);
        //    return details;
        //}

        //[WebMethod]
        //public ACC_CAT_L4 GetAllL4AccountDetailsById(ACC_CAT_L4 searchDetails)
        //{
        //    List<ACC_CAT_L4> details = accountMasterController4.GetAllAccountDetails(searchDetails, 2);
        //    return details[0];
        //}

        //[WebMethod]
        //public List<ACC_CAT_L4> GetAllL4AccountDetailsByMastCatId(ACC_CAT_L4 searchDetails)
        //{
        //    List<ACC_CAT_L4> details = accountMasterController4.GetAllAccountDetails(searchDetails, 3);
        //    return details;
        //}

        //[WebMethod]
        //public bool DeleteAccountL4Info(ACC_CAT_L4 searchDetails)
        //{
        //    bool status = accountMasterController4.DeleteCategory(searchDetails);
        //    return status;
        //}

        //#endregion

        //#region L5AccountInfo
        //[WebMethod]
        //public bool InsertAccountL5Info(ACC_CAT_L5 info)
        //{
        //    bool status = accountMasterController5.InsertNewCategory(info);
        //    return status;
        //}

        //[WebMethod]
        //public bool UpdateAccountL5Info(ACC_CAT_L5 searchDetails, ACC_CAT_L5 info)
        //{
        //    bool status = accountMasterController5.UpdateDetails(searchDetails, info);
        //    return status;
        //}

        //[WebMethod]
        //public List<ACC_CAT_L5> GetAllL5AccountDetails(ACC_CAT_L5 searchDetails)
        //{
        //    List<ACC_CAT_L5> details = accountMasterController5.GetAllAccountDetails(searchDetails, 1);
        //    return details;
        //}

        //[WebMethod]
        //public ACC_CAT_L5 GetAllL5AccountDetailsById(ACC_CAT_L5 searchDetails)
        //{
        //    List<ACC_CAT_L5> details = accountMasterController5.GetAllAccountDetails(searchDetails, 2);
        //    return details[0];
        //}

        //[WebMethod]
        //public List<ACC_CAT_L5> GetAllL5AccountDetailsByMastCatId(ACC_CAT_L5 searchDetails)
        //{
        //    List<ACC_CAT_L5> details = accountMasterController5.GetAllAccountDetails(searchDetails, 3);
        //    return details;
        //}

        //[WebMethod]
        //public bool DeleteAccountL5Info(ACC_CAT_L5 searchDetails)
        //{
        //    bool status = accountMasterController5.DeleteCategory(searchDetails);
        //    return status;
        //}
        //#endregion


        #region TransMast

        [WebMethod]
        public string InsertTransMastDetails(TRANS_MAST details, List<TRANS_DETAIL> itemDetails)
        {
            return transMastTransactions.AddTransferNote(details, itemDetails);
        }


        #endregion

        #region TransactionTypes
        [WebMethod]
        public List<TXN_TYPES> GetAllTxnTypes(string compcode, string strType)
        {
            List<TXN_TYPES> details = transactionTypesController.GetTransactionTypes(compcode, strType);
            return details;
        }
        #endregion

        #region GRN

        [WebMethod]
        public string InsertGRNeMastDetails(GRN_MAST details, List<GRN_DETAIL> itemDetails)
        {
            return grnMastTransactions.InsertGRNMastDetails(details, itemDetails);
        }


        [WebMethod]
        public bool InsertGRNDetails(GRN_DETAIL info)
        {
            bool status = grnDetailControllers.InsertNewDetails(info);
            return status;
        }

        #endregion

        #region SalesTypes
        [WebMethod]
        public List<PRICE_TYPES> GetAllSalesPriceTypes(string compcode, string strType)
        {
            List<PRICE_TYPES> details = salesPriceTypesController.GetAllPriceTypes(compcode, strType);
            return details;
        }

        #endregion

        //////Common Functions
        // #region DeductStockController
        // [WebMethod]
        // public string DeductStock(List<STOCK> STOCK_List)
        // {
        //     int count = 1;
        //     foreach (STOCK stock in STOCK_List)
        //     {

        //         //Insert to STOCK Table ( Common function – ADD STOCKS )
        //         STOCK stockdetails = new STOCK();
        //         stockdetails.COMPCODE = stock.COMPCODE;
        //         stockdetails.LOCA = stock.LOCA;
        //         stockdetails.ITEM = stock.ITEM;
        //         stockdetails.TXN_TYPE = stock.TXN_TYPE;
        //         stockdetails.DOC_NO = stock.DOC_NO;
        //         stockdetails.CSH_CRD = stock.CSH_CRD;
        //         stockdetails.LOCA_TO = stock.LOCA_TO;
        //         stockdetails.UNIT = stock.UNIT;
        //         stockdetails.LOCA_TO_DESC = stock.LOCA_TO_DESC;
        //         stockdetails.TXN_DATE = stock.TXN_DATE;
        //         stockdetails.SEQ_NO = stock.SEQ_NO;
        //         stockdetails.REFNO = stock.REFNO;
        //         stockdetails.PURCHASE_PRICE = stock.PURCHASE_PRICE;
        //         stockdetails.COST_PRICE = stock.COST_PRICE;
        //         stockdetails.BULK_UNIT = stock.BULK_UNIT;
        //         stockdetails.LOOSE_UNIT = stock.LOOSE_UNIT;
        //         stockdetails.BULK_QTY = stock.BULK_QTY;
        //         stockdetails.LOOSE_QTY = stock.LOOSE_QTY;
        //         stockdetails.DOC_BAL_BULK_QTY = stock.DOC_BAL_BULK_QTY;
        //         stockdetails.DOC_BAL_LOOSE_QTY = stock.DOC_BAL_LOOSE_QTY;
        //         stockdetails.STOCK_TYPE = stock.STOCK_TYPE;
        //         stockdetails.IS_MONTH_END = 0;

        //         count++;

        //         stockDetailsController.InsertNewDetails(stockdetails);

        //         //Update existing item BULK and LOOSE quantities in ITEM_MASTER table – In this screen need to add to existing QTYs. NOT overwrite.
        //         ITEM_MAST details = new ITEM_MAST();
        //         details.ITEM = stock.ITEM;
        //         details.COMPCODE = stock.COMPCODE;
        //         details.LOCA_CODE = stock.LOCA;
        //         details.UNIT_1_QTY = stock.BULK_QTY;//(Existing QTY) + stock.BULK_QTY ;
        //         details.UNIT_2_QTY = stock.LOOSE_QTY;//(Existing QTY) + stock.LOOSE_QTY;

        //         details = itemDetailsController.DeductItemQty(details);

        //     }

        //     return "0";
        // }
        // #endregion

        #region DeductStockController
        //[WebMethod]
        //public string DeductStock(List<STOCK> STOCK_List)
        //{
        ////    int count = 1;

        ////    foreach (STOCK stock in STOCK_List)
        ////    {

        ////        //Insert to STOCK Table ( Common function – Deduct Stock )
        ////        STOCK stockdetails = new STOCK();
        ////        stockdetails.COMPCODE = stock.COMPCODE;
        ////        stockdetails.LOCA = stock.LOCA;
        ////        stockdetails.ITEM = stock.ITEM;
        ////        stockdetails.TXN_TYPE = stock.TXN_TYPE;
        ////        stockdetails.DOC_NO = stock.DOC_NO;
        ////        stockdetails.CSH_CRD = stock.CSH_CRD;
        ////        stockdetails.LOCA_TO = stock.LOCA_TO;
        ////        stockdetails.LOCA_TO_DESC = stock.LOCA_TO_DESC;
        ////        stockdetails.TXN_DATE = stock.TXN_DATE;
        ////        stockdetails.SEQ_NO = stock.SEQ_NO;
        ////        stockdetails.REFNO = stock.REFNO;
        ////        stockdetails.PURCHASE_PRICE = stock.PURCHASE_PRICE;
        ////        stockdetails.COST_PRICE = stock.COST_PRICE;
        ////        stockdetails.BULK_UNIT = stock.BULK_UNIT;
        ////        stockdetails.LOOSE_UNIT = stock.LOOSE_UNIT;
        ////        stockdetails.BULK_QTY = stock.BULK_QTY * -1;
        ////        stockdetails.LOOSE_QTY = stock.LOOSE_QTY * -1;
        ////        stockdetails.DOC_BAL_BULK_QTY = 0;
        ////        stockdetails.DOC_BAL_LOOSE_QTY = 0;
        ////        stockdetails.STOCK_TYPE = stock.STOCK_TYPE;
        ////        stockdetails.IS_MONTH_END = 0;

        ////        count++;

        ////        stockDetailsController.InsertNewDetails(stockdetails);


        ////        //1. Deduct bulk qty
        ////        decimal tmpQTY=0;
        ////        decimal tmpBAL=0;
        ////        List<STOCK> BULKSTOCK_List  = new List<STOCK>;
        ////        //BULKSTOCK_List = Select * from STOCK where COMPCODE= stock.COMPCODE and LOCA= stock.LOCA and ITEM= stock.ITEM and DOC_BAL_BULK_QTY >0 order by TXNDATE,TXN_TYPE,DOC_NO
        ////        foreach (STOCK bulkStock in BULKSTOCK_List)
        ////        {
        ////                tmpQTY = stock.BULK_QTY;
        ////                while (tmpQTY > 0)
        ////                {   
        ////                   if (tmpQTY > bulkStock.DOC_BAL_BULK_QTY) 
        ////                    {
        ////                       tmpQTY = (tmpQTY - bulkStock.DOC_BAL_BULK_QTY);
        ////                       tmpBAL = 0;
        ////                    }
        ////                    else
        ////                    {
        ////                       tmpBAL = (bulkStock.DOC_BAL_BULK_QTY - tmpQTY);                    
        ////                       tmpQTY = 0;
        ////                    }

        ////                    //Update  STOCK set DOC_BAL_BULK_QTY = tmpBAL  where  Item = bulkStock.ITEM and compCode = bulkStock.COMPCODE and LOCA= bulkStock.LOCA and TXN_TYPE = bulkStock.TXN_TYPE and Doc_No = bulkStock.DOC_NO and CSH_CRD =bulkStock.CSH_CRD ")

        ////                }                 
        ////        }

        ////         //1. Deduct loose qty
        ////        tmpQTY=0;
        ////        tmpBAL=0;
        ////        List<STOCK> LOOSESTOCK_List  = new List<STOCK>;
        ////        //LOOSESTOCK_List = Select * from STOCK where COMPCODE= stock.COMPCODE and LOCA= stock.LOCA and ITEM= stock.ITEM and DOC_BAL_LOOSE_QTY >0 order by TXNDATE,TXN_TYPE,DOC_NO
        ////        foreach (STOCK looseStock in LOOSESTOCK_List)
        ////        {
        ////                tmpQTY = stock.LOOSE_QTY;
        ////                while (tmpQTY > 0)
        ////                {   
        ////                   if (tmpQTY > looseStock.DOC_BAL_LOOSE_QTY) 
        ////                    {
        ////                       tmpQTY = (tmpQTY - looseStock.DOC_BAL_LOOSE_QTY);
        ////                       tmpBAL = 0;
        ////                    }
        ////                    else
        ////                    {
        ////                       tmpBAL = (looseStock.DOC_BAL_LOOSE_QTY - tmpQTY);                    
        ////                       tmpQTY = 0;
        ////                    }

        ////                    //Update  STOCK set DOC_BAL_LOOSE_QTY = tmpBAL  where  Item = looseStock.ITEM and compCode = looseStock.COMPCODE and LOCA= looseStock.LOCA and TXN_TYPE = looseStock.TXN_TYPE and Doc_No = looseStock.DOC_NO and CSH_CRD =looseStock.CSH_CRD ")

        ////                }                 
        ////        }

        ////        //Update existing item BULK and LOOSE quantities in ITEM_MASTER table – In this screen need to add to existing QTYs. NOT overwrite.
        ////        ITEM_MAST details = new ITEM_MAST();
        ////        details.ITEM = stock.ITEM;
        ////        details.COMPCODE = stock.COMPCODE;
        ////        details.LOCA_CODE = stock.LOCA;
        ////        details.UNIT_1_QTY = (Existing QTY) - stock.BULK_QTY ;
        ////        details.UNIT_2_QTY = (Existing QTY) - stock.LOOSE_QTY;

        ////        details = itemDetailsController.UpdateDetailsByColumn(details, 1); 



        ////    }

        //    return "0";
        //}
        #endregion


    }
}
