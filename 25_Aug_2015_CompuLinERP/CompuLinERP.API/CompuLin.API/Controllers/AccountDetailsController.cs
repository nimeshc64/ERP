using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompuLinERP.API.Controllers
{
    public class AccountDetailsController
    {
        CompuLinEntityModelEntities entities;

        public bool InsertAccDetails(GL details)
        {
            using (entities = new CompuLinEntityModelEntities())
            {
                var query = (from info in entities.GLs
                             select info);

                details.CHANGED_DATE = DateTime.Now;

                entities.GLs.Add(details);
                entities.SaveChanges();
            }

            return true;
        }

        public List<GL> GetAllAccountDetailsbySearch(GL searchDetails, int option)
        {
            List<GL> details = new List<GL>();
            //Get All details
            if (option == 1)
            {
                using (entities = new CompuLinEntityModelEntities())
                {
                    var query = (from info in entities.GLs
                                 where info.COMPCODE == searchDetails.COMPCODE &&
                                 info.GLCODE == searchDetails.GLCODE
                                 select info);

                    if (query.Any())
                        details = query.ToList();

                }
            } else if(option == 2)
            {
                using (entities = new CompuLinEntityModelEntities())
                {
                    var query = (from info in entities.GLs
                                 where info.COMPCODE == searchDetails.COMPCODE &&
                                 info.IS_BANK==1 
                                 select info);

                    if (query.Any())
                        details = query.ToList();

                }
            } else if(option == 3)
            {
                using (entities = new CompuLinEntityModelEntities())
                {
                    var query = (from info in entities.GLs
                                 where info.COMPCODE == searchDetails.COMPCODE &&
                                 info.IS_CASHBOOK==1 
                                 select info);

                    if (query.Any())
                        details = query.ToList();

                }
            }
            else if (option == 4)
            {
                using (entities = new CompuLinEntityModelEntities())
                {
                    var query = (from info in entities.GLs
                                 where info.COMPCODE == searchDetails.COMPCODE &&
                                 info.IS_PROTECTED == 1
                                 select info);

                    if (query.Any())
                        details = query.ToList();

                }
            }
            return details;
        }

        public bool DeleteAccDetails(GL details)
        {
            GL searchResult = new GL();
            using (entities = new CompuLinEntityModelEntities())
            {
                var query = (from info in entities.GLs
                             where info.COMPCODE == details.COMPCODE &&
                             info.GLCODE == details.GLCODE
                             select info);
                if (query.Any())
                {
                    searchResult = query.ToList().First();

                    entities.GLs.Remove(searchResult);
                    entities.SaveChanges();
                    return true;
                }
                else {
                    return false;
                }

            }
        }

        public bool UpdateAccDetails(GL search, GL details)
        {
            GL searchResult = new GL();
            using (entities = new CompuLinEntityModelEntities())
            {
                var query = (from info in entities.GLs
                             where info.COMPCODE == search.COMPCODE &&
                             info.GLCODE == search.GLCODE
                             select info);
                if (query.Any())
                {
                    searchResult = query.First();

                    entities.GLs.Remove(searchResult);
                    entities.SaveChanges();

                    details.CHANGED_DATE = DateTime.Now;

                    entities.GLs.Add(details);
                    entities.SaveChanges();

                    return true;
                }
                else
                {
                    return false;
                }

            }
        }
    }
}
