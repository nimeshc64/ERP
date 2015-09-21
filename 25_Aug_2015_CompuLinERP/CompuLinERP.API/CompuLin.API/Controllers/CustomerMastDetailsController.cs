using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Dynamic;

namespace CompuLinERP.API.Controllers
{
    public class CustomerMastDetailsController
    {
        CompuLinEntityModelEntities entities;

        public bool InsertNewDetails(CUST_MAST details)
        {
            using (entities = new CompuLinEntityModelEntities())
            {
                var query = (from info in entities.CUST_MAST                             
                             select info);               

                entities.CUST_MAST.Add(details);
                entities.SaveChanges();
            }

            return true;
        }

        public bool InsertCustomerLedgerRecord(CUSTOMER_LEDGER  details)
        {
            using (entities = new CompuLinEntityModelEntities())
            {
                var query = (from info in entities.CUSTOMER_LEDGER
                             select info);

                entities.CUSTOMER_LEDGER.Add(details);
                entities.SaveChanges();
            }

            return true;
        }

        public List<CUST_MAST> GetAllDetailsByFilterCriteria(string searchValue, string searchColumn)
        {
           
            List<CUST_MAST> details = new List<CUST_MAST>();

            //string whereClause = searchColumn + " like " + "'%" + searchValue + "%'";
            // string whereClause = searchColumn + " = " + "'" + searchValue + "'";

            using (entities = new CompuLinEntityModelEntities())
            {
                //var query = (entities.CUST_MAST
                //             .Where(whereClause));
                //details = query.ToList();

                var query = (from info in entities.CUST_MAST
                             where info.CUST_NAME == searchValue
                             select info);

                if (searchValue == null || searchValue == "")
                {
                    query = (from info in entities.CUST_MAST
                             select info).Take(10);
                }
                else if (searchValue == "*")
                {
                    query = (from info in entities.CUST_MAST
                             select info);
                }
                else
                {
                    if (searchColumn == "CUST_NAME")
                    {
                        query = (from info in entities.CUST_MAST
                                 where info.CUST_NAME.Contains(searchValue)
                                 select info);

                    }
                    else if (searchColumn == "CUST_CODE")
                    {
                        query = (from info in entities.CUST_MAST
                                 where info.CUST_CODE.Contains(searchValue)
                                 select info);
                    }

                }

                if (query.Any())
                    details = query.ToList();

            }

            return details;
        }


        public bool UpdateCustomerLedgerRecord(CUSTOMER_LEDGER searchdetails, CUSTOMER_LEDGER details)
        {
            using (entities = new CompuLinEntityModelEntities())
            {
                var query = (from logInfo in entities.CUSTOMER_LEDGER
                             where logInfo.COMPCODE == searchdetails.COMPCODE &&
                             logInfo.CUST_CODE == searchdetails.CUST_CODE &&
                             logInfo.TXN_TYPE == searchdetails.TXN_TYPE &&
                             logInfo.DOC_NO == searchdetails.DOC_NO 
                             //logInfo.CSH_CRD = searchdetails.CSH_CRD 
                             select logInfo);


                if (query.Any())
                {
                    //entities.CUSTOMER_LEDGER.Remove(query.First());
                    //entities.SaveChanges();

                    entities.CUSTOMER_LEDGER.Add(details);
                    entities.SaveChanges();
                }
            }

            return true;
        }

        public bool UpdateDetails(CUST_MAST searchdetails, CUST_MAST details)
        {
            using (entities = new CompuLinEntityModelEntities())
            {
                var query = (from logInfo in entities.CUST_MAST
                             where logInfo.COMPCODE == searchdetails.COMPCODE &&
                             logInfo.CUST_CODE == searchdetails.CUST_CODE 
                             select logInfo);

                if (query.Any())
                {
                    entities.CUST_MAST.Remove(query.First());
                    entities.SaveChanges();

                    entities.CUST_MAST.Add(details);
                    entities.SaveChanges();                  
                }
            }

            return true;
        }

        public bool DeleteDetails(CUST_MAST searchdetails)
        {
            using (entities = new CompuLinEntityModelEntities())
            {
                var query = (from details in entities.CUST_MAST
                             where details.COMPCODE == searchdetails.COMPCODE &&
                             details.CUST_CODE == searchdetails.CUST_CODE 
                             select details);

                if (query.Any() )
                {
                    entities.CUST_MAST.Remove(query.First());
                    entities.SaveChanges();
                }
                else
                    return false;
            }

            return true;
        }

        public List<CUST_MAST> GetAllDetailsByCompany(string companyid)
        {
            List<CUST_MAST> details = new List<CUST_MAST>();

            using (entities = new CompuLinEntityModelEntities())
            {
                var query = (from info in entities.CUST_MAST
                             where info.COMPCODE == companyid 
                                 select info);

                    if (query.Any())
                        details = query.ToList();  
            }

            return details;
        }

        public CUSTOMER_LEDGER GetCustomerOutstandingById(string companyid, string customerCode)
        {
            CUSTOMER_LEDGER details = new CUSTOMER_LEDGER();

            using (entities = new CompuLinEntityModelEntities())
            {
                var query = (from info in entities.CUSTOMER_LEDGER 
                             where info.COMPCODE == companyid &&
                             info.CUST_CODE == customerCode
                             select info);

                if (query.Any())
                    details = query.ToList().First();
            }

            return details;
        }

        public CUST_MAST GetAllDetailsById(string companyid, string customerCode)
        {
            CUST_MAST details = new CUST_MAST();

            using (entities = new CompuLinEntityModelEntities())
            {
                var query = (from info in entities.CUST_MAST
                             where info.COMPCODE == companyid &&
                             info.CUST_CODE == customerCode
                             select info);

                if (query.Any())
                    details = query.ToList().First();
            }

            return details;
        }
    }
}
