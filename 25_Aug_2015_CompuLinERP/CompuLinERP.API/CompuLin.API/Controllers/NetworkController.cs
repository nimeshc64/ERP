using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompuLinERP.API.Controllers
{
    public class NetworkController
    {
        CompuLinEntityModelEntities entities;

        public List<NETWORK> GetAllNetworkDetails()
        {
            List<NETWORK> allCompanies = new List<NETWORK>();

            using (entities = new CompuLinEntityModelEntities())
            {
                var query = (from logInfo in entities.NETWORKs 
                             orderby logInfo.COMPCODE ascending
                            select logInfo);

                if (query.Any())
                    allCompanies = query.ToList();
            }

            return allCompanies;
        }

        public List<NETWORK> GetNetworkDetailsById(string compcode)
        {
            List<NETWORK> allCompanies = new List<NETWORK>();

            using (entities = new CompuLinEntityModelEntities())
            {
                var query = (from logInfo in entities.NETWORKs where logInfo.COMPCODE == compcode
                             orderby logInfo.COMPCODE ascending
                             select logInfo);

                if (query.Any())
                    allCompanies = query.ToList();
            }

            return allCompanies;
        }
    }
}
