using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompuLinERP.API.Controllers
{
    public class AccountL2Controller
    {
        CompuLinEntityModelEntities entities;

        public bool InsertNewCategoryL2(ACC_CAT_L2 details)
        {
            using (entities = new CompuLinEntityModelEntities())
            {
                var query = (from info in entities.ACC_CAT_L2
                             select info);


                details.CHANGED = 0;
                details.CHANGED_DATE = DateTime.Now;

                entities.ACC_CAT_L2.Add(details);
                entities.SaveChanges();
            }

            return true;
        }

        public List<ACC_CAT_L2> GetAllAccountDetailsL2(ACC_CAT_L2 searchDetails, int option)
        {
            List<ACC_CAT_L2> details = new List<ACC_CAT_L2>();

            //Get All details
            if (option == 1)
            {
                using (entities = new CompuLinEntityModelEntities())
                {
                    var query = (from info in entities.ACC_CAT_L2
                                 where info.COMPCODE == searchDetails.COMPCODE &&
                                 info.REMOVE == 0 &&
                                 info.CAT_L1 == searchDetails.CAT_L1
                                 select info);

                    if (query.Any())
                        details = query.ToList();

                }
            }

            //Get All details by code
            else if (option == 2)
            {
                using (entities = new CompuLinEntityModelEntities())
                {
                    var query = (from info in entities.ACC_CAT_L2
                                 where info.REMOVE == 0 &&
                                 info.CAT_L2 == searchDetails.CAT_L2 &&
                                 info.CAT_L1 == searchDetails.CAT_L1 &&
                                 info.COMPCODE == searchDetails.COMPCODE
                                 select info);

                    if (query.Any())
                        details = query.ToList();

                }
            }
            //Get All Category Details By Master Id
            else if (option == 3)
            {
                using (entities = new CompuLinEntityModelEntities())
                {
                    var query = (from info in entities.ACC_CAT_L2
                                 where info.REMOVE == 0 &&
                                 info.CAT_L1 == searchDetails.CAT_L1 &&
                                 info.COMPCODE == searchDetails.COMPCODE
                                 select info);

                    if (query.Any())
                        details = query.ToList();

                }
            }

            return details;
        }
        public bool UpdateDetails(ACC_CAT_L2 searchDetails, ACC_CAT_L2 details)
        {
            using (entities = new CompuLinEntityModelEntities())
            {
                var query = (from logInfo in entities.ACC_CAT_L2
                             where logInfo.CAT_L1 == searchDetails.CAT_L1 &&
                             logInfo.CAT_L2 == searchDetails.CAT_L2 &&
                             logInfo.COMPCODE == searchDetails.COMPCODE
                             select logInfo);

                if (query.Any())
                {
                    entities.ACC_CAT_L2.Remove(query.First());
                    entities.SaveChanges();

                    details.CHANGED = 1;
                    details.CHANGED_DATE = DateTime.Now;
                    details.REMOVE = 0;

                    entities.ACC_CAT_L2.Add(details);
                    entities.SaveChanges();
                }
            }

            return true;
        }

        public bool DeleteCategory(ACC_CAT_L2 searchDetails)
        {
            using (entities = new CompuLinEntityModelEntities())
            {
                var query = (from details in entities.ACC_CAT_L2
                             where details.CAT_L2 == searchDetails.CAT_L2 &&
                             details.COMPCODE == searchDetails.COMPCODE &&
                             details.CAT_L1 == searchDetails.CAT_L1 &&
                             details.REMOVE == 0
                             select details);

                //if (!query.Any())
                //{
                //    var query2 = (from details in entities.ACC_CAT_L2
                //                  where details.COMPCODE == searchDetails.COMPCODE &&
                //                  details.CAT_L1 == searchDetails.CAT_L1 &&
                //                  details.CAT_L2 == searchDetails.CAT_L2
                //                  select details);

                //    ACC_CAT_L2 catDetails = query2.First();
                //    catDetails.CHANGED = 1;
                //    catDetails.CHANGED_DATE = DateTime.Now;
                //    catDetails.REMOVE = 1;

                //    entities.SaveChanges();
                //}
                //else
                //    return false;
                ACC_CAT_L2 catDetails = query.First();
                catDetails.CHANGED = 1;
                catDetails.CHANGED_DATE = DateTime.Now;
                catDetails.REMOVE = 1;

                entities.SaveChanges();
            }

            return true;
        }

        
    }
}
