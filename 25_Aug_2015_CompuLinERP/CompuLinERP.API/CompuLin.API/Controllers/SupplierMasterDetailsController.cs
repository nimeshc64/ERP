using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompuLinERP.API.Controllers
{
    public class SupplierMasterDetailsController
    {
        CompuLinEntityModelEntities entities;

        public bool InsertNewDetails(SUPP_MAST details)
        {
            using (entities = new CompuLinEntityModelEntities())
            {
                var query = (from info in entities.SUPP_MAST                             
                             select info);                

                entities.SUPP_MAST.Add(details);
                entities.SaveChanges();
            }

            return true;
        }

        public bool UpdateDetails(SUPP_MAST searchdetails, SUPP_MAST details)
        {
            using (entities = new CompuLinEntityModelEntities())
            {
                var query = (from logInfo in entities.SUPP_MAST
                             where logInfo.COMCODE == searchdetails.COMCODE &&
                             logInfo.SUPP_CODE == searchdetails.SUPP_CODE                              
                             select logInfo);

                if (query.Any())
                {
                    entities.SUPP_MAST.Remove(query.First());
                    entities.SaveChanges();

                    entities.SUPP_MAST.Add(details);   
                    entities.SaveChanges();
                }
            }

            return true;
        }

        public bool DeleteDetails(string compCode, string suppCode)
        {
            using (entities = new CompuLinEntityModelEntities())
            {
                var query = (from details in entities.SUPP_MAST
                             where details.COMCODE == compCode &&
                             details.SUPP_CODE == suppCode  
                             select details);

                if (query.Any() )
                {
                    entities.SUPP_MAST.Remove(query.First());
                    entities.SaveChanges();
                }
                else
                    return false;
            }

            return true;
        }

        public List<SUPP_MAST> GetAllDetailsByCompany(string  tableName, string searchText, string locationId, string companyId, string searchColumn)
        {
            List<SUPP_MAST> details = new List<SUPP_MAST>();

            using (entities = new CompuLinEntityModelEntities())
            {
                var query = (from info in entities.SUPP_MAST
                             where info.COMCODE == companyId 
                                 select info);

                    if (query.Any())
                        details = query.ToList();  
            }

            return details;
        }

        


        public SUPP_MAST GetAllDetailsById(string code, string companyid)
        {
            SUPP_MAST details = new SUPP_MAST();

            using (entities = new CompuLinEntityModelEntities())
            {
                var query = (from info in entities.SUPP_MAST
                             where info.COMCODE == companyid &&
                             info.SUPP_CODE == code 
                             select info);

                if (query.Any())
                    details = query.ToList().First();
            }

            return details;
        }

    }
}
