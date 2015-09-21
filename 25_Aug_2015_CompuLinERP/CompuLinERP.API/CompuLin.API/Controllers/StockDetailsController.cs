using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompuLinERP.API.Controllers
{
    public class StockDetailsController
    {
        CompuLinEntityModelEntities entities;

        public bool InsertNewDetails(STOCK details)
        {
            using (entities = new CompuLinEntityModelEntities())
            {
                var query = (from info in entities.STOCKs                            
                             select info);

                entities.STOCKs.Add(details);
                entities.SaveChanges();
            }

            return true;
        }

        

    }
}
