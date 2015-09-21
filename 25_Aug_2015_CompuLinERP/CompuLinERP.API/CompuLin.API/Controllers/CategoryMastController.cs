using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompuLinERP.API.Controllers
{
    public class CategoryMastController
    {
        CompuLinEntityModelEntities entities;

        public bool InsertNewCategory(CAT_MAST details)
        {
            using (entities = new CompuLinEntityModelEntities())
            {
                var query = (from info in entities.CAT_MAST                             
                             select info);

                
                    details.CHANGED = 0;
                    details.CHANGEDDATE = DateTime.Now;

                    entities.CAT_MAST.Add(details);
                    entities.SaveChanges();
                }            

            return true;
        }

        public bool UpdateDetails(CAT_MAST searchDetails, CAT_MAST details)
        {
            using (entities = new CompuLinEntityModelEntities())
            {
                var query = (from logInfo in entities.CAT_MAST
                             where logInfo.CAT_CODE == searchDetails.CAT_CODE &&                             
                             logInfo.COMPCODE == searchDetails.COMPCODE
                             select logInfo);

                if (query.Any())
                {
                    entities.CAT_MAST.Remove(query.First());
                    entities.SaveChanges();

                    details.CHANGED = 0;
                    details.CHANGEDDATE = DateTime.Now;

                    entities.CAT_MAST.Add(details);
                    entities.SaveChanges();
                }
            }

            return true;
        }

        public bool DeleteCategory(CAT_MAST searchDetails)
        {
            using (entities = new CompuLinEntityModelEntities())
            {
                var query = (from details in entities.CAT_L2
                              where details.COMPCODE == searchDetails.COMPCODE &&
                              details.CATCODE == searchDetails.CAT_CODE                               
                              select details);

                if (!query.Any() )
                {
                    var query2 = (from details in entities.CAT_MAST
                                  where details.COMPCODE == searchDetails.COMPCODE &&
                                  details.CAT_CODE == searchDetails.CAT_CODE 
                                  select details);

                    CAT_MAST catDetails = query2.First();
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

        public List<CAT_MAST> GetAllCategoryDetails(CAT_MAST searchDetails, int option)
        {
            List<CAT_MAST> details = new List<CAT_MAST>();
             //Get All details
            if (option == 1)
            {
                using (entities = new CompuLinEntityModelEntities())
                {

                    var query = (from info in entities.CAT_MAST
                                 where info.COMPCODE == searchDetails.COMPCODE &&
                                 info.REMOVE == 0
                                 select info);

                    if (query.Any())
                        details = query.ToList();

                }
            }
            //GetAllCategoryDetailsById
            else if(option ==2)
            {
                using (entities = new CompuLinEntityModelEntities())
                {
                    var query = (from info in entities.CAT_MAST
                                 where info.REMOVE == 0 && 
                                 info.COMPCODE == searchDetails.COMPCODE &&
                                 info.CAT_CODE == searchDetails.CAT_CODE 
                                 select info);

                    if (query.Any())
                        details = query.ToList();

                }
            }

            return details;
        }

       
    }
}
