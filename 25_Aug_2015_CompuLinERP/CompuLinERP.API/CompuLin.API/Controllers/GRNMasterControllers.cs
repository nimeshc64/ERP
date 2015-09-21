using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompuLinERP.API.Controllers
{
    
    public class GRNMasterControllers
    {
        CompuLinEntityModelEntities entities;

        public bool InsertNewDetailstoMast(GRN_MAST  details)
        {
            using (entities = new CompuLinEntityModelEntities())
            {
                var query = (from info in entities.GRN_MAST
                             select info);

                entities.GRN_MAST.Add(details);
                entities.SaveChanges();
            }

            return true;
        }
    }
}
