using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace CompuLinERP.API.Controllers
{
    public class ItemDetailsTransactions
    {
        CompuLinEntityModelEntities entities;

        public bool UpdateItemDetails(ITEM_MAST searchDetails, ITEM_MAST details, List<ITEM_SUPP> itemsuppliers)
        {
            bool status = false;

            using (var transaction = new TransactionScope())
            {
                try
                {
                    using (entities = new CompuLinEntityModelEntities())
                    {
                        //bool status = itemDetailsController.UpdateDetails(searchDetails, details);
                        var query = (from logInfo in entities.ITEM_MAST
                                     where logInfo.COMPCODE == searchDetails.COMPCODE &&
                                     logInfo.ITEM == searchDetails.ITEM &&
                                     logInfo.LOCA_CODE == searchDetails.LOCA_CODE
                                     select logInfo);

                        if (query.Any())
                        {
                            entities.ITEM_MAST.Remove(query.First());
                            entities.SaveChanges();

                            details.CHANGED = 1;
                            details.LAST_CHANGE_DATE = DateTime.Now;

                            entities.ITEM_MAST.Add(details);
                            entities.SaveChanges();

                            //details.QIH = details.QIH;
                            //details.BF_BAL = details.BF_BAL;
                            //details.CF_BAL = details.CF_BAL;
                            //details.COST_PRICE = details.COST_PRICE;
                            //details.IS_ROUND = details.IS_ROUND;
                            //details.IS_CUTTED = details.IS_CUTTED;
                            //details.CUT_STAT = details.CUT_STAT;                    
                            //details.ACTUAL_COST = details.ACTUAL_COST;
                            //details.UNIT_1_QTY = details.UNIT_1_QTY;
                            //details.UNIT_2_QTY = details.UNIT_2_QTY;
                            //details.BACK_UNIT_1_QTY = details.BACK_UNIT_1_QTY;
                            //details.BACK_UNIT_2_QTY = details.BACK_UNIT_2_QTY; 
                            //details.UNIT_1_RESERVED_QTY = details.UNIT_1_RESERVED_QTY;
                            //details.UNIT_2_RESERVED_QTY = details.UNIT_2_RESERVED_QTY;  

                            entities.SaveChanges();
                        }

                        ITEM_SUPP searchItemDetails = new ITEM_SUPP();
                        searchItemDetails.COMPCODE = searchDetails.COMPCODE;
                        searchItemDetails.ITEMCODE = searchDetails.ITEM;
                        searchItemDetails.LOCA_CODE = searchDetails.LOCA_CODE;

                        //List<ITEM_SUPP> olditemsuppliers = itemSupplierController.GetAllSupplierDetailsByItemId(searchItemDetails);
                        List<ITEM_SUPP> details2 = new List<ITEM_SUPP>();

                        var query2 = (from info in entities.ITEM_SUPP
                                      where info.COMPCODE == searchItemDetails.COMPCODE &&
                                         info.LOCA_CODE == searchItemDetails.LOCA_CODE &&
                                         info.ITEMCODE == searchItemDetails.ITEMCODE
                                      select info);

                        if (query2.Any())
                            details2 = query2.ToList();

                        //itemSupplierController.DeleteAllSuppliersByItem(searchItemDetails);
                        var query3 = (from details3 in entities.ITEM_SUPP
                                      where details3.COMPCODE == searchItemDetails.COMPCODE &&
                                     details3.LOCA_CODE == searchItemDetails.LOCA_CODE &&
                                     details3.ITEMCODE == searchItemDetails.ITEMCODE
                                      select details3);

                        if (query3.Any())
                        {
                            entities.ITEM_SUPP.Remove(query3.First());
                            entities.SaveChanges();
                        }

                        //status = itemSupplierController.InsertNewSupplier(itemsuppliers);
                        foreach (var details4 in itemsuppliers)
                        {
                            var query4 = (from info in entities.ITEM_SUPP
                                          select info);

                            details4.Last_Change_Date = DateTime.Now;

                            entities.ITEM_SUPP.Add(details4);
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

            return status;
        }
        public bool DeleteItemMasterRecord(ITEM_MAST searchDetails, ITEM_SUPP suppDetails)
        {
            bool status = false;

            using (var transaction = new TransactionScope())
            {
                try
                {
                    using (entities = new CompuLinEntityModelEntities())
                    {

                        var queryStock = (from details in entities.STOCKs
                                          where details.COMPCODE == searchDetails.COMPCODE &&
                                              details.LOCA == searchDetails.LOCA_CODE &&
                                              details.ITEM == searchDetails.ITEM
                                          select details);
                        if (queryStock.Any())
                        {
                            //Stock table contacins data. therefore not allowing to delete.
                            status = false;
                        }
                        else
                        {
                            var query = (from details in entities.ITEM_SUPP
                                         where details.COMPCODE == suppDetails.COMPCODE &&
                                             details.LOCA_CODE == suppDetails.LOCA_CODE &&
                                             details.SUPP_CODE == suppDetails.SUPP_CODE &&
                                             details.ITEMCODE == suppDetails.ITEMCODE
                                         select details);

                            if (query.Any())
                            {
                                List<ITEM_SUPP> list = query.ToList();

                                foreach (var item in list)
                                {
                                    entities.ITEM_SUPP.Remove(item);
                                    entities.SaveChanges();
                                }
                            }

                            //bool status = itemDetailsController.DeleteDetails(searchDetails);
                            var query2 = (from details2 in entities.ITEM_MAST
                                          where details2.COMPCODE == searchDetails.COMPCODE &&
                                         details2.ITEM == searchDetails.ITEM &&
                                         details2.LOCA_CODE == searchDetails.LOCA_CODE
                                          select details2);

                            if (query2.Any())
                            {
                                entities.ITEM_MAST.Remove(query2.First());
                                entities.SaveChanges();
                            }
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

            return status;
        }
        public bool DeleteItemDetails(ITEM_MAST searchDetails, ITEM_SUPP suppDetails)
        {
            bool status = false;

            using (var transaction = new TransactionScope())
            {
                try
                {
                    using (entities = new CompuLinEntityModelEntities())
                    {

                        var queryStock = (from details in entities.STOCKs
                                          where details.COMPCODE == searchDetails.COMPCODE &&
                                              details.LOCA == searchDetails.LOCA_CODE &&
                                              details.ITEM == searchDetails.ITEM
                                          select details);
                        if (queryStock.Any())
                        {
                            //Stock table contacins data. therefore not allowing to delete.
                            status = false;
                        }
                        else
                        {

                            //itemSupplierController.DeleteSupplierByItemId(suppDetails);
                            var query = (from details in entities.ITEM_SUPP
                                         where details.COMPCODE == suppDetails.COMPCODE &&
                                             details.LOCA_CODE == suppDetails.LOCA_CODE &&
                                             details.SUPP_CODE == suppDetails.SUPP_CODE &&
                                             details.ITEMCODE == suppDetails.ITEMCODE
                                         select details);

                            if (query.Any())
                            {
                                List<ITEM_SUPP> list = query.ToList();

                                foreach (var item in list)
                                {
                                    entities.ITEM_SUPP.Remove(item);
                                    entities.SaveChanges();
                                }
                            }

                            //bool status = itemDetailsController.DeleteDetails(searchDetails);
                            var query2 = (from details2 in entities.ITEM_MAST
                                          where details2.COMPCODE == searchDetails.COMPCODE &&
                                         details2.ITEM == searchDetails.ITEM &&
                                         details2.LOCA_CODE == searchDetails.LOCA_CODE
                                          select details2);

                            if (query2.Any())
                            {
                                entities.ITEM_MAST.Remove(query2.First());
                                entities.SaveChanges();
                            }
                            status = true;
                        }

                    }
                    transaction.Complete();

                    
                }
                catch (Exception ex)
                {
                    status = false;
                }
            }

            return status;
        }

    }
}
