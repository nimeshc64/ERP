using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace CompuLinERP.API.Controllers
{
    public class PhysicalStockDetailsTransactions
    {
        CompuLinEntityModelEntities entities;

        public string StockAdjustmentsInsertNewDetails(List<PHY_MAST> PHY_MAST_List, string docNumber)
        {
            bool status = false;
            string docNo = null;
            int count = 1;

            using (var transaction = new TransactionScope())
            {
                try
                {
                    using (entities = new CompuLinEntityModelEntities())
                    {
                        LOCA_MAST location = new LOCA_MAST();
                        location.COMPCODE = PHY_MAST_List[0].COMPCODE;
                        location.LOCA_CODE = PHY_MAST_List[0].LOCA;


                        var query = (from logInfo in entities.LOCA_MAST
                                     where logInfo.COMPCODE == location.COMPCODE &&
                                     logInfo.LOCA_CODE == location.LOCA_CODE
                                     select logInfo);

                        if (query.Any())
                        {
                            LOCA_MAST details = query.ToList().First();
                            int docId = int.Parse(details.PHY_NO);
                            docId++;
                            string documentNumber = docId.ToString();
                            details.ADJ_NO = (documentNumber).ToString();

                            entities.SaveChanges();

                            docNo = "ADJ0" + documentNumber.PadLeft(4, '0');
                        }
                    }

                    foreach (PHY_MAST item in PHY_MAST_List)
                    {


                        //3.	Increase document number by one in LOCA_MAST table.

                        using (entities = new CompuLinEntityModelEntities())
                        {

                            //Add to STOCK_HISTORY table

                            //Delete from STOCK table

                            //Add to PHY_MAST table.
                            //StockAdjustmentsDetailsController.InsertNewDetails(item, docNo);
                            //var query2 = (from info2 in entities.PHY_MAST
                            //              select info2);
                            //item.PHY_NO = docNo;
                            //entities.PHY_MAST.Add(item);
                            //entities.SaveChanges();

                            //2.	Update existing item BULK and LOOSE quantities in ITEM_MASTER table – In this screen need to overwrite the QTYs
                            ITEM_MAST details3 = new ITEM_MAST();
                            details3.ITEM = item.ITEM;
                            details3.COMPCODE = item.COMPCODE;
                            details3.LOCA_CODE = item.LOCA;
                            details3.UNIT_1_QTY = item.BULK_QTY;
                            details3.UNIT_2_QTY = item.LOOSE_QTY;

                            string compcode = details3.COMPCODE;
                            string ITEM = item.ITEM;
                            string LOCA_CODE = item.LOCA;
                            //itemDetailsController.UpdateDetailsByColumn(details, 1);                            
                            var query3 = (from logInfo3 in entities.ITEM_MAST
                                          where logInfo3.COMPCODE == compcode &&
                                     logInfo3.ITEM == ITEM &&
                                     logInfo3.LOCA_CODE == LOCA_CODE
                                          select logInfo3);

                            ITEM_MAST details4 = query3.ToList().First();
                            details4.UNIT_1_QTY = details4.UNIT_1_QTY + details3.UNIT_1_QTY;
                            details4.UNIT_2_QTY = details4.UNIT_2_QTY + details3.UNIT_2_QTY;

                            entities.SaveChanges();

                            //4.	Insert to STOCK Table ( Common function – ADD STOCKS )
                            STOCK stockdetails = new STOCK();
                            stockdetails.COMPCODE = item.COMPCODE;
                            stockdetails.LOCA = item.LOCA;
                            stockdetails.ITEM = item.ITEM;
                            stockdetails.TXN_TYPE = "ADJ";
                            stockdetails.DOC_NO = docNo;
                            stockdetails.CSH_CRD = "ADJ";
                            stockdetails.LOCA_TO = item.LOCA;
                            stockdetails.UNIT = details4.UNIT;
                            stockdetails.LOCA_TO_DESC = item.LOCA;
                            stockdetails.TXN_DATE = DateTime.Now;
                            stockdetails.SEQ_NO = count;
                            stockdetails.REFNO = "";
                            stockdetails.PURCHASE_PRICE = 0;
                            stockdetails.COST_PRICE = 0;
                            stockdetails.BULK_UNIT = details4.UNIT;
                            stockdetails.LOOSE_UNIT = details4.ITEM_UNIT0;
                            stockdetails.BULK_QTY = item.BULK_QTY;
                            stockdetails.LOOSE_QTY = item.LOOSE_QTY;
                            stockdetails.DOC_BAL_BULK_QTY = item.BULK_QTY;
                            stockdetails.DOC_BAL_LOOSE_QTY = item.LOOSE_QTY;
                            stockdetails.STOCK_TYPE = "PHY";
                            stockdetails.IS_MONTH_END = 0;

                            count++;

                            //stockDetailsController.InsertNewDetails(stockdetails);
                            var query4 = (from info4 in entities.STOCKs
                                          select info4);

                            entities.STOCKs.Add(stockdetails);
                            entities.SaveChanges();
                        }
                    }

                    transaction.Complete();

                    status = true;
                }
                catch (Exception ex)
                {
                    status = false;
                }
            }

            return docNo;
        }

        public string PhysicalStockInsertNewDetails(List<PHY_MAST> PHY_MAST_List, string docNumber)
        {
            bool status = false;
            string docNo = null;
            int count = 1;

            using (var transaction = new TransactionScope())
            {
                try
                {
                    using (entities = new CompuLinEntityModelEntities())
                    {
                        LOCA_MAST location = new LOCA_MAST();
                        location.COMPCODE = PHY_MAST_List[0].COMPCODE;
                        location.LOCA_CODE = PHY_MAST_List[0].LOCA;


                        var query = (from logInfo in entities.LOCA_MAST
                                     where logInfo.COMPCODE == location.COMPCODE &&
                                     logInfo.LOCA_CODE == location.LOCA_CODE
                                     select logInfo);

                        if (query.Any())
                        {
                            LOCA_MAST details = query.ToList().First();
                            int docId = int.Parse(details.PHY_NO);
                            docId++;
                            string documentNumber = docId.ToString();
                            details.PHY_NO = (documentNumber).ToString();

                            entities.SaveChanges();

                            docNo = "P2014" + documentNumber.PadLeft(4, '0');
                        }
                    }

                    foreach (PHY_MAST item in PHY_MAST_List)
                    {


                        //3.	Increase document number by one in LOCA_MAST table.

                        using (entities = new CompuLinEntityModelEntities())
                        {

                            //Add to STOCK_HISTORY table

                            //Delete from STOCK table

                            //Add to PHY_MAST table.
                            //StockAdjustmentsDetailsController.InsertNewDetails(item, docNo);
                            var query2 = (from info2 in entities.PHY_MAST
                                          select info2);
                            item.PHY_NO = docNo;
                            entities.PHY_MAST.Add(item);
                            entities.SaveChanges();

                            //2.	Update existing item BULK and LOOSE quantities in ITEM_MASTER table – In this screen need to overwrite the QTYs
                            ITEM_MAST details3 = new ITEM_MAST();
                            details3.ITEM = item.ITEM;
                            details3.COMPCODE = item.COMPCODE;
                            details3.LOCA_CODE = item.LOCA;
                            details3.UNIT_1_QTY = item.BULK_QTY;
                            details3.UNIT_2_QTY = item.LOOSE_QTY;

                            string compcode = details3.COMPCODE;
                            string ITEM = item.ITEM;
                            string LOCA_CODE = item.LOCA;
                            //itemDetailsController.UpdateDetailsByColumn(details, 1);                            
                            var query3 = (from logInfo3 in entities.ITEM_MAST
                                          where logInfo3.COMPCODE == compcode &&
                                     logInfo3.ITEM == ITEM &&
                                     logInfo3.LOCA_CODE == LOCA_CODE
                                          select logInfo3);

                            ITEM_MAST details4 = query3.ToList().First();
                            details4.UNIT_1_QTY = details3.UNIT_1_QTY;
                            details4.UNIT_2_QTY = details3.UNIT_2_QTY;

                            entities.SaveChanges();

                            //4.	Insert to STOCK Table ( Common function – ADD STOCKS )
                            STOCK stockdetails = new STOCK();
                            stockdetails.COMPCODE = item.COMPCODE;
                            stockdetails.LOCA = item.LOCA;
                            stockdetails.ITEM = item.ITEM;
                            stockdetails.TXN_TYPE = "PHY";
                            stockdetails.DOC_NO = docNo;
                            stockdetails.CSH_CRD = "PHY";
                            stockdetails.LOCA_TO = item.LOCA;
                            stockdetails.UNIT = details4.UNIT;
                            stockdetails.LOCA_TO_DESC = item.LOCA;
                            stockdetails.TXN_DATE = DateTime.Now;
                            stockdetails.SEQ_NO = count;
                            stockdetails.REFNO = "";
                            stockdetails.PURCHASE_PRICE = 0;
                            stockdetails.COST_PRICE = 0;
                            stockdetails.BULK_UNIT = details4.UNIT;
                            stockdetails.LOOSE_UNIT = details4.ITEM_UNIT0;
                            stockdetails.BULK_QTY = item.BULK_QTY;
                            stockdetails.LOOSE_QTY = item.LOOSE_QTY;
                            stockdetails.DOC_BAL_BULK_QTY = item.BULK_QTY;
                            stockdetails.DOC_BAL_LOOSE_QTY = item.LOOSE_QTY;
                            stockdetails.STOCK_TYPE = "PHY";
                            stockdetails.IS_MONTH_END = 0;

                            count++;

                            //stockDetailsController.InsertNewDetails(stockdetails);
                            var query4 = (from info4 in entities.STOCKs
                                          select info4);

                            entities.STOCKs.Add(stockdetails);
                            entities.SaveChanges();
                        }
                    }

                    transaction.Complete();

                    status = true;
                }
                catch (Exception ex)
                {
                    status = false;
                }
            }

            return docNo;
        }
    }
        
       
}
