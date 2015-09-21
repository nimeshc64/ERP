using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompuLinERP.API.Controllers
{
    public class UnitDetailsController
    {
        CompuLinEntityModelEntities entities;

        public bool InsertNewDetails(UNIT_MAST details)
        {
            using (entities = new CompuLinEntityModelEntities())
            {
                var query = (from info in entities.UNIT_MAST                            
                             select info);              

                entities.UNIT_MAST.Add(details);
                entities.SaveChanges();
            }

            return true;
        }

        public bool UpdateDetails(UNIT_MAST searchdetails, UNIT_MAST details)
        {
            using (entities = new CompuLinEntityModelEntities())
            {
                var query = (from logInfo in entities.UNIT_MAST
                             where logInfo.COMPCODE == searchdetails.COMPCODE &&
                             logInfo.UNIT_CODE == searchdetails.UNIT_CODE
                             select logInfo);

                if (query.Any())
                {
                    UNIT_MAST catDetails = query.First();

                    entities.UNIT_MAST.Remove(query.First());
                    entities.SaveChanges();

                    entities.UNIT_MAST.Add(details);
                    entities.SaveChanges();
                }
            }

            return true;
        }

        public bool DeleteDetails(string compcode, string usercode )
        {
            using (entities = new CompuLinEntityModelEntities())
            {
                var query = (from details in entities.UNIT_MAST
                             where details.COMPCODE == compcode &&
                             details.UNIT_CODE == usercode
                             select details);

                if (query.Any() )
                {
                    entities.UNIT_MAST.Remove(query.First());
                    entities.SaveChanges();
                }
                else
                    return false;
            }

            return true;
        }

        public List<UNIT_MAST> GetAllUnitDetailsByCompany(string networkId)
        {
            List<UNIT_MAST> details = new List<UNIT_MAST>();

            using (entities = new CompuLinEntityModelEntities())
            {
                var query = (from info in entities.UNIT_MAST
                             where info.COMPCODE == networkId
                                 select info);

                    if (query.Any())
                        details = query.ToList();  
            }

            return details;
        }

        public UNIT_MAST GetAllDetailsById(string compcode, string unitCode)
        {
            UNIT_MAST details = new UNIT_MAST();

            using (entities = new CompuLinEntityModelEntities())
            {
                var query = (from info in entities.UNIT_MAST
                             where info.COMPCODE == compcode &&
                             info.UNIT_CODE == unitCode
                             select info);

                if (query.Any())
                    details = query.ToList().First();
            }

            return details;
        }

    }
}
