using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace CompuLinERP.API.Controllers
{
    public class GRNMastTransactions
    {
        CompuLinEntityModelEntities entities;

        public string InsertGRNMastDetails(GRN_MAST details, List<GRN_DETAIL> itemDetails)
        {
            LOCA_MAST location = new LOCA_MAST();
            location.COMPCODE = details.COMPCODE;
            location.LOCA_CODE = details.LOCA;
            string documentNumber = null;
            //string txtCSH_CRD = details.CSH_CRD ;
            int docId = 0;
            
            using (var transaction = new TransactionScope())
            {
                try
                {
                    using (entities = new CompuLinEntityModelEntities())
                    {
                        var query = (from logInfo in entities.LOCA_MAST
                                     where logInfo.COMPCODE == location.COMPCODE &&
                                     logInfo.LOCA_CODE == location.LOCA_CODE
                                     select logInfo);

                        if (query.Any())
                        {
                            LOCA_MAST details2 = query.ToList().First();

                                 docId = int.Parse(details2.GRN_NO);
                                 docId++;
                                 documentNumber = docId.ToString();
                                 details2.GRN_NO = (documentNumber).ToString();

                                 entities.SaveChanges();
                       
                            
                            
                        }
                        //locationDetailsController.GetNextInvoiceNumber(location).PadLeft(4, '0');
                        details.GRN_NO  = "G2015" + documentNumber.PadLeft(4, '0');

                        // bool status = invoiceMastController.InsertNewDetails(details);
                        var query2 = (from info2 in entities.GRN_MAST
                                      select info2);

                        entities.GRN_MAST.Add(details);
                        entities.SaveChanges();

                        List<STOCK> listStock = new List<STOCK>();

                        foreach (GRN_DETAIL item in itemDetails)
                        {
                            item.GRN_NO = details.GRN_NO;

                            //status = invoiceDetailsController.InsertNewDetails(item);
                            var query3 = (from info3 in entities.GRN_DETAIL
                                          select info3);

                            entities.GRN_DETAIL.Add(item);
                            entities.SaveChanges();

                            STOCK stock = new STOCK();

                            stock.BULK_QTY = decimal.Parse(item.BULK_QTY.ToString()) ;
                            stock.BULK_UNIT = item.BULK_UNIT;
                            stock.LOOSE_QTY = decimal.Parse(item.LOOSE_QTY.ToString()) ;
                            stock.LOOSE_UNIT = item.LOOSE_UNIT;
                            stock.COMPCODE = item.COMPCODE;
                            stock.COST_PRICE = 0;
                            stock.CSH_CRD = "GRN";
                            stock.DOC_BAL_BULK_QTY = 0;
                            stock.DOC_BAL_LOOSE_QTY = 0;
                            stock.DOC_NO = item.GRN_NO;
                            stock.IS_MONTH_END = 0;
                            stock.ITEM = item.ITEM;
                            stock.LOCA = item.ITEM_LOCA;
                            stock.LOCA_TO = item.ITEM_LOCA;
                            stock.LOCA_TO_DESC = item.ITEM_LOCA;
                            stock.PURCHASE_PRICE = 0;
                            stock.REFNO = "";
                            stock.SEQ_NO = 0;
                            stock.STOCK_TYPE = "";
                            stock.TXN_DATE = DateTime.Now;
                            stock.TXN_TYPE = "GRN";
                            stock.UNIT = item.UNIT;
                            listStock.Add(stock);

                        }

                        //Insert to Stock Table -  Deduct Stock
                        //Deduct stocks from Item mast
                        int count = 1;
                        foreach (STOCK stock in listStock)
                        {

                            //Insert to STOCK Table ( Common function – ADD STOCKS )
                            STOCK stockdetails = new STOCK();
                            stockdetails.COMPCODE = stock.COMPCODE;
                            stockdetails.LOCA = stock.LOCA;
                            stockdetails.ITEM = stock.ITEM;
                            stockdetails.TXN_TYPE = stock.TXN_TYPE;
                            stockdetails.DOC_NO = stock.DOC_NO;
                            stockdetails.CSH_CRD = stock.CSH_CRD;
                            stockdetails.LOCA_TO = stock.LOCA_TO;
                            stockdetails.UNIT = stock.UNIT;
                            stockdetails.LOCA_TO_DESC = stock.LOCA_TO_DESC;
                            stockdetails.TXN_DATE = stock.TXN_DATE;
                            stockdetails.SEQ_NO = stock.SEQ_NO;
                            stockdetails.REFNO = stock.REFNO;
                            stockdetails.PURCHASE_PRICE = stock.PURCHASE_PRICE;
                            stockdetails.COST_PRICE = stock.COST_PRICE;
                            stockdetails.BULK_UNIT = stock.BULK_UNIT;
                            stockdetails.LOOSE_UNIT = stock.LOOSE_UNIT;
                            stockdetails.BULK_QTY = stock.BULK_QTY;
                            stockdetails.LOOSE_QTY = stock.LOOSE_QTY;
                            stockdetails.DOC_BAL_BULK_QTY = stock.DOC_BAL_BULK_QTY;
                            stockdetails.DOC_BAL_LOOSE_QTY = stock.DOC_BAL_LOOSE_QTY;
                            stockdetails.STOCK_TYPE = stock.STOCK_TYPE;
                            stockdetails.IS_MONTH_END = 0;

                            count++;

                            //stockDetailsController.InsertNewDetails(stockdetails);
                            var query4 = (from info4 in entities.STOCKs
                                          select info4);

                            entities.STOCKs.Add(stockdetails);
                            entities.SaveChanges();

                            //Update existing item BULK and LOOSE quantities in ITEM_MASTER table – In this screen need to add to existing QTYs. NOT overwrite.
                            ITEM_MAST details5 = new ITEM_MAST();
                            details5.ITEM = stock.ITEM;
                            details5.COMPCODE = stock.COMPCODE;
                            details5.LOCA_CODE = stock.LOCA;
                            //details5.UNIT_1_QTY = stock.BULK_QTY;//(Existing QTY) + stock.BULK_QTY ;
                            //details5.UNIT_2_QTY = stock.LOOSE_QTY;//(Existing QTY) + stock.LOOSE_QTY;

                            //details5 = itemDetailsController.DeductItemQty(details);
                            var query6 = (from logInfo6 in entities.ITEM_MAST
                                          where logInfo6.COMPCODE == details5.COMPCODE &&
                                          logInfo6.ITEM == details5.ITEM &&
                                          logInfo6.LOCA_CODE == details5.LOCA_CODE
                                          select logInfo6);

                            if (query.Any())
                            {
                                details5 = query6.ToList().First();
                                                               
                                //Stocks are already in negative figures. so.. adding the stocks will deduct.
                                details5.UNIT_1_QTY = details5.UNIT_1_QTY + (stock.BULK_QTY );
                                details5.UNIT_2_QTY = details5.UNIT_2_QTY + (stock.LOOSE_QTY );

                                entities.SaveChanges();
                            }

                        }

                        transaction.Complete();
                    }
                }
                catch (Exception ex)
                {
                    return "Error";
                }
            }

            return details.GRN_NO;
        }
    }
}

