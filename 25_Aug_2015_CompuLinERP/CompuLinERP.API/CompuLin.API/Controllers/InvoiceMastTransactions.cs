using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace CompuLinERP.API.Controllers
{
    public class InvoiceMastTransactions
    {
        CompuLinEntityModelEntities entities;

        public string InsertInvoiceMastDetails(INV_MAST details, List<INV_DETAIL> itemDetails)
        {
            LOCA_MAST location = new LOCA_MAST();
            location.COMPCODE = details.COMPCODE;
            location.LOCA_CODE = details.LOCA;
            string documentNumber = null;
            string txtCSH_CRD = details.CSH_CRD ;
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
                            if (txtCSH_CRD == "CRD" || txtCSH_CRD == "CRV")
                            {
                                 docId = int.Parse(details2.CREDIT_INV_NO);
                                 docId++;
                                 documentNumber = docId.ToString();
                                 details2.CREDIT_INV_NO = (documentNumber).ToString();

                                 entities.SaveChanges();
                            }
                            else if (txtCSH_CRD == "CSH" || txtCSH_CRD == "CSV")
                            {
                                 docId = int.Parse(details2.CASH_INV_NO);
                                 docId++;
                                 documentNumber = docId.ToString();
                                 details2.CASH_INV_NO = (documentNumber).ToString();

                                 entities.SaveChanges();
                            }                          
                            
                            
                        }
                        //locationDetailsController.GetNextInvoiceNumber(location).PadLeft(4, '0');
                        details.INV_NO = "I2015" + documentNumber.PadLeft(4, '0');
                        details.CANCELLED = 0;

                        // bool status = invoiceMastController.InsertNewDetails(details);
                        var query2 = (from info2 in entities.INV_MAST
                                      select info2);

                        entities.INV_MAST.Add(details);
                        entities.SaveChanges();

                        List<STOCK> listStock = new List<STOCK>();

                        foreach (INV_DETAIL item in itemDetails)
                        {
                            item.INV_NO = details.INV_NO;

                            //status = invoiceDetailsController.InsertNewDetails(item);
                            var query3 = (from info3 in entities.INV_DETAIL
                                          select info3);

                            entities.INV_DETAIL.Add(item);
                            entities.SaveChanges();

                            STOCK stock = new STOCK();

                            stock.BULK_QTY = decimal.Parse(item.BULK_QTY.ToString()) * -1;
                            stock.BULK_UNIT = item.BULK_UNIT;
                            stock.LOOSE_QTY = decimal.Parse(item.LOOSE_QTY.ToString()) * -1;
                            stock.LOOSE_UNIT = item.LOOSE_UNIT;
                            stock.COMPCODE = item.COMPCODE;
                            stock.COST_PRICE = 0;
                            stock.CSH_CRD = item.CSH_CRD;
                            stock.DOC_BAL_BULK_QTY = 0;
                            stock.DOC_BAL_LOOSE_QTY = 0;
                            stock.DOC_NO = item.INV_NO;
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
                            stock.TXN_TYPE = "INV";
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

            return details.INV_NO;
        }

        public string AddCutNote(ITEM_MAST cutItem)
        {
            //0. Generate new Cutnote number
            //1. Add to cutnote table ( cancel =0)
            //2. Deduct item BULK_QTY by 1
            //3. Add item LOOSE_QTY by Pack Size
            //4. Add entry to Stock Table as BULK Deduction
            //5. Add entry to Stock Table as LOOSE Add

            string cutNoteNo = "";

            using (var transaction = new TransactionScope())
            {
                try
                {
                    using (entities = new CompuLinEntityModelEntities())
                    {
                        //List<STOCK> listStock = new List<STOCK>();
                        //0.Generate cutnote number
                        int docId = 0;
                        string documentNumber = "";

                         var query = (from logInfo in entities.LOCA_MAST
                                      where logInfo.COMPCODE == cutItem.COMPCODE &&
                                     logInfo.LOCA_CODE == cutItem.LOCA_CODE
                                     select logInfo);

                         if (query.Any())
                         {
                                 LOCA_MAST details2 = query.ToList().First();
                             
                                 docId = int.Parse(details2.CUTNO);
                                 docId++;
                                 documentNumber = docId.ToString();
                                 details2.CUTNO = (documentNumber).ToString();
                                 entities.SaveChanges();                             
                         }

                        cutNoteNo = "Y2015" + documentNumber.PadLeft(4, '0');

                        //Item Mast - Update Stock
                        var existingItemData = (from itemDetails in entities.ITEM_MAST
                                                where itemDetails.COMPCODE == cutItem.COMPCODE &&
                                      itemDetails.ITEM == cutItem.ITEM &&
                                      itemDetails.LOCA_CODE == cutItem.LOCA_CODE                        
                                                select itemDetails);
                        if (existingItemData.Any())
                        {
                            ITEM_MAST item = existingItemData.ToList().First();
                            item.UNIT_1_QTY = item.UNIT_1_QTY - 1;
                            item.UNIT_2_QTY = item.UNIT_2_QTY + item.PACK;
                            entities.SaveChanges();
                        

                        //Add entry to stock table - deduct 
                        STOCK stock = new STOCK();
                        stock.BULK_QTY =  -1;
                        stock.BULK_UNIT = item.UNIT;
                        stock.LOOSE_QTY = 0;
                        stock.LOOSE_UNIT = item.ITEM_UNIT0;
                        stock.COMPCODE = item.COMPCODE;
                        stock.COST_PRICE = 0;
                        stock.CSH_CRD = "CUT";
                        stock.DOC_BAL_BULK_QTY = 0;
                        stock.DOC_BAL_LOOSE_QTY = 0;
                        stock.DOC_NO = cutNoteNo;
                        stock.IS_MONTH_END = 0;
                        stock.ITEM = item.ITEM;
                        stock.LOCA = item.LOCA_CODE;
                        stock.LOCA_TO = item.LOCA_CODE;
                        stock.LOCA_TO_DESC = item.LOCA_CODE;
                        stock.PURCHASE_PRICE = 0;
                        stock.REFNO = "";
                        stock.SEQ_NO = 0;
                        stock.STOCK_TYPE = "";
                        stock.TXN_DATE = DateTime.Now;
                        stock.TXN_TYPE = "CUT";
                        stock.UNIT = item.UNIT;
                        entities.STOCKs.Add(stock);

                        //Add entry to stock table - Add loose
                        STOCK stock2 = new STOCK();
                        stock2.BULK_QTY = 0;
                        stock2.BULK_UNIT = item.UNIT;
                        stock2.LOOSE_QTY = item.PACK;
                        stock2.LOOSE_UNIT = item.ITEM_UNIT0;
                        stock2.COMPCODE = item.COMPCODE;
                        stock2.COST_PRICE = 0;
                        stock2.CSH_CRD = "CUT";
                        stock2.DOC_BAL_BULK_QTY = 0;
                        stock2.DOC_BAL_LOOSE_QTY = 0;
                        stock2.DOC_NO = cutNoteNo;
                        stock2.IS_MONTH_END = 0;
                        stock2.ITEM = item.ITEM;
                        stock2.LOCA = item.LOCA_CODE;
                        stock2.LOCA_TO = item.LOCA_CODE;
                        stock2.LOCA_TO_DESC = item.LOCA_CODE;
                        stock2.PURCHASE_PRICE = 0;
                        stock2.REFNO = "";
                        stock2.SEQ_NO = 0;
                        stock2.STOCK_TYPE = "";
                        stock2.TXN_DATE = DateTime.Now;
                        stock2.TXN_TYPE = "CUT";
                        stock2.UNIT = item.ITEM_UNIT0;
                        //listStock.Add(stock2);

                        entities.STOCKs.Add(stock2);
                        entities.SaveChanges();

                        transaction.Complete();
                        }
                    }
                }
                catch (Exception ex)
                {
                    return "Error";
                }
            }

            return "";
        }

        public string ReverseCutNote(ITEM_MAST details)
        {
            //1. Update cutnote table as cancel, canceldate , cancel user ( cancel =1)
            //2. Add item BULK_QTY by 1
            //3. deduct item LOOSE_QTY by Pack Size
            //4. Add entry to Stock Table as Loose Deduction
            //5. Add entry to Stock Table as BULK add

            using (var transaction = new TransactionScope())
            {
                try
                {
                    using (entities = new CompuLinEntityModelEntities())
                    {

                        transaction.Complete();
                    }
                }
                catch (Exception ex)
                {
                    return "Error";
                }
            }

            return "";
        }

        public string CancelInvoice(INV_MAST details)
        {

            using (var transaction = new TransactionScope())
            {
                try
                {
                    using (entities = new CompuLinEntityModelEntities())
                    {
                        var query = (from invMast in entities.INV_MAST
                                     where invMast.COMPCODE == details.COMPCODE &&
                                     invMast.LOCA  == details.LOCA && invMast.CSH_CRD == details.CSH_CRD &&
                                     invMast.INV_NO == details.INV_NO
                                     select invMast);

                        if (query.Any())
                        {
                            INV_MAST  details2 = query.ToList().First();
                            details2.CANCELLED  = 1;
                            details2.CANCEL_USERCODE = details.USERCODE;
                            details2.CANCEL_DATE = DateTime.Now;                            
                            entities.SaveChanges(); 
                        }

                        
                        var queryDetails = (from invDetail in entities.INV_DETAIL
                                            where invDetail.COMPCODE == details.COMPCODE &&
                                     invDetail.LOCA == details.LOCA && invDetail.CSH_CRD == details.CSH_CRD &&
                                     invDetail.INV_NO == details.INV_NO
                                            select invDetail);

                        List<INV_DETAIL> invDetails = new List<INV_DETAIL>();
                        invDetails = queryDetails.ToList();

                        foreach (INV_DETAIL inv in invDetails)
                        {
                            
                            STOCK stock = new STOCK();

                            stock.BULK_QTY = decimal.Parse(inv.BULK_QTY.ToString());
                            stock.BULK_UNIT = inv.BULK_UNIT;
                            stock.LOOSE_QTY = decimal.Parse(inv.LOOSE_QTY.ToString()) ;
                            stock.LOOSE_UNIT = inv.LOOSE_UNIT;
                            stock.COMPCODE = inv.COMPCODE;
                            stock.COST_PRICE = 0;
                            stock.CSH_CRD = inv.CSH_CRD;
                            stock.DOC_BAL_BULK_QTY = 0;
                            stock.DOC_BAL_LOOSE_QTY = 0;
                            stock.DOC_NO = inv.INV_NO;
                            stock.IS_MONTH_END = 0;
                            stock.ITEM = inv.ITEM;
                            stock.LOCA = inv.ITEM_LOCA;
                            stock.LOCA_TO = inv.ITEM_LOCA;
                            stock.LOCA_TO_DESC = inv.ITEM_LOCA;
                            stock.PURCHASE_PRICE = 0;
                            stock.REFNO = "";
                            stock.SEQ_NO = 0;
                            stock.STOCK_TYPE = "";
                            stock.TXN_DATE = DateTime.Now;
                            stock.TXN_TYPE = "CAN";
                            stock.UNIT = inv.UNIT;
                        
                            var query4 = (from info4 in entities.STOCKs
                                          select info4);

                            entities.STOCKs.Add(stock);
                            entities.SaveChanges();


                            ITEM_MAST details5 = new ITEM_MAST();

                            //details5 = itemDetailsController.DeductItemQty(details);
                            var query6 = (from logInfo6 in entities.ITEM_MAST
                                          where logInfo6.COMPCODE == stock.COMPCODE &&
                                          logInfo6.ITEM == stock.ITEM &&
                                          logInfo6.LOCA_CODE == stock.LOCA
                                          select logInfo6);

                            if (query.Any())
                            {
                                details5 = query6.ToList().First();

                                //Stocks are already in negative figures. so.. adding the stocks will deduct.
                                details5.UNIT_1_QTY = details5.UNIT_1_QTY + (stock.BULK_QTY);
                                details5.UNIT_2_QTY = details5.UNIT_2_QTY + (stock.LOOSE_QTY);

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


            return "";
        }

    }
}
