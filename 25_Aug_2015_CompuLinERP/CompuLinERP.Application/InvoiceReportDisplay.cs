using CompuLinERP.WIN.InventoryWebService;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace CompuLinINV.WIN
{
    public partial class InvoiceReportDisplay : Form
    {
        InventoryWebService webService = new InventoryWebService();
        string _documentNumber;
        string _location;
        string _csh_crd;
        string _reportType;
        string _itemCode;
        string _compCode;
        string _compName;
        DateTime _fromDate;
        DateTime _toDate;

        public InvoiceReportDisplay(string documentNumber, string location, string csh_crd, string reportType, string itemCode, string CompCode, string CompName , DateTime FromDate, DateTime ToDate)
        {
            InitializeComponent();
            _documentNumber = documentNumber;
            _location = location;
            _csh_crd = csh_crd;
            _reportType = reportType;
            _itemCode = itemCode;
            _compCode = CompCode;
            _fromDate = FromDate;
            _toDate = ToDate;
            NETWORK[] comp = webService.GetCompanyDetailsById(CompCode);
            _compName = comp[0].CompName;
            Loadreport();

        }
              

        private void Loadreport()
        {
            if (!String.IsNullOrEmpty(_documentNumber))
            {

                //Create object of report
                ReportDocument cryRpt = new ReportDocument();
                ParameterFields paramFields = new ParameterFields();
                ParameterField paramField = new ParameterField();

                this.crystalReportViewer1.RefreshReport();

                if (_reportType == "INV" || _reportType == "INV_NO")
                {
                    if (_compCode == "001")
                    {
                        cryRpt.Load("Invoice001.rpt");
                    }
                    else if (_compCode == "002")
                    {
                        cryRpt.Load("Invoice002.rpt");
                    }
                    else if (_compCode == "003")
                    {
                        cryRpt.Load("Invoice003.rpt");
                    }
                }
                else if (_reportType == "BINCARD")
                {
                    cryRpt.Load("StockMovement.rpt");
                }
                else if (_reportType == "PHY_ALL")
                {
                    cryRpt.Load("PhysicalEntryList.rpt");
                }
                else if (_reportType == "ITEM_LIST")
                {
                    cryRpt.Load("ItemList.rpt");
                }
                else if (_reportType == "PRICE_LIST")
                {
                    cryRpt.Load("ItemPriceList.rpt");
                }
                else if (_reportType == "SALES_REPOR")
                {
                    cryRpt.Load("SalesReport.rpt");
                }
                else if (_reportType == "TRN_NO")
                {
                    cryRpt.Load("TransferNote.rpt");
                }
                else if (_reportType == "GRN_NO")
                {
                    cryRpt.Load("GRN.rpt");
                }
                else
                {
                    cryRpt.Load("Invoice.rpt");
                }
                ConnectionInfo connectionInfo = new ConnectionInfo();
                connectionInfo.ServerName = System.Configuration.ConfigurationManager.AppSettings.Get("DatabaseDataSource");
                connectionInfo.UserID = System.Configuration.ConfigurationManager.AppSettings.Get("DatabaseUsername");
                connectionInfo.Password = System.Configuration.ConfigurationManager.AppSettings.Get("DatabasePassword");
                connectionInfo.DatabaseName = System.Configuration.ConfigurationManager.AppSettings.Get("DatabaseTable");

                TableLogOnInfo newLogonInfo = null;

                foreach (CrystalDecisions.CrystalReports.Engine.Table currentTable in cryRpt.Database.Tables)
                {
                    newLogonInfo = currentTable.LogOnInfo;
                    newLogonInfo.ConnectionInfo = connectionInfo;
                    currentTable.ApplyLogOnInfo(newLogonInfo);
                }

                ////set database login information
                cryRpt.SetDatabaseLogon
                    (System.Configuration.ConfigurationManager.AppSettings.Get("DatabaseUsername"),
                    System.Configuration.ConfigurationManager.AppSettings.Get("DatabasePassword"),
                    System.Configuration.ConfigurationManager.AppSettings.Get("DatabaseDataSource"),
                    System.Configuration.ConfigurationManager.AppSettings.Get("DatabaseTable"));


                crystalReportViewer1.RefreshReport();

                //This is the required code to add parameters +++++++++++++++++++++++++++++++++++++++++++++++++++
                //ParameterField pCompanyName = new ParameterField();
                //pCompanyName.ParameterFieldName = "CN";
                //ParameterDiscreteValue dcpCompanyName = new ParameterDiscreteValue();
                //dcpCompanyName.Value = _compName;
                //pCompanyName.CurrentValues.Add(dcpCompanyName);
                //paramFields.Add(pCompanyName);
                //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

                ParameterField pCompanyName = new ParameterField();
                pCompanyName.ParameterFieldName = "CN";
                ParameterDiscreteValue dcpCompanyName = new ParameterDiscreteValue();
                dcpCompanyName.Value = _compName;

                ParameterField pReportName = new ParameterField();
                pReportName.ParameterFieldName = "REPN";
                ParameterDiscreteValue dcpReportName = new ParameterDiscreteValue();               
                


                //{INV_DETAIL.INV_NO}
                if (_reportType == "INV" || _reportType == "INV_NO")
                {
                    ParameterField pfItemYr = new ParameterField();
                    pfItemYr.ParameterFieldName = "COPY";
                    ParameterDiscreteValue dcItemYr = new ParameterDiscreteValue();
                    dcItemYr.Value = "COPY";
                    pfItemYr.CurrentValues.Add(dcItemYr);
                    paramFields.Add(pfItemYr);

                    dcpReportName.Value = "INVOICE";
                    pReportName.CurrentValues.Add(dcpReportName);

                    //dcpReportName.Value = "INVOICE";
                    

                    crystalReportViewer1.SelectionFormula = ("{INV_DETAIL.COMPCODE}='" + _compCode + "' and {INV_MAST.INV_NO} = '" + _documentNumber + "' and {INV_MAST.LOCA}='" + _location + "' and {INV_DETAIL.CSH_CRD}= '" + _csh_crd + "'");
                }
                else if (_reportType == "BINCARD")
                {
                    dcpReportName.Value = "Stock Moment Report as at " + DateTime.Now;
                    pReportName.CurrentValues.Add(dcpReportName);
                    crystalReportViewer1.SelectionFormula = ("{STOCK.COMPCODE} = '" + _compCode + "' and {STOCK.LOCA}='" + _location + "' and {STOCK.ITEM}= '" + _itemCode + "'");
                }
                else if (_reportType == "PHY_ALL")
                {
                    dcpReportName.Value = "Pysical Entry List as at " + DateTime.Now;
                    pReportName.CurrentValues.Add(dcpReportName);                    
                    crystalReportViewer1.SelectionFormula = ("{PHY_MAST.COMPCODE} = '" + _compCode + "' and {PHY_MAST.LOCA}='" + _location + "'");
                }
                else if (_reportType == "ITEM_LIST")
                {
                    dcpReportName.Value = "Items List as at " + DateTime.Now;
                    pReportName.CurrentValues.Add(dcpReportName);
                    crystalReportViewer1.SelectionFormula = ("{ITEM_MAST.COMPCODE} = '" + _compCode + "' and {ITEM_MAST.LOCA_CODE}='" + _location + "'");
                }
                else if (_reportType == "PRICE_LIST")
                {
                    dcpReportName.Value = "Item Price List as at " + DateTime.Now;
                    pReportName.CurrentValues.Add(dcpReportName);
                    crystalReportViewer1.SelectionFormula = ("{ITEM_MAST.COMPCODE} = '" + _compCode + "' and {ITEM_MAST.LOCA_CODE}='" + _location + "'");
                }
                //else if (_reportType == "GRN_NO")
                //{
                //    dcpReportName.Value = "TRANSFER NOTE";
                //    pReportName.CurrentValues.Add(dcpReportName);
                //    crystalReportViewer1.SelectionFormula = ("{GRN_MAST.COMPCODE} = '" + _compCode + "' and {GRN_MAST.LOCA}='" + _location + "' and {GRN_MAST.GRN_NO} = '" + _documentNumber + "'");
                //}
                else if (_reportType == "GRN_NO")
                {
                    dcpReportName.Value = "GOODS RECEIVED NOTE";
                    pReportName.CurrentValues.Add(dcpReportName);                    
                    crystalReportViewer1.SelectionFormula = ("{GRN_MAST.COMPCODE} = '" + _compCode + "' and {GRN_MAST.LOCA}='" + _location + "' and {GRN_MAST.GRN_NO} = '" + _documentNumber + "'");
                }
                else if (_reportType == "TRN_NO")
                {
                    dcpReportName.Value = "GOODS TRANSFER NOTE";
                    pReportName.CurrentValues.Add(dcpReportName);
                    crystalReportViewer1.SelectionFormula = ("{TRANS_MAST.COMPCODE} = '" + _compCode + "' and {TRANS_MAST.LOCA_FROM}='" + _location + "' and {TRANS_MAST.TRANS_NO} = '" + _documentNumber + "'");
                    
                }
                else if (_reportType == "SALES_REPOR")
                {
                    dcpReportName.Value = "SALES REPORT - FROM: " + _fromDate.ToShortDateString() + " TO:" + _toDate.ToShortDateString();
                    pReportName.CurrentValues.Add(dcpReportName);
                    crystalReportViewer1.SelectionFormula = ("{INV_MAST.COMPCODE} = '" + _compCode + "' and {INV_MAST.INV_DATE} >= Date (" + _fromDate.Year.ToString() + "," + _fromDate.Month.ToString() + ", " + _fromDate.Day.ToString() + ") and {INV_MAST.INV_DATE} <= Date (" + _toDate.Year.ToString() + "," + _toDate.Month.ToString() + ", " + _toDate.Day.ToString() + ") ");
                    //{INV_MAST.INV_DATE} >= Date (yyyy, MM, DD)
                }
                else
                {
                    crystalReportViewer1.SelectionFormula = ("{INV_MAST.COMPCODE}='" + _compCode + "' and {INV_MAST.INV_NO} = '" + _documentNumber + "' and {INV_MAST.LOCA}='" + _location + "' and {INV_DETAIL.CSH_CRD}= '" + _csh_crd + "'");
                }
                                              
                pCompanyName.CurrentValues.Add(dcpCompanyName);
                paramFields.Add(pCompanyName);

                pReportName.CurrentValues.Add(dcpReportName);
                paramFields.Add(pReportName);

                crystalReportViewer1.ParameterFieldInfo = paramFields;
                crystalReportViewer1.ReportSource = cryRpt;
                crystalReportViewer1.Refresh();
                //this.crystalReportViewer1.RefreshReport();               
  
            }
        }

        private void crystalReportViewer1_Load(object sender, EventArgs e)
        {

        }

      
        
    }
}
