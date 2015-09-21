using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompuLinERP.API.Controllers
{
    public class CategoryL3Controller
    {
        CompuLinEntityModelEntities entities;

        public bool InsertNewCategory(CAT_L3 details)
        {
            using (entities = new CompuLinEntityModelEntities())
            {
                var query = (from info in entities.CAT_L3                             
                             select info);

                details.CHANGED = 0;
                details.CHANGEDDATE = DateTime.Now;

                entities.CAT_L3.Add(details);
                entities.SaveChanges();
            }

            return true;
        }

        public bool UpdateDetails(CAT_L3 searchDetails, CAT_L3 details)
        {
            using (entities = new CompuLinEntityModelEntities())
            {
                var query = (from logInfo in entities.CAT_L3
                             where logInfo.CATCODE == searchDetails.CATCODE &&
                             logInfo.CATCODE_L2 == searchDetails.CATCODE_L2 &&
                             logInfo.COMPCODE == searchDetails.COMPCODE &&
                             logInfo.CATCODE_L3 == searchDetails.CATCODE_L3
                             select logInfo);

                if (query.Any())
                {
                    entities.CAT_L3.Remove(query.First());
                    entities.SaveChanges();

                    details.CHANGED = 1;
                    details.CHANGEDDATE = DateTime.Now;
                    details.REMOVE = 0;

                    entities.CAT_L3.Add(details);
                    entities.SaveChanges();                       
                    
                }
            }

            return true;
        }

        public bool DeleteCategory(CAT_L3 searchDetails)
        {
            using (entities = new CompuLinEntityModelEntities())
            {
                var query = (from details in entities.CAT_L4
                             where details.CATCODE_L2 == searchDetails.CATCODE_L2 &&
                             details.COMPCODE == searchDetails.COMPCODE &&
                             details.CATCODE == searchDetails.CATCODE &&
                             details.CATCODE_L3 == searchDetails.CATCODE_L3 &&
                             details.REMOVE == 0
                             select details);

                if (!query.Any() )
                {
                    var query2 = (from details in entities.CAT_L3
                                  where details.COMPCODE == searchDetails.COMPCODE &&
                                  details.CATCODE == searchDetails.CATCODE &&
                                  details.CATCODE_L2 == searchDetails.CATCODE_L2 &&
                                  details.CATCODE_L3 == searchDetails.CATCODE_L3 
                                  select details);

                    CAT_L3 catDetails = query2.First();
                    catDetails.CHANGED = 1;
                    catDetails.CHANGEDDATE = DateTime.Now;
                    catDetails.REMOVE = 1;

                    entities.SaveChanges();
                }
                else
                    return false;
            }

            return true;
        }

        public List<CAT_L3> GetAllCategoryDetails(CAT_L3 searchDetails, int option)
        {
            List<CAT_L3> details = new List<CAT_L3>();

            //Get All details
            if (option == 1)
            {
                using (entities = new CompuLinEntityModelEntities())
                {
                    var query = (from info in entities.CAT_L3
                                 where info.COMPCODE == searchDetails.COMPCODE &&
                                 info.REMOVE == 0
                                 select info);

                    if (query.Any())
                        details = query.ToList();

                }
            }
            //Get All Category Details By Id
            else if (option == 2)
            {
                using (entities = new CompuLinEntityModelEntities())
                {
                    var query = (from info in entities.CAT_L3
                                 where info.REMOVE == 0 &&
                                 info.CATCODE_L2 == searchDetails.CATCODE_L2 &&
                                 info.CATCODE == searchDetails.CATCODE &&
                                 info.COMPCODE == searchDetails.COMPCODE &&
                                  info.CATCODE_L3 == searchDetails.CATCODE_L3 
                                 select info);

                    if (query.Any())
                        details = query.ToList();

                }
            }
            //GetAllCategoryDetailsByMasterId
            else if (option == 3)
            {
                using (entities = new CompuLinEntityModelEntities())
                {
                    var query = (from info in entities.CAT_L3
                                 where info.REMOVE == 0 && 
                                  info.CATCODE == searchDetails.CATCODE &&
                                  info.COMPCODE == searchDetails.COMPCODE 
                                 select info);

                    if (query.Any())
                        details = query.ToList();
                }
            }
            //GetAllCategoryDetailsByMasterId
            else if (option == 4)
            {
                using (entities = new CompuLinEntityModelEntities())
                {
                    var query = (from info in entities.CAT_L3
                                 where info.REMOVE == 0 &&
                                  info.CATCODE == searchDetails.CATCODE &&
                                  info.COMPCODE == searchDetails.COMPCODE &&
                                   info.CATCODE_L2 == searchDetails.CATCODE_L2 
                                 select info);

                    if (query.Any())
                        details = query.ToList();
                }
            }

            return details;
        }

       

      
       
    }
}
