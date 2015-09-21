using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompuLinERP.API.Controllers
{
    public class GRNDetailControllers
    {
        CompuLinEntityModelEntities entities;

        public bool InsertNewDetails(GRN_DETAIL details)
        {
            using (entities = new CompuLinEntityModelEntities())
            {
                var query = (from info in entities.GRN_DETAIL
                             select info);

                entities.GRN_DETAIL.Add(details);
                entities.SaveChanges();
            }

            return true;
        }
    }
}
