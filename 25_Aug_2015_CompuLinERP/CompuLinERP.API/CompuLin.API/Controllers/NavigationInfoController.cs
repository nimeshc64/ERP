using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompuLinERP.API.Controllers
{
    public class NavigationInfoController
    {
        CompuLinEntityModelEntities entities;

        public List<NAVIGATION> GetAllNavigationInfo()
        {
            List<NAVIGATION> allNavigationInfo = new List<NAVIGATION>();

            using (entities = new CompuLinEntityModelEntities())
            {
                var query = (from logInfo in entities.NAVIGATIONs
                             orderby logInfo.SEQ ascending
                             select logInfo);

                if (query.Any())
                    allNavigationInfo = query.ToList();
            }

            return allNavigationInfo;
        }
    }
}
