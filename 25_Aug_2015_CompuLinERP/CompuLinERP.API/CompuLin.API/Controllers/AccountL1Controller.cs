using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompuLinERP.API.Controllers
{
    public class AccountL1Controller
    {
        CompuLinEntityModelEntities entities;

        public bool InsertNewCategory(ACC_CAT_L1 details)
        {
            using (entities = new CompuLinEntityModelEntities())
            {
                var query = (from info in entities.ACC_CAT_L1
                             select info);


                details.CHANGED = 0;
                details.CHANGED_DATE = DateTime.Now;

                entities.ACC_CAT_L1.Add(details);
                entities.SaveChanges();
            }

            return true;
        }

        public List<ACC_CAT_L1> GetAllAccountDetails(ACC_CAT_L1 searchDetails, int option)
        {
            List<ACC_CAT_L1> details = new List<ACC_CAT_L1>();

            //Get All details
            if (option == 1)
            {
                using (entities = new CompuLinEntityModelEntities())
                {
                    var query = (from info in entities.ACC_CAT_L1
                                 where info.COMPCODE == searchDetails.COMPCODE &&
                                 info.REMOVE == 0
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
                    var query = (from info in entities.ACC_CAT_L1
                                 where info.REMOVE == 0 &&
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
                    var query = (from info in entities.ACC_CAT_L1
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

        public bool DeleteAccountDetails(ACC_CAT_L1 searchDetails)
        {
            using (entities = new CompuLinEntityModelEntities())
            {
                var query = (from details in entities.ACC_CAT_L1
                             where details.CAT_L1 == searchDetails.CAT_L1 &&
                             details.COMPCODE == searchDetails.COMPCODE &&
                             details.REMOVE == 0
                             select details);

                if (query.Any())
                {
                    var query2 = (from details in entities.ACC_CAT_L1
                                  where details.CAT_L1 == searchDetails.CAT_L1 &&
                                  details.COMPCODE == searchDetails.COMPCODE
                                  select details);

                    ACC_CAT_L1 catDetails = query2.First();
                    catDetails.CHANGED = 1;
                    catDetails.CHANGED_DATE = DateTime.Now;
                    catDetails.REMOVE = 1;

                    entities.SaveChanges();
                }
                else
                    return false;
            }

            return true;
        }


        public bool UpdateDetailsAcc(ACC_CAT_L1 searchDetails, ACC_CAT_L1 details)
        {
            using (entities = new CompuLinEntityModelEntities())
            {
                var query = (from logInfo in entities.ACC_CAT_L1
                             where logInfo.CAT_L1 == searchDetails.CAT_L1 &&                             
                             logInfo.COMPCODE == searchDetails.COMPCODE
                             select logInfo);

                if (query.Any())
                {
                    entities.ACC_CAT_L1.Remove(query.First());
                    entities.SaveChanges();

                    details.CHANGED = 0;
                    details.CHANGED_DATE = DateTime.Now;

                    entities.ACC_CAT_L1.Add(details);
                    entities.SaveChanges();
                }
            }

            return true;
        }


    }
}
