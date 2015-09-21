using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompuLinERP.API.Controllers
{
    public class ItemDetailsController
    {
        CompuLinEntityModelEntities entities;

        public bool InsertNewDetails(ITEM_MAST details)
        {
            using (entities = new CompuLinEntityModelEntities())
            {
                var query = (from info in entities.ITEM_MAST                             
                             select info);                

                entities.ITEM_MAST.Add(details);
                entities.SaveChanges();
            }

            return true;
        }

        public ITEM_MAST UpdateDetailsByColumn(ITEM_MAST searchdetails, int option)
        {
            ITEM_MAST details = null;

            using (entities = new CompuLinEntityModelEntities())
            {
                var query = (from logInfo in entities.ITEM_MAST
                             where logInfo.COMPCODE == searchdetails.COMPCODE &&
                             logInfo.ITEM == searchdetails.ITEM &&
                             logInfo.LOCA_CODE == searchdetails.LOCA_CODE
                             select logInfo);

                if (query.Any())
                {
                    if (option == 1)
                    {
                        details = query.ToList().First();
                        details.UNIT_1_QTY = searchdetails.UNIT_1_QTY;
                        details.UNIT_2_QTY = searchdetails.UNIT_2_QTY;

                        entities.SaveChanges();
                    }
                    else if (option == 2)
                    {
                        details = query.ToList().First();
                        details.SALES_PRICE_BULK = searchdetails.SALES_PRICE_BULK;
                        details.SALES_PRICE_LOOSE = searchdetails.SALES_PRICE_LOOSE;

                        entities.SaveChanges();
                    }
                    else if (option == 3)
                    {
                        details = query.ToList().First();
                        details.COST_PRICE_BULK = searchdetails.COST_PRICE_BULK;
                        details.COST_PRICE_LOOSE = searchdetails.COST_PRICE_LOOSE;

                        entities.SaveChanges();
                    }
                    else if (option == 4)
                    {
                        details = query.ToList().First();
                        
                        details.UNIT_1_QTY = details.UNIT_1_QTY - searchdetails.UNIT_1_QTY;
                        details.UNIT_2_QTY = searchdetails.UNIT_2_QTY - searchdetails.UNIT_2_QTY;

                        entities.SaveChanges();
                    }
                }
            }

            return details;
        }

        public ITEM_MAST DeductItemQty(ITEM_MAST searchdetails)
        {
            ITEM_MAST details = null;

            using (entities = new CompuLinEntityModelEntities())
            {
                var query = (from logInfo in entities.ITEM_MAST
                             where logInfo.COMPCODE == searchdetails.COMPCODE &&
                             logInfo.ITEM == searchdetails.ITEM &&
                             logInfo.LOCA_CODE == searchdetails.LOCA_CODE
                             select logInfo);

                if (query.Any())
                {
                        details = query.ToList().First();

                        details.UNIT_1_QTY = details.UNIT_1_QTY + searchdetails.UNIT_1_QTY;
                        details.UNIT_2_QTY = details.UNIT_2_QTY + searchdetails.UNIT_2_QTY;

                        entities.SaveChanges();                    
                }
            }

            return details;
        }


        public bool UpdateDetails(ITEM_MAST searchdetails, ITEM_MAST details)
        {
            using (entities = new CompuLinEntityModelEntities())
            {
                var query = (from logInfo in entities.ITEM_MAST
                             where logInfo.COMPCODE == searchdetails.COMPCODE &&
                             logInfo.ITEM == searchdetails.ITEM &&
                             logInfo.LOCA_CODE == searchdetails.LOCA_CODE 
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
            }

            return true;
        }

        public bool DeleteDetails(ITEM_MAST searchdetails)
        {
            using (entities = new CompuLinEntityModelEntities())
            {
                var query = (from details in entities.ITEM_MAST
                             where details.COMPCODE == searchdetails.COMPCODE &&
                             details.ITEM == searchdetails.ITEM &&
                             details.LOCA_CODE == searchdetails.LOCA_CODE 
                             select details);

                if (query.Any() )
                {
                    entities.ITEM_MAST.Remove(query.First());
                    entities.SaveChanges();
                }
                else
                    return false;
            }

            return true;
        }

        public List<ITEM_MAST> GetAllDetailsByCompany(string companyid)
        {
            List<ITEM_MAST> details = new List<ITEM_MAST>();

            using (entities = new CompuLinEntityModelEntities())
            {
                var query = (from info in entities.ITEM_MAST
                             where info.COMPCODE == companyid 
                                 select info);

                    if (query.Any())
                        details = query.ToList();  
            }

            return details;
        }

        public List<ITEM_MAST> GetAllDetailsByLocation(string locationId, string companyid)
        {
            List<ITEM_MAST> details = new List<ITEM_MAST>();

            using (entities = new CompuLinEntityModelEntities())
            {
                var query = (from info in entities.ITEM_MAST
                             where info.COMPCODE == companyid && info.LOCA_CODE == locationId
                             select info);

                if (query.Any())
                    details = query.ToList();
            }

            return details;
        }

        public List<ITEM_MAST> GetAllDetailsByFilterCriteria(string tableName, string searchValue, string locationId, string companyid, string searchColumn)
        {
            List<ITEM_MAST> details = new List<ITEM_MAST>();

            using (entities = new CompuLinEntityModelEntities())
            {
                if (searchValue ==  "")
                {
                    var query = (from info in entities.ITEM_MAST
                                 where info.COMPCODE == companyid &&
                                 info.LOCA_CODE == locationId 
                                 select info).Take(10);
                    details = query.ToList();
                }
                else if (searchValue == "*")
                {
                    var query = (from info in entities.ITEM_MAST
                                 where info.COMPCODE == companyid &&
                                 info.LOCA_CODE == locationId
                                 select info);
                    details = query.ToList();
                }
                else 
                {
                    if (searchColumn == "ITEM")
                    {
                        var query = (from info in entities.ITEM_MAST
                                     where info.COMPCODE == companyid &&
                                     info.LOCA_CODE == locationId &&
                                     info.ITEM.Contains(searchValue)
                                     select info);
                        details = query.ToList();
                    }
                    else if (searchColumn == "DESC1")
                    {
                        var query = (from info in entities.ITEM_MAST
                                     where info.COMPCODE == companyid &&
                                     info.LOCA_CODE == locationId &&
                                     info.DESC1.Contains(searchValue)
                                     select info);
                        details = query.ToList();
                    }
                }
            }

            return details;
        }

        public List<ITEM_MAST> GetAllDetailsByItemOrder(ITEM_MAST searchdetails, string fromvalue, string toValue)
        {
            List<ITEM_MAST> details = new List<ITEM_MAST>();

            using (entities = new CompuLinEntityModelEntities())
            {
                var query = (from info in entities.ITEM_MAST
                             where info.COMPCODE == searchdetails.COMPCODE &&
                             info.LOCA_CODE == searchdetails.LOCA_CODE
                             orderby info.ITEM ascending
                             select info);
                List<ITEM_MAST> tempList = new List<ITEM_MAST>();
                tempList = query.ToList();
                
                int count=0;
                int fromCount = -1;               
                foreach (ITEM_MAST item in tempList)
                {
                    if (item.ITEM == fromvalue)
                    {
                        fromCount = count;                        
                    }
                    if (fromCount > -1)
                    {
                        details.Add(item);
                    }

                    if (item.ITEM == toValue)
                    {                        
                        break;
                    }
                    count++;
                }
            }


            return details;
        }

        public ITEM_MAST GetAllDetailsById(ITEM_MAST searchdetails)
        {
            ITEM_MAST details = new ITEM_MAST();

            using (entities = new CompuLinEntityModelEntities())
            {
                var query = (from info in entities.ITEM_MAST
                             where info.COMPCODE == searchdetails.COMPCODE &&
                             info.LOCA_CODE == searchdetails.LOCA_CODE &&
                             info.ITEM == searchdetails.ITEM 
                             select info);

                if (query.Any())
                    details = query.ToList().First();
            }

            return details;
        }

        public List<ITEM_MAST> GetAllItemDetailsForAllLocations(ITEM_MAST searchdetails)
        {
            List<ITEM_MAST> details = new List<ITEM_MAST>();

            using (entities = new CompuLinEntityModelEntities())
            {
               var query = (from info in entities.ITEM_MAST
                             where info.COMPCODE == searchdetails.COMPCODE &&
                             info.ITEM == searchdetails.ITEM
                             select info);

                if (query.Any())
                    details = query.ToList();
            }

            return details;
        }
              

    }
}
