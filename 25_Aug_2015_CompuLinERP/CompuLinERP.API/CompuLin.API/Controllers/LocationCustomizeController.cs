using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompuLinERP.API.Controllers
{
    public class LocationCustomizeController
    {
        CompuLinEntityModelEntities entities;

        public bool InsertNewDetails(LOCA_DETAIL details)
        {
            using (entities = new CompuLinEntityModelEntities())
            {
                var query = (from info in entities.LOCA_DETAIL
                             select info);

                entities.LOCA_DETAIL.Add(details);
                entities.SaveChanges();
            }

            return true;
        }

        public bool UpdateDetails(LOCA_DETAIL searchdetails, LOCA_DETAIL details)
        {
            using (entities = new CompuLinEntityModelEntities())
            {
                var query = (from logInfo in entities.LOCA_DETAIL
                             where logInfo.COMPCODE == searchdetails.COMPCODE &&
                             logInfo.LOCACODE == searchdetails.LOCACODE
                             select logInfo);

                if (query.Any())
                {
                    entities.LOCA_DETAIL.Remove(query.First());
                    entities.SaveChanges();

                    entities.LOCA_DETAIL.Add(details);
                    entities.SaveChanges();
                }
            }

            return true;
        }

        public bool DeleteDetails(string code, string compCode)
        {
            using (entities = new CompuLinEntityModelEntities())
            {
                var query = (from details in entities.LOCA_DETAIL
                             where details.COMPCODE == compCode &&
                             details.LOCACODE == code
                             select details);

                if (query.Any())
                {
                    entities.LOCA_DETAIL.Remove(query.First());
                    entities.SaveChanges();
                }
                else
                    return false;
            }

            return true;
        }

        public List<LOCA_DETAIL> GetAllDetails(string netId)
        {
            List<LOCA_DETAIL> details = new List<LOCA_DETAIL>();

            using (entities = new CompuLinEntityModelEntities())
            {
                var query = (from info in entities.LOCA_DETAIL
                             where info.COMPCODE == netId
                             select info);

                if (query.Any())
                    details = query.ToList();
            }

            return details;
        }

        public List<USERINFO> GetAllUsers()
        {
            List<USERINFO> details = new List<USERINFO>();
            using (entities = new CompuLinEntityModelEntities())
            {
                var query = (from info in entities.USERINFOes
                             select info).Distinct();     
                if (query.Any())
                    details = query.ToList();
            }
            return details;
        }

        public List<LOCA_DETAIL> GetAllLocationDetails(string userId)
        {
            List<LOCA_DETAIL> details = new List<LOCA_DETAIL>();

            if (userId == "Admin")
            {
                using (entities = new CompuLinEntityModelEntities())
                {
                    var query = (from info in entities.LOCA_DETAIL                               
                                 select info);
                    if (query.Any())
                        details = query.ToList();
                }
            }
            else
            {
                using (entities = new CompuLinEntityModelEntities())
                {
                    var query = (from info in entities.LOCA_DETAIL
                                 where info.USERCODE == userId
                                 select info);
                    if (query.Any())
                        details = query.ToList();
                }
            }
            
            return details;
        }

    }
}
