using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompuLinERP.API.Controllers
{
    public class AccountL4Controller
    {
        CompuLinEntityModelEntities entities;

        public bool InsertNewCategory(ACC_CAT_L4 details)
        {
            using (entities = new CompuLinEntityModelEntities())
            {
                var query = (from info in entities.ACC_CAT_L4
                             select info);

                details.CHANGED = 0;
                details.CHANGED_DATE = DateTime.Now;

                entities.ACC_CAT_L4.Add(details);
                entities.SaveChanges();
            }

            return true;
        }

        public bool UpdateDetails(ACC_CAT_L4 searchDetails, ACC_CAT_L4 details)
        {
            using (entities = new CompuLinEntityModelEntities())
            {
                var query = (from logInfo in entities.ACC_CAT_L4
                             where logInfo.CAT_L1 == searchDetails.CAT_L1 &&
                             logInfo.CAT_L2 == searchDetails.CAT_L2 &&
                             logInfo.COMPCODE == searchDetails.COMPCODE &&
                             logInfo.CAT_L3 == searchDetails.CAT_L3 &&
                             logInfo.CAT_L4 == searchDetails.CAT_L4
                             select logInfo);

                if (query.Any())
                {
                    ACC_CAT_L4 catDetails = query.First();

                    entities.ACC_CAT_L4.Remove(query.First());
                    entities.SaveChanges();

                    details.CHANGED = 1;
                    details.CHANGED_DATE = DateTime.Now;
                    details.REMOVE = 0;

                    entities.ACC_CAT_L4.Add(details);
                    entities.SaveChanges();

                }
            }

            return true;
        }

        public bool DeleteCategory(ACC_CAT_L4 searchDetails)
        {
            using (entities = new CompuLinEntityModelEntities())
            {
                var query = (from details in entities.ACC_CAT_L4
                             where details.CAT_L2 == searchDetails.CAT_L2 &&
                             details.COMPCODE == searchDetails.COMPCODE &&
                             details.CAT_L1 == searchDetails.CAT_L1 &&
                             details.CAT_L3 == searchDetails.CAT_L3 &&
                             details.CAT_L4 == searchDetails.CAT_L4 &&
                             details.REMOVE == 0
                             select details);

                //if (!query.Any())
                //{
                //    var query2 = (from details in entities.ACC_CAT_L4
                //                  where details.COMPCODE == searchDetails.COMPCODE &&
                //                  details.CAT_L1 == searchDetails.CAT_L1 &&
                //                  details.CAT_L2 == searchDetails.CAT_L2 &&
                //                  details.CAT_L3 == searchDetails.CAT_L3 &&
                //                  details.CAT_L4 == searchDetails.CAT_L4
                //                  select details);

                //    ACC_CAT_L4 catDetails = query2.First();
                //    catDetails.CHANGED = 1;
                //    catDetails.CHANGED_DATE = DateTime.Now;
                //    catDetails.REMOVE = 1;

                //    entities.SaveChanges();
                //}
                //else
                //    return false;
                ACC_CAT_L4 catDetails = query.First();
                catDetails.CHANGED = 1;
                catDetails.CHANGED_DATE = DateTime.Now;
                catDetails.REMOVE = 1;

                entities.SaveChanges();
            }

            return true;
        }

        public List<ACC_CAT_L4> GetAllAccountDetails(ACC_CAT_L4 searchDetails, int option)
        {
            List<ACC_CAT_L4> details = new List<ACC_CAT_L4>();

            //Get All details
            if (option == 1)
            {
                using (entities = new CompuLinEntityModelEntities())
                {
                    var query = (from info in entities.ACC_CAT_L4
                                 where info.COMPCODE == searchDetails.COMPCODE &&
                                 info.REMOVE == 0
                                 select info);

                    if (query.Any())
                        details = query.ToList();

                }
            }
            //GetAllCategoryDetailsById
            else if (option == 2)
            {
                using (entities = new CompuLinEntityModelEntities())
                {
                    var query = (from info in entities.ACC_CAT_L4
                                 where info.REMOVE == 0 &&
                                 info.CAT_L2 == searchDetails.CAT_L2 &&
                                 info.CAT_L1 == searchDetails.CAT_L1 &&
                                 info.COMPCODE == searchDetails.COMPCODE &&
                                  info.CAT_L3 == searchDetails.CAT_L3 &&
                                  info.CAT_L4 == searchDetails.CAT_L4
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
                    var query = (from info in entities.ACC_CAT_L4
                                 where info.REMOVE == 0 &&
                                  info.CAT_L1 == searchDetails.CAT_L1 &&
                                  info.COMPCODE == searchDetails.COMPCODE
                                 select info);

                    if (query.Any())
                        details = query.ToList();
                }
            }
            //GetAllCategoryDetailsByL2Id
            else if (option == 4)
            {
                using (entities = new CompuLinEntityModelEntities())
                {
                    var query = (from info in entities.ACC_CAT_L4
                                 where info.REMOVE == 0 &&
                                  info.CAT_L1 == searchDetails.CAT_L1 &&
                                  info.COMPCODE == searchDetails.COMPCODE &&
                                  info.CAT_L2 == searchDetails.CAT_L2
                                 select info);

                    if (query.Any())
                        details = query.ToList();
                }
            }
            //GetAllCategoryDetailsByL3Id
            else if (option == 5)
            {
                using (entities = new CompuLinEntityModelEntities())
                {
                    var query = (from info in entities.ACC_CAT_L4
                                 where info.REMOVE == 0 &&
                                  info.CAT_L1 == searchDetails.CAT_L1 &&
                                  info.COMPCODE == searchDetails.COMPCODE &&
                                  info.CAT_L2 == searchDetails.CAT_L2 &&
                                  info.CAT_L3 == searchDetails.CAT_L3
                                 select info);

                    if (query.Any())
                        details = query.ToList();
                }
            }

            return details;
        }
    }
}
