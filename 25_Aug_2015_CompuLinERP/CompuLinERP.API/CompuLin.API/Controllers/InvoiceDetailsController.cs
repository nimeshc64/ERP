using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompuLinERP.API.Controllers
{
    public class InvoiceDetailsController
    {
        CompuLinEntityModelEntities entities;

        public bool InsertNewDetails(INV_DETAIL details)
        {
            using (entities = new CompuLinEntityModelEntities())
            {
                var query = (from info in entities.INV_DETAIL                            
                             select info);

                entities.INV_DETAIL.Add(details);
                entities.SaveChanges();
            }

            return true;
        }

      

    }
}
